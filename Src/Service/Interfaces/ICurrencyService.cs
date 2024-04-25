using DTO.ViewModel.Currency;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICurrencyService
    {
        Task<ServiceResult<CurrencyResponseModel>> AddCurrency(AddCurrencyRequestModel model);
        Task<ServiceResult<List<CurrencyResponseModel>>> GetCurrencies();
        Task<ServiceResult<List<CurrencyResponseModel>>> GetCurrenciesByBlockhainID(long BlockchainId);
        Task<ServiceResult<string>> EditCurrency(Guid Uuid, AddCurrencyRequestModel model);
        Task<ServiceResult<string>> EditCurrencyRates(Guid Uuid, decimal Rate);
        Task<ServiceResult<List<BlockChainResponseModel>>> GetAllBlockChain();
        Task<ServiceResult<List<CurrencyResponseModel>>> GetCurrenciesWithLiquidityByBlockchainuuid(Guid BlockchainUuid);
        Task<ServiceResult<CurrencyResponseModel>> UploadCurrencyImage(UploadCurrencyImageRequestModel model);
    }
}
