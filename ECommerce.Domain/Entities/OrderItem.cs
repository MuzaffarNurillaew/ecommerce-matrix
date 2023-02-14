using ECommerce.Domain.Commons;

namespace ECommerce.Domain.Entities
{
    public class OrderItem : Auditable
    {
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
