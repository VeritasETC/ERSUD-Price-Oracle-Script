using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModel
{
    public class WebResponse<T>
    {
        public bool Success { get; set; }

        public string Exception { get; set; }

        public string Token { get; set; } = "";
        public T Data { get; set; }
        public AccountVM AccountDetail { get; set; }

    }
    /// <summary>
    /// Account Modal
    /// </summary>
    /// 
    public class AccountVM
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public double TotalBalance { get; set; }
    }
}
