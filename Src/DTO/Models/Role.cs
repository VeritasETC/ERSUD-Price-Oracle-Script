using System;
using System.Collections.Generic;

namespace DTO.Models
{
    public partial class Role : CommonDbProp
    {
        public Role()
        {
            AccountRoles = new HashSet<AccountRole>();
            RoleRights = new HashSet<RoleRights>();
        }
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
        public virtual ICollection<RoleRights> RoleRights { get; set; }
    }
}
