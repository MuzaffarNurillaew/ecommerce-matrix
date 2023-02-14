using ECommerce.Domain.Entities;
using ECommerce.Service.Helpers;
using ECommerce.Service.Interfaces;

namespace ECommerce.Service.Services
{
    public class OrderService : IOrderService
    {
        public Response<Task<Order>> AddAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Response<bool> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Response<List<Order>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Response<Order> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Response<Order> UpdateAsync(long id, Order order)
        {
            throw new NotImplementedException();
        }
    }
}
