namespace PurchaseServiceAPI.Models
{
    public class PurchaseModel
    {
        public string Id { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string CarId { get; set; } = string.Empty;
        public double Amount { get; set; }
    }
}
