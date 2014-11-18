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
    public class AOL_PINsTests
    {
        /*		Data		*/
        int id;
        string pIN = "pIN";
        DateTime createDate = DateTime.Now;
        DateTime issueDate = DateTime.Now;
        bool active = true;
        string wireless_product_id = "wireless_product_id";
        DateTime expirationDate = DateTime.Now;
        string batchID = "batchID";
        string pricePlanID = "pricePlanID";
        decimal vendorPrice = 9.5M;
        string serialNumber = "serialNumber";
        string pinType = "pinType";
        string status = "status";
        
        /*		Constructors		*/
        public AOL_PINsTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            AOL_PINsTests test = new AOL_PINsTests();
            
            // UOW Tests
            test.addAOL_PINs();
            test.findAOL_PINs();
            test.saveAOL_PINs();
            test.findAllAOL_PINses();
            
            try
            {
                test.delAOL_PINs();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delAOL_PINs:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delAOL_PINs: " + e.Message);
            }
            
        }
        [Test]
        public void addAOL_PINs()
        {
            UOW uow = new UOW();
            uow.Service = "addAOL_PINs";
            AOL_PINs cls = new AOL_PINs(uow);
            
            cls.PIN = this.pIN;
            cls.CreateDate = this.createDate;
            cls.IssueDate = this.issueDate;
            cls.Active = this.active;
            cls.Wireless_product_id = this.wireless_product_id;
            cls.ExpirationDate = this.expirationDate;
            cls.BatchID = this.batchID;
            cls.PricePlanID = this.pricePlanID;
            cls.VendorPrice = this.vendorPrice;
            cls.SerialNumber = this.serialNumber;
            cls.PinType = this.pinType;
            cls.Status = this.status;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addAOL_PINs - assert";
            cls = AOL_PINs.find(uow, this.id);
            Assertion.Assert(cls.CreateDate == this.createDate);
            uow.close();
        }
        [Test]
        public void findAOL_PINs()
        {
            UOW uow = new UOW();
            uow.Service = "findAOL_PINs";
            
            AOL_PINs cls = AOL_PINs.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveAOL_PINs()
        {
            UOW uow = new UOW();
            uow.Service = "saveAOL_PINs";
            AOL_PINs cls = AOL_PINs.find(uow, this.id);
            
            cls.PIN = this.pIN;
            cls.CreateDate = this.createDate;
            cls.IssueDate = this.issueDate;
            cls.Active = this.active;
            cls.Wireless_product_id = this.wireless_product_id;
            cls.ExpirationDate = this.expirationDate;
            cls.BatchID = this.batchID;
            cls.PricePlanID = this.pricePlanID;
            cls.VendorPrice = this.vendorPrice;
            cls.SerialNumber = this.serialNumber;
            cls.PinType = this.pinType;
            cls.Status = this.status;
            cls.PIN += " saved";
            this.pIN = cls.PIN;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveAOL_PINs - assert";
            
            cls = AOL_PINs.find(uow, this.id);
            Assertion.Assert(cls.PIN == this.pIN);
            uow.close();
        }
        [Test]
        public void findAllAOL_PINses()
        {
            UOW uow = new UOW();
            uow.Service = "findAllAOL_PINses";
            AOL_PINs[] objs = AOL_PINs.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delAOL_PINs()
        {
            UOW uow = new UOW();
            uow.Service = "delAOL_PINs";
            AOL_PINs cls = AOL_PINs.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delAOL_PINs - assert";
            cls = AOL_PINs.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
