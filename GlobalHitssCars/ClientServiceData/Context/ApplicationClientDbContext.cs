using ClientServiceDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServiceData.Context
{
    public class ApplicationClientDbContext : DbContext
    {
        public ApplicationClientDbContext(DbContextOptions options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasKey(c => c.Id);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Client> Clients { get; set; }
        

    }
}
