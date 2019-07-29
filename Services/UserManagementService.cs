using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Isolani.Database;
using Isolani.Model;
using Isolani.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Isolani.Services
{
    public class UserExistsException : Exception { }
    
    public class UserManagementService : IUserManagementService
    {
        private readonly IsolaniDbContext _isolaniDbContext;
        
        public UserManagementService(IsolaniDbContext isolaniDbContext)
        {
            _isolaniDbContext = isolaniDbContext;
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

            var newUser = new User 
            {
                Id = Guid.NewGuid(),
                Email = newUserRequest.Email,
                CreatedDate  = now,
                LastLoginDate = now,
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