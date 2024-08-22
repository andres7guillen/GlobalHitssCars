using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsServiceDomain.SharedKernel
{
    public class Event<T> : IDomainEvent
    {
        public Header Header { get; set; }
        public T Body { get; set; }
    }
}
