using DTO.Enums;
using System;
using System.Collections.Generic;

namespace DTO.Models
{
    public class Currency : CommonDbProp
    {
        public Currency()
        {
            MasterWallets = new HashSet<MasterWallet>();
        }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public long BlockchainId { get; set; }
        public decimal RateInUSD { get; set; }
        public DateTime? RateUpdatedAt { get; set; }
        public bool CanUpdate { get; set; }
        public int? Decimals { get; set; }
        public string SmartContractAddress { get; set; }
        public string Image { get; set; }
        public string CurrencyIcon { get; set; }
        public decimal? RateBTC { get; set; }
        public decimal? RateGBP { get; set; }
        public decimal? RateEUR { get; set; }
        public decimal? RateUSD { get; set; }
        public decimal? TxFee { get; set; }
        public decimal? PlateformFee { get; set; }
        public string Color { get; set; }
        public FileStorageType? FileStorageType { get; set; }
        public virtual Blockchain Blockchain { get; set; }
        public virtual ICollection<MasterWallet> MasterWallets { get; set; }
    }
}


