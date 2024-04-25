using DTO.Models;
using DTO.ViewModel;
using Nethereum.RPC.Eth.DTOs;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IRateService
    {
        Task<ServiceResult<decimal>> GetRate();
        Task<ServiceResult<string>> UpdatePercentage(UpdatePercentageRequest request);
        Task<ServiceResult<TransactionReceipt>> RateUpdateFunction();
    }
}
