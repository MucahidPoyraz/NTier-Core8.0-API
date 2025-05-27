using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected int GetUserIdFromToken()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id" || c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                throw new UnauthorizedAccessException("Kullanıcı kimliği alınamadı.");

            return int.Parse(userIdClaim.Value);
        }
    }
}
