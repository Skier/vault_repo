using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.ComponentsTests
{
    [TestFixture]
    public class PermissionRuleTests
    {
        /*		Data		*/
        int id;
        string status = "status";
        DateTime startingDate = DateTime.Now;
        DateTime endDate      = DateTime.Now;

		string isDirectSeller       = "T";
        string isPymtStation        = "T";
        string isPriceLookup        = "T";
        string isLocalServiceSeller = "T";
        string isInternetSeller     = "T";
        string isWirelessSeller     = "T";
        
		//string jobTitle   = "jobTitle";
     //   string acctType   = "acctType";
        string permission = "Test";
        
        /*		Constructors		*/
        public PermissionRuleTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            PermissionRuleTests test = new PermissionRuleTests();
            
            // UOW Tests
            test.addPermissionRule();
            test.findPermissionRule();
            test.savePermissionRule();
            test.findAllPermissionRules();
            
            try
            {
                test.delPermissionRule();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delPermissionRule:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delPermissionRule: " + e.Message);
            }
            
        }
        [Test]
        public void addPermissionRule()
        {
            UOW uow = new UOW();
            uow.Service = "addPermissionRule";
            PermissionRule cls = new PermissionRule(uow);
            
            cls.Status = this.status;
            cls.StartingDate = this.startingDate;
            cls.EndDate = this.endDate;
            cls.IsDirectSeller = this.isDirectSeller;
            cls.IsPymtStation = this.isPymtStation;
            cls.IsPriceLookup = this.isPriceLookup;
            cls.IsLocalServiceSeller = this.isLocalServiceSeller;
            cls.IsInternetSeller = this.isInternetSeller;
            cls.IsWirelessSeller = this.isWirelessSeller;
     //       cls.JobTitle = this.jobTitle;
          //  cls.AcctType = this.acctType;
            cls.Permisn = this.permission;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addPermissionRule - assert";
            cls = PermissionRule.find(uow, this.id);
            Assertion.Assert(cls.IsDirectSeller == this.isDirectSeller);
            uow.close();
        }
        [Test]
        public void findPermissionRule()
        {
            UOW uow = new UOW();
            uow.Service = "findPermissionRule";
            
            PermissionRule cls = PermissionRule.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void savePermissionRule()
        {
            UOW uow = new UOW();
            uow.Service = "savePermissionRule";
            PermissionRule cls = PermissionRule.find(uow, this.id);
            
			this.status = "Inactive";
            cls.Status = this.status;
                           
            uow.commit();
            
            uow = new UOW();
            uow.Service = "savePermissionRule - assert";
            
            cls = PermissionRule.find(uow, this.id);
            Assertion.Assert(cls.Status.Trim() == this.status);
            uow.close();
        }
        [Test]
        public void findAllPermissionRules()
        {
            UOW uow = new UOW();
            uow.Service = "findAllPermissionRules";
            PermissionRule[] objs = PermissionRule.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delPermissionRule()
        {
            UOW uow = new UOW();
            uow.Service = "delPermissionRule";
            PermissionRule cls = PermissionRule.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delPermissionRule - assert";
            cls = PermissionRule.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
