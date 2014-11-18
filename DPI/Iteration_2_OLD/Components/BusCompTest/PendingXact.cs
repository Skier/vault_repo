using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;



namespace DPI.ComponentsTests
{
	[TestFixture]
	public class PendingXactTests
	{
		IMap imap;
		UOW uow;	
		int payInfo = 61;
		Random random = new Random();
		
		public PendingXactTests()
		{
			imap = new IdentityMap();
			
	
		}

		public static void Main()
		{	
			PendingXactTests test = new PendingXactTests();
			try
			{	
			//	test.SubmitOrderTest();
				test.ConfirmPayment();				
			}			
			catch(Exception e)
			{
				Console.WriteLine("Error: delPayInfo: " + e.Message);
			}            
		}

		[Test]
		public void ConfirmPayment()
		{			
			try
			{
				uow = new UOW(imap);
				
				IReceipt xactRct = PendingXact.ConfirmPendPayment(uow, random.Next(0, 200000).ToString(), payInfo);
				uow.commit();
			}
			finally
			{
				uow.close();
			}
			Console.WriteLine("New Payment submitted");
			Console.ReadLine();
		}

		[Test]
		public void SubmitOrderTest()
		{
				uow = new UOW(imap);

			IReceipt orderRct = PendingXact.SubmitOrder(uow, payInfo);	
			Console.WriteLine("New order submitted");
			uow.commit();
		}
	}
}

