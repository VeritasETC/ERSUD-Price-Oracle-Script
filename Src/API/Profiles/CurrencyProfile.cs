using AutoMapper;
using DTO.ViewModel.CurrencyPair;
using DTO.Models;
using DTO.ViewModel.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Profiles
{
    public class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            CreateMap<Currency, CurrencyResponseModel>();
            CreateMap<Blockchain, BlockChainResponseModel>();
        }
    }
}
