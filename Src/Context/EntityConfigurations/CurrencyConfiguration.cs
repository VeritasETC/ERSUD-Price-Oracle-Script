using Context.EntityConfigurations.Extension;
using DTO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Context.EntityConfigurations
{
    internal class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ConfigureBase();

            builder.HasOne(x => x.Blockchain)
                .WithMany(f => f.Currencies)
                .HasForeignKey(x => x.BlockchainId);

            builder.Property(p => p.RateBTC).HasColumnType("decimal(32,18)");
            builder.Property(p => p.RateEUR).HasColumnType("decimal(32,18)");
            builder.Property(p => p.RateGBP).HasColumnType("decimal(32,18)");
            builder.Property(p => p.RateInUSD).HasColumnType("decimal(32,18)");
        }
    }
}