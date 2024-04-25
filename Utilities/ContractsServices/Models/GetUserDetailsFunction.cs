using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Contract.Models
{
    public partial class GetUserDetailsFunction : GetUserDetailsBaseFunction { }
    [Function("getUserDetails")]
    public class GetUserDetailsBaseFunction : FunctionMessage
    {
        [Parameter("address", "user", 1)]
        public virtual string user { get; set; }
    }
}