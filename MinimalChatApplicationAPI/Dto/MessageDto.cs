namespace MinimalChatApplicationAPI.Dto
{
    public class MessageDto
    {
        public Guid ReceiverId { get; set; }
        public string? TextMessage { get; set; }
    }
}
