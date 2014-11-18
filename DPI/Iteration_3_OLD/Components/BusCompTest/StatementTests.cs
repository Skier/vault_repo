using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.ComponentsTests
{
    [TestFixture]
    public class StatementTests
    {
        /*		Data		*/
        int stmtId;
        int ver = 2;
        int billpayer = 3;
        string stmtType = "CustAcct";
        string status = "Pending";
        DateTime perStartDate = DateTime.Now;
        DateTime perEndDate = DateTime.Now;
        DateTime lastPymtDate = DateTime.Now;
        decimal lastPymtAmt = 8.5M;
        DateTime pymtDueDate = DateTime.Now;
        decimal pymtDueAmt = 10.5M;
        decimal totBalance = 11.5M;
        decimal unapplCR = 12.5M;
        decimal unbldBalance = 13.5M;
        decimal unbldDR = 14.5M;
        decimal unbldUsageDR = 15.5M;
        decimal unbldCR = 16.5M;
        decimal ubldApplCR = 17.5M;
        decimal billedBalance = 18.5M;
        decimal billedDR = 19.5M;
        decimal billedApplCR = 20.5M;
        decimal pD30Balance = 21.5M;
        decimal pD30DR = 22.5M;
        decimal pD30ApplCR = 23.5M;
        decimal pDOver30Balance = 24.5M;
        decimal pD0ver30DR = 25.5M;
        decimal pD0ver30ApplCR = 26.5M;
        
        /*		Constructors		*/
        public StatementTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            StatementTests test = new StatementTests();
            
            // UOW Tests
            test.addStatement();
            test.findStatement();
            test.saveStatement();
            test.findAllStatements();
            
            try
            {
                test.delStatement();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delStatement:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delStatement: " + e.Message);
            }
            
        }
        [Test]
        public void addStatement()
        {
            UOW uow = new UOW();
            uow.Service = "addStatement";
            Statement cls = new Statement(uow);
            
            cls.Billpayer = this.billpayer;
            cls.StmtType = this.stmtType;
            cls.Status = this.status;
            cls.PerStartDate = this.perStartDate;
            cls.PerEndDate = this.perEndDate;
            cls.LastPymtDate = this.lastPymtDate;
            cls.LastPymtAmt = this.lastPymtAmt;
            cls.PymtDueDate = this.pymtDueDate;
            cls.PymtDueAmt = this.pymtDueAmt;
            cls.TotBalance = this.totBalance;
            cls.UnapplCR = this.unapplCR;
            cls.UnbldBalance = this.unbldBalance;
            cls.UnbldDR = this.unbldDR;
            cls.UnbldUsageDR = this.unbldUsageDR;
            cls.UnbldCR = this.unbldCR;
            cls.UbldApplCR = this.ubldApplCR;
            cls.BilledBalance = this.billedBalance;
            cls.BilledDR = this.billedDR;
            cls.BilledApplCR = this.billedApplCR;
            cls.PD30Balance = this.pD30Balance;
            cls.PD30DR = this.pD30DR;
            cls.PD30ApplCR = this.pD30ApplCR;
            cls.PDOver30Balance = this.pDOver30Balance;
            cls.PD0ver30DR = this.pD0ver30DR;
            cls.PD0ver30ApplCR = this.pD0ver30ApplCR;
        
            uow.commit();
            this.stmtId = cls.StmtId;
            this.ver = cls.Ver;
            
            uow = new UOW();
            uow.Service = "addStatement - assert";
            cls = Statement.find(uow, this.stmtId);
            Assertion.Assert(cls.Billpayer == this.billpayer);
            uow.close();
        }
        [Test]
        public void findStatement()
        {
            UOW uow = new UOW();
            uow.Service = "findStatement";
            
            Statement cls = Statement.find(uow, this.stmtId);
            Assertion.Assert(cls.StmtId == this.stmtId);
            uow.close();
        }
        [Test]
        public void saveStatement()
        {
            UOW uow = new UOW();
            uow.Service = "saveStatement";
            Statement cls = Statement.find(uow, this.stmtId);
            
            cls.Billpayer = this.billpayer;
            cls.StmtType = this.stmtType;
            cls.Status = this.status;
            cls.PerStartDate = this.perStartDate;
            cls.PerEndDate = this.perEndDate;
            cls.LastPymtDate = this.lastPymtDate;
            cls.LastPymtAmt = this.lastPymtAmt;
            cls.PymtDueDate = this.pymtDueDate;
            cls.PymtDueAmt = this.pymtDueAmt;
            cls.TotBalance = this.totBalance;
            cls.UnapplCR = this.unapplCR;
            cls.UnbldBalance = this.unbldBalance;
            cls.UnbldDR = this.unbldDR;
            cls.UnbldUsageDR = this.unbldUsageDR;
            cls.UnbldCR = this.unbldCR;
            cls.UbldApplCR = this.ubldApplCR;
            cls.BilledBalance = this.billedBalance;
            cls.BilledDR = this.billedDR;
            cls.BilledApplCR = this.billedApplCR;
            cls.PD30Balance = this.pD30Balance;
            cls.PD30DR = this.pD30DR;
            cls.PD30ApplCR = this.pD30ApplCR;
            cls.PDOver30Balance = this.pDOver30Balance;
            cls.PD0ver30DR = this.pD0ver30DR;
            cls.PD0ver30ApplCR = this.pD0ver30ApplCR;
            cls.Billpayer += 2;
            this.billpayer = cls.Billpayer;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveStatement - assert";
            
            cls = Statement.find(uow, this.stmtId);
            Assertion.Assert(cls.Billpayer == this.billpayer);
            uow.close();
        }
        [Test]
        public void findAllStatements()
        {
            UOW uow = new UOW();
            uow.Service = "findAllStatements";
            Statement[] objs = Statement.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delStatement()
        {
            UOW uow = new UOW();
            uow.Service = "delStatement";
            Statement cls = Statement.find(uow, this.stmtId);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delStatement - assert";
            cls = Statement.find(uow, this.stmtId);
            Assertion.Assert((cls.StmtId == 0));
            uow.close();
        }
    }
}
