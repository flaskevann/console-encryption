using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace ConsoleEncryption {

    // Wrapper class which takes care of all the heavy lifting
    public class Encryption
    {
        // bytes to string
        public static string BytesToString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);

            return new string(chars);
        }

        // string to bytes
        public static byte[] StringToBytes(string str)
        {
            byte[] bytes = null;

            bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }

        // Generate random AES key and IV
        public static string[] GenerateAESKeyAndIV(int length)
        {
            using (Rijndael myAes = Rijndael.Create())
            {
				myAes.KeySize = length;

                myAes.GenerateKey();
                myAes.GenerateIV();

                string[] keyAndIV = new string[2];
                keyAndIV[0] = Convert.ToBase64String(myAes.Key);
                keyAndIV[1] = Convert.ToBase64String(myAes.IV);

                return keyAndIV;
            }
        }

        // AES encrypt aesText
        public static string AesEncrypt(string text, string key, string IV) // base64 returned
        {
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(key);
                aesAlg.IV = Convert.FromBase64String(IV);

                // Create a decrytor to perform the stream transform
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes as base64 from the memory stream. 
            return Convert.ToBase64String(encrypted);
        }

        // Decrypt AES cipher
        public static string AesDecrypt(string base64cipher, string key, string IV)
        {
            string text = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(key);
                aesAlg.IV = Convert.FromBase64String(IV);

                // Create a decrytor to perform the stream transform
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption
                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(base64cipher)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            text = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return text;
        }

        // Generate random RSA keys
        public static string[] GenerateRSAKeys(int length) // 512, 1024, etc.
        {
            using (RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider(length))
            {
                // Find place for keys
                string[] keys = new string[2];

                // Get keys
                keys[0] = myRSA.ToXmlString(false);
                keys[1] = myRSA.ToXmlString(true);

                // base64 encode keys
                keys[0] = Convert.ToBase64String(StringToBytes(keys[0]));
                keys[1] = Convert.ToBase64String(StringToBytes(keys[1]));

                // Return keys
                return keys;
            }
        }

        // RSA encrypt aesText
        public static string RsaEncrypt(string text, string publicKey)
        {
            byte[] encryptedData;

            //Create a new instance of RSACryptoServiceProvider. 
            using (RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider())
            {
                // Import the RSA Key information
                myRSA.FromXmlString(BytesToString(Convert.FromBase64String(publicKey)));

                // Encrypt with specified OAEP padding
                encryptedData = myRSA.Encrypt(StringToBytes(text), true);
            }
            return Convert.ToBase64String(encryptedData);
        }

        // Decrypt RSA cipher
        public static string RsaDecrypt(string base64cipher, string privateKey)
        {
            byte[] decryptedData;

            // Create a new instance of RSACryptoServiceProvider
            using (RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider())
            {
                // Import the RSA Key information
                myRSA.FromXmlString(BytesToString(Convert.FromBase64String(privateKey)));

                // Decrypt the passed byte array and specify OAEP padding
                decryptedData = myRSA.Decrypt(Convert.FromBase64String(base64cipher), true);
            }

            return BytesToString(decryptedData);
        }
    }
}