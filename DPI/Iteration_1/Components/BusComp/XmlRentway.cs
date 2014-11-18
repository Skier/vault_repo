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
#region RentWay Xml Requests
	public class XMLWirelessVendors : XElement
	{
		#region Constructors
		public XMLWirelessVendors(IVendors[] vendors)
		{
			name = "request";
			SetReqAttrs(vendors.Length);
			SetupElmnts(vendors);
		}
		public XMLWirelessVendors(string msg)
		{
			name = "request";
			SetReqAttrs();
			SetupElmnts(msg);
		}
		#endregion

		#region	Implementation
		void SetupElmnts(IVendors[] vendors)
		{
			children = new XElement[vendors.Length];

			for (int i = 0; i < vendors.Length; i++)
				children[i] = new XMLElementNew(vendors[i]);			
		}		
		void SetupElmnts(string msg)
		{
			children = new XElement[1];

			int i = 0;
			
			children[i] = new XMLElementNew(msg);			
		}		
		void SetReqAttrs(int row)
		{
			attrs = new KeyVal[2];
			
			int i = 0;

			attrs[i++] = new KeyVal("columns", "5");
			attrs[i++] = new KeyVal("rows", row.ToString());
		}
		
		void SetReqAttrs()
		{
			attrs = new KeyVal[2];
			
			int i = 0;

			attrs[i++] = new KeyVal("columns", "3");
			attrs[i++] = new KeyVal("rows", "0");
		}
		#endregion

		#region Internal Class

		internal class XMLElementNew : XElement
		{
			#region Constructors
			internal XMLElementNew(IVendors vendor)
			{
				name = "new";
				
				SetAttrs(vendor);				
			}
			internal XMLElementNew(string msg)
			{
				name = "new";
				
				SetAttrs(msg);				
			}
			#endregion

			#region Implementations
			void SetAttrs(IVendors vendor)
			{
				attrs = new KeyVal[5];
				
				int i = 0;
				attrs[i++] = new KeyVal("errorcode", Const.WS_SUCCESSCODE);
				attrs[i++] = new KeyVal("message", "Successful");
				attrs[i++] = new KeyVal("transactiontime", DateTime.Now.ToString("dd-MMM-yy HH:mm tt"));
				attrs[i++] = new KeyVal("vendor_id", vendor.Vendor_id.ToString());
				attrs[i++] = new KeyVal("vendor_name", vendor.Vendor_name);				
			}
			void SetAttrs(string msg)
			{
				attrs = new KeyVal[3];
				
				int i = 0;
				attrs[i++] = new KeyVal("errorcode", Const.WS_ERRORCODE);
				attrs[i++] = new KeyVal("message", msg);
				attrs[i++] = new KeyVal("transactiontime", DateTime.Now.ToString("dd-MMM-yy HH:mm tt"));
			}
			#endregion
		}

		#endregion
	}	
	public class XMLWirelessProducts : XElement
	{
		#region Constructors
		public XMLWirelessProducts(IWireless_Products[] wps)
		{
			name = "request";
			SetReqAttrs(wps.Length);
			SetupElmnts(wps);
		}
		public XMLWirelessProducts(string msg)
		{
			name = "request";
			SetReqAttrs();
			SetupElmnts(msg);
		}
		#endregion

		#region	Implementation
		void SetupElmnts(IWireless_Products[] wps)
		{
			children = new XElement[wps.Length];

			for (int i = 0; i < wps.Length; i++)
				children[i] = new XMLElementNew(wps[i]);			
		}		
		void SetupElmnts(string msg)
		{
			children = new XElement[1];

			int i = 0;
			
			children[i] = new XMLElementNew(msg);			
		}		
		void SetReqAttrs(int row)
		{
			attrs = new KeyVal[2];
			
			int i = 0;

			attrs[i++] = new KeyVal("columns", "8");
			attrs[i++] = new KeyVal("rows", row.ToString());
		}
		
		void SetReqAttrs()
		{
			attrs = new KeyVal[2];
			
			int i = 0;

			attrs[i++] = new KeyVal("columns", "5");
			attrs[i++] = new KeyVal("rows", "0");
		}
		#endregion

		#region Internal Class

		internal class XMLElementNew : XElement
		{
			#region Constructors
			internal XMLElementNew(IWireless_Products wp)
			{
				name = "new";
				
				SetAttrs(wp);				
			}
			internal XMLElementNew(string msg)
			{
				name = "new";
				
				SetAttrs(msg);				
			}
			#endregion

			#region Implementations
			void SetAttrs(IWireless_Products wp)
			{
				attrs = new KeyVal[8];
				
				int i = 0;
				attrs[i++] = new KeyVal("errorcode", Const.WS_SUCCESSCODE);
				attrs[i++] = new KeyVal("expiration", wp.Expiration);
				attrs[i++] = new KeyVal("message", "Successful");
				attrs[i++] = new KeyVal("price", wp.Price.ToString());
				attrs[i++] = new KeyVal("product_name", wp.Product_name);
				attrs[i++] = new KeyVal("soc", wp.Soc);
				attrs[i++] = new KeyVal("transactiontime", DateTime.Now.ToString("dd-MMM-yy HH:mm tt"));
				attrs[i++] = new KeyVal("wireless_product_id", wp.Wireless_product_id.ToString());				
			}
			void SetAttrs(string msg)
			{
				attrs = new KeyVal[3];
				
				int i = 0;
				attrs[i++] = new KeyVal("errorcode", Const.WS_ERRORCODE);
				attrs[i++] = new KeyVal("message", msg);
				attrs[i++] = new KeyVal("transactiontime", DateTime.Now.ToString("dd-MMM-yy HH:mm tt"));
			}
			#endregion
		}

		#endregion
	}	
	
	public class XMLOrderProducts : XElement
	{
		#region Constructors
		public XMLOrderProducts(IPinReceipt rcpt, IWireless_Products wp)
		{
			name = "order";
			SetOrderAttrs();
			SetupElmnts(rcpt, wp);
		}
		public XMLOrderProducts(string msg)
		{
			name = "order";
			SetOrderAttrs();
			SetupElmnts(msg);
		}
		#endregion

		#region	Implementation
		void SetupElmnts(IPinReceipt rcpt, IWireless_Products wp)
		{
			children = new XElement[1];

			children[0] = new XMLElementNew(rcpt, wp);			
		}		
		void SetupElmnts(string msg)
		{
			children = new XElement[1];

			int i = 0;
			
			children[i] = new XMLElementNew(msg);			
		}		
		void SetOrderAttrs()
		{
			attrs = new KeyVal[2];
			
			int i = 0;

			attrs[i++] = new KeyVal("columns", "9");
			attrs[i++] = new KeyVal("rows", "1");
		}
		#endregion

		#region Internal Class

		internal class XMLElementNew : XElement
		{
			#region Constructors
			internal XMLElementNew(IPinReceipt rcpt, IWireless_Products wp)
			{
				name = "new";
				
				SetAttrs(rcpt, wp);				
			}
			internal XMLElementNew(string msg)
			{
				name = "new";
				
				SetAttrs(msg);				
			}
			#endregion

			#region Implementations
			void SetAttrs(IPinReceipt rcpt, IWireless_Products wp)
			{
				attrs = new KeyVal[8];
				
				int i = 0;
				attrs[i++] = new KeyVal("commission", rcpt.Commission.ToString());
				attrs[i++] = new KeyVal("errorcode", Const.WS_SUCCESSCODE);
				attrs[i++] = new KeyVal("message", "");
				attrs[i++] = new KeyVal("pin", rcpt.Pin);				
				attrs[i++] = new KeyVal("price", wp.Price.ToString());
				attrs[i++] = new KeyVal("productid", wp.Wireless_product_id.ToString());
				attrs[i++] = new KeyVal("receipt_text", wp.Receipt_text);
				attrs[i++] = new KeyVal("trconfirm", rcpt.ConfNum);				
			}
			void SetAttrs(string msg)
			{
				attrs = new KeyVal[3];
				
				int i = 0;
				attrs[i++] = new KeyVal("errorcode", Const.WS_ERRORCODE);
				attrs[i++] = new KeyVal("message", msg);
				attrs[i++] = new KeyVal("transactiontime", DateTime.Now.ToString("dd-MMM-yy HH:mm tt"));
			}
			#endregion
		}

		#endregion
	}	
	
	public class PostDPIPaymentRequest : XElement
	{
		public PostDPIPaymentRequest(IRWPostArgs args)
		{
			name = "dpiWSData";
			attrs = SetAttrs();
			SetupElmnts(args);
		} 

		/*		Implementation		*/
		void SetupElmnts(IRWPostArgs args)
		{
			children = new XElement[3];
			int i = 0;

			children[i++] = new XEDpiResult(args);
			children[i++] = new XEPaymentTypes(args);
			children[i++] = new XEStoreInfo(args.User.Token, args.User.StoreNumber.Trim());
		}
		KeyVal[] SetAttrs()
		{
			KeyVal[] attrs = new KeyVal[1];

			int i = 0;
			attrs[i++] = new KeyVal("xmlns", "http://rentway.com/dpiResult.xsd");

			return attrs;
		}
	}
	public class ReverseDPIPaymentRequest : XElement
	{
		public ReverseDPIPaymentRequest(IRWPostArgs args)
		{
			name = "dpiWSData";
			SetupElmnts(args);
		}

		/*		Implementation		*/
		void SetupElmnts(IRWPostArgs args)
		{
			children = new XElement[2];
			int i = 0;

			children[i++] = new XEDpiRevResult(args);
			children[i++] = new XEStoreInfo(args.User.Token, args.User.StoreNumber.Trim());
		}
	}


#endregion

#region Elements	
	public class XEDpiResult : XElement
	{
		public XEDpiResult(IRWPostArgs args)
		{
			name = "dpiResult";

			children = new XElement[7];
			int i = 0;

			children[i++] = new XElement("transactionId", args.PayInfo.Id.ToString());		
			
			if (args.PayInfo.VFConf == null)
				children[i++] = new XElement("confirmation", args.PayInfo.Id.ToString());
			else
				children[i++] = new XElement("confirmation", args.PayInfo.VFConf);
			
			children[i++] = new XElement("salesCommissionAmount", args.AmtComm.ToString());
			children[i++] = new XElement("totalSaleAmount", args.AmtPaid.ToString());
			children[i++] = new XElement("serviceType", args.ServType);
			children[i++] = new XElement("taxAmount", args.AmtTaxes.ToString());
			children[i++] = new XElement("receiptId", args.ReceitId); 
		}
	}
	public class XEDpiRevResult : XElement
	{
		public XEDpiRevResult(IRWPostArgs args)
		{
			name = "dpiResult";

			children = new XElement[2];
			int i = 0;

			children[i++] = new XElement("transactionId", args.PayInfo.Id.ToString());		
			children[i++] = new XElement("confirmation", args.PayInfo.VFConf);			
		}
	}
	public class XEPaymentTypes : XElement
	{
		public XEPaymentTypes(IRWPostArgs args)
		{
			name = "paymentTypes";

			children = new XElement[4];
			int i = 0;

			children[i++] = new XElement("paymentType",  args.PymtType);  
			children[i++] = new XElement("paymentValue", args.PayInfo.TotalAmountPaid.ToString());

			children[i++] = new XElement("paymentAddt0", args.PayInfo.CheckNumber == null ? "" : args.PayInfo.CheckNumber);	
			children[i++] = new XElement("paymentAddt1", args.PayInfo.CheckName == null ? "" : args.PayInfo.CheckName);		
		}	}
	public class XEStoreInfo : XElement
	{

		public XEStoreInfo(string token, string storeNumber)
		{
			name = "storeInfo";

			children = new XElement[2];
			int i = 0;

			children[i++] = new XElement("storeId", storeNumber);
			children[i++] = new XElement("token", token);			
		}
	}
	
	public class XEDpiTransactionList : XElement
	{

		public XEDpiTransactionList()
		{
			name = "dpiTransactionList";

			children = new XElement[2];
			int i = 0;

			children[i++] = new XElement("dpiPassword", "2");
			children[i++] = new XElement("transactionDate", "6758921");			
		}
	}
	
#endregion
}