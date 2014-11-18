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
	public class DlvTaxTests
	{
		/*		Data		*/
		int id;
		int dlv = 1;
		string taxId = "S";
		decimal taxAmount = 3.5M;
        
		/*		Constructors		*/
		public DlvTaxTests()
		{
			// try { cleanup(); } 	catch {}
			// Console.WriteLine("Cleanup completed");
		}
        
		/*		Methods		*/
		public static void Main()
		{
			DlvTaxTests test = new DlvTaxTests();
            
			// UOW Tests
			test.addDlvTax();
			test.findDlvTax();
			test.saveDlvTax();
			test.findAllDlvTaxs();
            
			try
			{
				test.delDlvTax();
			}
			catch(ArgumentException ae)
			{
				Console.WriteLine("Expected exception: delDlvTax:" + ae.Message);
			}
			catch(Exception e)
			{
				Console.WriteLine("Error: delDlvTax: " + e.Message);
			}
            
		}
		[Test]
		public void addDlvTax()
		{
			UOW uow = new UOW();
			uow.Service = "addDlvTax";
			DlvTax cls = new DlvTax(uow);
            
		//	cls.Dlv = this.dlv;
			cls.TaxId = this.taxId;
			cls.TaxAmount = this.taxAmount;
        
			uow.commit();
			this.id = cls.Id;
            
			uow = new UOW();
			uow.Service = "addDlvTax - assert";
			cls = DlvTax.find(uow, this.id);
			Assertion.Assert(cls.TaxId == this.taxId);
			uow.close();
		}
		[Test]
		public void findDlvTax()
		{
			UOW uow = new UOW();
			uow.Service = "findDlvTax";
            
			DlvTax cls = DlvTax.find(uow, this.id);
			Assertion.Assert(cls.Id == this.id);
			uow.close();
		}
		[Test]
		public void saveDlvTax()
		{
			UOW uow = new UOW();
			uow.Service = "saveDlvTax";
			DlvTax cls = DlvTax.find(uow, this.id);
            
		//	cls.Dlv = this.dlv;
			cls.TaxId = this.taxId;
			this.taxAmount += 2m;
			cls.TaxAmount = this.taxAmount;
         
		//	this.dlv = cls.Dlv; 
                
			uow.commit();
            
			uow = new UOW();
			uow.Service = "saveDlvTax - assert";
            
			cls = DlvTax.find(uow, this.id);
	//		Assertion.Assert(cls.Dlv == this.dlv);
			uow.close();
		}
		[Test]
		public void findAllDlvTaxs()
		{
			UOW uow = new UOW();
			uow.Service = "findAllDlvTaxs";
			DlvTax[] objs = DlvTax.getAll(uow);
			Assertion.Assert(objs.Length > 0);
			uow.close();
		}
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void delDlvTax()
		{
			UOW uow = new UOW();
			uow.Service = "delDlvTax";
			DlvTax cls = DlvTax.find(uow, this.id);
			cls.delete();
            
			uow.commit();
            
			uow = new UOW();
			uow.Service = "delDlvTax - assert";
			cls = DlvTax.find(uow, this.id);
			Assertion.Assert((cls.Id == 0));
			uow.close();
		}
	}
}
