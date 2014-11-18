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
    public class CatMappingRuleTests
    {
        /*		Data		*/
        int id;
        string fromDomain = "fromDomain";
        string fromCategory = "fromCategory";
        string fromValue = "fromValue";
        string toDomain = "toDomain";
        string toCategory = "toCategory";
        string toValue = "toValue";
        DateTime startEffDate = DateTime.Now;
        DateTime endEffDate = DateTime.Now;
        string status = "status";
        
        /*		Constructors		*/
        public CatMappingRuleTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            CatMappingRuleTests test = new CatMappingRuleTests();
            
            // UOW Tests
            test.addCatMappingRule();
            test.findCatMappingRule();
            test.saveCatMappingRule();
            test.findAllCatMappingRules();
            
            try
            {
                test.delCatMappingRule();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delCatMappingRule:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delCatMappingRule: " + e.Message);
            }
            
        }
        [Test]
        public void addCatMappingRule()
        {
            UOW uow = new UOW();
            uow.Service = "addCatMappingRule";
            CatMappingRule cls = new CatMappingRule(uow);
            
            cls.FromDomain = this.fromDomain;
            cls.FromCategory = this.fromCategory;
            cls.FromValue = this.fromValue;
            cls.ToDomain = this.toDomain;
            cls.ToCategory = this.toCategory;
            cls.ToValue = this.toValue;
            cls.StartEffDate = this.startEffDate;
            cls.EndEffDate = this.endEffDate;
            cls.Status = this.status;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addCatMappingRule - assert";
            cls = CatMappingRule.find(uow, this.id);
            Assertion.Assert(cls.FromCategory == this.fromCategory);
            uow.close();
        }
        [Test]
        public void findCatMappingRule()
        {
            UOW uow = new UOW();
            uow.Service = "findCatMappingRule";
            
            CatMappingRule cls = CatMappingRule.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveCatMappingRule()
        {
            UOW uow = new UOW();
            uow.Service = "saveCatMappingRule";
            CatMappingRule cls = CatMappingRule.find(uow, this.id);
            
            cls.FromDomain = this.fromDomain;
            cls.FromCategory = this.fromCategory;
            cls.FromValue = this.fromValue;
            cls.ToDomain = this.toDomain;
            cls.ToCategory = this.toCategory;
            cls.ToValue = this.toValue;
            cls.StartEffDate = this.startEffDate;
            cls.EndEffDate = this.endEffDate;
            cls.Status = this.status;
            cls.FromDomain += " saved";
            this.fromDomain = cls.FromDomain;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveCatMappingRule - assert";
            
            cls = CatMappingRule.find(uow, this.id);
            Assertion.Assert(cls.FromDomain == this.fromDomain);
            uow.close();
        }
        [Test]
        public void findAllCatMappingRules()
        {
            UOW uow = new UOW();
            uow.Service = "findAllCatMappingRules";
            CatMappingRule[] objs = CatMappingRule.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delCatMappingRule()
        {
            UOW uow = new UOW();
            uow.Service = "delCatMappingRule";
            CatMappingRule cls = CatMappingRule.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delCatMappingRule - assert";
            cls = CatMappingRule.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
