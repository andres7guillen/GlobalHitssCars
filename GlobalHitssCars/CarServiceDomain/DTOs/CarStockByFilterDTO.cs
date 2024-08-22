using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServiceDomain.DTOs
{
    public class CarStockByFilterDTO
    {
        public Guid BrandId { get; set; }
        public Guid ReferenceId { get; set; }
        public string Colour { get; set; } = string.Empty;
        public byte? Model { get; set; }
    }
}
