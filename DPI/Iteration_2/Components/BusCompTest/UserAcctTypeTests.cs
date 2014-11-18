//using System;
//using System.Data;
//using System.Data.SqlClient;
//using NUnit.Framework;
//using DPI.Components;
//using DPI.Interfaces;
//
//namespace DPI.ComponentsTests
//{
//    [TestFixture]
//    public class UserAcctTypeTests
//    {
//        /*		Data		*/
////        string acctType = "acctType";
////        bool isAutoLoginOnly = true;
//        //string description = "description";
//
//		string acctType = "AutoLogin2";
//		string isAutoLoginOnly = "T";
//		string isStoreBased = "T";
//		bool requestClerkId = true;
//        
//        /*		Constructors		*/
//        public UserAcctTypeTests()
//        {
//            // try { cleanup(); } 	catch {}
//            // Console.WriteLine("Cleanup completed");
//        }
//        
//        /*		Methods		*/
//        public static void Main()
//        {
//            UserAcctTypeTests test = new UserAcctTypeTests();
//            
//            // UOW Tests
//           // test.addUserAcctType();
//            test.findUserAcctType();
//            test.saveUserAcctType();
//            test.findAllUserAcctTypes();
//            
//            try
//            {
//                test.delUserAcctType();
//            }
//            catch(ArgumentException ae)
//            {
//                Console.WriteLine("Expected exception: delUserAcctType:" + ae.Message);
//            }
//            catch(Exception e)
//            {
//                Console.WriteLine("Error: delUserAcctType: " + e.Message);
//            }
//            
//        }
//        [Test]
//        public void addUserAcctType()
//        {
//            UOW uow = new UOW();
//            uow.Service = "addUserAcctType";
//            UserAcctType cls = new UserAcctType(uow);
//            
//            cls.AcctType = this.acctType;
//            cls.IsAutoLoginOnly = this.isAutoLoginOnly;
//			cls.IsStoreBased  = this.isStoreBased;
//			cls.RequestClerkId = this.requestClerkId;
//          //  cls.Description = this.description;
//        
//            uow.commit();
//            
//            uow = new UOW();
//            uow.Service = "addUserAcctType - assert";
//            cls = UserAcctType.find(uow, this.acctType);
//            Assertion.AssertNotNull(cls);
//			Assertion.Assert(cls.RequestClerkId == this.requestClerkId);
//            uow.close();
//        }
//        [Test]
//        public void findUserAcctType()
//        {
//            UOW uow = new UOW();
//            uow.Service = "findUserAcctType";
//            
//            UserAcctType cls = UserAcctType.find(uow, this.acctType);
//            Assertion.Assert(cls.AcctType.Trim() == this.acctType.Trim());
//            uow.close();
//        }
//        [Test]
//        public void saveUserAcctType()
//        {
//            UOW uow = new UOW();
//            uow.Service = "saveUserAcctType";
//            UserAcctType cls = UserAcctType.find(uow, this.acctType);
//            
//            cls.AcctType = this.acctType;
//            cls.IsAutoLoginOnly = this.isAutoLoginOnly;
//			cls.IsAutoLoginOnly = this.isAutoLoginOnly;
//			cls.IsStoreBased  = this.isStoreBased;
//			cls.RequestClerkId = this.requestClerkId;
//        //   cls.Description = this.description;
//           // cls.IsAutoLoginOnly = false;
//            this.isAutoLoginOnly = cls.IsAutoLoginOnly;
//                
//            uow.commit();
//            
//            uow = new UOW();
//            uow.Service = "saveUserAcctType - assert";
//            
//            cls = UserAcctType.find(uow, this.acctType);
//            Assertion.Assert(cls.IsAutoLoginOnly == this.isAutoLoginOnly);
//            uow.close();
//        }
//        [Test]
//        public void findAllUserAcctTypes()
//        {
//            UOW uow = new UOW();
//            uow.Service = "findAllUserAcctTypes";
//            UserAcctType[] objs = UserAcctType.getAll(uow);
//            Assertion.Assert(objs.Length > 0);
//            uow.close(); 
//        }
//        [Test]
//        [ExpectedException(typeof(ArgumentException))]
//        public void delUserAcctType()
//        {
//            UOW uow = new UOW();
//            uow.Service = "delUserAcctType";
//            UserAcctType cls = UserAcctType.find(uow, this.acctType);
//            cls.delete();
//            
//            uow.commit();
//            
//            uow = new UOW();
//            uow.Service = "delUserAcctType - assert";
//            cls = UserAcctType.find(uow, this.acctType);
//            Assertion.Assert((cls.AcctType ==  null));
//            uow.close();
//        }
//    }
//}
