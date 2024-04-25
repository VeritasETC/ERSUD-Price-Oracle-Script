using Context;
using DTO.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Implementations.Base;
using Repository.Interfaces.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Implementations.Admin
{
    internal class AccountRoleRepository : RepositoryBase<AccountRole>, IAccountRoleRepository
    {

        private readonly ExchnageContext _db;

        public AccountRoleRepository(ExchnageContext db)
            : base(db)
        {
            _db = db;
        }
        public async Task<AccountRole> GetAccountRoleAsync(long accountId)
        {
            return await FindByCondition(f => f.IsEnabled && f.AccountId.Equals(accountId) && !f.IsDeleted).FirstOrDefaultAsync();
        }
        public async Task<AccountRole> GetAccountRoleAsyncByRoleId(long roleId)
        {
            return await FindByCondition(f => f.IsEnabled && f.RoleId.Equals(roleId) && !f.IsDeleted).FirstOrDefaultAsync();
        }
        public async Task<List<AccountRole>> GetAllAccountRoleByAccountIdAsync(long accountId)
        {
            return await FindByCondition(f => f.IsEnabled && f.AccountId.Equals(accountId) && !f.IsDeleted).ToListAsync();
        }
        public async Task<AccountRole> GetAccountRoleByAccountId(long accountId)
        {
            return await FindByCondition(f => f.IsEnabled && f.AccountId.Equals(accountId) && !f.IsDeleted).Include(x => x.Account).Include(x => x.Roles).FirstOrDefaultAsync();
        }

    }
}
