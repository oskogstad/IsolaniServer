using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using foo_chess_server.Database;

namespace foo_chess_server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
       private FooChessContext _fooChessContext;
        public AuthController(FooChessContext fooChessContext) 
        {
            _fooChessContext = fooChessContext;
        }

        [HttpPost("login/")]
        public async Task<IActionResult> LogIn([FromBody] TokenRequest request) 
        {
            var user = await _fooChessContext.Users.SingleOrDefaultAsync(usr => usr.Email.Equals(request.Email));
            if(user == null)
            {
                return Unauthorized("Incorrect email and/or password");
            }

            return Ok("User found");
        }

        public class TokenRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
