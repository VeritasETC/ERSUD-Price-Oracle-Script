using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IFileManagementService
    {
        Task<(string response, bool isSuccess)> UploadProfileImageFile(IFormFile File, string Prefix, string[] AllowedFileTypes, long MaxFileSize = 2097152);
        Task<(string response, bool isSuccess)> UploadProductImageFile(IFormFile File, string Prefix, string[] AllowedFileTypes, long MaxFileSize = 100000000);
        Task<(string response, bool isSuccess)> UploadCurrencyImageFile(IFormFile File, string Prefix, string[] AllowedFileTypes, long MaxFileSize = 2097152);
        Task<(string response, bool isSuccess)> UploadPoolImageFile(IFormFile File, string Prefix, string[] AllowedFileTypes, long MaxFileSize = 100000000);
    }
}
