using DTO.Enums;
using Microsoft.AspNetCore.Http;



namespace DTO.ViewModel.Admin.Request
{
    public class CreateUserRequest
    {
        public long Id { get; set; }
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile ProfileImage { get; set; }
        public string ProfilImage { get; set; }
        public string Bio { get; set; }
        public string TwitterLink { get; set; }
        public string InstagramLink { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public AccountType AccountType { get; set; }

    }
}
