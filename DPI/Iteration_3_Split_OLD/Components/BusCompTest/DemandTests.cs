using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.ComponentsTests
{
    [TestFixture]
    public class DemandTests
    {
        /*		Data		*/
        int id;
        string dmdType = "New";
        int statement = 1;
        int consumer = 4;
        string consumerAgent = "AgentOne";
        string status = "WIP";
        bool isUnderWF = true;
        string workflow = "workflow";
        string wFStep = "wFStep";
		DateTime dmdDate = DateTime.Today;
		DateTime statusChangeDate =  DateTime.Today;
		int billPayer=11;
		string storeCode = "myStore";
        
        /*		Constructors		*/
        public DemandTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            DemandTests test = new DemandTests();
            test.getForStoreCode();
			test.GetDmd();
            // UOW Tests
            test.addDemand();
            test.findDemand();
            test.saveDemand();
            test.findAllDemands();
            
            try
            {
                test.delDemand();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delDemand:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delDemand: " + e.Message);
            }
            Console.ReadLine();
        }
		public void GetDmd()
		{
			UOW uow = new UOW();
			Demand dmd = Demand.find(uow, 715);
			IDmdItem[] dis = dmd.GetDmdItems(uow);
		}
        [Test]
        public void addDemand()
        {
            UOW uow = new UOW();
            uow.Service = "addDemand";
            Demand cls = new Demand(uow,  DemandType.New.ToString());
            
            cls.DmdType = this.dmdType;
            cls.Statement = this.statement;
      //      cls.Consumer = this.consumer;
            cls.ConsumerAgent = this.consumerAgent;
            cls.Status = this.status;
            cls.IsUnderWF = this.isUnderWF;
            cls.Workflow = this.workflow;
            cls.WFStep = this.wFStep;
			cls.DmdDate = this.dmdDate;
			cls.StatusChangeDate = this.statusChangeDate;
			cls.BillPayer = this.billPayer;
			cls.StoreCode = this.storeCode;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addDemand - assert";
            cls = Demand.find(uow, this.id);

           	Assertion.Assert(cls.StoreCode == this.storeCode);	
			Assertion.Assert(cls.ConsumerAgent == this.consumerAgent);	
			uow.close();
        }
        [Test]
        public void findDemand()
        {
            UOW uow = new UOW();
            uow.Service = "findDemand";
            
            Demand cls = Demand.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);

			//DmdItem[] items = cls.
            uow.close();
        }
		[Test]
		public void getForStoreCode()
		{
			UOW uow = new UOW();
			uow.Service = "getForStoreCode";
            
			IDemand[] dmds = Demand.GetForStoreCode(uow, this.storeCode, DateTime.Today.AddMonths(-1), DateTime.Today);
			Assertion.Assert(dmds.Length > 0);

			//DmdItem[] items = cls.
			uow.close();
		}
        [Test]
        public void saveDemand()
        {
            UOW uow = new UOW();
            uow.Service = "saveDemand";
            Demand cls = Demand.find(uow, this.id);
            
            cls.DmdType = this.dmdType;
            cls.Statement = this.statement;
			this.consumer += 2;
      //      cls.Consumer = this.consumer;
            cls.ConsumerAgent = this.consumerAgent;
            cls.Status = this.status;
            cls.IsUnderWF = this.isUnderWF;
            cls.Workflow = this.workflow;
            cls.WFStep = this.wFStep;
            this.dmdType = cls.DmdType;
			cls.BillPayer = this.billPayer;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveDemand - assert";
            
            cls = Demand.find(uow, this.id);
            Assertion.Assert(cls.Workflow == this.workflow);
            uow.close();
        }
        [Test]
        public void findAllDemands()
        {
            UOW uow = new UOW();
            uow.Service = "findAllDemands";
            Demand[] objs = Demand.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delDemand()
        {
            UOW uow = new UOW();
            uow.Service = "delDemand";
            Demand cls = Demand.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delDemand - assert";
            cls = Demand.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
