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

        private const int _saltLength = 31;
        private const int _hashLength = 33;
        private const int _numberOfIterations = 2500;

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
            user.LastLoginDate = now;
            
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
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[_saltLength]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _numberOfIterations);
            var hash = pbkdf2.GetBytes(_hashLength);

            var hashAndSaltBytes = new byte[_saltLength + _hashLength];
            Array.Copy(salt, 0, hashAndSaltBytes, 0, _saltLength);
            Array.Copy(hash, 0, hashAndSaltBytes, _saltLength, _hashLength);

            return Convert.ToBase64String(hashAndSaltBytes);
        }

        private static void VerifyPassword(string savedSaltyPasswordHash, string requestPassword)
        {
            var saltyHashBytes = Convert.FromBase64String(savedSaltyPasswordHash);
            var salt = new byte[_saltLength];
            var savedPasswordHash = new byte[_hashLength];

            Array.Copy(saltyHashBytes, _saltLength, savedPasswordHash, 0, _hashLength);
            Array.Copy(saltyHashBytes, 0, salt, 0, _saltLength);

            var pbkdf2 = new Rfc2898DeriveBytes(requestPassword, salt, _numberOfIterations);
            var requestPasswordHash = pbkdf2.GetBytes(_hashLength);

            // slow equals
            var diff = savedPasswordHash.Length ^ requestPasswordHash.Length;
            for (var i = 0; i < savedPasswordHash.Length && i < requestPasswordHash.Length; i++)
                diff |= savedPasswordHash[i] ^ requestPasswordHash[i];

            if (diff != 0)
                throw new UnauthorizedAccessException();
        }
    }
}