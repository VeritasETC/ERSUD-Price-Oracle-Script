using DTO.Models;
using Repository.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAuthTokenRepository : IRepositoryBase<AuthToken>
    {
        AuthToken GetById(long id);

        AuthToken GetByUuid(Guid uuid);

        AuthToken GetByToken(string token);
        AuthToken GetByAccountId(long id, bool Isweb);
        AuthToken GetTokenByWeb(string token, bool Isweb);
        bool AnyByToken(string token);

        Task DisableAllSessions(long accountId);
    }
}
