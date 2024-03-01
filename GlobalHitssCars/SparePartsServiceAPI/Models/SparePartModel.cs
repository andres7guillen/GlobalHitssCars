namespace SparePartsServiceAPI.Models
{
    public class SparePartModel
    {
        public string Id { get; set; } = string.Empty;
        public string SpareName { get; set; } = string.Empty;
        public string BrandSpare { get; set; } = string.Empty;
        public string BrandCar { get; set; } = string.Empty;
        public bool IsInStock { get; set; }
    }
}
