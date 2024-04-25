using System;
using System.Collections.Generic;

namespace DTO.Models
{
    public partial class RoleRights : CommonDbProp
    {

        public long RoleId { get; set; }
        public long ScreenId { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsEnabled { get; set; }
        public virtual Role Role { get; set; }
        public virtual Screen Screen { get; set; }
    }
}
