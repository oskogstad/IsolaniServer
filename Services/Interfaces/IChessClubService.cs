using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Isolani.Models;

namespace Isolani.Services.Interfaces
{
    public interface IChessClubService
    {
        Task<List<ChessClub>> GetAllChessClubs();
        Task<Guid> CreateNewChessClub(NewChessClubRequest newChessClubRequest);
    }
}