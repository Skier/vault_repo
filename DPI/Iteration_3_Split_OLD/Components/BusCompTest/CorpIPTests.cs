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
    public class CorpIPTests
    {
        /*		Data		*/
        string publicIP = "7.7.7.7";
        int corpId = 496;
        
        /*		Constructors		*/
        public CorpIPTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            CorpIPTests test = new CorpIPTests();
            
            // UOW Tests
            test.addCorpIP();
            test.findCorpIP();
            test.saveCorpIP();
            test.findAllCorpIPs();
            
            try
            {
                test.delCorpIP();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delCorpIP:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delCorpIP: " + e.Message);
            }
            
        }
        [Test]
        public void addCorpIP()
        {
            UOW uow = new UOW();
            uow.Service = "addCorpIP";
            CorpIP cls = new CorpIP(uow);
            
            cls.PublicIP = this.publicIP;
            cls.CorpId = this.corpId;
        
            uow.commit();
            
            uow = new UOW();
            uow.Service = "addCorpIP - assert";
            cls = CorpIP.find(uow, this.publicIP);
            Assertion.Assert(cls.CorpId == this.corpId);
            uow.close();
        }
        [Test]
        public void findCorpIP()
        {
            UOW uow = new UOW();
            uow.Service = "findCorpIP";
            
            CorpIP cls = CorpIP.find(uow, this.publicIP);
            Assertion.Assert(cls.PublicIP.Trim() == this.publicIP.Trim());
            uow.close();
        }
        [Test]
        public void saveCorpIP()
        {
            UOW uow = new UOW();
            uow.Service = "saveCorpIP";
            CorpIP cls = CorpIP.find(uow, this.publicIP);
            
            cls.PublicIP = this.publicIP;
            cls.CorpId = this.corpId;
            cls.CorpId += 2;
            this.corpId = cls.CorpId;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveCorpIP - assert";
            
            cls = CorpIP.find(uow, this.publicIP);
            Assertion.Assert(cls.CorpId == this.corpId);
            uow.close();
        }
        [Test]
        public void findAllCorpIPs()
        {
            UOW uow = new UOW();
            uow.Service = "findAllCorpIPs";
            CorpIP[] objs = CorpIP.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delCorpIP()
        {
            UOW uow = new UOW();
            uow.Service = "delCorpIP";
            CorpIP cls = CorpIP.find(uow, this.publicIP);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delCorpIP - assert";
            cls = CorpIP.find(uow, this.publicIP);
            Assertion.Assert((cls.PublicIP ==  null));
            uow.close();
        }
    }
}
