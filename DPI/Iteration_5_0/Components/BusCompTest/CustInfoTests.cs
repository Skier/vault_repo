using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;


namespace DPI.ComponentsTests
{
	[TestFixture]
	public class CustInfoTests
	{
		/*		Data		*/
		int custInfoID;
		string custInfoType = "custInfoType";
		string status = "status";
		string lastName = "lastName";
		string firstName = "firstName";
		string birthday = "birthday";
		string email = "email";
		string contact = "contact";
		string contact2 = "contact2";
		string prevPhone = "prevPhone";
		string prevILEC = "prevILEC";
		int servAddID = 3;
		int mailAddID = 3;
        int accNumber = 5;
		string driversLicense = "1122789";
		DateTime dl_ExpDate = DateTime.Today;
		string ssn = "459264889";
		DateTime dob = DateTime.Today;

		/*		Constructors		*/
		public CustInfoTests()
		{
			// try { cleanup(); } 	catch {}
			// Console.WriteLine("Cleanup completed");
		}
        
		/*		Methods		*/
		public static void Main()
		{
			CustInfoTests test = new CustInfoTests();
            
			// UOW Tests
			test.addCustInfo();
			test.findCustInfo();
			test.saveCustInfo();
			test.findAllCustInfos();
            
			try
			{
				test.delCustInfo();
			}
			catch(ArgumentException ae)
			{
				Console.WriteLine("Expected exception: delCustInfo:" + ae.Message);
			}
			catch(Exception e)
			{
				Console.WriteLine("Error: delCustInfo: " + e.Message);
			}
            
		}
		[Test]
		public void addCustInfo()
		{
			UOW uow = new UOW();
			uow.Service = "addCustInfo";
			CustInfo cls = new CustInfo(uow);
            
			cls.CustInfoType = this.custInfoType;
			cls.Status = this.status;
			cls.LastName = this.lastName;
			cls.FirstName = this.firstName;
			cls.Birthday = this.birthday;
			cls.Email = this.email;
			cls.Contact = this.contact;
			cls.Contact2 = this.contact2;
			cls.PrevPhone = this.prevPhone;
			cls.PrevILEC = this.prevILEC;
			cls.ServAddID = this.servAddID;
			cls.MailAddID = this.mailAddID;
			cls.AccNumber = this.accNumber;
//			cls.DriversLicense = this.driversLicense;
//			cls.Dl_ExpDate = this.dl_ExpDate;
			cls.Ssn = this.ssn;
			cls.Dob = this.dob;
			//cls.State = "TX";
        
			uow.commit();
			this.custInfoID = cls.CustInfoID;
            
			uow = new UOW();
			uow.Service = "addCustInfo - assert";
			cls = CustInfo.find(uow, this.custInfoID);
			Assertion.Assert(cls.Status == this.status);
			Assertion.Assert(cls.AccNumber == this.accNumber);
			uow.close();
		}
		[Test]
		public void findCustInfo()
		{
			UOW uow = new UOW();
			uow.Service = "findCustInfo";
            
			CustInfo cls = CustInfo.find(uow, this.custInfoID);
			Assertion.Assert(cls.CustInfoID == this.custInfoID);
			uow.close();
		}
		[Test]
		public void saveCustInfo()
		{
			UOW uow = new UOW();
			uow.Service = "saveCustInfo";
			CustInfo cls = CustInfo.find(uow, this.custInfoID);
            
			cls.CustInfoType = this.custInfoType;
			cls.Status = this.status;
			cls.LastName = this.lastName;
			cls.FirstName = this.firstName;
			cls.Birthday = this.birthday;
			cls.Email = this.email;
			cls.Contact = this.contact;
			cls.Contact2 = this.contact2;
			cls.PrevPhone = this.prevPhone;
			cls.PrevILEC = this.prevILEC;
			cls.ServAddID = this.servAddID;
			cls.MailAddID = this.mailAddID;
			cls.AccNumber = this.accNumber;
//			cls.DriversLicense = this.driversLicense;
//			cls.Dl_ExpDate = this.dl_ExpDate;
			cls.Ssn = this.ssn;
			cls.Dob = this.dob;
			cls.CustInfoType += " saved";
			this.custInfoType = cls.CustInfoType;
                
			uow.commit();
            
			uow = new UOW();
			uow.Service = "saveCustInfo - assert";
            
			cls = CustInfo.find(uow, this.custInfoID);
			Assertion.Assert(cls.CustInfoType == this.custInfoType);
			uow.close();
		}
		[Test]
		public void findAllCustInfos()
		{
			UOW uow = new UOW();
			uow.Service = "findAllCustInfos";
			CustInfo[] objs = CustInfo.getAll(uow);
			Assertion.Assert(objs.Length > 0);
			uow.close();
		}
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void delCustInfo()
		{
			UOW uow = new UOW();
			uow.Service = "delCustInfo";
			CustInfo cls = CustInfo.find(uow, this.custInfoID);
			cls.delete();
            
			uow.commit();
            
			uow = new UOW();
			uow.Service = "delCustInfo - assert";
			cls = CustInfo.find(uow, this.custInfoID);
			Assertion.Assert((cls.CustInfoID == 0));
			uow.close();
		}
	}
}
