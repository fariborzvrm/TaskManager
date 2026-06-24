using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MyTestApp
{
    internal class AppDbContext : DbContext
    {
        public DbSet<TaskItem> TaskItems { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }
    }
}
