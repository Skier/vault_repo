using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;


namespace DPI.ComponentsTests
{
    [TestFixture]
    public class AgentRegistrationTests
    {
        /*		Data		*/
        int id;
        string regType = "regType";
        int exclusiveIncentive = 3;
        DateTime regDate = DateTime.Now;
        DateTime effStartDate = DateTime.Now;
        DateTime effEnddate = DateTime.Now;
        bool isAgreed = true;
        string firstName = "firstName";
        string lastName = "lastName";
        int title = 10;
        string phone = "phone";
        string email = "email";
        int mailAddr = 13;
        int confNum = 14;
        int userAcct = 15;
        string storeCode = "storeCode";
        int corpId = 17;
        string status = "status";
        
        /*		Constructors		*/
        public AgentRegistrationTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            AgentRegistrationTests test = new AgentRegistrationTests();
            
            // UOW Tests
            test.addAgentRegistration();
            test.findAgentRegistration();
            test.saveAgentRegistration();
            test.findAllAgentRegistrations();
            
            try
            {
                test.delAgentRegistration();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delAgentRegistration:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delAgentRegistration: " + e.Message);
            }
            
        }
        [Test]
        public void addAgentRegistration()
        {
            UOW uow = new UOW();
            uow.Service = "addAgentRegistration";
            AgentRegistration cls = new AgentRegistration(uow);
            
            cls.RegType = this.regType;
            cls.ExclusiveIncentive = this.exclusiveIncentive;
            cls.RegDate = this.regDate;
            cls.EffStartDate = this.effStartDate;
            cls.EffEnddate = this.effEnddate;
            cls.IsAgreed = this.isAgreed;
            cls.FirstName = this.firstName;
            cls.LastName = this.lastName;
            cls.Title = this.title;
            cls.Phone = this.phone;
            cls.Email = this.email;
            cls.AddrId = this.mailAddr;
            cls.ConfNum = this.confNum;
            cls.UserAcct = this.userAcct;
            cls.StoreCode = this.storeCode;
            cls.CorpId = this.corpId;
            cls.Status = this.status;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addAgentRegistration - assert";
            cls = AgentRegistration.find(uow, this.id);
            Assertion.Assert(cls.ExclusiveIncentive == this.exclusiveIncentive);
            uow.close();
        }
        [Test]
        public void findAgentRegistration()
        {
            UOW uow = new UOW();
            uow.Service = "findAgentRegistration";
            
            AgentRegistration cls = AgentRegistration.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveAgentRegistration()
        {
            UOW uow = new UOW();
            uow.Service = "saveAgentRegistration";
            AgentRegistration cls = AgentRegistration.find(uow, this.id);
            
            cls.RegType = this.regType;
            cls.ExclusiveIncentive = this.exclusiveIncentive;
            cls.RegDate = this.regDate;
            cls.EffStartDate = this.effStartDate;
            cls.EffEnddate = this.effEnddate;
            cls.IsAgreed = this.isAgreed;
            cls.FirstName = this.firstName;
            cls.LastName = this.lastName;
            cls.Title = this.title;
            cls.Phone = this.phone;
            cls.Email = this.email;
            cls.AddrId = this.mailAddr;
            cls.ConfNum = this.confNum;
            cls.UserAcct = this.userAcct;
            cls.StoreCode = this.storeCode;
            cls.CorpId = this.corpId;
            cls.Status = this.status;
            cls.RegType += " saved";
            this.regType = cls.RegType;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveAgentRegistration - assert";
            
            cls = AgentRegistration.find(uow, this.id);
            Assertion.Assert(cls.RegType == this.regType);
            uow.close();
        }
        [Test]
        public void findAllAgentRegistrations()
        {
            UOW uow = new UOW();
            uow.Service = "findAllAgentRegistrations";
            AgentRegistration[] objs = AgentRegistration.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delAgentRegistration()
        {
            UOW uow = new UOW();
            uow.Service = "delAgentRegistration";
            AgentRegistration cls = AgentRegistration.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delAgentRegistration - assert";
            cls = AgentRegistration.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
