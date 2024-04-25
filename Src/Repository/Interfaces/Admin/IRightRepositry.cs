using DTO.Models;
using Repository.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.Admin
{
    public interface IRightRepositry : IRepositoryBase<Right>
    {
        Task<Right> GetRightByIdAsync(long Id);
        Task<List<Right>> GetRightListAsync();
        Task<Right> GetRightByNameAsync(string Name);

    }
}
