using Context;
using DTO.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Implementations.Base;
using Repository.Interfaces.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Implementations.Admin
{
    internal class ScreenRepository : RepositoryBase<Screen>, IScreenRepository
    {

        private readonly ExchnageContext _db;

        public ScreenRepository(ExchnageContext db)
            : base(db)
        {
            _db = db;
        }

        public async Task<Screen> GetById(long id)
        {
            return await FindByCondition(x => x.Id.Equals(id) && !x.IsDeleted).FirstOrDefaultAsync();
        }
        public async Task<Screen> GetScreenByNameAsync(string Name)
        {
            return await FindByCondition(x => x.Name.ToLower().Equals(Name.ToLower()) && !x.IsDeleted).FirstOrDefaultAsync();
        }
        public async Task<List<Screen>> GetScreenListAsync()
        {
            return await FindByCondition(x => !x.IsDeleted).ToListAsync();
        }

    }
}
