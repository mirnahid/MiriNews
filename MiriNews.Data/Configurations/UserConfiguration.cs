using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiriNews.Core.Entity;
using MiriNews.Core.Entity.IdentityCore;

namespace MiriNews.Data.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.DateOfBrith)
                .IsRequired();

            builder.HasMany<Post>(x => x.AddedPosts)
                .WithOne(x => x.AddUser)
                .HasForeignKey(x => x.AddedUserId);

            builder.HasMany<Post>(x => x.UpdatedPost)
                .WithOne(x => x.UpdateUser)
                .HasForeignKey(x => x.UpdateUserId);

            //builder.HasMany(x => x.UserRoles)
            //    .WithOne(x => x.User)
            //    .HasForeignKey(x => x.UserId);

        }
    }
}
