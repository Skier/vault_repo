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
    public class Address_StreetTypeTests
    {
        /*		Data		*/
        int address_StreetType_ID;
        string streetType = "streetType";
        string streetTypeAbbr = "TypeAbbr";
        
        /*		Constructors		*/
        public Address_StreetTypeTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            Address_StreetTypeTests test = new Address_StreetTypeTests();
            
            // UOW Tests
            test.addAddress_StreetType();
            test.findAddress_StreetType();
            test.saveAddress_StreetType();
            test.findAllAddress_StreetTypes();
            
            try
            {
                test.delAddress_StreetType();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delAddress_StreetType:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delAddress_StreetType: " + e.Message);
            }
            
        }
        [Test]
        public void addAddress_StreetType()
        {
            UOW uow = new UOW();
            uow.Service = "addAddress_StreetType";
            Address_StreetType cls = new Address_StreetType(uow);
            
            cls.StreetType = this.streetType;
            cls.StreetTypeAbbr = this.streetTypeAbbr;
        
            uow.commit();
            this.address_StreetType_ID = cls.Address_StreetType_ID;
            
            uow = new UOW();
            uow.Service = "addAddress_StreetType - assert";
            cls = Address_StreetType.find(uow, this.address_StreetType_ID);
            Assertion.Assert(cls.StreetTypeAbbr == this.streetTypeAbbr);
            uow.close();
        }
        [Test]
        public void findAddress_StreetType()
        {
            UOW uow = new UOW();
            uow.Service = "findAddress_StreetType";
            
            Address_StreetType cls = Address_StreetType.find(uow, this.address_StreetType_ID);
            Assertion.Assert(cls.Address_StreetType_ID == this.address_StreetType_ID);
            uow.close();
        }
        [Test]
        public void saveAddress_StreetType()
        {
            UOW uow = new UOW();
            uow.Service = "saveAddress_StreetType";
            Address_StreetType cls = Address_StreetType.find(uow, this.address_StreetType_ID);
            
            cls.StreetType = this.streetType;
            cls.StreetTypeAbbr = this.streetTypeAbbr;
            cls.StreetType += " saved";
            this.streetType = cls.StreetType;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveAddress_StreetType - assert";
            
            cls = Address_StreetType.find(uow, this.address_StreetType_ID);
            Assertion.Assert(cls.StreetType == this.streetType);
            uow.close();
        }
        [Test]
        public void findAllAddress_StreetTypes()
        {
            UOW uow = new UOW();
            uow.Service = "findAllAddress_StreetTypes";
            Address_StreetType[] objs = Address_StreetType.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delAddress_StreetType()
        {
            UOW uow = new UOW();
            uow.Service = "delAddress_StreetType";
            Address_StreetType cls = Address_StreetType.find(uow, this.address_StreetType_ID);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delAddress_StreetType - assert";
            cls = Address_StreetType.find(uow, this.address_StreetType_ID);
            Assertion.Assert((cls.Address_StreetType_ID == 0));
            uow.close();
        }
    }
}
