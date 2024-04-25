using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModel.Admin
{
    public class UserResponseModel
    {
        public long Id { get; set; }
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfileImage { get; set; }
        public string ProfilImage { get; set; }
        public string Bio { get; set; }
        public string TwitterLink { get; set; }
        public string InstagramLink { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public string AccountStatus { get; set; }
        public bool IsBlock { get; set; }
        public DateTime Created { get; set; }
    }
}
