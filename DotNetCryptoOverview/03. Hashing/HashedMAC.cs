using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DotNetCryptoOverview
{
    public static class HashedMAC
    {
        public static byte[] CreateHMAC(byte[] input, byte[] key)
        {
            using (var hmac = new HMACSHA256(key))
            {
                return hmac.ComputeHash(input);
            }
        }
    }
}
