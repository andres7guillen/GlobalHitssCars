using System.ComponentModel.DataAnnotations;

namespace PurchaseServiceAPI.Models
{
    public class PurchaseModel
    {
        public string Id { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string CarId { get; set; } = string.Empty;
        public string SpareId { get; set; }
        public int Quantity { get; set; }
        public double Amount { get; set; }
        [Required]
        public string TypePurchase { get; set; }
    }
}
