using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace ConsoleEncryption {

    public class Program
    {
        public static void Main(string[] args)
        {
            bool invalidInput = false;

            switch (args.Length)
            {
                // Test command
                case 1:
                    if (args[0] == "test")
                    {
                        Tests();
                    }
                    else
                    {
                        invalidInput = true;
                    }
                    break;

                // Key (and IV) generating
                case 2:

                    switch (args[0])
                    {
                        case "AES":

                            string[] newKeyAndIV = null;

                            newKeyAndIV = Encryption.GenerateAESKeyAndIV( int.Parse(args[1]) );
                            Console.WriteLine(newKeyAndIV[0]);
                            Console.WriteLine(newKeyAndIV[1]);

                            break;

                        case "RSA":

                            string[] newKeys = null;

                            newKeys = Encryption.GenerateRSAKeys( int.Parse(args[1]) );
                            Console.WriteLine(newKeys[0]);
                            Console.WriteLine(newKeys[1]);

                            break;

                        default:
                            invalidInput = true;
                            break;
                    }
                    break;

                // RSA encryption or decryption
                case 4:
                    
                    if (args[0] == "RSA")
                    {
                        switch (args[1])
                        {
                            case "encrypt":
                                string cipher = Encryption.RsaEncrypt(args[2], args[3]);
                                Console.WriteLine(cipher);
                                break;

                            case "decrypt":
                                string plain = Encryption.RsaDecrypt(args[2], args[3]);
                                Console.WriteLine(plain);
                                break;

                            default:
                                invalidInput = true;
                                break;
                        }
                    }
                    else
                    {
                        invalidInput = true;
                    }
                    break;

                // AES encryption or decryption
                case 5:
                    
                    if (args[0] == "AES")
                    {
                        switch (args[1])
                        {
                            case "encrypt":
                                string cipher = Encryption.AesEncrypt(args[2], args[3], args[4]);
                                Console.WriteLine(cipher);
                                break;

                            case "decrypt":
                                string plain = Encryption.AesDecrypt(args[2], args[3], args[4]);
                                Console.WriteLine(plain);
                                break;

                            default:
                                invalidInput = true;
                                break;
                        }
                    }
                    else
                    {
                        invalidInput = true;
                    }
                    break;

                default:
                    invalidInput = true;
                    break;
            }

            // Invalid input so show all the different uses
            if (invalidInput)
            {
                Console.WriteLine("");
                Console.WriteLine("Console Encryption generates keys, encrypts and decrypts text.");
                Console.WriteLine("");
                Console.WriteLine("AES uses:");
                Console.WriteLine("==============================================================================");
                Console.WriteLine("'ce.exe AES 128' generates a 128 key and 128 IV for you.");
                Console.WriteLine("'ce.exe AES 192' generates a 192 key and 128 IV for you.");
                Console.WriteLine("'ce.exe AES 256' generates a 256 key and 128 IV for you.");
                Console.WriteLine("'ce.exe AES encrypt \"your text\" key iv' encrypts text for you.");
                Console.WriteLine("'ce.exe AES decrypt cipher key iv' decrypts cipher for you.");
                Console.WriteLine();
                Console.WriteLine("Some RSA uses:");
                Console.WriteLine("==============================================================================");
                Console.WriteLine("'ce.exe RSA 1024' generates a 1024 public key and private key for you.");
                Console.WriteLine("'ce.exe RSA 2048' generates a 2048 public key and private key for you.");
                Console.WriteLine("'ce.exe RSA encrypt \"your text\" public_key' encrypts text for you.");
                Console.WriteLine("'ce.exe RSA decrypt cipher private_key' decrypts cipher for you.");
                Console.WriteLine("");
                Console.WriteLine("Skeptic?");
                Console.WriteLine("==============================================================================");
                Console.WriteLine("'ce.exe test' runs tests of all uses listed above!");
            }
        }

        // Encryption tests with result printed to console
        public static void Tests()
        {
            Console.WriteLine("");
            Console.WriteLine("Encryption.exe tests:");
            Console.WriteLine("==============================================================================");

            /*
             *  TEST AES:
             */

            string aesText = "AES encrypted text, øæå!";

            int[] aesLengths = new int[3] {128, 192, 256};
            for (int i = 0; i < aesLengths.Length; i++) {

                string aesEncrypted = null;
                string aesDecrypted = null;
                string[] newAESKeyAndIV = Encryption.GenerateAESKeyAndIV( aesLengths[i] );

                Console.WriteLine();
                Console.WriteLine("AES-" + aesLengths[i] + ":\n=================================================");
                Console.WriteLine("Key: " + newAESKeyAndIV[0]);
                Console.WriteLine("IV: " + newAESKeyAndIV[1]);
                aesEncrypted = Encryption.AesEncrypt(aesText, newAESKeyAndIV[0], newAESKeyAndIV[1] );
                aesDecrypted = Encryption.AesDecrypt(aesEncrypted, newAESKeyAndIV[0], newAESKeyAndIV[1] );
                Console.WriteLine("Text: " + aesText);
                Console.WriteLine("Encrypted: " + aesEncrypted);
                Console.WriteLine("Decrypted: " + aesDecrypted);
            }

            /*
             *  TEST RSA:
             */
            
            string rsaText = "RSA encrypted text, øæå!";

            int[] rsaLengths = new int[2] {1024, 2048};
            for (int i = 0; i < rsaLengths.Length; i++) {

                string rsaEncrypted = null;
                string rsaDecrypted = null;
                string[] newRSAKeys = Encryption.GenerateRSAKeys( rsaLengths[i] );

                Console.WriteLine();
                Console.WriteLine("RSA-" + rsaLengths[i] + ":\n=================================================");
                Console.WriteLine("Public key: " + newRSAKeys[0]);
                Console.WriteLine("Private key: " + newRSAKeys[1]);
                rsaEncrypted = Encryption.RsaEncrypt(rsaText, newRSAKeys[0]);
                rsaDecrypted = Encryption.RsaDecrypt(rsaEncrypted, newRSAKeys[1]);
                Console.WriteLine("Text: " + rsaText);
                Console.WriteLine("Encrypted: " + rsaEncrypted);
                Console.WriteLine("Decrypted: " + rsaDecrypted);
            }
        }
    }
}
