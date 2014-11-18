using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Interfaces;

namespace DPI.Components
{
	[TestFixture]
	public class TaxSvcTest
	{
		/*		Data		*/
		IMap im;
		const string taxSvc = "TaxSvc"; 
		
		public TaxSvcTest()
		{
			im = new IdentityMap();
			PostTaxWip ptWip =  new PostTaxWip();
			im.add(ptWip);
		}
		public static void Main()
		{
			TaxSvcTest t = new TaxSvcTest();

			t.getTaxes();
			t.postDlvTax();
			t.updateTaxes();
			t.postDlvTaxes();

			Console.ReadLine();
		}
		[Test]
		public void getTaxes()
		{
			IDelTax[] taxes = new TaxSvc().getTaxes(im);
			Assertion.Assert(taxes.Length > 0);	

			PostTaxWip myWip = getWip(im);
			myWip.Taxes = taxes;
		}
		[Test]
		public void postDlvTax()
		{
			IChargeDto charge = TaxFactory.getChargeDto();
			charge.Amt = 25m;
			charge.DlvId = 1; 
			
			Assertion.Assert(new TaxSvc().postCharge(charge));
		}
		[Test]
		public void updateTaxes()
		{
			Assertion.Assert(new TaxSvc().updateDtax(im, getWip(im).Taxes[0].Id));
		}
		[Test]
		public void postDlvTaxes()
		{
			IChargeDto[] charges = makeCharges();
			Assertion.Assert(new TaxSvc().postCharge(charges, im));
		}
		public IChargeDto[] makeCharges()
		{
			IChargeDto[] charges = new IChargeDto[2];
			for (int i = 0; i < charges.Length; i++)
			{
				charges[i] = TaxFactory.getChargeDto();
				charges[i].Amt = 10m * (i + 1);
				charges[i].DlvId = 1;
			}
			return charges;
		}
		public void displayTaxes(IDelTax[] taxes, string header)
		{
			Console.WriteLine("");
			Console.WriteLine("   " + header);
		
			if (taxes == null)
			{
				Console.WriteLine("No taxes to display");
				return;
			}

			if (taxes.Length == 0)
			{
				Console.WriteLine("No taxes to display");
				return;
			}

			Console.WriteLine("Id   Tax   Amount"); 
			for (int i = 0; i < taxes.Length; i++)
				Console.WriteLine("{0}    {2}    {1}   ", taxes[i].Id, taxes[i].TaxAmt, taxes[i].TaxId);
		}
		static PostTaxWip getWip(IMap im) {	return (PostTaxWip)im.find(PostTaxWip.WipKey); }
		
	    [Serializable]
		class PostTaxWip : WIP
		{
			IDelTax[] taxes;
			public IDelTax[] Taxes
			{
				get { return taxes; }
				set { taxes = value; }
			}
			public override IDomKey IKey   { get { return new Key(Const.testWip, Const.wip); }}
			public static   IDomKey WipKey { get { return new PostTaxWip().IKey; }}
		}
	}
}