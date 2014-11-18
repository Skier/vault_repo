//using System;
//using System.Collections;
//using System.ComponentModel;
//using System.Xml;
//using System.IO;
//using System.Text;
//
//using DPI.Interfaces;
//using DPI.Components;
//
//namespace DPI.Components
//{	
//	public class MonthlyPaymentXml : XElement
//	{
//		public MonthlyPaymentXml(string userName, string password, string phone, string  transactionNumber,
//									decimal localAmount, decimal ldAmount)
//		{
//			name = "MonthlyPymtReq";
//			
//			attrs = new KeyVal[6];
//			int i = 0;
//			
//			attrs[i++] = new KeyVal("userName",           userName);
//			attrs[i++] = new KeyVal("password",           password);
//			attrs[i++] = new KeyVal("phone",              phone); 
//			attrs[i++] = new KeyVal("transactionNumber",  transactionNumber);
//			attrs[i++] = new KeyVal("localAmount",        localAmount);
//			attrs[i++] = new KeyVal("ldAmount",           ldAmount);
//			
//		}
//	}
//}