using Context;
using DTO.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Implementations.Base;
using Repository.Interfaces.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations.Admin
{
    internal class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {

        private readonly ExchnageContext _db;

        public AccountRepository(ExchnageContext db)
            : base(db)
        {
            _db = db;
        }

        public async Task<Account> GetAccountByRefferalId(string refferalId)
        {
            return await FindByCondition(x => x.RefferalId.ToLower().Equals(refferalId.ToLower()) && !x.IsDeleted).FirstOrDefaultAsync();
        }
        public async Task<Account> GetAccountByEmail(string email)
        {
            return await FindByCondition(x => x.Email.ToLower().Equals(email.ToLower()) && !x.IsDeleted).FirstOrDefaultAsync();
        }

        public async Task<Account> GetAccountById(long accountId)
        {
            return await FindByCondition(x => x.Id.Equals(accountId) && !x.IsDeleted).FirstOrDefaultAsync();
        }
        public async Task<Account> GetAccountByEmailAndPasswordAsync(string email, string password)
        {
            return await FindByCondition(x => x.Email.ToLower().Equals(email.ToLower()) && x.Password.Equals(password) && x.IsDeleted == false).Include(x => x.AccountRoles).FirstOrDefaultAsync();
        }
        public async Task<Account> GetAccountByEmailAndPasswordForLoginAsync(string email, string password)
        {
            return await FindByCondition(x => x.Email.ToLower().Equals(email.ToLower()) && x.Password.Equals(password) && x.IsDeleted == false).FirstOrDefaultAsync();
        }
        public bool AnyAccountByEmail(string email)
        {
            return FindByCondition(f => f.Email.Equals(email)).Any();
        }
        public async Task<Account> GetAccountByIdWithRole(long accountId)
        {
            return await FindByCondition(x => x.Id.Equals(accountId) && !x.IsDeleted).Include(x => x.AccountRoles).FirstOrDefaultAsync();
        }

        Account IAccountRepository.GetAccountByRefferalId(string refferalId)
        {
            return FindByCondition(x => x.RefferalId.ToLower().Equals(refferalId.ToLower())).FirstOrDefault();
        }
    }
}