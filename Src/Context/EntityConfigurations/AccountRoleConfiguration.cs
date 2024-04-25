using Context.EntityConfigurations.Extension;
using DTO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.EntityConfigurations
{
    internal class AccountRoleConfiguration : IEntityTypeConfiguration<AccountRole>
    {
        public void Configure(EntityTypeBuilder<AccountRole> builder)
        {
            builder.ConfigureBase();

            builder.HasOne(x => x.Roles)
                .WithMany(x => x.AccountRoles)
                .HasForeignKey(x => x.RoleId);

            builder.HasOne(x => x.Account)
                .WithMany(x => x.AccountRoles)
                .HasForeignKey(x => x.AccountId);
        }
    }
}