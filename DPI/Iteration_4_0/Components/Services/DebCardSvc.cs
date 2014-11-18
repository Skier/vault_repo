using System;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class DebCardSvc
	{
		public static IProdPrice GetDebCardProd(IMap imap, string storeCode) 
		{
			UOW uow = null;
		
			try
			{
				uow = new UOW(imap, "DebCardSvc.GetDebCardProd");

				if (storeCode == null)
					throw new ArgumentException("DebCardSvc.GetDebCardProd: StoreCode is required");

				if (storeCode.Trim().Length == 0)
					throw new ArgumentException("DebCardSvc.GetDebCardProd: StoreCode is required");

				int prod = StoreLocation.GetDebitCardProd(uow, storeCode);
				if (prod == 0)
					throw new ApplicationException("Debit card product is not found for store: " + storeCode);

				int supplier = ProdInfoCol.GetProd(prod).Supplier;

				Location state = Location.find(uow, StoreLocation.find(uow, storeCode).St);

				return ProdPrice2.GetProdPrice(uow, prod, state.LocId, supplier);
			}
			finally
			{	
				uow.close();
			}
		}
		public static IProductOrderRule GetProductOrderRule(IMap imap, string dmdType, int prod)
		{
			UOW uow = null;
		
			try
			{
				if (prod == 0)
					throw new ArgumentException("DebCardSvc.GetProductOrderRule: Product is required");

				if (dmdType == null)
					throw new ArgumentException("DebCardSvc.GetProductOrderRule: DmdType is required");

				if (dmdType.Trim().Length == 0)
					throw new ArgumentException("DebCardSvc.GetProductOrderRule: DmdType is required");

				uow = new UOW(imap, "DebCardSvc.GetProductOrderRule");
				return ProductOrderRule.find(uow, dmdType, prod);
			}
			finally
			{	
				uow.close();
			}
		}
		public static void PreSave(IMap imap)
		{
			UOW uow = null; 
		
			try
			{
				uow = new UOW(imap, "DebCardSvc.NewCard()"); 
	
				uow.BeginTransaction(); 
				uow.commit();   // pre-saves Bus objects.
			}
			finally
			{
				uow.close();
			}
		}
		public static IDebitCardReceipt NewCard(IMap imap, IPayInfo payInfo, ICardApp app, 
												IUser user, IWireless_Transactions tran)
		{ 
			UOW uow = null; 
		
			try
			{
				uow = new UOW(imap, "DebCardSvc.NewCard()"); 

				uow.BeginTransaction(); 
				IDebitCardReceipt rcpt = DebitCardFactory.GetProvider(payInfo.ParDemand).Enroll(uow, payInfo, app, tran);

				if (rcpt.IsApproved)
					WebServSvc.PresetPymt(uow, user, payInfo, rcpt.ConfNum); //post payment if a corp requires it
				
				uow.commit();
				
				return rcpt;
			}
			catch (Exception ex)
			{
				uow.Rollback();		
				
				ErrLogSvc.LogError("DebCardSvc.NewCard", user.ClerkId, ex.Message + ", Stack trace: " + ex.StackTrace);
				throw ex;
			}
			finally
			{	
				uow.close();	
			}
		}
		public static IDebitCardReceipt Refill(IMap imap, IPayInfo payInfo, ICardApp app, 
			                                   IUser user, IWireless_Transactions tran)
		{
			UOW uow = null; 

			try
			{
				uow = new UOW(imap, "DebCardSvc.NewCard()"); 
				
				uow.BeginTransaction();

				IDebitCardReceipt rcpt = DebitCardFactory.GetProvider(payInfo.ParDemand).Reload(uow, payInfo, app, tran);		
				if (rcpt.IsApproved)
					WebServSvc.PresetPymt(uow, user, payInfo, rcpt.ConfNum); //post payment if a corp requires it
				
				uow.commit();
				return rcpt;
			}
			catch (Exception ex)
			{
				uow.Rollback();
				ErrLogSvc.LogError("DebCardSvc", user.ClerkId, ex.Message + ", Stack trace: " + ex.StackTrace);
				
				throw ex;
			}
			finally
			{	
				uow.close();
			}
		}
		public static ICardApp GetDebitCardApp(IMap imap, IDemand demand)
		{
			return DebitCardFactory.GetCardApp(imap, demand);
		}
	}
}