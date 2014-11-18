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
    public class Wireless_CustdataTests
    {
        /*		Data		*/
        int iD;
        string eSN = "eSN";
        string phNumber = "phNumber";
        string subscriberId = "subscriberId";
        string nameFirst = "nameFirst";
        string nameLast = "nameLast";
        string addr1 = "addr1";
        string addr2 = "addr2";
        string city = "city";
        string state = "state";
        string zip = "zip";
        string email = "email";
        string contactNumber = "contactNumber";
        
        /*		Constructors		*/
        public Wireless_CustdataTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            Wireless_CustdataTests test = new Wireless_CustdataTests();
            
            // UOW Tests
            test.addWireless_Custdata();
            test.findWireless_Custdata();
            test.saveWireless_Custdata();
            test.findAllWireless_Custdatas();
            
            try
            {
                test.delWireless_Custdata();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delWireless_Custdata:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delWireless_Custdata: " + e.Message);
            }
            
        }
        [Test]
        public void addWireless_Custdata()
        {
            UOW uow = new UOW();
            uow.Service = "addWireless_Custdata";
            Wireless_Custdata cls = new Wireless_Custdata(uow);
            
            cls.ESN = this.eSN;
            cls.PhNumber = this.phNumber;
            cls.SubscriberId = this.subscriberId;
            cls.NameFirst = this.nameFirst;
            cls.NameLast = this.nameLast;
            cls.Addr1 = this.addr1;
            cls.Addr2 = this.addr2;
            cls.City = this.city;
            cls.State = this.state;
            cls.Zip = this.zip;
            cls.Email = this.email;
            cls.ContactNumber = this.contactNumber;
        
            uow.commit();
            this.iD = cls.ID;
            
            uow = new UOW();
            uow.Service = "addWireless_Custdata - assert";
            cls = Wireless_Custdata.find(uow, this.iD);
            Assertion.Assert(cls.PhNumber == this.phNumber);
            uow.close();
        }
        [Test]
        public void findWireless_Custdata()
        {
            UOW uow = new UOW();
            uow.Service = "findWireless_Custdata";
            
            Wireless_Custdata cls = Wireless_Custdata.find(uow, this.iD);
            Assertion.Assert(cls.ID == this.iD);
            uow.close();
        }
        [Test]
        public void saveWireless_Custdata()
        {
            UOW uow = new UOW();
            uow.Service = "saveWireless_Custdata";
            Wireless_Custdata cls = Wireless_Custdata.find(uow, this.iD);
            
            cls.ESN = this.eSN;
            cls.PhNumber = this.phNumber;
            cls.SubscriberId = this.subscriberId;
            cls.NameFirst = this.nameFirst;
            cls.NameLast = this.nameLast;
            cls.Addr1 = this.addr1;
            cls.Addr2 = this.addr2;
            cls.City = this.city;
            cls.State = this.state;
            cls.Zip = this.zip;
            cls.Email = this.email;
            cls.ContactNumber = this.contactNumber;
            cls.ESN += " saved";
            this.eSN = cls.ESN;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveWireless_Custdata - assert";
            
            cls = Wireless_Custdata.find(uow, this.iD);
            Assertion.Assert(cls.ESN == this.eSN);
            uow.close();
        }
        [Test]
        public void findAllWireless_Custdatas()
        {
            UOW uow = new UOW();
            uow.Service = "findAllWireless_Custdatas";
            Wireless_Custdata[] objs = Wireless_Custdata.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delWireless_Custdata()
        {
            UOW uow = new UOW();
            uow.Service = "delWireless_Custdata";
            Wireless_Custdata cls = Wireless_Custdata.find(uow, this.iD);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delWireless_Custdata - assert";
            cls = Wireless_Custdata.find(uow, this.iD);
            Assertion.Assert((cls.ID == 0));
            uow.close();
        }
    }
}
