using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Isolani.Database;
using Isolani.Models;
using Isolani.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Isolani.Services
{
    public class MissingSubscriptionException : Exception { }
    
    class TournamentService : ITournamentService
    {
        private readonly IsolaniDbContext _isolaniDbContext;

        public TournamentService(IsolaniDbContext _isolaniDbContext)
        {
            this._isolaniDbContext = _isolaniDbContext;
        }
        
        public async Task<List<Tournament>> GetAllPublicTournamentsAsync()
        {
            var allTournaments = await _isolaniDbContext.Tournaments.ToListAsync();
            return allTournaments;
        }

        public async Task<Guid> CreateNewTournament(NewTournamentRequest newTournamentRequest)
        {
            // TODO Check for subscription
            var newTournament = new Tournament
            {
                Id = Guid.NewGuid(),
                Name = newTournamentRequest.Name
            };
            await _isolaniDbContext.Tournaments.AddAsync(newTournament);
            await _isolaniDbContext.SaveChangesAsync();

            return newTournament.Id;
        }
    }
}