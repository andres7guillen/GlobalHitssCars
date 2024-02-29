using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitssCarsDataServiceDomain.DTOs
{
    internal class CarByFilterDTO
    {
        public string Brand { get; set; } = string.Empty;
        public string Colour { get; set; } = string.Empty;
        public byte? Model { get; set; }
    }
}
