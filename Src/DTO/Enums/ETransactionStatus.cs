using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Enums
{
    public enum ETransactionStatus
    {
        Pending = 1,
        Confirm = 2,
        Reverted = 3,
        Cancelled = 4,
        Failed = 5
    }
}
