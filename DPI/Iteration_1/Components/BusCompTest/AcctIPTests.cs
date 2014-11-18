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
    public class AcctIPTests
    {
        /*		Data		*/
        int id;
        int corpId = 496;
        string privateIP = "127.0.0.7";
        int autoLoginAcct = 2185;
        string autoLoginPw = "pw";
        string status = "Active";
     //   string externalStoreNum = "externalStoreNum";
       // string storeCode = "storeCode";
        
        /*		Constructors		*/
        public AcctIPTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            AcctIPTests test = new AcctIPTests();
            
            // UOW Tests
            test.addAcctIP();
            test.findAcctIP();
            test.saveAcctIP();
            test.findAllAcctIPs();
            
            try
            {
                test.delAcctIP();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delAcctIP:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delAcctIP: " + e.Message);
            }
            
        }
        [Test]
        public void addAcctIP()
        {
            UOW uow = new UOW();
            uow.Service = "addAcctIP";
            AcctIP cls = new AcctIP(uow);
            
            cls.CorpId = this.corpId;
            cls.PrivateIP = this.privateIP;
            cls.AutoLoginAcct = this.autoLoginAcct;
            cls.AutoLoginPw = this.autoLoginPw;
            cls.Status = this.status;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addAcctIP - assert";
            cls = AcctIP.find(uow, this.id);
            Assertion.Assert(cls.PrivateIP == this.privateIP);
            uow.close();
        }
        [Test]
        public void findAcctIP()
        {
            UOW uow = new UOW();
            uow.Service = "findAcctIP";
            
            AcctIP cls = AcctIP.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveAcctIP()
        {
            UOW uow = new UOW();
            uow.Service = "saveAcctIP";
            AcctIP cls = AcctIP.find(uow, this.id);
            
            cls.CorpId = this.corpId;
            cls.PrivateIP = this.privateIP;
            cls.AutoLoginAcct = this.autoLoginAcct;
            cls.AutoLoginPw = this.autoLoginPw;
            cls.Status = this.status;
            cls.CorpId += 2;
            this.corpId = cls.CorpId;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveAcctIP - assert";
            
            cls = AcctIP.find(uow, this.id);
            Assertion.Assert(cls.CorpId == this.corpId);
            uow.close();
        }
        [Test]
        public void findAllAcctIPs()
        {
            UOW uow = new UOW();
            uow.Service = "findAllAcctIPs";
            AcctIP[] objs = AcctIP.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delAcctIP()
        {
            UOW uow = new UOW();
            uow.Service = "delAcctIP";
            AcctIP cls = AcctIP.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delAcctIP - assert";
            cls = AcctIP.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
