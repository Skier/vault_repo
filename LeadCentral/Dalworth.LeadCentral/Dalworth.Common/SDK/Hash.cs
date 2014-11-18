using System;
using System.Security.Cryptography;
using System.Text;

namespace Dalworth.Common.SDK
{
    public class Hash
    {   
        
        public static string ComputeHash(string plainText)
        {            
            byte[] plainTextBytes = Encoding.UTF8.GetBytes("rb@78JG!#" + plainText + "W24^@gj6");
            HashAlgorithm hash = new SHA512Managed();
            byte[] hashBytes = hash.ComputeHash(plainTextBytes);
            return  Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyHash(string plainText, string hashValue)
        {
            byte[] hashBytes = Convert.FromBase64String(hashValue);
            const int hashSizeInBits = 512;
            const int hashSizeInBytes = hashSizeInBits / 8;

            if (hashBytes.Length < hashSizeInBytes)
                return false;

            string expectedHashString =
                        ComputeHash(plainText);

            return (hashValue == expectedHashString);
        }
    }
}