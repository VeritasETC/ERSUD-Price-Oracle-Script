using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModel.Admin.Request
{
    public class AccountLoginRequest
    {
        [Required]
        [JsonProperty(PropertyName = "UserInfo")]
        public string UserInfo { get; set; }

        [Required]
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
