using DTO.Enums;
using System;

namespace DTO.Models
{
    public class AuthToken : CommonDbProp
    {
        public string Token { get; set; }
        public DateTime? IssuedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public DateTime? NotValidBefore { get; set; }
        public bool IsLogout { get; set; }
        public DateTime? LogoutAt { get; set; }
        public bool? IsWeb { get; set; }
        public long? AccountId { get; set; }
        public AccountType AccountType { get; set; }
        public virtual Account Account { get; set; }
    }
}