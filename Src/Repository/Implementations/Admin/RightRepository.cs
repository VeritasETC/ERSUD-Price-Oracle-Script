using Context;
using DTO.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Implementations.Base;
using Repository.Interfaces.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Implementations.Admin
{
    internal class RightRepository : RepositoryBase<Right>, IRightRepositry
    {
        private readonly ExchnageContext _db;

        public RightRepository(ExchnageContext db)
            : base(db)
        {
            _db = db;
        }
        public async Task<List<Right>> GetRightListAsync()
        {
            return await FindByCondition(x => x.IsEnabled).ToListAsync();
        }
        public async Task<Right> GetRightByNameAsync(string Name)
        {
            return await FindByCondition(x => x.Name.ToLower().Equals(Name.ToLower()) && x.IsEnabled).FirstOrDefaultAsync();
        }
        public async Task<Right> GetRightByIdAsync(long Id)
        {
            return await FindByCondition(x => x.Id == Id && x.IsEnabled).FirstOrDefaultAsync();
        }

    }
}
