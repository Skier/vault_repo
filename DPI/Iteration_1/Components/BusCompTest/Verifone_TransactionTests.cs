using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;


namespace DPI.ComponentsTests
{
    [TestFixture]
    public class Verifone_TransactionTests
    {
        /*		Data		*/
        int verifone_Transaction_ID;
        string trConfirm = "trConfirm";
        string trLDConfirm = "trLDConfirm";
        string trNumber = "trNumber";
        int accNumber = 5;
        string phNumber = "phNumber";
        DateTime payDate = DateTime.Now;
        string payTime = "payTime";
        decimal localAmount = 8.5M;
        decimal lDAmount = 9.5M;
        decimal comAmount = 10.5M;
        string clerkID = "clerkID";
        int transaction_Type_ID = 13;
        int transaction_Method_ID = 14;
        string storeCode = "storeCode";
        string aNI = "aNI";
        
        /*		Constructors		*/
        public Verifone_TransactionTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            Verifone_TransactionTests test = new Verifone_TransactionTests();
            
            // UOW Tests
            test.addVerifone_Transaction();
            test.findVerifone_Transaction();
            test.saveVerifone_Transaction();
            test.findAllVerifone_Transactions();
            
            try
            {
                test.delVerifone_Transaction();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delVerifone_Transaction:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delVerifone_Transaction: " + e.Message);
            }
            
        }
        [Test]
        public void addVerifone_Transaction()
        {
            UOW uow = new UOW();
            uow.Service = "addVerifone_Transaction";
            Verifone_Transaction cls = new Verifone_Transaction(uow);
            
            cls.TrConfirm = this.trConfirm;
            cls.TrLDConfirm = this.trLDConfirm;
            cls.TrNumber = this.trNumber;
            cls.AccNumber = this.accNumber;
            cls.PhNumber = this.phNumber;
            cls.PayDate = this.payDate;
            cls.PayTime = this.payTime;
            cls.LocalAmount = this.localAmount;
            cls.LDAmount = this.lDAmount;
            cls.ComAmount = this.comAmount;
            cls.ClerkID = this.clerkID;
            cls.Transaction_Type_ID = this.transaction_Type_ID;
            cls.Transaction_Method_ID = this.transaction_Method_ID;
            cls.StoreCode = this.storeCode;
            cls.ANI = this.aNI;
        
            uow.commit();
            this.verifone_Transaction_ID = cls.Verifone_Transaction_ID;
            
            uow = new UOW();
            uow.Service = "addVerifone_Transaction - assert";
            cls = Verifone_Transaction.find(uow, this.verifone_Transaction_ID);
            Assertion.Assert(cls.TrLDConfirm == this.trLDConfirm);
            uow.close();
        }
        [Test]
        public void findVerifone_Transaction()
        {
            UOW uow = new UOW();
            uow.Service = "findVerifone_Transaction";
            
            Verifone_Transaction cls = Verifone_Transaction.find(uow, this.verifone_Transaction_ID);
            Assertion.Assert(cls.Verifone_Transaction_ID == this.verifone_Transaction_ID);
            uow.close();
        }
        [Test]
        public void saveVerifone_Transaction()
        {
            UOW uow = new UOW();
            uow.Service = "saveVerifone_Transaction";
            Verifone_Transaction cls = Verifone_Transaction.find(uow, this.verifone_Transaction_ID);
            
            cls.TrConfirm = this.trConfirm;
            cls.TrLDConfirm = this.trLDConfirm;
            cls.TrNumber = this.trNumber;
            cls.AccNumber = this.accNumber;
            cls.PhNumber = this.phNumber;
            cls.PayDate = this.payDate;
            cls.PayTime = this.payTime;
            cls.LocalAmount = this.localAmount;
            cls.LDAmount = this.lDAmount;
            cls.ComAmount = this.comAmount;
            cls.ClerkID = this.clerkID;
            cls.Transaction_Type_ID = this.transaction_Type_ID;
            cls.Transaction_Method_ID = this.transaction_Method_ID;
            cls.StoreCode = this.storeCode;
            cls.ANI = this.aNI;
            cls.TrConfirm += " saved";
            this.trConfirm = cls.TrConfirm;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveVerifone_Transaction - assert";
            
            cls = Verifone_Transaction.find(uow, this.verifone_Transaction_ID);
            Assertion.Assert(cls.TrConfirm == this.trConfirm);
            uow.close();
        }
        [Test]
        public void findAllVerifone_Transactions()
        {
            UOW uow = new UOW();
            uow.Service = "findAllVerifone_Transactions";
            Verifone_Transaction[] objs = Verifone_Transaction.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delVerifone_Transaction()
        {
            UOW uow = new UOW();
            uow.Service = "delVerifone_Transaction";
            Verifone_Transaction cls = Verifone_Transaction.find(uow, this.verifone_Transaction_ID);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delVerifone_Transaction - assert";
            cls = Verifone_Transaction.find(uow, this.verifone_Transaction_ID);
            Assertion.Assert((cls.Verifone_Transaction_ID == 0));
            uow.close();
        }
    }
}
