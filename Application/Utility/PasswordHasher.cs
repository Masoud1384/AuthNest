using System.Security.Cryptography;

namespace Application.Utility
{
    public static class PasswordHasher
    {
        public static string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(16);
            var key = Rfc2898DeriveBytes.Pbkdf2(password, salt, 10000, HashAlgorithmName.SHA256, 32);
            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
        }

        public static bool Verify(string password, string hash)
        {
            var parts = hash.Split('.');
            if (parts.Length != 2)
                return false;

            var salt = Convert.FromBase64String(parts[0]);
            var key = Convert.FromBase64String(parts[1]);
            var keyToCheck = Rfc2898DeriveBytes.Pbkdf2(password, 
                salt, 10000, HashAlgorithmName.SHA256, 32);

            return keyToCheck.SequenceEqual(key);
        }
    }
}
