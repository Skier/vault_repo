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
    public class ProductOrderRuleTests
    {
        /*		Data		*/
        int id = 1;
        int product = 2;
        string dmdType = "dmdType";
        decimal minAmt = 3.5M;
        decimal maxAmt = 4.5M;
        int expirationPeriod = 6;
        
        /*		Constructors		*/
        public ProductOrderRuleTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            ProductOrderRuleTests test = new ProductOrderRuleTests();
            
            // UOW Tests
            test.addProductOrderRule();
            test.findProductOrderRule();
            test.saveProductOrderRule();
            test.findAllProductOrderRules();
            
            try
            {
                test.delProductOrderRule();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delProductOrderRule:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delProductOrderRule: " + e.Message);
            }
            
        }
        [Test]
        public void addProductOrderRule()
        {
            UOW uow = new UOW();
            uow.Service = "addProductOrderRule";
            ProductOrderRule cls = new ProductOrderRule(uow);
            
            cls.Id = this.id;
            cls.Product = this.product;
            cls.DmdType = this.dmdType;
            cls.MinAmt = this.minAmt;
            cls.MaxAmt = this.maxAmt;
            cls.ExpirationPeriod = this.expirationPeriod;
        
            uow.commit();
            
            uow = new UOW();
            uow.Service = "addProductOrderRule - assert";
            cls = ProductOrderRule.find(uow, this.id);
            Assertion.Assert(cls.DmdType == this.dmdType);
            uow.close();
        }
        [Test]
        public void findProductOrderRule()
        {
            UOW uow = new UOW();
            uow.Service = "findProductOrderRule";
            
            ProductOrderRule cls = ProductOrderRule.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveProductOrderRule()
        {
            UOW uow = new UOW();
            uow.Service = "saveProductOrderRule";
            ProductOrderRule cls = ProductOrderRule.find(uow, this.id);
            
            cls.Id = this.id;
            cls.Product = this.product;
            cls.DmdType = this.dmdType;
            cls.MinAmt = this.minAmt;
            cls.MaxAmt = this.maxAmt;
            cls.ExpirationPeriod = this.expirationPeriod;
            cls.Product += 2;
            this.product = cls.Product;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveProductOrderRule - assert";
            
            cls = ProductOrderRule.find(uow, this.id);
            Assertion.Assert(cls.Product == this.product);
            uow.close();
        }
        [Test]
        public void findAllProductOrderRules()
        {
            UOW uow = new UOW();
            uow.Service = "findAllProductOrderRules";
            ProductOrderRule[] objs = ProductOrderRule.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delProductOrderRule()
        {
            UOW uow = new UOW();
            uow.Service = "delProductOrderRule";
            ProductOrderRule cls = ProductOrderRule.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delProductOrderRule - assert";
            cls = ProductOrderRule.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
