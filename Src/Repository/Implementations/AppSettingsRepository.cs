using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Models;
using Context;
using Repository.Interfaces;
using Repository.Implementations.Base;

namespace Repository.Implementations
{
    internal class AppSettingsRepository : RepositoryBase<AppSettings>, IAppSettingsRepository
    {
        private readonly ExchnageContext _db;

        public AppSettingsRepository(ExchnageContext db)
            : base(db)
        {
            _db = db;
        }

        public async Task<AppSettings> GetByKeywordAsync(string key)
        {
            return await FindByCondition(f => !f.IsDeleted && f.keyword == key).FirstOrDefaultAsync();
        }

        public async Task<List<AppSettings>> GetAllAppSettingsAsync()
        {
            return await FindAll().ToListAsync();
        }
        public AppSettings GetByKeyword(string key)
        {
            return FindByCondition(f => !f.IsDeleted && f.keyword == key).FirstOrDefault();
        }
    }
}