using CarServiceData.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Tests.Config
{
    public static class ApplicationCarDbContextInMemory
    {
        public static ApplicationCarDbContext Get() 
        { 
            var options = new DbContextOptionsBuilder<ApplicationCarDbContext>()
                .UseInMemoryDatabase(databaseName: "GlobalHitssCarDb")
                .Options;
            return new ApplicationCarDbContext(options);
        }
    }
}
