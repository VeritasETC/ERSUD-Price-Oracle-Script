using System;

namespace DTO.ViewModel.CurrencyPair
{
    public class CurrencyPairResponseModel
    {
        public Guid Uuid { get; set; }
        public Guid CurrencyOneUuid { get; set; }
        public Guid CurrencyTwoUuid { get; set; }
        public string CurrencyOneImage { get; set; }
        public string CurrencyTwoImage { get; set; }
        public string CurrencyOne { get; set; }
        public string CurrencyTwo { get; set; }
        public string Status { get; set; }
        public long CurrencyOneBlockchainId { get; set; }
        public long CurrencyTwoBlockchainId { get; set; }
        public string CurrencyOneSmartContractAddress { get; set; }
        public string CurrencyTwoSmartContractAddress { get; set; }
        public string Description { get; set; }
    }
}
