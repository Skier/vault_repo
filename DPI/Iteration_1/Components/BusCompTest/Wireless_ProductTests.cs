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
    public class Wireless_ProductsTests
    {
        /*		Data		*/
        int wireless_product_id;
        string product_name = "product_name";
        int supplier_id = 3;
        int vendor_id = 4;
        string soc = "soc";
        string expiration = "expiration";
        decimal price = 6.5M;
        DateTime start_date = DateTime.Now;
        DateTime end_date = DateTime.Now;
        string receipt_text = "receipt_text";
        int product_commission_percent = 11;
        decimal product_commission_flat = 11.5M;
        int prodId = 13;
        
        /*		Constructors		*/
        public Wireless_ProductsTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            Wireless_ProductsTests test = new Wireless_ProductsTests();
            
            // UOW Tests
            test.addWireless_Products();
            test.findWireless_Products();
            test.saveWireless_Products();
            test.findAllWireless_Productses();
            
            try
            {
                test.delWireless_Products();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delWireless_Products:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delWireless_Products: " + e.Message);
            }
            
        }
        [Test]
        public void addWireless_Products()
        {
            UOW uow = new UOW();
            uow.Service = "addWireless_Products";
            Wireless_Products cls = new Wireless_Products(uow);
            
            cls.Product_name = this.product_name;
            cls.Supplier_id = this.supplier_id;
            cls.Vendor_id = this.vendor_id;
            cls.Soc = this.soc;
            cls.Expiration = this.expiration;
            cls.Price = this.price;
            cls.Start_date = this.start_date;
            cls.End_date = this.end_date;
            cls.Receipt_text = this.receipt_text;
            cls.Product_commission_percent = this.product_commission_percent;
            cls.Product_commission_flat = this.product_commission_flat;
            cls.ProdId = this.prodId;
        
            uow.commit();
            this.wireless_product_id = cls.Wireless_product_id;
            
            uow = new UOW();
            uow.Service = "addWireless_Products - assert";
            cls = Wireless_Products.find(uow, this.wireless_product_id);
            Assertion.Assert(cls.Supplier_id == this.supplier_id);
            uow.close();
        }
        [Test]
        public void findWireless_Products()
        {
            UOW uow = new UOW();
            uow.Service = "findWireless_Products";
            
            Wireless_Products cls = Wireless_Products.find(uow, this.wireless_product_id);
            Assertion.Assert(cls.Wireless_product_id == this.wireless_product_id);
            uow.close();
        }
        [Test]
        public void saveWireless_Products()
        {
            UOW uow = new UOW();
            uow.Service = "saveWireless_Products";
            Wireless_Products cls = Wireless_Products.find(uow, this.wireless_product_id);
            
            cls.Product_name = this.product_name;
            cls.Supplier_id = this.supplier_id;
            cls.Vendor_id = this.vendor_id;
            cls.Soc = this.soc;
            cls.Expiration = this.expiration;
            cls.Price = this.price;
            cls.Start_date = this.start_date;
            cls.End_date = this.end_date;
            cls.Receipt_text = this.receipt_text;
            cls.Product_commission_percent = this.product_commission_percent;
            cls.Product_commission_flat = this.product_commission_flat;
            cls.ProdId = this.prodId;
            cls.Product_name += " saved";
            this.product_name = cls.Product_name;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveWireless_Products - assert";
            
            cls = Wireless_Products.find(uow, this.wireless_product_id);
            Assertion.Assert(cls.Product_name == this.product_name);
            uow.close();
        }
        [Test]
        public void findAllWireless_Productses()
        {
            UOW uow = new UOW();
            uow.Service = "findAllWireless_Productses";
            Wireless_Products[] objs = Wireless_Products.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delWireless_Products()
        {
            UOW uow = new UOW();
            uow.Service = "delWireless_Products";
            Wireless_Products cls = Wireless_Products.find(uow, this.wireless_product_id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delWireless_Products - assert";
            cls = Wireless_Products.find(uow, this.wireless_product_id);
            Assertion.Assert((cls.Wireless_product_id == 0));
            uow.close();
        }
    }
}
