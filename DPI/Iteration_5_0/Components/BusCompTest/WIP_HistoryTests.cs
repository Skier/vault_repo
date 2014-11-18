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
    public class WIP_HistoryTests
    {
        /*		Data		*/
        int id;
        string workflow = "workflow";
        int wipId = 3;
        string step = "step";
        DateTime stepStart = DateTime.Now;
        DateTime stepFinish = DateTime.Now;
        string busObjId = "busObjId";
        string busObjType = "busObjType";
        string nextStep = "nextStep";
        bool isCompleted = true;
        
        /*		Constructors		*/
        public WIP_HistoryTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            WIP_HistoryTests test = new WIP_HistoryTests();
            
            // UOW Tests
            test.addWIP_History();
            test.findWIP_History();
            test.saveWIP_History();
            test.findAllWIP_Historys();
            
            try
            {
                test.delWIP_History();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delWIP_History:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delWIP_History: " + e.Message);
            }
            
        }
        [Test]
        public void addWIP_History()
        {
            UOW uow = new UOW();
            uow.Service = "addWIP_History";
            WIP_History cls = new WIP_History(uow);
            
            cls.Workflow = this.workflow;
            cls.WipId = this.wipId;
            cls.Step = this.step;
            cls.StepStart = this.stepStart;
            cls.StepFinish = this.stepFinish;
            cls.BusObjId = this.busObjId;
            cls.BusObjType = this.busObjType;
            cls.NextStep = this.nextStep;
            cls.IsCompleted = this.isCompleted;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addWIP_History - assert";
            cls = WIP_History.find(uow, this.id);
            Assertion.Assert(cls.WipId == this.wipId);
            uow.close();
        }
        [Test]
        public void findWIP_History()
        {
            UOW uow = new UOW();
            uow.Service = "findWIP_History";
            
            WIP_History cls = WIP_History.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveWIP_History()
        {
            UOW uow = new UOW();
            uow.Service = "saveWIP_History";
            WIP_History cls = WIP_History.find(uow, this.id);
            
            cls.Workflow = this.workflow;
            cls.WipId = this.wipId;
            cls.Step = this.step;
            cls.StepStart = this.stepStart;
            cls.StepFinish = this.stepFinish;
            cls.BusObjId = this.busObjId;
            cls.BusObjType = this.busObjType;
            cls.NextStep = this.nextStep;
            cls.IsCompleted = this.isCompleted;
            cls.Workflow += " saved";
            this.workflow = cls.Workflow;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveWIP_History - assert";
            
            cls = WIP_History.find(uow, this.id);
            Assertion.Assert(cls.Workflow == this.workflow);
            uow.close();
        }
        [Test]
        public void findAllWIP_Historys()
        {
            UOW uow = new UOW();
            uow.Service = "findAllWIP_Historys";
            WIP_History[] objs = WIP_History.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delWIP_History()
        {
            UOW uow = new UOW();
            uow.Service = "delWIP_History";
            WIP_History cls = WIP_History.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delWIP_History - assert";
            cls = WIP_History.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
