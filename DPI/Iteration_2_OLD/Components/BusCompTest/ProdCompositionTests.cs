using System;
using NUnit.Framework;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.ComponentsTests
{
    [TestFixture]
    public class ProdCompositionTests
    {
        /*		Data		*/
		int parent = 165;

        int id;
        int prod = 165;
        int subProd = 3;
        string compType = "DarkMatter";
        int rev = 2;
        string status = "Active";
		string alternativeComp = "Alt";
        
        /*		Constructors		*/
        public ProdCompositionTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            ProdCompositionTests test = new ProdCompositionTests();
            
            // UOW Tests
			test.FindAllPackageComp();
            test.addProdComposition();
            test.findProdComposition();
            test.saveProdComposition();
            test.findAllProdCompositions();
            
            try
            {
                test.delProdComposition();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delProdComposition:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delProdComposition: " + e.Message);
            }
        }
		[Test]
		public void FindAllPackageComp()
		{
			UOW uow = new UOW();
			uow.Service = "FindAllPackageComp";
			ProdComposition[] children;
			try
			{

				children = ProdComposition.getAllPackageComp(uow, parent);
		//		for (int i = 0; i < children.Length; i++)
		//			Console.WriteLine("Parent {0}, child {1}", children[i].Prod.ToString(), children[i].SubProd.ToString()); 
				Assertion.Assert(children.Length > 0);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				uow.close();
			}
		}

        [Test]
        public void addProdComposition()
        {
            UOW uow = new UOW();
            uow.Service = "addProdComposition";
            ProdComposition cls = new ProdComposition(uow);
            
            cls.Prod = this.prod;
            cls.SubProd = this.subProd;
            cls.CompType = this.compType;
            cls.Rev = this.rev;
            cls.Status = this.status;
			cls.AlternativeComp = this.alternativeComp;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addProdComposition - assert";
            cls = ProdComposition.find(uow, this.id);
            Assertion.Assert(cls.SubProd == this.subProd);
            uow.close();
        }
        [Test]
        public void findProdComposition()
        {
            UOW uow = new UOW();
            uow.Service = "findProdComposition";
            
            ProdComposition cls = ProdComposition.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveProdComposition()
        {
            UOW uow = new UOW();
            uow.Service = "saveProdComposition";
            ProdComposition cls = ProdComposition.find(uow, this.id);

			string newStatus = "Inactive";
            
			cls.Status = newStatus;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveProdComposition - assert";
            
            cls = ProdComposition.find(uow, this.id);
            Assertion.Assert(cls.Status == newStatus);
            uow.close();
        }
        [Test]
        public void findAllProdCompositions()
        {
            UOW uow = new UOW();
            uow.Service = "findAllProdCompositions";
            ProdComposition[] objs = ProdComposition.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delProdComposition()
        {
            UOW uow = new UOW();
            uow.Service = "delProdComposition";
            ProdComposition cls = ProdComposition.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delProdComposition - assert";
            cls = ProdComposition.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
