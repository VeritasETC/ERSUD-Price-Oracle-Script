using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModel.Admin.Screen
{
    public class ScreenResponseModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        //public List<RightsModel> Rights { get; set; }
    }
    public class RightsModel
    {
        public string ScreenId { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    }
}
