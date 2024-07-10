using AutoMapper;
using Binance.Net.Clients;
using Common.Helpers;
using DTO.Models;
using DTO.ViewModel;
using DTO.ViewModel.Currency;
using Ethereum.Contract.Interfaces;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Repository.Interfaces.Unit;
using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class RateService : IRateService
    {
        private readonly IRepositoryUnit _repository;
        private IContractHandler _contractHandler;
        private readonly string _key = "kB]09(2&.K?<b0Ie<oI1iX";

        public RateService(IRepositoryUnit repository, IContractHandler contractHandler)
        {
            _repository = repository;
            _contractHandler = contractHandler;
        }

        public async Task<ServiceResult<decimal>> GetRate()
        {
            try
            {
                var data = await _repository.RateRepo.GetRate();
                if(data != null)
                    return ServiceResults.GetSuccessfully(data.latestRate);
                else
                {
                    return ServiceResults.Errors.Invalid<decimal>("null value", 0);
                }
            }
            catch (Exception ex)
            {
                return ServiceResults.Errors.Invalid<decimal>(ex.Message, 0);
            }
        }
        public async Task<ServiceResult<string>> UpdatePercentage(UpdatePercentageRequest request)
        {
            try
            {
                string combinedKey = AppSettingHelper.GetEncryptionKey() + _key;
                if (combinedKey != request.key)
                    return ServiceResults.Errors.Unauthorized("record not found", "");

                var data = await _repository.RateRepo.GetRate();
                if (data == null)
                    return ServiceResults.Errors.NotFound("record not found", "");

                data.Percentage = request.Percentage;
                _repository.RateRepo.Update(data);
                await _repository.SaveAsync();

                return ServiceResults.GetSuccessfully("Updated Percentage Successful");
            }
            catch (Exception ex)
            {
                return ServiceResults.Errors.Invalid<string>(ex.Message, "");
            }
        }

        public async Task<ServiceResult<TransactionReceipt>> RateUpdateFunction()
        {
            try
            {
                //var restClient = new BinanceRestClient();
                //var tickerResult = await restClient.SpotApi.ExchangeData.GetTickerAsync("ETCUSDT");

                var data = await _repository.RateRepo.GetRate();

                if (data.latestRate == 0)
                {
                    return ServiceResults.Errors.Invalid<TransactionReceipt>("Rate", null);
                }

                var result = await _contractHandler.SetETHClassicRate(nodeUrl: AppSettingHelper.GetETHClassicNodeUrl(),
                                                                                                     chainId: AppSettingHelper.GetETHClassicChainId(),
                                                                                                     accountPrivateKey: AppSettingHelper.GetETHClassicPrivateKey(),
                                                                                                     contractAddress: AppSettingHelper.GetETHClassicSmartAddress(),
                                                                                                     ethClassicRate: new()
                                                                                                     {
                                                                                                         amount = Web3.Convert.ToWei(data.latestRate),
                                                                                                     },
                                                                                                     UseLegacyAsDefault: true,
                                                                                                     cancellationToken: null);

                return ServiceResults.ContractUpdatedSuccessfully(result);
            }
            catch (Exception ex)
            {
                return ServiceResults.Errors.Invalid<TransactionReceipt>(ex.Message, null);
            }
        }

    }
}
