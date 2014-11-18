using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;


namespace DPI.ComponentsTests
{
    [TestFixture]
    public class CorporationTests
    {
        /*		Data		*/
        string name = "name";
        string address = "address";
        string city = "city";
        string state = "state";
        string zip = "zip";
        string fax = "fax";
        string contact = "contact";
        string phone = "phone";
        int corpID = 27;
        DateTime date_Created = DateTime.Now;
        bool rAC_WF = true;
		int parentId = 496;                 
		bool skipStoreStats = true;
		bool usePapentForStoreStats = true;
		bool requestClerkId = true;
        
        /*		Constructors		*/
        public CorporationTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            CorporationTests test = new CorporationTests();
            
            // UOW Tests
            test.addCorporation();
            test.findCorporation();
            test.saveCorporation();
            test.findAllCorporations();
            
//            try
//            {
//                test.delCorporation();
//            }
//            catch(ArgumentException ae)
//            {
//                Console.WriteLine("Expected exception: delCorporation:" + ae.Message);
//            }
//            catch(Exception e)
//            {
//                Console.WriteLine("Error: delCorporation: " + e.Message);
//            }
            
        }
        [Test]
        public void addCorporation()
        {
            UOW uow = new UOW();
            uow.Service = "addCorporation";
            Corporation cls = new Corporation(uow);
            
            cls.Name = this.name;
            cls.Address = this.address;
            cls.City = this.city;
            cls.St = this.state;
            cls.Zip = this.zip;
            cls.Fax = this.fax;
            cls.Contact = this.contact;
            cls.Phone = this.phone;
            cls.Date_Created = this.date_Created;
            cls.RAC_WF = this.rAC_WF;
			cls.ParentId = this.parentId;
			cls.SkipStoreStats = this.skipStoreStats;
			cls.UsePapentForStoreStats = this.usePapentForStoreStats;
			cls.RequestClerkId = this.requestClerkId;
			
            uow.commit();
            this.corpID = cls.CorpID;
            
            uow = new UOW();
            uow.Service = "addCorporation - assert";
            cls = Corporation.find(uow, this.corpID);
            Assertion.Assert(cls.City == this.city);
			Assertion.Assert(cls.RequestClerkId == this.requestClerkId);
            uow.close();
        }
        [Test]
        public void findCorporation()
        {
            UOW uow = new UOW();
            uow.Service = "findCorporation";
            
            Corporation cls = Corporation.find(uow, this.corpID);
            Assertion.Assert(cls.CorpID == this.corpID);
            uow.close();
        }
        [Test]
        public void saveCorporation()
        {
            UOW uow = new UOW();
            uow.Service = "saveCorporation";
            Corporation cls = Corporation.find(uow, this.corpID);
            
            cls.Name = this.name;
            cls.Address = this.address;
            cls.City = this.city;
            cls.St = this.state;
            cls.Zip = this.zip;
            cls.Fax = this.fax;
            cls.Contact = this.contact;
            cls.Phone = this.phone;
            cls.Date_Created = this.date_Created;
            cls.RAC_WF = this.rAC_WF;
			cls.ParentId = this.parentId;
			cls.SkipStoreStats = this.skipStoreStats;
			cls.UsePapentForStoreStats = this.usePapentForStoreStats;
			cls.RequestClerkId = this.requestClerkId;
            cls.Name += " saved";
            this.name = cls.Name;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveCorporation - assert";
            
            cls = Corporation.find(uow, this.corpID);
            Assertion.Assert(cls.Name == this.name);
            uow.close();
        }
        [Test]
        public void findAllCorporations()
        {
            UOW uow = new UOW();
            uow.Service = "findAllCorporations";
            Corporation[] objs = Corporation.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delCorporation()
        {
            UOW uow = new UOW();
            uow.Service = "delCorporation";
            Corporation cls = Corporation.find(uow, this.corpID);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delCorporation - assert";
            cls = Corporation.find(uow, this.corpID);
            Assertion.Assert((cls.CorpID == 0));
            uow.close();
        }
    }
}
