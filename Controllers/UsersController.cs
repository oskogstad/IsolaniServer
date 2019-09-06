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
        public async Task<IActionResult> CreateNewUser([FromBody] NewUserPlayerRequest newUserPlayerRequest)
        {
            try
            {
                var newUserId = await _userManagementService.CreateNewUserPlayerAsync(newUserPlayerRequest);
                return Ok(newUserId);
            }
            catch (UserExistsException)
            {
                return StatusCode((int) HttpStatusCode.Conflict, $"User with email '{newUserPlayerRequest.Email}' already exists");
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
