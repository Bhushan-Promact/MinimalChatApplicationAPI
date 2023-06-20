namespace MinimalChatApplicationAPI.Dto
{
    public class ResUserLoginDto
    {
        public string Token { get; set; } = string.Empty;
        public ResUserRegistrationDto? User { get; set; }
    }
}
