using System.Security.Cryptography;
using System;

namespace foo_chess_server.Utils
{
    public static class AuthHelpers
    {
        private const int _saltLength = 31;
        private const int _hashLength = 33;
        private const int _numberOfIterations = 2500;

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

        public static void VerifyPassword(string savedSaltyPasswordHash, string requestPassword)
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
            for(var i = 0; i < savedPasswordHash.Length && i < requestPasswordHash.Length; i++)
                diff |= savedPasswordHash[i] ^ requestPasswordHash[i];
            
            if(diff != 0)
                throw new UnauthorizedAccessException();
        }
    }
}