using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiriNews.Core.Entity;

namespace MiriNews.Data.Configurations
{
    class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.CategoryName).IsRequired().HasMaxLength(50);
            builder.HasMany(x => x.Posts).WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId);
            builder.ToTable("Categories");
        }
    }
}
