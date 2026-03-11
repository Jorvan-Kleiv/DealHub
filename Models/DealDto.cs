namespace DealHub.Models
{
    public class DealDto
    {
        public IEnumerable<Deal> Deals { get; set; }
        public IEnumerable<Merchant> Merchants { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
