using AutoMapper;
using EthereumSdk.Interfaces;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.ViewModel.Currency;
using DTO.Enums;
using Logger.Interfaces;
using Repository.Interfaces.Unit;
using Service.Interfaces;
using Service.Models;

namespace Service.Implementations
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IRepositoryUnit _repository;
        private readonly IEventLogger _eventLogger;
        private readonly IEtherService _ether;
        private readonly IMapper mapper;
        private readonly IFileManagementService fileManagementService;
        public CurrencyService(IRepositoryUnit repository, IEventLogger eventLogger, IMapper mapper, IEtherService ether, IFileManagementService fileManagementService)
        {
            _repository = repository;
            _eventLogger = eventLogger;
            _ether = ether;
            this.mapper = mapper;
            this.fileManagementService = fileManagementService;
        }

        public async Task<ServiceResult<CurrencyResponseModel>> AddCurrency(AddCurrencyRequestModel model)
        {
            var blockchain = _repository.Blockchain.GetById(model.BlockchainId);
            if (blockchain == null)
            {
                return ServiceResults.Errors.NotFound<CurrencyResponseModel>("Block Chain ", null);
            }
            var currency = new Currency()
            {
                BlockchainId = model.BlockchainId,
                CanUpdate = model.CanUpdate,
                CurrencyType = model.CurrencyType,
                Decimals = model.Decimals,
                Name = model.Name,
                RateInUSD = model.RateInUSD,
                ShortName = model.ShortName,
                SmartContractAddress = model.SmartContractAddress,
            };
            _repository.Currency.Create(currency);
            await _repository.SaveAsync();

            return ServiceResults.GetSuccessfully(mapper.Map<CurrencyResponseModel>(currency));
        }
        public async Task<ServiceResult<List<CurrencyResponseModel>>> GetCurrencies()
        {
            var list = _repository.Currency.GetAllCurrencies();
            if (list.Count == 0)
            {
                return ServiceResults.Errors.NotFound<List<CurrencyResponseModel>>("Currencies", null);
            }
            return ServiceResults.GetSuccessfully(mapper.Map<List<CurrencyResponseModel>>(list));
        }
        public async Task<ServiceResult<List<CurrencyResponseModel>>> GetCurrenciesByBlockhainID(long id)
        {
            var list = _repository.Currency.GetbyBlockChainid(id);
            if (list.Count == 0)
            {
                return ServiceResults.Errors.NotFound<List<CurrencyResponseModel>>("Currencies", null);
            }
            return ServiceResults.GetSuccessfully(mapper.Map<List<CurrencyResponseModel>>(list));
        }
        public async Task<ServiceResult<string>> EditCurrency(Guid Uuid, AddCurrencyRequestModel model)
        {
            var blockchain = _repository.Blockchain.GetById(model.BlockchainId);
            if (blockchain == null)
            {
                return ServiceResults.Errors.NotFound("Block Chain ", "");
            }
            var currency = _repository.Currency.GetByUuid(Uuid);
            if (currency == null)
            {
                return ServiceResults.Errors.NotFound("Currency", "");
            }
            currency.BlockchainId = model.BlockchainId;
            currency.CanUpdate = model.CanUpdate;
            currency.CurrencyType = model.CurrencyType;
            currency.Decimals = model.Decimals;
            currency.Name = model.Name;
            currency.ShortName = model.ShortName;
            currency.SmartContractAddress = model.SmartContractAddress;

            _repository.Currency.Update(currency);
            await _repository.SaveAsync();
            return ServiceResults.UpdatedSuccessfully("Currency Updated");
        }
        public async Task<ServiceResult<List<BlockChainResponseModel>>> GetAllBlockChain()
        {
            var list = _repository.Blockchain.GetAllBlockChainsList();
            if (list.Count == 0)
            {
                return ServiceResults.Errors.NotFound<List<BlockChainResponseModel>>("BlockChains", null);
            }

            return ServiceResults.GetSuccessfully(mapper.Map<List<BlockChainResponseModel>>(list));
        }
        public async Task<ServiceResult<string>> EditCurrencyRates(Guid Uuid, decimal Rate)
        {
            var currency = _repository.Currency.GetByUuid(Uuid);
            if (currency == null)
            {
                return ServiceResults.Errors.NotFound("Currency", "");
            }
            currency.RateInUSD = Rate;
            currency.RateUpdatedAt = DateTime.UtcNow;
            _repository.Currency.Update(currency);
            await _repository.SaveAsync();
            return ServiceResults.UpdatedSuccessfully("Rates Updated");
        }
        public async Task<ServiceResult<List<CurrencyResponseModel>>> GetCurrenciesWithLiquidityByBlockchainuuid(Guid BlockchainUuid)
        {
            var list = _repository.Currency.GetCurrenciesWithLiquidity(BlockchainUuid);
            if (list.Count == 0)
            {
                return ServiceResults.Errors.NotFound<List<CurrencyResponseModel>>("Currencies", null);
            }
            return ServiceResults.GetSuccessfully(mapper.Map<List<CurrencyResponseModel>>(list));
        }

        public async Task<ServiceResult<CurrencyResponseModel>> UploadCurrencyImage(UploadCurrencyImageRequestModel model)
        {
            var currency = _repository.Currency.GetByUuid(model.CurrenctUuid);
            if (currency == null)
            {
                return ServiceResults.Errors.NotFound<CurrencyResponseModel>("Currency", null);
            }
            if (model.FileStorageType.Value == FileStorageType.ExternalLink)
            {
                var result = await fileManagementService.UploadCurrencyImageFile(model.Image, "", new string[] { "image/jpeg", "image/png", "image/gif" }, 2097152);
                if (result.isSuccess)
                {
                    currency.Image = result.response;
                    currency.FileStorageType = model.FileStorageType;
                    _repository.Currency.Update(currency);
                    await _repository.SaveAsync();
                }
                else
                {
                    return ServiceResults.Errors.UnhandledError<CurrencyResponseModel>(result.response, null); ;
                }
            }
            else
            {
            }
            return ServiceResults.GetSuccessfully(mapper.Map<CurrencyResponseModel>(currency));
        }
    }
}