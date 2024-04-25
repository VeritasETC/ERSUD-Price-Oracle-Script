using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Models;
using DTO.Enums;
using Context;
using Repository.Implementations.Base;
using Repository.Interfaces;

namespace Repository.Implementations
{
    internal class CurrencyRepository : RepositoryBase<Currency>, ICurrencyRepository
    {
        private readonly ExchnageContext _db;


        public CurrencyRepository(ExchnageContext db)
            : base(db)
        {
            _db = db;
        }

        public async Task<Currency> GetByIdAsync(long id)
        {
            return await FindByCondition(f => f.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Currency> GetByShortName(string Name)
        {
            return await FindByCondition(f => f.ShortName.Trim() == Name.Trim()).FirstOrDefaultAsync();
        }

        public async Task<Currency> GetByUuidAsync(Guid uuid)
        {
            return await FindByCondition(f => f.Uuid == uuid).FirstOrDefaultAsync();
        }

        public async Task<List<Currency>> GetCurrenciesWithLiquidity()
        {
            var currencies = FindAll().Where(w => w.MasterWallets.Sum(s => s.Balance) - w.MasterWallets.Sum(s => s.PendingBalance) > 0);

            return await currencies.ToListAsync();
        }

        public async Task<Currency> GetCurrencyBySmartAddress(string contractAddress)
        {
            return await FindByCondition(f => f.SmartContractAddress == contractAddress && f.IsDeleted == false).FirstOrDefaultAsync();
        }
        public List<Currency> GetCurrenciesWithLiquidity(Guid bloclchainUuid)
        {
            var currencies = FindAll().Where(w => w.MasterWallets.Sum(s => s.Balance) - w.MasterWallets.Sum(s => s.PendingBalance) > 0
            && w.Blockchain.Uuid == bloclchainUuid);

            return currencies.ToList();
        }
        public Currency GetByUuid(Guid uuid)
        {
            return FindByCondition(f => f.Uuid == uuid).FirstOrDefault();
        }
        public List<Currency> GetAllCurrencies()
        {
            return FindAll().Include(i => i.Blockchain).ToList();
        }
        public List<Currency> GetbyBlockChainid(long id)
        {
            return FindAll().Include(i => i.Blockchain).Where(w => w.Blockchain.ChainID == id).ToList();
        }
        public List<Currency> GetAllCurrenciesForRateUpdate()
        {
            return FindAll().Where(w => w.CanUpdate).ToList();
        }
        public Currency GetByid(long id)
        {
            return FindByCondition(w => w.Id == id).FirstOrDefault();
        }
        public List<Currency> GetEthTokenCurrencies()
        {
            return FindByCondition(x => x.ShortName == "ETH" && x.CurrencyType == CurrencyType.Token).ToList();
        }
        public List<Currency> GetEthCurrencies()
        {
            return FindByCondition(x => x.ShortName == "ETH").ToList();
        }
    }
}
