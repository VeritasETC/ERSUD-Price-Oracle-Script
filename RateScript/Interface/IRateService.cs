using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateScript.Interface
{
    public interface IRateService
    {
        Task<string> UpdateRate();
    }
}
