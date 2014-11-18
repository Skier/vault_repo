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
    public class UserAccountTests
    {
        /*		Data		*/
        int acctId;
        string acctType = "acctType";
        string acctName = "acctName";
        string password = "password";
        string storeCode = "storeCode";
        int corpId = 6;
        string clerkId = "clerkId";
        string displayName = "displayName";
        string jobTitle = "jobTitle";
        string status = "status";
        
        /*		Constructors		*/
        public UserAccountTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            UserAccountTests test = new UserAccountTests();
            
            // UOW Tests
            test.addUserAccount();
            test.findUserAccount();
            test.saveUserAccount();
            test.findAllUserAccounts();
            
            try
            {
                test.delUserAccount();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delUserAccount:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delUserAccount: " + e.Message);
            }
            
        }
        [Test]
        public void addUserAccount()
        {
            UOW uow = new UOW();
            uow.Service = "addUserAccount";
            UserAccount cls = new UserAccount(uow);
            
            cls.AcctType = this.acctType;
            cls.AcctName = this.acctName;
            cls.Password = this.password;
            cls.StoreCode = this.storeCode;
            cls.CorpId = this.corpId;
            cls.ClerkId = this.clerkId;
            cls.DisplayName = this.displayName;
            cls.JobTitle = this.jobTitle;
            cls.Status = this.status;
        
            uow.commit();
            this.acctId = cls.AcctId;
            
            uow = new UOW();
            uow.Service = "addUserAccount - assert";
            cls = UserAccount.find(uow, this.acctId);
            Assertion.Assert(cls.AcctName == this.acctName);
            uow.close();
        }
        [Test]
        public void findUserAccount()
        {
            UOW uow = new UOW();
            uow.Service = "findUserAccount";
            
            UserAccount cls = UserAccount.find(uow, this.acctId);
            Assertion.Assert(cls.AcctId == this.acctId);
            uow.close();
        }
        [Test]
        public void saveUserAccount()
        {
            UOW uow = new UOW();
            uow.Service = "saveUserAccount";
            UserAccount cls = UserAccount.find(uow, this.acctId);
            
            cls.AcctType = this.acctType;
            cls.AcctName = this.acctName;
            cls.Password = this.password;
            cls.StoreCode = this.storeCode;
            cls.CorpId = this.corpId;
            cls.ClerkId = this.clerkId;
            cls.DisplayName = this.displayName;
            cls.JobTitle = this.jobTitle;
            cls.Status = this.status;
            cls.AcctType += " saved";
            this.acctType = cls.AcctType;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveUserAccount - assert";
            
            cls = UserAccount.find(uow, this.acctId);
            Assertion.Assert(cls.AcctType == this.acctType);
            uow.close();
        }
        [Test]
        public void findAllUserAccounts()
        {
            UOW uow = new UOW();
            uow.Service = "findAllUserAccounts";
            UserAccount[] objs = UserAccount.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delUserAccount()
        {
            UOW uow = new UOW();
            uow.Service = "delUserAccount";
            UserAccount cls = UserAccount.find(uow, this.acctId);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delUserAccount - assert";
            cls = UserAccount.find(uow, this.acctId);
            Assertion.Assert((cls.AcctId == 0));
            uow.close();
        }
    }
}
