using DTO.ViewModel.Account;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModel.Admin
{
    public class AccountLoginResponse
    {
        [JsonProperty(PropertyName = "accessToken")]
        public string AccessToken { get; set; } = "";
        public AccountViewModel UserInfo { get; set; }
        public List<RoleRightsModel> Rights { get; set; }
    }
    public class RoleRightsModel
    {
        public long ScreenId { get; set; }
        public string ScreenName { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsEnabled { get; set; }
        public string RoleName { get; set; }
    }
}
