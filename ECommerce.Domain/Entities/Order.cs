using ECommerce.Domain.Commons;

namespace ECommerce.Domain.Entities
{
    public class Order : Auditable
    {
        public List<OrderItem> Items { get; set; }
    }
}
