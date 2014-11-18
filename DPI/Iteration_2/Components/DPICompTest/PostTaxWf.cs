using System;
using DPI.Interfaces;
 
namespace DPI.Components
{
	public class PostTaxWf
	{
		UOW uow;

		/*		Constructors		*/
		public PostTaxWf(UOW uow)
		{
			if (uow == null)
				throw new ArgumentException("Unit Of Work is required");
			
			this.uow = uow;
		}
		/*		Methods		*/
		public void postDlvTaxPriority(IChargeDto charge, int pri)
		{
			try 
			{
				DlvTax dtax = new DlvTax(uow);

				dtax.Dlv = charge.DlvId;
				dtax.TaxAmount = charge.Amt * 0.0825m;
				dtax.TaxId = "K";
				dtax.Priority = 100 - pri;
				
			} 
			catch (Exception e)
			{
				Console.WriteLine (e.Message);
				throw e;
			}
		}

		public void postDlvTax(IChargeDto charge)
		{
			try 
			{
				DlvTax dtax = new DlvTax(uow);
				dtax.Dlv = charge.DlvId;
				dtax.TaxAmount= charge.Amt * 0.0825m;
				dtax.TaxId = "K";
				
			} 
			catch (Exception e)
			{
				Console.WriteLine (e.Message);
				throw e;
			}
		}
		public void postDlvTax(IChargeDto[] charges)
		{
			for (int i = 0; i < charges.Length; i++)
				postDlvTax(charges[i]);
		}
		public IDelTax[] getTaxes() 
		{
			return convTaxItem(DlvTax.getAll(uow));
		}
		public IDelTax findDlvTax(int id)
		{
			return (IDelTax)DlvTax.find(uow, id);
		}

		public IDelTax[] findDlvTax()
		{
			return (IDelTax[])DlvTax.getAll(uow);
		}
		public void updateDlvTax(int dlv)
		{
			try 
			{
				DlvTax dtax = DlvTax.find(uow, dlv);

				if (dtax == null)
					throw new ArgumentException("No dlvTax found, id = " + dlv.ToString());
			
				dtax.TaxAmount *= 1.12m;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				throw e;
			}
		}
		public void updateDlvTax(int[] ids)
		{
			for (int i = 0; i < ids.Length; i++)
				updateDlvTax(ids[i]);
		}
		IDelTax[] convTaxItem(DlvTax[] dtaxes)
		{
			TaxItem[] ti = new TaxItem[dtaxes.Length];

//			for (int i = 0; i < ti.Length; i++)
//				ti[i] = DlvTax.getTaxItem(dtaxes[i]);

			return ti;
		}
	}
}    