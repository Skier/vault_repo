using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;


namespace DPI.ComponentsTests
{
	[TestFixture]
	public class XMLPurposeMsgTest
	{
		UOW uow;
		XmlDocument xmlDocument;
		IUserAccount user;
		IDemand dmd;
		IPayInfo payInfo;
		ICustInfo2 custInfo;
		ICardApp app;
		string storeCode;
		string clerkId;
		IOrderSum sum;

		string outputString;

		public static void Main()
		{
			XMLPurposeMsgTest test = new XMLPurposeMsgTest();
			test.SetValues();
			test.GetEnrollMsg();
//			test.GetEnrollMsg("different");
			

		}
		
		void GetEnrollMsg()
		{
			XmlEleEnrollReq msg = new XmlEleEnrollReq(payInfo, app);
			XmlDocument xdoc = msg.ToXmlDoc();
			
			outputString = xdoc.OuterXml;
			Console.WriteLine(outputString);
			Assertion.AssertNotNull(outputString);
		}
//		[Test]
//		void GetEnrollMsg(string str)
//		{
//			XMLEnrollMsg msg = new XMLEnrollMsg(sum, app, custInfo, dmd);
//			
//			outputString = msg.XmlStringMessage;
//			Console.WriteLine(outputString);
//			Assertion.AssertNotNull(outputString);
//		}		
//		void GetEnrollMsg()
//		{
//			XMLPurposeMsg msg = new XMLPurposeMsg();
//			XmlDocument xdoc = msg.GetEnrollReqMsg(storeCode, clerkId, custInfo, app, payInfo);
//
//			outputString = msg.XmlStringMessage;
//			Console.WriteLine(outputString);
//			Assertion.AssertNotNull(outputString);
//		}
		void SetValues()
		{
			uow = new UOW();

			dmd = new Demand();
			
			dmd.ConsumerAgent = "Omar";
			dmd.StoreCode = "WE235Y65";
			dmd.Consumer = CustInfo.find(uow, 148);
			
			sum = dmd.OrderSummary(uow);
			

			//user = new UserAccount();
			storeCode = "WE235Y65";
			clerkId = "Omar";

			IPayInfo payInfo = PayInfo.GetPayInfo(uow, PayInfoClass.PayInfo.ToString());
			payInfo.TotalAmountPaid = 119m;

			custInfo = new CustInfo();
			custInfo.Dob = DateTime.Today;
			custInfo.FirstName = "Omar";
			custInfo.LastName = "Azad";

			custInfo.MailAddr = new CustAddress();
			custInfo.MailAddr.Street = "San Jacinto Drive";
			custInfo.MailAddr.StreetNum = "1016";
			custInfo.MailAddr.City = "Irving";
			custInfo.MailAddr.State = "TX";
			custInfo.MailAddr.Zipcode = "75063";


			app = new CardApp();
			app.CardNum = "12345678901";
		}
	}
}