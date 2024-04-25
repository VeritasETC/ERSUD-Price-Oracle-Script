using Ethereum.Contract.Models;
using Nethereum.RPC.Eth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ethereum.Contract.Interfaces
{
    public interface IContractHandler
    {
        Task<TransactionReceipt> AddLiquidityRequestAndWaitForReceiptAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, AddLiquidityFunction addLiquidityFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> CreatePairRequestAndWaitForReceiptAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, CreatePairFunction createPairFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null);
        Task<string> FactoryRequestAndWaitForReceiptAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, FactoryFunction factoryFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> SwapRequestAndWaitForReceiptAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, SwapFunction SwapFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> AddLiquidityEthRequestAndWaitForReceiptAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, AddLiquidityEthFunction addLiquidityFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> GetPriceUSDTAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, getUSDTAmountout getUSDTAmountout, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> MintTokenRequestAndWaitForReceiptAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, MintAssetsFunction mintAssetsFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> AddUserDetailsRequestAndWaitForReceiptAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, AddUserDetailsFunction addUserDetailsFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> AddUserRLCRecievingDetailsRequestAndWaitForReceiptAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, AddUserRLCRecievingDetailsFunction addUserRLCRecievingDetailsFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> GetPendingUsersListAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, GetUsersListFunction getUsersListFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> GetUserDetailsAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, GetUserDetailsFunction getUserDetailsFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null);
        Task<GetRlcUserResponse> GetUserRlcDetailsAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, GetUserRlcDetailsFunction getUserRlcDetailsFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> UpdateUserRLCMintingRequestAndWaitForReceiptAsync(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, UpdateUserRLCMintingFunction updateUserRLCMintingFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> AddMultipleMinterRLC(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, AddMultipleMinterRLCFunction multipleMinterRLCFunction, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null);
        Task<TransactionReceipt> SetETHClassicRate(string nodeUrl, int chainId, string accountPrivateKey, string contractAddress, EthClassicRateFunction  ethClassicRate, bool UseLegacyAsDefault = false, CancellationTokenSource cancellationToken = null);

    }
}
