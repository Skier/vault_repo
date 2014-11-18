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
    public class Energy_TransactionsTests
    {
        /*		Data		*/
        int iD;
        int confirmNum = 2;
        DateTime payDateTime = DateTime.Now;
        decimal tran_Amount = 3.5M;
        string storeCode = "storeCode";
        string clerkid = "clerkid";
        string pin = "pin";
        decimal commission = 7.5M;
        string status = "status";
        int acctID = 10;
        decimal activationFee = 10.5M;
        decimal taxAmt = 11.5M;
        
        /*		Constructors		*/
        public Energy_TransactionsTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            Energy_TransactionsTests test = new Energy_TransactionsTests();
            
            // UOW Tests
            test.addEnergy_Transactions();
            test.findEnergy_Transactions();
            test.saveEnergy_Transactions();
            test.findAllEnergy_Transactionses();
            
            try
            {
                test.delEnergy_Transactions();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delEnergy_Transactions:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delEnergy_Transactions: " + e.Message);
            }
            
        }
        [Test]
        public void addEnergy_Transactions()
        {
            UOW uow = new UOW();
            uow.Service = "addEnergy_Transactions";
            Energy_Transactions cls = new Energy_Transactions(uow);
            
            cls.ConfirmNum = this.confirmNum;
            cls.PayDateTime = this.payDateTime;
            cls.Tran_Amount = this.tran_Amount;
            cls.StoreCode = this.storeCode;
            cls.Clerkid = this.clerkid;
            cls.Pin = this.pin;
            cls.Commission = this.commission;
            cls.Status = this.status;
            cls.AcctID = this.acctID;
            cls.ActivationFee = this.activationFee;
            cls.TaxAmt = this.taxAmt;
        
            uow.commit();
            this.iD = cls.ID;
            
            uow = new UOW();
            uow.Service = "addEnergy_Transactions - assert";
            cls = Energy_Transactions.find(uow, this.iD);
            Assertion.Assert(cls.PayDateTime == this.payDateTime);
            uow.close();
        }
        [Test]
        public void findEnergy_Transactions()
        {
            UOW uow = new UOW();
            uow.Service = "findEnergy_Transactions";
            
            Energy_Transactions cls = Energy_Transactions.find(uow, this.iD);
            Assertion.Assert(cls.ID == this.iD);
            uow.close();
        }
        [Test]
        public void saveEnergy_Transactions()
        {
            UOW uow = new UOW();
            uow.Service = "saveEnergy_Transactions";
            Energy_Transactions cls = Energy_Transactions.find(uow, this.iD);
            
            cls.ConfirmNum = this.confirmNum;
            cls.PayDateTime = this.payDateTime;
            cls.Tran_Amount = this.tran_Amount;
            cls.StoreCode = this.storeCode;
            cls.Clerkid = this.clerkid;
            cls.Pin = this.pin;
            cls.Commission = this.commission;
            cls.Status = this.status;
            cls.AcctID = this.acctID;
            cls.ActivationFee = this.activationFee;
            cls.TaxAmt = this.taxAmt;
            cls.ConfirmNum += 2;
            this.confirmNum = cls.ConfirmNum;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveEnergy_Transactions - assert";
            
            cls = Energy_Transactions.find(uow, this.iD);
            Assertion.Assert(cls.ConfirmNum == this.confirmNum);
            uow.close();
        }
        [Test]
        public void findAllEnergy_Transactionses()
        {
            UOW uow = new UOW();
            uow.Service = "findAllEnergy_Transactionses";
            Energy_Transactions[] objs = Energy_Transactions.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delEnergy_Transactions()
        {
            UOW uow = new UOW();
            uow.Service = "delEnergy_Transactions";
            Energy_Transactions cls = Energy_Transactions.find(uow, this.iD);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delEnergy_Transactions - assert";
            cls = Energy_Transactions.find(uow, this.iD);
            Assertion.Assert((cls.ID == 0));
            uow.close();
        }
    }
}
