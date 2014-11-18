using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace MobileTech
{
    public class Crypto
	{
		// Private Variables for this class
		private static SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();
		private static byte[] cryptKey = {168,248,1,245,231,218,43,153,179,132,142,61,
		                           85,166,86,230,224,65,119,231,91,156,163,174};
		private static byte[] cryptIV = {147,8,99,15,35,33,157,170};
        //void GenerateKey()
        //{
        //    //mCSP = SetEnc();

        //    //mCSP.GenerateKey();

        //    //cryptKey = Convert.ToBase64String(mCSP.Key);
        //}
        //void GenerateIV()
        //{
        //    //mCSP.GenerateIV();

        //    //cryptIV = Convert.ToBase64String(mCSP.IV);
        //}
        static public string EncryptString(string Value)
        {
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;

            ct = mCSP.CreateEncryptor(cryptKey, cryptIV);

            byt = Encoding.UTF8.GetBytes(Value);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            return Convert.ToBase64String(ms.ToArray(),0,ms.ToArray().Length);
        }

        static public string DecryptString(string Value)
        {
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;

            ct = mCSP.CreateDecryptor(cryptKey, cryptIV);

            byt = Convert.FromBase64String(Value);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            return Encoding.UTF8.GetString(ms.ToArray(), 0, ms.ToArray().Length);
        }
	}
}
