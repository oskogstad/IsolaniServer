using System.Threading.Tasks;
using Isolani.Models;
using Isolani.Services;
using Isolani.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Isolani.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TournamentsController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;

        public TournamentsController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        [AllowAnonymous]
        [HttpGet("public/")]
        public async Task<IActionResult> GetAllPublicTournaments()
        {
            var allTournaments = await _tournamentService.GetAllPublicTournamentsAsync();
            return Ok(allTournaments);
        }

        #if DEBUG
        [AllowAnonymous]
        #endif
        [HttpPost]
        public async Task<IActionResult> CreateNewTournament([FromBody] NewTournamentRequest newTournamentRequest)
        {
            try
            {
                var tournamentId = await _tournamentService.CreateNewTournament(newTournamentRequest);
                return Ok(tournamentId);
            }
            catch(MissingSubscriptionException)
            {
                return Unauthorized("A subscription is required for tournament creation");
            }
        }
    }
}