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
    public class GQueueTests
    {
        /*		Data		*/
        int id;
        string gQueType = "gQueType";
        string svcProvider = "svcProvider";
        string method = "method";
        string xmlMessage = "xmlMessage";
        string agent = "agent";
        string clerkid = "clerkid";
        int predecessor = 8;
        string initiator = "initiator";
        DateTime startDateTime = DateTime.Now;
        DateTime lastDateTime = DateTime.Now;
        int timeInterval = 12;
        DateTime expires = DateTime.Now;
        int accessCnt = 14;
        int maxCnt = 15;
        string status = "status";
        
        /*		Constructors		*/
        public GQueueTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            GQueueTests test = new GQueueTests();
            
            // UOW Tests
            test.addGQueue();
            test.findGQueue();
            test.saveGQueue();
            test.findAllGQueues();
            
            try
            {
                test.delGQueue();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delGQueue:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delGQueue: " + e.Message);
            }
            
        }
        [Test]
        public void addGQueue()
        {
            UOW uow = new UOW();
            uow.Service = "addGQueue";
            GQueue cls = new GQueue(uow);
            
            cls.GQueType = this.gQueType;
            cls.SvcProvider = this.svcProvider;
            cls.Method = this.method;
            cls.XmlMessage = this.xmlMessage;
            cls.Agent = this.agent;
            cls.Clerkid = this.clerkid;
            cls.Predecessor = this.predecessor;
            cls.Initiator = this.initiator;
            cls.StartDateTime = this.startDateTime;
            cls.LastDateTime = this.lastDateTime;
            cls.TimeInterval = this.timeInterval;
            cls.Expires = this.expires;
            cls.AccessCnt = this.accessCnt;
            cls.MaxCnt = this.maxCnt;
            cls.Status = this.status;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addGQueue - assert";
            cls = GQueue.find(uow, this.id);
            Assertion.Assert(cls.SvcProvider == this.svcProvider);
            uow.close();
        }
        [Test]
        public void findGQueue()
        {
            UOW uow = new UOW();
            uow.Service = "findGQueue";
            
            GQueue cls = GQueue.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveGQueue()
        {
            UOW uow = new UOW();
            uow.Service = "saveGQueue";
            GQueue cls = GQueue.find(uow, this.id);
            
            cls.GQueType = this.gQueType;
            cls.SvcProvider = this.svcProvider;
            cls.Method = this.method;
            cls.XmlMessage = this.xmlMessage;
            cls.Agent = this.agent;
            cls.Clerkid = this.clerkid;
            cls.Predecessor = this.predecessor;
            cls.Initiator = this.initiator;
            cls.StartDateTime = this.startDateTime;
            cls.LastDateTime = this.lastDateTime;
            cls.TimeInterval = this.timeInterval;
            cls.Expires = this.expires;
            cls.AccessCnt = this.accessCnt;
            cls.MaxCnt = this.maxCnt;
            cls.Status = this.status;
            cls.GQueType += " saved";
            this.gQueType = cls.GQueType;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveGQueue - assert";
            
            cls = GQueue.find(uow, this.id);
            Assertion.Assert(cls.GQueType == this.gQueType);
            uow.close();
        }
        [Test]
        public void findAllGQueues()
        {
            UOW uow = new UOW();
            uow.Service = "findAllGQueues";
            GQueue[] objs = GQueue.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delGQueue()
        {
            UOW uow = new UOW();
            uow.Service = "delGQueue";
            GQueue cls = GQueue.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delGQueue - assert";
            cls = GQueue.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
