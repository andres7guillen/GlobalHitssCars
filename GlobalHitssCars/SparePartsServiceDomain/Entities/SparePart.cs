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
        public string SpareName { get; set; }
        public string BrandSpare { get; set; }
        public string BrandCar { get; set; }
        public bool IsInStock { get; set; }
    }
}
