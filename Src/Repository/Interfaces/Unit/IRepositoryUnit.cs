using Repository.Interfaces.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.Unit
{
    public interface IRepositoryUnit
    {
        IAppSettingsRepository AppSettings { get; }
        IAccountConfirmationRepository AccountConfirmation { get; }
        IAuthTokenRepository AuthToken { get; }
        IBlockchainRepository Blockchain { get; }
        ICurrencyRepository Currency { get; }
        IMasterWalletRepository MasterWallet { get; }
        IAccountRepository Account { get; }
        IRoleRepository Role { get; }
        IRateRepository RateRepo { get; }
        IRightRepositry Right { get; }
        IRoleRightsRepository RoleRights { get; }
        IAccountRoleRepository AccountRole { get; }
        IScreenRepository Screen { get; }
        IRefferalBonusRepository RefferalBonus { get; }

        void DetachAllEntities();

        void Save();

        Task SaveAsync();

        void BeginTransaction();

        void CommitTransaction();

        void RollBackTransaction();

        Task BeginTransactionAsync();
    }
}
