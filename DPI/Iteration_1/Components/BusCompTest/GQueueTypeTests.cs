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
    public class GQueueTypeTests
    {
        /*		Data		*/
        string gQueType = "gQueType";
        bool isEvergreen = true;
        bool isToComplition = true;
        bool isOneShot = true;
        
        /*		Constructors		*/
        public GQueueTypeTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            GQueueTypeTests test = new GQueueTypeTests();
            
            // UOW Tests
            test.addGQueueType();
            test.findGQueueType();
            test.saveGQueueType();
            test.findAllGQueueTypes();
            
            try
            {
                test.delGQueueType();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delGQueueType:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delGQueueType: " + e.Message);
            }
            
        }
        [Test]
        public void addGQueueType()
        {
            UOW uow = new UOW();
            uow.Service = "addGQueueType";
            GQueueType cls = new GQueueType(uow);
            
            cls.GQueType = this.gQueType;
            cls.IsEvergreen = this.isEvergreen;
            cls.IsToComplition = this.isToComplition;
            cls.IsOneShot = this.isOneShot;
        
            uow.commit();
            
            uow = new UOW();
            uow.Service = "addGQueueType - assert";
            cls = GQueueType.find(uow, this.gQueType);
            Assertion.Assert(cls.IsToComplition == this.isToComplition);
            uow.close();
        }
        [Test]
        public void findGQueueType()
        {
            UOW uow = new UOW();
            uow.Service = "findGQueueType";
            
            GQueueType cls = GQueueType.find(uow, this.gQueType);
            Assertion.Assert(cls.GQueType.Trim() == this.gQueType.Trim());
            uow.close();
        }
        [Test]
        public void saveGQueueType()
        {
            UOW uow = new UOW();
            uow.Service = "saveGQueueType";
            GQueueType cls = GQueueType.find(uow, this.gQueType);
            
            cls.GQueType = this.gQueType;
            cls.IsEvergreen = this.isEvergreen;
            cls.IsToComplition = this.isToComplition;
            cls.IsOneShot = this.isOneShot;
            cls.IsEvergreen = false;
            this.isEvergreen = cls.IsEvergreen;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveGQueueType - assert";
            
            cls = GQueueType.find(uow, this.gQueType);
            Assertion.Assert(cls.IsEvergreen == this.isEvergreen);
            uow.close();
        }
        [Test]
        public void findAllGQueueTypes()
        {
            UOW uow = new UOW();
            uow.Service = "findAllGQueueTypes";
            GQueueType[] objs = GQueueType.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delGQueueType()
        {
            UOW uow = new UOW();
            uow.Service = "delGQueueType";
            GQueueType cls = GQueueType.find(uow, this.gQueType);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delGQueueType - assert";
            cls = GQueueType.find(uow, this.gQueType);
            Assertion.Assert((cls.GQueType ==  null));
            uow.close();
        }
    }
}
