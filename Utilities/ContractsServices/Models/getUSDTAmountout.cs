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
    [Function("getUSDTAmountout")]
    public class getUSDTAmountout : FunctionMessage
    {

        [Parameter("uint256", "amountIn")]
        public virtual BigInteger amountIn { get; set; }
    }
}
