using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.ComponentsTests
{
	[TestFixture]	
	public class YonexNewTranTests
	{
		public YonexNewTranTests()
		{
			UOW uow = new UOW();
		}
		public static void Main()
		{	
			YonexNewTranTests test = new YonexNewTranTests();
			try
			{		
				test.WriteTransaction();
			//	test.submitNewOrder2();				
			}			
			catch(Exception e)
			{
				Console.WriteLine("Error: delPayInfo: " + e.Message);
			} 			         
		}
//		[Test]
//		public void submitNewOrder2()
//		{
//			UOW uow = null;			
//			uow = new UOW();
//			PayInfo pi = PayInfo.find(uow, 83);
//			IDemand dmd = Demand.find(uow, pi.DmdId);		
//			IReceipt recpt = YonixNewTran.SubmitNewOrder2(uow, OrderType.Add, dmd);
//			
//			int i = recpt.AccNumber;
//			string j = recpt.ConfNum;
//			int k = recpt.Demand;
//		}
		[Test]
		public void WriteTransaction()
		{
			UOW uow = null;			
			uow = new UOW();
			PayInfo pi = PayInfo.find(uow, 83);
			pi.Status = PaymentStatus.Paid.ToString();
			IDemand dmd = Demand.find(uow, pi.DmdId);
			IAcctNotes notes = Notes.find(uow, pi.DmdId);

//			IReceipt recpt = YonixNewTran.WriteTransaction(uow, dmd, 
//				dmd.Ilec, 
//				dmd.StoreCode.ToString(), 
//				dmd.ConsumerAgent.ToString(), 
//				"83", 
//				pi, 
//				pi.Id.ToString(), 
//				notes);
		}
	}
}
