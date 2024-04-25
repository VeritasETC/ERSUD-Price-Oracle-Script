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
    public partial class CreatePairFunction : CreatePairBaseFunction { }

    [Function("createPair")]
    public class CreatePairBaseFunction : FunctionMessage
    {
        [Parameter("address", "tokenA", 1)]
        public virtual string tokenA { get; set; }
        [Parameter("address", "tokenB", 1)]
        public virtual string tokenB { get; set; }
    }
}