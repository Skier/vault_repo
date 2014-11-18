using System;
using System.IO;
using System.Security.Cryptography;

using NUnit.Framework;
using DPI.Interfaces;

namespace DPI.Components
{
	
	[TestFixture]
	public class ErrorDtoTest
	{
		string tempFile = @"c:\temp\encrypted";

		public ErrorDtoTest()
		{
		}

		public static void Main()
		{
			ErrorDtoTest t = new ErrorDtoTest();

			t.CryptoTest();

			t.TestNoError();
			t.TestAddError();
			t.TestMultipleErrors();
		}
		[Test]
		public void CryptoTest()
		{
			string s = "This is a test";
			Encrypt(s);
			string res = Decrypt();
			
			Assertion.Assert(s == res);

		}
		void Encrypt(string clearText)
		{
			FileStream fs = File.Create(tempFile + ".txt");
			TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider(); 
			CryptoStream cs = new CryptoStream(fs, tdes.CreateEncryptor(), CryptoStreamMode.Write);
			
			StreamWriter sw = new StreamWriter(cs);
			sw.Write(clearText);
			sw.Flush();
			sw.Close();

			FileStream fsKey = File.Create(tempFile + ".key");
			BinaryWriter bw = new BinaryWriter(fsKey);
			bw.Write(tdes.Key);
			bw.Write(tdes.IV);
			bw.Flush();
			bw.Close();
		}
		string Decrypt()
		{
			TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider(); 
            FileStream fsKey = File.OpenRead(tempFile + ".key");
			BinaryReader br = new BinaryReader(fsKey);
            tdes.Key = br.ReadBytes(24);
			tdes.IV  = br.ReadBytes(8);

			FileStream fs = File.OpenRead(tempFile + ".txt");
			CryptoStream cs = new CryptoStream(fs, tdes.CreateDecryptor(), CryptoStreamMode.Read);
			StreamReader sr = new StreamReader(cs);
			string res = sr.ReadToEnd();
			sr.Close();
			return res;
		}
		[Test]
		public void TestNoError()
		{
			IErrorDto d=new ErrorDto();
			Assertion.Assert(!d.IsError);
		}

		[Test]
		public void TestAddError()
		{
			IErrorDto d=new ErrorDto();
			d.AddError(123, "Sample Error 1");
			Assertion.Assert(d.IsError);
			Console.WriteLine("(TestAddError) Length = " + d.ErrorInfo.GetLength(0));
			Assertion.AssertEquals("Wrong error returned", "Sample Error 1", d.ErrorInfo[0].Message);
			Assertion.Assert(d.ErrorInfo[0].Number==123);
		}

		[Test]
		public void TestMultipleErrors()
		{
			IErrorDto d=new ErrorDto();
			d.AddError("Sample Error 1");
			d.AddError(321, "Sample Error 2");

			Assertion.Assert(d.ErrorInfo.GetLength(0)==2);
			Assertion.AssertEquals("Wrong error returned", "Sample Error 1", d.ErrorInfo[0].Message);
			Assertion.AssertEquals("Wrong error returned", "Sample Error 2", d.ErrorInfo[1].Message);
		}
	}
}