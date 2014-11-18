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
    public class UserAccountExtensionTests
    {
        /*		Data		*/
        int id;
        int acctId = 2;
        string userName = "userName";
        string password = "password";
        string entityName = "entityName";
        string applicationName = "applicationName";
        string url = "url";
        
        /*		Constructors		*/
        public UserAccountExtensionTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            UserAccountExtensionTests test = new UserAccountExtensionTests();
            
            // UOW Tests
            test.addUserAccountExtension();
            test.findUserAccountExtension();
            test.saveUserAccountExtension();
            test.findAllUserAccountExtensions();
            
            try
            {
                test.delUserAccountExtension();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delUserAccountExtension:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delUserAccountExtension: " + e.Message);
            }
            
        }
        [Test]
        public void addUserAccountExtension()
        {
            UOW uow = new UOW();
            uow.Service = "addUserAccountExtension";
            UserAccountExtension cls = new UserAccountExtension(uow);
            
            cls.AcctId = this.acctId;
            cls.UserName = this.userName;
            cls.Password = this.password;
            cls.EntityName = this.entityName;
            cls.ApplicationName = this.applicationName;
            cls.Url = this.url;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addUserAccountExtension - assert";
            cls = UserAccountExtension.find(uow, this.id);
            Assertion.Assert(cls.UserName == this.userName);
            uow.close();
        }
        [Test]
        public void findUserAccountExtension()
        {
            UOW uow = new UOW();
            uow.Service = "findUserAccountExtension";
            
            UserAccountExtension cls = UserAccountExtension.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveUserAccountExtension()
        {
            UOW uow = new UOW();
            uow.Service = "saveUserAccountExtension";
            UserAccountExtension cls = UserAccountExtension.find(uow, this.id);
            
            cls.AcctId = this.acctId;
            cls.UserName = this.userName;
            cls.Password = this.password;
            cls.EntityName = this.entityName;
            cls.ApplicationName = this.applicationName;
            cls.Url = this.url;
            cls.AcctId += 2;
            this.acctId = cls.AcctId;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveUserAccountExtension - assert";
            
            cls = UserAccountExtension.find(uow, this.id);
            Assertion.Assert(cls.AcctId == this.acctId);
            uow.close();
        }
        [Test]
        public void findAllUserAccountExtensions()
        {
            UOW uow = new UOW();
            uow.Service = "findAllUserAccountExtensions";
            UserAccountExtension[] objs = UserAccountExtension.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delUserAccountExtension()
        {
            UOW uow = new UOW();
            uow.Service = "delUserAccountExtension";
            UserAccountExtension cls = UserAccountExtension.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delUserAccountExtension - assert";
            cls = UserAccountExtension.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
