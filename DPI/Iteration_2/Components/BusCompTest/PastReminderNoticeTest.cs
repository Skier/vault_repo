using System;
using NUnit.Framework;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class PastReminderNoticeTest
	{
		public static void Main()
		{
			PastReminderNoticeTest t = new PastReminderNoticeTest();
		//	t.GetLastPastReminderNoticeFilename();
			t.GetPastReminderNotices();
		}
//
//   Test bellow does not compile.
//

/*		[Test]
		public void GetLastPastReminderNoticeFilename()
		{	
			UOW uow = new UOW();
			int accNumber = 30103374;

			try 
			{
				string filename = PastReminderNotice.GetLastPastReminderNoticeFilename(uow, accNumber);				
				Assertion.Assert(filename == @"\\iasapp01\web\customer_bills\view\" + 
					"30103374_08032004_6624558250.pdf");	
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				uow.close();
			}
		}
*/
		[Test]
		public void GetPastReminderNotices()
		{	
			UOW uow = new UOW();
			int accNumber = 50312985;

			try 
			{
				PastReminderNotice[] prns = PastReminderNotice.GetPastReminderNotices(uow, accNumber);				
				Assertion.Assert(prns.Length == 11);	
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				uow.close();
			}
		}
	}
}