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
    public class Wireless_TransactionsTests
    {
        /*		Data		*/
        int wireless_Transaction_ID;
        int trConfirm = 2;
        string trNumber = "trNumber";
        DateTime payDateTime = DateTime.Now;
        decimal tran_Amount = 4.5M;
        int transaction_Method_ID = 6;
        string storeCode = "storeCode";
        string clerkid = "clerkid";
        int wireless_product_ID = 9;
        string pin = "pin";
        DateTime redeemDate = DateTime.Now;
        
        /*		Constructors		*/
        public Wireless_TransactionsTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            Wireless_TransactionsTests test = new Wireless_TransactionsTests();
            
            // UOW Tests
            test.addWireless_Transactions();
            test.findWireless_Transactions();
            test.saveWireless_Transactions();
            test.findAllWireless_Transactionses();
            
            try
            {
                test.delWireless_Transactions();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delWireless_Transactions:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delWireless_Transactions: " + e.Message);
            }
            
        }
        [Test]
        public void addWireless_Transactions()
        {
            UOW uow = new UOW();
            uow.Service = "addWireless_Transactions";
            Wireless_Transactions cls = new Wireless_Transactions(uow);
            
            cls.TrConfirm = this.trConfirm;
            cls.TrNumber = this.trNumber;
            cls.PayDateTime = this.payDateTime;
            cls.Tran_Amount = this.tran_Amount;
            cls.Transaction_Method_ID = this.transaction_Method_ID;
            cls.StoreCode = this.storeCode;
            cls.Clerkid = this.clerkid;
            cls.Wireless_product_ID = this.wireless_product_ID;
            cls.Pin = this.pin;
//            cls.IsRedeemed = this.isRedeemed;
//            cls.RedeemDate = this.redeemDate;
        
            uow.commit();
            this.wireless_Transaction_ID = cls.Wireless_Transaction_ID;
            
            uow = new UOW();
            uow.Service = "addWireless_Transactions - assert";
            cls = Wireless_Transactions.find(uow, this.wireless_Transaction_ID);
            Assertion.Assert(cls.TrNumber == this.trNumber);
            uow.close();
        }
        [Test]
        public void findWireless_Transactions()
        {
            UOW uow = new UOW();
            uow.Service = "findWireless_Transactions";
            
            Wireless_Transactions cls = Wireless_Transactions.find(uow, this.wireless_Transaction_ID);
            Assertion.Assert(cls.Wireless_Transaction_ID == this.wireless_Transaction_ID);
            uow.close();
        }
        [Test]
        public void saveWireless_Transactions()
        {
            UOW uow = new UOW();
            uow.Service = "saveWireless_Transactions";
            Wireless_Transactions cls = Wireless_Transactions.find(uow, this.wireless_Transaction_ID);
            
            cls.TrConfirm = this.trConfirm;
            cls.TrNumber = this.trNumber;
            cls.PayDateTime = this.payDateTime;
            cls.Tran_Amount = this.tran_Amount;
            cls.Transaction_Method_ID = this.transaction_Method_ID;
            cls.StoreCode = this.storeCode;
            cls.Clerkid = this.clerkid;
            cls.Wireless_product_ID = this.wireless_product_ID;
            cls.Pin = this.pin;
//            cls.IsRedeemed = this.isRedeemed;
//            cls.RedeemDate = this.redeemDate;
            cls.TrConfirm += 2;
            this.trConfirm = cls.TrConfirm;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveWireless_Transactions - assert";
            
            cls = Wireless_Transactions.find(uow, this.wireless_Transaction_ID);
            Assertion.Assert(cls.TrConfirm == this.trConfirm);
            uow.close();
        }
        [Test]
        public void findAllWireless_Transactionses()
        {
            UOW uow = new UOW();
            uow.Service = "findAllWireless_Transactionses";
            Wireless_Transactions[] objs = Wireless_Transactions.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delWireless_Transactions()
        {
            UOW uow = new UOW();
            uow.Service = "delWireless_Transactions";
            Wireless_Transactions cls = Wireless_Transactions.find(uow, this.wireless_Transaction_ID);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delWireless_Transactions - assert";
            cls = Wireless_Transactions.find(uow, this.wireless_Transaction_ID);
            Assertion.Assert((cls.Wireless_Transaction_ID == 0));
            uow.close();
        }
    }
}
