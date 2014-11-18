using System;
using System.Data;
using System.Text;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;

using DPI.Components;
using DPI.Interfaces;
using DPI.Ordering;
using DPI.Services;

namespace DPI.Reports
{
	public class InfActReceipt : dsInfinity_Receipt
	{
		public InfActReceipt(IWIP wip, IUser user, ReportClass rc, IReceipt2 rct) : base()
		{
			Infinity_ReceiptRow row = (Infinity_ReceiptRow)Infinity_Receipt.NewRow();

			SetupTitle(wip, rc, rct);
			SetupHeading(wip, row, rct);
			SetupVars(wip, row, rct);
			SetupBody(wip, row, rct);
			SetupFooter(wip, row, rct);

			Tables["Infinity_Receipt"].Rows.Add(row);  //?
		}
		void SetupVars(IWIP wip, dsInfinity_Receipt.Infinity_ReceiptRow row, IReceipt2 rct)
		{
			row.Confirmation_Number = ((ICellPhoneReceipt)wip["receipt"]).ConfNum;//((IPayInfo)wip["payinfo"]).Id.ToString();	
			row.Pin = ((ICellPhoneReceipt)wip["receipt"]).Pin;
			row.ControlNumber = ((ICellPhoneReceipt)wip["receipt"]).ControlNumber;				
			row.PhoneNumber = DPI.ClientComp.FormatPhone.Format(((ICellPhoneReceipt)wip["receipt"]).PhoneNumber);
			row.StoreCode = wip.StoreCode;
			row.PayDate = ((IPayInfo)wip["payinfo"]).PayDate;
			row.PaymentType = PaySvc.GetPaymentType(((IPayInfo)wip["payinfo"]).PaymentType);
												
			row.MDN = ((ICellPhoneReceipt)wip["receipt"]).Mdn;
			row.MSID = ((ICellPhoneReceipt)wip["receipt"]).Msid;
			row.MSL = ((ICellPhoneReceipt)wip["receipt"]).Msl;
			row.Product_ID = ((IPinProduct)wip["selectedpinproduct"]).Product_Id;
			row.Product_Name = ((IPinProduct)wip["selectedpinproduct"]).Product_Name;
			row.Product_Price = ((IPinProduct)wip["selectedpinproduct"]).Price;				
			row.SubTotal = ((IPinProduct)wip["selectedpinproduct"]).Price;
			row.TotalAmountDue = ((IPayInfo)wip["payinfo"]).TotalAmountDue;
			row.AmountTendered = ((IPayInfo)wip["payinfo"]).AmountTendered;
			row.ChangeDue = ((IPayInfo)wip["payinfo"]).ChangeAmount;
		}
		void SetupBody(IWIP wip, dsInfinity_Receipt.Infinity_ReceiptRow row, IReceipt2 rct)
		{
			IReceiptItem[] first = rct.GetFirst(ReceiptItemType.Body);	// First group
			row.Receipt_Text1 = rct.Conv(first);

			if (first.Length == 0)
				return;

			row.Receipt_Text2 = rct.Conv(rct.GetNext(first[0]));  // need a column for the second
		}
		void SetupHeading(IWIP wip,  dsInfinity_Receipt.Infinity_ReceiptRow row, IReceipt2 rct)
		{
			IReceiptItem[] items = rct.FilterItems(ReceiptItemType.Header);
			row.Heading1 = ReceiptSvc.ReplaceVars(wip, items[0].Text);
			row.Heading2 =  ReceiptSvc.ReplaceVars(wip, items[1].Text);
		}
		void SetupTitle(IWIP wip, ReportClass rc, IReceipt2 rct)
		{
			IReceiptItem[] items = rct.FilterItems(ReceiptItemType.Title);
			rc.SummaryInfo.ReportTitle = items[0].Text;
		}
		void SetupFooter(IWIP wip,  dsInfinity_Receipt.Infinity_ReceiptRow row, IReceipt2 rct)
		{
			IReceiptItem[] items = rct.FilterItems(ReceiptItemType.Footer);

			row.Message1 = ReceiptSvc.ReplaceVars(wip, items[0].Text);
			row.Message2 = ReceiptSvc.ReplaceVars(wip, items[1].Text);
			row.Message3 = ReceiptSvc.ReplaceVars(wip, items[2].Text);
			row.Message4 = ReceiptSvc.ReplaceVars(wip, items[3].Text);
			row.Message5 = ReceiptSvc.ReplaceVars(wip, items[4].Text);
		}
	}
}			