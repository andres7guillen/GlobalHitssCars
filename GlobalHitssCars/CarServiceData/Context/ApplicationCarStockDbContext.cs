using CarServiceDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServiceData.Context
{
    public class ApplicationCarStockDbContext : DbContext
    {
        public ApplicationCarStockDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarStock>().HasKey(c => c.Id);
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<CarStock> Cars { get; set; }


    }
}
