using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiriNews.Core.Entity;
using MiriNews.Core.Entity.IdentityCore;

namespace MiriNews.Data.Configurations
{
    class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Description)
                .IsRequired();

            builder.Property(x => x.CoverPhoto)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.EndDate)
                .IsRequired();

            builder.HasMany<Tag>(x => x.Tags)
                .WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId);

            builder.HasOne<ApplicationUser>(x => x.AddUser)
                .WithMany(j => j.AddedPosts)
                .HasForeignKey(k => k.AddedUserId);
            
            builder.HasOne<ApplicationUser>(x => x.UpdateUser)
                .WithMany(j => j.UpdatedPost)
                .HasForeignKey(k => k.UpdateUserId);

            builder.HasOne<Category>(x => x.Category)
                .WithMany(j => j.Posts)
                .HasForeignKey(k=>k.CategoryId);

            builder.ToTable("Posts");
        }
    }
}
