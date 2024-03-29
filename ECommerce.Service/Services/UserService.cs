﻿using ECommerce.Data.IRepositories;
using ECommerce.Data.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Service.Helpers;
using ECommerce.Service.Interfaces;

namespace ECommerce.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> repostoryService = new Repository<User>();

        public async Task<Response<User>> CreateAsync(User user)
        {
            var model = await repostoryService.SelectAsync(x => 
            x.Username == user.Username
            || x.Email.ToLower() == user.Email.ToLower() 
            || x.PhoneNumber == user.PhoneNumber);

            if (model is not null)
            {
                return new Response<User>()
                {
                    Message = "Bunaqa user mavjud"
                };
            }

            var result = await repostoryService.CreateAsync(user);
            return new Response<User>()
            {
                StatusCode = 201,
                Message = "Created",
                Result = result
            };
        }

        public async Task<Response<bool>> DeleteAsync(long id)
        {
            var model = await repostoryService.SelectAsync(p => p.Id == id);

            if (model is not null)
                return new Response<bool>
                {
                    Message = "Not found",
                    Result = false
                };

            await repostoryService.DeleteAsync(v => v.Id == id);
            return new Response<bool>
            {
                StatusCode = 200,
                Message = "OK",
                Result = true
            };
        }

        public async Task<Response<List<User>>> GetAllAsync(Predicate<User> predicate = null)
        {
            var results = await repostoryService.SelectAllAsync(x => predicate(x));

            return new Response<List<User>>()
            {
                StatusCode = 200,
                Message = "OK",
                Result = results
            };
        }

        public async Task<Response<User>> GetByIdAsync(long id)
        {
            var entities = await repostoryService.SelectAllAsync();
            var model = entities.FirstOrDefault(p => p.Id == id);

            if (model is null)
            {
                return new Response<User>();
            }

            return new Response<User>()
            {
                StatusCode = 200,
                Message = "OK",
                Result = model
            };
        }

        public async Task<Response<User>> GetAsync(Predicate<User> predicate)
        {
            var model = await repostoryService.SelectAsync(x => predicate(x));

            if (model is null)
            {
                return new Response<User>();
            }

            return new Response<User>()
            {
                StatusCode = 200,
                Message = "OK",
                Result = model
            };
        }

        public async Task<Response<User>> GetByNameAsync(string name)
        {
            var enteties = await repostoryService.SelectAllAsync();
            var model = enteties.FirstOrDefault(p => p.FirstName == name);

            if (model is null)
            {
                return new Response<User>();
            }

            return new Response<User>()
            {
                StatusCode = 200,
                Message = "OK",
                Result = model
            };
        }

        public async Task<Response<User>> UpdateAsync(long id, User user)
        {
            var entities = await repostoryService.SelectAllAsync();
            var model = entities.FirstOrDefault(p => p.Id == id);

            if (model is null)
                return new Response<User>();

            var result = await repostoryService.UpdateAsync(user);
            return new Response<User>()
            {
                StatusCode = 200,
                Message = "OK",
                Result = model
            };
        }
    }
}
