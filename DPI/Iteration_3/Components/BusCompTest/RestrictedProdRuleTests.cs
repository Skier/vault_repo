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
    public class RestrictedProdRuleTests
    {
        /*		Data		*/
        int id;
        string name = "name";
        int enabledProd = 3;
        string criteria = "criteria";
        DateTime startDate = DateTime.Now;
        DateTime endDate = DateTime.Now;
        string status = "status";
        string description = "description";
        
        /*		Constructors		*/
        public RestrictedProdRuleTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            RestrictedProdRuleTests test = new RestrictedProdRuleTests();
            
            // UOW Tests
            test.addRestrictedProdRule();
            test.findRestrictedProdRule();
            test.saveRestrictedProdRule();
            test.findAllRestrictedProdRules();
            
            try
            {
                test.delRestrictedProdRule();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delRestrictedProdRule:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delRestrictedProdRule: " + e.Message);
            }
            
        }
        [Test]
        public void addRestrictedProdRule()
        {
            UOW uow = new UOW();
            uow.Service = "addRestrictedProdRule";
            RestrictedProdRule cls = new RestrictedProdRule(uow);
            
            cls.Name = this.name;
            cls.EnabledProd = this.enabledProd;
            cls.Criteria = this.criteria;
            cls.StartDate = this.startDate;
            cls.EndDate = this.endDate;
            cls.Status = this.status;
            cls.Description = this.description;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addRestrictedProdRule - assert";
            cls = RestrictedProdRule.find(uow, this.id);
            Assertion.Assert(cls.EnabledProd == this.enabledProd);
            uow.close();
        }
        [Test]
        public void findRestrictedProdRule()
        {
            UOW uow = new UOW();
            uow.Service = "findRestrictedProdRule";
            
            RestrictedProdRule cls = RestrictedProdRule.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveRestrictedProdRule()
        {
            UOW uow = new UOW();
            uow.Service = "saveRestrictedProdRule";
            RestrictedProdRule cls = RestrictedProdRule.find(uow, this.id);
            
            cls.Name = this.name;
            cls.EnabledProd = this.enabledProd;
            cls.Criteria = this.criteria;
            cls.StartDate = this.startDate;
            cls.EndDate = this.endDate;
            cls.Status = this.status;
            cls.Description = this.description;
            cls.Name += " saved";
            this.name = cls.Name;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveRestrictedProdRule - assert";
            
            cls = RestrictedProdRule.find(uow, this.id);
            Assertion.Assert(cls.Name == this.name);
            uow.close();
        }
        [Test]
        public void findAllRestrictedProdRules()
        {
            UOW uow = new UOW();
            uow.Service = "findAllRestrictedProdRules";
            RestrictedProdRule[] objs = RestrictedProdRule.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delRestrictedProdRule()
        {
            UOW uow = new UOW();
            uow.Service = "delRestrictedProdRule";
            RestrictedProdRule cls = RestrictedProdRule.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delRestrictedProdRule - assert";
            cls = RestrictedProdRule.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
