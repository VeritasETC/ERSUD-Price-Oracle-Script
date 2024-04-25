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
    public partial class AddLiquidityEthFunction : AddLiquidityEthBaseFunction { }

    [Function("addLiquidity")]
    public class AddLiquidityEthBaseFunction : FunctionMessage
    {
        [Parameter("address", "token", 1)]
        public virtual string token { get; set; }
        
        [Parameter("uint", "amountTokenDesired", 1)]
        public virtual BigInteger amountTokenDesired { get; set; }
        [Parameter("uint", "amountETHMin", 1)]
        public virtual BigInteger amountETHMin { get; set; }
        [Parameter("address", "to", 1)]
        public virtual string to { get; set; }
        [Parameter("uint", "deadline", 1)]
        public virtual BigInteger deadline { get; set; }
    }
}