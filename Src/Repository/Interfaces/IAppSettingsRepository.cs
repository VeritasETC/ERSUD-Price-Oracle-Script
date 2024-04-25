using DTO.Models;
using Repository.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAppSettingsRepository : IRepositoryBase<AppSettings>
    {
        Task<AppSettings> GetByKeywordAsync(string key);
        AppSettings GetByKeyword(string key);

        Task<List<AppSettings>> GetAllAppSettingsAsync();
    }
}
