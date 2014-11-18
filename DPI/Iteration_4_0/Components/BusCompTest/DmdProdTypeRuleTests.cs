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
    public class DmdProdTypeRuleTests
    {
        /*		Data		*/
        int id;
        string dmdType = "dmdType";
        string prodType = "prodType";
        
        /*		Constructors		*/
        public DmdProdTypeRuleTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            DmdProdTypeRuleTests test = new DmdProdTypeRuleTests();
            
            // UOW Tests
            test.addDmdProdTypeRule();
            test.findDmdProdTypeRule();
            test.saveDmdProdTypeRule();
            test.findAllDmdProdTypeRules();
            
            try
            {
                test.delDmdProdTypeRule();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delDmdProdTypeRule:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delDmdProdTypeRule: " + e.Message);
            }
            
        }
        [Test]
        public void addDmdProdTypeRule()
        {
            UOW uow = new UOW();
            uow.Service = "addDmdProdTypeRule";
            DmdProdTypeRule cls = new DmdProdTypeRule(uow);
            
            cls.DmdType = this.dmdType;
            cls.ProdType = this.prodType;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addDmdProdTypeRule - assert";
            cls = DmdProdTypeRule.find(uow, this.id);
            Assertion.Assert(cls.ProdType == this.prodType);
            uow.close();
        }
        [Test]
        public void findDmdProdTypeRule()
        {
            UOW uow = new UOW();
            uow.Service = "findDmdProdTypeRule";
            
            DmdProdTypeRule cls = DmdProdTypeRule.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveDmdProdTypeRule()
        {
            UOW uow = new UOW();
            uow.Service = "saveDmdProdTypeRule";
            DmdProdTypeRule cls = DmdProdTypeRule.find(uow, this.id);
            
            cls.DmdType = this.dmdType;
            cls.ProdType = this.prodType;
            cls.DmdType += " saved";
            this.dmdType = cls.DmdType;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveDmdProdTypeRule - assert";
            
            cls = DmdProdTypeRule.find(uow, this.id);
            Assertion.Assert(cls.DmdType == this.dmdType);
            uow.close();
        }
        [Test]
        public void findAllDmdProdTypeRules()
        {
            UOW uow = new UOW();
            uow.Service = "findAllDmdProdTypeRules";
            DmdProdTypeRule[] objs = DmdProdTypeRule.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delDmdProdTypeRule()
        {
            UOW uow = new UOW();
            uow.Service = "delDmdProdTypeRule";
            DmdProdTypeRule cls = DmdProdTypeRule.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delDmdProdTypeRule - assert";
            cls = DmdProdTypeRule.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
