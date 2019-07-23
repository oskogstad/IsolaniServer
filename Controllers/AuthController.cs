using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace foo_chess_server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            await Task.FromResult(1);
            return Ok("OK from AuthController");
        }
    }
}
