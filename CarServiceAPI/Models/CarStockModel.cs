using System.ComponentModel.DataAnnotations;

namespace CarServiceAPI.Models
{
    public class CarStockModel
    {
        public string? Id { get; set; } = string.Empty;
        public string BrandId { get; set; } = string.Empty;
        public int Model { get; set; } = 0;

        [StringLength(50)]
        public string Colour { get; set; } = string.Empty;
        public string ReferenceId { get; set; } = string.Empty;
    }
}
