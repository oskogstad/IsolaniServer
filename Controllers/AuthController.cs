using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using foo_chess_server.Database;
using System;
using foo_chess_server.Model;
using foo_chess_server.Utils;

namespace foo_chess_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private string _unauthorizedMessage = "Incorrect email and/or password";

        private readonly FooChessContext _fooChessContext;
        public AuthController(FooChessContext fooChessContext) 
        {
            _fooChessContext = fooChessContext;
        }

        [HttpPost("login/")]
        public async Task<IActionResult> LogIn([FromBody] TokenRequest tokenRequest) 
        {
            var user = await _fooChessContext.Users.SingleOrDefaultAsync(usr => usr.Email.Equals(tokenRequest.Email));
            if(user == null)
                return Unauthorized(_unauthorizedMessage);

            try 
            {
                AuthHelpers.VerifyPassword(user.Password, tokenRequest.Password);
                
                user.LastLoginDate = DateTime.UtcNow;
                await _fooChessContext.SaveChangesAsync();

                return Ok("Login OK");
            }
            catch(UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
                return Unauthorized(_unauthorizedMessage);
            }
        }
    }
}
