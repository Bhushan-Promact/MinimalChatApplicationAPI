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

        public Guid? SenderId { get; set; }
        public User? User1 { get; set; }

        public Guid? ReceiverId { get; set; }
        public User? User2 { get; set; }

        public string? TextMessage { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
