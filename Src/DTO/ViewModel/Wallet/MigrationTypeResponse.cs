using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModel.Wallet
{
    public class MigrationTypeResponse
    {
        public long Id { get; set; }
        public string RedLightNodeDistrict { get; set; }
        public long SwapQuantity { get; set; }
        public long RecieveQuantity { get; set; }
        public string ScarletChains { get; set; }
    }
}
