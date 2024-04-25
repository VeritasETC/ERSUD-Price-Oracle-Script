using Microsoft.AspNetCore.Http;

namespace DTO.ViewModel.Account
{
    public class AccountSignUpRequestModel
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public string RefferalId { get; set; }
    }
}
