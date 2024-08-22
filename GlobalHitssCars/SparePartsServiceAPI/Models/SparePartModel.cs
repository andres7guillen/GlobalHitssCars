using System.ComponentModel.DataAnnotations;

namespace SparePartsServiceAPI.Models
{
    public class SparePartModel
    {
        public string? Id { get; set; }

        [StringLength(100)]
        public string SpareName { get; set; } 

        [StringLength(100)]
        public string BrandSpare { get; set; } 

        [StringLength(50)]
        public string BrandCar { get; set; } 
        public int ModelCar { get; set; }

        [StringLength(100)]
        public string ReferenceCar { get; set; } 
        public bool IsInStock { get; set; }
        public int Stock { get; set; }
    }
}
