using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Context;
using Repository.Interfaces;
using Repository.Implementations;
using Repository.Interfaces.Unit;
using Repository.Interfaces.Admin;
using Repository.Implementations.Admin;

namespace Repository.Implementations.Unit
{
    internal class RepositoryUnit : IRepositoryUnit
    {
        private readonly ExchnageContext _db;

        private IAppSettingsRepository _appSettings;
        private IAccountConfirmationRepository _accountConfirmation;
        private IAuthTokenRepository _authToken;
        private IBlockchainRepository _blockchain;
        private ICurrencyRepository _currency;
        private IMasterWalletRepository _masterWallet;
        private IAccountRepository _account;
        private IRoleRepository _role;
        private IRightRepositry _right;
        private IRoleRightsRepository _roleRights;
        private IAccountRoleRepository _accountRole;
        private IScreenRepository _screen;
        private IRefferalBonusRepository _refferalBonus;
        private IRateRepository _rateRepo;

        public RepositoryUnit(ExchnageContext db)
        {
            _db = db;
        }

        public IAppSettingsRepository AppSettings =>
            _appSettings ??= new AppSettingsRepository(_db);

        public IAuthTokenRepository AuthToken =>
            _authToken ??= new AuthTokenRepository(_db);

        public IAccountConfirmationRepository AccountConfirmation =>
            _accountConfirmation ??= new AccountConfirmationRepository(_db);

        public IBlockchainRepository Blockchain =>
            _blockchain ??= new BlockchainRepository(_db);

        public ICurrencyRepository Currency =>
            _currency ??= new CurrencyRepository(_db);

        public IMasterWalletRepository MasterWallet =>
            _masterWallet ??= new MasterWalletRepository(_db);

        public IAccountRepository Account =>
             _account ??= new AccountRepository(_db);

        public IRoleRepository Role =>
                _role ??= new RoleRepository(_db);

        public IRightRepositry Right =>
            _right ??= new RightRepository(_db);

        public IRoleRightsRepository RoleRights =>
            _roleRights ??= new RoleRightsRepository(_db);

        public IAccountRoleRepository AccountRole =>
            _accountRole ??= new AccountRoleRepository(_db);

        public IScreenRepository Screen =>
            _screen ??= new ScreenRepository(_db);

        public IRefferalBonusRepository RefferalBonus =>
             _refferalBonus ??= new RefferalBonusRepository(_db);

        public IRateRepository RateRepo => _rateRepo ??= new RateRepository(_db);

        public void Save()
        {
            SetCommonValues();
            _db.SaveChanges();
        }

        public void SetCommonValues()
        {
            var AddedEntities = _db.ChangeTracker.Entries()
        .Where(E => E.State == EntityState.Added)
        .ToList();

            AddedEntities.ForEach(E =>
            {
                E.Property("CreatedAt").CurrentValue = E.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            });

            var EditedEntities = _db.ChangeTracker.Entries()
                .Where(E => E.State == EntityState.Modified)
                .ToList();

            EditedEntities.ForEach(E =>
            {
                E.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            });
        }

        public async Task SaveAsync()
        {
            SetCommonValues();
            await _db.SaveChangesAsync();
        }

        public void BeginTransaction()
        {
            _db.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _db.Database.CommitTransaction();
        }

        public void RollBackTransaction()
        {
            _db.Database.RollbackTransaction();
        }

        public async Task BeginTransactionAsync()
        {
            await _db.Database.BeginTransactionAsync();
        }

        public void DetachAllEntities()
        {
            var changedEntriesCopy = _db.ChangeTracker.Entries()
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }
    }
}
