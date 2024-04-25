using DTO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Context.EntityConfigurations.Extension
{
    public static class CommonConfigurationExtension
    {
        public static void ConfigureBase<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : CommonDbProp
        {
            builder.HasKey(h => h.Id);
            builder.Property(p => p.Id)
                .IsRequired();
            builder.HasIndex(h => h.Id)
                .IsUnique();
            builder.Property(p => p.Uuid)
               .IsRequired()
               .HasDefaultValueSql("NEWID()");
            builder.HasIndex(h => h.Uuid)
                .IsUnique();
            builder.Property(p => p.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();
            builder.Property(p => p.CreatedAt)
                .IsRequired();

        }
    }
}
