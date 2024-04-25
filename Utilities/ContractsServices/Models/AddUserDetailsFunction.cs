using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Contract.Models
{
    public partial class AddUserDetailsFunction : AddUserDetailsBaseFunction { }
    [Function("AddDetails")]
    public class AddUserDetailsBaseFunction: FunctionMessage
    {
        [Parameter("uint256", "noCities", 1)]
        public virtual BigInteger noCities { get; set; }
        [Parameter("uint256", "noDistricts", 1)]
        public virtual BigInteger noDistricts { get; set; }
        [Parameter("uint256", "noMansions", 1)]
        public virtual BigInteger noMansions { get; set; }
        [Parameter("uint256", "noPlaymates", 1)]
        public virtual BigInteger noPlaymates { get; set; }
        [Parameter("address", "user", 1)]
        public virtual string user { get; set; }
    }
}