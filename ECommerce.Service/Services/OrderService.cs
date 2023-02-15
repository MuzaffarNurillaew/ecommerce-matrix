using ECommerce.Data.IRepositories;
using ECommerce.Data.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Service.Helpers;
using ECommerce.Service.Interfaces;

namespace ECommerce.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> orderRepository = new Repository<Order>();
        private readonly IRepository<Payment> paymentRepository = new Repository<Payment>();
        public async Task<Response<Order>> AddAsync(Order order)
        {
            var payment = await paymentRepository.SelectAsync(x => x.OrderId == order.Id);

            if (payment is null)
            {
                return new Response<Order>()
                {
                    Message = "Payment is not done."
                };
            }
            await orderRepository.CreateAsync(order);
            return new Response<Order>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = order
            };
        }

        public async Task<Response<bool>> DeleteAsync(long id)
        {
            var order = await orderRepository.SelectAsync(x => x.Id == id);

            if (order is null)
            {
                return new Response<bool>();
            }

            await paymentRepository.DeleteAsync(x => x.OrderId == id);
            await orderRepository.DeleteAsync(x => x.Id == id);

            return new Response<bool>()
            {
                StatusCode = 200,
                Message = "Success",
               Result = true
            };
        }

        public async Task<Response<List<Order>>> GetAllAsync(Predicate<Order>? predicate = null)
        {
            var result = await orderRepository.SelectAllAsync(x => predicate(x));

            return new Response<Order>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = result;
            }
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
