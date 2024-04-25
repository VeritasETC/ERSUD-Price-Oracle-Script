using DTO.ViewModel.Admin.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModel.Admin
{
    public class GetUsersResponse : ListGeneralModel
    {
        [JsonProperty(PropertyName = "users")]
        public List<CreateUserRequest> Users { get; set; } = new List<CreateUserRequest>();
    }
}