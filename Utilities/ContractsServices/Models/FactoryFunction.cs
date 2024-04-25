﻿using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Contract.Models
{
    public partial class FactoryFunction : FactoryBaseFunction { }
    [Function("factory")]
    public class FactoryBaseFunction : FunctionMessage
    {
    }
}