using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsServiceDomain.DTOs
{
    public class SparePartByFilter
    {
        public string BrandSpare { get; set; } = string.Empty;
        public string BrandCar { get; set; } = string.Empty;
    }
}
