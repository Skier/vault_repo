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
    public class EnergyQuotationTests
    {
        /*		Data		*/
        int iD;
        decimal prepayAmt = 1.5M;
        int estimatedUsage = 3;
        decimal ratePerKwh = 3.5M;
        
        /*		Constructors		*/
        public EnergyQuotationTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            EnergyQuotationTests test = new EnergyQuotationTests();
            
            // UOW Tests
            test.addEnergyQuotation();
            test.findEnergyQuotation();
            test.saveEnergyQuotation();
            test.findAllEnergyQuotations();
            
            try
            {
                test.delEnergyQuotation();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delEnergyQuotation:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delEnergyQuotation: " + e.Message);
            }
            
        }
        [Test]
        public void addEnergyQuotation()
        {
            UOW uow = new UOW();
            uow.Service = "addEnergyQuotation";
            EnergyQuotation cls = new EnergyQuotation(uow);
            
            cls.PrepayAmt = this.prepayAmt;
            cls.EstimatedUsage = this.estimatedUsage;
            cls.RatePerKwh = this.ratePerKwh;
        
            uow.commit();
            this.iD = cls.ID;
            
            uow = new UOW();
            uow.Service = "addEnergyQuotation - assert";
            cls = EnergyQuotation.find(uow, this.iD);
            Assertion.Assert(cls.EstimatedUsage == this.estimatedUsage);
            uow.close();
        }
        [Test]
        public void findEnergyQuotation()
        {
            UOW uow = new UOW();
            uow.Service = "findEnergyQuotation";
            
            EnergyQuotation cls = EnergyQuotation.find(uow, this.iD);
            Assertion.Assert(cls.ID == this.iD);
            uow.close();
        }
        [Test]
        public void saveEnergyQuotation()
        {
            UOW uow = new UOW();
            uow.Service = "saveEnergyQuotation";
            EnergyQuotation cls = EnergyQuotation.find(uow, this.iD);
            
            cls.PrepayAmt = this.prepayAmt;
            cls.EstimatedUsage = this.estimatedUsage;
            cls.RatePerKwh = this.ratePerKwh;
            cls.PrepayAmt += 2;
            this.prepayAmt = cls.PrepayAmt;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveEnergyQuotation - assert";
            
            cls = EnergyQuotation.find(uow, this.iD);
            Assertion.Assert(cls.PrepayAmt == this.prepayAmt);
            uow.close();
        }
        [Test]
        public void findAllEnergyQuotations()
        {
            UOW uow = new UOW();
            uow.Service = "findAllEnergyQuotations";
            EnergyQuotation[] objs = EnergyQuotation.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delEnergyQuotation()
        {
            UOW uow = new UOW();
            uow.Service = "delEnergyQuotation";
            EnergyQuotation cls = EnergyQuotation.find(uow, this.iD);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delEnergyQuotation - assert";
            cls = EnergyQuotation.find(uow, this.iD);
            Assertion.Assert((cls.ID == 0));
            uow.close();
        }
    }
}
