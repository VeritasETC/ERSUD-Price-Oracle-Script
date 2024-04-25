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
    public partial class AddLiquidityFunction : AddLiquidityBaseFunction { }

    [Function("addLiquidity")]
    public class AddLiquidityBaseFunction : FunctionMessage
    {
        [Parameter("uint256", "usdtamount", 1)]
        public virtual BigInteger usdtamount { get; set; }
        [Parameter("uint256", "oskamount", 1)]
        public virtual BigInteger oskamount { get; set; }
        [Parameter("address", "toaddress", 1)]
        public virtual string toaddress { get; set; }
    }
}