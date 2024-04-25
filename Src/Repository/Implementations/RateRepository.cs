using Context;
using DTO.Models;
using DTO.ViewModel;
using Microsoft.EntityFrameworkCore;
using Repository.Implementations.Base;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    internal class RateRepository : RepositoryBase<Rate>, IRateRepository
    {
        private readonly ExchnageContext _db;

        public RateRepository(ExchnageContext db)
            : base(db)
        {
            _db = db;
        }

        public async Task<Rate> GetRate()
        {
            return await FindByCondition(x => !x.IsDeleted).FirstOrDefaultAsync();
        }

    }
}
