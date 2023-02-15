using ECommerce.Domain.Commons;

namespace ECommerce.Domain.Entities
{
    public class Product : Auditable
    {
        public long CategoryId { get; set;}
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool CanDeliver { get; set; }
        public string QRCode { get; set; }
        public string Description { get; set; }
        
    }
}
