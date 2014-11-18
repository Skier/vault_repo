using System;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	/// <summary>
	/// Summary description for CancelPending.
	/// </summary>
	[Serializable]
	public class CancelPending
	{
		public CancelPending()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static void PaymentInfo(UOW uow, int payInfoId)
		{
			bool canceldmd = true;
			PayInfo pi = PayInfo.find(uow, payInfoId);
			pi.Status = PaymentStatus.Cancelled.ToString();

			PayInfo[] pis = PayInfo.getDmdPayInfo(uow, pi.DmdId);	
			for (int i = 0; i < pis.Length; i++)
			{
				if (pi.Status.Trim().ToLower() != PaymentStatus.Cancelled.ToString().ToLower())
					canceldmd = false;
			}
			
			if (canceldmd)
			{
				IDemand dmd = Demand.find(uow, pi.DmdId);
				dmd.Status = DemandStatus.Cancelled.ToString();
			}
			
			
		}
	}
}
