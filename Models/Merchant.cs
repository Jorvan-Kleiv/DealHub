namespace DealHub.Models
{
    public class Merchant
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string WebsiteUrl { get; set; } = string.Empty;
        public string LogUrl { get; set; } = string.Empty;
        public bool IsVerified { get; set; } = false;

        public List<Deal> Deals { get; set; } = new();
    }
}
