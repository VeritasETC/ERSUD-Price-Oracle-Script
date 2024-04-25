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
    public partial class SwapFunction : SwapBaseFunction { }
    [Function("swapbnbtousdt")]
    public class SwapBaseFunction : FunctionMessage
    {
    }
}
