using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace eOvjera.Common.Security
{
    /// <summary>
    /// Common - Security - Hashing helper class.
    /// </summary>
    public static class CryptoHashManager
    {
        // 24 = 192 bits
        private const int m_SaltByteSize = 24;
        private const int m_HashByteSize = 24;
        private const int m_HasingIterationsCount = 10101;


        /// <summary>
        /// Hashes the input password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>Password hash.</returns>
        /// <exception cref="ArgumentNullException">password</exception>
        public static string HashPassword(string password)
        {
            // http://stackoverflow.com/questions/19957176/asp-net-identity-password-hashing

            byte[] salt;
            byte[] buffer2;

            if (String.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");

            using (var bytes = new Rfc2898DeriveBytes(password, m_SaltByteSize, m_HasingIterationsCount))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(m_HashByteSize);
            }

            var dst = new byte[(m_SaltByteSize + m_HashByteSize) + 1];
            Buffer.BlockCopy(salt, 0, dst, 1, m_SaltByteSize);
            Buffer.BlockCopy(buffer2, 0, dst, m_SaltByteSize + 1, m_HashByteSize);

            return Convert.ToBase64String(dst);
        }

        /// <summary>
        /// Verifies the hashed password.
        /// </summary>
        /// <param name="hashedPassword">The hashed password.</param>
        /// <param name="password">The password.</param>
        /// <returns>True if password is valid, false if not.</returns>
        /// <exception cref="ArgumentNullException">password</exception>
        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] passwordHashBytes;

            int arrayLen = (m_SaltByteSize + m_HashByteSize) + 1;

            if (hashedPassword == null)
                return false;

            if (password == null)
                throw new ArgumentNullException("password");

            var src = Convert.FromBase64String(hashedPassword);

            if ((src.Length != arrayLen) || (src[0] != 0))
                return false;

            var currentSaltBytes = new byte[m_SaltByteSize];
            Buffer.BlockCopy(src, 1, currentSaltBytes, 0, m_SaltByteSize);

            var currentHashBytes = new byte[m_HashByteSize];
            Buffer.BlockCopy(src, m_SaltByteSize + 1, currentHashBytes, 0, m_HashByteSize);

            using (var bytes = new Rfc2898DeriveBytes(password, currentSaltBytes, m_HasingIterationsCount))
            {
                passwordHashBytes = bytes.GetBytes(m_HashByteSize);
            }

            return AreHashesEqual(currentHashBytes, passwordHashBytes);
        }

        /// <summary>
        /// Ares the hashes equal.
        /// </summary>
        /// <param name="firstHash">The first hash.</param>
        /// <param name="secondHash">The second hash.</param>
        /// <returns></returns>
        private static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            int _minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < _minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }

    }
}