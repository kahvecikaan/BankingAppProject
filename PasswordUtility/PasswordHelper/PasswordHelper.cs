using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordUtility
{
    public class PasswordHelper
    {
        public static string HashPassword(string plainTextPassword)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainTextPassword));
                StringBuilder builder = new StringBuilder();
                for(int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // the byte should be represented by a hexadecimal string of 2 char in length  
                }
                return builder.ToString();
            }
        }
    }
}
