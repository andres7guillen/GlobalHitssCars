using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsServiceDomain.Events
{
    public class AddStockSparePartEvent
    {
        public Guid SparePartId { get; set; }
        public int Quantity { get; set; }
    }
}
