using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Models;
using DTO.Enums;
using Context;
using Repository.Interfaces;
using Repository.Implementations.Base;

namespace Repository.Implementations
{
    internal class MasterWalletRepository : RepositoryBase<MasterWallet>, IMasterWalletRepository
    {
        private readonly ExchnageContext _db;

        public MasterWalletRepository(ExchnageContext db)
            : base(db)
        {
            _db = db;
        }

        public async Task<MasterWallet> GetByIdAsync(long id)
        {
            return await FindByCondition(f => f.Id == id).FirstOrDefaultAsync();
        }

        public async Task<MasterWallet> GetByUuidAsync(Guid uuid)
        {
            return await FindByCondition(f => f.Uuid == uuid).FirstOrDefaultAsync();
        }

        public MasterWallet GetMasterWalletByCurrencyId(long currencyId)
        {
            return FindAll().Include(i => i.Currency)
                .Include(i => i.Currency.Blockchain)
                .Where(w => w.CurrencyId == currencyId).FirstOrDefault();
        }

        public MasterWallet GetMasterWalletByCurrencyId(long currencyId, string address)
        {
            return FindAll().Include(i => i.Currency)
                .Include(i => i.Currency.Blockchain)
                .Where(w => w.CurrencyId == currencyId && w.Address.ToLower() == address.ToLower()).FirstOrDefault();
        }

        public List<MasterWallet> GetMasterWalletsByCurrencyId(long currencyId)
        {
            return FindAll().Include(i => i.Currency)
                .Include(i => i.Currency.Blockchain)
                .Where(w => w.CurrencyId == currencyId).ToList();
        }

        public List<MasterWallet> GetAllMasterWallets()
        {
            return FindAll().ToList();
        }
        public async Task<MasterWallet> GetMasterWalletByBlockchainType(EBlockchainType Type)
        {
            return await FindByCondition(f => f.BlockchainType == Type).FirstOrDefaultAsync();
        }
        public async Task<MasterWallet> GetMasterWalletByPrivatekey(string key)
        {
            return await FindByCondition(f => f.PrivateKey == key).FirstOrDefaultAsync();
        }
        public async Task<MasterWallet> GetMasterWalletByPublicKey(string key)
        {
            return await FindByCondition(f => f.PublicKey == key).FirstOrDefaultAsync();
        }
        public async Task<MasterWallet> GetMasterWalletByName(string name)
        {
            return await FindByCondition(f => f.Name == name && f.IsDeleted == false).FirstOrDefaultAsync();
        }
    }
}
