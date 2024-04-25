using DTO.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTO.Models
{
    public class MasterWallet : CommonDbProp
    {
        public MasterWallet()
        {
        }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public decimal Balance { get; set; }
        public decimal PendingBalance { get; set; }
        public bool IsToken { get; set; }
        public string SmartContractAddress { get; set; }
        public EBlockchainType BlockchainType { get; set; }
        public long? CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
    }
}
