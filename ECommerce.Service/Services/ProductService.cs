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
            var value = await genericRepository.SelectAsync(p => 
            p.Name.ToLower() == product.Name.ToLower()
            && p.QRCode.ToLower() == product.QRCode.ToLower());
            
            if (value is not null)
            {
                return new Response<Product>()
                {
                    Message = "Bu mahsulot allaqachon mavjud",
                    Result = value
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

        public async Task<Response<bool>> DeleteByIdAsync(long id, long ownerId)
        {
            var value = await genericRepository.SelectAsync(p=> p.Id == id);
            
            if (value is null)
            {
                return new Response<bool>()
                {
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

        public async Task<Response<List<Product>>> GetAllAsync(Predicate<Product> predicate)
        {
            var result = await genericRepository.SelectAllAsync(x => predicate(x));
            return new Response<List<Product>>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = result
            };
        }

        public async Task<Response<Product>> GetByIdAsync(long id)
        {
            var values = await genericRepository.SelectAllAsync();
            var value = values.FirstOrDefault(v =>v.Id == id);
            
            if(value is null)
            {
                return new Response<Product>();
            }

            return new Response<Product>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = value
            };
        }
        public async Task<Response<Product>> GetAsync(Predicate<Product> predicate)
        {
            var value = await genericRepository.SelectAsync(x => predicate(x));

            if (value is null)
            {
                return new Response<Product>();
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
            var value = values.FirstOrDefault(p => p.Name.ToLower().Contains(name.ToLower()));
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
                return new Response<Product>();
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
