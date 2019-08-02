using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Isolani.Database;
using Isolani.Models;
using Isolani.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Isolani.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IsolaniDbContext _isolaniDbContext;
        private readonly TokenSettings _tokenSettings;

        private const int SaltLength = 31;
        private const int HashLength = 33;
        private const int NumberOfIterations = 2500;

        public AuthenticationService(IsolaniDbContext isolaniDbContext, IOptions<TokenSettings> tokenSettings)
        {
            _isolaniDbContext = isolaniDbContext;
            _tokenSettings = tokenSettings.Value;
        }

        public async Task<SecurityToken> GetTokenAsync(TokenRequest tokenRequest)
        {
            var user = await _isolaniDbContext.Users.SingleOrDefaultAsync(usr => usr.Email.Equals(tokenRequest.Email));
            if (user == null)
                throw new UnauthorizedAccessException();

            VerifyPassword(user.Password, tokenRequest.Password);

            var now = DateTime.UtcNow;
            user.LastLoginDateUtc = now;
            
            await _isolaniDbContext.SaveChangesAsync();

            return CreateSecurityToken(user, now);
        }

        private SecurityToken CreateSecurityToken(User user, DateTime now)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _tokenSettings.Issuer,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                IssuedAt = now,
                Expires = now.AddMinutes(_tokenSettings.ExpirationTimeInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            
            return tokenHandler.CreateToken(tokenDescriptor);
        }


        public static string CreateSaltyPasswordHash(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltLength]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, NumberOfIterations);
            var hash = pbkdf2.GetBytes(HashLength);

            var hashAndSaltBytes = new byte[SaltLength + HashLength];
            Array.Copy(salt, 0, hashAndSaltBytes, 0, SaltLength);
            Array.Copy(hash, 0, hashAndSaltBytes, SaltLength, HashLength);

            return Convert.ToBase64String(hashAndSaltBytes);
        }

        private static void VerifyPassword(string savedSaltyPasswordHash, string requestPassword)
        {
            var saltyHashBytes = Convert.FromBase64String(savedSaltyPasswordHash);
            var salt = new byte[SaltLength];
            var savedPasswordHash = new byte[HashLength];

            Array.Copy(saltyHashBytes, SaltLength, savedPasswordHash, 0, HashLength);
            Array.Copy(saltyHashBytes, 0, salt, 0, SaltLength);

            var pbkdf2 = new Rfc2898DeriveBytes(requestPassword, salt, NumberOfIterations);
            var requestPasswordHash = pbkdf2.GetBytes(HashLength);

            // slow equals
            var diff = savedPasswordHash.Length ^ requestPasswordHash.Length;
            for (var i = 0; i < savedPasswordHash.Length && i < requestPasswordHash.Length; i++)
                diff |= savedPasswordHash[i] ^ requestPasswordHash[i];

            if (diff != 0)
                throw new UnauthorizedAccessException();
        }
    }
}