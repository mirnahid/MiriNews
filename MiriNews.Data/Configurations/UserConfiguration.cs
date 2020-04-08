using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiriNews.Core.Entity.IdentityCore;
using System;

namespace MiriNews.Data.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.DateOfBrith).IsRequired();
            builder.HasMany(x => x.AddedPosts).WithOne(x => x.AddUser)
                .HasForeignKey(x => x.AddedUserId);
            builder.HasMany(x => x.UpdatedPost).WithOne(x => x.UpdateUser)
                .HasForeignKey(x => x.UpdateUserId);

        }
    }
}
