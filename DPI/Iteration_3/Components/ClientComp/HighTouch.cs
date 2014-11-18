using System;
using System.Text;
using System.Collections;
using DPI.Interfaces;
using DPI.Services;

namespace DPI.ClientComp
{
	public class HighTouch
	{
		public static bool IsHighTouch(WIP wip, IUser user)
		{
			if ((bool)wip["IsHighTouchDisallowed"])
				return false;

			return StoreSvc.IsRac_WF(user);
		}	

		public static void PaidAmt(IPayInfo pi, WIP wip, IUser user)
		{
			if (IsHighTouch(wip, user))
				 pi.AmountTendered = pi.TotalAmountDue;
		}
	}
}

