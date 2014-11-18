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
    public class DeliverableTests
    {
        /*		Data		*/
        int dlvId;
        int ver = 2;
        int predDlvId = 3;
        string dlvType = "Charge";
        string aRType = "DR";
        int stmtId = 1;
        int prodId = 201;
        int locId = 3646;
        int supplier = 22;
        int dmdItem = 1;
        DateTime effDate = DateTime.Now;
        DateTime effEndDate = DateTime.Now;
        DateTime datePosted = DateTime.Now;
        string priceRule = "ABC";
        decimal amt = 14.5M;
        int qt = 16;
        string uOM = "Each";
        decimal taxSurAmt = 17.5M;
        decimal totalAmt = 18.5M;
        decimal availForXferBal = 19.5M;
        string adjReasonCode = "ReasonCode";
        string cSR = "cSR";
        
        /*		Constructors		*/
        public DeliverableTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            DeliverableTests test = new DeliverableTests();
            
            // UOW Tests
            test.addDeliverable();
            test.findDeliverable();
            test.saveDeliverable();
            test.findAllDeliverables();
            
            try
            {
                test.delDeliverable();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delDeliverable:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delDeliverable: " + e.Message);
            }
            
        }
        [Test]
        public void addDeliverable()
        {
            UOW uow = new UOW();
            uow.Service = "addDeliverable";
            Deliverable cls = new Deliverable(uow);
            
            cls.PredDlvId = this.predDlvId;
            cls.DlvType = this.dlvType;
            cls.ARType = this.aRType;
         //   cls.Stmt = this.stmtId;
            cls.ProdId = this.prodId;
            cls.LocId = this.locId;
            cls.Supplier = this.supplier;
         //   cls.DmdItm = this.dmdItem;
            cls.EffDate = this.effDate;
            cls.EffEndDate = this.effEndDate;
            cls.DatePosted = this.datePosted;
            cls.PriceRule = this.priceRule;
            cls.Amt = this.amt;
            cls.Qt = this.qt;
            cls.UOM = this.uOM;
            cls.TaxSurAmt = this.taxSurAmt;
            cls.TotalAmt = this.totalAmt;
            cls.AvailForXferBal = this.availForXferBal;
            cls.AdjReasonCode = this.adjReasonCode;
            cls.CSR = this.cSR;
        
            uow.commit();
            this.dlvId = cls.DlvId;
            this.ver = cls.Ver;
            
            uow = new UOW();
            uow.Service = "addDeliverable - assert";
            cls = Deliverable.find(uow, this.dlvId);
            Assertion.Assert(cls.PredDlvId == this.predDlvId);
            uow.close();
        }
        [Test]
        public void findDeliverable()
        {
            UOW uow = new UOW();
            uow.Service = "findDeliverable";
            
            Deliverable cls = Deliverable.find(uow, this.dlvId);
            Assertion.Assert(cls.DlvId == this.dlvId);
            uow.close();
        }
        [Test]
        public void saveDeliverable()
        {
            UOW uow = new UOW();
            uow.Service = "saveDeliverable";
            Deliverable cls = Deliverable.find(uow, this.dlvId);
            
            cls.PredDlvId = this.predDlvId;
            cls.DlvType = this.dlvType;
            cls.ARType = this.aRType;
          //  cls.Stmt = this.stmtId;
            cls.ProdId = this.prodId;
            cls.LocId = this.locId;
            cls.Supplier = this.supplier;
         //   cls.DmdItm = this.dmdItem; 
            cls.EffDate = this.effDate;
            cls.EffEndDate = this.effEndDate;
            cls.DatePosted = this.datePosted;
            cls.PriceRule = this.priceRule;
            cls.Amt = this.amt;
            cls.Qt = this.qt;
            cls.UOM = this.uOM;
            cls.TaxSurAmt = this.taxSurAmt;
            cls.TotalAmt = this.totalAmt;
            cls.AvailForXferBal = this.availForXferBal;
            cls.AdjReasonCode = this.adjReasonCode;
            cls.CSR = this.cSR;
            cls.PredDlvId += 2;
            this.predDlvId = cls.PredDlvId;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveDeliverable - assert";
            
            cls = Deliverable.find(uow, this.dlvId);
            Assertion.Assert(cls.PredDlvId == this.predDlvId);
            uow.close();
        }
        [Test]
        public void findAllDeliverables()
        {
            UOW uow = new UOW();
            uow.Service = "findAllDeliverables";
            Deliverable[] objs = Deliverable.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delDeliverable()
        {
            UOW uow = new UOW();
            uow.Service = "delDeliverable";
            Deliverable cls = Deliverable.find(uow, this.dlvId);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delDeliverable - assert";
            cls = Deliverable.find(uow, this.dlvId);
            Assertion.Assert((cls.DlvId == 0));
            uow.close();
        }
    }
}
