using System;
using System.Security.Cryptography;

// This class is responsible for the encryption processes 
// Allows to call HashPassword method which returns encrypted password
namespace backend_dockerAPI.Helpers
{
    public class PasswordEncryption
    {

        private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        private const int HasingIterationsCount = 10101;
        
        public string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, SaltByteSize, HasingIterationsCount))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(HashByteSize);
            }
            byte[] dst = new byte[(SaltByteSize + HashByteSize) + 1];
            Buffer.BlockCopy(salt, 0, dst, 1, SaltByteSize);
            Buffer.BlockCopy(buffer2, 0, dst, SaltByteSize + 1, HashByteSize);
            return Convert.ToBase64String(dst);
        }
    }
}