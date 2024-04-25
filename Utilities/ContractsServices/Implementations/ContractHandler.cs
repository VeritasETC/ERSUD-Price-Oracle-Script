using Ethereum.Contract.Interfaces;
using Ethereum.Contract.Models;
using Nethereum.HdWallet;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ethereum.Contract.Implementations
{
    public class ContractHandler : IContractHandler
    {
        public async Task<TransactionReceipt> CreatePairRequestAndWaitForReceiptAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, CreatePairFunction createPairFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null)
        {
            var account = new Account(accountPrivateKey, chainId);
            var web3 = new Web3(account, nodeUrl);
            web3.TransactionManager.UseLegacyAsDefault = UseLegacyAsDefault;
            var contractHandler = web3.Eth.GetContractHandler(contractAddress);

            return await contractHandler.SendRequestAndWaitForReceiptAsync(createPairFunction, cancellationToken);
        }
        public async Task<TransactionReceipt> AddLiquidityRequestAndWaitForReceiptAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, AddLiquidityFunction addLiquidityFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null)
        {
            var account = new Account(accountPrivateKey, chainId);
            var web3 = new Web3(account, nodeUrl);
            web3.TransactionManager.UseLegacyAsDefault = UseLegacyAsDefault;
            var contractHandler = web3.Eth.GetContractHandler(contractAddress);

            return await contractHandler.SendRequestAndWaitForReceiptAsync(addLiquidityFunction, cancellationToken);
        }

        public async Task<TransactionReceipt> AddLiquidityEthRequestAndWaitForReceiptAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, AddLiquidityEthFunction addLiquidityFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null)
        {
            var account = new Account(accountPrivateKey, chainId);
            var web3 = new Web3(account, nodeUrl);
            web3.TransactionManager.UseLegacyAsDefault = UseLegacyAsDefault;
            var contractHandler = web3.Eth.GetContractHandler(contractAddress);

            return await contractHandler.SendRequestAndWaitForReceiptAsync(addLiquidityFunction, cancellationToken);
        }
        public async Task<string> FactoryRequestAndWaitForReceiptAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, FactoryFunction factoryFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null)
        {
            var account = new Account(accountPrivateKey, chainId);
            var web3 = new Web3(account, nodeUrl);
            web3.TransactionManager.UseLegacyAsDefault = UseLegacyAsDefault;
            var contractHandler = web3.Eth.GetContractHandler(contractAddress);

            return await contractHandler.SendRequestAsync(factoryFunction);
            //return await contractHandler.SendRequestAndWaitForReceiptAsync(factoryFunction, cancellationToken);
        }
        public async Task<TransactionReceipt> SwapRequestAndWaitForReceiptAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, SwapFunction SwapFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null)
        {
            var account = new Account(accountPrivateKey, chainId);
            var web3 = new Web3(account, nodeUrl);
            web3.TransactionManager.UseLegacyAsDefault = UseLegacyAsDefault;
            var contractHandler = web3.Eth.GetContractHandler(contractAddress);

            return await contractHandler.SendRequestAndWaitForReceiptAsync(SwapFunction, cancellationToken);
        }
        public async Task<TransactionReceipt> GetPriceUSDTAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, getUSDTAmountout getUSDTAmountout, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null)
        {
            var account = new Account(accountPrivateKey, chainId);
            var web3 = new Web3(account, nodeUrl);
            web3.TransactionManager.UseLegacyAsDefault = UseLegacyAsDefault;
            var contractHandler = web3.Eth.GetContractHandler(contractAddress);

            return await contractHandler.SendRequestAndWaitForReceiptAsync(getUSDTAmountout, cancellationToken);
        }
        public async Task<TransactionReceipt> MintTokenRequestAndWaitForReceiptAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, MintAssetsFunction mintAssetsFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null)
        {
            var account = new Account(accountPrivateKey, chainId);
            var web3 = new Web3(account, nodeUrl);
            web3.TransactionManager.UseLegacyAsDefault = UseLegacyAsDefault;
            var contractHandler = web3.Eth.GetContractHandler(contractAddress);

            return await contractHandler.SendRequestAndWaitForReceiptAsync(mintAssetsFunction, cancellationToken);
        }
        public async Task<TransactionReceipt> AddUserDetailsRequestAndWaitForReceiptAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, AddUserDetailsFunction addUserDetailsFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null)
        {
            var account = new Account(accountPrivateKey, chainId);
            var web3 = new Web3(account, nodeUrl);
            web3.TransactionManager.UseLegacyAsDefault = UseLegacyAsDefault;
            var contractHandler = web3.Eth.GetContractHandler(contractAddress);

            return await contractHandler.SendRequestAndWaitForReceiptAsync(addUserDetailsFunction, cancellationToken);
        }
        public async Task<TransactionReceipt> AddUserRLCRecievingDetailsRequestAndWaitForReceiptAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, AddUserRLCRecievingDetailsFunction addUserRLCRecievingDetailsFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null)
        {
            var account = new Account(accountPrivateKey, chainId);
            var web3 = new Web3(account, nodeUrl);
            web3.TransactionManager.UseLegacyAsDefault = UseLegacyAsDefault;
            var contractHandler = web3.Eth.GetContractHandler(contractAddress);

            return await contractHandler.SendRequestAndWaitForReceiptAsync(addUserRLCRecievingDetailsFunction, cancellationToken);
        }
        //public async Task<List<string>> GetUserAddressesRequestAndWaitForReceiptAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, GetUsersListFunction getUsersListFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null)
        //{
        //    var account = new Account(accountPrivateKey, chainId);
        //    var web3 = new Web3(account, nodeUrl);
        //    web3.TransactionManager.UseLegacyAsDefault = UseLegacyAsDefault;
        //    var contractHandler = web3.Eth.GetContractHandler(contractAddress);

        //    return await contractHandler.SendRequestAsync(getUsersListFunction);
        //    //return await contractHandler.SendRequestAndWaitForReceiptAsync(factoryFunction, cancellationToken);
        //}
        public async Task<TransactionReceipt> GetPendingUsersListAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, GetUsersListFunction getUsersListFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null)
        {
            var account = new Account(accountPrivateKey, chainId);
            var web3 = new Web3(account, nodeUrl);
            web3.TransactionManager.UseLegacyAsDefault = UseLegacyAsDefault;
            var contractHandler = web3.Eth.GetContractHandler(contractAddress);

            return await contractHandler.SendRequestAndWaitForReceiptAsync(getUsersListFunction, cancellationToken);
        }
        public async Task<TransactionReceipt> GetUserDetailsAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, GetUserDetailsFunction getUserDetailsFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null)
        {
            var account = new Account(accountPrivateKey, chainId);
            var web3 = new Web3(account, nodeUrl);
            web3.TransactionManager.UseLegacyAsDefault = UseLegacyAsDefault;
            var contractHandler = web3.Eth.GetContractHandler(contractAddress);

            return await contractHandler.SendRequestAndWaitForReceiptAsync(getUserDetailsFunction, cancellationToken);
        }
        //public async Task<GetRlcUserResponse> GetUserRlcDetailsAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, GetUserRlcDetailsFunction getUserRlcDetailsFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null)
        //{
        //    var account = new Account(accountPrivateKey, chainId);
        //    var web3 = new Web3(account, nodeUrl);
        //    web3.TransactionManager.UseLegacyAsDefault = UseLegacyAsDefault;
        //    //var contractHandler = web3.Eth.GetContractHandler(contractAddress);
        //    string abi = File.ReadAllText(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data"), "LockingABI.json"));

        //    var contract = web3.Eth.GetContract(abi, contractAddress);

        //    var contractHandler = web3.Eth.GetContractHandler(contractAddress);
        //    return await contractHandler.QueryAsync<GetUserRlcDetailsFunction, GetRlcUserResponse>(getUserRlcDetailsFunction);
        //}
        public async Task<GetRlcUserResponse> GetUserRlcDetailsAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, GetUserRlcDetailsFunction getUserRlcDetailsFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null)
        {
            var account = new Account(accountPrivateKey, chainId);
            var web3 = new Web3(account, nodeUrl);

            web3.TransactionManager.UseLegacyAsDefault = UseLegacyAsDefault;

            string abi = File.ReadAllText(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data"), "LockingABI.json"));

            var contract = web3.Eth.GetContract(abi, contractAddress);

            var balanceFunction = contract.GetFunction("getUserRlcDetails");

            var data = await balanceFunction.CallDeserializingToObjectAsync<GetRlcUserResponse>(getUserRlcDetailsFunction.user);

            return data;
        }
        public async Task<TransactionReceipt> UpdateUserRLCMintingRequestAndWaitForReceiptAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, UpdateUserRLCMintingFunction updateUserRLCMintingFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null)
        {
            var account = new Account(accountPrivateKey, chainId);
            var web3 = new Web3(account, nodeUrl);
            web3.TransactionManager.UseLegacyAsDefault = UseLegacyAsDefault;
            var contractHandler = web3.Eth.GetContractHandler(contractAddress);

            return await contractHandler.SendRequestAndWaitForReceiptAsync(updateUserRLCMintingFunction, cancellationToken);
        }
        public async Task<TransactionReceipt> AddMultipleMinterRLC(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, AddMultipleMinterRLCFunction multipleMinterRLCFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null)
        {
            var account = new Account(accountPrivateKey, chainId);
            var web3 = new Web3(account, nodeUrl);
            web3.TransactionManager.UseLegacyAsDefault = UseLegacyAsDefault;
            var contractHandler = web3.Eth.GetContractHandler(contractAddress);

            return await contractHandler.SendRequestAndWaitForReceiptAsync(multipleMinterRLCFunction, cancellationToken);
        }

        public async Task<TransactionReceipt> SetETHClassicRate(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, EthClassicRateFunction ethClassicRate, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null)
        {
            try
            {
                var account = new Account(accountPrivateKey, chainId);
                var web3 = new Web3(account, nodeUrl);
                web3.TransactionManager.UseLegacyAsDefault = UseLegacyAsDefault;
                var contractHandler = web3.Eth.GetContractHandler(contractAddress);
                return await contractHandler.SendRequestAndWaitForReceiptAsync(ethClassicRate, cancellationToken);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}