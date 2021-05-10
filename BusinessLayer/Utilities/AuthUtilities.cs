using DataLayer;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static Common.Constants;

namespace BusinessLayer.Utilities
{
    public static class AuthUtilities
    {
        public static bool Validate(string password, User user)
        {
            var salt = Encoding.Default.GetBytes(user.PasswordSalt);

            var hash = Encoding.Default.GetBytes(user.PasswordHash);

            var passwordBytes = Encoding.Default.GetBytes(password);

            var generatedHash = GenerateHash(passwordBytes, salt, Auth.HashIterations, password.Length);

            return hash.SequenceEqual(generatedHash);
        }

        public static (byte[], byte[]) Create(string password)
        {
            var salt = GenerateSalt(password.Length);

            var passwordBytes = Encoding.Default.GetBytes(password);

            var hash = GenerateHash(passwordBytes, salt, Auth.HashIterations, password.Length);

            return (salt, hash);
        }

        public static byte[] GenerateSalt(int length)
        {
            var bytes = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return bytes;
        }

        public static byte[] GenerateHash(byte[] password, byte[] salt, int iterations, int length)
        {
            using var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations);
            return deriveBytes.GetBytes(length);
        }
    }
}
