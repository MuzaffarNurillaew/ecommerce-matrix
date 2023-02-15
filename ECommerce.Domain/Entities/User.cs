using ECommerce.Domain.Commons;

namespace ECommerce.Domain.Entities
{
    public class User : Auditable
    {
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public decimal AvailableMoney { get; set; }
    }
}