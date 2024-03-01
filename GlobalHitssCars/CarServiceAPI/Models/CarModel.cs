namespace CarServiceAPI.Models
{
    public class CarModel
    {
        public string Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public byte Model { get; set; } = 0;
        public string Colour { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
    }
}
