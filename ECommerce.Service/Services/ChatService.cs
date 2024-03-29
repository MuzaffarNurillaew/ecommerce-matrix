﻿using ECommerce.Data.IRepositories;
using ECommerce.Data.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Service.Helpers;
using ECommerce.Service.Interfaces;

namespace ECommerce.Service.Services
{
    public class ChatService : IChatService
    {
        private readonly IRepository<ChatInfo> chatRepo = new Repository<ChatInfo>();

        public async Task<Response<bool>> DeleteMessageAsync(long id)
        {
            if (chatRepo.SelectAsync(x => x.Id == id) is null)
            {
                return new Response<bool>();
            }

            await chatRepo.DeleteAsync(x => x.Id == id);

            return new Response<bool>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = true
            };
        }

        public async Task<Response<List<ChatInfo>>> GetAll(Predicate<ChatInfo> predicate = null)
        {
            return new Response<List<ChatInfo>>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = await chatRepo.SelectAllAsync(x => predicate(x))
            };
        }

        public async Task<Response<ChatInfo>> SendMessageAsync(ChatInfo message)
        {
            await chatRepo.CreateAsync(message);

            return new Response<ChatInfo>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = message
            };
        }

        public async Task<Response<ChatInfo>> UpdateMessageAsync(long id, string message)
        {
            var OldchatInfo = await chatRepo.SelectAsync(x => x.Id == id);

            if (OldchatInfo is null)
            {
                return new Response<ChatInfo>();
            }

            OldchatInfo.Message = message;

            return new Response<ChatInfo>()
            {
                StatusCode = 200,
                Message = "Success",
                Result = OldchatInfo
            };
        }
    }
}
