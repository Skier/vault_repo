using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;


namespace DPI.ComponentsTests
{
    [TestFixture]
    public class CardAppTests
    {
        /*		Data		*/
        int id;
        string appType = "appType";
        DateTime appDate = DateTime.Now;
        string idNumber = "idNumber";
        string idState = "idState";
        string idType = "idType";
        DateTime idExpDate = DateTime.Now;
        int dmd = 8;
        string cardNumber = "cardNumber";
        string expMonth = "expMonth";
        string expYear = "expYear";
        string prevCard = "prevCard";
        bool approved = true;
        string status = "status";
        
        /*		Constructors		*/
        public CardAppTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            CardAppTests test = new CardAppTests();
            
            // UOW Tests
            test.addCardApp();
            test.findCardApp();
            test.saveCardApp();
            test.findAllCardApps();
            
            try
            {
                test.delCardApp();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delCardApp:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delCardApp: " + e.Message);
            }
            
        }
        [Test]
        public void addCardApp()
        {
            UOW uow = new UOW();
            uow.Service = "addCardApp";
            CardApp cls = new CardApp(uow);
            
            cls.AppType = this.appType;
            cls.AppDate = this.appDate;
            cls.IdNumber = this.idNumber;
            cls.IdState = this.idState;
            cls.IdType = this.idType;
            cls.IdExpDate = this.idExpDate;
            cls.Dmd = this.dmd;
            cls.CardNum = this.cardNumber;
            cls.ExpMonth = this.expMonth;
            cls.ExpYear = this.expYear;
            cls.PrevCard = this.prevCard;
            cls.Approved = this.approved;
            cls.Status = this.status;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addCardApp - assert";
            cls = CardApp.find(uow, this.id);
            Assertion.Assert(cls.CardNum == this.cardNumber);
            uow.close();
        }
        [Test]
        public void findCardApp()
        {
            UOW uow = new UOW();
            uow.Service = "findCardApp";
            
            CardApp cls = CardApp.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveCardApp()
        {
            UOW uow = new UOW();
            uow.Service = "saveCardApp";
            CardApp cls = CardApp.find(uow, this.id);
            
            cls.AppType = this.appType;
            cls.AppDate = this.appDate;
            cls.IdNumber = this.idNumber;
            cls.IdState = this.idState;
            cls.IdType = this.idType;
            cls.IdExpDate = this.idExpDate;
            cls.Dmd = this.dmd;
            cls.CardNum = this.cardNumber;
            cls.ExpMonth = this.expMonth;
            cls.ExpYear = this.expYear;
            cls.PrevCard = this.prevCard;
            cls.Approved = this.approved;
            cls.Status = this.status;
            cls.AppType += " saved";
            this.appType = cls.AppType;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveCardApp - assert";
            
            cls = CardApp.find(uow, this.id);
            Assertion.Assert(cls.AppType == this.appType);
            uow.close();
        }
        [Test]
        public void findAllCardApps()
        {
            UOW uow = new UOW();
            uow.Service = "findAllCardApps";
            CardApp[] objs = CardApp.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delCardApp()
        {
            UOW uow = new UOW();
            uow.Service = "delCardApp";
            CardApp cls = CardApp.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delCardApp - assert";
            cls = CardApp.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
