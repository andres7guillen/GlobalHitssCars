using Microsoft.EntityFrameworkCore;
using SparePartsServiceData.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsService.Tests.Config
{
    public static class ApplicationSparePartDbContextInMemory
    {
        public static ApplicationSparePartDbContext Get()
        {
            var options = new DbContextOptionsBuilder<ApplicationSparePartDbContext>()
                .UseInMemoryDatabase(databaseName: "GlobalHitssSparePartDb")
                .Options;
            return new ApplicationSparePartDbContext(options);
        }
    }
}
