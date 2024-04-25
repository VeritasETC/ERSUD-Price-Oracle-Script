using Ethereum.Contract.Interfaces;
using Ethereum.Contract.Models;
using EthereumSdk.Interfaces;
using Microsoft.EntityFrameworkCore;
using Nethereum.Web3;
using Common.Helpers;
using DTO.Enums;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using DTO.ViewModel.Wallet;
using Logger.Interfaces;
using Repository.Interfaces.Unit;
using Service.Interfaces;
using Service.Models;
using DTO.ViewModel.Account;
using DTO.ViewModel.Token;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using DTO.ViewModel.Nft;

namespace Service.Implementations
{
    public class WalletServices : IWalletServices
    {
        private readonly IRepositoryUnit _repository;
        private readonly IEventLogger _eventLogger;
        private readonly IEtherService _ether;
        private readonly IContractHandler _ContractHandler;

        public WalletServices(IRepositoryUnit repository, IEventLogger eventLogger, IEtherService ether, IContractHandler ContractHandler)
        {
            _repository = repository;
            _eventLogger = eventLogger;
            _ether = ether;
            _ContractHandler = ContractHandler;
        }
     
  
        public JwtTokenModel ValidateToken(string token)
        {
            try
            {
                ClaimsPrincipal principal = GetPrincipal(token);
                if (principal == null)
                    return null;
                ClaimsIdentity identity = (ClaimsIdentity)principal.Identity;

                // Token values
                var id = long.Parse(CryptoHelper.SymmetricDecryptString(AppSettingHelper.GetJwtValueSecret(), identity.FindFirst(TokenClaimKeys.Value).Value));

                var issuedAt = long.Parse(identity.FindFirst(TokenClaimKeys.IssuedAt).Value);

                var expiresAt = long.Parse(identity.FindFirst(TokenClaimKeys.ExpiresAt).Value);

                var notValidBefore = long.Parse(identity.FindFirst(TokenClaimKeys.NotValidBefore).Value);

                var type = int.Parse(CryptoHelper.SymmetricDecryptString(AppSettingHelper.GetJwtValueSecret(),
                        identity.FindFirst(TokenClaimKeys.Type).Value));

                return new JwtTokenModel(id, issuedAt, expiresAt, notValidBefore, (AccountType)type);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private static ClaimsPrincipal GetPrincipal(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
            if (jwtToken == null)
                return null;
            byte[] key = Encoding.ASCII.GetBytes(AppSettingHelper.GetJwtTokenSecret());
            TokenValidationParameters parameters = new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
            return tokenHandler.ValidateToken(token,
                parameters, out _);
        }
        public async Task<List<NftResponseModel>> GetAllAsync()
        {
            var responses = new List<NftResponseModel>();
            return responses;//await _context.Todo.ToListAsync();
        }

        public async Task SaveAsync(ConnectRequestModel newTodo)
        {
           // _context.Todo.Add(newTodo);
            //await _context.SaveChangesAsync();
        }

    }
}