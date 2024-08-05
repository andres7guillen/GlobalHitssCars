using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsServiceDomain.Entities
{
    public class SparePart
    {
        public Guid Id { get; set; }
        [StringLength(100)]
        public string SpareName { get; set; } = string.Empty;
        [StringLength(100)]
        public string BrandSpare { get; set; } = string.Empty;
        [StringLength(50)]
        public string BrandCar { get; set; } = string.Empty;
        public int ModelCar { get; set; }
        [StringLength(100)]
        public string ReferenceCar { get; set; } = string.Empty;
        public bool IsInStock { get; set; }
        public int Stock { get; set; }
    }
}
