using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiriNews.Data
{
    class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MiriContext>
    {
        public MiriContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MiriContext>();
            var connectionString = "Server=MASTER;Database=MiriNewsDb;Trusted_Connection=True;MultipleActiveResultSets=true";
            builder.UseSqlServer(connectionString);
            return new MiriContext(builder.Options);
        }
    }
}
