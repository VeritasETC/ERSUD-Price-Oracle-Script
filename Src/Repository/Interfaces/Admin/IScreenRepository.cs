using DTO.Models;
using Repository.Interfaces.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces.Admin
{
    public interface IScreenRepository : IRepositoryBase<Screen>
    {
        Task<Screen> GetScreenByNameAsync(string Name);

        Task<List<Screen>> GetScreenListAsync();
        Task<Screen> GetById(long id);
    }
}
