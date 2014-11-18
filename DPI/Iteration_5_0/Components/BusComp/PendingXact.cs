using System;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]  
	public class PendingXact
	{
		public static IReceipt SubmitMonthlyPayment(UOW uow, IDemand dmd, IPayInfo pi)
		{
			pi.Status = PaymentStatus.Paid.ToString();
			CustData cd = CustData.find(uow, dmd.BillPayer);
			return SubmitMonthly(uow, dmd.StoreCode, cd.PhNumber, pi);
		}
		public static IReceipt SubmitMonthly(UOW uow, string storeCode, string phone, IPayInfo pi)
		{
			if (!(pi is IPayInfoLocal))
				throw new ArgumentException("PayInfo type PayInfoLocal is required");

			IPayInfoLocal pil = (IPayInfoLocal)pi;
			IDemand demand = pi.ParDemand;

			IVerifoneResult verifoneResult = VerifoneWrapper.SubmitMonthlyXact(
				uow,
				storeCode,
				pil.ParDemand.ConsumerAgent,  //	Const.YONIX_USERID,  // should be ClerkId from Demand (Demand should use either Sales Id or Clerk Id of User Account); 
				pil.Id.ToString(),
				phone, 
				pil.LocalAmountPaid,
				pil.LdAmount, 
				Const.VF_MONTHLY_PAYMENT);
			
			return new Receipt(verifoneResult.ConfNum.ToString(), verifoneResult.AccNumber);
		}
		public static IReceipt	SubmitPayment(UOW uow, int payInfoId)
		{
			PayInfo pi = PayInfo.find(uow, payInfoId);
			pi.Status = PaymentStatus.Paid.ToString();
			IDemand dmd = Demand.find(uow, pi.DmdId);
			IAcctNotes[] notes = Notes.GetDmdNotes(uow, pi.DmdId);
			
			IAcctNotes note = null;
			if (notes != null)
				if (notes.Length > 0)
					note = notes[0];

			IReceipt receipt =  YonixNewTran.WritePendTrans(uow, dmd,
				dmd.Ilec,
				dmd.StoreCode.ToString(), 
				dmd.ConsumerAgent.ToString(), 
				payInfoId.ToString(), 
				pi, 
				pi.Id.ToString(), 
				note);

			dmd.BillPayer = receipt.AccNumber;
			return receipt;
		}
		public static bool CheckPendPayInfos(UOW uow,  int payInfoId)
		{
			PayInfo[] pis = PayInfo.getDmdPayInfo(uow, PayInfo.find(uow, payInfoId).DmdId);
				
			bool ready = true;
		
			for (int i = 0; i < pis.Length; i++)
				if (pis[i].Status.Trim().ToLower() == PaymentStatus.PendConfirm.ToString().Trim().ToLower())
					ready = false;

			return ready;
		}
		public static IReceipt ConfirmPendPayment(UOW uow,  string trConf, int payInfoId)
		{
			PayInfo pi = PayInfo.find(uow, payInfoId);
			if (pi.Status.Trim().ToLower() != PaymentStatus.PendConfirm.ToString().ToLower())
				throw new ArgumentException("Payment is not in Pending status. PaymentInfo id: " + payInfoId.ToString());
	
			IDemand dmd = Demand.find(uow, pi.DmdId);
			dmd.Status = DemandStatus.Submited.ToString();

			IReceipt rcp = null;

			if (dmd.DmdType.Trim().ToLower() == DemandType.New.ToString().ToLower())
				rcp = PendingXact.ConfirmPendingNewOrder(uow, dmd, payInfoId);

			if (dmd.DmdType.Trim().ToLower() == DemandType.LocalConv.ToString().ToLower())
				rcp = PendingXact.ConfirmPendingNewOrder(uow, dmd, payInfoId);

			if (dmd.DmdType.Trim().ToLower() == DemandType.Monthly.ToString().ToLower())
				rcp =  PendingXact.SubmitMonthlyPayment(uow, dmd, pi);
			
			if (rcp == null)
				throw new ArgumentException("Receipt cannot be null. Demand id = " +dmd.Id.ToString());


			pi.VFConf = rcp.ConfNum; // YONIX conformation
			pi.ConfNumber = trConf;  // HighTouch conformation

			return rcp;
		}
		static IReceipt ConfirmPendingNewOrder(UOW uow, IDemand dmd, int payInfoId)
		{		
			if (CheckPendPayInfos(uow, payInfoId))
				return PendingXact.SubmitOrder(uow, payInfoId);

			IReceipt xactRct = PendingXact.SubmitPayment(uow, payInfoId);

			ICustInfoExt custInfo = new CustInfoExt().FindCustInfoById(uow, dmd.ConsId);
			custInfo.CustInfo.AccNumber = dmd.BillPayer;
			return xactRct;
		}
		public static IReceipt SubmitOrder(UOW uow, int payInfoId)
		{
			IPayInfo ppi = PayInfo.find(uow, payInfoId);
			if (!(ppi is IPayInfoLocal))
				throw new ApplicationException("Local Payinfo expected. Found: " + ppi.PayClass);
 
			IPayInfoLocal pi = (IPayInfoLocal)ppi;
			IDemand dmd = Demand.find(uow, pi.DmdId);				
			
			ICustInfoExt custInfo = new CustInfoExt().FindCustInfoById(uow, dmd.ConsId);

			PayInfo[] pymt = PayInfo.getDmdPayInfo(uow, pi.DmdId);			
			PayInfo payInfo = PayInfo.Sum(pymt, PaymentStatus.Paid);
			
			INewOrderDTO dto = new NewOrderDTO();
			dto.Dmd = dmd;
			dto.OrderType = OrderType.New;
			return	YonixNewTran.SubmitNewOrder2(uow, dto, null);
		}
	}
}