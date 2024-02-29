using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitssCarsDataServiceDomain.Entities
{
    public class Purchase
    {
        public Guid PurchaseId { get; set; }
        public IEnumerable<PurchaseCarClient> PurchaseCarClients { get; set; } = Enumerable.Empty<PurchaseCarClient>();
    }
}
