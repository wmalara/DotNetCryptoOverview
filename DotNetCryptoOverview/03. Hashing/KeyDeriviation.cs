using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;

namespace DotNetCryptoOverview
{
    public static class KeyDeriviation
    {
        public const int SaltBytesLength = 8;
        public const int NumberOfDerivedBytes = 32;

        public static KeyDerivationResult CreateDerivedKey(byte[] password, int numberOfDerivedBytes, int numberOfIterations = 100000)
        {
            var salt = CreateSalt();

            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, numberOfIterations))
            {
                var derivedKey = deriveBytes.GetBytes(numberOfDerivedBytes);

                return new KeyDerivationResult
                {
                    DerivedKey = derivedKey,
                    Salt = salt,
                    NumberOfIterations = numberOfIterations
                };
            }
        }

        private static byte[] CreateSalt()
        {
            return RandomNumbers.GenerateRandomBytes(SaltBytesLength);
        }
    }

    public class KeyDerivationResult
    {
        public byte[] DerivedKey { get; set; }

        public byte[] Salt { get; set; }

        public int NumberOfIterations { get; set; }
    }
}