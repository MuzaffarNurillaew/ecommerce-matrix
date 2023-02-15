using ECommerce.Domain.Commons;

namespace ECommerce.Domain.Entities
{
    public class ChatInfo : Auditable
    {
        public long SenderId { get; set; }
        public long RespondentId { get; set; }
        public string Message { get; set; }
    }
}
