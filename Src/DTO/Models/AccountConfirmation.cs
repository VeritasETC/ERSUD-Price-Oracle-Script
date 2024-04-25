using DTO.Enums;
using System;

namespace DTO.Models
{
    public class AccountConfirmation : CommonDbProp
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public DateTime? ExpireAt { get; set; }
        public bool? IsExpired { get; set; }
        public bool IsUsed { get; set; }
        public DateTime? UsedAt { get; set; }
        public long? AccountId { get; set; }
        public virtual Account Account { get; set; }
        public AccountConfirmationType? Type { get; set; }
    }
}
