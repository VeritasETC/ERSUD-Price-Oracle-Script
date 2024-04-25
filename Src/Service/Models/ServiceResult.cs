using DTO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class ServiceResult<T> : ServiceResult
    {
        protected ServiceResult()
        {
        }

        public ServiceResult(ErrorResponse error, bool isSuccess, string message, T data)
        {
            Error = error;
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public T Data { get; set; }
    }

    public class ServiceListResult<T> : ServiceResult<T>
    {
        public ServiceListResult(ErrorResponse error, bool isSuccess, string message, T data)
        {
            Error = error;
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
    }

    public class ServiceResult
    {
        public ServiceResult() { }

        public ServiceResult(ErrorResponse error, bool isSuccess, string message)
        {
            Error = error;
            IsSuccess = isSuccess;
            Message = message;
        }

        public ErrorResponse Error { get; set; }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }

    public static class ServiceResults
    {
        public static ServiceResult AddedSuccessfully()
        {
            string message = "Data successfully added";
            var result = new ServiceResult(null, true, message);
            return result;
        }

        public static ServiceResult<TData> AddedSuccessfully<TData>(TData data)
        {
            string message = "Data successfully added";
            var result = new ServiceResult<TData>(null, true, message, data);
            return result;
        }
        public static ServiceResult ContractUpdatedSuccessfully()
        {
            string message = "successfully updated";
            var result = new ServiceResult(null, true, message);
            return result;
        }

        public static ServiceResult<TData> ContractUpdatedSuccessfully<TData>(TData data)
        {
            string message = "successfully updated";
            var result = new ServiceResult<TData>(null, true, message, data);
            return result;
        }

        public static ServiceResult GetSuccessfully()
        {
            string message = "Data successfully get";
            var result = new ServiceResult(null, true, message);
            return result;
        }

        public static ServiceResult<TData> GetSuccessfully<TData>(TData data)
        {
            string message = "Data successfully get";
            var result = new ServiceResult<TData>(null, true, message, data);
            return result;
        }

        public static ServiceResult CheckSuccessfully()
        {
            string message = "Check successfully";
            var result = new ServiceResult(null, true, message);
            return result;
        }

        public static ServiceResult<TData> CheckSuccessfully<TData>(TData data)
        {
            string message = "Check successfully";
            var result = new ServiceResult<TData>(null, true, message, data);
            return result;
        }

        public static ServiceListResult<TData> GetListSuccessfully<TData>(TData data, int TotalItmes = 0)
        {
            string message = "Data successfully returned";
            var result = new ServiceListResult<TData>(null, true, message, data);
            result.TotalItems = TotalItmes;
            return result;
        }

        public static ServiceResult UpdatedSuccessfully()
        {
            string message = "Data successfully updated";
            var result = new ServiceResult(null, true, message);
            return result;
        }

        public static ServiceResult<TData> UpdatedSuccessfully<TData>(TData data)
        {
            string message = "Data successfully updated";
            var result = new ServiceResult<TData>(null, true, message, data);
            return result;
        }

        public static ServiceResult DeletedSuccessfully()
        {
            string message = "Data successfully deleted";
            var result = new ServiceResult(null, true, message);
            return result;
        }

        public static ServiceResult<TData> DeletedSuccessfully<TData>(TData data)
        {
            string message = "Data successfully deleted";
            var result = new ServiceResult<TData>(null, true, message, data);
            return result;
        }

        public static ServiceResult SuccessfullyLogin()
        {
            string message = "Successfully login";
            var result = new ServiceResult(null, true, message);
            return result;
        }

        public static ServiceResult<TData> SuccessfullyLogin<TData>(TData data)
        {
            string message = "Successfully login";
            var result = new ServiceResult<TData>(null, true, message, data);
            return result;
        }

        public static ServiceResult SuccessfullyLogout()
        {
            string message = "Successfully logout";
            var result = new ServiceResult(null, true, message);
            return result;
        }

        public static ServiceResult<TData> SuccessfullyLogout<TData>(TData data)
        {
            string message = "Successfully logout";
            var result = new ServiceResult<TData>(null, true, message, data);
            return result;
        }

        public static ServiceResult Successfully(string message)
        {
            var result = new ServiceResult(null, true, message);
            return result;
        }

        public static ServiceResult<TData> Successfully<TData>(string message, TData data)
        {
            var result = new ServiceResult<TData>(null, true, message, data);
            return result;
        }

        public static class Errors
        {

            public static ServiceResult Required(string value)
            {
                string message = $"{value} required";
                var result = new ServiceResult(new ErrorResponse(nameof(Invalid), message), false, message);
                return result;
            }
            public static ServiceResult<TData> Blocked<TData>(string value, TData data)
            {
                string message = $"{value} blocked";
                var result = new ServiceResult<TData>(new ErrorResponse(nameof(Blocked), message), false, message, data);
                return result;
            }
            public static ServiceResult<TData> Required<TData>(string value, TData data)
            {
                string message = $"{value} required";
                var result = new ServiceResult<TData>(new ErrorResponse(nameof(Invalid), message), false, message, data);
                return result;
            }

            public static ServiceResult Invalid(string value)
            {
                string message = $"Invalid {value}";
                var result = new ServiceResult(new ErrorResponse(nameof(Invalid), message), false, message);
                return result;
            }

            public static ServiceResult Error(string value)
            {
                string message = $"Error {value}";
                var result = new ServiceResult(new ErrorResponse(nameof(Invalid), message), false, message);
                return result;
            }

            public static ServiceResult<TData> Invalid<TData>(string value, TData data)
            {
                string message = $"Invalid {value}";
                var result = new ServiceResult<TData>(new ErrorResponse(nameof(Invalid), message), false, message, data);
                return result;
            }

            public static ServiceResult NotFound(string value)
            {
                string message = $"{value} not found";
                var result = new ServiceResult(new ErrorResponse(nameof(NotFound), message), false, message);
                return result;
            }

            public static ServiceResult Success(string value)
            {
                string message = $"{value} successfully updated";
                var result = new ServiceResult(new ErrorResponse(nameof(Success), message), false, message);
                return result;
            }

            public static ServiceResult Unauthorized(string value)
            {
                string message = $"{value} unauthorized";
                var result = new ServiceResult(new ErrorResponse(nameof(Unauthorized), message), false, message);
                return result;
            }

            public static ServiceResult<TData> Unauthorized<TData>(string value, TData data)
            {
                string message = $"{value} Unauthorized";
                var result = new ServiceResult<TData>(new ErrorResponse(nameof(Unauthorized), message), false, message, data);
                return result;
            }

            public static ServiceResult<TData> NotFound<TData>(string value, TData data)
            {
                string message = $"{value} not found";
                var result = new ServiceResult<TData>(new ErrorResponse(nameof(NotFound), message), false, message, data);
                return result;
            }

            public static ServiceResult AlreadyExist(string value)
            {
                string message = $"{value} already exist";
                var result = new ServiceResult(new ErrorResponse(nameof(AlreadyExist), message), false, message);
                return result;
            }

            public static ServiceResult<TData> AlreadyExist<TData>(string value, TData data)
            {
                string message = $"{value} already exist";
                var result = new ServiceResult<TData>(new ErrorResponse(nameof(AlreadyExist), message), false, message, data);
                return result;
            }

            public static ServiceResult HeaderMissing(string value)
            {
                string message = $"The header {value} is missing";
                var result = new ServiceResult(new ErrorResponse(nameof(HeaderMissing), message), false, message);
                return result;
            }

            public static ServiceResult<TData> HeaderMissing<TData>(string value, TData data)
            {
                string message = $"The header {value} is missing";
                var result = new ServiceResult<TData>(new ErrorResponse(nameof(HeaderMissing), message), false, message, data);
                return result;
            }

            public static ServiceResult HeaderValueMissing(string value)
            {
                string message = $"The header {value} value is missing";
                var result = new ServiceResult(new ErrorResponse(nameof(HeaderValueMissing), message), false, message);
                return result;
            }

            public static ServiceResult<TData> HeaderValueMissing<TData>(string value, TData data)
            {
                string message = $"The header {value} value is missing";
                var result = new ServiceResult<TData>(new ErrorResponse(nameof(HeaderValueMissing), message), false, message, data);
                return result;
            }

            public static ServiceResult HasEmail(string value)
            {
                string message = $"The email address {value} already exist";
                var result = new ServiceResult(new ErrorResponse(nameof(HasEmail), message), false, message);
                return result;
            }

            public static ServiceResult<TData> HasEmail<TData>(string value, TData data)
            {
                string message = $"The email address {value} already exist";
                var result = new ServiceResult<TData>(new ErrorResponse(nameof(HasEmail), message), false, message, data);
                return result;
            }

            public static ServiceResult HasUsername(string value)
            {
                string message = $"The username {value} already exist";
                var result = new ServiceResult(new ErrorResponse(nameof(HasUsername), message), false, message);
                return result;
            }

            public static ServiceResult<TData> HasUsername<TData>(string value, TData data)
            {
                string message = $"The username {value} already exist";
                var result = new ServiceResult<TData>(new ErrorResponse(nameof(HasUsername), message), false, message, data);
                return result;
            }

            public static ServiceResult HasPhoneNumber(string value)
            {
                string message = $"The phone number {value} already exist";
                var result = new ServiceResult(new ErrorResponse(nameof(HasPhoneNumber), message), false, message);
                return result;
            }

            public static ServiceResult<TData> HasPhoneNumber<TData>(string value, TData data)
            {
                string message = $"The phone number {value} already exist";
                var result = new ServiceResult<TData>(new ErrorResponse(nameof(HasPhoneNumber), message), false, message, data);
                return result;
            }

            public static ServiceResult InvalidAccountType()
            {
                string message = $"Invalid account type";
                var result = new ServiceResult(new ErrorResponse(nameof(InvalidAccountType), message), false, message);
                return result;
            }

            public static ServiceResult<TData> InvalidAccountType<TData>(TData data)
            {
                string message = $"Invalid account type";
                var result = new ServiceResult<TData>(new ErrorResponse(nameof(InvalidAccountType), message), false, message, data);
                return result;
            }

            public static ServiceResult InvalidUsernameOrPassword()
            {
                string message = $"Invalid username or password";
                var result = new ServiceResult(new ErrorResponse(nameof(InvalidUsernameOrPassword), message), false, message);
                return result;
            }

            public static ServiceResult<TData> InvalidUsernameOrPassword<TData>(TData data)
            {
                string message = $"Invalid username or password";
                var result = new ServiceResult<TData>(new ErrorResponse(nameof(InvalidUsernameOrPassword), message), false, message, data);
                return result;
            }

            public static ServiceResult InvalidJwtToken()
            {
                string message = $"Invalid jwt token";
                var result = new ServiceResult(new ErrorResponse(nameof(InvalidJwtToken), message), false, message);
                return result;
            }

            public static ServiceResult<TData> InvalidJwtToken<TData>(TData data)
            {
                string message = $"Invalid jwt token";
                var result = new ServiceResult<TData>(new ErrorResponse(nameof(InvalidJwtToken), message), false, message, data);
                return result;
            }
            public static ServiceResult InsufficientWalletBalance()
            {
                string message = $"Insufficient Wallet Balance";
                var result = new ServiceResult(new ErrorResponse(nameof(InsufficientWalletBalance), message), false, message);
                return result;
            }

            public static ServiceResult<TData> InsufficientWalletBalance<TData>(TData data)
            {
                string message = $"Insufficient Wallet Balance";
                var result = new ServiceResult<TData>(new ErrorResponse(nameof(InsufficientWalletBalance), message), false, message, data);
                return result;
            }

            public static ServiceResult UnhandledError(string exMessage)
            {
                string message = $"Unhandled error";
                var result = new ServiceResult(new ErrorResponse(nameof(UnhandledError), message), false, exMessage);
                return result;
            }

            public static ServiceResult<TData> UnhandledError<TData>(string exMessage, TData data)
            {
                string message = $"Unhandled error";
                var result = new ServiceResult<TData>(new ErrorResponse(nameof(UnhandledError), message), false, exMessage, data);
                return result;
            }

            public static ServiceListResult<TData> UnhandledListError<TData>(string exMessage, TData data)
            {
                string message = $"Unhandled error";
                var result = new ServiceListResult<TData>(new ErrorResponse(nameof(UnhandledListError), message), false, exMessage, data);
                return result;
            }

            public static ServiceResult WalletLock(string value)
            {
                string message = $"{value} wallet is Locked";
                var result = new ServiceResult(new ErrorResponse(nameof(WalletLock), message), false, message);
                return result;
            }

            public static ServiceResult<TData> WalletLock<TData>(string value, TData data)
            {
                string message = $"{value} wallet is Locked";
                var result = new ServiceResult<TData>(new ErrorResponse(nameof(WalletLock), message), false, message, data);
                return result;
            }
        }
    }
}
