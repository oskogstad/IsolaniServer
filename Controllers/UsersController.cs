using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Net;
using Isolani.Model;
using Isolani.Services;
using Microsoft.AspNetCore.Authorization;

namespace Isolani.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet]
        public async Task<IActionResult> GetAllUsers() 
        {
            var allUsers = await _userManagementService.GetAllUsersAsync();
            return Ok(allUsers);
        }
    }
}
