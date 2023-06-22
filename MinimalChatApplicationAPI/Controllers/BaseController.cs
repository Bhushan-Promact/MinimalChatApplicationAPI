using Microsoft.AspNetCore.Mvc;

namespace MinimalChatApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected Guid GetUserId()
        {
            string userId = HttpContext.User.FindFirst("Id")!.Value;
            return Guid.Parse(userId);
        }
    }
}
