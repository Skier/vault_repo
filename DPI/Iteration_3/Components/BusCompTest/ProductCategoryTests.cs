using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.ComponentsTests
{
    [TestFixture]
    public class ProductCategoryTests
    {
        /*		Data		*/
        string prodCategory = "prodCategory";
        bool isLocal = true;
        bool isWireless = true;
        bool isInternet = true;
        bool isDebitCard = true;
        
        /*		Constructors		*/
        public ProductCategoryTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            ProductCategoryTests test = new ProductCategoryTests();
            
            // UOW Tests
            test.addProductCategory();
            test.findProductCategory();
            test.saveProductCategory();
            test.findAllProductCategorys();
            
            try
            {
                test.delProductCategory();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delProductCategory:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delProductCategory: " + e.Message);
            }
            
        }
        [Test]
        public void addProductCategory()
        {
            UOW uow = new UOW();
            uow.Service = "addProductCategory";
            ProductCategory cls = new ProductCategory(uow);
            
            cls.ProdCategory = this.prodCategory;
            cls.IsLocal = this.isLocal;
            cls.IsWireless = this.isWireless;
            cls.IsInternet = this.isInternet;
            cls.IsDebitCard = this.isDebitCard;
        
            uow.commit();
            
            uow = new UOW();
            uow.Service = "addProductCategory - assert";
            cls = ProductCategory.find(uow, this.prodCategory);
            Assertion.Assert(cls.IsWireless == this.isWireless);
            uow.close();
        }
        [Test]
        public void findProductCategory()
        {
            UOW uow = new UOW();
            uow.Service = "findProductCategory";
            
            ProductCategory cls = ProductCategory.find(uow, this.prodCategory);
            Assertion.Assert(cls.ProdCategory.Trim() == this.prodCategory.Trim());
            uow.close();
        }
        [Test]
        public void saveProductCategory()
        {
            UOW uow = new UOW();
            uow.Service = "saveProductCategory";
            ProductCategory cls = ProductCategory.find(uow, this.prodCategory);
            
            cls.ProdCategory = this.prodCategory;
            cls.IsLocal = this.isLocal;
            cls.IsWireless = this.isWireless;
            cls.IsInternet = this.isInternet;
            cls.IsDebitCard = this.isDebitCard;
            cls.IsLocal = false;
            this.isLocal = cls.IsLocal;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveProductCategory - assert";
            
            cls = ProductCategory.find(uow, this.prodCategory);
            Assertion.Assert(cls.IsLocal == this.isLocal);
            uow.close();
        }
        [Test]
        public void findAllProductCategorys()
        {
            UOW uow = new UOW();
            uow.Service = "findAllProductCategorys";
            ProductCategory[] objs = ProductCategory.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delProductCategory()
        {
            UOW uow = new UOW();
            uow.Service = "delProductCategory";
            ProductCategory cls = ProductCategory.find(uow, this.prodCategory);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delProductCategory - assert";
            cls = ProductCategory.find(uow, this.prodCategory);
            Assertion.Assert((cls.ProdCategory ==  null));
            uow.close();
        }
    }
}
