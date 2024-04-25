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
    public partial class AddUserRLCRecievingDetailsFunction : AddUserRLCRecievingDetailsBaseFunction { }

    [Function("AddRlcDetails")]
    public class AddUserRLCRecievingDetailsBaseFunction : FunctionMessage
    {
        [Parameter("uint256", "noRedchain", 1)]
        public virtual BigInteger noRedchain { get; set; }
        [Parameter("uint256", "noBlackchain", 1)]
        public virtual BigInteger noBlackchain { get; set; }
        [Parameter("uint256", "noPlatinumchain", 1)]
        public virtual BigInteger noPlatinumchain { get; set; }
        [Parameter("uint256", "noscarlettoken", 1)]
        public virtual BigInteger noscarlettoken { get; set; }
        [Parameter("address", "user", 1)]
        public virtual string user { get; set; }
    }
}
