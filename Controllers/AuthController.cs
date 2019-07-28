using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Isolani.Database;
using Isolani.Model;
using Isolani.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Isolani.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private const string _unauthorizedMessage = "Incorrect email and/or password";

        private readonly IsolaniDbContext _isolaniDbContext;
        private readonly TokenSettings _tokenSettings;
        
        public AuthController(IsolaniDbContext isolaniDbContext, IOptions<TokenSettings> tokenSettings)
        {
            _isolaniDbContext = isolaniDbContext;
            _tokenSettings = tokenSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("login/")]
        public async Task<IActionResult> LogIn([FromBody] TokenRequest tokenRequest) 
        {
            var user = await _isolaniDbContext.Users.SingleOrDefaultAsync(usr => usr.Email.Equals(tokenRequest.Email));
            if(user == null)
                return Unauthorized(_unauthorizedMessage);

            try 
            {
                AuthHelpers.VerifyPassword(user.Password, tokenRequest.Password);
                
                user.LastLoginDate = DateTime.UtcNow;
                await _isolaniDbContext.SaveChangesAsync();

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _tokenSettings.Issuer,
                    Subject = new ClaimsIdentity(new[] 
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    IssuedAt = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                
                return Ok(token);
            }
            catch(UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
                return Unauthorized(_unauthorizedMessage);
            }
        }
    }
}
