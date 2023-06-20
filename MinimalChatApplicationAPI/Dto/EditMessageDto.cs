namespace MinimalChatApplicationAPI.Dto
{
    public class EditMessageDto
    {
        public Guid MessageId { get; set; }
        public string? TextMessage { get; set; }
    }
}
