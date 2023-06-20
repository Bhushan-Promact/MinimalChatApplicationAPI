using Microsoft.AspNetCore.Mvc;
using MinimalChatApplicationAPI.Dto;

namespace MinimalChatApplicationAPI.Service
{
    public interface IUserService
    {
        public Task<List<ResUserRegistrationDto>?> GetUsersAsync();
        public Task<ResUserRegistrationDto?> UpsertUser(UserRegistrationDto userDto);
        public Task<ResUserLoginDto?> LoginUserAsync(UserLoginDto userLoginDto);
    }
}
