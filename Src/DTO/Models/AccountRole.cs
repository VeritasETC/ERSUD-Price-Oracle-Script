using System;
using System.Collections.Generic;

namespace DTO.Models
{
    public class AccountRole : CommonDbProp
    {

        public long AccountId { get; set; }
        public long RoleId { get; set; }
        public bool IsEnabled { get; set; }
        public virtual Account Account { get; set; }
        public virtual Role Roles { get; set; }
    }
}
