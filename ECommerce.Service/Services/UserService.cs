using ECommerce.Domain.Entities;
using ECommerce.Service.Helpers;
using ECommerce.Service.Interfaces;

namespace ECommerce.Service.Services
{
    internal class UserService : IUserService
    {
        public Task<Response<User>> CreateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<User>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<User>> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<User>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Response<User>> UpdateAsync(long id, User user)
        {
            throw new NotImplementedException();
        }
    }
}
