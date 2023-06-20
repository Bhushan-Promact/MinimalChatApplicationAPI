using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalChatApplicationAPI.Model
{
    [Table("Messages")]
    [PrimaryKey("MessageId")]
    public class Message
    {
        [DefaultValue("newid()")]
        public Guid MessageId { get; set; }
        public virtual User? SenderId { get; set; }
        public virtual User? ReceiverId { get; set; }
        public string? TextMessage { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
