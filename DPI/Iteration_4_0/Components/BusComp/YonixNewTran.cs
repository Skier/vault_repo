using System;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]  
	public class YonixNewTran
	{
		public static IReceipt SubmitNewOrder2(UOW uow, INewOrderDTO dto, IAcctNotes notes)
		{
			PayInfo pymt = PayInfo.Sum(PayInfo.getDmdPayInfo(uow, dto.Dmd.Id), PaymentStatus.Paid);
			ICustInfoExt cie = new CustInfoExt().FindCustInfoById(uow, dto.Dmd.ConsId);
			
			CustData cd = CustData.find(uow,  cie.CustInfo.AccNumber); 
			cd.Ilec = ILECInfo.Find(uow, dto.Dmd.Ilec).ILECCode;
			cd.Uow = uow;
			cd.Balance = pymt.LocalAmountPaid;
			
			if (dto.Dmd != null)
                cd.SourceCode = dto.Dmd.Source;
			
			if (notes != null)
				cd.AdditionalInfo = notes.Text;

			if (cie.CustInfo != null)
				cd.setCustInfo(cie.CustInfo);
				
			if (cie.MailAddr != null)
				cd.setMailAddr(cie.MailAddr);
				
			if (cie.ServAddr != null) 
				cd.setSvcAddr((IAddr)cie.ServAddr);
			
				
			int ordernum = OrderUtil.CreateOrder(uow, cd.AccNumber, dto.Dmd.ConsumerAgent); //  create order

			OrderUtil.CreateCustomerProducts2(uow, dto.Dmd, cd, ordernum, ordernum.ToString(), dto.OrderType);	//  create orderdetails & customerproduct
			Service_Transaction.CreateServiceXact(uow, cd, dto.Dmd);
			cd.Balance = pymt.LocalAmountPaid - pymt.LocalAmountDue;// summ.TotalAmountDue;	//  update custdata to adjust balance
			
			Account_PaymentLog.AddEntries(uow, cd, dto.Dmd, pymt.LocalAmountPaid);//  .LocalAmountDue);

			IILECInfo lec = ILECInfo.Find(uow, dto.Dmd.Ilec); //Alex
			OrderUtil.AssignIlec(uow, dto.Dmd.ConsumerAgent, cd.AccNumber, lec.ILECCode);

			cd.Status1 = "1" + cd.Status1.Substring(1); 
			
			// Add row to activity log here:
			Account_ActivityLog al = new Account_ActivityLog(uow, Const.YONIX_USERID, cd.AccNumber, 
										"New Order Completed", Const.YONIX_DEPARTMENT);

			return new Receipt(ordernum, pymt.VFConf, cie.CustInfo.AccNumber);
		}

//		public static IReceipt SubmitNewOrder2(UOW uow, INewOrderDTO dto)
//		{
//			PayInfo pymt = PayInfo.Sum(PayInfo.getDmdPayInfo(uow, dto.Dmd.Id), PaymentStatus.Paid);
//			ICustInfoExt cie = new CustInfoExt().FindCustInfoById(uow, dto.Dmd.ConsId);
//			
//			CustData cd = CustData.find(uow,  cie.CustInfo.AccNumber); 
//			cd.Ilec = ILECInfo.Find(uow, dto.Dmd.Ilec).ILECCode;
//			cd.Uow = uow;
//			cd.Balance = pymt.LocalAmountPaid;
//			
//			if (cie.CustInfo != null)
//				cd.setCustInfo(cie.CustInfo);
//				
//			if (cie.MailAddr != null)
//				cd.setMailAddr(cie.MailAddr);				
//				
//			if (cie.ServAddr != null) 
//				cd.setSvcAddr((IAddr)cie.ServAddr);
//			
//				
//			int ordernum = OrderUtil.CreateOrder(uow, cd.AccNumber, dto.Dmd.ConsumerAgent); //  create order
//
//			OrderUtil.CreateCustomerProducts2(uow, dto.Dmd, cd, ordernum, ordernum.ToString(), dto.OrderType);	//  create orderdetails & customerproduct
//			Service_Transaction.CreateServiceXact(uow, cd, dto.Dmd);
//			cd.Balance = pymt.LocalAmountPaid - pymt.LocalAmountDue;// summ.TotalAmountDue;	//  update custdata to adjust balance
//			
//			Account_PaymentLog.AddEntries(uow, cd, dto.Dmd, pymt.LocalAmountPaid);//  .LocalAmountDue);
//
//			IILECInfo lec = ILECInfo.Find(uow, dto.Dmd.Ilec); //Alex
//			OrderUtil.AssignIlec(uow, dto.Dmd.ConsumerAgent, cd.AccNumber, lec.ILECCode);
//
//			cd.Status1 = "1" + cd.Status1.Substring(1); 
//			// Add row to activity log here:
//
//			Account_ActivityLog al = new Account_ActivityLog(uow);
//			al.AccNumber = cd.AccNumber;
//			al.Date = DateTime.Now;
//			al.Department = Const.YONIX_DEPARTMENT; 
//			al.Activity = "New Order Completed";
//			al.UserId = Const.YONIX_USERID;
//			return new Receipt(ordernum, pymt.VFConf, cie.CustInfo.AccNumber);
//		}

		public static string MakeAni(IDemand dmd)
		{
			if (dmd.DmdType.ToLower() == DemandType.New.ToString().ToLower()) 
				return "New Order";

			if (dmd.DmdType.ToLower() == DemandType.LocalConv.ToString().ToLower()) 
				return "Local Conv";

			if (dmd.DmdType.ToLower()== DemandType.NewPymt.ToString().ToLower())
				return "New Paymen";

			if (dmd.DmdType.ToLower() == DemandType.Monthly.ToString().ToLower())
				return "Monthly Pa";
			
			return null;
			
		}
		public static IReceipt WriteTransaction(UOW uow, IDemand dmd, int ilec, string storeCode, IUser user, string transnum, 
			IPayInfo ppi, string commPort, IAcctNotes notes)
		{
			string	ani = MakeAni(dmd);

			IPayInfoLocal pi = (IPayInfoLocal)ppi;
			IVerifoneResult vResult 
				= VerifoneWrapper.SubmitNewXact (uow,  storeCode, dmd.ConsumerAgent,  transnum, pi.LocalAmountPaid, pi.LdAmount, ani);

			Notes(uow, notes, vResult, user);
	
			pi.Status =  PaymentStatus.Paid.ToString();//"Closed";
			pi.VFConf = vResult.ConfNum.ToString();
			dmd.Status = "Pend CustInfo";
			
			IReceipt receipt = new Receipt(vResult.ConfNum.ToString(), vResult.AccNumber);
			
			return receipt;
		}
		static void RentwayPost(UOW uow, IUser user,  IPayInfo pi)
		{
			PostDPIPaymentRequest xml = new PostDPIPaymentRequest(BuildArgs(uow, user, pi));
			// insert into WebSvcQueue
		}
		static IRWPostArgs BuildArgs(UOW uow, IUser user,  IPayInfo pi)
		{
			return null;
		}
		static void Notes(UOW uow, IAcctNotes notes, IVerifoneResult verifoneResult, IUser user)
		{
			if (notes == null)
				return;
			if (notes.Text.Trim().Length == 0)
				return;

			Account_Notes an = new Account_Notes(uow);
			an.AccNumber = verifoneResult.AccNumber;
			an.Date = notes.Date;
			an.Department = Const.YONIX_DEPARTMENT;
			an.UserId = user.ClerkId; 
			an.Note = notes.Text;
		}
		public static IReceipt WritePendTrans(UOW uow, IDemand dmd, int ilec, string storeCode, string clerkId, 
			string transnum, IPayInfo ppi, string commPort, IAcctNotes notes)
		{
			if (!(ppi is IPayInfoLocal))
				throw new ArgumentException("Local PayInfo is expected. Found: " + ppi.PayClass);

			IPayInfoLocal pi = (IPayInfoLocal)ppi;
			string	ani = MakeAni(dmd);

			IVerifoneResult verifoneResult = VerifoneWrapper.SubmitPendXact (uow,  
				storeCode, 
				clerkId,
				transnum, 
				pi.LocalAmountPaid, 
				pi.LdAmount, 
				ani,
				dmd.BillPayer);
			if (notes != null && notes.Text.Length > 0) 
			{
				Account_Notes an = new Account_Notes(uow);
				an.AccNumber     = verifoneResult.AccNumber;
				an.Date          = notes.Date;
				an.Department    = Const.YONIX_DEPARTMENT;
				an.UserId        = clerkId; 
				an.Note          = notes.Text;
			}
		
			pi.Status =  PaymentStatus.Paid.ToString();
			pi.VFConf = verifoneResult.ConfNum.ToString();
			dmd.Status = "Pend CustInfo";
			return new Receipt(verifoneResult.ConfNum.ToString(), verifoneResult.AccNumber);
		}
	}
}