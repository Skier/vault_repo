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
    public class Customer_ROPTests
    {
        /*		Data		*/
        int id;
        DateTime dateInserted = DateTime.Now;
        DateTime dateModified = DateTime.Now;
        string userId = "userId";
        int accNumber = 5;
        string billingFirstName = "billingFirstName";
        string billingLastName = "billingLastName";
        string billingAddress = "billingAddress";
        string billingCity = "billingCity";
        string billingState = "billingState";
        string billingZip = "billingZip";
        string phNumber = "phNumber";
        bool active = true;
        int accountTypeId = 14;
        string bAccNumber = "bAccNumber";
        string bRouteNumber = "bRouteNumber";
        string dLStateNumber = "dLStateNumber";
        string expirationMonthYear = "expirationMonthYear";
        string cVV2 = "cVV2";
        int priority = 20;
        
        /*		Constructors		*/
        public Customer_ROPTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            Customer_ROPTests test = new Customer_ROPTests();
            
            // UOW Tests
            test.addCustomer_ROP();
            test.findCustomer_ROP();
            test.saveCustomer_ROP();
            test.findAllCustomer_ROPs();
            
            try
            {
                test.delCustomer_ROP();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delCustomer_ROP:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delCustomer_ROP: " + e.Message);
            }
            
        }
        [Test]
        public void addCustomer_ROP()
        {
            UOW uow = new UOW();
            uow.Service = "addCustomer_ROP";
            Customer_ROP cls = new Customer_ROP(uow);
            
            cls.DateInserted = this.dateInserted;
            cls.DateModified = this.dateModified;
            cls.UserId = this.userId;
            cls.AccNumber = this.accNumber;
            cls.BillingFirstName = this.billingFirstName;
            cls.BillingLastName = this.billingLastName;
            cls.BillingAddress = this.billingAddress;
            cls.BillingCity = this.billingCity;
            cls.BillingState = this.billingState;
            cls.BillingZip = this.billingZip;
            cls.PhNumber = this.phNumber;
            cls.Active = this.active;
            cls.AccountTypeId = this.accountTypeId;
            cls.BAccNumber = this.bAccNumber;
            cls.BRouteNumber = this.bRouteNumber;
            cls.DLStateNumber = this.dLStateNumber;
            cls.ExpirationMonthYear = this.expirationMonthYear;
            cls.CVV2 = this.cVV2;
            cls.Priority = this.priority;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addCustomer_ROP - assert";
            cls = Customer_ROP.find(uow, this.id);
            Assertion.Assert(cls.DateModified == this.dateModified);
            uow.close();
        }
        [Test]
        public void findCustomer_ROP()
        {
            UOW uow = new UOW();
            uow.Service = "findCustomer_ROP";
            
            Customer_ROP cls = Customer_ROP.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveCustomer_ROP()
        {
            UOW uow = new UOW();
            uow.Service = "saveCustomer_ROP";
            Customer_ROP cls = Customer_ROP.find(uow, this.id);
            
            cls.DateInserted = this.dateInserted;
            cls.DateModified = this.dateModified;
            cls.UserId = this.userId;
            cls.AccNumber = this.accNumber;
            cls.BillingFirstName = this.billingFirstName;
            cls.BillingLastName = this.billingLastName;
            cls.BillingAddress = this.billingAddress;
            cls.BillingCity = this.billingCity;
            cls.BillingState = this.billingState;
            cls.BillingZip = this.billingZip;
            cls.PhNumber = this.phNumber;
            cls.Active = this.active;
            cls.AccountTypeId = this.accountTypeId;
            cls.BAccNumber = this.bAccNumber;
            cls.BRouteNumber = this.bRouteNumber;
            cls.DLStateNumber = this.dLStateNumber;
            cls.ExpirationMonthYear = this.expirationMonthYear;
            cls.CVV2 = this.cVV2;
            cls.Priority = this.priority;
            cls.DateInserted.AddDays(2.0);
            this.dateInserted = cls.DateInserted;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveCustomer_ROP - assert";
            
            cls = Customer_ROP.find(uow, this.id);
            Assertion.Assert(cls.DateInserted == this.dateInserted);
            uow.close();
        }
        [Test]
        public void findAllCustomer_ROPs()
        {
            UOW uow = new UOW();
            uow.Service = "findAllCustomer_ROPs";
            Customer_ROP[] objs = Customer_ROP.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delCustomer_ROP()
        {
            UOW uow = new UOW();
            uow.Service = "delCustomer_ROP";
            Customer_ROP cls = Customer_ROP.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delCustomer_ROP - assert";
            cls = Customer_ROP.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
