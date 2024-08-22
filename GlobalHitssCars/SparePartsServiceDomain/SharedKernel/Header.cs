using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsServiceDomain.SharedKernel
{
    public class Header
    {
        public Guid AggregateId { get; set; }
        public string AggregateName { get; set; }
        public string EventType { get; set; }
        public string TenantId { get; set; }
        public DateTime EventDate { get; set; }
        public string MessageId { get; set; }
        public string UserId { get; set; }
    }
}
