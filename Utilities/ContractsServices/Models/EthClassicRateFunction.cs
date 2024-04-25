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
 

    public partial class EthClassicRateFunction : EthClassicRateBaseFunction { }

    [Function("setETHCRate")]
    public class EthClassicRateBaseFunction : FunctionMessage
    {
        [Parameter("uint256", "amount", 1)]
        public virtual BigInteger amount { get; set; }

    }
}
