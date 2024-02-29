using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitssCarsDataServiceDomain.Entities
{
    public class PurchaseCarClient
    {
        public Guid PurchaseId { get; set; }
        public Purchase Purchase { get; set; }

        public Guid CarId { get; set; }
        public Car Car { get; set; }

        public Guid ClientId { get; set; }
        public Client Client { get; set; }
    }
}
