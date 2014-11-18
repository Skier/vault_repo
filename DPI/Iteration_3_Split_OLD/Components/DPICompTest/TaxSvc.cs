using System;
using System.Data.SqlClient;
using DPI.Interfaces;

namespace DPI.Components
{
	public class TaxSvc : ITaxSvc
	{
		internal TaxSvc() {}
		public IDelTax[] getTaxes(IMap im) 
		{
			UOW uow = new UOW(im);
			uow.Service = "getTaxes";

			PostTaxWf step = new PostTaxWf(uow);

			try
			{
				return step.findDlvTax();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return new IDelTax[0];
			}
			finally
			{
				uow.close();
			}
		}
		public bool postCharge(IChargeDto charge)
		{
			return postCharge(charge, new IdentityMap());
		}
		public bool postCharge(IChargeDto charge, IMap im)
		{	
			UOW uow = new UOW(im);
			uow.Service = "postCharge";
			PostTaxWf step = new PostTaxWf(uow);		
			
			try 
			{
				step.postDlvTax(charge);
				uow.commit();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
			finally
			{
				uow.close();
			}
		}
		public bool postCharge(IChargeDto[] charges, IMap im)
		{
			UOW uow = new UOW(im);
			PostTaxWf step = new PostTaxWf(uow);
			uow.Service = "postCharge([])";

			try
			{
				step.postDlvTax(charges);
				uow.commit();

				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
			finally
			{
				uow.close();
			}
		}
		public bool postChargePriority(IChargeDto[] charges)
		{
			IdentityMap im = new IdentityMap();

			UOW uow = new UOW(im);
			PostTaxWf step = new PostTaxWf(uow);
			uow.Service = "postChargePriority([])";

			try
			{
				for (int i = 0; i < charges.Length; i++)
					step.postDlvTaxPriority(charges[i], 10 - i);
				
				uow.commit();

				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
			finally
			{
				uow.close();
			}
		}
		public IDelTax findDtax(IMap im, int id)
		{
			UOW uow = new UOW(im);
			uow.Service = "findDtax";
			
			PostTaxWf step = new PostTaxWf(uow);
			
			try
			{
				return step.findDlvTax(id);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return null;
			}
			finally
			{
				uow.close();
			}
		}
		public bool updateDtax(IMap im, int id)
		{
			UOW uow = new UOW(im);
			PostTaxWf step = new PostTaxWf(uow);
			uow.Service = "updateDtax";
			
			try
			{
				step.updateDlvTax(id);
				uow.commit();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
			finally
			{
				uow.close();
			}
		}
		public bool updateDtax(IMap im, int[] ids)
		{
			UOW uow = new UOW(im);
			uow.Service = "updateDtax([])";
			PostTaxWf step = new PostTaxWf(uow);
			try
			{
				step.updateDlvTax(ids);
				uow.commit();
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
			finally
			{
				uow.close();
			}
		}
	}
}