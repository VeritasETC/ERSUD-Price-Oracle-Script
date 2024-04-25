using DTO.Enums;
using DTO.Models;
using Repository.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IMasterWalletRepository : IRepositoryBase<MasterWallet>
    {
        Task<MasterWallet> GetByIdAsync(long id);
        Task<MasterWallet> GetByUuidAsync(Guid uuid);
        List<MasterWallet> GetMasterWalletsByCurrencyId(long currencyId);
        MasterWallet GetMasterWalletByCurrencyId(long currencyId);
        Task<MasterWallet> GetMasterWalletByPrivatekey(string key);
        Task<MasterWallet> GetMasterWalletByPublicKey(string key);
        MasterWallet GetMasterWalletByCurrencyId(long currencyId, string address);
        List<MasterWallet> GetAllMasterWallets();
        Task<MasterWallet> GetMasterWalletByBlockchainType(EBlockchainType Type);
        Task<MasterWallet> GetMasterWalletByName(string name);
    }
}
