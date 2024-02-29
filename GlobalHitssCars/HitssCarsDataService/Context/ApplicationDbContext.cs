using HitssCarsDataServiceDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitssCarsDataService.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Client>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<PurchaseCarClient>()
        .HasKey(pcc => new { pcc.PurchaseId, pcc.CarId, pcc.ClientId });

            modelBuilder.Entity<PurchaseCarClient>()
        .HasOne(pcc => pcc.Purchase)
        .WithMany(p => p.PurchaseCarClients)
        .HasForeignKey(pcc => pcc.PurchaseId);

            modelBuilder.Entity<PurchaseCarClient>()
        .HasOne(pcc => pcc.Car)
        .WithMany()
        .HasForeignKey(pcc => pcc.CarId);

            modelBuilder.Entity<PurchaseCarClient>()
        .HasOne(pcc => pcc.Client)
        .WithMany()
        .HasForeignKey(pcc => pcc.ClientId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
