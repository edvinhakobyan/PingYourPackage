using System;
using System.Security.Cryptography;
using System.Text;

namespace PingYourPackage.Domain.Services
{
    public class CryptoService : ICryptoService
    {
        public string EncryptPassword(string password, string salt)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");
            if (string.IsNullOrEmpty(salt))
                throw new ArgumentNullException("salt");

            using (var sha256 = SHA256.Create())
            {
                var saltPassword = $"{salt}{password}";
                var saltPasswordAsBayts = Encoding.UTF8.GetBytes(saltPassword);
                var hashedsaltPasswordAsBayts = sha256.ComputeHash(saltPasswordAsBayts);
                return Convert.ToBase64String(hashedsaltPasswordAsBayts);
            }

        }

        public string GenerateSalt()
        {
            var data = new byte[0x10];
            using (var cryptoServerProvaider = new RNGCryptoServiceProvider())
            {
                cryptoServerProvaider.GetBytes(data);
                return Convert.ToBase64String(data);
            }
        }
    }
}
