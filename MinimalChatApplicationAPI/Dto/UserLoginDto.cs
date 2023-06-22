using System.ComponentModel.DataAnnotations;

namespace MinimalChatApplicationAPI.Dto
{
    public class UserLoginDto
    {
        [EmailAddress]
        [RegularExpression(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$", ErrorMessage = "Please Enter Valid Email ID")]
        public string Email { get; set; } = string.Empty;

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")]
        public string Password { get; set; } = string.Empty;
    }
}
