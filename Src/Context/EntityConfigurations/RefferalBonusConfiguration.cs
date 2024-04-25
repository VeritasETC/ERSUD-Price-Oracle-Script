﻿using Context.EntityConfigurations.Extension;
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
    internal class RefferalBonusConfiguration : IEntityTypeConfiguration<RefferalBonus>
    {
        public void Configure(EntityTypeBuilder<RefferalBonus> builder)
        {
            builder.ConfigureBase();


            builder.HasOne(x => x.FromAccount)
                    .WithMany()
                    .HasForeignKey(x => x.FormAccountId);

            builder.HasOne(x => x.ToAccount)
                    .WithMany()
                    .HasForeignKey(x => x.ToAccountId);
        }
    }
}
