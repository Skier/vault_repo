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
    public class CustomerWebLogTests
    {
        /*		Data		*/
        int id = 1;
        int acctNumber = 2;
        DateTime visitDate = DateTime.Now;
        
        /*		Constructors		*/
        public CustomerWebLogTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            CustomerWebLogTests test = new CustomerWebLogTests();
            
            // UOW Tests
            test.addCustomerWebLog();
            test.findCustomerWebLog();
            test.saveCustomerWebLog();
            test.findAllCustomerWebLogs();
            
            try
            {
                test.delCustomerWebLog();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delCustomerWebLog:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delCustomerWebLog: " + e.Message);
            }
            
        }
        [Test]
        public void addCustomerWebLog()
        {
            UOW uow = new UOW();
            uow.Service = "addCustomerWebLog";
            CustomerWebLog cls = new CustomerWebLog(uow);
            
            cls.Id = this.id;
            cls.AcctNumber = this.acctNumber;
            cls.VisitDate = this.visitDate;
        
            uow.commit();
            
            uow = new UOW();
            uow.Service = "addCustomerWebLog - assert";
            cls = CustomerWebLog.find(uow, this.id);
            Assertion.Assert(cls.VisitDate == this.visitDate);
            uow.close();
        }
        [Test]
        public void findCustomerWebLog()
        {
            UOW uow = new UOW();
            uow.Service = "findCustomerWebLog";
            
            CustomerWebLog cls = CustomerWebLog.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveCustomerWebLog()
        {
            UOW uow = new UOW();
            uow.Service = "saveCustomerWebLog";
            CustomerWebLog cls = CustomerWebLog.find(uow, this.id);
            
            cls.Id = this.id;
            cls.AcctNumber = this.acctNumber;
            cls.VisitDate = this.visitDate;
            cls.AcctNumber += 2;
            this.acctNumber = cls.AcctNumber;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveCustomerWebLog - assert";
            
            cls = CustomerWebLog.find(uow, this.id);
            Assertion.Assert(cls.AcctNumber == this.acctNumber);
            uow.close();
        }
        [Test]
        public void findAllCustomerWebLogs()
        {
            UOW uow = new UOW();
            uow.Service = "findAllCustomerWebLogs";
            CustomerWebLog[] objs = CustomerWebLog.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delCustomerWebLog()
        {
            UOW uow = new UOW();
            uow.Service = "delCustomerWebLog";
            CustomerWebLog cls = CustomerWebLog.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delCustomerWebLog - assert";
            cls = CustomerWebLog.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
