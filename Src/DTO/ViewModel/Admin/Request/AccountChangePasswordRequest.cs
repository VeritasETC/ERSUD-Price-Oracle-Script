using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModel.Admin.Request
{
    public class AccountChangePasswordRequest
    {
        [Required]
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty(PropertyName = "oldPassword")]
        public string OldPassword { get; set; } = "";

        [Required]
        [JsonProperty(PropertyName = "newPassword")]
        public string NewPassword { get; set; } = "";
    }
}
