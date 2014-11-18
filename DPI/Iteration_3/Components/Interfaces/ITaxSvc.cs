using System;
 
namespace DPI.Interfaces
{	
	public interface ITaxSvc
	{
		IDelTax[] getTaxes   (IMap im);
		bool      postCharge (IChargeDto charge);
		bool	  postCharge (IChargeDto charge, IMap im);
		bool	  postCharge (IChargeDto[] charges, IMap im);
		IDelTax   findDtax   (IMap im, int id);
		bool      updateDtax (IMap im, int id);
		bool	  updateDtax (IMap im, int[] ids);
	}
}