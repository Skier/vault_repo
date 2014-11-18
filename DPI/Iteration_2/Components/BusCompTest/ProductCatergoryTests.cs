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
    public class ProductCatergoryTests
    {
        /*		Data		*/
        string prodCategory = "prodCategory";
        bool isLocal = true;
        bool isWireless = true;
        bool isIntern0et = true;
        bool isDbitCard = true;
        
        /*		Constructors		*/
        public ProductCatergoryTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            ProductCatergoryTests test = new ProductCatergoryTests();
            
            // UOW Tests
            test.addProductCatergory();
            test.findProductCatergory();
            test.saveProductCatergory();
            test.findAllProductCatergorys();
            
            try
            {
                test.delProductCatergory();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delProductCatergory:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delProductCatergory: " + e.Message);
            }
            
        }
        [Test]
        public void addProductCatergory()
        {
            UOW uow = new UOW();
            uow.Service = "addProductCatergory";
            ProductCatergory cls = new ProductCatergory(uow);
            
            cls.ProdCategory = this.prodCategory;
            cls.IsLocal = this.isLocal;
            cls.IsWireless = this.isWireless;
            cls.IsIntern0et = this.isIntern0et;
            cls.IsDbitCard = this.isDbitCard;
        
            uow.commit();
            
            uow = new UOW();
            uow.Service = "addProductCatergory - assert";
            cls = ProductCatergory.find(uow, this.prodCategory);
            Assertion.Assert(cls.IsWireless == this.isWireless);
            uow.close();
        }
        [Test]
        public void findProductCatergory()
        {
            UOW uow = new UOW();
            uow.Service = "findProductCatergory";
            
            ProductCatergory cls = ProductCatergory.find(uow, this.prodCategory);
            Assertion.Assert(cls.ProdCategory.Trim() == this.prodCategory.Trim());
            uow.close();
        }
        [Test]
        public void saveProductCatergory()
        {
            UOW uow = new UOW();
            uow.Service = "saveProductCatergory";
            ProductCatergory cls = ProductCatergory.find(uow, this.prodCategory);
            
            cls.ProdCategory = this.prodCategory;
            cls.IsLocal = this.isLocal;
            cls.IsWireless = this.isWireless;
            cls.IsIntern0et = this.isIntern0et;
            cls.IsDbitCard = this.isDbitCard;
            cls.IsLocal = false;
            this.isLocal = cls.IsLocal;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveProductCatergory - assert";
            
            cls = ProductCatergory.find(uow, this.prodCategory);
            Assertion.Assert(cls.IsLocal == this.isLocal);
            uow.close();
        }
        [Test]
        public void findAllProductCatergorys()
        {
            UOW uow = new UOW();
            uow.Service = "findAllProductCatergorys";
            ProductCatergory[] objs = ProductCatergory.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delProductCatergory()
        {
            UOW uow = new UOW();
            uow.Service = "delProductCatergory";
            ProductCatergory cls = ProductCatergory.find(uow, this.prodCategory);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delProductCatergory - assert";
            cls = ProductCatergory.find(uow, this.prodCategory);
            Assertion.Assert((cls.ProdCategory ==  null));
            uow.close();
        }
    }
}
