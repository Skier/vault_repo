//using System;
//using System.Xml;
//using System.Text;
//using System.Collections;
//
//using DPI.Components;
//using DPI.Interfaces;
//using DPI.Services;
//
//namespace DPI.ClientComp
//{
//	public class XmlReqFactory
//	{
//		public static IXReader GetReader(XmlNode node)
//		{
//			return new XReader(node);
//		}
//		public static IXElement GetMonthlyPymtReq(string userName, string password, string phone, string  transactionNumber,
//											   decimal localAmount, decimal ldAmount)
//		{
//			return new MonthlyPaymentXml(userName, password, phone, transactionNumber,
//											   localAmount, ldAmount);
//
//		}
//	}
//}