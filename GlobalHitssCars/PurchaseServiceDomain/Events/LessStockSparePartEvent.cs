using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseServiceDomain.Events
{
    public class LessStockSparePartEvent
    {
        public Guid SparePartId { get; set; }
        public int Quantity { get; set; }
    }
}
