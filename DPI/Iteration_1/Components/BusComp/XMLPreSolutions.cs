using System;
using System.Xml;

using DPI.Interfaces;

namespace DPI.Components
{

	public class XMLPurchaseReq : XElement
	{

		#region Constructors
		public XMLPurchaseReq(string soc, string storeNumber)
		{
			name = "message";
			SetMsgAttrs(storeNumber);
			SetupElmnts(soc);
		}		
		#endregion

		#region	Implementation
		void SetMsgAttrs(string storeNumber)
		{
			attrs = new KeyVal[5];

			int i = 0;
			attrs[i++] = new KeyVal("id", "0");
			attrs[i++] = new KeyVal("name", Const.PRE_PURCHASE_REQ);
			attrs[i++] = new KeyVal("source", Const.PRE_SOURCE);
			attrs[i++] = new KeyVal("storenumber", storeNumber);
			attrs[i++] = new KeyVal("version", Const.PRE_VERSION);			
		}
		void SetupElmnts(string soc)
		{
			children = new XElement[5];

			int i = 0;
			//children[i++] = new XElement("PartnerDescription", Const.PRE_SOURCE);
			children[i++] = new XMLPINRequest(soc);
			children[i++] = new XElement("FulfillAllBatch", "1");
			children[i++] = new XElement("FulFillAllPin", "1");
			children[i++] = new XElement("AutoConfirmation", "1");
			children[i++] = new XElement("DateTimeStamp", DateTime.Now.ToString("G"));		
		}		
		#endregion

		#region Internal Class
		internal class XMLPINRequest : XElement
		{
			#region Constructors
			internal XMLPINRequest(string soc)
			{
				name = "PINRequest";			
				SetElements(soc);		
			}
			
			#endregion

			#region Implementations
			void SetElements(string soc)
			{
				children = new XElement[4];

				int i = 0;
				children[i++] = new XElement("LineNumber", "1");
				children[i++] = new XElement("ProductCode", soc);
				children[i++] = new XElement("SendReceiptText", "1");
				children[i++] = new XElement("Count", "1");
			}
			#endregion
		}

		#endregion
	}	
	

}