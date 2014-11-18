using System;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;

using DPI.Interfaces;
using DPI.Components;
using DPI.Services;
using DPI.ClientComp;

namespace DPI.Reports
{
	public class ReportSvc
	{
	#region Receipts
		#region Hi Touch Receipts
		
		public static dsHIMP_Receipt HIMP_GetReceipt(IReceipt receipt, 
													ICustInfo custInfo, 
													IAcctInfo acctInfo, 
													IPayInfo paymentInfo, 
													IUser user)
		{
			dsHIMP_Receipt ds = new dsHIMP_Receipt();
			try
			{
				dsHIMP_Receipt.HIMP_ReceiptRow dsRow = (dsHIMP_Receipt.HIMP_ReceiptRow)ds.HIMP_Receipt.NewRow();
				//Populate table rows (dsRow)
				string s = ""; 
				if (Conn.Env == Const.PROD)
					s = ": ";
				dsRow.Heading1 = EnvIdent.ReceiptHead1;
				dsRow.Heading2 = "customerservice@dpiteleconnect.com";
				dsRow.AccNumber = receipt.AccNumber;
				dsRow.PhNumber = acctInfo.PhNumFormated;
				//dsRow.PaymentInfo = receipt.ConfNum;				
				dsRow.Customer_Name = custInfo.FirstName + " " + custInfo.LastName;
				dsRow.StoreCode = user.LoginStoreCode;
				dsRow.PayDate = paymentInfo.PayDate;
				dsRow.PaymentInfo = paymentInfo.Id;

				dsRow.PaymentType = GetPaymentType(paymentInfo.PaymentType);
			
				dsRow.Customer_Status = acctInfo.Status;
				dsRow.CurrentDueDate = acctInfo.DueDate;
				dsRow.SDiscoDate = acctInfo.DiscoDate.ToShortDateString();				

				dsRow.Message1 = "Your payment has been applied to your account.";
				dsRow.Message2 = "";

				dsRow.Message3 = ""; //reserved for LD
				dsRow.Message4 = ""; //reserved for LD
								
				DoLocalPayInfo(dsRow, paymentInfo);
				
				dsRow.TotalAmountDue = paymentInfo.TotalAmountDue;
				dsRow.AmountTendered = paymentInfo.AmountTendered;
				dsRow.ChangeDue = paymentInfo.ChangeAmount;
				
				dsRow.Message5 = ""; 
				dsRow.Message6 = "dPi Customer Service Phone Number: 800-350-4009";
				dsRow.Message7 = "";
				dsRow.Message8 = "Please retain this copy for your records.";
				dsRow.Message9 = "Thank you for choosing dPi Teleconnect!";
				ds.HIMP_Receipt.Rows.Add(dsRow);								
				return ds;
			}			
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex.InnerException);
			}
		}
				
		public static dsHI_Receipt HI_GetReceipt(IReceipt receipt, 
												ICustInfo custInfo, 
												IOrderSum oSum,
			                                    IPayInfoLocal paymentInfo, 
												IUser user, 
												string message1)
		{
			dsHI_Receipt ds = new dsHI_Receipt();
			try
			{
				foreach (IProdPrice prodPrice in oSum.Products)
				{					
					if (prodPrice.SuppressOnWebReceipt)
						continue;					

					if (prodPrice.StartServMon != 1)
						continue;		
					
					if (prodPrice.UnitPrice == 0m)
						if (prodPrice.SuppressZeroPriceProd)
							continue;
					
					dsHI_Receipt.HI_ReceiptRow dsRow = (dsHI_Receipt.HI_ReceiptRow)ds.HI_Receipt.NewRow();
					
					string s = ""; 
					if (Conn.Env == Const.PROD)
						s = ": ";
					dsRow.Heading1 = EnvIdent.ReceiptHead1;					
					dsRow.Heading2 = "customerservice@dpiteleconnect.com";

					dsRow.OrdNumber =  ((IOrderSum)oSum).Demand.BillPayer; 
					dsRow.PaymentInfo = paymentInfo.Id;
					dsRow.Pin = receipt.Pin;
					dsRow.InternetPinText = GetInternetPinText(oSum.Products);
					
					dsRow.Customer_Name = "";

					if (custInfo != null)   // to make it work with New Payment Workflow  alex
						dsRow.Customer_Name = custInfo.FirstName + " " + custInfo.LastName;
					
					dsRow.StoreCode = user.LoginStoreCode;
					dsRow.PayDate = paymentInfo.PayDate;

					dsRow.PaymentType = GetPaymentType(paymentInfo.PaymentType);
					
					dsRow.ProductID = prodPrice.ProdId; 
					dsRow.Parent_ProductID = prodPrice.PackageId;
					dsRow.Product_Name = prodPrice.BillText;
					
					if (prodPrice.StartServMon == 1)
						dsRow.Product_UnitPrice = prodPrice.UnitPrice;

					if ((prodPrice.StartServMon > 1) && (prodPrice.OrdSumryStartMon2 == Const.ORD_Sumry_ZERO))
						dsRow.Product_UnitPrice = 0;

					dsRow.SubTotal = oSum.GetProdSubTotal(1);// .ProdSubTotal;
					dsRow.Tax = oSum.GetTaxAmt(1);//   TaxAmt;

					dsRow.Message1 = message1;
					
					dsRow.Message2 = "Your NEW phone service order has been submitted to dPi for processing. In 5 to 7 business days,";
					dsRow.Message3 = "call dPi at 800-350-4009 to receive your line activation date and your new phone number.";
					dsRow.Message4 = "To access your account information, you will need to provide your new account number";
					dsRow.Message5 = "located on the top left corner of the dPi order confirmation page.";
					
					dsRow.Message6 = ""; //reserved for LD
					dsRow.Message7 = ""; //reserved for LD

					DoLocalPayInfo(dsRow, paymentInfo); 

					dsRow.TotalAmountDue = paymentInfo.TotalAmountDue;
					dsRow.AmountTendered = paymentInfo.AmountTendered;
					dsRow.ChangeDue = paymentInfo.ChangeAmount;
					
					
					dsRow.Message8 = "";
					dsRow.Message9 = "Please retain this copy for your records.";
					dsRow.Message10 = "Thank you for choosing dPi Teleconnect!";					
					
					ds.HI_Receipt.Rows.Add(dsRow);									
				}
				return ds;
			}			
			catch (ArgumentException ae)
			{
				throw new ArgumentException (ae.Message);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex.InnerException);
			}
		}
		

		#endregion

		#region Regular Receipts

		public static dsMP_Receipt MP_GetReceipt(IReceipt receipt, 
												ICustInfo custInfo, 
												IAcctInfo acctInfo, 
												IPayInfoLocal paymentInfo, 
												IUser user)
		{
			dsMP_Receipt ds = new dsMP_Receipt();
			try
			{
				dsMP_Receipt.MP_ReceiptRow dsRow = (dsMP_Receipt.MP_ReceiptRow)ds.MP_Receipt.NewRow();

				dsRow.Heading1 = EnvIdent.ReceiptHead1;
				dsRow.Heading2 = "customerservice@dpiteleconnect.com";
				dsRow.AccNumber = receipt.AccNumber;
				dsRow.PhNumber = acctInfo.PhNumFormated;
				dsRow.Confirmation_Number = receipt.ConfNum;				
				dsRow.Customer_Name = custInfo.FirstName + " " + custInfo.LastName;
				dsRow.StoreCode = user.LoginStoreCode;
				dsRow.PayDate = paymentInfo.PayDate;

				dsRow.PaymentType = GetPaymentType(paymentInfo.PaymentType);				

				dsRow.Customer_Status = acctInfo.Status;
				dsRow.CurrentDueDate = acctInfo.DueDate;
				dsRow.SDiscoDate = acctInfo.DiscoDate;
				dsRow.Message1 = "To contact Customer Service Please call 1-800-350-4009 " 
								+ "or email us at customerservice@dpiteleconnect.com";
				dsRow.Message2 = "";
				dsRow.Message3 = "";				
				
				DoLocalPayInfo(dsRow, paymentInfo);
				
				dsRow.TotalAmountDue = paymentInfo.TotalAmountDue;
				dsRow.AmountTendered = paymentInfo.AmountTendered;
				dsRow.ChangeDue = paymentInfo.ChangeAmount;

				
				dsRow.Message4 = "Retain this copy for your records";
				dsRow.Message5 = "Thank you for choosing dPi Teleconnect!";
				ds.MP_Receipt.Rows.Add(dsRow);								
				return ds;
			}			
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex.InnerException);
			}
		}
	
		public static dsNO_Receipt NO_GetReceipt(IReceipt receipt, 
												ICustInfo custInfo, 
												IOrderSum oSum,
												IPayInfoLocal paymentInfo, 
												IUser user, 
												string message1)
		{
			dsNO_Receipt ds = new dsNO_Receipt();
			try
			{
				foreach (IProdPrice prodPrice in oSum.Products)
				{					
					if (prodPrice.SuppressOnWebReceipt)
						continue;
					
					if (prodPrice.StartServMon != 1)
							continue;		
					
					if (prodPrice.UnitPrice == 0m)
						if (prodPrice.SuppressZeroPriceProd)
							continue;

					dsNO_Receipt.NO_ReceiptRow dsRow = (dsNO_Receipt.NO_ReceiptRow)ds.NO_Receipt.NewRow();		

					//Populate table rows (dsRow)
					string s = ""; 
					if (Conn.Env == Const.PROD)
						s = ": ";
					dsRow.Heading1 = EnvIdent.ReceiptHead1;
					dsRow.Heading2 = "customerservice@dpiteleconnect.com";
					dsRow.AccNumber = receipt.AccNumber;
					dsRow.Confirmation_Number = receipt.ConfNum;
					dsRow.Pin = receipt.Pin;
					dsRow.InternetPinText = GetInternetPinText(oSum.Products);

					dsRow.Customer_Name = "";
					
					if (custInfo != null)   // to make it work with New Payment Workflow  alex
						dsRow.Customer_Name = custInfo.FirstName + " " + custInfo.LastName;
					dsRow.StoreCode = user.LoginStoreCode;
					dsRow.PayDate = paymentInfo.PayDate;


					dsRow.PaymentType = GetPaymentType(paymentInfo.PaymentType);
									
					dsRow.ProductID = prodPrice.ProdId; 
					dsRow.Parent_ProductID = prodPrice.PackageId;					
					dsRow.Product_Name = prodPrice.BillText;
					dsRow.Product_UnitPrice = prodPrice.UnitPrice;

					dsRow.SubTotal = oSum.GetProdSubTotal(1);//   .ProdSubTotal;
					dsRow.Tax = oSum.GetTaxAmt(1);//.TaxAmt;

					dsRow.Message1 = message1;
					dsRow.Message2 = "";
					dsRow.Message3 = "";
					
					DoLocalPayInfo(dsRow, paymentInfo);

					dsRow.TotalAmountDue = paymentInfo.TotalAmountDue;
					dsRow.AmountTendered = paymentInfo.AmountTendered;
					dsRow.ChangeDue = paymentInfo.ChangeAmount;

					dsRow.Message4 = "Retain this copy for your records";
					dsRow.Message5 = "Thank you for choosing dPi Teleconnect!";
					ds.NO_Receipt.Rows.Add(dsRow);									
				}
				return ds;
			}			
			catch (ArgumentException ae)
			{
				throw new ArgumentException (ae.Message);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex.InnerException);
			}
		}
	
		public static DataSet Pin_GetReceipt(IPinReceipt receipt, IPinProduct pinProduct, IPayInfo[] paymentInfos, IUser user)
		{
			return Pin_GetReceipt(receipt, pinProduct, paymentInfos[0], user);
		}
		public static DataSet Pin_GetReceipt(IPinReceipt receipt, IPinProduct pinProduct, IPayInfo paymentInfo, IUser user)
		{
			dsPin_Receipt ds = new dsPin_Receipt();			
			try
			{
				dsPin_Receipt.Pin_ReceiptRow dsRow = (dsPin_Receipt.Pin_ReceiptRow)ds.Pin_Receipt.NewRow();
				//Populate table rows (dsRow)
				string s = ""; 
				if (Conn.Env == Const.PROD)
					s = ": ";
				dsRow.Heading1 = EnvIdent.ReceiptHead1;
				dsRow.Heading2 = "customerservice@dpiteleconnect.com";				
				dsRow.Confirmation_Number = receipt.ConfNum;	
				dsRow.Pin = receipt.Pin;
				dsRow.StoreCode = user.LoginStoreCode;
				dsRow.PayDate = paymentInfo.PayDate;

				dsRow.PaymentType = GetPaymentType(paymentInfo.PaymentType);
												
				dsRow.Product_ID = pinProduct.Product_Id;
				dsRow.Product_Name = pinProduct.Product_Name;
				dsRow.Product_Price = pinProduct.Price;				
				dsRow.Receipt_Text = receipt.Receipt_Text;
				dsRow.SubTotal = pinProduct.Price;
				dsRow.TotalAmountDue = paymentInfo.TotalAmountDue;
				dsRow.AmountTendered = paymentInfo.AmountTendered;
				dsRow.ChangeDue = paymentInfo.ChangeAmount;
				dsRow.Message1 = "To contact Customer Service Please call 1-800-350-4009 " 
								+ "or email us at customerservice@dpiteleconnect.com";
				dsRow.Message2 = "";
				dsRow.Message3 = "";
				dsRow.Message4 = "Retain this copy for your records";
				dsRow.Message5 = "Thank you for choosing dPi Teleconnect!";
				ds.Pin_Receipt.Rows.Add(dsRow);									
				return ds;
			}			
			catch (ArgumentException ae)
			{
				throw new ArgumentException (ae.Message);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex.InnerException);
			}
		}
		

		public static DataSet Infinity_GetReceipt(ICellPhoneReceipt receipt, 
												  IPinProduct pinProduct, 
												  IPayInfo paymentInfo, 
												  IUser user)
		{
			dsInfinity_Receipt ds = new dsInfinity_Receipt();			

			try
			{
				dsInfinity_Receipt.Infinity_ReceiptRow dsRow 
					= (dsInfinity_Receipt.Infinity_ReceiptRow)ds.Infinity_Receipt.NewRow();
				
				string s = ""; 
				if (Conn.Env == Const.PROD)
					s = ": ";
				
				dsRow.Heading1 = EnvIdent.ReceiptHead1;
				dsRow.Heading2 = "customerservice@dpiteleconnect.com";				
				
				dsRow.Confirmation_Number = paymentInfo.Id.ToString();
				if ((receipt.ConfNum != null) && (receipt.ConfNum.Trim().Length != 0))
					dsRow.Confirmation_Number = receipt.ConfNum;

				dsRow.Pin = receipt.Pin;
				dsRow.ControlNumber = receipt.ControlNumber;				
				dsRow.PhoneNumber = DPI.ClientComp.FormatPhone.Format(receipt.PhoneNumber);
				dsRow.StoreCode = user.LoginStoreCode;
				dsRow.PayDate = paymentInfo.PayDate;
				dsRow.PaymentType = GetPaymentType(paymentInfo.PaymentType);
												
				dsRow.Product_ID = pinProduct.Product_Id;
				dsRow.Product_Name = pinProduct.Product_Name;
				dsRow.Product_Price = pinProduct.Price;
				//dsRow.Receipt_Text = receipt.Receipt_Text;

				dsRow.SubTotal = pinProduct.Price;
				dsRow.TotalAmountDue = paymentInfo.TotalAmountDue;
				dsRow.AmountTendered = paymentInfo.AmountTendered;
				dsRow.ChangeDue = paymentInfo.ChangeAmount;
				
				dsRow.Message1 = "To contact Customer Service Please call 1-888-416-2020 " 
					+ "Infinity Mobile Customer Service";
				
				dsRow.Message2 = "";
				dsRow.Message3 = "";
				dsRow.Message4 = "Retain this copy for your records";
				dsRow.Message5 = "Thank you for choosing dPi Teleconnect!";
				
				ds.Infinity_Receipt.Rows.Add(dsRow);									
				
				return ds;
			}			
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex.InnerException);
			}
		}
		
		#endregion

		#region Debit Card Receipts
		public static dsDCNew_Receipt DCNew_Receipt
			(IDebitCardReceipt receipt, IProdPrice prod, IPayInfo paymentInfo, ICardApp app, IUser user)
		{
			dsDCNew_Receipt ds = new dsDCNew_Receipt();
			IDemand demand = paymentInfo.ParDemand;

			try
			{
				dsDCNew_Receipt.DCNew_ReceiptRow dsRow = (dsDCNew_Receipt.DCNew_ReceiptRow)ds.DCNew_Receipt.NewRow();
				//Populate table rows (dsRow)
				string s = ""; 
				if (Conn.Env == Const.PROD)
					s = ": ";
				dsRow.Heading1 = EnvIdent.ReceiptHead1;
				dsRow.Heading2 = "customerservice@dpiteleconnect.com";
							
				dsRow.CardProvider = ProdInfoCol.GetProd(prod.ProdId).ProdName;
				

				dsRow.TransType = GetPaymentType(paymentInfo.PaymentType);

				IOrderSum sum   = demand.OrderSummary(null);
				dsRow.EnrollFee = sum.GetFeeAmt();
				dsRow.LoadValue = sum.GetProdAmt();
				dsRow.AmountDue = sum.GetTotalAmtDue();
				dsRow.SaleDate = paymentInfo.PayDate;
				dsRow.ChangeAmount	= paymentInfo.ChangeAmount;
				dsRow.AmountTendered = paymentInfo.AmountTendered;
				
				dsRow.CardNumber = app.CardNum;

				dsRow.ConfNumber = int.Parse(receipt.ConfNum);  
				dsRow.Merchant = demand.StoreCode;

				dsRow.Cardholder = "";
				dsRow.LoadFee = 0;
				dsRow.Message1 =	"Thank you for purchasing the Purpose Prepaid MasterCard \n\n"+
									"You must call 1-888-618-0023 before you can start using your card to select a 4-digit Personal \n" +
									"Identification Number (PIN).\n\n" +
									"To Reload your Purpose Prepaid MasterCard visit any dPi Teleconnect authorized location \n" +
									"nearest you or call Customer Service 1-800-350-4009.\n\n"+
									"Retain this copy for your records.\n"+
									"Thank you for choosing dPi Teleconnect!";

				ds.DCNew_Receipt.Rows.Add(dsRow);								
				return ds;
			}			
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex.InnerException);
			}
		}


		public static dsDCNew_Receipt DCNew_Receipt_Deny(IDebitCardReceipt receipt, IProdPrice prod, IPayInfo paymentInfo,
			IUser user)
		{
			dsDCNew_Receipt ds = new dsDCNew_Receipt();
			try
			{
				dsDCNew_Receipt.DCNew_ReceiptRow dsRow = (dsDCNew_Receipt.DCNew_ReceiptRow)ds.DCNew_Receipt.NewRow();
				string s = ""; 
				if (Conn.Env == Const.PROD)
					s = ": ";
				dsRow.Heading1 = EnvIdent.ReceiptHead1;
				dsRow.Heading2 = "customerservice@dpiteleconnect.com";
				dsRow.CardProvider = ProdInfoCol.GetProd(prod.ProdId).ProdName;;
				
				dsRow.TransType = GetPaymentType(paymentInfo.PaymentType);

				dsRow.AmountDue = 0;
				dsRow.ChangeAmount	= 0;
				dsRow.AmountTendered = 0;
				dsRow.EnrollFee = 0;
				dsRow.CardNumber = "####-####-####-####";
				dsRow.LoadValue = 0;
				dsRow.ConfNumber = int.Parse(receipt.ConfNum);
				dsRow.Merchant = user.LoginStoreCode;
				dsRow.SaleDate = paymentInfo.PayDate;
				dsRow.LoadFee = 0;
				dsRow.Message1 =	"We have been unable to process your request. \n\n"+
									"Please contact Purpose Solutions Customer "+
									"Service at 1-800-962-4294 for further details.";

				ds.DCNew_Receipt.Rows.Add(dsRow);								
				return ds;
			}			
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex.InnerException);
			}
		}


		public static dsDCNew_Receipt DCReload_Receipt
			(IDebitCardReceipt receipt, ICardApp app, IProdPrice prod, ICustInfo2 ci, IPayInfo paymentInfo, IUser user)
		{
			dsDCNew_Receipt ds = new dsDCNew_Receipt();
			try
			{
				// use data set for new denit card orders since fields are the same.
				dsDCNew_Receipt.DCNew_ReceiptRow dsRow = (dsDCNew_Receipt.DCNew_ReceiptRow)ds.DCNew_Receipt.NewRow();
				
				string s = ""; 
				if (Conn.Env == Const.PROD)
					s = ": ";
				dsRow.Heading1 = EnvIdent.ReceiptHead1;
				dsRow.Heading2 = "customerservice@dpiteleconnect.com";
				dsRow.CardProvider = ProdInfoCol.GetProd(prod.ProdId).ProdName;
				
				dsRow.EnrollFee = 0;
				dsRow.CardNumber = app.CardNum;

				dsRow.TransType = GetPaymentType(paymentInfo.PaymentType);

				IOrderSum sum   = paymentInfo.ParDemand.OrderSummary(null);
				dsRow.LoadFee = sum.GetFeeAmt();
				dsRow.LoadValue = sum.GetProdAmt();
				dsRow.AmountDue = sum.GetTotalAmtDue();
				dsRow.SaleDate = paymentInfo.PayDate;
				dsRow.ChangeAmount	= paymentInfo.ChangeAmount;
				dsRow.AmountTendered = paymentInfo.AmountTendered;
				

				dsRow.ConfNumber = Convert.ToInt32(receipt.ConfNum);
				dsRow.Merchant = user.LoginStoreCode ;
				dsRow.CurrentBalance = 20;

				string reciptMsg = "";
				reciptMsg = "Thank you for reloading your Purpose Instant Issue Card or Purpose Prepaid MasterCard \n\n"+
							"Any questions regarding your Purpose Prepaid MasterCard, please call Purpose Solutions \n" +
							"Customer Service at  1-800-962-4294.\n\n"+
							"Retain this copy for your records.\n"+
							"Thank you for choosing dPi Teleconnect!";


				dsRow.Message1 = reciptMsg;
				ds.DCNew_Receipt.Rows.Add(dsRow);								
				return ds;
			}			
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex.InnerException);
			}
		}


		public static dsDCNew_Receipt DCReload_Receipt_Deny(IDebitCardReceipt receipt, ICustInfo2 custInfo, IPayInfo paymentInfo, IUser user, IWIP wip)
		{
			dsDCNew_Receipt ds = new dsDCNew_Receipt();
			try
			{
				// use data set for new denit card orders since fields are the same.
				dsDCNew_Receipt.DCNew_ReceiptRow dsRow = (dsDCNew_Receipt.DCNew_ReceiptRow)ds.DCNew_Receipt.NewRow();
				
				string s = ""; 
				if (Conn.Env == Const.PROD)
					s = ": ";
				dsRow.Heading1 = EnvIdent.ReceiptHead1;
				dsRow.Heading2 = "customerservice@dpiteleconnect.com";
				IProdPrice prodi = (IProdPrice)wip["DebCard"];
				dsRow.CardProvider = ProdInfoCol.GetProd(prodi.ProdId).ProdName;;

				dsRow.TransType = GetPaymentType(paymentInfo.PaymentType);

				dsRow.EnrollFee = 0;
				dsRow.LoadFee = 0;
				dsRow.LoadValue = 0;
				dsRow.CardNumber = "####-####-####-####";

				dsRow.AmountDue = 0;
				dsRow.ChangeAmount	= 0;
				dsRow.AmountTendered = 0;

				dsRow.ConfNumber = int.Parse(receipt.ConfNum);
				dsRow.Merchant = user.LoginStoreCode;
				dsRow.SaleDate = paymentInfo.PayDate; //((ICardApp)wip["DebCardApp"]).AppDate;
				dsRow.Cardholder = "";
				
				dsRow.Message1 = "We have been unable to process your request. \n\n"+
								 "Please contact Purpose Solutions Customer "+
								 "Service at 1-800-962-4294 for further details.";

				ds.DCNew_Receipt.Rows.Add(dsRow);								
				return ds;
			}			
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex.InnerException);
			}
		}

		#endregion

		#region Methods
		static void DoLocalPayInfo(dsHIMP_Receipt.HIMP_ReceiptRow dsRow, IPayInfo pi)
		{
			if (!(pi is PayInfoLocal))
				return;

			PayInfoLocal paymentInfo = (PayInfoLocal)pi;

			dsRow.LocalAmountDue = paymentInfo.LocalAmountDue;
			dsRow.LocalAmountPaid = paymentInfo.LocalAmountPaid;
			dsRow.LDAmount = paymentInfo.LdAmount;

			if (paymentInfo.LdAmount > 0) 
			{
				dsRow.Message3 = "Long Distance provided by Reunion 1-877-559-5724";
				dsRow.Message4 = "5.9 cents/minute domestic calling.  International calling rates vary by country.";
			}
		}
				
		static void DoLocalPayInfo(dsHI_Receipt.HI_ReceiptRow dsRow, IPayInfo pi)
		{
			if (!(pi is PayInfoLocal))
				return;

			PayInfoLocal paymentInfo = (PayInfoLocal)pi;

			dsRow.LocalAmountDue = paymentInfo.LocalAmountDue;
			dsRow.LocalAmountPaid = paymentInfo.LocalAmountPaid;
			dsRow.LdAmount = paymentInfo.LdAmount;

			if (paymentInfo.LdAmount > 0) 
			{
				dsRow.Message6 = "Long Distance provided by Reunion 1-877-559-5724";
				dsRow.Message7 = "5.9 cents/minute domestic calling.  International calling rates vary by country.";
			}
		}

		static void DoLocalPayInfo(dsMP_Receipt.MP_ReceiptRow dsRow, IPayInfo ppi)
		{
			if (!(ppi is PayInfoLocal))
				return;

			PayInfoLocal paymentInfo = (PayInfoLocal)ppi;

			dsRow.LocalAmountDue = ((IPayInfoLocal)paymentInfo).LocalAmountDue;
			dsRow.LocalAmountPaid = paymentInfo.LocalAmountPaid;
			dsRow.LDAmount = paymentInfo.LdAmount;

			if (paymentInfo.LdAmount > 0) 
			{
				dsRow.Message2 = "Long Distance provided by Reunion 1-877-559-5724";
				dsRow.Message3 = "5.9 cents/minute domestic calling.  International calling rates vary by country.";
			}
		}
						
		static void DoLocalPayInfo(dsNO_Receipt.NO_ReceiptRow dsRow, IPayInfoLocal pi)
		{
			dsRow.LocalAmountDue = pi.LocalAmountDue;
			dsRow.LocalAmountPaid = pi.LocalAmountPaid;
			dsRow.LdAmount = pi.LdAmount;
			
			if (pi.LdAmount > 0) 
			{
				dsRow.Message2 = "Long Distance provided by Reunion 1-877-559-5724";
				dsRow.Message3 = "5.9 cents/minute domestic calling.  International calling rates vary by country.";
			}
		}
		
		static string GetPaymentType(PaymentType pt)
		{
			switch(pt) 
			{
				case PaymentType.Cash :
					return "Cash";

				case PaymentType.Debit :
					return "Debit Card";

				case PaymentType.Credit :
					return "Credit Card";

				case PaymentType.Check :
					return "Check";

				case PaymentType.MoneyOrder :
					return "Money Order";

				case PaymentType.TurboCash :
					return "Turbo Cash";

				default :
					throw new ArgumentException("No such property: '" + pt.ToString() + "'"); 
			}			
		}
		
		static string GetInternetPinText(IProdPrice[] products)
		{
			
			if (!IsInternetExist(products))//If we have more internet product we need to write a common text
				return "";

			string pinText = "Terms and Conditions:";
			pinText += "\nYour use of Slingshot's services through the software is also subject to Slingshot’s Terms of Service as amended from time to time by Slingshot and located at http://dialup.slingshot.com/unlimited_tos.asp. ";
			pinText += "\nCustomer Support contact information: ";
			pinText += "\nSlingshot Customer Support: 866-506-9600 ";
			pinText += "\nWeb Site: www.slingshot.com ";
			pinText += "\nsupport@corp.slingshot.com ";			

			return pinText;
		}
		#endregion

		static bool IsInternetExist(IProdPrice[] products)
		{
			for (int i = 0; i < products.Length; i++)
				if (products[i].ProdType == ProdCategory.Internet.ToString())
					return true;
			
			return false;
		}

	#endregion

	#region Reports
		public static dsEndOfDayReport Store_EndOfDay(IUser user, DateTime payDate)
		{
			UOW uow = null;
			
			try
			{				
				uow = new UOW();
				return MakeEndOfDayReportDS(StoreWrapper.Store_EndOfDay(uow, user.LoginStoreCode,
					StoreStatsCol.GetStoreStat(user.LoginStoreCode).StoreNumber, payDate));
			}				
			finally
			{
				uow.close();
			}
		}

		public static DataSet Store_GetDayTotals(IUser user, DateTime payDate)
		{
			SqlConnection cn = null;
			try
			{				
				cn = Conn.GetConn();
				return StoreWrapper.spGetDayTotals(cn, user, payDate);
			}				
			finally
			{
				cn.Close();
			}
		}

		public static DataSet Store_GetCommission(IUser user, DateTime startDate, DateTime endDate, string filter, string sort)
		{
			SqlConnection cn = null;			
			dsStore_Commission dss = new dsStore_Commission();
			try
			{		
				DataSet ds;
				cn = Conn.GetConn();	
				if(filter.Equals("Local"))
				{
					ds = StoreWrapper.spGetCommission(cn, user, startDate, endDate);
				}
				else
				{
					ds = StoreWrapper.spGetWirelessCommission(cn, user, startDate, endDate);
				}
				if(sort != null)
				{
					ds.Tables["spStores_GetCommission"].DefaultView.Sort = sort;				

					foreach(DataRowView dataRowView in ds.Tables["spStores_GetCommission"].DefaultView)
					{					
						dss.Tables["spStores_GetCommission"].ImportRow(dataRowView.Row);					
					}
					return(dss);
				}
				else
					return ds;
			}				
			finally
			{
				cn.Close();
			}
		}

		public static DataSet Store_GetOrderStatus(IUser user, DateTime startDate, DateTime endDate)
		{
			SqlConnection cn = null;
			try
			{				
				cn = Conn.GetConn();				
				return StoreWrapper.spGetOrderStatus(cn, user, startDate, endDate);
			}				
			finally
			{
				cn.Close();
			}
		}
		public static DataSet Store_GetOrderStatus(IUser user, DateTime startDate, DateTime endDate, string sort)
		{
			SqlConnection cn = null;			
			dsStore_OrderStatus dss = new dsStore_OrderStatus();
			try
			{				
				cn = Conn.GetConn();				
				DataSet ds = StoreWrapper.spGetOrderStatus(cn, user, startDate, endDate);

				ds.Tables["spStores_GetOrderStatus"].DefaultView.Sort = sort;				

				foreach(DataRowView dataRowView in ds.Tables["spStores_GetOrderStatus"].DefaultView)
				{					
					dss.Tables["spStores_GetOrderStatus"].ImportRow(dataRowView.Row);					
				}
				return(dss);			
			}				
			finally
			{
				cn.Close();
			}
		}
		public static DataSet CertResultsGetByStore(IUser user)
		{
			SqlConnection cn = null;
			
			dsCertResult dss = new dsCertResult();
			
			try
			{				
				cn = Conn.GetConn();		
				

				DataSet ds = StoreWrapper.spCertResults_Get_StoreCode(cn, user);
				
				
//				ds.Tables["spStores_GetCustomerList"].DefaultView.Sort = sort;
				foreach(DataRowView dataRowView in ds.Tables["spCertResults_Get_StoreCode"].DefaultView)
				{					
					dss.Tables["spCertResults_Get_StoreCode"].ImportRow(dataRowView.Row);					
				}
				return(dss);			
				//return StoreWrapper.spCertResults_Get_StoreCode(cn, user);				
			}				
			finally
			{
				cn.Close();
			}
		}
		public static DataSet CertResultsGetByCorp(int corpid)
		{
			SqlConnection cn = null;

			dsCertResult dss = new dsCertResult();
			try
			{				
				cn = Conn.GetConn();
				DataSet ds = StoreWrapper.spCertResults_Get_Corp(cn, corpid);
				foreach(DataRowView dataRowView in ds.Tables["spCertResults_Get_StoreCode"].DefaultView)
				{					
					//Table name is same for both CertReuslt store and corp. Before change make sure dataset name changed in store wrapper
					dss.Tables["spCertResults_Get_StoreCode"].ImportRow(dataRowView.Row);					
				}
				return(dss);
				//return StoreWrapper.spCertResults_Get_Corp(cn, corpid);			
			}				
			finally
			{
				cn.Close();
			}
		}
		public static DataSet PendingOrderPaymentInfoByCorp(int corpid)
		{
			SqlConnection cn = null;
			try
			{				
				cn = Conn.GetConn();				
				return StoreWrapper.spPendingOrderPaymentInfoByCorp(cn, corpid);
			}				
			finally
			{
				cn.Close();
			}
		}
		public static DataSet PendingTransactionsByStore(int corpid, string storeNum, DateTime fromDate)
		{
			SqlConnection cn = null;
			try
			{				
				cn = Conn.GetConn();				
				return StoreWrapper.spPendingTransactionsByStore(cn, corpid, storeNum, fromDate);
			}				
			finally
			{
				cn.Close();
			}
		}
		public static DataSet Store_GetOrderStatus(IUser user, DateTime startDate, DateTime endDate, string sort, string rowFilter)
		{
			SqlConnection cn = null;			
			dsStore_OrderStatus dss = new dsStore_OrderStatus();
			try
			{				
				cn = Conn.GetConn();				
				DataSet ds = StoreWrapper.spGetOrderStatus(cn, user, startDate, endDate);

				ds.Tables["spStores_GetOrderStatus"].DefaultView.Sort = sort;				
				ds.Tables["spStores_GetOrderStatus"].DefaultView.RowFilter = rowFilter;

				foreach(DataRowView dataRowView in ds.Tables["spStores_GetOrderStatus"].DefaultView)
				{					
					dss.Tables["spStores_GetOrderStatus"].ImportRow(dataRowView.Row);					
				}
				return(dss);			
			}				
			finally
			{
				cn.Close();
			}
		}
		public static DataSet Store_GetCustomerList(IUser user, DateTime startDate, DateTime endDate, string sort)
		{
			SqlConnection cn = null;			
			dsStore_CustomerList dss = new dsStore_CustomerList();
			try
			{				
				cn = Conn.GetConn();				
				DataSet ds = StoreWrapper.spGetCustomerList(cn, user, startDate, endDate);

				ds.Tables["spStores_GetCustomerList"].DefaultView.Sort = sort;				

				foreach(DataRowView dataRowView in ds.Tables["spStores_GetCustomerList"].DefaultView)
				{					
					dss.Tables["spStores_GetCustomerList"].ImportRow(dataRowView.Row);					
				}
				return(dss);			
			}				
			finally
			{
				cn.Close();
			}
		}
		public static DataSet Store_GetCustomerList(IUser user, DateTime startDate, DateTime endDate, string sort, string rowFilter)
		{
			SqlConnection cn = null;			
			dsStore_CustomerList dss = new dsStore_CustomerList();
			try
			{				
				cn = Conn.GetConn();				
				DataSet ds = StoreWrapper.spGetCustomerList(cn, user, startDate, endDate);

				ds.Tables["spStores_GetCustomerList"].DefaultView.Sort = sort;				
				ds.Tables["spStores_GetCustomerList"].DefaultView.RowFilter = rowFilter;

				foreach(DataRowView dataRowView in ds.Tables["spStores_GetCustomerList"].DefaultView)
				{					
					dss.Tables["spStores_GetCustomerList"].ImportRow(dataRowView.Row);					
				}
				return(dss);			
			}				
			finally
			{
				cn.Close();
			}
		}

		public static DataSet Store_GetActiveDueDate(IUser user, DateTime startDate, DateTime endDate, string sort, string rowFilter)
		{
			SqlConnection cn = null;			
			dsStore_ActCustListByDueDate dss = new dsStore_ActCustListByDueDate();
			try
			{				
				cn = Conn.GetConn();				
				DataSet ds = StoreWrapper.GetActiveDueDate(cn, user, startDate, endDate);

				ds.Tables["spStores_GetActiveCustByDueDate"].DefaultView.Sort = sort;				
				ds.Tables["spStores_GetActiveCustByDueDate"].DefaultView.RowFilter = rowFilter;

				foreach(DataRowView dataRowView in ds.Tables["spStores_GetActiveCustByDueDate"].DefaultView)
				{					
					dss.Tables["spStores_GetActiveCustByDueDate"].ImportRow(dataRowView.Row);					
				}
				return(dss);			
			}				
			finally
			{
				cn.Close();
			}
		}
		public static DataSet GetSalesRptByStateData(int corpId, string state, string dma, DateTime startDate, DateTime endDate, string tranType)
		{
			SqlConnection cn = null;			
			dsSalesRptByState dss = new dsSalesRptByState();
			try
			{				
				cn = Conn.GetConn();
				DataSet ds = AgentExtranetRptWrapper.GetSalesRptByStateData(cn, corpId, state, dma, startDate, endDate, tranType);

				//ds.Tables["spWeb_LocalSalesByDMA"].DefaultView.Sort = sort;				
				//ds.Tables["spWeb_LocalSalesByDMA"].DefaultView.RowFilter = rowFilter;

				foreach(DataRowView dataRowView in ds.Tables["spWeb_LocalSalesByDMA"].DefaultView)
				{					
					dss.Tables["SalesRptByState"].ImportRow(dataRowView.Row);					
				}
				return(dss);			
			}				
			finally
			{
				cn.Close();
			}
		}
		public static DataSet GetSalesRptByStoreData(int corpId, string state, string dma, DateTime startDate, DateTime endDate, string tranType)
		{
			SqlConnection cn = null;			
			dsSalesRptByStore dss = new dsSalesRptByStore();
			try
			{				
				cn = Conn.GetConn();
				DataSet ds = AgentExtranetRptWrapper.GetSalesRptByStoreData(cn, corpId, state, dma, startDate, endDate, tranType);

				//ds.Tables["spWeb_LocalSalesByDMA"].DefaultView.Sort = sort;				
				//ds.Tables["spWeb_LocalSalesByDMA"].DefaultView.RowFilter = rowFilter;

				foreach(DataRowView dataRowView in ds.Tables["spWeb_Rpt_LocalSalesByStore"].DefaultView)
				{					
					dss.Tables["SalesRptByStore"].ImportRow(dataRowView.Row);					
				}
				return(dss);			
			}				
			finally
			{
				cn.Close();
			}
		}	
	#endregion

	#region Implementation
		static dsEndOfDayReport MakeEndOfDayReportDS(EndOfDayDTO dto)
		{
			dsEndOfDayReport ds = new dsEndOfDayReport();
			dsEndOfDayReport.EndOfDayReportRow row 
				= (dsEndOfDayReport.EndOfDayReportRow)ds.EndOfDayReport.NewEndOfDayReportRow();
			
			row.StoreCode = dto.StoreCode;
			row.StoreNumber = dto.StoreNumber;
			row.Date = dto.Date;
	
			row.LocalRevenue = dto.LocalRev;
			row.OtherRevenue = dto.OtherRev;		
			
			row.LocalCommission = dto.OtherCom;
			row.OtherCommission = dto.OtherCom;
			
			row.CreditReceipts = dto.CreditReceipts;
			row.OtherReceipts = dto.OtherReceipts;

			row.ControlNumber = dto.ControlNumber;

			ds.EndOfDayReport.Rows.Add(row);
			
			return ds;
		}
	#endregion
	}
}
