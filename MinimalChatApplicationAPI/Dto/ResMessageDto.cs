using MinimalChatApplicationAPI.Model;

namespace MinimalChatApplicationAPI.Dto
{
    public class ResMessageDto
    {
        public Guid MessageId { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string? TextMessage { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
