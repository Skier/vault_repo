using System;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]
	public class DebitCardTran
	{
		public static void Enroll(UOW uow, IPayInfo payInfo, ICardApp app, string conf)
		{
			FinancialProdTrans tran = new FinancialProdTrans(uow);
			tran.TranType = FinProdTranType.DC_Enroll.ToString();
			
			if (app.PrevCard != null)
				if (app.PrevCard.Trim().Length > 1)
					tran.TranType = FinProdTranType.DC_CompEnroll.ToString();
			
			SetTranAttrs(uow, tran, payInfo, app, conf);
		}
		public static void Refill(UOW uow, IPayInfo payInfo, ICardApp app, string conf)
		{
			FinancialProdTrans tran = new FinancialProdTrans(uow);
			tran.TranType = FinProdTranType.DC_Reload.ToString();
			SetTranAttrs(uow, tran, payInfo, app, conf);
		}
		static void SetTranAttrs(UOW uow, FinancialProdTrans tran, IPayInfo payInfo, ICardApp app, string conf)
		{
			IDemand dmd = payInfo.ParDemand;
			IOrderSum sumry = dmd.OrderSummary(uow);
			int prod = dmd.GetDmdItems()[0].Prod;
			int mon = 1;

			tran.Product      = prod;
			tran.ParDemand    = dmd;
			tran.Vendor       = ProdInfoCol.GetProd(prod).Vendor;
			tran.Storecode    = dmd.StoreCode;
			tran.ClerkId      = dmd.ConsumerAgent;
	
			if (dmd.Consumer != null)
				tran.Customer = dmd.Consumer;
			
			tran.TranDate     = DateTime.Now;
			tran.TranAmt      = sumry.GetTotalAmtDue(mon);
			tran.ProdAmt	  = sumry.GetProdAmt(mon);
			tran.FeeAmt       = sumry.GetFeeAmt(mon);
			tran.TaxAmt       = sumry.GetTaxAmt(mon);
			tran.Confirmation = conf;
			tran.Status       = Const.APPROVED;
		}
	}
}