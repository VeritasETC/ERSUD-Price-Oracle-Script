using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModel.MasterWallet
{
    public class MasterWalletRequestModel
    {
        public string Address { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public decimal PendingBalance { get; set; }
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
    }
}
