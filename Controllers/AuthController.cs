using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Isolani.Model;
using Isolani.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Isolani.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private const string _unauthorizedMessage = "Incorrect email and/or password";

        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        
        [AllowAnonymous]
        [HttpPost("token/")]
        public async Task<IActionResult> RequestToken([FromBody] TokenRequest tokenRequest) 
        {
            try
            {
                var token = await _authenticationService.GetTokenAsync(tokenRequest);
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
