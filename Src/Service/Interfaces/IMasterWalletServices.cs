using DTO.ViewModel.MasterWallet;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IMasterWalletServices
    {
        Task<ServiceResult<MasterWalletResponseModel>> AddMasterWallet(MasterWalletRequestModel model);
        Task<ServiceResult<List<MasterWalletResponseModel>>> GetMasterWallets();
        Task<ServiceResult<MasterWalletResponseModel>> GetMasterWalletbyUuid(Guid uuid);
        Task<ServiceResult<List<MasterWalletResponseModel>>> GetMasterWalletbyCurrencyId(long id);
        Task<ServiceResult<MasterWalletResponseModel>> GetMasterWalletByAddressAndCurrencyId(long id, string address);
        Task<ServiceResult<MasterWalletResponseModel>> GetMasterWalletByprivateKey(string key);
        Task<ServiceResult<MasterWalletResponseModel>> GetMasterWalletByPublicKey(string key);
        Task<ServiceResult<MasterWalletResponseModel>> EditMasterWallet(Guid uuid, MasterWalletRequestModel model);
        Task<ServiceResult<MasterWalletResponseModel>> AddMasterWalletEth(MasterWalletRequestModel model);
    }
}
