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
    public class FinancialProdTransTests
    {
        /*		Data		*/
        int id;
        string tranType = "tranType";
        int product = 3;
        int demand = 4;
        int agenInvoice = 5;
        int vendor = 6;
        string storecode = "storecode";
        string clerkId = "clerkId";
        int customer = 9;
        DateTime tranDate = DateTime.Now;
        decimal tranAmt = 40.9M;
        decimal prodAmt = 11.5M;
        decimal feeAmt = 12.5M;
        decimal comAmt = 2.3M;
        string confirmation = "confirmation";
        string status = "status";
        
        /*		Constructors		*/
        public FinancialProdTransTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            FinancialProdTransTests test = new FinancialProdTransTests();
            
            // UOW Tests
            test.addFinancialProdTrans();
            test.findFinancialProdTrans();
            test.saveFinancialProdTrans();
            test.findAllFinancialProdTranses();
            
            try
            {
                test.delFinancialProdTrans();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delFinancialProdTrans:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delFinancialProdTrans: " + e.Message);
            }
            
        }
        [Test]
        public void addFinancialProdTrans()
        {
            UOW uow = new UOW();
            uow.Service = "addFinancialProdTrans";
            FinancialProdTrans cls = new FinancialProdTrans(uow);
            
            cls.TranType = this.tranType;
            cls.Product = this.product;
            cls.Dmd = this.demand;
            cls.AgenInvoice = this.agenInvoice;
            cls.Vendor = this.vendor;
            cls.Storecode = this.storecode;
            cls.ClerkId = this.clerkId;
          //  cls.Customer = this.customer;
            cls.TranDate = this.tranDate;
            cls.TranAmt = this.tranAmt;
            cls.ProdAmt = this.product;
            cls.FeeAmt = this.feeAmt;
            cls.ComAmt = this.comAmt;
            cls.Confirmation = this.confirmation;
            cls.Status = this.status;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addFinancialProdTrans - assert";
            cls = FinancialProdTrans.find(uow, this.id);
            Assertion.Assert(cls.Product == this.product);
            uow.close();
        }
        [Test]
        public void findFinancialProdTrans()
        {
            UOW uow = new UOW();
            uow.Service = "findFinancialProdTrans";
            
            FinancialProdTrans cls = FinancialProdTrans.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveFinancialProdTrans()
        {
            UOW uow = new UOW();
            uow.Service = "saveFinancialProdTrans";
            FinancialProdTrans cls = FinancialProdTrans.find(uow, this.id);
            
            cls.TranType = this.tranType;
            cls.Product = this.product;
            cls.Dmd = this.demand;
            cls.AgenInvoice = this.agenInvoice;
            cls.Vendor = this.vendor;
            cls.Storecode = this.storecode;
            cls.ClerkId = this.clerkId;
        //    cls.Customer = this.customer;
            cls.TranDate = this.tranDate;
            cls.TranAmt = this.tranAmt;
            cls.ProdAmt = this.prodAmt;
            cls.FeeAmt = this.feeAmt;
            cls.ComAmt = this.comAmt;
            cls.Confirmation = this.confirmation;
            cls.Status = this.status;
            cls.TranType += " saved";
            this.tranType = cls.TranType;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveFinancialProdTrans - assert";
            
            cls = FinancialProdTrans.find(uow, this.id);
            Assertion.Assert(cls.TranType == this.tranType);
            uow.close();
        }
        [Test]
        public void findAllFinancialProdTranses()
        {
            UOW uow = new UOW();
            uow.Service = "findAllFinancialProdTranses";
            FinancialProdTrans[] objs = FinancialProdTrans.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delFinancialProdTrans()
        {
            UOW uow = new UOW();
            uow.Service = "delFinancialProdTrans";
            FinancialProdTrans cls = FinancialProdTrans.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delFinancialProdTrans - assert";
            cls = FinancialProdTrans.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
