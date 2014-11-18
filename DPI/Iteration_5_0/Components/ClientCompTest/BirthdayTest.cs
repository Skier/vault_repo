using System;
using DPI.ClientComp;
using DPI.Interfaces;
using NUnit.Framework;

namespace DPI.ClientCompTest
{
	[TestFixture]
	public class BirthdayTest
	{
		string[] bdays = { "1/1/56", "01/01/56", "011/01/1956"};
		string[] good  = { "01/01/1956", "02/17" };

		/*		Methods		*/
		public static void Main()
		{
			BirthdayTest test = new BirthdayTest();
            
			test.Validate();
			test.Validate2();
			test.ShowEmpty();
			test.Show();
			//test.Store();
		}
		[Test]
		public void Validate()
		{
			for (int i = 0; i < bdays.Length; i++)
				Assertion.Assert(!Birthday.IsValid(bdays[i]));
		}
		[Test]
		public void Validate2()
		{
			for (int i = 0; i < good.Length; i++)
				Assertion.Assert(Birthday.IsValid(good[i]));
		}
		[Test]
		public void ShowEmpty()
		{
			for (int i = 0; i < bdays.Length; i++)
				Assertion.Assert(Birthday.Show(bdays[i]).Length == 0);
		}
		[Test]
		public void Show()
		{
			for (int i = 0; i < good.Length; i++)
				Assertion.Assert(Birthday.Show(good[i]).Length > 0);
		}
/*		[Test]
		public void Store()
		{
			for (int i = 0; i < bdays.Length; i++)
				Assertion.Assert(Birthday.Store(bdays[i]) == @"__/__/____");
		}
*/
	}
}