using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net;
using Isolani.Models;
using Isolani.Services;
using Isolani.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Isolani.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;
        public UsersController(IUserManagementService userManagementService) 
        {
            _userManagementService = userManagementService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateNewUser([FromBody] NewUserRequest newUserRequest)
        {
            try
            {
                var newUserId = await _userManagementService.CreateNewUserAsync(newUserRequest);
                return Ok(newUserId);
            }
            catch (UserExistsException)
            {
                return StatusCode((int) HttpStatusCode.Conflict, $"User with email '{newUserRequest.Email}' already exists");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers() 
        {
            var allUsers = await _userManagementService.GetAllUsersAsync();
            return Ok(allUsers);
        }
    }
}
