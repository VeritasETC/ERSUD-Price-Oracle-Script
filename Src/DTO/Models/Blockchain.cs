using DTO.Enums;
using System.Collections.Generic;

namespace DTO.Models
{
    public class Blockchain : CommonDbProp
    {
        public Blockchain()
        {
            Currencies = new HashSet<Currency>();
        }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string NetworkName { get; set; }
        public string NetworkShortName { get; set; }
        public string RpcUrl { get; set; }
        public int? ChainID { get; set; }
        public string Symbol { get; set; }
        public string BlockExplorerUrl { get; set; }
        public decimal GasPrice { get; set; }
        public decimal GasLimit { get; set; }
        public BlockchainNetworkType BlockchainNetworkType { get; set; }
        public EBlockchainType BlockchainType { get; set; }
        public virtual ICollection<Currency> Currencies { get; set; }
    }
}
