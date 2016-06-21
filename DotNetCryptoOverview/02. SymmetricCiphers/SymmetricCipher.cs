using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace DotNetCryptoOverview
{
    public static class SymmetricCipher
    {
        public static EncryptionResult Encrypt(byte[] data, byte[] key)
        {
            using (var cipher = new AesCryptoServiceProvider())
            {
                cipher.Key = key;
                cipher.GenerateIV();
                
                //setting the defaults explicitly
                cipher.Mode = CipherMode.CBC;
                cipher.Padding = PaddingMode.PKCS7;

                using (var encryptor = cipher.CreateEncryptor())
                {
                    var cipherText = Crypt(data, encryptor);

                    return new EncryptionResult
                    {
                        Ciphertext = cipherText,
                        IV = cipher.IV
                    };
                }
            }
        }

        public static byte[] Decrypt(EncryptionResult encryptionResult, byte[] key)
        {
            using (var cipher = new AesCryptoServiceProvider())
            {
                cipher.Key = key;
                cipher.IV = encryptionResult.IV;

                //setting the defaults explicitly
                cipher.Mode = CipherMode.CBC;
                cipher.Padding = PaddingMode.PKCS7;

                using (var encryptor = cipher.CreateDecryptor())
                {
                    var plaintext = Crypt(encryptionResult.Ciphertext, encryptor);
                    return plaintext;
                }
            }
        }

        private static byte[] Crypt(byte[] data, ICryptoTransform cryptor)
        {
            var memoryStream = new MemoryStream();
            using (Stream cryptoStream = new CryptoStream(memoryStream, cryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(data, 0, data.Length);
            }

            return memoryStream.ToArray();
        }
    }

    public class EncryptionResult
    {
        public byte[] Ciphertext { get; set; }

        public byte[] IV { get; set; }
    }
}
