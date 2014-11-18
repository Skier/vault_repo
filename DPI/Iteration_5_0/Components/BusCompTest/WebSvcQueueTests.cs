using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.ComponentsTests
{
    [TestFixture]
    public class WebSvcQueueTests
    {
        /*		Data		*/
        int id;
        string queType = "queType";
        string wSProvider = "wSProvider";
        string webMethod = "webMethod";
        string reversalMethod = "reversalMethod";
        string storeCode = "storeCode";
        string clerkId = "clerkId";
        string busObject = "busObject";
        string busObjId = "busObjId";
        DateTime initDate = DateTime.Now;
        DateTime lastAccessDate = DateTime.Now;
        string initReasonCode = "initReasonCode";
        string lastReasonCode = "lastReasonCode";
        string initialMsg = "initialMsg";
        string lastMsg = "lastMsg";
        int attemps = 16;
        string xml = "xml";
        string reversalXml = "reversalXml";
        string status = "status";
        
        /*		Constructors		*/
        public WebSvcQueueTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            WebSvcQueueTests test = new WebSvcQueueTests();
            
            // UOW Tests
            test.addWebSvcQueue();
            test.findWebSvcQueue();
            test.saveWebSvcQueue();
            test.findAllWebSvcQueues();
            
            try
            {
                test.delWebSvcQueue();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delWebSvcQueue:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delWebSvcQueue: " + e.Message);
            }
            
        }
        [Test]
        public void addWebSvcQueue()
        {
            UOW uow = new UOW();
            uow.Service = "addWebSvcQueue";
            WebSvcQueue cls = new WebSvcQueue(uow);
            
            cls.QueType = this.queType;
            cls.WSProvider = this.wSProvider;
            cls.WebMethod = this.webMethod;
            cls.ReversalMethod = this.reversalMethod;
            cls.StoreCode = this.storeCode;
            cls.ClerkId = this.clerkId;
            cls.BusObject = this.busObject;
            cls.BusObjId = this.busObjId;
            cls.InitDate = this.initDate;
            cls.LastAccessDate = this.lastAccessDate;
            cls.InitReasonCode = this.initReasonCode;
            cls.LastReasonCode = this.lastReasonCode;
            cls.InitialMsg = this.initialMsg;
            cls.LastMsg = this.lastMsg;
            //cls.Attemps = this.attemps;
            cls.Xml = this.xml;
            cls.ReversalXml = this.reversalXml;
            cls.Status = this.status;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addWebSvcQueue - assert";
            cls = WebSvcQueue.find(uow, this.id);
            Assertion.Assert(cls.WSProvider == this.wSProvider);
            uow.close();
        }
        [Test]
        public void findWebSvcQueue()
        {
            UOW uow = new UOW();
            uow.Service = "findWebSvcQueue";
            
            WebSvcQueue cls = WebSvcQueue.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveWebSvcQueue()
        {
            UOW uow = new UOW();
            uow.Service = "saveWebSvcQueue";
            WebSvcQueue cls = WebSvcQueue.find(uow, this.id);
            
            cls.QueType = this.queType;
            cls.WSProvider = this.wSProvider;
            cls.WebMethod = this.webMethod;
            cls.ReversalMethod = this.reversalMethod;
            cls.StoreCode = this.storeCode;
            cls.ClerkId = this.clerkId;
            cls.BusObject = this.busObject;
            cls.BusObjId = this.busObjId;
            cls.InitDate = this.initDate;
            cls.LastAccessDate = this.lastAccessDate;
            cls.InitReasonCode = this.initReasonCode;
            cls.LastReasonCode = this.lastReasonCode;
            cls.InitialMsg = this.initialMsg;
            cls.LastMsg = this.lastMsg;
            //cls.Attemps = this.attemps;
            cls.Xml = this.xml;
            cls.ReversalXml = this.reversalXml;
            cls.Status = this.status;
            cls.QueType += " saved";
            this.queType = cls.QueType;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveWebSvcQueue - assert";
            
            cls = WebSvcQueue.find(uow, this.id);
            Assertion.Assert(cls.QueType == this.queType);
            uow.close();
        }
        [Test]
        public void findAllWebSvcQueues()
        {
            UOW uow = new UOW();
            uow.Service = "findAllWebSvcQueues";
            WebSvcQueue[] objs = WebSvcQueue.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delWebSvcQueue()
        {
            UOW uow = new UOW();
            uow.Service = "delWebSvcQueue";
            WebSvcQueue cls = WebSvcQueue.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delWebSvcQueue - assert";
            cls = WebSvcQueue.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
