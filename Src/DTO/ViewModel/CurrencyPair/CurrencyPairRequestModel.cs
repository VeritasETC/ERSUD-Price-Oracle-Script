using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModel.CurrencyPair
{
    public class CurrencyPairRequestModel
    {
        public Guid CurrencyOneUuid { get; set; }
        public Guid CurrencyTwoUuid { get; set; }
    }
}
