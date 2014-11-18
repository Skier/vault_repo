using System;
using NUnit.Framework;

using DPI.Interfaces;
using DPI.Components;
using DPI.Services;
using DPI.ClientComp;

namespace DPI.Services
{
	[TestFixture]
	public class DebCardSvcTest
	{
		/* Data */
		IMap imap;
		int prod; //test with debit card product id
		string storeCode = "NCRW0443RW";
		OrderType otype = OrderType.New;
		IProdPrice debCard;
		IDemand dmd;
		ICustInfo2 custInfo;
		IProdPrice[] fees;
		IPayInfo payInfo;
		ICardApp app;
		UOW uow;
		IUser user;

		

		public static void Main()
		{
			DebCardSvcTest test = new DebCardSvcTest();
			test.GetDebCardProd();
//			test.GetFees();
			test.GetProductOrderRule();
			test.NewCard();
			test.Refill();
			
		}

		[Test]
		void GetDebCardProd()
		{
			imap = new IdentityMap();
			debCard = DebCardSvc.GetDebCardProd(imap, storeCode);
			prod = debCard.ProdId;
			Assertion.AssertNotNull(debCard);
		}

//		[Test]
//		void GetFees()
//		{
////			imap = new IdentityMap();
//			fees = DebCardSvc.GetFees(imap, prod, storeCode, otype);
//			Assertion.AssertNotNull(fees);
//		}
		
		[Test]
		void GetProductOrderRule()
		{
//			imap = new IdentityMap();
			IProductOrderRule por = GetProductOrderRule(imap);
			Assertion.Assert(por != null);
		}
		[Test]
		void NewCard()
		{
//			imap = new IdentityMap();
			IDebitCardReceipt rct = NewCard(imap);
			Assertion.Assert(rct != null);
		}
		[Test]
		void Refill()
		{
//			imap = new IdentityMap();
			IDebitCardReceipt rct = Refill(imap);
			Assertion.Assert(rct != null);
		}
		IProductOrderRule GetProductOrderRule(IMap imap)
		{
			
			uow = new UOW(imap);

			dmd = new Demand(uow, DemandType.New.ToString());
			//dmd.DmdType = "New";
			dmd.Statement = 1;
			dmd.ConsId = 4;
			dmd.ConsumerAgent = "ConsumerAgent";
			dmd.Status = "WIP";
			dmd.IsUnderWF = false;
			//dmd.Ilec = 22; // SWB
			
			return DebCardSvc.GetProductOrderRule(imap, dmd.DmdType, prod);
		}
		IDebitCardReceipt NewCard(IMap imap)
		{
			
			//debCard, fees & dmd already defined
			
			custInfo = new CustInfo(uow);
			custInfo.Birthday = DateTime.Today.ToShortDateString();
			custInfo.FirstName = "Omar";
			custInfo.LastName = "Azad";
			
			payInfo = PayInfo.GetPayInfo(uow, PayInfoClass.PayInfo.ToString());
			payInfo.ConfNumber = new Random().Next(100, 1000000).ToString();
			payInfo.ParDemand = dmd; 
			payInfo.PaymentType = (PaymentType)8;
			
			app = new CardApp();
			//app.AppDate = DateTime.Today;

			
			user = new User();
			user.LoginStoreCode = storeCode;
			user.ClerkId = "Roumel DC";
			
			return DebCardSvc.NewCard(imap, payInfo, app, user, null);
		}
		
		IDebitCardReceipt Refill(IMap imap)
		{	
			return DebCardSvc.Refill(imap, payInfo, app, user, null);
		}
	}
}