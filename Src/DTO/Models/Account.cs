using DTO.Enums;
using System;
using System.Collections.Generic;

namespace DTO.Models
{
    public partial class Account : CommonDbProp
    {
        public Account()
        {
            AccountRoles = new HashSet<AccountRole>();
            AccountConfirmation = new HashSet<AccountConfirmation>();

        }
        public string NickName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsEmailVerfied { get; set; }
        public bool IsVerfiedAccount { get; set; }
        public bool TwoFactorAuthEnabled { get; set; }
        public DateTime? EmailVerfiedAt { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string ResidentialAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ProfileImage { get; set; }
        public string RefferalId { get; set; }
        public bool IsBlocked { get; set; }
        public EProfileStatus ProfileStatus { get; set; }
        public AccountType AccountType { get; set; }
        public AccountStatusEnum AccountStatus { get; set; }
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
        public virtual ICollection<AccountConfirmation> AccountConfirmation { get; set; }

    }
}
