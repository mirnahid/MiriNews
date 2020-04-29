using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiriNews.Core.Entity;
using MiriNews.Core.Entity.IdentityCore;
using MiriNews.Data.Configurations;
using MiriNews.Data.Seeds;

namespace MiriNews.Data
{
    public class MiriContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {
        public MiriContext(DbContextOptions<MiriContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           // builder.ApplyConfiguration(new ApplicationUserRoleConfiguration());

            builder.ApplyConfiguration(new UserConfiguration());

            //builder.ApplyConfiguration(new RoleConfiguration());

            builder.ApplyConfiguration(new PostConfiguration());

            builder.ApplyConfiguration(new CategoryConfiguration());

            builder.ApplyConfiguration(new TagConfiguration());

            builder.ApplyConfiguration(new CategorySeed(new int[] {1,2 }));

            base.OnModelCreating(builder);
        }
    }
}