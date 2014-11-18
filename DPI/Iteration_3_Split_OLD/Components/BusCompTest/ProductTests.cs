using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;


namespace GenTest
{
    [TestFixture]
    public class ProductTests
    {
        /*		Data		*/
        int id;
        string prodType = "prodType";
        string prodName = "prodName";
        string billText = "billText";
        string prodCode = "prodCode";
        string oldPriceCode = "oldPriceCode";
        string prodSubClass = "prodSubClass";
        bool isComponentOnly = true;
        bool isBillable = true;
        bool isProvisionable = true;
        bool isProvViaMapping = true;
        string description = "description";
        string eligibilityCriteria = "eligibilityCriteria";
        string provCategory = "provCategory";
        int supplier = 15;
        int vendor = 16;
        string taxCode = "taxCode";
        bool isTaxExempt = true;
        string status = "status";
        DateTime startDate = DateTime.Now;
        DateTime endDate = DateTime.Now;
        string acctCode = "acctCode";
        string compCode = "compCode";
        string deptCode = "deptCode";
        int startServMon = 25;
        int endServMon = 26;
        int predId = 27;
        int mappingProd = 28;
        
        /*		Constructors		*/
        public ProductTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            ProductTests test = new ProductTests();
            
            // UOW Tests
            test.addProduct();
            test.findProduct();
            test.saveProduct();
            test.findAllProducts();
            
            try
            {
                test.delProduct();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delProduct:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delProduct: " + e.Message);
            }
            
        }
        [Test]
        public void addProduct()
        {
            UOW uow = new UOW();
            uow.Service = "addProduct";
            Product cls = new Product(uow);
            
            cls.ProdType = this.prodType;
            cls.ProdName = this.prodName;
            cls.BillText = this.billText;
            cls.ProdCode = this.prodCode;
            cls.OldPriceCode = this.oldPriceCode;
            cls.ProdSubClass = this.prodSubClass;
            cls.IsComponentOnly = this.isComponentOnly;
            cls.IsBillable = this.isBillable;
            cls.IsProvisionable = this.isProvisionable;
            cls.IsProvViaMapping = this.isProvViaMapping;
            cls.Description = this.description;
            cls.EligibilityCriteria = this.eligibilityCriteria;
            cls.ProvCategory = this.provCategory;
            cls.Supplier = this.supplier;
            cls.Vendor = this.vendor;
            cls.TaxCode = this.taxCode;
            cls.IsTaxExempt = this.isTaxExempt;
            cls.Status = this.status;
            cls.StartDate = this.startDate;
            cls.EndDate = this.endDate;
            cls.AcctCode = this.acctCode;
            cls.CompCode = this.compCode;
            cls.DeptCode = this.deptCode;
            cls.StartServMon = this.startServMon;
            cls.EndServMon = this.endServMon;
            cls.PredId = this.predId;
            cls.MappingProd = this.mappingProd;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addProduct - assert";
            cls = Product.find(uow, this.id);
            Assertion.Assert(cls.ProdName == this.prodName);
            uow.close();
        }
        [Test]
        public void findProduct()
        {
            UOW uow = new UOW();
            uow.Service = "findProduct";
            
            Product cls = Product.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveProduct()
        {
            UOW uow = new UOW();
            uow.Service = "saveProduct";
            Product cls = Product.find(uow, this.id);
            
            cls.ProdType = this.prodType;
            cls.ProdName = this.prodName;
            cls.BillText = this.billText;
            cls.ProdCode = this.prodCode;
            cls.OldPriceCode = this.oldPriceCode;
            cls.ProdSubClass = this.prodSubClass;
            cls.IsComponentOnly = this.isComponentOnly;
            cls.IsBillable = this.isBillable;
            cls.IsProvisionable = this.isProvisionable;
            cls.IsProvViaMapping = this.isProvViaMapping;
            cls.Description = this.description;
            cls.EligibilityCriteria = this.eligibilityCriteria;
            cls.ProvCategory = this.provCategory;
            cls.Supplier = this.supplier;
            cls.Vendor = this.vendor;
            cls.TaxCode = this.taxCode;
            cls.IsTaxExempt = this.isTaxExempt;
            cls.Status = this.status;
            cls.StartDate = this.startDate;
            cls.EndDate = this.endDate;
            cls.AcctCode = this.acctCode;
            cls.CompCode = this.compCode;
            cls.DeptCode = this.deptCode;
            cls.StartServMon = this.startServMon;
            cls.EndServMon = this.endServMon;
            cls.PredId = this.predId;
            cls.MappingProd = this.mappingProd;
            cls.ProdType += " saved";
            this.prodType = cls.ProdType;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveProduct - assert";
            
            cls = Product.find(uow, this.id);
            Assertion.Assert(cls.ProdType == this.prodType);
            uow.close();
        }
        [Test]
        public void findAllProducts()
        {
            UOW uow = new UOW();
            uow.Service = "findAllProducts";
            Product[] objs = Product.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delProduct()
        {
            UOW uow = new UOW();
            uow.Service = "delProduct";
            Product cls = Product.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delProduct - assert";
            cls = Product.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
