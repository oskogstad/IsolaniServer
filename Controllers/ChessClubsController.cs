using System;
using System.Threading.Tasks;
using Isolani.Database;
using Isolani.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Isolani.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChessClubsController : ControllerBase
    {
        private readonly IsolaniDbContext _isolaniDbContext;
        public ChessClubsController(IsolaniDbContext isolaniDbContext)
        {
            _isolaniDbContext = isolaniDbContext;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateNewChessClub(NewChessClubRequest newChessClubRequest)
        {
            var newChessClub = new ChessClub
            {
                Id = Guid.NewGuid(),
                Name = newChessClubRequest.Name
            };

            await _isolaniDbContext.ChessClubs.AddAsync(newChessClub);
            await _isolaniDbContext.SaveChangesAsync();

            return Ok(newChessClub.Id);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllChessClubs()
        {
            var allChessClubs = await _isolaniDbContext.ChessClubs.ToListAsync();
            return Ok(allChessClubs);
        }
    }
}