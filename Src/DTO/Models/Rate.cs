using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class Rate : CommonDbProp
    {
        public decimal latestRate { get; set; }
        public decimal contractLatestRate { get; set; }
        public decimal Percentage { get; set; }
        public int UpdateHour { get; set; }
        public DateTime ContractUpdatedAt { get; set; }

    }
}
