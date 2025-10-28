using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;

namespace melkikerapostgrescrud.Utils
{
    public class AesEncryption
    {
        public static string EncryptStringToBytes_Aes(string plainText, string key)
        {
            // Create a new instance of the AES encryption algorithm
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = new byte[16]; // Initialization vector (IV)
                                       // Create an encryptor to perform the stream transform
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                // Create the streams used for encryption
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create a CryptoStream using the encryptor
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                    }
                    // Store the encrypted data in the public static byte array
                    byte[] encryptedData = ms.ToArray();
                    return Convert.ToBase64String(encryptedData);
                }
            }
        }

        public static string DecryptStringFromBytes_Aes(string ciphertext, byte[] cipherText, string key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = new byte[16]; // Initialization vector (IV)
                                       // Create a decryptor to perform the stream transform
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                // Create the streams used for decryption
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(ciphertext)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }

}
