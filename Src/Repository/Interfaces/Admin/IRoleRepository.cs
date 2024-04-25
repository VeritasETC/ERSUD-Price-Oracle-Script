
using DTO.Models;
using Repository.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.Admin
{
    public interface IRoleRepository : IRepositoryBase<Role>
    {
        Task<Role> GetRoleByNameAsync(string Name);

        Task<List<Role>> GetRoleListAsync();
        Task<Role> GetRoleById(long id);
    }
}
