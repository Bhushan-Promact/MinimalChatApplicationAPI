using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalChatApplicationAPI.Model
{
    [Table("Users")]
    [PrimaryKey("UserId")]
    public class User
    {
        [DefaultValue("newid()")]
        public Guid UserId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        
        public string Password { get; set; } = string.Empty;
    }
}
