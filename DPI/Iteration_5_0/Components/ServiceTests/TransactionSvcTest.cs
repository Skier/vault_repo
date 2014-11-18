using System;
using NUnit.Framework;
using DPI.Interfaces;
using DPI.Components;
using DPI.Services;

namespace DPI.Services
{
	[TestFixture]
	public class TransactionSvcTest
	{
		/*		Data		*/
		IMap imap;		
		/*		Methods		*/
		public static void Main()
		{
			TransactionSvcTest test = new TransactionSvcTest(); 
			test.VoidTransaction();
			Console.Read();
		}
		[Test]
		public void VoidTransaction()
		{						
			imap = new IdentityMap();
			int transaction_id = 2679240; 
			bool result = TransactionSvc.VoidTransaction(imap, transaction_id);			
			Assertion.Assert(result == true);								
		}
	}	
}
