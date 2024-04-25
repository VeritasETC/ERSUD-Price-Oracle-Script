using DTO.Models;
using Repository.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAccountConfirmationRepository : IRepositoryBase<AccountConfirmation>
    {
        AccountConfirmation GetById(long id);
        AccountConfirmation GetByUuid(Guid uuid);
        Task<AccountConfirmation> GetAccountConfirmationWithAccountByToken(string token);
    }
}
