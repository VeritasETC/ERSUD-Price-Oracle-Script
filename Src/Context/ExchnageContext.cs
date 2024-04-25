using Microsoft.EntityFrameworkCore;
using Context.EntityConfigurations;
using DTO.Models;
using Common.Helpers;

namespace Context
{
    public class ExchnageContext : DbContext
    {
        //public virtual DbSet<AppSettings> AppSettings { get; set; }
        //public virtual DbSet<AccountConfirmation> AccountConfirmations { get; set; }
        //public virtual DbSet<AuthToken> AuthTokens { get; set; }
        //public virtual DbSet<Blockchain> Blockchains { get; set; }
        //public virtual DbSet<Currency> Currencies { get; set; }
        //public virtual DbSet<MasterWallet> MasterWallets { get; set; }
        //public virtual DbSet<AccountRole> AccountRoles { get; set; }
        //public virtual DbSet<Account> Account { get; set; }
        //public virtual DbSet<Role> Roles { get; set; }
        //public virtual DbSet<RoleRights> RoleRights { get; set; }
        //public virtual DbSet<Screen> Screen { get; set; }
        //public virtual DbSet<Right> Rights { get; set; }
        //public virtual DbSet<RefferalBonus> RefferalBonus { get; set; }

        public virtual DbSet<Rate> Rates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppSettingHelper.GetDefaultConnection());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new AppSettingsConfiguration());
            //modelBuilder.ApplyConfiguration(new AccountConfirmationConfiguration());
            //modelBuilder.ApplyConfiguration(new AuthTokenConfiguration());
            //modelBuilder.ApplyConfiguration(new BlockchainConfiguration());
            //modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
            //modelBuilder.ApplyConfiguration(new MasterWalletConfiguration());
            //modelBuilder.ApplyConfiguration(new RoleRightsConfiguration());
            //modelBuilder.ApplyConfiguration(new RolesConfiguration());
            //modelBuilder.ApplyConfiguration(new AccountConfiguration());
            //modelBuilder.ApplyConfiguration(new ScreenConfiguration());
            //modelBuilder.ApplyConfiguration(new RightsConfiguration());
            //modelBuilder.ApplyConfiguration(new AccountRoleConfiguration());
            //modelBuilder.ApplyConfiguration(new RefferalBonusConfiguration());
            modelBuilder.ApplyConfiguration(new RateConfiguration());
        }
    }
}