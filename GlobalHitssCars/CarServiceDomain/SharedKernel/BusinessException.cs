using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServiceDomain.SharedKernel
{
    public abstract class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
        public virtual int Code { get; set; }
    }
}
