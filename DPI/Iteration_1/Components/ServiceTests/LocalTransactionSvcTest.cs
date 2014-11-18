using System;
using System.Collections;
using NUnit.Framework;
using DPI.Interfaces;
using DPI.Components;
using DPI.ClientComp;
using DPI.Services;

namespace DPI.Services
{
	[TestFixture]
	public class LocalTransactionSvcTest
	{
		IMap imap;		
		
		public LocalTransactionSvcTest()
		{
		}

		public static void Main()
		{
			LocalTransactionSvcTest test = new LocalTransactionSvcTest(); 

			test.GetVoidableTransactions();
		}

		[Test]
		public void GetVoidableTransactions()
		{
//			imap = new IdentityMap();
//			ILocalTransactionInfo[] localTransactionInfos;
//
//			localTransactionInfos = LocalTransactionSvc.GetVoidableTransactions(imap, "19993", Convert.ToDateTime("12/03/2004"));		
//
//
//			Assertion.Assert(localTransactionInfos.Length == 8);
		}

	}
}