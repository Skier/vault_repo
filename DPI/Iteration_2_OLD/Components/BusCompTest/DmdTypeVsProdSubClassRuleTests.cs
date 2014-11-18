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
    public class DmdTypeVsProdSubClassRuleTests
    {
        /*		Data		*/
        int id;
        string dmdType = "dmdType";
        string prodSubClass = "prodSubClass";
        bool isRequired = true;
        DateTime effStartDate = DateTime.Now;
        DateTime effEndDate = DateTime.Now;
        
        /*		Constructors		*/
        public DmdTypeVsProdSubClassRuleTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            DmdTypeVsProdSubClassRuleTests test = new DmdTypeVsProdSubClassRuleTests();
            
            // UOW Tests
            test.addDmdTypeVsProdSubClassRule();
            test.findDmdTypeVsProdSubClassRule();
            test.saveDmdTypeVsProdSubClassRule();
            test.findAllDmdTypeVsProdSubClassRules();
            
            try
            {
                test.delDmdTypeVsProdSubClassRule();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delDmdTypeVsProdSubClassRule:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delDmdTypeVsProdSubClassRule: " + e.Message);
            }
            
        }
        [Test]
        public void addDmdTypeVsProdSubClassRule()
        {
            UOW uow = new UOW();
            uow.Service = "addDmdTypeVsProdSubClassRule";
            DmdTypeVsProdSubClassRule cls = new DmdTypeVsProdSubClassRule(uow);
            
            cls.DmdType = this.dmdType;
            cls.ProdSubClass = this.prodSubClass;
            cls.IsRequired = this.isRequired;
            cls.EffStartDate = this.effStartDate;
            cls.EffEndDate = this.effEndDate;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addDmdTypeVsProdSubClassRule - assert";
            cls = DmdTypeVsProdSubClassRule.find(uow, this.id);
            Assertion.Assert(cls.ProdSubClass == this.prodSubClass);
            uow.close();
        }
        [Test]
        public void findDmdTypeVsProdSubClassRule()
        {
            UOW uow = new UOW();
            uow.Service = "findDmdTypeVsProdSubClassRule";
            
            DmdTypeVsProdSubClassRule cls = DmdTypeVsProdSubClassRule.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveDmdTypeVsProdSubClassRule()
        {
            UOW uow = new UOW();
            uow.Service = "saveDmdTypeVsProdSubClassRule";
            DmdTypeVsProdSubClassRule cls = DmdTypeVsProdSubClassRule.find(uow, this.id);
            
            cls.DmdType = this.dmdType;
            cls.ProdSubClass = this.prodSubClass;
            cls.IsRequired = this.isRequired;
            cls.EffStartDate = this.effStartDate;
            cls.EffEndDate = this.effEndDate;
            cls.DmdType += " saved";
            this.dmdType = cls.DmdType;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveDmdTypeVsProdSubClassRule - assert";
            
            cls = DmdTypeVsProdSubClassRule.find(uow, this.id);
            Assertion.Assert(cls.DmdType == this.dmdType);
            uow.close();
        }
        [Test]
        public void findAllDmdTypeVsProdSubClassRules()
        {
            UOW uow = new UOW();
            uow.Service = "findAllDmdTypeVsProdSubClassRules";
            DmdTypeVsProdSubClassRule[] objs = DmdTypeVsProdSubClassRule.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delDmdTypeVsProdSubClassRule()
        {
            UOW uow = new UOW();
            uow.Service = "delDmdTypeVsProdSubClassRule";
            DmdTypeVsProdSubClassRule cls = DmdTypeVsProdSubClassRule.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delDmdTypeVsProdSubClassRule - assert";
            cls = DmdTypeVsProdSubClassRule.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
