using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModel.Wallet
{
    public class AddTransactionRequest
    {
        public string Signature { get; set; }
        public string UserAddress { get; set; }
        public string DestinationAddress { get; set; }
        public List<SwapRequest> SwapRequest { get; set; }
    }
    public class SwapRequest
    {
        public string RedLightNode { get; set; }
        public long SwapQuantity { get; set; }
        public long TotalSwapQuantity { get; set; }
        public bool IsToken { get; set; }
        public string ScarletChains { get; set; }
        public long RecieveQuantity { get; set; }
        //public List<RecieveAssetsRequest> RecieveAssets { get; set; }
    }
}
