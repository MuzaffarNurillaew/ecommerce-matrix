using ECommerce.Domain.Entities;
using ECommerce.Service.Helpers;

namespace ECommerce.Service.Interfaces
{
    public interface IOrderService
    {
        Task<Response<Order>> AddAsync(Order order);
        Task<Response<Order>> UpdateAsync(long id, Order order);
        Task<Response<bool>> DeleteAsync(long id);
        Task<Response<Order>> GetAsync(long id);
        Task<Response<List<Order>>> GetAllAsync(Predicate<Order> predicate = null);
    }
}
