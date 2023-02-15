using ECommerce.Domain.Entities;
using ECommerce.Service.Helpers;

namespace ECommerce.Service.Interfaces
{
    public interface IChatService
    {
        Task<Response<ChatInfo>> SendMessageAsync(ChatInfo message);
        Task<Response<bool>> DeleteMessageAsync(long id);
        Task<Response<ChatInfo>> UpdateMessageAsync(long id, string message);
    }
}
