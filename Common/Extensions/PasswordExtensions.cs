using System.Security.Cryptography;

namespace Common.Extensions
{
    public static class PasswordExtensions
    {
        public static (byte[] hashedPassword, byte[] passwordKey) ToPasswordHmacSha512Hash(this string password)
        {
            using var hmac = new HMACSHA512();
            var passwordKey = hmac.Key;
            var passwordHash = hmac.ComputeHash(password.ToByteArray());
            return (passwordHash, passwordKey);
        }
    }
}
