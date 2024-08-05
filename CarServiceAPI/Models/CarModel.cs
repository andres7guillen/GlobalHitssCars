using System.ComponentModel.DataAnnotations;

namespace CarServiceAPI.Models
{
    public class CarModel
    {
        public string? Id { get; set; } = string.Empty;

        [StringLength(50)]
        public string Brand { get; set; } = string.Empty;
        public int Model { get; set; } = 0;

        [StringLength(50)]
        public string Colour { get; set; } = string.Empty;

        [StringLength(10)]
        public string LicensePlate { get; set; } = string.Empty;
        [StringLength(100)]
        public string Reference { get; set; } = string.Empty;
    }
}
