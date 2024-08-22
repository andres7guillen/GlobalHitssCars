using Microsoft.EntityFrameworkCore;
using PurchaseServiceDomain.Entities;
using PurchaseServiceDomain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseServiceData.Context
{
    public class ApplicationPurchaseDbContext : DbContext
    {
        public ApplicationPurchaseDbContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Purchase>().HasKey(p => p.Id);
            modelBuilder.Entity<Purchase>()
            .Property(p => p.TypePurchase)
            .HasConversion(
                v => v.ToString(),
                v => (TypePurchaseEnum)Enum.Parse(typeof(TypePurchaseEnum), v)
            )
            .HasMaxLength(10);
            base.OnModelCreating(modelBuilder);
        }


    }
}
