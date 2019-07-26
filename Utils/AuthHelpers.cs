using System.Security.Cryptography;
using System;

namespace foo_chess_server.Utils
{
    public static class AuthHelpers
    {
        private const int _saltLength = 31;
        private const int _hashLength = 33;

        public static string CreateSaltyPasswordHash(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[_saltLength]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations: 2500);
            byte[] hash = pbkdf2.GetBytes(_hashLength);

            byte[] hashAndSaltBytes = new byte[_saltLength + _hashLength];
            Array.Copy(salt, 0, hashAndSaltBytes, 0, _saltLength);
            Array.Copy(hash, 0, hashAndSaltBytes, _saltLength, _hashLength);

            return Convert.ToBase64String(hashAndSaltBytes);
        }
    }

}