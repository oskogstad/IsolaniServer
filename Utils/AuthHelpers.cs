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

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations: _numberOfIterations);
            byte[] hash = pbkdf2.GetBytes(_hashLength);

            byte[] hashAndSaltBytes = new byte[_saltLength + _hashLength];
            Array.Copy(salt, 0, hashAndSaltBytes, 0, _saltLength);
            Array.Copy(hash, 0, hashAndSaltBytes, _saltLength, _hashLength);

            return Convert.ToBase64String(hashAndSaltBytes);
        }

        public static void VerifyPassword(string savedPasswordHash, string requestPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            byte[] salt = new byte[_saltLength];
            
            Array.Copy(hashBytes, 0, salt, 0, _saltLength);
            var pbkdf2 = new Rfc2898DeriveBytes(requestPassword, salt, _numberOfIterations);
            
            byte[] hash = pbkdf2.GetBytes(_hashLength);

            for (int i = 0; i < _hashLength; i++)
                if (hashBytes[i + _saltLength] != hash[i])
                    throw new UnauthorizedAccessException();
        }
    }
}