using ECommerce.Domain.Entities;
using ECommerce.Service.Helpers;

namespace ECommerce.Service.Interfaces
{
    public interface IOrderService
    {
        Response<Task<Order>> AddAsync(Order order);
        Response<Order> UpdateAsync(long id, Order order);
        Response<bool> DeleteAsync(long id);
        Response<Order> GetAsync(long id);
        Response<List<Order>> GetAllAsync();
    }
}
