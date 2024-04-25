using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModel.Currency
{
    public class BlockChainResponseModel
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string NetworkName { get; set; }
        public string NetworkShortName { get; set; }
        public string RpcUrl { get; set; }
        public int? ChainID { get; set; }
        public string Symbol { get; set; }
        public string BlockExplorerUrl { get; set; }
        public BlockchainNetworkType BlockchainNetworkType { get; set; }
        public EBlockchainType BlockchainType { get; set; }
    }
}
