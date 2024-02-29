using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseServiceDomain.Entities
{
    public class Purchase
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid CarId { get; set; }
    }
}
