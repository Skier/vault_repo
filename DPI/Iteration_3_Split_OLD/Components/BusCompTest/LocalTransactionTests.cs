using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class LocalTransactionTests
	{
		/*		Data		*/
		int transaction_Id;
		string trConfirm = "trConfirm";
		string trLDConfirm = "trLDConfirm";
		string trNumber = "trNumber";
		int accNumber = 5;
		string phNumber = "phNumber";
		DateTime payDate = DateTime.Now;
		string payTime = "payTime";
		decimal localAmount = 8.5M;
		decimal lDAmount = 9.5M;
		decimal comAmount = 10.5M;
		string clerkID = "clerkID";
		int transaction_Type_ID = 1;
		int transaction_Method_ID = 1;
		string storeCode = "TXCT0002OF";
		string aNI = "aNI";
        
		/*		Constructors		*/
		public LocalTransactionTests()
		{
			// try { cleanup(); } 	catch {}
			// Console.WriteLine("Cleanup completed");
		}
        
		/*		Methods		*/
		public static void Main()
		{
			LocalTransactionTests test = new LocalTransactionTests();
            
			// UOW Tests
			test.addLocalTransaction();
			test.findLocalTransaction();
			test.saveLocalTransaction();
			test.findAllLocalTransactions();
            
			try
			{
				test.delLocalTransaction();
			}
			catch(ArgumentException ae)
			{
				Console.WriteLine("Expected exception: delLocalTransaction:" + ae.Message);
			}
			catch(Exception e)
			{
				Console.WriteLine("Error: delLocalTransaction: " + e.Message);
			}
            
		}
		[Test]
		public void addLocalTransaction()
		{
			UOW uow = new UOW();
			uow.Service = "addLocalTransaction";
			LocalTransaction cls = new LocalTransaction(uow);
            
			cls.TrConfirm = this.trConfirm;
			cls.TrLDConfirm = this.trLDConfirm;
			cls.TrNumber = this.trNumber;
			cls.AccNumber = this.accNumber;
			cls.PhNumber = this.phNumber;
			cls.PayDate = this.payDate;
			cls.PayTime = this.payTime;
			cls.LocalAmount = this.localAmount;
			cls.LDAmount = this.lDAmount;
			cls.ComAmount = this.comAmount;
			cls.ClerkID = this.clerkID;
			cls.Transaction_Type_ID = this.transaction_Type_ID;
			cls.Transaction_Method_ID = this.transaction_Method_ID;
			cls.StoreCode = this.storeCode;
			cls.ANI = this.aNI;
        
			uow.commit();
			this.transaction_Id = cls.Transaction_Id;
            
			uow = new UOW();
			uow.Service = "addLocalTransaction - assert";
			cls = LocalTransaction.find(uow, this.transaction_Id);
			Assertion.Assert(cls.TrLDConfirm == this.trLDConfirm);
			uow.close();
		}
		[Test]
		public void findLocalTransaction()
		{
			UOW uow = new UOW();
			uow.Service = "findLocalTransaction";
            
			LocalTransaction cls = LocalTransaction.find(uow, this.transaction_Id);			
			Assertion.Assert(cls.Transaction_Id == this.transaction_Id);
			uow.close();
		}
		[Test]
		public void saveLocalTransaction()
		{
			UOW uow = new UOW();
			uow.Service = "saveLocalTransaction";
			LocalTransaction cls = LocalTransaction.find(uow, this.transaction_Id);
            
			cls.TrConfirm = this.trConfirm;
			cls.TrLDConfirm = this.trLDConfirm;
			cls.TrNumber = this.trNumber;
			cls.AccNumber = this.accNumber;
			cls.PhNumber = this.phNumber;
			cls.PayDate = this.payDate;
			cls.PayTime = this.payTime;
			cls.LocalAmount = this.localAmount;
			cls.LDAmount = this.lDAmount;
			cls.ComAmount = this.comAmount;
			cls.ClerkID = this.clerkID;
			cls.Transaction_Type_ID = this.transaction_Type_ID;
			cls.Transaction_Method_ID = this.transaction_Method_ID;
			cls.StoreCode = this.storeCode;
			cls.ANI = this.aNI;
			cls.TrConfirm += " saved";
			this.trConfirm = cls.TrConfirm;
                
			uow.commit();
            
			uow = new UOW();
			uow.Service = "saveLocalTransaction - assert";
            
			cls = LocalTransaction.find(uow, this.transaction_Id);
			Assertion.Assert(cls.TrConfirm == this.trConfirm);
			uow.close();
		}
		[Test]
		public void findAllLocalTransactions()
		{
			UOW uow = new UOW();
			uow.Service = "findAllLocalTransactions";
			LocalTransaction[] objs = LocalTransaction.getAll(uow);
			Assertion.Assert(objs.Length > 0);
			uow.close();
		}
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void delLocalTransaction()
		{
			UOW uow = new UOW();
			uow.Service = "delLocalTransaction";
			LocalTransaction cls = LocalTransaction.find(uow, this.transaction_Id);
			cls.delete();
            
			uow.commit();
            
			uow = new UOW();
			uow.Service = "delLocalTransaction - assert";
			cls = LocalTransaction.find(uow, this.transaction_Id);
			Assertion.Assert((cls.Transaction_Id == 0));
			uow.close();
		}
	}
}
