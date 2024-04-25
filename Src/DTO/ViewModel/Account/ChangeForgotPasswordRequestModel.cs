namespace DTO.ViewModel.Account
{
    public class ChangeForgotPasswordRequestModel
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
