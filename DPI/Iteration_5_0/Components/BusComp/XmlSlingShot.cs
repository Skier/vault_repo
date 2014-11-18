using System;
using System.Collections;
using System.ComponentModel;
using System.Xml;
using System.IO;
using System.Text;

using DPI.Interfaces;
using DPI.Components;

namespace DPI.Components
{	
	#region SlingShot Xml Requests
	public class GetACodeXml : XElement
	{
		public GetACodeXml(ISSPostArgs args)
		{
			name = "request";
			SetupElmnts(args);
		} 

		/*		Implementation		*/
		void SetupElmnts(ISSPostArgs args)
		{
			children = new XElement[7];
			int i = 0;

			children[i++] = new XElement("source",   args.Source);
			children[i++] = new XElement("pwd",		 args.Pwd);
			children[i++] = new XElement("tranid",   args.TranId.ToString());
			children[i++] = new XElement("retailer", args.Retailer);
			children[i++] = new XElement("storeid",  args.StoreId);
			children[i++] = new XElement("upc",      args.Upc);
			children[i++] = new XElement("price",    args.Price.ToString()); 
		}		
	}
	public class VoidACodeXml : XElement
	{
		public VoidACodeXml(ISSPostArgs args)
		{
			name = "request";
			SetupElmnts(args);
		}

		/*		Implementation		*/
		void SetupElmnts(ISSPostArgs args)
		{
			children = new XElement[9];
			int i = 0;

			children[i++] = new XElement("source", args.Source);					
			children[i++] = new XElement("pwd",		 args.Pwd);
			children[i++] = new XElement("reason",		 args.Reason);
			children[i++] = new XElement("confnum",		 args.ConfNum);			
			children[i++] = new XElement("tranid", args.TranId.ToString());
			children[i++] = new XElement("retailer", args.Retailer);
			children[i++] = new XElement("storeid", args.StoreId);
			children[i++] = new XElement("acode", args.ACode);
			children[i++] = new XElement("manid", args.ManId.ToString());
		}
	}

	public class UpcForPhoneXml : XElement
	{
		public UpcForPhoneXml(ISSPostArgs args)
		{
			name = "request";
			SetupElmnts(args);
		}

		/*		Implementation		*/
		void SetupElmnts(ISSPostArgs args)
		{
			children = new XElement[4];
			int i = 0;

			children[i++] = new XElement("source", args.Source);					
			children[i++] = new XElement("pwd",		 args.Pwd);		
			children[i++] = new XElement("areacode", args.AreaCode.ToString());
			children[i++] = new XElement("prefix", args.Prefix.ToString());			
		}
	}

	#endregion

	#region Elements	
//	public class XESource : XElement
//	{
//		public XESource(string source)
//		{
//			name = "source";
//
//			children = new XElement[7];
//			int i = 0;
//
//			children[i++] = new XElement("transactionId", args.PayInfo.Id.ToString());		
//			
//			if (args.PayInfo.VFConf == null)
//				children[i++] = new XElement("confirmation", args.PayInfo.Id.ToString());
//			else
//				children[i++] = new XElement("confirmation", args.PayInfo.VFConf);
//			
//			children[i++] = new XElement("salesCommissionAmount", args.AmtComm.ToString());
//			children[i++] = new XElement("totalSaleAmount", args.AmtPaid.ToString());
//			children[i++] = new XElement("serviceType", args.ServType);
//			children[i++] = new XElement("taxAmount", args.AmtTaxes.ToString());
//			children[i++] = new XElement("receiptId", args.ReceitId); 
//		}
//	}
//	public class XETranId : XElement
//	{
//		public XETranId(IRWPostArgs args)
//		{
//			name = "dpiResult";
//
//			children = new XElement[2];
//			int i = 0;
//
//			children[i++] = new XElement("transactionId", args.PayInfo.Id.ToString());		
//			children[i++] = new XElement("confirmation", args.PayInfo.VFConf);			
//		}
//	}
//	public class XERetailer : XElement
//	{
//		public XERetailer(IRWPostArgs args)
//		{
//			name = "paymentTypes";
//
//			children = new XElement[4];
//			int i = 0;
//
//			children[i++] = new XElement("paymentType",  args.PymtType);  
//			children[i++] = new XElement("paymentValue", args.PayInfo.TotalAmountPaid.ToString());
//
//			children[i++] = new XElement("paymentAddt0", args.PayInfo.CheckNumber == null ? "" : args.PayInfo.CheckNumber);	
//			children[i++] = new XElement("paymentAddt1", args.PayInfo.CheckName == null ? "" : args.PayInfo.CheckName);		
//		}	
//	}
//	public class XEStoreId : XElement
//	{
//
//		public XEStoreId(string token, string storeNumber)
//		{
//			name = "storeInfo";
//
//			children = new XElement[2];
//			int i = 0;
//
//			children[i++] = new XElement("storeId", storeNumber);
//			children[i++] = new XElement("token", token);			
//		}
//	}
//	
//	public class XEUpc : XElement
//	{
//
//		public XEUpc()
//		{
//			name = "dpiTransactionList";
//
//			children = new XElement[2];
//			int i = 0;
//
//			children[i++] = new XElement("dpiPassword", "2");
//			children[i++] = new XElement("transactionDate", "6758921");			
//		}
//	}
//	
//	public class XEPrice : XElement
//	{
//		public XEPrice(IRWPostArgs args)
//		{
//			name = "dpiResult";
//
//			children = new XElement[7];
//			int i = 0;
//
//			children[i++] = new XElement("transactionId", args.PayInfo.Id.ToString());		
//			
//			if (args.PayInfo.VFConf == null)
//				children[i++] = new XElement("confirmation", args.PayInfo.Id.ToString());
//			else
//				children[i++] = new XElement("confirmation", args.PayInfo.VFConf);
//			
//			children[i++] = new XElement("salesCommissionAmount", args.AmtComm.ToString());
//			children[i++] = new XElement("totalSaleAmount", args.AmtPaid.ToString());
//			children[i++] = new XElement("serviceType", args.ServType);
//			children[i++] = new XElement("taxAmount", args.AmtTaxes.ToString());
//			children[i++] = new XElement("receiptId", args.ReceitId); 
//		}
//	}
	#endregion
}