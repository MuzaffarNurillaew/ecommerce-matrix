using ECommerce.Domain.Entities;
using ECommerce.Service.Helpers;

namespace ECommerce.Service.Interfaces
{
    public interface IRecommendService
    {
        Task<Response<List<Product>>> RecommendWithoutInfo();
        Task<Response<List<Product>>> RecommendBasedOn(long id);
    }
}
