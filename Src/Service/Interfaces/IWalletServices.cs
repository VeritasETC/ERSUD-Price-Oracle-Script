using DTO.Models;
using DTO.ViewModel.Account;
using DTO.ViewModel.Nft;
using DTO.ViewModel.Token;
using DTO.ViewModel.Wallet;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IWalletServices
    {
     
        JwtTokenModel ValidateToken(string token);
        Task<List<NftResponseModel>> GetAllAsync();
        Task SaveAsync(ConnectRequestModel newTodo);
    }
}
