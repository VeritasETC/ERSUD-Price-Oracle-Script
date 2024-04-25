using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Contract.Models
{
    public partial class MintAssetsFunction : MintAssetsBaseFunction { }

    [Function("safeMint")]

    public class MintAssetsBaseFunction : FunctionMessage
    {
        [Parameter("address", "to", 1)]
        public virtual string to { get; set; }
    }
}