using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace folder_Protector
{
    public static class EncryptionHelper
    {
        private static readonly byte[] Key = new byte[32] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 };
        private static readonly byte[] IV = new byte[16] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

        public static string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor=aesAlg.CreateEncryptor(aesAlg.Key,aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using(CryptoStream csEncrypt=new CryptoStream(msEncrypt,encryptor,CryptoStreamMode.Write))
                    using(StreamWriter swEncrypt=new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }

                    
                    
            }
        }

        public static string Decrypt(string cipherText)
        {
            using(Aes aesAlg=Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor=aesAlg.CreateDecryptor(aesAlg.Key,aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                using(CryptoStream csDecrypt=new CryptoStream(msDecrypt,decryptor,CryptoStreamMode.Read))
                using(StreamReader srDecrypt=new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();  
                }


            }
        }

    }
}
