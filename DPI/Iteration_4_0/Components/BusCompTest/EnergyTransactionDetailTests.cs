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
    public class EnergyTransactionDetailTests
    {
        /*		Data		*/
        int iD;
        int tranId = 2;
        decimal tranAmt = 2.5M;
        string description = "description";
        
        /*		Constructors		*/
        public EnergyTransactionDetailTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            EnergyTransactionDetailTests test = new EnergyTransactionDetailTests();
            
            // UOW Tests
            test.addEnergyTransactionDetail();
            test.findEnergyTransactionDetail();
            test.saveEnergyTransactionDetail();
            test.findAllEnergyTransactionDetails();
            
            try
            {
                test.delEnergyTransactionDetail();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delEnergyTransactionDetail:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delEnergyTransactionDetail: " + e.Message);
            }
            
        }
        [Test]
        public void addEnergyTransactionDetail()
        {
            UOW uow = new UOW();
            uow.Service = "addEnergyTransactionDetail";
            EnergyTransactionDetail cls = new EnergyTransactionDetail(uow);
            
            cls.TranId = this.tranId;
            cls.TranAmt = this.tranAmt;
            cls.Description = this.description;
        
            uow.commit();
            this.iD = cls.ID;
            
            uow = new UOW();
            uow.Service = "addEnergyTransactionDetail - assert";
            cls = EnergyTransactionDetail.find(uow, this.iD);
            Assertion.Assert(cls.TranAmt == this.tranAmt);
            uow.close();
        }
        [Test]
        public void findEnergyTransactionDetail()
        {
            UOW uow = new UOW();
            uow.Service = "findEnergyTransactionDetail";
            
            EnergyTransactionDetail cls = EnergyTransactionDetail.find(uow, this.iD);
            Assertion.Assert(cls.ID == this.iD);
            uow.close();
        }
        [Test]
        public void saveEnergyTransactionDetail()
        {
            UOW uow = new UOW();
            uow.Service = "saveEnergyTransactionDetail";
            EnergyTransactionDetail cls = EnergyTransactionDetail.find(uow, this.iD);
            
            cls.TranId = this.tranId;
            cls.TranAmt = this.tranAmt;
            cls.Description = this.description;
            cls.TranId += 2;
            this.tranId = cls.TranId;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveEnergyTransactionDetail - assert";
            
            cls = EnergyTransactionDetail.find(uow, this.iD);
            Assertion.Assert(cls.TranId == this.tranId);
            uow.close();
        }
        [Test]
        public void findAllEnergyTransactionDetails()
        {
            UOW uow = new UOW();
            uow.Service = "findAllEnergyTransactionDetails";
            EnergyTransactionDetail[] objs = EnergyTransactionDetail.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delEnergyTransactionDetail()
        {
            UOW uow = new UOW();
            uow.Service = "delEnergyTransactionDetail";
            EnergyTransactionDetail cls = EnergyTransactionDetail.find(uow, this.iD);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delEnergyTransactionDetail - assert";
            cls = EnergyTransactionDetail.find(uow, this.iD);
            Assertion.Assert((cls.ID == 0));
            uow.close();
        }
    }
}
