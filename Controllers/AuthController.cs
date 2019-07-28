using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Isolani.Database;
using Isolani.Model;
using Isolani.Utils;

namespace Isolani.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private string _unauthorizedMessage = "Incorrect email and/or password";

        private readonly IsolaniContext _isolaniContext;
        public AuthController(IsolaniContext isolaniContext) 
        {
            _isolaniContext = isolaniContext;
        }

        [HttpPost("login/")]
        public async Task<IActionResult> LogIn([FromBody] TokenRequest tokenRequest) 
        {
            var user = await _isolaniContext.Users.SingleOrDefaultAsync(usr => usr.Email.Equals(tokenRequest.Email));
            if(user == null)
                return Unauthorized(_unauthorizedMessage);

            try 
            {
                AuthHelpers.VerifyPassword(user.Password, tokenRequest.Password);
                
                user.LastLoginDate = DateTime.UtcNow;
                await _isolaniContext.SaveChangesAsync();

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
