using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DotNetCryptoOverview
{
    class Program
    {
        static void Main(string[] args)
        {
            //SymmetricCiphersDemo();
            //ChecksumDemo();
            //PasswordHashDemo();
            //KeyDerivationDemo();
            //CryptoStreamDemo();
            SecureQueryParamEncryptionDemo();

            Console.ReadKey();
        }

        private static void PasswordHashDemo()
        {
            var password = "admin1";
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            var hashingResult = PasswordHash.CreatePasswordHash(passwordBytes);
            var hashingResult2 = PasswordHash.CreatePasswordHash(passwordBytes);

            var invalidPassword = "admin2";
            var invalidPasswordBytes = Encoding.UTF8.GetBytes(invalidPassword);
            var isInvalidPasswordValid = PasswordHash.IsPasswordValid(invalidPasswordBytes, hashingResult);

            var validPassword = "admin1";
            var validPasswordBytes = Encoding.UTF8.GetBytes(validPassword);
            var isValidPasswordValid = PasswordHash.IsPasswordValid(validPasswordBytes, hashingResult);

            Console.WriteLine($"Password: {password}");
            Console.WriteLine($"Password bytes: {Convert.ToBase64String(passwordBytes)}");

            Console.WriteLine($"Password hash 1: {Convert.ToBase64String(hashingResult.HashValue)}");
            Console.WriteLine($"Salt 1: {Convert.ToBase64String(hashingResult.Salt)}");

            Console.WriteLine($"Password hash 2: {Convert.ToBase64String(hashingResult2.HashValue)}");
            Console.WriteLine($"Salt 2: {Convert.ToBase64String(hashingResult2.Salt)}");

            Console.WriteLine($"Is 'admin2' valid?: {isInvalidPasswordValid}");
            Console.WriteLine($"Is 'admin1' valid?: {isValidPasswordValid}");
        }

        private static void ChecksumDemo()
        {
            var plaintext1 = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui.";
            var ptBytes1 = Encoding.UTF8.GetBytes(plaintext1);
            var hashValue1 = Checksum.CreateChecksumHash(ptBytes1);

            var plaintext2 = "Lorem ipsum dolor sit amet, consectetuer bdipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui.";
            var ptBytes2 = Encoding.UTF8.GetBytes(plaintext2);
            var hashValue2 = Checksum.CreateChecksumHash(ptBytes2);

            Console.WriteLine($"Hash value 1: {Convert.ToBase64String(hashValue1)}");
            Console.WriteLine($"Hash value 2: {Convert.ToBase64String(hashValue2)}");
        }

        private static void KeyDerivationDemo()
        {
            var password = "admin1";
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            var sw = new Stopwatch();

            sw.Start();
            var keyDeriviationResult = KeyDeriviation.CreateDerivedKey(passwordBytes, 128, 100000);
            sw.Stop();

            Console.WriteLine($"Derived key: {Convert.ToBase64String(keyDeriviationResult.DerivedKey)}");
            Console.WriteLine($"Elapsed time: {sw.ElapsedMilliseconds}ms");
        }

        private static void SymmetricCiphersDemo()
        {
            var plaintext = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit.";
            var ptBytes = Encoding.UTF8.GetBytes(plaintext);
            var key256 = RandomNumbers.GenerateRandomBytes(32);
            var encryptionResult = SymmetricCipher.Encrypt(ptBytes, key256);
            var encryptionResult2 = SymmetricCipher.Encrypt(ptBytes, key256);
            var encryptionResult3 = SymmetricCipher.Encrypt(ptBytes, key256);

            var decryptedData = SymmetricCipher.Decrypt(encryptionResult, key256);
            var decryptedText = Encoding.UTF8.GetString(decryptedData);

            Console.WriteLine($"Plaintext: {plaintext}");
            Console.WriteLine($"Plaintext bytes: {ptBytes}");
            Console.WriteLine($"Key: {Convert.ToBase64String(ptBytes)}");

            Console.WriteLine($"Ciphertext 1: {Convert.ToBase64String(encryptionResult.Ciphertext)}");
            Console.WriteLine($"IV 1: {Convert.ToBase64String(encryptionResult.IV)}");

            Console.WriteLine($"Ciphertext 2: {Convert.ToBase64String(encryptionResult2.Ciphertext)}");
            Console.WriteLine($"IV 2: {Convert.ToBase64String(encryptionResult2.IV)}");

            Console.WriteLine($"Ciphertext 3: {Convert.ToBase64String(encryptionResult3.Ciphertext)}");
            Console.WriteLine($"IV 3: {Convert.ToBase64String(encryptionResult3.IV)}");

            Console.WriteLine($"Decrypted text: {decryptedText}");
        }

        private static void CryptoStreamDemo()
        {
            var aesKey = RandomNumbers.GenerateRandomBytes(32);
            var iv = RandomNumbers.GenerateRandomBytes(16);

            CryptoStreamHelper.CompressEncryptFile("plaintext.txt", "output.bin", aesKey, iv);
            CryptoStreamHelper.DecryptDecompressFile("output.bin", "decrypted.txt", aesKey, iv);
        }

        private static void SecureQueryParamEncryptionDemo()
        {
            var message = "I heart cryptography";
            var key = RandomNumbers.GenerateRandomBytes(32);

            var encryptedQueryParam = GetEncryptedQueryParam(message, key);
            var decryptedMessage = DecrypteFromQueryParam(encryptedQueryParam, key);

            Console.WriteLine($"Original message: {message}");
            Console.WriteLine($"Encrypted query param: {encryptedQueryParam}");
            Console.WriteLine($"Decrypted message: {decryptedMessage}");
        }

        private static string GetEncryptedQueryParam(string message, byte[] key)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);

            var encryptionResult = SymmetricCipher.Encrypt(messageBytes, key);

            var dataToMac = encryptionResult.Ciphertext.Concat(encryptionResult.IV).ToArray();
            var hmac = HashedMAC.CreateHMAC(dataToMac, key);

            var dataToSend = new AuthenticatedEncryptionResult
            {
                EncryptionResult = encryptionResult,
                HMAC = hmac
            };

            var serializedData = JsonConvert.SerializeObject(dataToSend);
            var queryParam = System.Net.WebUtility.UrlEncode(serializedData);
            return queryParam;
        }

        private static string DecrypteFromQueryParam(string queryParam, byte[] key)
        {
            var decodedQueryParam = System.Net.WebUtility.UrlDecode(queryParam);
            var deserializedData = JsonConvert.DeserializeObject<AuthenticatedEncryptionResult>(decodedQueryParam);
            var dataToMac = deserializedData.EncryptionResult.Ciphertext.Concat(deserializedData.EncryptionResult.IV).ToArray();
            var hmac = HashedMAC.CreateHMAC(dataToMac, key);

            if (hmac.SequenceEqual(deserializedData.HMAC) == false)
                throw new InvalidOperationException("The message has been tampered with!!!");

            var decryptedMessageBytes = SymmetricCipher.Decrypt(deserializedData.EncryptionResult, key);
            var decryptedMessage = Encoding.UTF8.GetString(decryptedMessageBytes);
            return decryptedMessage;
        }

        class AuthenticatedEncryptionResult
        {
            public EncryptionResult EncryptionResult { get; set; }

            public byte[] HMAC { get; set; }
        }
    }
}
