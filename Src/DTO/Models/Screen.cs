﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class Screen : CommonDbProp
    {
        public Screen()
        {
            RoleRights = new HashSet<RoleRights>();
        }
        public string Name { get; set; }
        public virtual ICollection<RoleRights> RoleRights { get; set; }
    }
}
