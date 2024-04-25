using Context.EntityConfigurations.Extension;
using DTO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Context.EntityConfigurations
{
    internal class MasterWalletConfiguration : IEntityTypeConfiguration<MasterWallet>
    {
        public void Configure(EntityTypeBuilder<MasterWallet> builder)
        {
            builder.ConfigureBase();

            builder.HasOne(x => x.Currency)
                .WithMany(f => f.MasterWallets)
                .HasForeignKey(x => x.CurrencyId);
            builder.Property(p => p.Balance).HasColumnType("decimal(32,18)");
        }
    }
}