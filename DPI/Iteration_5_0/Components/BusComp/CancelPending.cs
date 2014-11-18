using System;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Components
{

	[Serializable]
	public class PendingCancelation
	{		
		public static void PaymentInfo(UOW uow, int payInfoId)
		{
			IPayInfo pi = PayInfo.find(uow, payInfoId);
			if (pi.Status.Trim().ToLower() != PaymentStatus.PendConfirm.ToString().ToLower())
				throw new ArgumentException("Payment needs to be in the Pending Confirmation status. PaymentInfo id: " +
					payInfoId.ToString());

			pi.Status = PaymentStatus.Cancelled.ToString();

			IPayInfo[] pis = PayInfo.getDmdPayInfo(uow, pi.DmdId);	
			for (int i = 0; i < pis.Length; i++)
				if (pis[i].Status.Trim().ToLower() != PaymentStatus.Cancelled.ToString().ToLower())
					return;

			IDemand dmd = Demand.find(uow, pi.DmdId);
			dmd.Status = DemandStatus.Cancelled.ToString();
		}
	}
}