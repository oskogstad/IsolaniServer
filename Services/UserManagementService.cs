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

        public async Task<Guid> CreateNewUserAsync(NewUserRequest newUserRequest)
        {
            var userWithEmailExists = await 
                _isolaniDbContext.Users
                    .AnyAsync(user => user.Email.Equals(newUserRequest.Email));

            if (userWithEmailExists)
                throw new UserExistsException();

            var now = DateTime.UtcNow;

            var savedPasswordHash = AuthenticationService.CreateSaltyPasswordHash(newUserRequest.Password);

//            var newUser = _objectMapper.Map<User>(newUserRequest);
//            newUser.CreatedDate = newUser.LastLoginDate = now;
//            newUser.Password = savedPasswordHash;
//            newUser.Id = Guid.NewGuid();

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = newUserRequest.Name,
                ChessClubId = newUserRequest.ChessClubId,
                BirthYear = newUserRequest.BirthYear,
                Title = newUserRequest.Title,
                BlitzRating = newUserRequest.BlitzRating,
                Country = newUserRequest.Country,
                RapidRating = newUserRequest.RapidRating,
                StandardRating = newUserRequest.StandardRating,
                Email = newUserRequest.Email,
                CreatedDateUtc = now,
                LastLoginDateUtc = now,
                Password = savedPasswordHash

            };
            
            await _isolaniDbContext.AddAsync(newUser);
            await _isolaniDbContext.SaveChangesAsync();

            return newUser.Id;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _isolaniDbContext.Users.ToListAsync();
        }
    }

}