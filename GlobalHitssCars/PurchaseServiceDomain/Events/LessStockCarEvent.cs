using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseServiceDomain.Events
{
    public class LessStockCarEvent
    {
        public Guid CarId { get; set; }
        public int Quantity { get; set; }

    }
}
