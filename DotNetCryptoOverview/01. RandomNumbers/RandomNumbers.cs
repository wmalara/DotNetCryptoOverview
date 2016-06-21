using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DotNetCryptoOverview
{
    public static class RandomNumbers
    {
        public static byte[] GenerateRandomBytes(int bytesCount)
        {
            using (var rng = RandomNumberGenerator.Create())    //new RNGCryptoServiceProvider()
            {
                var buffer = new byte[bytesCount];
                rng.GetBytes(buffer);
                return buffer;
            }
        }
    }
}
