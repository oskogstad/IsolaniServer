using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Isolani.Database;
using Isolani.Models;
using Isolani.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Isolani.Services
{
    public class UserExistsException : Exception { }

    internal class UserManagementService : IUserManagementService
    {
        private readonly IsolaniDbContext _isolaniDbContext;
        private readonly IMapper _objectMapper;

        public UserManagementService(IsolaniDbContext isolaniDbContext, IMapper objectMapper)
        {
            _isolaniDbContext = isolaniDbContext;
            _objectMapper = objectMapper;
        }

        public async Task<Guid> CreateNewUserPlayerAsync(NewUserPlayerRequest newUserPlayerRequest)
        {
            var userWithEmailExists = await 
                _isolaniDbContext.Users
                    .AnyAsync(user => 
                        user.Email.Equals(newUserPlayerRequest.Email, StringComparison.OrdinalIgnoreCase));

            if (userWithEmailExists)
                throw new UserExistsException();

            var now = DateTime.UtcNow;

            var savedPasswordHash = AuthenticationService.CreateSaltyPasswordHash(newUserPlayerRequest.Password);

            var userId = Guid.NewGuid();
            
            var newUser = _objectMapper.Map<User>(newUserPlayerRequest);
            newUser.Id = userId;
            newUser.CreatedDateUtc = newUser.LastLoginDateUtc = now;
            newUser.Password = savedPasswordHash;

            await _isolaniDbContext.AddAsync(newUser);
            
            var newPlayer = _objectMapper.Map<Player>(newUserPlayerRequest);
            newPlayer.Id = userId;

            await _isolaniDbContext.AddAsync(newPlayer);
            
            await _isolaniDbContext.SaveChangesAsync();

            return newUser.Id;
        }


        public async Task<List<Player>> GetAllUsersAsync()
        {
            return await _isolaniDbContext
                .Players
                .ToListAsync();
        }
    }
}