using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Contract.Models
{
    [FunctionOutput]
    public class GetRlcUserResponse : IFunctionOutputDTO
    {
        [Parameter("bool", "isMinted", 1)]
        public virtual bool isMinted { get; set; }

        [Parameter("uint256", "noofRedchain", 2)]
        public virtual BigInteger noofRedchain { get; set; }

        [Parameter("uint256", "noOfBlackchain", 3)]
        public virtual BigInteger noOfBlackchain { get; set; }

        [Parameter("uint256", "noOfPlatinumchain", 4)]
        public virtual BigInteger noOfPlatinumchain { get; set; }

        [Parameter("uint256", "noOfScarletToken", 5)]
        public virtual BigInteger noOfScarletToken { get; set; }

        [Parameter("address", "user", 6)]
        public virtual string user { get; set; }
    }
}