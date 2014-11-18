using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class PayInfoTests
	{
		/*		Data		*/
		int id;
		Demand dmd;
		DateTime pymtDate = DateTime.Now;
		decimal localDue = 3.5M;
		decimal localPaid = 4.5M;
		decimal lDamount = 5.5M;
		decimal amtTendered = 6.5M;
		int pymtType = 8;
		string confNumber = "confNumber";
		string status = "status";
        
		/*		Constructors		*/
		public PayInfoTests()
		{
			// try { cleanup(); } 	catch {}
			// Console.WriteLine("Cleanup completed");
			UOW uow = new UOW();
			dmd = new Demand(uow, DemandType.New.ToString());
			uow.commit();
		}
        
		/*		Methods		*/
		public static void Main()
		{
			PayInfoTests test = new PayInfoTests();
            
			// UOW Tests
//			test.addPayInfo();
//			test.findPayInfo();
//			test.savePayInfo();
//			test.findAllPayInfos();
//            test.findDmdPayInfo();
			test.getSumPayInfo();
			try
			{
				test.delPayInfo();
			}
			catch(ArgumentException ae)
			{
				Console.WriteLine("Expected exception: delPayInfo:" + ae.Message);
			}
			catch(Exception e)
			{
				Console.WriteLine("Error: delPayInfo: " + e.Message);
			}
            
		}
		[Test]
		public void addPayInfo()
		{
			UOW uow = new UOW();
			uow.Service = "addPayInfo";
			IPayInfo cls = PayInfo.GetPayInfo(uow, PayInfoClass.PayInfoLocal.ToString());
            
			cls.ParDemand = this.dmd;
			cls.PayDate = this.pymtDate;
			
			cls.TotalAmountDue = this.localDue;
			cls.TotalAmountPaid = this.localPaid;
			cls.AmountTendered = this.amtTendered;
			cls.PaymentType = (PaymentType)this.pymtType;
			cls.ConfNumber = this.confNumber;
			cls.Status = this.status;
        
			uow.commit();
			this.id = cls.Id;
            
			uow = new UOW();
			uow.Service = "addPayInfo - assert";
			cls = PayInfo.find(uow, this.id);
			Assertion.Assert(cls.ParDemand.Id == this.dmd.Id);
			uow.close();
		}
		[Test]
		public void findPayInfo()
		{
			UOW uow = new UOW();
			uow.Service = "findPayInfo";
            
			PayInfo cls = PayInfo.find(uow, this.id);
			Assertion.Assert(cls.Id == this.id);
			uow.close();
		}
		[Test]
		public void findDmdPayInfo()
		{
			UOW uow = new UOW();
			uow.Service = "findDmdPayInfo";
            
			PayInfo[] pis = PayInfo.getDmdPayInfo(uow, this.dmd.Id);

			for (int i = 0; i < pis.Length; i++)
				Assertion.Assert(pis[i].ParDemand.Id == this.dmd.Id);
			uow.close();
		}
		[Test]
		public void savePayInfo()
		{
			UOW uow = new UOW();
			uow.Service = "savePayInfo";
			PayInfo cls = PayInfo.find(uow, this.id);
            Demand dmd2 = new Demand(uow,  DemandType.New.ToString());

			cls.ParDemand = dmd2;
			cls.PayDate = this.pymtDate;
			cls.LocalAmountDue = this.localDue;
			cls.LocalAmountPaid = this.localPaid;
			cls.LdAmount = this.lDamount;
			cls.AmountTendered = this.amtTendered;
			cls.ConfNumber = this.confNumber;
			cls.Status = this.status;
			this.localPaid += 2m;
			cls.LocalAmountPaid = this.localPaid;
                
			//uow.commit();
            
			//uow = new UOW();
			uow.Service = "savePayInfo - assert";
            
			cls = PayInfo.find(uow, this.id);
			Assertion.Assert(cls.ParDemand.Id == dmd2.Id);
			uow.close();
		}
		[Test]
		public void findAllPayInfos()
		{
			UOW uow = new UOW();
			uow.Service = "findAllPayInfos";
			PayInfo[] objs = PayInfo.getAll(uow);
			Assertion.Assert(objs.Length > 0);
			uow.close();
		}
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void delPayInfo()
		{
			UOW uow = new UOW();
			uow.Service = "delPayInfo";
			PayInfo cls = PayInfo.find(uow, this.id);
			cls.delete();
            
			uow.commit();
            
			uow = new UOW();
			uow.Service = "delPayInfo - assert";
			cls = PayInfo.find(uow, this.id);
			Assertion.Assert((cls.Id == 0));
			uow.close();
		}
		[Test]
		public void getSumPayInfo()
		{
			UOW uow = new UOW();
			uow.Service = "findDmdPayInfo";
            
			PayInfo[] pis = PayInfo.getDmdPayInfo(uow, 388);

			PayInfo pi = PayInfo.Sum(pis, PaymentStatus.Cancelled);
			Assertion.Assert(pi.LocalAmountPaid > 0);
			
			uow.close();
		}
	}
}