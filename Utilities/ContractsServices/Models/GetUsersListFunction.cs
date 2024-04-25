using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ethereum.Contract.Models
{
    public partial class GetUsersListFunction : GetUsersListBaseFunction { }
    [Function("GetUsersList")]
    public class GetUsersListBaseFunction : FunctionMessage
    {
    }
}