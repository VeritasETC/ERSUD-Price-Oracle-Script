using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModel.Admin.Request
{
    public class AssignRight
    {
        [Required]
        public long RoleId { get; set; }

        [Required]
        public List<AssignRightViewModel> RightList { get; set; }
    }
    public class AssignRightViewModel
    {
        public long ScreenId { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    }
}
