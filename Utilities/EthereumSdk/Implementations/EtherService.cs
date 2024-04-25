﻿using EthereumSdk.Interfaces;
using EthereumSdk.Models;
using NBitcoin;
using Nethereum.HdWallet;
using Transaction = Nethereum.RPC.Eth.DTOs.Transaction;
using TransactionReceipt = Nethereum.RPC.Eth.DTOs.TransactionReceipt;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Nethereum.RPC.Eth.DTOs;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using System.Text.RegularExpressions;
using Nethereum.Util;
using System;
using System.Diagnostics;
using EthereumSdk.Forwarder;
using EthereumSdk.ForwarderFactory;
using System.Collections.Generic;
using System.Linq;
using Nethereum.Signer;

namespace EthereumSdk.Implementations
{
    internal class EtherService : IEtherService
    {
        public async Task<string> FlushTokenAsync(string accountKey, string url, int chainId, string address, string tokenContractAddress, string gas = "", string gasPrice = "", int? nonce = null)
        {
            var wallet = new Wallet(accountKey, "");
            var accounttokey = wallet.GetAccount(0);
            var account = new Account(accounttokey.PrivateKey, chainId);
            var web3 = new Web3(account, url);
            var forwarderService = new ForwarderService(web3, address);

            BigInteger? gasValue = null;
            BigInteger? gasPriceValue = null;
            BigInteger? nonceValue = null;

            if (!string.IsNullOrWhiteSpace(gas))
                gasValue = BigInteger.Parse(gas);

            if (!string.IsNullOrWhiteSpace(gasPrice))
                gasPriceValue = BigInteger.Parse(gasPrice);

            if (nonce.HasValue)
                nonceValue = BigInteger.Parse(nonce.Value.ToString());

            var flushTxHash = await forwarderService.FlushTokensRequestAsync(tokenContractAddress, gas: gasValue, gasPrice: gasPriceValue, nonce: nonceValue);

            return flushTxHash;
        }
        public async Task<string> CreateNewAddressAsync(string forwardContractAddress, string forwardFactoryContractAddress, string accountKey, string url, int chainId, long index, string gas = "", string gasPrice = "", int? nonce = null)
        {
            var wallet = new Wallet(accountKey, "");
            var accounttokey = wallet.GetAccount(0);
            var account = new Account(accounttokey.PrivateKey, chainId);
            var web3 = new Web3(account, url);
            var factoryService = new ForwarderFactoryService(web3, forwardFactoryContractAddress);
            var salt = BigInteger.Parse(index.ToString());

            BigInteger? gasValue = null;
            BigInteger? gasPriceValue = null;
            BigInteger? NonceValue = null;

            if (!string.IsNullOrWhiteSpace(gas))
                gasValue = BigInteger.Parse(gas);

            if (!string.IsNullOrWhiteSpace(gasPrice))
                gasPriceValue = BigInteger.Parse(gasPrice);

            if (nonce.HasValue)
                NonceValue = BigInteger.Parse(nonce.Value.ToString());

            var txnHash = await factoryService.CloneForwarderRequestAsync(forwardContractAddress, salt, gas: gasValue, gasPrice: gasPriceValue, nonce: NonceValue);

            return txnHash;
        }
        public async Task<string> GetDestinationAddressInContract(string accountKey, string url, int chainId, string contractAddress)
        {
            var wallet = new Wallet(accountKey, "");
            var accounttokey = wallet.GetAccount(0);
            var account = new Account(accounttokey.PrivateKey, chainId);
            var web3 = new Web3(account, url);

            var forwarderService = new ForwarderService(web3, contractAddress);
            var destinationInContract = await forwarderService.DestinationQueryAsync();

            return destinationInContract;
        }

        private DateTime FromUnixTime(long unixTime)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return epoch.AddSeconds(unixTime);
        }

        public async Task<EtherscanTransactionModel> GetErc20TransactionsByAddress(string etherscanUrl, string address)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(etherscanUrl + address);

                if (response.IsSuccessStatusCode)
                {
                    var rawData = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<EtherscanTransactionModel>(rawData);

                    if (data.Message == EtherscanConstantValues.OkMessage && data.Status == EtherscanConstantValues.SuccessStatus)
                        data.Result = JsonSerializer.Deserialize<List<EtherscanTransactionResultModel>>(data.ResultObject.ToString());

                    if (data.Result.Any())
                    {
                        for (int i = 0; i < data.Result.Count; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(data.Result[i].TimeStamp))
                            {
                                data.Result[i].TimeStampDate = FromUnixTime(long.Parse(data.Result[i].TimeStamp));
                            }

                            if (!string.IsNullOrWhiteSpace(data.Result[i].Value))
                            {
                                data.Result[i].Amount = (double)Web3.Convert.FromWei(BigInteger.Parse(data.Result[i].Value));
                            }
                        }
                    }

                    return data;
                }
                else
                {
                    return new EtherscanTransactionModel()
                    {
                        Status = EtherscanConstantValues.FailStatus,
                        Message = EtherscanConstantValues.NotOkMessage,
                        Result = null
                    };
                }
            }
        }

        public bool CheckAddress(string address)
        {
            try
            {
                if (address.Length != 42)
                {
                    return false;
                }

                var isHexString = Regex.IsMatch(address.Replace("0x", ""), @"\A\b[0-9a-fA-F]+\b\Z");

                if (!isHexString)
                    return false;

                if (!Web3.IsChecksumAddress(Web3.ToChecksumAddress(address)))
                    return false;

                var addressUtil = new AddressUtil();

                return addressUtil.IsValidEthereumAddressHexFormat(address);
            }
            catch (IndexOutOfRangeException)
            {
                // Web3.IsChecksumAddress convert hex string to byes to validate the address, if hex string is invalid then it will throw the IndexOutOfRangeException
                // but in our case it is still a invalid address
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
        public async Task<decimal> GetTokenBalanceAsync(string address, string tokenContractAddress, string url, int decimalPlaces = 18)
        {
            var web3 = new Web3(url);
            var handler = web3.Eth.GetContractHandler(tokenContractAddress);
            var balanceMessage = new BalanceOfFunction() { Owner = address };
            var balance = await handler.QueryAsync<BalanceOfFunction, BigInteger>(balanceMessage);
            //assuming all have 18 decimals
            //var value = Web3.Convert.FromWeiToBigDecimal(balance/*, decimalPlaces*/);
            var check = balance.ToString();
            return decimal.Parse(balance.ToString());
        }
        public async Task<decimal> GetPlaymatesBalanceAsync(string address, string tokenContractAddress, string url, int decimalPlaces = 18)
        {
            var web3 = new Web3(url);
            var handler = web3.Eth.GetContractHandler(tokenContractAddress);
            var balanceMessage = new BalanceOfFunction() { Owner = address };
            var balance = await handler.QueryAsync<BalanceOfFunction, BigInteger>(balanceMessage);
            //assuming all have 18 decimals
            var value = Web3.Convert.FromWeiToBigDecimal(balance, decimalPlaces);
            return decimal.Parse(value.ToString());
        }
        public async Task<decimal> GetETHBalanceAsync(string address, string url)
        {
            var web3 = new Web3(url);
            var handler = web3.Eth.GetBalance;
            //var balanceMessage = new BalanceOfFunction() { Owner = address };
            var balance = await handler.SendRequestAsync(address);
            //assuming all have 18 decimals
            var value = Web3.Convert.FromWeiToBigDecimal(balance);
            return decimal.Parse(value.ToString());
        }
        public async Task<GasTrackModel> GasTrackAsync(string etherscanUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(etherscanUrl);

                if (response.IsSuccessStatusCode)
                {
                    var rawData = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<GasTrackModel>(rawData);

                    if (data.Message == GasTrackConstantValues.OkMessage && data.Status == GasTrackConstantValues.SuccessStatus)
                        data.Result = JsonSerializer.Deserialize<GasTrackResultModel>(data.ResultObject.ToString());

                    return data;
                }
                else
                {
                    return new GasTrackModel()
                    {
                        Status = GasTrackConstantValues.FailStatus,
                        Message = GasTrackConstantValues.NotOkMessage,
                        Result = null
                    };
                }
            }
        }
        public async Task<TransactionReceipt> GetTransactionReceiptByHash(string accountKey, string url, int chainId, string transactionHash)
        {
            var wallet = new Wallet(accountKey, "");
            var accounttokey = wallet.GetAccount(0);
            var account = new Account(accounttokey.PrivateKey, chainId);
            var web3 = new Web3(account, url);
            var transactionReceipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

            return transactionReceipt;
        }
        public async Task<Transaction> GetTransactionByHash(string accountKey, string url, int chainId, string transactionHash)
        {
            var wallet = new Wallet(accountKey, "");
            var accounttokey = wallet.GetAccount(0);
            var account = new Account(accounttokey.PrivateKey, chainId);
            var web3 = new Web3(account, url);
            var transaction = await web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(transactionHash);

            return transaction;
        }
        public async Task<TransactionReceipt> GetTransactionReceiptByHash(string url, string transactionHash)
        {
            var web3 = new Web3(url);
            var transactionReceipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

            return transactionReceipt;
        }
        public async Task<Transaction> GetTransactionByHash(string url, string transactionHash)
        {
            var web3 = new Web3(url);
            var transaction = await web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(transactionHash);

            return transaction;
        }
        public Account CreateWallet(string password = "", int index = 0, string words = "")
        {
            if (string.IsNullOrWhiteSpace(words))
            {
                Mnemonic mnemo = new Mnemonic(Wordlist.English, WordCount.Twelve);
                words = mnemo.ToString();
            }
            var wallet = new Wallet(words, password);
            var account = wallet.GetAccount(index);

            return account;
        }
        public async Task<string> SendEther(string accountKey, string url, int chainId, string toAddr, BigInteger amount, BigInteger? gastLimit = null, BigInteger? gasPrice = null, bool useLegacy = false)
        {
            var wallet = new Wallet(accountKey, "");
            var accounttokey = wallet.GetAccount(0);
            var account = new Account(accounttokey.PrivateKey, chainId);

            var web3 = new Web3(account, url);

            if (useLegacy)
            {
                web3.TransactionManager.UseLegacyAsDefault = true;
            }

            var transactionInput = new TransactionInput()
            {
                From = account.Address,
                To = toAddr,
                Value = new HexBigInteger(amount)
            };

            if (gastLimit.HasValue)
                transactionInput.Gas = new HexBigInteger(gastLimit.Value);

            if (gasPrice.HasValue)
                transactionInput.GasPrice = new HexBigInteger(gasPrice.Value);
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var transactionHash = await web3.Eth.TransactionManager.SendTransactionAsync(transactionInput);
            return transactionHash;
        }
        public async Task<string> TransferErc20Token(string accountKey, string url, int chainId, string contractAddress, string toAddr, BigInteger tokenAmount, BigInteger? gastLimit = null, BigInteger? gasPrice = null, bool useLegacy = false)
        {
            var wallet = new Wallet(accountKey, "");
            var accounttokey = wallet.GetAccount(0);
            var account = new Account(accounttokey.PrivateKey, chainId);
            var web3 = new Web3(account, url);

            if (useLegacy)
            {
                web3.TransactionManager.UseLegacyAsDefault = true;
            }

            var transferFunction = new TransferFunction
            {
                TokenAmount = tokenAmount,
                To = toAddr,
                FromAddress = account.Address
            };

            if (gastLimit.HasValue)
                transferFunction.Gas = gastLimit.Value;

            if (gasPrice.HasValue)
                transferFunction.GasPrice = gasPrice.Value;

            var handler = web3.Eth.GetContractHandler(contractAddress);
            var transactionhash = await handler.SendRequestAsync(transferFunction);
            return transactionhash;
        }
        public Account GetAccount(string password, int index, string words)
        {
            var wallet = new Wallet(words, password);
            var account = wallet.GetAccount(index);

            return account;
        }
        public Account GetAccount(string accountKey, int chainId)
        {
            var wallet = new Wallet(accountKey, "");
            var accounttokey = wallet.GetAccount(0);
            var account = new Account(accounttokey.PrivateKey, chainId);

            return account;
        }
        public string GetMnemonics()
        {
            Mnemonic mnemo = new Mnemonic(Wordlist.English, WordCount.Twelve);
            return mnemo.ToString();
        }
        public async Task<string> ApproveErc20Token(string accountKey, string url, int chainId, string contractAddress, string toAddr, BigInteger tokenAmount, BigInteger? gastLimit = null, BigInteger? gasPrice = null, bool useLegacy = false)
        {
            var account = new Account(accountKey, chainId);
            var web3 = new Web3(account, url);

            if (useLegacy)
            {
                web3.TransactionManager.UseLegacyAsDefault = true;
            }

            var transferFunction = new ApproveFunction
            {
                TokenAmount = tokenAmount,
                Spender = toAddr,
                FromAddress = account.Address
            };
            if (gastLimit.HasValue)
                transferFunction.Gas = gastLimit.Value;

            if (gasPrice.HasValue)
                transferFunction.GasPrice = gasPrice.Value;

            var handler = web3.Eth.GetContractHandler(contractAddress);
            var transactionhash = await handler.SendRequestAndWaitForReceiptAsync(transferFunction);
            return transactionhash.TransactionHash;
        }
        public bool IsValidMessageAddress(string message, string signature, string address)
        {
            var signer = new EthereumMessageSigner();

            string signatureAddress = signer.EncodeUTF8AndEcRecover(message, signature);

            return address.Equals(signatureAddress, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
