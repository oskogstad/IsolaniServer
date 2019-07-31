using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Isolani.Database;
using Isolani.Models;
using Isolani.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Isolani.Services
{
    class ChessClubService : IChessClubService
    {
        private readonly IsolaniDbContext _isolaniDbContext;

        public ChessClubService(IsolaniDbContext isolaniDbContext)
        {
            _isolaniDbContext = isolaniDbContext;
        }
        
        public async Task<List<ChessClub>> GetAllChessClubs()
        {
            var allChessClubs = await _isolaniDbContext.ChessClubs.ToListAsync();
            return allChessClubs;
        }

        public async Task<Guid> CreateNewChessClub(NewChessClubRequest newChessClubRequest)
        {
            var newChessClub = new ChessClub
            {
                Id = Guid.NewGuid(),
                Name = newChessClubRequest.Name
            };

            await _isolaniDbContext.ChessClubs.AddAsync(newChessClub);
            await _isolaniDbContext.SaveChangesAsync();

            return newChessClub.Id;
        }
    }
}