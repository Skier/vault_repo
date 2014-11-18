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
    public class VendorsTests
    {
        /*		Data		*/
        int vendor_id;
        string vendor_name = "vendor_name";
        string vendor_address = "vendor_address";
        string vendor_city = "vendor_city";
        string vendor_state = "vendor_state";
        string vendor_zip = "vendor_zip";
        string vendor_phone = "vendor_phone";
        string product_type = "product_type";
        string prodCategory = "prodCategory";
        
        /*		Constructors		*/
        public VendorsTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            VendorsTests test = new VendorsTests();
            
            // UOW Tests
            test.addVendors();
            test.findVendors();
            test.saveVendors();
            test.findAllVendorses();
            
            try
            {
                test.delVendors();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delVendors:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delVendors: " + e.Message);
            }
            
        }
        [Test]
        public void addVendors()
        {
            UOW uow = new UOW();
            uow.Service = "addVendors";
            Vendors cls = new Vendors(uow);
            
            cls.Vendor_name = this.vendor_name;
            cls.Vendor_address = this.vendor_address;
            cls.Vendor_city = this.vendor_city;
            cls.Vendor_state = this.vendor_state;
            cls.Vendor_zip = this.vendor_zip;
            cls.Vendor_phone = this.vendor_phone;
            cls.Product_type = this.product_type;
            cls.ProdCategory = this.prodCategory;
        
            uow.commit();
            this.vendor_id = cls.Vendor_id;
            
            uow = new UOW();
            uow.Service = "addVendors - assert";
            cls = Vendors.find(uow, this.vendor_id);
            Assertion.Assert(cls.Vendor_address == this.vendor_address);
            uow.close();
        }
        [Test]
        public void findVendors()
        {
            UOW uow = new UOW();
            uow.Service = "findVendors";
            
            Vendors cls = Vendors.find(uow, this.vendor_id);
            Assertion.Assert(cls.Vendor_id == this.vendor_id);
            uow.close();
        }
        [Test]
        public void saveVendors()
        {
            UOW uow = new UOW();
            uow.Service = "saveVendors";
            Vendors cls = Vendors.find(uow, this.vendor_id);
            
            cls.Vendor_name = this.vendor_name;
            cls.Vendor_address = this.vendor_address;
            cls.Vendor_city = this.vendor_city;
            cls.Vendor_state = this.vendor_state;
            cls.Vendor_zip = this.vendor_zip;
            cls.Vendor_phone = this.vendor_phone;
            cls.Product_type = this.product_type;
            cls.ProdCategory = this.prodCategory;
            cls.Vendor_name += " saved";
            this.vendor_name = cls.Vendor_name;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveVendors - assert";
            
            cls = Vendors.find(uow, this.vendor_id);
            Assertion.Assert(cls.Vendor_name == this.vendor_name);
            uow.close();
        }
        [Test]
        public void findAllVendorses()
        {
            UOW uow = new UOW();
            uow.Service = "findAllVendorses";
            Vendors[] objs = Vendors.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delVendors()
        {
            UOW uow = new UOW();
            uow.Service = "delVendors";
            Vendors cls = Vendors.find(uow, this.vendor_id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delVendors - assert";
            cls = Vendors.find(uow, this.vendor_id);
            Assertion.Assert((cls.Vendor_id == 0));
            uow.close();
        }
    }
}
