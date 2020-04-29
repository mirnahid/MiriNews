using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiriNews.Core.Entity.IdentityCore;

namespace MiriNews.Data.Configurations
{
    class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            //builder.HasMany(x => x.UserRoles)
            //    .WithOne(x => x.Role)
            //    .HasForeignKey(x => x.RoleId);
        }
    }
}
