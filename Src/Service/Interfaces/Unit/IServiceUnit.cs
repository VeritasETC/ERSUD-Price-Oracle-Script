using Service.Interfaces.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.Unit
{
    public interface IServiceUnit
    {
        IEmailServices Email { get; }
        ICurrencyService Currency { get; }
        IMasterWalletServices MasterWallet { get; }
        IWalletServices Wallet { get; }
        IAccountService Account { get; }
        ITokenService Token { get; }

        IRateService Rate { get; }
    }
}
