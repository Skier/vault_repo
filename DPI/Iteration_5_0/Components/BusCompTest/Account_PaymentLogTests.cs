using System;
using NUnit.Framework;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.ComponentsTests
{
    [TestFixture]
    public class Account_PaymentLogTests
    {
        /*		Data		*/
        int account_PaymentLog_ID;
        int accNumber = 50343996;
        DateTime date = DateTime.Now;
        string description = "description";
        decimal amount = 4.5M;
        decimal balance = 5.5M;
        
        /*		Constructors		*/
        public Account_PaymentLogTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            Account_PaymentLogTests test = new Account_PaymentLogTests();
            
            // UOW Tests
            test.addAccount_PaymentLog();
            test.findAccount_PaymentLog();
            test.saveAccount_PaymentLog();
            test.findAllAccount_PaymentLogs();
            
            try
            {
                test.delAccount_PaymentLog();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delAccount_PaymentLog:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delAccount_PaymentLog: " + e.Message);
            }
            
        }
        [Test]
        public void addAccount_PaymentLog()
        {
            UOW uow = new UOW();
            uow.Service = "addAccount_PaymentLog";
            Account_PaymentLog cls = new Account_PaymentLog(uow);
            
            cls.AccNumber = this.accNumber;
            cls.Date = this.date;
            cls.Description = this.description;
            cls.Amount = this.amount;
            cls.Balance = this.balance;
        
            uow.commit();
            this.account_PaymentLog_ID = cls.Account_PaymentLog_ID;
            
            uow = new UOW();
            uow.Service = "addAccount_PaymentLog - assert";
            cls = Account_PaymentLog.find(uow, this.account_PaymentLog_ID);
            Assertion.Assert(cls.Date == this.date);
            uow.close();
        }
        [Test]
        public void findAccount_PaymentLog()
        {
            UOW uow = new UOW();
            uow.Service = "findAccount_PaymentLog";
            
            Account_PaymentLog cls = Account_PaymentLog.find(uow, this.account_PaymentLog_ID);
            Assertion.Assert(cls.Account_PaymentLog_ID == this.account_PaymentLog_ID);
            uow.close();
        }
        [Test]
        public void saveAccount_PaymentLog()
        {
            UOW uow = new UOW();
            uow.Service = "saveAccount_PaymentLog";
            Account_PaymentLog cls = Account_PaymentLog.find(uow, this.account_PaymentLog_ID);
            
            cls.AccNumber = this.accNumber;
            cls.Date = this.date;
            cls.Description = this.description;
            cls.Amount = this.amount;
            cls.Balance = this.balance;
            cls.AccNumber += 2;
            this.accNumber = cls.AccNumber;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveAccount_PaymentLog - assert";
            
            cls = Account_PaymentLog.find(uow, this.account_PaymentLog_ID);
            Assertion.Assert(cls.AccNumber == this.accNumber);
            uow.close();
        }
        [Test]
        public void findAllAccount_PaymentLogs()
        {
            UOW uow = new UOW();
            uow.Service = "findAllAccount_PaymentLogs";
            Account_PaymentLog[] objs = Account_PaymentLog.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delAccount_PaymentLog()
        {
            UOW uow = new UOW();
            uow.Service = "delAccount_PaymentLog";
            Account_PaymentLog cls = Account_PaymentLog.find(uow, this.account_PaymentLog_ID);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delAccount_PaymentLog - assert";
            cls = Account_PaymentLog.find(uow, this.account_PaymentLog_ID);
            Assertion.Assert((cls.Account_PaymentLog_ID == 0));
            uow.close();
        }
    }
}
