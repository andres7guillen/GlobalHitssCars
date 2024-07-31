namespace CarServiceAPI.Models
{
    public class CarByFilterModel
    {
        public string Brand { get; set; } = string.Empty;
        public string Colour { get; set; } = string.Empty;
        public byte? Model { get; set; }
    }
}
