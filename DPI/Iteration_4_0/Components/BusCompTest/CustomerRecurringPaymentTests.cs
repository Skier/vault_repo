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
    public class CustomerRecurringPaymentTests
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
        string emailAddress = "emailAddress";
        bool active = true;
        int accountTypeId = 15;
        string bAccNumber = "bAccNumber";
        string bRouteNumber = "bRouteNumber";
        string dLStateNumber = "dLStateNumber";
        string expirationMonthYear = "expirationMonthYear";
        string cVV2 = "cVV2";
        int priority = 21;
        
        /*		Constructors		*/
        public CustomerRecurringPaymentTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            CustomerRecurringPaymentTests test = new CustomerRecurringPaymentTests();
            
            // UOW Tests
            test.addCustomerRecurringPayment();
            test.findCustomerRecurringPayment();
            test.saveCustomerRecurringPayment();
            test.findAllCustomerRecurringPayments();
            
            try
            {
                test.delCustomerRecurringPayment();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delCustomerRecurringPayment:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delCustomerRecurringPayment: " + e.Message);
            }
            
        }
        [Test]
        public void addCustomerRecurringPayment()
        {
            UOW uow = new UOW();
            uow.Service = "addCustomerRecurringPayment";
            CustomerRecurringPayment cls = new CustomerRecurringPayment(uow);
            
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
            cls.EmailAddress = this.emailAddress;
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
            uow.Service = "addCustomerRecurringPayment - assert";
            cls = CustomerRecurringPayment.find(uow, this.id);
            Assertion.Assert(cls.DateModified == this.dateModified);
            uow.close();
        }
        [Test]
        public void findCustomerRecurringPayment()
        {
            UOW uow = new UOW();
            uow.Service = "findCustomerRecurringPayment";
            
            CustomerRecurringPayment cls = CustomerRecurringPayment.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveCustomerRecurringPayment()
        {
            UOW uow = new UOW();
            uow.Service = "saveCustomerRecurringPayment";
            CustomerRecurringPayment cls = CustomerRecurringPayment.find(uow, this.id);
            
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
            cls.EmailAddress = this.emailAddress;
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
            uow.Service = "saveCustomerRecurringPayment - assert";
            
            cls = CustomerRecurringPayment.find(uow, this.id);
            Assertion.Assert(cls.DateInserted == this.dateInserted);
            uow.close();
        }
        [Test]
        public void findAllCustomerRecurringPayments()
        {
            UOW uow = new UOW();
            uow.Service = "findAllCustomerRecurringPayments";
            CustomerRecurringPayment[] objs = CustomerRecurringPayment.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delCustomerRecurringPayment()
        {
            UOW uow = new UOW();
            uow.Service = "delCustomerRecurringPayment";
            CustomerRecurringPayment cls = CustomerRecurringPayment.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delCustomerRecurringPayment - assert";
            cls = CustomerRecurringPayment.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
