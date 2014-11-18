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
    public class StoreStatsTests2
    {
        /*		Data		*/
        int id;
        DateTime statDate = new DateTime(2005, 2, 18);
        int corpId = 27;
        string storeCode = "KSRW1485RW";
        string storeNumber = "1485";
        int activeCustCnt=2;
        int mDT_NewCustCnt=0;
        decimal revenue=0;
        decimal lDRevenue=0;
        decimal wirelessRev=0;
        
        /*		Constructors		*/
        public StoreStatsTests2()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            StoreStatsTests2 test = new StoreStatsTests2();
            
            // UOW Tests
            //test.addStoreStats();
            //test.findStoreStats();
            //test.saveStoreStats();
            test.findAllStoreStatses();
            
            try
            {
                test.delStoreStats();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delStoreStats:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delStoreStats: " + e.Message);
            }
            
        }
        [Test]
        public void addStoreStats()
        {
            UOW uow = new UOW();
            uow.Service = "findStoreStats";
            StoreStats2 cls = new StoreStats2(uow);
            
            cls.StatDate = this.statDate;
            cls.CorpId = this.corpId;
            cls.StoreCode = this.storeCode;
            cls.StoreNumber = this.storeNumber;
            cls.ActiveCust = this.activeCustCnt;
            cls.MDT_NewCustCnt = this.mDT_NewCustCnt;
           // cls.Revenue = this.revenue;
            cls.LDRevenue = this.lDRevenue;
            cls.WirelessRev = this.wirelessRev;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addStoreStats - assert";
            cls = StoreStats2.find(uow, this.id);
            Assertion.Assert(cls.CorpId == this.corpId);
            uow.close();
        }
        [Test]
        public void findStoreStats()
        {
            UOW uow = new UOW();
            uow.Service = "findStoreStats";
            
            StoreStats2 cls = StoreStats2.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveStoreStats()
        {
            UOW uow = new UOW();
            uow.Service = "saveStoreStats";
            StoreStats2 cls = StoreStats2.find(uow, this.id);
            
            cls.StatDate = this.statDate;
            cls.CorpId = this.corpId;
            cls.StoreCode = this.storeCode;
            cls.StoreNumber = this.storeNumber;
            cls.ActiveCust = this.activeCustCnt;
            cls.MDT_NewCustCnt = this.mDT_NewCustCnt;
           // cls.Revenue = this.revenue;
            cls.LDRevenue = this.lDRevenue;
            cls.WirelessRev = this.wirelessRev;
            cls.StatDate.AddDays(2.0);
            this.statDate = cls.StatDate;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveStoreStats - assert";
            
            cls = StoreStats2.find(uow, this.id);
            Assertion.Assert(cls.StatDate == this.statDate);
            uow.close();
        }
        [Test]
        public void findAllStoreStatses()
        {
            UOW uow = new UOW();
            uow.Service = "findAllStoreStatses";
            StoreStats2[] objs = StoreStats2.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delStoreStats()
        {
            UOW uow = new UOW();
            uow.Service = "delStoreStats";
            StoreStats2 cls = StoreStats2.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delStoreStats - assert";
            cls = StoreStats2.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
