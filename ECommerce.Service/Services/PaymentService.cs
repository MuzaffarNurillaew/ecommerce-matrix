using ECommerce.Data.IRepositories;
using ECommerce.Data.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Service.Helpers;
using ECommerce.Service.Interfaces;

namespace ECommerce.Service.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Payment> paymentRepository = new Repository<Payment>();
        private readonly IRepository<Order> orderRepository = new Repository<Order>();
        public async Task<Response<Payment>> AddAsync(Payment payment)
        {
            var order = await orderRepository.SelectAsync(x => x.PaymentId == payment.Id || x.Id == payment.OrderId);

            if (order is null)
            {
                return new Response<Payment>()
                {
                    Message = "Order is not found."
                };
            }
            else if (order.TotalAmount > payment.Amount)
            {
                return new Response<Payment>()
                {
                    Message = "Not Enough money to pay."
                };
            }
            else
            {
                var result = await paymentRepository.CreateAsync(payment);

                return new Response<Payment>()
                {
                    StatusCode = 200,
                    Message = "Success",
                    Result = result
                };
            }

        }

        public async Task<Response<List<Payment>>> GetAllAsync(Predicate<Payment> predicate = null)
        {
            var result = await paymentRepository.SelectAllAsync(x => predicate(x));

            return new Response<List<Payment>>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = result
            };
        }

        public async Task<Response<Payment>> GetByIdAsync(long id)
        {
            var result = await paymentRepository.SelectAsync(x => x.Id == id);

            if (result is null)
            {
                return new Response<Payment>();
            }

            return new Response<Payment>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = result
            };
        }


        public async Task<Response<Payment>> UpdateAsync(long id, Payment payment)
        {
            var oldPayment = await paymentRepository.SelectAsync(x => x.Id == id);

            if (oldPayment is null)
            {
                return new Response<Payment>()
                {
                    Message = "Payment not found"
                };
            }

            await paymentRepository.UpdateAsync(payment);

            return new Response<Payment>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = oldPayment
            };
        }
    }
}
