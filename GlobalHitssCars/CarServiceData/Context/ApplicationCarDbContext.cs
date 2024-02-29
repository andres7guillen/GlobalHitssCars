using CarServiceDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServiceData.Context
{
    public class ApplicationCarDbContext : DbContext
    {
        public ApplicationCarDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasKey(c => c.Id);
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Car> Cars { get; set; }


    }
}
