using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitssCarsDataServiceDomain.Entities
{
    internal class Car
    {
        [Key]
        public Guid Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public byte Model { get; set; }
        public string Colour { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
    }
}
