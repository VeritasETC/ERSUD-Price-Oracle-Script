using DTO.Models;
using Repository.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.Admin
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        Task<Account> GetAccountByEmail(string email);
        Task<Account> GetAccountById(long accountId);
        Task<Account> GetAccountByEmailAndPasswordAsync(string email, string password);
        Task<Account> GetAccountByEmailAndPasswordForLoginAsync(string email, string password);
        bool AnyAccountByEmail(string email);
        Task<Account> GetAccountByIdWithRole(long accountId);
        Account GetAccountByRefferalId(string refferalId);
    }
}
