using Microsoft.EntityFrameworkCore;
using PurchaseServiceData.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseService.Tests.Config
{
    public static class ApplicationPurchaseDbContextInMemory
    {
        public static ApplicationPurchaseDbContext Get()
        {
            var options = new DbContextOptionsBuilder<ApplicationPurchaseDbContext>()
                .UseInMemoryDatabase(databaseName: "GlobalHitssPurchaseDb")
                .Options;
            return new ApplicationPurchaseDbContext(options);
        }
    }
}
