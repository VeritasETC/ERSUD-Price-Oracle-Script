using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModel.Nft
{
    public class AddNftRequestModel
    {
        public string NFTTokenId { get; set; }
        public string Name { get; set; }
        public string NFTSmartContractAddress { get; set; }
        public string ExternalLink { get; set; }
        public string Description { get; set; }
        public string NFTFileName { get; set; }
        public string BuyPrice { get; set; }
        public string SellPrice { get; set; }
    }
}
