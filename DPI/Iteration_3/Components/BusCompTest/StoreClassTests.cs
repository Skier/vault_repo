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
    public class StoreClassTests
    {
        /*		Data		*/
        string storeCls = "storeCls";
        bool isDirectSeller = true;
        bool isPymtStation = true;
        bool isPriceLookup = true;
        string description = "description";
        
        /*		Constructors		*/
        public StoreClassTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            StoreClassTests test = new StoreClassTests();
            
            // UOW Tests
            test.addStoreClass();
            test.findStoreClass();
            test.saveStoreClass();
            test.findAllStoreClasses();
            
            try
            {
                test.delStoreClass();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delStoreClass:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delStoreClass: " + e.Message);
            }
            
        }
        [Test]
        public void addStoreClass()
        {
            UOW uow = new UOW();
            uow.Service = "addStoreClass";
            StoreClass cls = new StoreClass(uow);
            
            cls.StoreCls = this.storeCls;
            cls.IsDirectSeller = this.isDirectSeller;
            cls.IsPymtStation = this.isPymtStation;
            cls.IsPriceLookup = this.isPriceLookup;
            cls.Description = this.description;
        
            uow.commit();
            
            uow = new UOW();
            uow.Service = "addStoreClass - assert";
            cls = StoreClass.find(uow, this.storeCls);
            Assertion.Assert(cls.IsPymtStation == this.isPymtStation);
            uow.close();
        }
        [Test]
        public void findStoreClass()
        {
            UOW uow = new UOW();
            uow.Service = "findStoreClass";
            
            StoreClass cls = StoreClass.find(uow, this.storeCls);
            Assertion.Assert(cls.StoreCls.Trim() == this.storeCls.Trim());
            uow.close();
        }
        [Test]
        public void saveStoreClass()
        {
            UOW uow = new UOW();
            uow.Service = "saveStoreClass";
            StoreClass cls = StoreClass.find(uow, this.storeCls);
            
            cls.StoreCls = this.storeCls;
            cls.IsDirectSeller = this.isDirectSeller;
            cls.IsPymtStation = this.isPymtStation;
            cls.IsPriceLookup = this.isPriceLookup;
            cls.Description = this.description;
            cls.IsDirectSeller = false;
            this.isDirectSeller = cls.IsDirectSeller;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveStoreClass - assert";
            
            cls = StoreClass.find(uow, this.storeCls);
            Assertion.Assert(cls.IsDirectSeller == this.isDirectSeller);
            uow.close();
        }
        [Test]
        public void findAllStoreClasses()
        {
            UOW uow = new UOW();
            uow.Service = "findAllStoreClasses";
            StoreClass[] objs = StoreClass.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delStoreClass()
        {
            UOW uow = new UOW();
            uow.Service = "delStoreClass";
            StoreClass cls = StoreClass.find(uow, this.storeCls);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delStoreClass - assert";
            cls = StoreClass.find(uow, this.storeCls);
            Assertion.Assert((cls.StoreCls ==  null));
            uow.close();
        }
    }
}
