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
	public class EnergyEnrollmentTests
	{
		/*		Data		*/
		int iD;
		int accountNumber = 2;
		string accountName = "accountName";
		string serviceProviderName = "serviceProviderName";
		string serviceProviderPhone = "serviceProviderPhone";
		decimal initPrepayAmt = 5.5M;
		int customer = 7;
		string address1 = "address1";
		string address2 = "address2";
		string city = "city";
		string state = "state";
		string zip = "zip";
		string zip4 = "zip4";
		DateTime startDate = DateTime.Now;
		DateTime enrollDate = DateTime.Now;
		DateTime modifyDate = DateTime.Now;
		string status = "status";
        
		/*		Constructors		*/
		public EnergyEnrollmentTests()
		{
			// try { cleanup(); } 	catch {}
			// Console.WriteLine("Cleanup completed");
		}
        
		/*		Methods		*/
		public static void Main()
		{
			EnergyEnrollmentTests test = new EnergyEnrollmentTests();
            
			// UOW Tests
			test.addEnergyEnrollment();
			test.findEnergyEnrollment();
			test.saveEnergyEnrollment();
			test.findAllEnergyEnrollments();
            
			try
			{
				test.delEnergyEnrollment();
			}
			catch(ArgumentException ae)
			{
				Console.WriteLine("Expected exception: delEnergyEnrollment:" + ae.Message);
			}
			catch(Exception e)
			{
				Console.WriteLine("Error: delEnergyEnrollment: " + e.Message);
			}
            
		}
		[Test]
		public void addEnergyEnrollment()
		{
			UOW uow = new UOW();
			uow.Service = "addEnergyEnrollment";
			EnergyEnrollment cls = new EnergyEnrollment(uow);
            
			cls.AccountNumber = this.accountNumber;
			cls.AccountName = this.accountName;
			cls.ServiceProviderName = this.serviceProviderName;
			cls.ServiceProviderPhone = this.serviceProviderPhone;
			cls.InitPrepayAmt = this.initPrepayAmt;
			cls.Customer = this.customer;
			cls.Address1 = this.address1;
			cls.Address2 = this.address2;
			cls.City = this.city;
			cls.State = this.state;
			cls.Zip = this.zip;
			cls.Zip4 = this.zip4;
			cls.StartDate = this.startDate;
			cls.EnrollDate = this.enrollDate;
			cls.ModifyDate = this.modifyDate;
			cls.Status = this.status;
        
			uow.commit();
			this.iD = cls.ID;
            
			uow = new UOW();
			uow.Service = "addEnergyEnrollment - assert";
			cls = EnergyEnrollment.find(uow, this.iD);
			Assertion.Assert(cls.AccountName == this.accountName);
			uow.close();
		}
		[Test]
		public void findEnergyEnrollment()
		{
			UOW uow = new UOW();
			uow.Service = "findEnergyEnrollment";
            
			EnergyEnrollment cls = EnergyEnrollment.find(uow, this.iD);
			Assertion.Assert(cls.ID == this.iD);
			uow.close();
		}
		[Test]
		public void saveEnergyEnrollment()
		{
			UOW uow = new UOW();
			uow.Service = "saveEnergyEnrollment";
			EnergyEnrollment cls = EnergyEnrollment.find(uow, this.iD);
            
			cls.AccountNumber = this.accountNumber;
			cls.AccountName = this.accountName;
			cls.ServiceProviderName = this.serviceProviderName;
			cls.ServiceProviderPhone = this.serviceProviderPhone;
			cls.InitPrepayAmt = this.initPrepayAmt;
			cls.Customer = this.customer;
			cls.Address1 = this.address1;
			cls.Address2 = this.address2;
			cls.City = this.city;
			cls.State = this.state;
			cls.Zip = this.zip;
			cls.Zip4 = this.zip4;
			cls.StartDate = this.startDate;
			cls.EnrollDate = this.enrollDate;
			cls.ModifyDate = this.modifyDate;
			cls.Status = this.status;
			cls.AccountNumber += 2;
			this.accountNumber = cls.AccountNumber;
                
			uow.commit();
            
			uow = new UOW();
			uow.Service = "saveEnergyEnrollment - assert";
            
			cls = EnergyEnrollment.find(uow, this.iD);
			Assertion.Assert(cls.AccountNumber == this.accountNumber);
			uow.close();
		}
		[Test]
		public void findAllEnergyEnrollments()
		{
			UOW uow = new UOW();
			uow.Service = "findAllEnergyEnrollments";
			EnergyEnrollment[] objs = EnergyEnrollment.getAll(uow);
			Assertion.Assert(objs.Length > 0);
			uow.close();
		}
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void delEnergyEnrollment()
		{
			UOW uow = new UOW();
			uow.Service = "delEnergyEnrollment";
			EnergyEnrollment cls = EnergyEnrollment.find(uow, this.iD);
			cls.delete();
            
			uow.commit();
            
			uow = new UOW();
			uow.Service = "delEnergyEnrollment - assert";
			cls = EnergyEnrollment.find(uow, this.iD);
			Assertion.Assert((cls.ID == 0));
			uow.close();
		}
	}
}
