//using System;
//using System.Collections;
//using DPI.Components;
//using DPI.Interfaces;
//
//namespace DPI.Services
//{
//	public class SubmitNewOrderSvc 
//	{
//		public static IReceipt SubmitNewOrder2(
//			IMap imap,
//			OrderType otype,
//			IAddr mailAddr, 
//			IAddr svcAddr, 
//			ICustInfo cust,
//			IDemand dmd,
//			string zipcode,
//			string ilecCode,
//			string transnum,
//			IPaymentInfo pymt,			
//			IReceipt receipt,
//			string clerkId
//			)
//		{
//			UOW uow = new UOW(imap);
//
//			uow.Service = "CustSvc.SubmitNewOrder()";
//
//			try
//			{
//				uow.BeginTransaction();
//			   
//				//OrderSummary summ = OrderSummary.BuildOrderSummary(uow, (ProdPrice[])prods, zipcode, ilecCode, otype);
//				CustData cd = CustData.find(uow, receipt.AccNumber);
//
//				if (cust!= null)
//					cd.setCustInfo(cust);
//				
//				if (mailAddr != null)
//					cd.setMailAddr((IAddr)mailAddr);				
//				
//				if (svcAddr != null) 
//					cd.setSvcAddr((IAddr)svcAddr);
//			
//				cd.Uow = uow;
//				cd.Balance = pymt.LocalAmountPaid;
//				
//				int ordernum = OrderUtil.CreateOrder(uow, cd.AccNumber, clerkId); //  create order
//
//				OrderUtil.CreateCustomerProducts2(uow, dmd, cd, ordernum, transnum, otype);	//  create orderdetails & customerproduct
//				OrderUtil.CreateServiceTransactions2(uow, cd, dmd);		//	create servicetransactions (taxes)
//				cd.Balance = pymt.LocalAmountPaid - pymt.LocalAmountDue;// summ.TotalAmountDue;	//  update custdata to adjust balance
//				OrderUtil.UpdateAccountLog2(uow, cd, dmd, pymt.LocalAmountDue); 
//					
//				OrderUtil.AssignIlec(uow, clerkId, cd.AccNumber, cd.Ilec);
//
//				cd.Status1 = "1" + cd.Status1.Substring(1); 
//				
//				// Add row to activity log here:
//				Account_ActivityLog al = new Account_ActivityLog(uow);
//				al.AccNumber=cd.AccNumber;
//				al.Date = DateTime.Now;
//				al.Department = Const.YONIX_DEPARTMENT; 
//				al.Activity = "New Order Completed";
//				al.UserId = clerkId;
//
//				uow.commit();
//				return new Receipt(ordernum, receipt.ConfNum, receipt.AccNumber);
//			}
//			finally
//			{	
//				uow.close();
//				imap.ClearDomainObjs();	
//			}
//		}
//	}
//}