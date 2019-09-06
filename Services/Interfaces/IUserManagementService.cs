using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Isolani.Models;

namespace Isolani.Services.Interfaces
{
    public interface IUserManagementService
    {
        Task<Guid> CreateNewUserPlayerAsync(NewUserPlayerRequest newUserPlayerRequest);
        Task<List<Player>> GetAllUsersAsync();
    }
}