using System;
using DPI.Components;
using DPI.Interfaces;
using NUnit.Framework;

namespace DPI.ComponentsTests
{
	/// <summary>
	/// Summary description for ProdTaxTests.
	/// </summary>
	[TestFixture]
	public class ProdTaxTests
	{
		public ProdTaxTests()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static void Main()
		{
			ProdTaxTests t = new ProdTaxTests();
			Console.ReadLine();
		}
		[Test]
		public void CheckValue()
		{	
			Console.WriteLine("ProdTaxTests::CheckValue()");

			ProdTax pt=new ProdTax("A", 0.025m, false);//, "testing 1");
			ProdTax[] pta = new ProdTax[1];
			pta[0]=pt;

			ProdTax.SetTaxedValues(pta,10.00m);

			Assertion.Assert(pt.TaxAmt == 0.25m);

		}

		[Test] 
		public void CheckSum()
		{	
//			Console.WriteLine("ProdTaxTests::CheckSum()");
//			ProdTax pt=new ProdTax("A", .025m, false); //, "testing 1");
//			ProdTax[] pta1 = new ProdTax[1];
//			pta1[0]=pt;
//			pt=new ProdTax("B", .025m, false);//, "testing 1");
//			ProdTax[] pta2 = new ProdTax[2];
//			pta2[0]=pt;
//			pt=new ProdTax("C", 2.00m, true);//, "testing 2");
//			pta2[1]=pt;
//
//
//			ProdTax[] ptaOut;
//
//			ProdTax.SetTaxedValues(pta1,10.00m);
//			ProdTax.SetTaxedValues(pta2,10.00m);
//
//			ptaOut=ProdTax.SumTaxes(pta1, pta2);
///*
//			Console.WriteLine("Dumping");
//			for (int i=0; i<ptaOut.Length; i++)
//			{
//				ptaOut[i].Dump();
//			}
//*/
//			Assertion.Assert(ptaOut[0].TaxAmt == 0.50m);
//			Assertion.Assert(ptaOut[1].TaxAmt == 2.00m);

		}
	}
}
