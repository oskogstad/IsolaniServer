using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Isolani.Models;

namespace Isolani.Services.Interfaces
{
    public interface ITournamentService
    {
        Task<List<Tournament>> GetAllPublicTournamentsAsync();
        Task<Guid> CreateNewTournament(NewTournamentRequest newTournamentRequest);
    }
}