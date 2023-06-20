using MinimalChatApplicationAPI.Model;

namespace MinimalChatApplicationAPI.Dto
{
    public class ResMessageDto
    {
        public Guid MessageId { get; set; }
        public virtual User? SenderId { get; set; }
        public virtual User? ReceiverId { get; set; }
        public string? TextMessage { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
