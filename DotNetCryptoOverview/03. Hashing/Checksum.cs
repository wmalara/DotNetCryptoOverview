using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;

namespace DotNetCryptoOverview
{
    public static class Checksum
    {
        public static byte[] CreateChecksumHash(byte[] input)
        {
            using (var hashAlgorithm = new SHA256Cng())
            {
                return hashAlgorithm.ComputeHash(input);
            }
        }
    }
}
