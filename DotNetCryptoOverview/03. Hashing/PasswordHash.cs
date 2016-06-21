using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;

namespace DotNetCryptoOverview
{
    public static class PasswordHash
    {
        public const int SaltBytesLength = 8;

        public static PasswordHashingResult CreatePasswordHash(byte[] password)
        {
            var salt = GenerateSalt();
            var saltedPassword = password.Concat(salt).ToArray();

            using (var hashAlgorithm = CreateHashAlgorithm())
            {
                var hash = hashAlgorithm.ComputeHash(saltedPassword);

                return new PasswordHashingResult
                {
                    HashValue = hash,
                    Salt = salt
                };
            }
        }

        public static bool IsPasswordValid(byte[] password, PasswordHashingResult referenceHashResult)
        {
            var saltedPassword = password.Concat(referenceHashResult.Salt).ToArray();

            using (var hashAlgorithm = CreateHashAlgorithm())
            {
                var hash = hashAlgorithm.ComputeHash(saltedPassword);
                
                return hash.SequenceEqual(referenceHashResult.HashValue);
            }
        }

        private static byte[] GenerateSalt()
        {
            return RandomNumbers.GenerateRandomBytes(SaltBytesLength);
        }

        private static HashAlgorithm CreateHashAlgorithm()
        {
            return new SHA256Cng();
        }
    }

    public class PasswordHashingResult
    {
        public byte[] HashValue { get; set; }

        public byte[] Salt { get; set; }
    }
}
