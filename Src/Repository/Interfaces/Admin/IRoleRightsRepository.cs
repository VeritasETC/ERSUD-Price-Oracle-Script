using DTO.Models;
using Repository.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.Admin
{
    public interface IRoleRightsRepository : IRepositoryBase<RoleRights>
    {
        Task<RoleRights> GetRoleRightsAsync(long roleId, long rightId);
        Task<List<RoleRights>> GetRoleRightsByRoleId(long RoleId);
        Task<List<RoleRights>> GetRoleRightsByRoleIdAndUserId(long RoleId, long UserId);
        Task<List<RoleRights>> GetRoleRightsByScreenId(long ScreenId);
        Task<List<RoleRights>> GetRoleRightsByRoleIdUpdate(long roleId);
    }
}
