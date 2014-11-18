using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using DPI.Services;
using DPI.Interfaces;
using DPI.Ordering;
using DPI.ClientComp;

namespace DPI.Ordering
{
	public interface IAccSum 
	{
		void Btn_Next();
	}

	public class PendingFactory 
	{
		public static IAccSum GetAccountSummary(bool pending, BasePage page)
		{
			if (pending)
				return new AcctSumPend((AccountSummary)page);

			return new AcctSumPlain((AccountSummary)page);
		}
	}
	public class AcctSumPlain : IAccSum
	{
		AccountSummary parent;
		Wipper wipper;

		public AcctSumPlain(AccountSummary parent)
		{
			this.parent = parent;
			wipper = parent.wipper;
		}
		public void Btn_Next()
		{
			CustSvc.PreSave(wipper.IMap);
			wipper.Wip["receipt"] = CustSvc.SubmitMonthlyXact(
				wipper.IMap,
				((IAcctInfo)wipper.Wip["acctinfo"]).PhNumber,	
				DPI.ClientComp.User.GetUser(parent),
				((int)wipper.Wip.WipId).ToString(), 
				(IPayInfo)wipper.Wip["PayInfo"],
				wipper.Wip.Workflow.Name);

			CustSvc.PreSave(wipper.IMap);
			wipper.Wip["AcctInfo"] = CustSvc.GetAcctInfo(wipper.IMap, ((IReceipt)wipper.Wip["receipt"]).AccNumber);
			wipper.Wip.BusObjId = ((IReceipt)wipper.Wip["receipt"]).AccNumber;
			wipper.Wip.BusObjType = "Cust Account";
		}
	}
	public class AcctSumPend : IAccSum
	{
		AccountSummary parent;
		Wipper wipper;

		public AcctSumPend(AccountSummary parent)
		{
			this.parent = parent;
			wipper = parent.wipper;
		}
		public void Btn_Next()
		{
			wipper.Wip["IsHighTouch"] = ((IPayInfo)wipper.Wip["PayInfo"]).IsConfReq = true;
			((IPayInfo)wipper.Wip["PayInfo"]).Status = PaymentStatus.PendConfirm.ToString();  
	
			IDemand dmd = (IDemand)wipper.Wip["Demand"];
			dmd.Status = "Pend";

			// add to IMap
			wipper.IMap.add((IMapObj)wipper.Wip["PayInfo"]);
			wipper.IMap.add((IMapObj)dmd);
			
			CustSvc.PreSave(wipper.IMap);

			wipper.Wip["Receipt"] = CustSvc.GetReceipt(dmd);
			wipper.Wip.BusObjId   = dmd.BillPayer;
			wipper.Wip.BusObjType = "Cust Account";		
		}
	}
}