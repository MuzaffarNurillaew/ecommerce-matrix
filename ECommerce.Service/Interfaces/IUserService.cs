using ECommerce.Domain.Entities;
using ECommerce.Service.Helpers;

namespace ECommerce.Service.Interfaces
{
    public interface IUserService
    {
        //  CRUD    methods

        // creating new product
        public Task<Response<User>> CreateAsync(User user);
        // changing product information
        public Task<Response<User>> UpdateAsync(long id, User user);
        // finding by id
        public Task<Response<User>> GetByIdAsync(long id);
        // finding by name
        public Task<Response<User>> GetByNameAsync(string name);
        public Task<Response<User>> GetAsync(Predicate<User> predicate);
        // taking all information 
        public Task<Response<List<User>>> GetAllAsync();
        // Deleting user 
        public Task<Response<bool>> DeleteAsync(long id);
    }
}
