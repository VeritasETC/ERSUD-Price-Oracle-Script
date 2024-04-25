using DTO.Models;
using DTO.ViewModel;
using Repository.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRateRepository : IRepositoryBase<Rate>
    {
        Task<Rate> GetRate();
    }
}
