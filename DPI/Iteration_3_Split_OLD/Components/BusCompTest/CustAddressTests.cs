using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;


namespace DPI.ComponentsTests
{
	[TestFixture]
	public class CustAddressTests
	{
		/*		Data		*/
		int addressID;
		string adrStatus = "adrStatus";
		AddressType adrType = AddressType.Service;
		string streetNum = "streetNum";
		string streetPrefix = "stPf";
		string street = "street";
		string streetType = "stTp";
		string streetSuffix = "stsx";
		string unit = "unit";
		string unitType = "unitType";
		string city = "city";
		string state = "state";
		string zipcode = "zipcode";
        
		/*		Constructors		*/
		public CustAddressTests()
		{
			// try { cleanup(); } 	catch {}
			// Console.WriteLine("Cleanup completed");
		}
        
		/*		Methods		*/
		public static void Main()
		{
			CustAddressTests test = new CustAddressTests();
            
			// UOW Tests
			test.addCustAddress();
			test.findCustAddress();
			test.saveCustAddress();
			test.findAllCustAddresses();
            
			try
			{
				test.delCustAddress();
			}
			catch(ArgumentException ae)
			{
				Console.WriteLine("Expected exception: delCustAddress:" + ae.Message);
			}
			catch(Exception e)
			{
				Console.WriteLine("Error: delCustAddress: " + e.Message);
			}
            
		}
		[Test]
		public void addCustAddress()
		{
			UOW uow = new UOW();
			uow.Service = "addCustAddress";
			CustAddress cls = new CustAddress(uow);
            
			cls.AdrStatus = this.adrStatus;
			cls.AddrType = this.adrType;
			cls.StreetNum = this.streetNum;
			cls.StreetPrefix = this.streetPrefix;
			cls.Street = this.street;
			cls.StreetType = this.streetType;
			cls.StreetSuffix = this.streetSuffix;
			cls.Unit = this.unit;
			cls.UnitType = this.unitType;
			cls.City = this.city;
			cls.State = this.state;
			cls.Zipcode = this.zipcode;
        
			uow.commit();
			this.addressID = cls.AddressID;
            
			uow = new UOW();
			uow.Service = "addCustAddress - assert";
			cls = (CustAddress)CustAddress.find(uow, this.addressID);
			Assertion.Assert(cls.AddressID == this.addressID);
			uow.close();
		}
		[Test]
		public void findCustAddress()
		{
			UOW uow = new UOW();
			uow.Service = "findCustAddress";
            
			CustAddress cls = (CustAddress)CustAddress.find(uow, this.addressID);
			Assertion.Assert(cls.AddressID == this.addressID);
			uow.close();
		}
		[Test]
		public void saveCustAddress()
		{
			UOW uow = new UOW();
			uow.Service = "saveCustAddress";
			CustAddress cls = (CustAddress)CustAddress.find(uow, this.addressID);
            
			cls.AdrStatus = this.adrStatus;
			cls.AddrType = this.adrType;
			cls.StreetNum = this.streetNum;
			cls.StreetPrefix = this.streetPrefix;
			cls.Street = this.street;
			cls.StreetType = this.streetType;
			cls.StreetSuffix = this.streetSuffix;
			cls.Unit = this.unit;
			cls.UnitType = this.unitType;
			cls.City = this.city;
			cls.State = this.state;
			cls.Zipcode = this.zipcode;
			cls.AdrStatus += " saved";
			this.adrStatus = cls.AdrStatus;
                
			uow.commit();
            
			uow = new UOW();
			uow.Service = "saveCustAddress - assert";
            
			cls = (CustAddress)CustAddress.find(uow, this.addressID);
			Assertion.Assert(cls.AdrStatus == this.adrStatus);
			uow.close();
		}
		[Test]
		public void findAllCustAddresses()
		{
			UOW uow = new UOW();
			uow.Service = "findAllCustAddresses";
			IAddr[] objs = CustAddress.getAll(uow);
			Assertion.Assert(objs.Length > 0);
			uow.close();
		}
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void delCustAddress()
		{
			IMap imap = new IdentityMap();
			UOW uow = new UOW(imap);
			uow.Service = "delCustAddress";
			CustAddress cls = (CustAddress)CustAddress.find(uow, this.addressID);
			cls.Uow = uow;
			cls.delete();
            
			uow.commit();
            
			uow = new UOW(imap);
			uow.Service = "delCustAddress - assert";
			cls = (CustAddress)CustAddress.find(uow, this.addressID);
			Assertion.Assert((cls.AddressID == 0));
			uow.close();
		}
	}
}
