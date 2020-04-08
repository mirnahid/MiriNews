using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiriNews.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiriNews.Data.Configurations
{
    class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.CoverPhoto).IsRequired().HasMaxLength(50);
            builder.Property(x => x.EndDate).IsRequired();
            builder.HasMany(x => x.Tags).WithOne(x => x.Post)
                .HasForeignKey(x => x.PostId);
            builder.ToTable("Posts");
        }
    }
}
