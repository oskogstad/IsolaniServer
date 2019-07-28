using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System;
using Isolani.Database;
using Isolani.Model;
using Isolani.Utils;
using Microsoft.AspNetCore.Authorization;

namespace Isolani.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IsolaniDbContext _isolaniDbContext;
        public UsersController(IsolaniDbContext isolaniDbContext) 
        {
            _isolaniDbContext = isolaniDbContext;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateNewUser([FromBody] NewUserRequest newUserRequest)
        {
            var userWithEmailExists = await 
                _isolaniDbContext.Users
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
            
            await _isolaniDbContext.AddAsync(newUser);
            await _isolaniDbContext.SaveChangesAsync();

            return Ok(newUser.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers() 
        {
            var allUsers = await _isolaniDbContext.Users.ToListAsync();
            return Ok(allUsers);
        }
    }
}
