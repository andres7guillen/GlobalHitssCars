using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsServiceDomain.Entities
{
    public class SparePart
    {
        public Guid Id { get; set; }
        public string SpareName { get; set; } = string.Empty;
        public string BrandSpare { get; set; } = string.Empty;
        public string BrandCar { get; set; } = string.Empty;
        public string ModelCar { get; set; }
        public bool IsInStock { get; set; }
        public int Stock { get; set; }
    }
}
