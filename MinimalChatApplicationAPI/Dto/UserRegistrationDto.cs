using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MinimalChatApplicationAPI.Dto
{
    public class UserRegistrationDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Please Enter Valid Email ID")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
