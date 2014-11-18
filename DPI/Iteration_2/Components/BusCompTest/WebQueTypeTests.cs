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
    public class WebQueTypeTests
    {
        /*		Data		*/
        string queType = "queType";
        bool isReversal = true;
        bool isPost = true;
        bool isReadOnly = true;
        
        /*		Constructors		*/
        public WebQueTypeTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            WebQueTypeTests test = new WebQueTypeTests();
            
            // UOW Tests
            test.addWebQueType();
            test.findWebQueType();
            test.saveWebQueType();
            test.findAllWebQueTypes();
            
            try
            {
                test.delWebQueType();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delWebQueType:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delWebQueType: " + e.Message);
            }
            
        }
        [Test]
        public void addWebQueType()
        {
            UOW uow = new UOW();
            uow.Service = "addWebQueType";
            WebQueType cls = new WebQueType(uow);
            
            cls.QueType = this.queType;
            cls.IsReversal = this.isReversal;
            cls.IsPost = this.isPost;
            cls.IsReadOnly = this.isReadOnly;
        
            uow.commit();
            
            uow = new UOW();
            uow.Service = "addWebQueType - assert";
            cls = WebQueType.find(uow, this.queType);
            Assertion.Assert(cls.IsPost == this.isPost);
            uow.close();
        }
        [Test]
        public void findWebQueType()
        {
            UOW uow = new UOW();
            uow.Service = "findWebQueType";
            
            WebQueType cls = WebQueType.find(uow, this.queType);
            Assertion.Assert(cls.QueType.Trim() == this.queType.Trim());
            uow.close();
        }
        [Test]
        public void saveWebQueType()
        {
            UOW uow = new UOW();
            uow.Service = "saveWebQueType";
            WebQueType cls = WebQueType.find(uow, this.queType);
            
            cls.QueType = this.queType;
            cls.IsReversal = this.isReversal;
            cls.IsPost = this.isPost;
            cls.IsReadOnly = this.isReadOnly;
            cls.IsReversal = false;
            this.isReversal = cls.IsReversal;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveWebQueType - assert";
            
            cls = WebQueType.find(uow, this.queType);
            Assertion.Assert(cls.IsReversal == this.isReversal);
            uow.close();
        }
        [Test]
        public void findAllWebQueTypes()
        {
            UOW uow = new UOW();
            uow.Service = "findAllWebQueTypes";
            WebQueType[] objs = WebQueType.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delWebQueType()
        {
            UOW uow = new UOW();
            uow.Service = "delWebQueType";
            WebQueType cls = WebQueType.find(uow, this.queType);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delWebQueType - assert";
            cls = WebQueType.find(uow, this.queType);
            Assertion.Assert((cls.QueType ==  null));
            uow.close();
        }
    }
}
