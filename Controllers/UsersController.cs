using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using foo_chess_server.Domain;
using foo_chess_server.Database;
using foo_chess_server.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System;
using foo_chess_server.Model;

namespace foo_chess_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly FooChessContext _fooChessContext;
        public UsersController(FooChessContext fooChessContext) 
        {
            _fooChessContext = fooChessContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewUser([FromBody] NewUserRequest newUserRequest)
        {
            var userWithEmailExists = await 
                _fooChessContext.Users
                .AnyAsync(user => user.Email.Equals(newUserRequest.Email));

            if(userWithEmailExists)
                return StatusCode((int) HttpStatusCode.Conflict, $"User with email '{newUserRequest.Email}' already exists");

            var now = DateTime.UtcNow;

            var savedPasswordHash = AuthHelpers.CreateSaltyPasswordHash(newUserRequest.Password);

            var newUser = new User 
            {
                Id = Guid.NewGuid(),
                Email = newUserRequest.Email,
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
