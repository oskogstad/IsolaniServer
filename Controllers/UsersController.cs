using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using foo_chess_server.Domain;
using foo_chess_server.Database;
using foo_chess_server.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System;

namespace foo_chess_server.Controllers
{
    public class NewUserDto 
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private FooChessContext _fooChessContext;
        public UsersController(FooChessContext fooChessContext) 
        {
            _fooChessContext = fooChessContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewUser([FromBody] NewUserDto newUserDto)
        {
            if(!newUserDto.Email.IsValidEmail())
                return BadRequest($"Email '{newUserDto.Email}' is not valid");

            if(string.IsNullOrWhiteSpace(newUserDto.Password))
                return BadRequest("Password cannot be empty");

            var userWithEmailExists = await 
                _fooChessContext.Users
                .AnyAsync(user => user.Email.Equals(newUserDto.Email));

            if(userWithEmailExists)
                return StatusCode((int) HttpStatusCode.Conflict, $"User with email '{newUserDto.Email}' already exists");

            var now = DateTime.UtcNow;

            string savedPasswordHash = AuthHelpers.CreateSaltyPasswordHash(newUserDto.Password);

            var newUser = new User 
            {
                Id = Guid.NewGuid(),
                Email = newUserDto.Email,
                CreatedDate  = now,
                LastLoginDate = now,
                Password = savedPasswordHash
            };
            
            await _fooChessContext.AddAsync(newUser);
            await _fooChessContext.SaveChangesAsync();

            return Ok(newUser.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers() 
        {
            var allUsers = await _fooChessContext.Users.ToListAsync();
            return Ok(allUsers);
        }
    }
}
