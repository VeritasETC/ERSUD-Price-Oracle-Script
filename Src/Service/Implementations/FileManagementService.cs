using Microsoft.AspNetCore.Http;
using Service.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Implementations
{
    internal class FileManagementService : IFileManagementService
    {

        public FileManagementService()
        {
        }

        public async Task<(string response, bool isSuccess)> UploadProfileImageFile(IFormFile File, string Prefix, string[] AllowedFileTypes, long MaxFileSize = 2097152)
        {
            try
            {
                if (File == null || File.Length <= 0)
                {
                    return (null, false);
                }
                if (File.Length > MaxFileSize)
                {
                    return ($"Max size: {MaxFileSize / 10000} Kilobytes", false);
                }
                if (AllowedFileTypes.Length > 0 ? !AllowedFileTypes.Contains(File.ContentType, StringComparer.OrdinalIgnoreCase) : false)
                {
                    return ($"Allowed format {string.Join(", ", AllowedFileTypes)}", false);
                }
                var uniqueName = DateTime.UtcNow.Ticks.ToString() + Path.GetExtension(File.FileName);
                var dbPAthTemp = Path.Combine(Path.Combine("Images", "Profile"), Prefix + "_" + uniqueName);
                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Images", "Profile"))))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Images", "Profile")));
                }
                var FullfilePath = Path.Combine(Directory.GetCurrentDirectory(), dbPAthTemp);
                using (Stream fileStream = new FileStream(FullfilePath, FileMode.Create))
                {
                    await File.CopyToAsync(fileStream);
                }
                return (Path.Combine(Path.Combine("Images", "Profile"), Prefix + "_" + uniqueName), true);
            }
            catch (Exception ex) { throw ex; }
        }
        public async Task<(string response, bool isSuccess)> UploadProductImageFile(IFormFile File, string Prefix, string[] AllowedFileTypes, long MaxFileSize = 100000000)
        {
            try
            {
                if (File == null || File.Length <= 0)
                {
                    return (null, false);
                }
                if (File.Length > MaxFileSize)
                {
                    return ($"Max size: {MaxFileSize / 10000} Kilobytes", false);
                }
                if (AllowedFileTypes.Length > 0 ? !AllowedFileTypes.Contains(File.ContentType, StringComparer.OrdinalIgnoreCase) : false)
                {
                    return ($"Allowed format {string.Join(", ", AllowedFileTypes)}", false);
                }
                var uniqueName = DateTime.UtcNow.Ticks.ToString() + Path.GetExtension(File.FileName);
                var dbPAthTemp = Path.Combine(Path.Combine("Images", "Product"), Prefix + "_" + uniqueName);
                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Images", "Product"))))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Images", "Product")));
                }
                var FullfilePath = Path.Combine(Directory.GetCurrentDirectory(), dbPAthTemp);
                using (Stream fileStream = new FileStream(FullfilePath, FileMode.Create))
                {
                    await File.CopyToAsync(fileStream);
                }
                return (Path.Combine(Path.Combine("Images", "Product"), Prefix + "_" + uniqueName), true);
            }
            catch (Exception ex) { throw ex; }
        }
        public async Task<(string response, bool isSuccess)> UploadCurrencyImageFile(IFormFile File, string Prefix, string[] AllowedFileTypes, long MaxFileSize = 2097152)
        {
            try
            {
                if (File == null || File.Length <= 0)
                {
                    return (null, false);
                }
                if (File.Length > MaxFileSize)
                {
                    return ($"Max size: {MaxFileSize / 10000} Kilobytes", false);
                }
                if (AllowedFileTypes.Length > 0 ? !AllowedFileTypes.Contains(File.ContentType, StringComparer.OrdinalIgnoreCase) : false)
                {
                    return ($"Allowed format {string.Join(", ", AllowedFileTypes)}", false);
                }
                var uniqueName = DateTime.UtcNow.Ticks.ToString() + Path.GetExtension(File.FileName);
                var dbPAthTemp = Path.Combine(Path.Combine("Images", "Currency"), Prefix + "_" + uniqueName);
                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Images", "Currency"))))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Images", "Currency")));
                }
                var FullfilePath = Path.Combine(Directory.GetCurrentDirectory(), dbPAthTemp);
                using (Stream fileStream = new FileStream(FullfilePath, FileMode.Create))
                {
                    await File.CopyToAsync(fileStream);
                }
                return (Path.Combine(Path.Combine("Images", "Currency"), Prefix + "_" + uniqueName), true);
            }
            catch (Exception ex) { throw ex; }
        }
        public async Task<(string response, bool isSuccess)> UploadPoolImageFile(IFormFile File, string Prefix, string[] AllowedFileTypes, long MaxFileSize = 100000000)
        {
            try
            {
                if (File == null || File.Length <= 0)
                {
                    return (null, false);
                }
                if (File.Length > MaxFileSize)
                {
                    return ($"Max size: {MaxFileSize / 10000} Kilobytes", false);
                }
                if (AllowedFileTypes.Length > 0 ? !AllowedFileTypes.Contains(File.ContentType, StringComparer.OrdinalIgnoreCase) : false)
                {
                    return ($"Allowed format {string.Join(", ", AllowedFileTypes)}", false);
                }
                var uniqueName = DateTime.UtcNow.Ticks.ToString() + Path.GetExtension(File.FileName);
                var dbPAthTemp = Path.Combine(Path.Combine("Images", "Pool"), Prefix + "_" + uniqueName);
                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Images", "Pool"))))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Images", "Pool")));
                }
                var FullfilePath = Path.Combine(Directory.GetCurrentDirectory(), dbPAthTemp);
                using (Stream fileStream = new FileStream(FullfilePath, FileMode.Create))
                {
                    await File.CopyToAsync(fileStream);
                }
                return (Path.Combine(Path.Combine("Images", "Pool"), Prefix + "_" + uniqueName), true);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}