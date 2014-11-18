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
    public class PymtTypeRuleTests
    {
        /*		Data		*/
        int id;
        string pymtRule = "pymtRule";
        string pymtType = "pymtType";
        
        /*		Constructors		*/
        public PymtTypeRuleTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            PymtTypeRuleTests test = new PymtTypeRuleTests();
            
            // UOW Tests
            test.addPymtTypeRule();
            test.findPymtTypeRule();
            test.savePymtTypeRule();
            test.findAllPymtTypeRules();
            
            try
            {
                test.delPymtTypeRule();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delPymtTypeRule:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delPymtTypeRule: " + e.Message);
            }
            
        }
        [Test]
        public void addPymtTypeRule()
        {
            UOW uow = new UOW();
            uow.Service = "addPymtTypeRule";
            PymtTypeRule cls = new PymtTypeRule(uow);
            
            cls.PymtRule = this.pymtRule;
            cls.PymtType = this.pymtType;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addPymtTypeRule - assert";
            cls = PymtTypeRule.find(uow, this.id);
            Assertion.Assert(cls.PymtType == this.pymtType);
            uow.close();
        }
        [Test]
        public void findPymtTypeRule()
        {
            UOW uow = new UOW();
            uow.Service = "findPymtTypeRule";
            
            PymtTypeRule cls = PymtTypeRule.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void savePymtTypeRule()
        {
            UOW uow = new UOW();
            uow.Service = "savePymtTypeRule";
            PymtTypeRule cls = PymtTypeRule.find(uow, this.id);
            
            cls.PymtRule = this.pymtRule;
            cls.PymtType = this.pymtType;
            cls.PymtRule += " saved";
            this.pymtRule = cls.PymtRule;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "savePymtTypeRule - assert";
            
            cls = PymtTypeRule.find(uow, this.id);
            Assertion.Assert(cls.PymtRule == this.pymtRule);
            uow.close();
        }
        [Test]
        public void findAllPymtTypeRules()
        {
            UOW uow = new UOW();
            uow.Service = "findAllPymtTypeRules";
            PymtTypeRule[] objs = PymtTypeRule.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delPymtTypeRule()
        {
            UOW uow = new UOW();
            uow.Service = "delPymtTypeRule";
            PymtTypeRule cls = PymtTypeRule.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delPymtTypeRule - assert";
            cls = PymtTypeRule.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
