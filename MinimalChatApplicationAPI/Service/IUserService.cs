using MinimalChatApplicationAPI.Dto;

namespace MinimalChatApplicationAPI.Service
{
    public interface IUserService
    {
        public Task<List<ResUserRegistrationDto>?> GetUsersAsync(Guid userID);
        public Task<ResUserRegistrationDto> UpsertUser(UserRegistrationDto userDto);
        public Task<ResUserLoginDto?> LoginUserAsync(UserLoginDto userLoginDto);
    }
}
