using ECommerce.Data.IRepositories;
using ECommerce.Data.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
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
            var payment = await paymentRepository.SelectAsync(x => x.OrderId == order.PaymentId);

            // to'liq narxini hisoblash
            foreach (var item in order.Items)
            {
                order.TotalAmount += item.Price;
            }

            // to'lov qilingan bo'lsa pending qilib qo'yamiz
            if  (payment is not null)
            {
                order.OrderStatus = OrderStatus.Pending;
                order.PaymentId = payment.Id;
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

        public async Task<Response<List<Order>>> GetAllAsync(Predicate<Order> predicate = null)
        {
            List<Order> result = await orderRepository.SelectAllAsync(x => predicate(x));

            return new Response<List<Order>>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = result
            };
        }

        public async Task<Response<Order>> GetAsync(long id)
        {
            var result = await orderRepository.SelectAsync(x => x.Id == id);

            if (result is null)
            {
                return new Response<Order>();
            }

            return new Response<Order>
            {
                StatusCode = 200,
                Message = "Success",
                Result = result
            };
        }

        public async Task<Response<Order>> UpdateAsync(long id, Order order)
        {
            var order1 = await orderRepository.SelectAsync(x => x.Id == id);

            if (order1 is null)
            {
                return new Response<Order>();
            }

            await orderRepository.UpdateAsync(order);

            return new Response<Order>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = order1
            };
        }
    }
}
