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
    public class AgentIncenTypeTests
    {
        /*		Data		*/
        string incentiveType = "incentiveType";
        bool isRegReq = true;
        bool isEarlyRegAllowed = true;
        bool isOnePerStore = true;
        bool isOnePerPeriod = true;
        
        /*		Constructors		*/
        public AgentIncenTypeTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            AgentIncenTypeTests test = new AgentIncenTypeTests();
            
            // UOW Tests
            test.addAgentIncenType();
            test.findAgentIncenType();
            test.saveAgentIncenType();
            test.findAllAgentIncenTypes();
            
            try
            {
                test.delAgentIncenType();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delAgentIncenType:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delAgentIncenType: " + e.Message);
            }
            
        }
        [Test]
        public void addAgentIncenType()
        {
            UOW uow = new UOW();
            uow.Service = "addAgentIncenType";
            AgentIncenType cls = new AgentIncenType(uow);
            
            cls.IncentiveType = this.incentiveType;
            cls.IsRegReq = this.isRegReq;
            cls.IsEarlyRegAllowed = this.isEarlyRegAllowed;
            cls.IsOnePerStore = this.isOnePerStore;
            cls.IsOnePerPeriod = this.isOnePerPeriod;
        
            uow.commit();
            
            uow = new UOW();
            uow.Service = "addAgentIncenType - assert";
            cls = AgentIncenType.find(uow, this.incentiveType);
            Assertion.Assert(cls.IsEarlyRegAllowed == this.isEarlyRegAllowed);
            uow.close();
        }
        [Test]
        public void findAgentIncenType()
        {
            UOW uow = new UOW();
            uow.Service = "findAgentIncenType";
            
            AgentIncenType cls = AgentIncenType.find(uow, this.incentiveType);
            Assertion.Assert(cls.IncentiveType.Trim() == this.incentiveType.Trim());
            uow.close();
        }
        [Test]
        public void saveAgentIncenType()
        {
            UOW uow = new UOW();
            uow.Service = "saveAgentIncenType";
            AgentIncenType cls = AgentIncenType.find(uow, this.incentiveType);
            
            cls.IncentiveType = this.incentiveType;
            cls.IsRegReq = this.isRegReq;
            cls.IsEarlyRegAllowed = this.isEarlyRegAllowed;
            cls.IsOnePerStore = this.isOnePerStore;
            cls.IsOnePerPeriod = this.isOnePerPeriod;
            cls.IsRegReq = false;
            this.isRegReq = cls.IsRegReq;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveAgentIncenType - assert";
            
            cls = AgentIncenType.find(uow, this.incentiveType);
            Assertion.Assert(cls.IsRegReq == this.isRegReq);
            uow.close();
        }
        [Test]
        public void findAllAgentIncenTypes()
        {
            UOW uow = new UOW();
            uow.Service = "findAllAgentIncenTypes";
            AgentIncenType[] objs = AgentIncenType.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delAgentIncenType()
        {
            UOW uow = new UOW();
            uow.Service = "delAgentIncenType";
            AgentIncenType cls = AgentIncenType.find(uow, this.incentiveType);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delAgentIncenType - assert";
            cls = AgentIncenType.find(uow, this.incentiveType);
            Assertion.Assert((cls.IncentiveType ==  null));
            uow.close();
        }
    }
}
