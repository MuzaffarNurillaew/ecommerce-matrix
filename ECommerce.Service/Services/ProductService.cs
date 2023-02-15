using ECommerce.Data.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Service.Helpers;
using ECommerce.Service.Interfaces;

namespace ECommerce.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly Repository<Product> genericRepository = new Repository<Product>();
        public async Task<Response<Product>> AddAsync(Product product)
        {
            var values = await genericRepository.SelectAllAsync(p => p.Name == product.Name);
            if (values is not null)
            {
                return new Response<Product>()
                {
                    StatusCode = 404,
                    Message = "Not Found",
                    Result = null
                };
            }
            await genericRepository.CreateAsync(product);
            return new Response<Product>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = product
            };
        }

        public async Task<Response<bool>> DeleteByIdAsync(long id)
        {
            var values = await genericRepository.SelectAllAsync(p=>p.Id == id);
            if (values is null)
            {
                return new Response<bool>()
                {
                    StatusCode = 404,
                    Message = "Not Found",
                    Result = false
                };
            }
            await genericRepository.DeleteAsync(v => v.Id == id);
            return new Response<bool>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = true
            };
        }

        public async Task<Response<List<Product>>> GetAllAsync()
        {
            return new Response<List<Product>>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = await genericRepository.SelectAllAsync()
            };
        }

        async Task<Response<Product>> IProductService.GetByIdAsync(long id)
        {
            var values = await genericRepository.SelectAllAsync();
            var value = values.FirstOrDefault(v =>v.Id == id);
            if(value is null)
            {
                return new Response<Product>()
                {
                    StatusCode = 404,
                    Message = "Not Found",
                    Result = null
                };
            }
            return new Response<Product>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = value
            };
        }

        public async Task<Response<Product>> GetByNameAsync(string name)
        {
            var values = await genericRepository.SelectAllAsync();
            var value = values.FirstOrDefault(p => p.Name == name);
            if (value is null)
            {
                return new Response<Product>()
                {
                    StatusCode = 404,
                    Message = "Not Found",
                    Result = null
                };
            }            
            return new Response<Product>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = value
            };
        }

        public async Task<Response<Product>> UpdateAsync(long id, Product product)
        {
            var value = await genericRepository.SelectAsync(p=>p.Id == id);
            if (value is null)
            {
                return new Response<Product>()
                {
                    StatusCode = 404,
                    Message = "Not Found",
                    Result = null
                };
            }
            await genericRepository.UpdateAsync(product);
            return new Response<Product>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = product
            };
        }
    }
}
