using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModel.Currency
{
    public class AddCurrencyRequestModel
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public long BlockchainId { get; set; }
        public decimal RateInUSD { get; set; }
        public bool CanUpdate { get; set; }
        public int? Decimals { get; set; }
        public string SmartContractAddress { get; set; }
    }
}
