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
    internal class AccountConfirmationRepository : RepositoryBase<AccountConfirmation>, IAccountConfirmationRepository
    {
        private readonly ExchnageContext _db;

        public AccountConfirmationRepository(ExchnageContext db)
            : base(db)
        {
            _db = db;
        }
        public AccountConfirmation GetById(long id)
        {
            return FindByCondition(f => !f.IsDeleted && f.Id == id).FirstOrDefault();
        }

        public AccountConfirmation GetByUuid(Guid uuid)
        {
            return FindByCondition(f => !f.IsDeleted && f.Uuid == uuid).FirstOrDefault();
        }

        public Task<AccountConfirmation> GetAccountConfirmationWithAccountByToken(string token) => FindByConditionWithTracking(x => x.IsDeleted == false && x.IsUsed == false && x.Code == token).Include(x => x.Account).FirstOrDefaultAsync();


    }
}
