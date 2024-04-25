using DTO.Enums;
using Microsoft.AspNetCore.Http;
using System;

namespace DTO.ViewModel.Currency
{
    public class UploadCurrencyImageRequestModel
    {
        public Guid CurrenctUuid { get; set; }
        public IFormFile Image { get; set; }
        public FileStorageType? FileStorageType { get; set; }
    }
}
