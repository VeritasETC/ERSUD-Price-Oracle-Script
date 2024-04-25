using AutoMapper;
using DTO.Models;
using DTO.ViewModel.Account;
using DTO.ViewModel.Admin;
using DTO.ViewModel.Admin.Screen;
using DTO.ViewModel.MasterWallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
           
            CreateMap<MasterWallet, MasterWalletResponseModel>();
            CreateMap<Screen, ScreenResponseModel>().ReverseMap();
            CreateMap<RoleRights, RoleRightsModel>()
                .ForMember(des => des.ScreenName, opt => opt.MapFrom(src => src.Screen.Name))
                .ForMember(des => des.RoleName, opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<Account, AccountViewModel>().ReverseMap();
            CreateMap<Account, AccountProfileViewModel>().ReverseMap();
            CreateMap<Account, UpdateAccountRequestModel>().ReverseMap();
        }
    }
}
