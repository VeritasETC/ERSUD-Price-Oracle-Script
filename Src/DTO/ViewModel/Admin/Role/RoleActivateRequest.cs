using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModel.Admin.Role
{
    public class RoleActivateRequest
    {
        public long RoleId { get; set; }
        public bool IsEnable { get; set; }
    }
}
