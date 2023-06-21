using Microsoft.AspNetCore.Mvc;
using MinimalChatApplicationAPI.Dto;
using MinimalChatApplicationAPI.Model;

namespace MinimalChatApplicationAPI.Service
{
    public interface IUserService
    {
        public Task<List<ResUserRegistrationDto>?> GetUsersAsync(string userID);
        public Task<ResUserRegistrationDto?> UpsertUser(UserRegistrationDto userDto);
        public Task<ResUserLoginDto?> LoginUserAsync(UserLoginDto userLoginDto);
    }
}
