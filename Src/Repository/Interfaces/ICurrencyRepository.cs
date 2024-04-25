using DTO.Models;
using Repository.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICurrencyRepository : IRepositoryBase<Currency>
    {
        Task<Currency> GetByIdAsync(long id);
        Task<Currency> GetByUuidAsync(Guid uuid);
        Currency GetByUuid(Guid uuid);
        Currency GetByid(long id);
        Task<List<Currency>> GetCurrenciesWithLiquidity();
        Task<Currency> GetCurrencyBySmartAddress(string contractAddress);
        List<Currency> GetCurrenciesWithLiquidity(Guid bloclchainUuid);
        List<Currency> GetbyBlockChainid(long id);
        List<Currency> GetAllCurrencies();
        List<Currency> GetAllCurrenciesForRateUpdate();
        List<Currency> GetEthTokenCurrencies();
        List<Currency> GetEthCurrencies();
        Task<Currency> GetByShortName(string Name);
    }
}
