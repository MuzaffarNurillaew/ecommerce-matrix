using ECommerce.Domain.Entities;
using ECommerce.Service.Helpers;

namespace ECommerce.Service.Interfaces
{
    public interface IProductService
    {
        public Task<Response<Product>> AddAsync(Product product);
        public Task<Response<Product>> UpdateAsync(long id, Product product);
        public Task<Response<Product>> GetByIdAsync(long id);
        public Task<Response<bool>> DeleteByIdAsync(long id);
        public Task<Response<Product>> GetByNameAsync(string name);
        public Task<Response<List<Product>>> GetAllAsync(Predicate<Product> predicate = null);
    }
}
