using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace DTO.ViewModel
{
    public class UpdateRateResponce
    {
        public string errorMessage {  get; set; }
        public string status {  get; set; }
        public decimal latestRate {  get; set; }
    }
}
