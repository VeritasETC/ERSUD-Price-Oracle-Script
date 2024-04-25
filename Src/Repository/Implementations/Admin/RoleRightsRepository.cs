using Context;
using DTO.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Implementations.Base;
using Repository.Interfaces.Admin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Implementations.Admin
{
    internal class RoleRightsRepository : RepositoryBase<RoleRights>, IRoleRightsRepository
    {
        private readonly ExchnageContext _db;

        public RoleRightsRepository(ExchnageContext db)
            : base(db)
        {
            _db = db;
        }
        public async Task<RoleRights> GetRoleRightsAsync(long roleId, long rightId)
        {
            return await FindByCondition(f => f.IsEnabled && f.RoleId.Equals(roleId)).FirstOrDefaultAsync();
        }
        public async Task<List<RoleRights>> GetRoleRightsByRoleId(long roleId)
        {
            return await FindByCondition(f => f.RoleId.Equals(roleId) && f.IsEnabled && !f.IsDeleted).Include(f=>f.Screen).Include(f=>f.Role).ToListAsync();
        }
        public async Task<List<RoleRights>> GetRoleRightsByRoleIdUpdate(long roleId)
        {
            return await FindByCondition(f => f.RoleId.Equals(roleId) && f.IsEnabled && !f.IsDeleted).ToListAsync();
        }
        public async Task<List<RoleRights>> GetRoleRightsByScreenId(long ScreenId)
        {
            return await FindByCondition(f => ScreenId.Equals(ScreenId) && f.IsEnabled && !f.IsDeleted).ToListAsync();
        }

        public async Task<List<RoleRights>> GetRoleRightsByRoleIdAndUserId(long Roleid, long userid)
        {
            return await FindByCondition(f => f.RoleId == Roleid && f.IsEnabled && !f.IsDeleted).ToListAsync();
        }

       }
}
