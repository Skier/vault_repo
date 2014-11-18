using System;
using NUnit.Framework;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.ComponentsTests
{
    [TestFixture]
    public class Service_TransactionTests
    {
        /*		Data		*/
        int service_Transaction_ID;
        int accNumber = 2;
        string phNumber = "phNumber";
        DateTime trans_Date = DateTime.Now;
        int charge_Type_ID = 5;
        decimal charge_Amount = 5.5M;
        int tP_ID = 7;
        DateTime tP_SendDate = DateTime.Now;
        int tP_Status = 9;
        
        /*		Constructors		*/
        public Service_TransactionTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            Service_TransactionTests test = new Service_TransactionTests();
            
            // UOW Tests
            test.addService_Transaction();
            test.findService_Transaction();
            test.saveService_Transaction();
            test.findAllService_Transactions();
            
            try
            {
                test.delService_Transaction();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delService_Transaction:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delService_Transaction: " + e.Message);
            }
            
        }
        [Test]
        public void addService_Transaction()
        {
            UOW uow = new UOW();
            uow.Service = "addService_Transaction";
            Service_Transaction cls = new Service_Transaction(uow);
            
            cls.AccNumber = this.accNumber;
            cls.PhNumber = this.phNumber;
            cls.Trans_Date = this.trans_Date;
            cls.Charge_Type_ID = this.charge_Type_ID;
            cls.Charge_Amount = this.charge_Amount;
            cls.TP_ID = this.tP_ID;
            cls.TP_SendDate = this.tP_SendDate;
            cls.TP_Status = this.tP_Status;
        
            uow.commit();
            this.service_Transaction_ID = cls.Service_Transaction_ID;
            
            uow = new UOW();
            uow.Service = "addService_Transaction - assert";
            cls = Service_Transaction.find(uow, this.service_Transaction_ID);
            Assertion.Assert(cls.PhNumber == this.phNumber);
            uow.close();
        }
        [Test]
        public void findService_Transaction()
        {
            UOW uow = new UOW();
            uow.Service = "findService_Transaction";
            
            Service_Transaction cls = Service_Transaction.find(uow, this.service_Transaction_ID);
            Assertion.Assert(cls.Service_Transaction_ID == this.service_Transaction_ID);
            uow.close();
        }
        [Test]
        public void saveService_Transaction()
        {
            UOW uow = new UOW();
            uow.Service = "saveService_Transaction";
            Service_Transaction cls = Service_Transaction.find(uow, this.service_Transaction_ID);
            
            cls.AccNumber = this.accNumber;
            cls.PhNumber = this.phNumber;
            cls.Trans_Date = this.trans_Date;
            cls.Charge_Type_ID = this.charge_Type_ID;
            cls.Charge_Amount = this.charge_Amount;
            cls.TP_ID = this.tP_ID;
            cls.TP_SendDate = this.tP_SendDate;
            cls.TP_Status = this.tP_Status;
            cls.AccNumber += 2;
            this.accNumber = cls.AccNumber;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveService_Transaction - assert";
            
            cls = Service_Transaction.find(uow, this.service_Transaction_ID);
            Assertion.Assert(cls.AccNumber == this.accNumber);
            uow.close();
        }
        [Test]
        public void findAllService_Transactions()
        {
            UOW uow = new UOW();
            uow.Service = "findAllService_Transactions";
            Service_Transaction[] objs = Service_Transaction.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delService_Transaction()
        {
            UOW uow = new UOW();
            uow.Service = "delService_Transaction";
            Service_Transaction cls = Service_Transaction.find(uow, this.service_Transaction_ID);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delService_Transaction - assert";
            cls = Service_Transaction.find(uow, this.service_Transaction_ID);
            Assertion.Assert((cls.Service_Transaction_ID == 0));
            uow.close();
        }
    }
}
