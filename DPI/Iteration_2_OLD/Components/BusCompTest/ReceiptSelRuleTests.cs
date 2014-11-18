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
    public class ReceiptSelRuleTests
    {
        /*		Data		*/
        int id;
        string prodGroup = "prodGroup";
        int product = 3;
        int wLProd = 4;
        int exclusiveCorp = 5;
        string exclusiveStore = "exclusiveStore";
        string workflow = "workflow";
        int reportId = 8;
        DateTime effStartDate = DateTime.Now;
        DateTime effEndDate = DateTime.Now;
        string status = "status";
        
        /*		Constructors		*/
        public ReceiptSelRuleTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            ReceiptSelRuleTests test = new ReceiptSelRuleTests();
            
            // UOW Tests
            test.addReceiptSelRule();
            test.findReceiptSelRule();
            test.saveReceiptSelRule();
            test.findAllReceiptSelRules();
            
            try
            {
                test.delReceiptSelRule();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delReceiptSelRule:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delReceiptSelRule: " + e.Message);
            }
            
        }
        [Test]
        public void addReceiptSelRule()
        {
            UOW uow = new UOW();
            uow.Service = "addReceiptSelRule";
            ReceiptSelRule cls = new ReceiptSelRule(uow);
            
            cls.ProdGroup = this.prodGroup;
            cls.Product = this.product;
            cls.WLProd = this.wLProd;
            cls.ExclusiveCorp = this.exclusiveCorp;
            cls.ExclusiveStore = this.exclusiveStore;
            cls.Workflow = this.workflow;
            cls.ReportId = this.reportId;
            cls.EffStartDate = this.effStartDate;
            cls.EffEndDate = this.effEndDate;
            cls.Status = this.status;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addReceiptSelRule - assert";
            cls = ReceiptSelRule.find(uow, this.id);
            Assertion.Assert(cls.Product == this.product);
            uow.close();
        }
        [Test]
        public void findReceiptSelRule()
        {
            UOW uow = new UOW();
            uow.Service = "findReceiptSelRule";
            
            ReceiptSelRule cls = ReceiptSelRule.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveReceiptSelRule()
        {
            UOW uow = new UOW();
            uow.Service = "saveReceiptSelRule";
            ReceiptSelRule cls = ReceiptSelRule.find(uow, this.id);
            
            cls.ProdGroup = this.prodGroup;
            cls.Product = this.product;
            cls.WLProd = this.wLProd;
            cls.ExclusiveCorp = this.exclusiveCorp;
            cls.ExclusiveStore = this.exclusiveStore;
            cls.Workflow = this.workflow;
            cls.ReportId = this.reportId;
            cls.EffStartDate = this.effStartDate;
            cls.EffEndDate = this.effEndDate;
            cls.Status = this.status;
            cls.ProdGroup += " saved";
            this.prodGroup = cls.ProdGroup;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveReceiptSelRule - assert";
            
            cls = ReceiptSelRule.find(uow, this.id);
            Assertion.Assert(cls.ProdGroup == this.prodGroup);
            uow.close();
        }
        [Test]
        public void findAllReceiptSelRules()
        {
            UOW uow = new UOW();
            uow.Service = "findAllReceiptSelRules";
            ReceiptSelRule[] objs = ReceiptSelRule.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delReceiptSelRule()
        {
            UOW uow = new UOW();
            uow.Service = "delReceiptSelRule";
            ReceiptSelRule cls = ReceiptSelRule.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delReceiptSelRule - assert";
            cls = ReceiptSelRule.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
