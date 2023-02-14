using ECommerce.Data.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Service.Helpers;
using ECommerce.Service.Interfaces;

namespace ECommerce.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly Repository<Product> genericRepository = new GenericRepository<Product>();
        Task<Response<Product>> IProductService.AddAsync(Product product)
        {
            throw new NotImplementedException();
        }

        Task<Response<List<Product>>> IProductService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<Response<Product>> IProductService.GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        Task<Response<Product>> IProductService.GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        Task<Response<Product>> IProductService.UpdateAsync(long id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
