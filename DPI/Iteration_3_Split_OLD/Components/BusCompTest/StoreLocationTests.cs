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
    public class StoreLocationTests
    {
        /*		Data		*/
        string storeCode = "storeCode";
        string storeClass = "storeClass";
        string name = "name";
        string storeNumber = "storeNumber";
        string address = "address";
        string city = "city";
        string state = "state";
        string zip = "zip";
        string phone = "phone";
        string fax = "fax";
        string manager = "manager";
        bool active = true;
        DateTime activeDate = DateTime.Now;
        string priceCode = "priceCode";
        string wireless_PriceCode = "wireless_PriceCode";
        string notes = "notes";
        string addLocInf = "addLocInf";
        DateTime termDate = DateTime.Now;
        string status = "status";
        string ilec = "ilec";
        int dMA = 21;
        int corpID = 22;
        string type = "type";
        int internet_Channel_ID = 24;
        bool localService = true;
        bool wireless = true;
        bool internet = true;
        bool smartConnect = true;
        decimal nET_FlatRate = 28.5M;
        decimal sC_FlatRate = 29.5M;
        decimal lS_FlatRate = 30.5M;
        string divisional_Manager = "divisional_Manager";
        
        /*		Constructors		*/
        public StoreLocationTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            StoreLocationTests test = new StoreLocationTests();
            
            // UOW Tests
            test.addStoreLocation();
            test.findStoreLocation();
            test.saveStoreLocation();
            test.findAllStoreLocations();
            
            try
            {
                test.delStoreLocation();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delStoreLocation:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delStoreLocation: " + e.Message);
            }
            
        }
        [Test]
        public void addStoreLocation()
        {
            UOW uow = new UOW();
            uow.Service = "addStoreLocation";
            StoreLocation cls = new StoreLocation(uow);
            
            cls.StoreCode = this.storeCode;
            cls.StoreClass = this.storeClass;
            cls.Name = this.name;
            cls.StoreNumber = this.storeNumber;
            cls.Address = this.address;
            cls.City = this.city;			
            //cls.State = this.state;
            cls.Zip = this.zip;
            cls.Phone = this.phone;
            cls.Fax = this.fax;
            cls.Manager = this.manager;
            cls.Active = this.active;
            cls.ActiveDate = this.activeDate;
            cls.PriceCode = this.priceCode;
            cls.Wireless_PriceCode = this.wireless_PriceCode;
            cls.Notes = this.notes;
            cls.AddLocInf = this.addLocInf;
            cls.TermDate = this.termDate;
            cls.Status = this.status;
            cls.Ilec = this.ilec;
            cls.DMA = this.dMA;
            cls.CorpID = this.corpID;
            cls.Type = this.type;
            cls.Internet_Channel_ID = this.internet_Channel_ID;
            cls.LocalService = this.localService;
            cls.Wireless = this.wireless;
            cls.Internet = this.internet;
            cls.SmartConnect = this.smartConnect;
            cls.NET_FlatRate = this.nET_FlatRate;
            cls.SC_FlatRate = this.sC_FlatRate;
            cls.LS_FlatRate = this.lS_FlatRate;
            cls.Divisional_Manager = this.divisional_Manager;
        
            uow.commit();
            
            uow = new UOW();
            uow.Service = "addStoreLocation - assert";
            cls = StoreLocation.find(uow, this.storeCode);
            Assertion.Assert(cls.Name == this.name);
            uow.close();
        }
        [Test]
        public void findStoreLocation()
        {
            UOW uow = new UOW();
            uow.Service = "findStoreLocation";
            
            StoreLocation cls = StoreLocation.find(uow, this.storeCode);
            Assertion.Assert(cls.StoreCode.Trim() == this.storeCode.Trim());
            uow.close();
        }
        [Test]
        public void saveStoreLocation()
        {
            UOW uow = new UOW();
            uow.Service = "saveStoreLocation";
            StoreLocation cls = StoreLocation.find(uow, this.storeCode);
            
            cls.StoreCode = this.storeCode;
            cls.StoreClass = this.storeClass;
            cls.Name = this.name;
            cls.StoreNumber = this.storeNumber;
            cls.Address = this.address;
            cls.City = this.city;
            //cls.State = this.state;
            cls.Zip = this.zip;
            cls.Phone = this.phone;
            cls.Fax = this.fax;
            cls.Manager = this.manager;
            cls.Active = this.active;
            cls.ActiveDate = this.activeDate;
            cls.PriceCode = this.priceCode;
            cls.Wireless_PriceCode = this.wireless_PriceCode;
            cls.Notes = this.notes;
            cls.AddLocInf = this.addLocInf;
            cls.TermDate = this.termDate;
            cls.Status = this.status;
            cls.Ilec = this.ilec;
            cls.DMA = this.dMA;
            cls.CorpID = this.corpID;
            cls.Type = this.type;
            cls.Internet_Channel_ID = this.internet_Channel_ID;
            cls.LocalService = this.localService;
            cls.Wireless = this.wireless;
            cls.Internet = this.internet;
            cls.SmartConnect = this.smartConnect;
            cls.NET_FlatRate = this.nET_FlatRate;
            cls.SC_FlatRate = this.sC_FlatRate;
            cls.LS_FlatRate = this.lS_FlatRate;
            cls.Divisional_Manager = this.divisional_Manager;
            cls.StoreClass += " saved";
            this.storeClass = cls.StoreClass;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveStoreLocation - assert";
            
            cls = StoreLocation.find(uow, this.storeCode);
            Assertion.Assert(cls.StoreClass == this.storeClass);
            uow.close();
        }
        [Test]
        public void findAllStoreLocations()
        {
            UOW uow = new UOW();
            uow.Service = "findAllStoreLocations";
            StoreLocation[] objs = StoreLocation.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delStoreLocation()
        {
            UOW uow = new UOW();
            uow.Service = "delStoreLocation";
            StoreLocation cls = StoreLocation.find(uow, this.storeCode);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delStoreLocation - assert";
            cls = StoreLocation.find(uow, this.storeCode);
            Assertion.Assert((cls.StoreCode ==  null));
            uow.close();
        }
    }
}
