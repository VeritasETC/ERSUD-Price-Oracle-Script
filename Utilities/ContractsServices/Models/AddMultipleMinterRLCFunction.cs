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
    public partial class AddMultipleMinterRLCFunction : AddMultipleMinterRLCBaseFunction { }

    [Function("multipleMinterRLC")]
    public class AddMultipleMinterRLCBaseFunction : FunctionMessage
    {
        [Parameter("uint256", "noRedChains", 1)]
        public virtual BigInteger noRedChains { get; set; }

        [Parameter("uint256", "noBlackchains", 1)]
        public virtual BigInteger noBlackchains { get; set; }

        [Parameter("uint256", "noPlatinumChain", 1)]
        public virtual BigInteger noPlatinumChain { get; set; }

        [Parameter("uint256", "noScarletTokens", 1)]
        public virtual BigInteger noScarletTokens { get; set; }

        [Parameter("address", "userAddress", 1)]
        public virtual string userAddress { get; set; }
    }
}
