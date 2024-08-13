using ClientServiceData.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Tests.Config
{
    public static class ApplicationClientDbContextInMemory
    {
        public static ApplicationClientDbContext Get()
        {
            var options = new DbContextOptionsBuilder<ApplicationClientDbContext>()
                .UseInMemoryDatabase(databaseName: "GlobalHitssClientDb")
                .Options;
            return new ApplicationClientDbContext(options);
        }
    }
}
