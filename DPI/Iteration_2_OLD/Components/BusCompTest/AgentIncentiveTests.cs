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
    public class AgentIncentiveTests
    {
        /*		Data		*/
        int id;
        string incentType = "Threshold";
        string incentName = "incentName";
        string incentDescr = "incentDescr";
        DateTime effStartDate = DateTime.Now;
        DateTime effEndDate = DateTime.Now;
        string incentCond = "incentCond";
        string eligibility = "eligibility";
        string status = "status";
        
        /*		Constructors		*/
        public AgentIncentiveTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            AgentIncentiveTests test = new AgentIncentiveTests();
            
            // UOW Tests
			try
			{
				test.addAgentIncentive();
				test.findAgentIncentive();
				test.saveAgentIncentive();
				test.findAllAgentIncentives();
			}

			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
            try
            {
                test.delAgentIncentive();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delAgentIncentive:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delAgentIncentive: " + e.Message);
            }
            
        }
        [Test]
        public void addAgentIncentive()
        {
            UOW uow = new UOW();
            uow.Service = "addAgentIncentive";
            AgentIncentive cls = new AgentIncentive(uow);
            
            cls.IncentType = this.incentType;
            cls.IncentName = this.incentName;
            cls.IncentDescr = this.incentDescr;
            cls.EffStartDate = this.effStartDate;
            cls.EffEndDate = this.effEndDate;
            cls.IncentCond = this.incentCond;
            cls.Eligibility = this.eligibility;
            cls.Status = this.status;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addAgentIncentive - assert";
            cls = AgentIncentive.find(uow, this.id);
            Assertion.Assert(cls.IncentName == this.incentName);
            uow.close();
        }
        [Test]
        public void findAgentIncentive()
        {
            UOW uow = new UOW();
            uow.Service = "findAgentIncentive";
            
            AgentIncentive cls = AgentIncentive.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveAgentIncentive()
        {
            UOW uow = new UOW();
            uow.Service = "saveAgentIncentive";
            AgentIncentive cls = AgentIncentive.find(uow, this.id);
            
            cls.IncentType = this.incentType;
            cls.IncentName = this.incentName;
            cls.IncentDescr = this.incentDescr;
            cls.EffStartDate = this.effStartDate;
            cls.EffEndDate = this.effEndDate;
            cls.IncentCond = this.incentCond;
			this.eligibility += " saved";
            cls.Eligibility = this.eligibility;
            cls.Status = this.status;
            this.incentType = cls.IncentType;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveAgentIncentive - assert";
            
            cls = AgentIncentive.find(uow, this.id);
            Assertion.Assert(cls.Eligibility == this.eligibility);
            uow.close();
        }
        [Test]
        public void findAllAgentIncentives()
        {
            UOW uow = new UOW();
            uow.Service = "findAllAgentIncentives";
            AgentIncentive[] objs = AgentIncentive.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delAgentIncentive()
        {
            UOW uow = new UOW();
            uow.Service = "delAgentIncentive";
            AgentIncentive cls = AgentIncentive.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delAgentIncentive - assert";
            cls = AgentIncentive.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
