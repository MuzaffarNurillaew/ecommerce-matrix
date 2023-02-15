namespace ECommerce.Domain.Enums
{
    public enum OrderStatus
    {
        Unpaid = 0,
        Pending = 10, 
        Packing = 20,
        Shipping = 30,
        Shipped =40
    }
}
