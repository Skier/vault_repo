using System;
using NUnit.Framework;
using DPI.Interfaces;
using DPI.Components;
using DPI.Services;

namespace DPI.Services
{
	[TestFixture]
	public class StoreSvcTest
	{
		/*		Data		*/
		IMap imap;		
		
		/*		Constructors	*/
		public StoreSvcTest()
		{
		}
		/*		Methods		*/
		public static void Main()
		{
			StoreSvcTest test = new StoreSvcTest(); 
			test.GetVoidableTransactions();
			Console.Read();
		}
		[Test]
		public void GetVoidableTransactions()
		{			
			imap = new IdentityMap();			
			string storecode = "NCRW1903RW";
			DateTime paydate = DateTime.Parse("4/20/2004");
			int transaction_type_id = 1; //Monthly Payment

			IVoidableTransactionDto dto = StoreSvc.GetVoidableTransactions(imap, storecode, paydate, transaction_type_id);
			Assertion.Assert(dto.VoidableTransactions.Length > 0);					
		}
	}	
}
