using DTO.Models;
using EthereumSdk.Interfaces;
using RateScript.Interface;
using Repository.Interfaces.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Binance.Net.Clients;
using Ethereum.Contract.Interfaces;
using Common.Helpers;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;

namespace RateScript.Implementation
{
    public class RateService : IRateService
    {
        private readonly IRepositoryUnit _repository;
        private IContractHandler _contractHandler;
        public RateService(IRepositoryUnit _repository, IContractHandler contractHandler)
        {
            this._repository = _repository;
            _contractHandler = contractHandler;
        }

        public async Task<string> UpdateRate()
        {
            try
            {
                var rate = await _repository.RateRepo.GetRate();
                var getBinanceRate = await getAPILatestRate();
                if (rate == null)
                {
                    _repository.RateRepo.Create(new()
                    {
                        latestRate = getBinanceRate,
                        CreatedAt = DateTime.UtcNow,
                        IsDeleted = false,
                        Percentage = 2,
                        Uuid = Guid.NewGuid(),
                        contractLatestRate = getBinanceRate
                    });

                    await _repository.SaveAsync();
                    var callContract = await RateUpdateFunction(getBinanceRate);
                }
                else
                {
                    //Get 2% diffrenec

                    decimal rateDifference = (rate.contractLatestRate > getBinanceRate) ?
                                             (rate.contractLatestRate - getBinanceRate) / rate.contractLatestRate * 100 :
                                             (getBinanceRate - rate.contractLatestRate);


                    //Get Percentage from Database
                    if (rateDifference >= rate.Percentage)
                    {
                        var callContract = await RateUpdateFunction(getBinanceRate);
                        rate.contractLatestRate = getBinanceRate;
                    }

                    rate.latestRate = getBinanceRate;
                    rate.UpdatedAt = DateTime.UtcNow;
                    _repository.RateRepo.Update(rate);
                    await _repository.SaveAsync();

                }



                return "Update Successfully";
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        private async Task<decimal> getAPILatestRate()
        {
            var restClient = new BinanceRestClient();
            var tickerResult = await restClient.SpotApi.ExchangeData.GetTickerAsync("ETCUSDT");
            return tickerResult.Data.LastPrice;
        }

        private async Task<TransactionReceipt> RateUpdateFunction(decimal getBinanceRate)
        {
            var result = await _contractHandler.SetETHClassicRate(nodeUrl: AppSettingHelper.GetETHClassicNodeUrl(),
                                                                                                 chainId: AppSettingHelper.GetETHClassicChainId(),
                                                                                                 accountPrivateKey: AppSettingHelper.GetETHClassicPrivateKey(),
                                                                                                 contractAddress: AppSettingHelper.GetETHClassicSmartAddress(),
                                                                                                 ethClassicRate: new()
                                                                                                 {
                                                                                                     amount = Web3.Convert.ToWei(getBinanceRate),
                                                                                                 },
                                                                                                 UseLegacyAsDefault: true,
                                                                                                 cancellationToken: null);


            return result;
        }
    }
}
