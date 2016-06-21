using System.IO;
using System.Security.Cryptography;
using System.IO.Compression;

namespace DotNetCryptoOverview
{
    public class CryptoStreamHelper
    {
        public static void CompressEncryptFile(string inputFile, string outputFile, byte[] key, byte[] iv)
        {
            using (var cipher = new AesCryptoServiceProvider())
            using (ICryptoTransform encryptor = cipher.CreateEncryptor(key, iv))
            using (var outputFileStream = File.Create(outputFile))
            using (var cryptoStream = new CryptoStream(outputFileStream, encryptor, CryptoStreamMode.Write))
            using (var deflateStream = new DeflateStream(cryptoStream, CompressionMode.Compress))
            using (var inputFileStream = File.OpenRead(inputFile))
            {
                inputFileStream.CopyTo(deflateStream);
            }
        }

        public static void DecryptDecompressFile(string inputFile, string outputFile, byte[] key, byte[] iv)
        {
            using (var cipher = new AesCryptoServiceProvider())
            using (ICryptoTransform decryptor = cipher.CreateDecryptor(key, iv))
            using (var inputFileStream = File.OpenRead(inputFile))            
            using (var cryptoStream = new CryptoStream(inputFileStream, decryptor, CryptoStreamMode.Read))
            using (var deflateStream = new DeflateStream(cryptoStream, CompressionMode.Decompress))
            using (var outputFileStream = File.Create(outputFile))
            {
                deflateStream.CopyTo(outputFileStream);
            }                
        }
    }
}
