using ECommerce.Domain.Entities;
using ECommerce.Service.Helpers;
using ECommerce.Service.Interfaces;

namespace ECommerce.Service.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<Response<Payment>> AddAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<Payment>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<Payment>> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Payment>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Payment>> UpdateAsync(long id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
