using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServiceDomain.Entities
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(50)]
        public string Brand { get; set; } = string.Empty;
        public int Model { get; set; }

        [StringLength(100)]
        public string Reference { get; set; } = string.Empty;

        [StringLength(50)]
        public string Colour { get; set; } = string.Empty;
        [StringLength(10)]
        public string LicensePlate { get; set; } = string.Empty;
    }
}
