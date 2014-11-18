//using System;
//using System.Data;
//using System.Data.SqlClient;
//using NUnit.Framework;
//using DPI.Components;
//using DPI.Interfaces;
//using DPI.Components;
//
//namespace DPI.ComponentsTests
//{
//    [TestFixture]
//    public class ReversalQueueTests
//    {
//        /*		Data		*/
//        int id;
//        string reversalType = "reversalType";
//        int reversalProvider = 3;
//        DateTime initDateTime = DateTime.Now;
//        DateTime compDateTime = DateTime.Now;
//        int attemps = 6;
//        int demand = 7;
//        string reasonCode = "reasonCode";
//        string reasonText = "reasonText";
//        string status = "status";
//        string xml = "xml";
//        
//        /*		Constructors		*/
//        public ReversalQueueTests()
//        {
//            // try { cleanup(); } 	catch {}
//            // Console.WriteLine("Cleanup completed");
//        }
//        
//        /*		Methods		*/
//        public static void Main()
//        {
//            ReversalQueueTests test = new ReversalQueueTests();
//            
//            // UOW Tests
//            test.addReversalQueue();
//            test.findReversalQueue();
//            test.saveReversalQueue();
//            test.findAllReversalQueues();
//            
//            try
//            {
//                test.delReversalQueue();
//            }
//            catch(ArgumentException ae)
//            {
//                Console.WriteLine("Expected exception: delReversalQueue:" + ae.Message);
//            }
//            catch(Exception e)
//            {
//                Console.WriteLine("Error: delReversalQueue: " + e.Message);
//            }
//            
//        }
//        [Test]
//        public void addReversalQueue()
//        {
//            UOW uow = new UOW();
//            uow.Service = "addReversalQueue";
//            ReversalQueue cls = new ReversalQueue(uow);
//            
//            cls.ReversalType = this.reversalType;
//            cls.ReversalProvider = this.reversalProvider;
//            cls.InitDateTime = this.initDateTime;
//            cls.CompDateTime = this.compDateTime;
//            cls.Attemps = this.attemps;
//            cls.Demand = this.demand;
//            cls.ReasonCode = this.reasonCode;
//            cls.ReasonText = this.reasonText;
//            cls.Status = this.status;
//            cls.Xml = this.xml;
//        
//            uow.commit();
//            this.id = cls.Id;
//            
//            uow = new UOW();
//            uow.Service = "addReversalQueue - assert";
//            cls = ReversalQueue.find(uow, this.id);
//            Assertion.Assert(cls.ReversalProvider == this.reversalProvider);
//            uow.close();
//        }
//        [Test]
//        public void findReversalQueue()
//        {
//            UOW uow = new UOW();
//            uow.Service = "findReversalQueue";
//            
//            ReversalQueue cls = ReversalQueue.find(uow, this.id);
//            Assertion.Assert(cls.Id == this.id);
//            uow.close();
//        }
//        [Test]
//        public void saveReversalQueue()
//        {
//            UOW uow = new UOW();
//            uow.Service = "saveReversalQueue";
//            ReversalQueue cls = ReversalQueue.find(uow, this.id);
//            
//            cls.ReversalType = this.reversalType;
//            cls.ReversalProvider = this.reversalProvider;
//            cls.InitDateTime = this.initDateTime;
//            cls.CompDateTime = this.compDateTime;
//            cls.Attemps = this.attemps;
//            cls.Demand = this.demand;
//            cls.ReasonCode = this.reasonCode;
//            cls.ReasonText = this.reasonText;
//            cls.Status = this.status;
//            cls.Xml = this.xml;
//            cls.ReversalType += " saved";
//            this.reversalType = cls.ReversalType;
//                
//            uow.commit();
//            
//            uow = new UOW();
//            uow.Service = "saveReversalQueue - assert";
//            
//            cls = ReversalQueue.find(uow, this.id);
//            Assertion.Assert(cls.ReversalType == this.reversalType);
//            uow.close();
//        }
//        [Test]
//        public void findAllReversalQueues()
//        {
//            UOW uow = new UOW();
//            uow.Service = "findAllReversalQueues";
//            ReversalQueue[] objs = ReversalQueue.getAll(uow);
//            Assertion.Assert(objs.Length > 0);
//            uow.close();
//        }
//        [Test]
//        [ExpectedException(typeof(ArgumentException))]
//        public void delReversalQueue()
//        {
//            UOW uow = new UOW();
//            uow.Service = "delReversalQueue";
//            ReversalQueue cls = ReversalQueue.find(uow, this.id);
//            cls.delete();
//            
//            uow.commit();
//            
//            uow = new UOW();
//            uow.Service = "delReversalQueue - assert";
//            cls = ReversalQueue.find(uow, this.id);
//            Assertion.Assert((cls.Id == 0));
//            uow.close();
//        }
//    }
//}
