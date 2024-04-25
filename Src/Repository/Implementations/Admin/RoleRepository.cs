
using Common.Helpers;
using Context;
using DTO.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Implementations.Base;
using Repository.Interfaces.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Implementations.Admin
{
    internal class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        private readonly ExchnageContext _db;

        public RoleRepository(ExchnageContext db)
            : base(db)
        {
            _db = db;
        }
        public async Task<Role> GetRoleById(long id)
        {
            return await FindByCondition(x => x.Id.Equals(id) && x.IsEnabled && !x.IsDeleted).FirstOrDefaultAsync();
        }
        public async Task<Role> GetRoleByNameAsync(string Name)
        {
            return await FindByCondition(x => x.Name.ToLower().Equals(Name.ToLower()) && x.IsEnabled && !x.IsDeleted).FirstOrDefaultAsync();
        }
        public async Task<List<Role>> GetRoleListAsync()
        {
            return await FindByCondition(x => x.IsEnabled && x.Name.ToLower() != AppSettingHelper.SuperAdmin.ToLower() && !x.IsDeleted).ToListAsync();
        }

    }
}
