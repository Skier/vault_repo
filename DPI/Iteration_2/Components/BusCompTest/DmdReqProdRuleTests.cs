using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.ComponentsTests
{
    [TestFixture]
    public class DmdReqProdRuleTests
    {
        /*		Data		*/
        int id;
        DemandType dmdType = DemandType.ReversalVoid;
        string ruleName = "ruleName";
        int reqProd = 4;
        DateTime effStartDate = DateTime.Now;
        DateTime effEndDate = DateTime.Now;
        
        /*		Constructors		*/
        public DmdReqProdRuleTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            DmdReqProdRuleTests test = new DmdReqProdRuleTests();
            
			test.GetAllForDmdType();
            // UOW Tests
            test.addDmdReqProdRule();
            test.findDmdReqProdRule();
            test.saveDmdReqProdRule();
            test.findAllDmdReqProdRules();
            
            try
            {
                test.delDmdReqProdRule();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delDmdReqProdRule:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delDmdReqProdRule: " + e.Message);
            }
            
        }
		[Test]
		public void GetAllForDmdType()
		{
			
			DmdReqProdRule[][] rules = DmdReqProdRule.GetRules(new UOW(), DemandType.New);
			Assertion.Assert(rules.Length > 0);
			Assertion.Assert(rules[0].Length > 0);
			
			
		}

        [Test]
        public void addDmdReqProdRule()
        {
            UOW uow = new UOW();
            uow.Service = "addDmdReqProdRule";
            DmdReqProdRule cls = new DmdReqProdRule(uow);
            
            cls.DmdType = this.dmdType;
            cls.RuleName = this.ruleName;
            cls.ReqProd = this.reqProd;
            cls.EffStartDate = this.effStartDate;
            cls.EffEndDate = this.effEndDate;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addDmdReqProdRule - assert";
            cls = DmdReqProdRule.find(uow, this.id);
            Assertion.Assert(cls.RuleName == this.ruleName);
            uow.close();
        }
        [Test]
        public void findDmdReqProdRule()
        {
            UOW uow = new UOW();
            uow.Service = "findDmdReqProdRule";
            
            DmdReqProdRule cls = DmdReqProdRule.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveDmdReqProdRule()
        {
            UOW uow = new UOW();
            uow.Service = "saveDmdReqProdRule";
            DmdReqProdRule cls = DmdReqProdRule.find(uow, this.id);
            
            cls.DmdType = this.dmdType;
            cls.RuleName = this.ruleName;
            cls.ReqProd = this.reqProd;
            cls.EffStartDate = this.effStartDate;
            cls.EffEndDate = this.effEndDate;
            cls.DmdType = DemandType.NewPymt;
            this.dmdType = cls.DmdType;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveDmdReqProdRule - assert";
            
            cls = DmdReqProdRule.find(uow, this.id);
            Assertion.Assert(cls.DmdType == this.dmdType);
            uow.close();
        }
        [Test]
        public void findAllDmdReqProdRules()
        {
            UOW uow = new UOW();
            uow.Service = "findAllDmdReqProdRules";
            DmdReqProdRule[] objs = DmdReqProdRule.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delDmdReqProdRule()
        {
            UOW uow = new UOW();
            uow.Service = "delDmdReqProdRule";
            DmdReqProdRule cls = DmdReqProdRule.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delDmdReqProdRule - assert";
            cls = DmdReqProdRule.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
