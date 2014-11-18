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
    public class WirelessZipCodeTests
    {
        /*		Data		*/
        string zipcode = "zipcode";
        string zip_Postal_City = "zip_Postal_City";
        string state = "state";
        string sPCS_Customer_Service_ID = "sPCS_Customer_Service_ID";
        
        /*		Constructors		*/
        public WirelessZipCodeTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            WirelessZipCodeTests test = new WirelessZipCodeTests();
            
            // UOW Tests
            test.addWirelessZipCode();
            test.findWirelessZipCode();
            test.saveWirelessZipCode();
            test.findAllWirelessZipCodes();
            
            try
            {
                test.delWirelessZipCode();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delWirelessZipCode:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delWirelessZipCode: " + e.Message);
            }
            
        }
        [Test]
        public void addWirelessZipCode()
        {
            UOW uow = new UOW();
            uow.Service = "addWirelessZipCode";
            WirelessZipCode cls = new WirelessZipCode(uow);
            
            cls.Zipcode = this.zipcode;
            cls.Zip_Postal_City = this.zip_Postal_City;
            cls.State = this.state;
            cls.SPCS_Customer_Service_ID = this.sPCS_Customer_Service_ID;
        
            uow.commit();
            
            uow = new UOW();
            uow.Service = "addWirelessZipCode - assert";
            cls = WirelessZipCode.find(uow, this.zipcode);
            Assertion.Assert(cls.State == this.state);
            uow.close();
        }
        [Test]
        public void findWirelessZipCode()
        {
            UOW uow = new UOW();
            uow.Service = "findWirelessZipCode";
            
            WirelessZipCode cls = WirelessZipCode.find(uow, this.zipcode);
            Assertion.Assert(cls.Zipcode.Trim() == this.zipcode.Trim());
            uow.close();
        }
        [Test]
        public void saveWirelessZipCode()
        {
            UOW uow = new UOW();
            uow.Service = "saveWirelessZipCode";
            WirelessZipCode cls = WirelessZipCode.find(uow, this.zipcode);
            
            cls.Zipcode = this.zipcode;
            cls.Zip_Postal_City = this.zip_Postal_City;
            cls.State = this.state;
            cls.SPCS_Customer_Service_ID = this.sPCS_Customer_Service_ID;
            cls.Zip_Postal_City += " saved";
            this.zip_Postal_City = cls.Zip_Postal_City;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveWirelessZipCode - assert";
            
            cls = WirelessZipCode.find(uow, this.zipcode);
            Assertion.Assert(cls.Zip_Postal_City == this.zip_Postal_City);
            uow.close();
        }
        [Test]
        public void findAllWirelessZipCodes()
        {
            UOW uow = new UOW();
            uow.Service = "findAllWirelessZipCodes";
            WirelessZipCode[] objs = WirelessZipCode.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delWirelessZipCode()
        {
            UOW uow = new UOW();
            uow.Service = "delWirelessZipCode";
            WirelessZipCode cls = WirelessZipCode.find(uow, this.zipcode);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delWirelessZipCode - assert";
            cls = WirelessZipCode.find(uow, this.zipcode);
            Assertion.Assert((cls.Zipcode ==  null));
            uow.close();
        }
    }
}
