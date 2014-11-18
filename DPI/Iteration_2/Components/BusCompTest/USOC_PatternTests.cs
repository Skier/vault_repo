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
    public class USOC_PatternTests
    {
        /*		Data		*/
        int id;
        string uSOC_Pattern = "uSOC_Pattern";
        int step = 3;
        int call_Feature = 4;
        string cF_State = "cF_State";
        
        /*		Constructors		*/
        public USOC_PatternTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            USOC_PatternTests test = new USOC_PatternTests();
            
            // UOW Tests
            test.addUSOC_Pattern();
            test.findUSOC_Pattern();
            test.saveUSOC_Pattern();
            test.findAllUSOC_Patterns();
            
            try
            {
                test.delUSOC_Pattern();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delUSOC_Pattern:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delUSOC_Pattern: " + e.Message);
            }
            
        }
        [Test]
        public void addUSOC_Pattern()
        {
            UOW uow = new UOW();
            uow.Service = "addUSOC_Pattern";
            USOC_Rule cls = new USOC_Rule(uow);
            
            cls.USOC_Pattern = this.uSOC_Pattern;
            cls.Step = this.step;
            cls.Call_Feature = this.call_Feature;
            cls.CF_State = this.cF_State;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addUSOC_Pattern - assert";
            cls = USOC_Rule.find(uow, this.id);
            Assertion.Assert(cls.Step == this.step);
            uow.close();
        }
        [Test]
        public void findUSOC_Pattern()
        {
            UOW uow = new UOW();
            uow.Service = "findUSOC_Pattern";
            
            USOC_Rule cls = USOC_Rule.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveUSOC_Pattern()
        {
            UOW uow = new UOW();
            uow.Service = "saveUSOC_Pattern";
            USOC_Rule cls = USOC_Rule.find(uow, this.id);
            
            cls.USOC_Pattern = this.uSOC_Pattern;
            cls.Step = this.step;
            cls.Call_Feature = this.call_Feature;
            cls.CF_State = this.cF_State;
            cls.USOC_Pattern += " saved";
            this.uSOC_Pattern = cls.USOC_Pattern;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveUSOC_Pattern - assert";
            
            cls = USOC_Rule.find(uow, this.id);
            Assertion.Assert(cls.USOC_Pattern == this.uSOC_Pattern);
            uow.close();
        }
        [Test]
        public void findAllUSOC_Patterns()
        {
            UOW uow = new UOW();
            uow.Service = "findAllUSOC_Patterns";
            USOC_Rule[] objs = USOC_Rule.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delUSOC_Pattern()
        {
            UOW uow = new UOW();
            uow.Service = "delUSOC_Pattern";
            USOC_Rule cls = USOC_Rule.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delUSOC_Pattern - assert";
            cls = USOC_Rule.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
