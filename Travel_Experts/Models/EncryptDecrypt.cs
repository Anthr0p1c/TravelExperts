//Password encryption done following an online video
//Code courtesy - Encrypt and Decrypt Data with C# E-learning portal - Digital Knack dt.Aug 19,2017
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Travel_Experts.Models
{
    public class EncryptDecrypt
    {
        //Encryption algorithm for password
        
        public static string Encrypt(string nePassword)
        {
            string EncryptionKey = "NYZXrenweoiTYBupqi12987bvzmxcbnlkjkljjkSHDAGFASG()&^am,.";
            byte[] inputBytes = Encoding.Unicode.GetBytes(nePassword);
            string ePassword; //stores encrypted password
            using (Aes encrypt = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x51, 0x75, 0x61, 0x6e, 0x23, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x81 });
                encrypt.Key = pdb.GetBytes(32);
                encrypt.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encrypt.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputBytes, 0, inputBytes.Length);
                        cs.Close();
                    }
                    ePassword = Convert.ToBase64String(ms.ToArray());//store encrypted password
                }
            }
            return ePassword; //returns encrypted password
        }

        //Decryption alogrithm for password
        public static string Decrypt(string ePassword)
        {
            string EncryptionKey = "NYZXrenweoiTYBupqi12987bvzmxcbnlkjkljjkSHDAGFASG()&^am,."; 
            ePassword = ePassword.Replace(" ", "+");
            string nePassword;
            
                byte[] encryptBytes = Convert.FromBase64String(ePassword);
                using (Aes encrypt = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x51, 0x75, 0x61, 0x6e, 0x23, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x81 });
                    encrypt.Key = pdb.GetBytes(32);
                    encrypt.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encrypt.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(encryptBytes, 0, encryptBytes.Length);
                            cs.Close();
                        }
                        nePassword = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
 
            return nePassword;
        }
    }
}
