namespace DealHub.Models
{
    public class DealRequest
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Merchant> Merchants { get; set; } = new List<Merchant>();
        public Deal Deal { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
