using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;


namespace DPI.ComponentsTests
{
    [TestFixture]
    public class AgentInvoiceTests
    {
        /*		Data		*/
        int id;
        string invoiceType = "invoiceType";
        string vendor = "vendor";
        int agent = 27;
        DateTime startDate = DateTime.Now;
        DateTime endDate = DateTime.Now;
        string terms = "terms";
        decimal amount = 7.5M;
        string status = "status";
        
        /*		Constructors		*/
        public AgentInvoiceTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            AgentInvoiceTests test = new AgentInvoiceTests();
            
            // UOW Tests
            test.addAgentInvoice();
            test.findAgentInvoice();
            test.saveAgentInvoice();
            test.findAllAgentInvoices();
            
            try
            {
                test.delAgentInvoice();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delAgentInvoice:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delAgentInvoice: " + e.Message);
            }
            
        }
        [Test]
        public void addAgentInvoice()
        {
            UOW uow = new UOW();
            uow.Service = "addAgentInvoice";
            AgentInvoice cls = new AgentInvoice(uow);
            
            cls.InvoiceType = this.invoiceType;
            cls.Vendor = this.vendor;
            cls.Agent = this.agent;
            cls.StartDate = this.startDate;
            cls.EndDate = this.endDate;
            cls.Terms = this.terms;
            cls.Amount = this.amount;
            cls.Status = this.status;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addAgentInvoice - assert";
            
			AgentInvoice ai = AgentInvoice.find(uow, this.id);
			
			
            Assertion.Assert(ai.Vendor.Trim() == this.vendor.Trim());
            uow.close();
        }
        [Test]
        public void findAgentInvoice()
        {
            UOW uow = new UOW();
            uow.Service = "findAgentInvoice";
            
            AgentInvoice cls = AgentInvoice.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveAgentInvoice()
        {
            UOW uow = new UOW();
            uow.Service = "saveAgentInvoice";
            AgentInvoice cls = AgentInvoice.find(uow, this.id);
            
            cls.InvoiceType = this.invoiceType;
            cls.Vendor = this.vendor;
            cls.Agent = this.agent;
            cls.StartDate = this.startDate;
            cls.EndDate = this.endDate;
            cls.Terms = this.terms;
            cls.Amount = this.amount;
            cls.Status = this.status;
            cls.InvoiceType += " saved";
            this.invoiceType = cls.InvoiceType;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveAgentInvoice - assert";
            
            cls = AgentInvoice.find(uow, this.id);
            Assertion.Assert(cls.InvoiceType == this.invoiceType);
            uow.close();
        }
        [Test]
        public void findAllAgentInvoices()
        {
            UOW uow = new UOW();
            uow.Service = "findAllAgentInvoices";
            AgentInvoice[] objs = AgentInvoice.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delAgentInvoice()
        {
            UOW uow = new UOW();
            uow.Service = "delAgentInvoice";
            AgentInvoice cls = AgentInvoice.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delAgentInvoice - assert";
            cls = AgentInvoice.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
