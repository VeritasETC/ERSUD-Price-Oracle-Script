using Context.EntityConfigurations.Extension;
using DTO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Context.EntityConfigurations
{
    internal class AccountConfirmationConfiguration : IEntityTypeConfiguration<AccountConfirmation>
    {
        public void Configure(EntityTypeBuilder<AccountConfirmation> builder)
        {
            builder.ConfigureBase();


            builder.HasOne(x => x.Account)
                .WithMany(x => x.AccountConfirmation)
                .HasForeignKey(x => x.AccountId);
        }
    }
}
