using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public async Task<Guid> CreateNewUserAsync(NewUserRequest newUserRequest)
        {
            var userWithEmailExists = await 
                _isolaniDbContext.Users
                    .AnyAsync(user => user.Email.Equals(newUserRequest.Email));

            if (userWithEmailExists)
                throw new UserExistsException();

            var now = DateTime.UtcNow;

            var savedPasswordHash = AuthenticationService.CreateSaltyPasswordHash(newUserRequest.Password);

            var newUser = _objectMapper.Map<User>(newUserRequest);
            newUser.CreatedDateUtc = newUser.LastLoginDateUtc = now;
            newUser.Password = savedPasswordHash;
            newUser.Id = Guid.NewGuid();

            await _isolaniDbContext.AddAsync(newUser);
            await _isolaniDbContext.SaveChangesAsync();

            return newUser.Id;
        }


        public async Task<List<Player>> GetAllUsersAsync()
        {
            return await _isolaniDbContext
                .Users
                .ProjectTo<Player>(_objectMapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}