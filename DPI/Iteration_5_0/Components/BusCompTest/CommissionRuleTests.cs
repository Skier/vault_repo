using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;


namespace DPI.ComponentsTests
{
    [TestFixture]
    public class CommissionRuleTests
    {
        /*		Data		*/
        int id;
        int product = 2;
        string vendor = "vendor";
        int agent = 4;
        string minAmt = "minAmt";
        string maxAmt = "maxAmt";
        decimal commissionAmt = 6.5M;
        int rate = 8;
        DateTime fromEffDate = DateTime.Now;
        DateTime toEffDate = DateTime.Now;
        DateTime status = DateTime.Now;
        
        /*		Constructors		*/
        public CommissionRuleTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            CommissionRuleTests test = new CommissionRuleTests();
            
            // UOW Tests
            test.addCommissionRule();
            test.findCommissionRule();
            test.saveCommissionRule();
            test.findAllCommissionRules();
            
            try
            {
                test.delCommissionRule();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delCommissionRule:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delCommissionRule: " + e.Message);
            }
            
        }
        [Test]
        public void addCommissionRule()
        {
            UOW uow = new UOW();
            uow.Service = "addCommissionRule";
            CommissionRule cls = new CommissionRule(uow);
            
            cls.Product = this.product;
            cls.Vendor = this.vendor;
            cls.Agent = this.agent;
            cls.MinAmt = this.minAmt;
            cls.MaxAmt = this.maxAmt;
            cls.CommissionAmt = this.commissionAmt;
            cls.Rate = this.rate;
            cls.FromEffDate = this.fromEffDate;
            cls.ToEffDate = this.toEffDate;
            cls.Status = this.status;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addCommissionRule - assert";
            cls = CommissionRule.find(uow, this.id);
            Assertion.Assert(cls.Product == this.product);
            uow.close();
        }
        [Test]
        public void findCommissionRule()
        {
            UOW uow = new UOW();
            uow.Service = "findCommissionRule";
            
            CommissionRule cls = CommissionRule.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveCommissionRule()
        {
            UOW uow = new UOW();
            uow.Service = "saveCommissionRule";
            CommissionRule cls = CommissionRule.find(uow, this.id);
            
            cls.Product = this.product;
            cls.Vendor = this.vendor;
            cls.Agent = this.agent;
            cls.MinAmt = this.minAmt;
            cls.MaxAmt = this.maxAmt;
            cls.CommissionAmt = this.commissionAmt;
            cls.Rate = this.rate;
            cls.FromEffDate = this.fromEffDate;
            cls.ToEffDate = this.toEffDate;
            cls.Status = this.status;
            cls.Product += 2;
            this.product = cls.Product;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveCommissionRule - assert";
            
            cls = CommissionRule.find(uow, this.id);
            Assertion.Assert(cls.Product == this.product);
            uow.close();
        }
        [Test]
        public void findAllCommissionRules()
        {
            UOW uow = new UOW();
            uow.Service = "findAllCommissionRules";
            CommissionRule[] objs = CommissionRule.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delCommissionRule()
        {
            UOW uow = new UOW();
            uow.Service = "delCommissionRule";
            CommissionRule cls = CommissionRule.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delCommissionRule - assert";
            cls = CommissionRule.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
