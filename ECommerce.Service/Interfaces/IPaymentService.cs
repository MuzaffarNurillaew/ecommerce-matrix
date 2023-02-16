using ECommerce.Domain.Entities;
using ECommerce.Service.Helpers;

namespace ECommerce.Service.Interfaces
{
    public interface IPaymentService
    {
        public Task<Response<Payment>> AddAsync(Payment payment);
        public Task<Response<Payment>> UpdateAsync(long id, Payment payment);
        public Task<Response<Payment>> GetByIdAsync(long? id);
        public Task<Response<List<Payment>>> GetAllAsync(Predicate<Payment> predicate = null);
    }
}
