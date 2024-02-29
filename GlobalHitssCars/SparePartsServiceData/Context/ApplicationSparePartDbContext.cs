using Microsoft.EntityFrameworkCore;
using SparePartsServiceDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsServiceData.Context
{
    public class ApplicationSparePartDbContext : DbContext
    {
        public ApplicationSparePartDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<SparePart> SpareParts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SparePart>()
                .HasKey(s => s.Id);
            base.OnModelCreating(modelBuilder);
        }

    }
}
