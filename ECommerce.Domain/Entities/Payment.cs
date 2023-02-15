using ECommerce.Domain.Commons;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities
{
    public class Payment : Auditable
    {
        public long OrderId { get; set; }
        //public long RecieverId { get; set; }
        public PaymentType PaymentType { get; set; }
        public decimal Amount { get; set; }
    }
}
