using System.Threading.Tasks;
using Isolani.Models;
using Isolani.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Isolani.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChessClubsController : ControllerBase
    {
        private readonly IChessClubService _chessClubService;
        public ChessClubsController(IChessClubService chessClubService)
        {
            _chessClubService = chessClubService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateNewChessClub(NewChessClubRequest newChessClubRequest)
        {
            var newChessClubId = await _chessClubService.CreateNewChessClub(newChessClubRequest);

            return Ok(newChessClubId);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllChessClubs()
        {
            var allChessClubs = await _chessClubService.GetAllChessClubs();
            return Ok(allChessClubs);
        }
    }
}