using Context.EntityConfigurations.Extension;
using DTO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Context.EntityConfigurations
{
    internal class RoleRightsConfiguration : IEntityTypeConfiguration<RoleRights>
    {
        public void Configure(EntityTypeBuilder<RoleRights> builder)
        {
            builder.ConfigureBase();

            builder.HasOne(x => x.Screen)
                .WithMany(x => x.RoleRights)
                .HasForeignKey(x => x.ScreenId);

            builder.HasOne(x => x.Role)
                .WithMany(x => x.RoleRights)
                .HasForeignKey(x => x.RoleId);

        }
    }
}