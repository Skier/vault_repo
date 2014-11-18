using System;
using System.IO;
using System.Security.Cryptography;

using System.Collections;
using DPI.Interfaces;

namespace DPI.Components
{	
	public class Crypto 
	{
		static string tempFile = @"c:\temp\encrypted";

		public Crypto()
		{
			//			string s = "This is a test";
			//			Encrypt(s);
			//			string res = Decrypt();
		
		}
		public static int Scramble(string message)
		{
			return Encrypt(message, new Random().Next(1, int.MaxValue));
		}
		public static string Unscramble(int id)
		{
			return Read(GetTdes(id), id);
		}

		/*		Implementation		*/
		static int Encrypt(string clearText, int id)
		{
			FileStream fs = File.Create(tempFile + id.ToString() + ".txt");
			TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider(); 
			CryptoStream cs = new CryptoStream(fs, tdes.CreateEncryptor(), CryptoStreamMode.Write);
			
			WriteData(cs, clearText);
			WriteKey(tdes, id);

			return id;
		}
		static void WriteKey(TripleDESCryptoServiceProvider tdes, int id)
		{
			FileStream fsKey = File.Create(tempFile + id.ToString() + ".key");
			BinaryWriter bw = new BinaryWriter(fsKey);
			
			bw.Write(tdes.Key);
			bw.Write(tdes.IV);
			
			bw.Flush();
			bw.Close();
		}
		static void WriteData(CryptoStream cs, string clearText)
		{
			StreamWriter sw = new StreamWriter(cs);
			
			sw.Write(clearText);
			sw.Flush();
			sw.Close();
		}

		static TripleDESCryptoServiceProvider GetTdes(int id)
		{
			TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider(); 
			FileStream fsKey = File.OpenRead(tempFile + id.ToString() + ".key");
			BinaryReader br = new BinaryReader(fsKey);
			tdes.Key = br.ReadBytes(24);
			tdes.IV  = br.ReadBytes(8);
			return tdes;
		}
		static string Read(TripleDESCryptoServiceProvider tdes, int id)
		{
			FileStream fs = File.OpenRead(tempFile + id.ToString() + ".txt");
			CryptoStream cs = new CryptoStream(fs, tdes.CreateDecryptor(), CryptoStreamMode.Read);
			StreamReader sr = new StreamReader(cs);
			string res = sr.ReadToEnd();
			sr.Close();
			return res;
		}
	}
}