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

    internal class TournamentService : ITournamentService
    {
        private readonly IsolaniDbContext _isolaniDbContext;

        public TournamentService(IsolaniDbContext isolaniDbContext)
        {
            _isolaniDbContext = isolaniDbContext;
        }
        
        public async Task<List<Tournament>> GetAllPublicTournamentsAsync()
        {
            return await _isolaniDbContext.Tournaments.ToListAsync();
        }

        public async Task<Guid> CreateNewTournament(NewTournamentRequest newTournamentRequest)
        {
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