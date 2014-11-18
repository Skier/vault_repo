//using System;
//using System.Xml;
//using System.Data;
//using System.Data.SqlClient;
//using NUnit.Framework;
//using DPI.Components;
//using DPI.Interfaces;
//
//
//namespace DPI.ComponentsTests
//{
//	[TestFixture]
//	public class XmlSlingShotTest
//	{
//		UOW uow;
//		XmlDocument xmlDocument;
//		IUserAccount user;
//		IPayInfo payInfo;
//		
//		string outputString;
//
//		public static void Main()
//		{
//			XmlSlingShotTest test = new XmlSlingShotTest();
//			test.SetValues();
//			test.GetEnrollMsg();
//			//			test.GetEnrollMsg("different");
//			
//
//		}
//		
//		void GetEnrollMsg()
//		{
//			XmlEleEnrollReq msg = new XmlEleEnrollReq(payInfo, app);
//			XmlDocument xdoc = msg.ToXmlDoc();
//			
//			outputString = xdoc.OuterXml;
//			Console.WriteLine(outputString);
//			Assertion.AssertNotNull(outputString);
//		}
//		//		[Test]
//		//		void GetEnrollMsg(string str)
//		//		{
//		//			XMLEnrollMsg msg = new XMLEnrollMsg(sum, app, custInfo, dmd);
//		//			
//		//			outputString = msg.XmlStringMessage;
//		//			Console.WriteLine(outputString);
//		//			Assertion.AssertNotNull(outputString);
//		//		}		
//		//		void GetEnrollMsg()
//		//		{
//		//			XMLPurposeMsg msg = new XMLPurposeMsg();
//		//			XmlDocument xdoc = msg.GetEnrollReqMsg(storeCode, clerkId, custInfo, app, payInfo);
//		//
//		//			outputString = msg.XmlStringMessage;
//		//			Console.WriteLine(outputString);
//		//			Assertion.AssertNotNull(outputString);
//		//		}
//		void SetValues()
//		{
////			uow = new UOW();
////
////			dmd = new Demand();
////			
////			dmd.ConsumerAgent = "Omar";
////			dmd.StoreCode = "WE235Y65";
////			dmd.Consumer = CustInfo.find(uow, 148);
////			
////			sum = dmd.OrderSummary(uow);
//			
//
//			user = new UserAccount();
//			
//			storeCode = "WE235Y65";
//			clerkId = "Omar";
//
//			IPayInfo payInfo = PayInfo.GetPayInfo(uow, PayInfoClass.PayInfo.ToString());
//			payInfo.TotalAmountPaid = 119m;
//
//			
//		}
//	}
//}