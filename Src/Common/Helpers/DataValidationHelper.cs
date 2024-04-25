using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Common.Helpers
{
    public static class DataValidationHelper
    {

        public static bool IsValidEmail(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return false;
            }

            return new EmailAddressAttribute().IsValid(source);
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            bool isPhoneNumber = false;

            if (phoneNumber.Length > 10 && phoneNumber.Length <= 15 && phoneNumber.StartsWith('+') && phoneNumber.Trim('+').All(char.IsDigit))
            {
                isPhoneNumber = true;
            }

            return isPhoneNumber;
        }

    }
}
