
using RateScript.Interface;
using Repository.Interfaces.Unit;
using Binance.Net.Clients;
using Ethereum.Contract.Interfaces;
using Common.Helpers;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Signer;
using Nethereum.Web3.Accounts;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Ethereum.Contract.Models;
using Hangfire;
using Org.BouncyCastle.Math;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using DTO.Models;
using System;

namespace RateScript.Implementation
{
    public class RateService : IRateService
    {
        /// <summary>
        /// Repository unit for interacting with the database.
        /// </summary>
        private readonly IRepositoryUnit _repository;

        /// <summary>
        /// Contract handler for interacting with smart contracts.
        /// </summary>
        private IContractHandler _contractHandler;
        public RateService(IRepositoryUnit repository, IContractHandler contractHandler)
        {
            _repository = repository;
            _contractHandler = contractHandler;
        }

        /// <summary>
        /// Updates the rate in the database and potentially on the Ethereum smart contract.
        /// 
        /// This method retrieves the latest rate from the Binance API, compares it to the stored
        /// rate and a threshold percentage, and updates the database accordingly. If the rate
        /// difference exceeds the threshold or a certain time has passed since the last update,
        /// it also calls the `RateUpdateFunction` to update the smart contract. Additionally,
        /// it performs liquidation checks and processes for users with insufficient collateral.
        /// 
        /// Throws an ArgumentException if an exception occurs during the update process.
        /// </summary>
        /// <returns>
        /// A string indicating successful update ("Update Successfully").
        /// </returns>
        public async Task<string> UpdateRate()
        {
            try
            {
                var rate = await _repository.RateRepo.GetRate();
                //var getBinanceRate = await getAPILatestRate();
                var getBinanceRate = await getContractRate();

                var currentTimeStamp = DateTime.UtcNow;

                if (rate == null && getBinanceRate != 0)
                {
                    Rate _rate = new Rate();
                    _rate.latestRate = getBinanceRate;
                    _rate.CreatedAt = DateTime.UtcNow;
                    _rate.ContractUpdatedAt = DateTime.UtcNow;
                    _rate.IsDeleted = false;
                    _rate.Percentage = 2;
                    _rate.UpdateHour = 1;
                    _rate.Uuid = Guid.NewGuid();
                    _rate.contractLatestRate = getBinanceRate;

                    _repository.RateRepo.Create(_rate);

                    await _repository.SaveAsync();
                    //var callContract = await RateUpdateFunction(getBinanceRate);
                    await Task.Delay(5000);
                }
                else
                {
                    if (getBinanceRate != 0)
                    {
                        var updateTime = rate.ContractUpdatedAt.AddHours(rate.UpdateHour);

                        _repository.DetachAllEntities();

                        //Get 2% diffrenec
                        decimal rateDifference = (rate.contractLatestRate > getBinanceRate) ?
                                                 (rate.contractLatestRate - getBinanceRate) / rate.contractLatestRate * 100 :
                                                 (getBinanceRate - rate.contractLatestRate);

                        //Get Percentage from Database
                        if (rateDifference >= rate.Percentage || currentTimeStamp >= updateTime)
                        {
                            //var callContract = await RateUpdateFunction(getBinanceRate);
                            rate.contractLatestRate = getBinanceRate;
                            rate.ContractUpdatedAt = DateTime.UtcNow;

                            // liquidation process

                            decimal _length = await getLoanUsersLength();

                            List<string> allLoanerAddresses = new List<string>();
                            int size = 20;
                            var _ratioLimit = Web3.Convert.ToWei(137);

                            for (int x = 0; x <= _length / size; x++)
                            {
                                allLoanerAddresses = await getLoanUsers(x, size, _ratioLimit);
                            }

                            foreach (var userAddress in allLoanerAddresses)
                            {
                                var liquidateStatus = await SwapLiquidationFunction(userAddress);
                                if (liquidateStatus.Status.Value != null && liquidateStatus.Status.Value == 1)
                                    Console.WriteLine("\tUser address: {0} is liquidated", userAddress);
                                else
                                    Console.WriteLine("\tUser address: {0} is not liquidated", userAddress);
                            }
                        }

                        rate.latestRate = getBinanceRate;
                        rate.UpdatedAt = DateTime.UtcNow;
                        _repository.RateRepo.Update(rate);
                        await _repository.SaveAsync();
                    }
                }

                if(getBinanceRate == 0)
                    return "Zero rate cannot be updated";
                else
                    return "Update Successfully";
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves the latest ETC/USDT exchange rate from the Binance API.
        /// 
        /// This method creates a `BinanceRestClient` object and uses it to call the
        /// `GetTickerAsync` method for the "ETCUSDT" symbol. The returned ticker result
        /// is then parsed to extract the latest price (LastPrice).
        /// 
        /// If an exception occurs during the API call, it logs the error message
        /// to the console and returns a default value of 0.
        /// </summary>
        /// <returns>
        /// The latest ETC/USDT exchange rate as a decimal, or 0 on error.
        /// </returns>
        private async Task<decimal> getAPILatestRate()
        {
            decimal result = 0;
            try
            {
                var restClient = new BinanceRestClient();
                var tickerResult = await restClient.SpotApi.ExchangeData.GetTickerAsync("ETCUSDT");
                result = tickerResult.Data.LastPrice;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Updates the exchange rate on the Ethereum Classic smart contract.
        /// 
        /// This method relies on the `_contractHandler` to call the
        /// `SetETHClassicRate` function. It retrieves various configuration values
        /// using `AppSettingHelper` (likely from an appsettings.json file)
        /// to construct the necessary parameters for the smart contract call.
        /// 
        /// If an exception occurs during the contract interaction, it logs the error
        /// message to the console and returns an empty `TransactionReceipt` object.
        /// </summary>
        /// <param name="getBinanceRate">The latest ETC/USDT rate obtained from Binance.</param>
        /// <returns>
        /// A `TransactionReceipt` object containing details about the smart contract
        /// transaction (empty on error).
        /// </returns>
        private async Task<TransactionReceipt> RateUpdateFunction(decimal getBinanceRate)
        {
            TransactionReceipt result = new TransactionReceipt();
            try
            {
                result = await _contractHandler.SetETHClassicRate(nodeUrl: AppSettingHelper.GetBscClientNodeUrl(),
                                                                                                     chainId: AppSettingHelper.GetBscChainId(),
                                                                                                     accountPrivateKey: AppSettingHelper.GetBSCPrivateKey(),
                                                                                                     contractAddress: AppSettingHelper.GetOracleContractAddress(),
                                                                                                     ethClassicRate: new()
                                                                                                     {
                                                                                                         amount = Web3.Convert.ToWei(getBinanceRate),
                                                                                                     },
                                                                                                     UseLegacyAsDefault: true,
                                                                                                     cancellationToken: null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Gets the total number of loan users from the Ethereum Classic vault contract.
        /// 
        /// This method uses the `_contractHandler` to call the
        /// `GetLoanUsersLength` function. It provides the Ethereum Classic node URL
        /// and the vault contract address as parameters.
        /// 
        /// If an exception occurs during the contract interaction, it logs the error
        /// message to the console and returns a default value of 0.
        /// </summary>
        /// <returns>
        /// The total number of loan users as a decimal (likely an integer value).
        /// </returns>
        private async Task<decimal> getLoanUsersLength()
        {
            decimal result = 0;
            try
            {
                result = await _contractHandler.GetLoanUsersLength(url: AppSettingHelper.GetBscClientNodeUrl(),
                                                                        contractAddress: AppSettingHelper.GetVaultContractAddress());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Gets the total number of loan users from the Ethereum Classic vault contract.
        /// 
        /// This method uses the `_contractHandler` to call the
        /// `GetLoanUsersLength` function. It provides the Ethereum Classic node URL
        /// and the vault contract address as parameters.
        /// 
        /// If an exception occurs during the contract interaction, it logs the error
        /// message to the console and returns a default value of 0.
        /// </summary>
        /// <returns>
        /// The total number of loan users as a decimal (likely an integer value).
        /// </returns>
        private async Task<decimal> getContractRate()
        {
            decimal result = 0;
            try
            {
                result = await _contractHandler.GetContractRate(url: AppSettingHelper.GetBscClientNodeUrl(),
                                                                        contractAddress: AppSettingHelper.GetOracleContractAddress());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Gets the collateral ratio for a specific loan user from the liquidation contract.
        /// 
        /// This method uses the `_contractHandler` to call the
        /// `GetCollateralRatio` function. It provides the Ethereum Classic node URL,
        /// the liquidation contract address, and the loan user's address as parameters.
        /// 
        /// If an exception occurs during the contract interaction, it logs the error
        /// message to the console and returns a default value of 0.
        /// </summary>
        /// <param name="userAddress">The address of the loan user.</param>
        /// <returns>
        /// The collateral ratio for the user as a decimal.
        /// </returns>
        private async Task<decimal> getCollateralRatio(string userAddress)
        {
            decimal result = 0;
            try
            {
                result = await _contractHandler.GetCollateralRatio(url: AppSettingHelper.GetETHClassicNodeUrl(),
                                                                        contractAddress: AppSettingHelper.GetLiquidationContractAddress(),
                                                                        userAddress: userAddress);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Retrieves a paginated list of loan user addresses from the Ethereum Classic vault contract.
        /// 
        /// This method uses the `_contractHandler` to call the `GetLoanUsers` function. It
        /// provides the Ethereum Classic node URL, the vault contract address, the page number,
        /// and the number of users per page (`size`) as parameters. This allows for fetching loan
        /// users in chunks to avoid memory limitations or gas cost issues.
        /// 
        /// If an exception occurs during the contract interaction, it logs the error message
        /// to the console and returns an empty list.
        /// </summary>
        /// <param name="page">The zero-based page number to retrieve (e.g., 0 for the first page).</param>
        /// <param name="size">The number of loan user addresses to retrieve per page.</param>
        /// <returns>
        /// A list of loan user addresses as strings.
        /// </returns>
        private async Task<List<string>> getLoanUsers(int page, int size, System.Numerics.BigInteger ratio)
        {
            List<string> result = new List<string>();
            try
            {
                result = await _contractHandler.GetLoanUsers(url: AppSettingHelper.GetBscClientNodeUrl(),
                                                                        contractAddress: AppSettingHelper.GetVaultContractAddress(),
                                                                        _size: size, _page: page, _ratio: ratio);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }


        /// <summary>
        /// Performs a swap liquidation for a specific loan user on the Binance Smart Chain (BSC).
        /// 
        /// This method uses the `_contractHandler` to call the `SwapLiquidate` function. It
        /// provides various configuration values to interact with the BSC network and the
        /// liquidation contract:
        ///  - `nodeUrl`: URL of the BSC node
        ///  - `chainId`: ID of the BSC chain
        ///  - `accountPrivateKey`: Private key of the account authorized to perform liquidation
        ///  - `contractAddress`: Address of the liquidation contract
        ///  - `swapLiquidation`: Object containing the user address to be liquidated (`userAddress`)
        /// 
        /// If an exception occurs during the contract interaction or transaction execution,
        /// it logs the error message to the console and returns an empty `TransactionReceipt` object.
        /// </summary>
        /// <param name="_userAddress">The address of the loan user to be liquidated.</param>
        /// <returns>
        /// A `TransactionReceipt` object containing details about the swap liquidation transaction
        /// (empty on error).
        /// </returns>
        //public static async Task SwapLiquidationFunction(string _userAddress)
        public async Task<TransactionReceipt> SwapLiquidationFunction(string _userAddress)
        
        {
            TransactionReceipt result = new TransactionReceipt();
            try
            {
                result = await _contractHandler.SwapLiquidate(nodeUrl: AppSettingHelper.GetBscClientNodeUrl(),
                                                                                                     chainId: AppSettingHelper.GetBscChainId(),
                                                                                                     accountPrivateKey: AppSettingHelper.GetBSCPrivateKey(),
                                                                                                     contractAddress: AppSettingHelper.GetLiquidationContractAddress(),
                                                                                                     swapLiquidation: new()
                                                                                                     {
                                                                                                         userAddress = _userAddress,
                                                                                                     },
                                                                                                     UseLegacyAsDefault: true,
                                                                                                     cancellationToken: null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

    }
}
