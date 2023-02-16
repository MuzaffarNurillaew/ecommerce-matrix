using ECommerce.Data.IRepositories;
using ECommerce.Data.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Service.Helpers;
using ECommerce.Service.Interfaces;
using Newtonsoft.Json.Linq;

namespace ECommerce.Service.Services
{
    public class RecommendService : IRecommendService
    {
        private readonly IRepository<Product> productRepository = new Repository<Product>();
        private readonly IRepository<Order> orderRepository = new Repository<Order>();
        public async Task<Response<List<Product>>> RecommendBasedOn(long id)
        {
            var values = await orderRepository.SelectAllAsync();
            var user = values.FirstOrDefault(p=>p.UserId == id);            
            List<OrderItem> orderHistory = user.Items;
            List<long> purchasedProducts = new List<long>();
            foreach (OrderItem order in orderHistory)
            {
                purchasedProducts.Add(order.ProductId);
            }            
            List<Product> allProducts = await productRepository.SelectAllAsync();
            
            List<Product> recommendedProducts = new List<Product>();
            foreach (Product product in allProducts)
            {
                if (!purchasedProducts.Contains(product.Id))
                {
                    recommendedProducts.Add(product);
                }
            }            

            Random random = new Random();
            for (int i = 0; i < recommendedProducts.Count; i++)
            {
                int randomIndex = random.Next(recommendedProducts.Count);
                Product temp = recommendedProducts[i];
                recommendedProducts[i] = recommendedProducts[randomIndex];
                recommendedProducts[randomIndex] = temp;
            }

            // Return the first n recommended products
            int numRecommendations = 5; // for example
            List<Product> randomRecommendations = recommendedProducts.Take(numRecommendations).ToList();
            return new Response<List<Product>>()
            {
                Result= randomRecommendations
            };
        }

        public async Task<Response<List<Product>>> RecommendWithoutInfo()
        {
            var values = await productRepository.SelectAllAsync();
            Random rnd = new Random();
            List<Product> products = new List<Product>();
            int count = 0;
            // bu yerda men 10 ta random product oladi 100 ta rnadom product ichidan)
            while (true)
            {
                var random = rnd.Next(1,100);
                var value = values.FirstOrDefault(p => p.Id == random);
                if (value is not null)
                {
                    products.Add(value);
                    count++;
                }
                if (count == 10) 
                {
                    break;
                }
            }
            return new Response<List<Product>>()
            {
                Result = products
            };
        }
    }
}
