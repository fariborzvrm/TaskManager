using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyTestApp
{
    internal class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB;Database=TaskManagerDB;Trusted_Connection=True;"
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
