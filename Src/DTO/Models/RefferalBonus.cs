using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class RefferalBonus : CommonDbProp
    {
        public long FormAccountId { get; set; }
        public virtual Account FromAccount { get; set; }
        public long ToAccountId { get; set; }
        public virtual Account ToAccount { get; set; }
        public string RefferalId { get; set; }
        public bool IsBonusDeliverd { get; set; } = false;
    }
}
