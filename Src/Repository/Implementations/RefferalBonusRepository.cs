using Context;
using DTO.Models;
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
    internal class RefferalBonusRepository : RepositoryBase<RefferalBonus>, IRefferalBonusRepository
    {
        private readonly ExchnageContext _db;

        public RefferalBonusRepository(ExchnageContext db)
            : base(db)
        {
            _db = db;
        }

      
    }
}
