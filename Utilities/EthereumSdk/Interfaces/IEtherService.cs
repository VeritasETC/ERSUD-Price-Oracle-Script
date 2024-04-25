using EthereumSdk.Models;
using NBitcoin;
using Nethereum.HdWallet;
using Transaction = Nethereum.RPC.Eth.DTOs.Transaction;
using TransactionReceipt = Nethereum.RPC.Eth.DTOs.TransactionReceipt;
using Nethereum.Web3.Accounts;
using System.Threading.Tasks;
using System.Numerics;

namespace EthereumSdk.Interfaces
{
    public interface IEtherService
    {
        bool CheckAddress(string address);
        Task<decimal> GetTokenBalanceAsync(string address, string tokenContractAddress, string url, int decimalPlaces = 18);
        Task<decimal> GetPlaymatesBalanceAsync(string address, string tokenContractAddress, string url, int decimalPlaces = 18);
        Task<decimal> GetETHBalanceAsync(string address, string url);
        Task<GasTrackModel> GasTrackAsync(string etherscanUrl);
        Task<TransactionReceipt> GetTransactionReceiptByHash(string accountKey, string url, int chainId, string transactionHash);
        Task<Transaction> GetTransactionByHash(string accountKey, string url, int chainId, string transactionHash);

        Task<string> CreateNewAddressAsync(string forwardContractAddress, string forwardFactoryContractAddress, string accountKey, string url, int chainId, long index, string gas = "", string gasPrice = "", int? nonce = null);

        Task<string> FlushTokenAsync(string accountKey, string url, int chainId, string address, string tokenContractAddress, string gas = "", string gasPrice = "", int? nonce = null);

        Task<Transaction> GetTransactionByHash(string url, string transactionHash);
        Task<string> GetDestinationAddressInContract(string accountKey, string url, int chainId, string contractAddress);

        Task<EtherscanTransactionModel> GetErc20TransactionsByAddress(string etherscanUrl, string address);
        Task<TransactionReceipt> GetTransactionReceiptByHash(string url, string transactionHash);
        Account CreateWallet(string password = "", int index = 0, string words = "");
        Task<string> SendEther(string accountKey, string url, int chainId, string toAddr, BigInteger amount, BigInteger? gastLimit = null, BigInteger? gasPrice = null, bool useLegacy = false);
        Task<string> TransferErc20Token(string accountKey, string url, int chainId, string contractAddress, string toAddr, BigInteger tokenAmount, BigInteger? gastLimit = null, BigInteger? gasPrice = null, bool useLegacy = false);
        Account GetAccount(string password, int index, string words);

        Account GetAccount(string accountKey, int chainId);
        string GetMnemonics();
        Task<string> ApproveErc20Token(string accountKey, string url, int chainId, string contractAddress, string toAddr, BigInteger tokenAmount, BigInteger? gastLimit = null, BigInteger? gasPrice = null, bool useLegacy = false);
        bool IsValidMessageAddress(string message, string signature, string address);
    }
}
