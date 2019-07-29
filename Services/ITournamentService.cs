using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Isolani.Model;

namespace Isolani.Services
{
    public interface ITournamentService
    {
        Task<List<Tournament>> GetAllPublicTournamentsAsync();
        Task<Guid> CreateNewTournament(NewTournamentRequest newTournamentRequest);
    }
}