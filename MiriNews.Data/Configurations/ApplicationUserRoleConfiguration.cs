﻿using Microsoft.EntityFrameworkCore;
using MiriNews.Core.Entity.IdentityCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MiriNews.Data.Configurations
{
    class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
        {
            //builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            //builder.HasOne(ur => ur.Role)
            //    .WithMany(r => r.UserRoles)
            //    .HasForeignKey(ur => ur.RoleId)
            //    .IsRequired();

            //builder.HasOne(ur => ur.User)
            //    .WithMany(u => u.UserRoles)
            //    .HasForeignKey(ur => ur.UserId)
            //    .IsRequired();
        }
    }
}
