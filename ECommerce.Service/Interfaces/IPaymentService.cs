using ECommerce.Domain.Entities;
using ECommerce.Service.Helpers;

namespace ECommerce.Service.Interfaces
{
    public interface IPaymentService
    {
        public Task<Response<Payment>> AddAsync(Product product);
        public Task<Response<Payment>> UpdateAsync(long id, Product product);
        public Task<Response<Payment>> GetByIdAsync(long id);
        public Task<Response<Payment>> GetByNameAsync(string name);
        public Task<Response<List<Payment>>> GetAllAsync();
    }
}
