﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTrain.Domain.Entity;

namespace WebApiTrain.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x=>x.Id).ValueGeneratedOnAdd();
            builder.Property(x=>x.Login).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Password).IsRequired();

            builder.HasMany<Report>(x => x.Reports)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .HasPrincipalKey(x => x.Id);

            throw new NotImplementedException();
        }
    }
}
