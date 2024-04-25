using DTO.Models;
using Repository.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.Admin
{
    public interface IAccountRoleRepository : IRepositoryBase<AccountRole>
    {
        Task<AccountRole> GetAccountRoleAsync(long accountId);
        Task<AccountRole> GetAccountRoleAsyncByRoleId(long RoleId);
        Task<List<AccountRole>> GetAllAccountRoleByAccountIdAsync(long accountId);
        Task<AccountRole> GetAccountRoleByAccountId(long accountId);
    }
}
