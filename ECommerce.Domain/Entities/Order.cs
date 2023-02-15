using ECommerce.Domain.Commons;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities
{  
    public class Order : Auditable
    {        
        public long UserId { get; set; }
        public long? PaymentId { get; set; } = null;
        public List<OrderItem> Items { get; set; }
        public bool IsPaid { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Unpaid;
        public decimal TotalAmount { get; set; }
    }
}
