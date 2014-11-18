using System;
using System.Collections;
using System.Xml;

using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]  
	public class PinReceipt : Receipt, IPinReceipt
	{
	#region Data
		//protected string   pin;
		protected decimal  commission;
		protected string   receipt_Text;
		protected DateTime transactionTime;
	#endregion
	
	#region Properties
//		public string   Pin 			
//		{ 
//			get { return pin; }
//			set { pin = value; }
//		}
		public decimal  Commission		{ get { return commission; }}
		public DateTime TransactionTime	{ get { return transactionTime; }}
		public string   Receipt_Text	
		{
			get { return receipt_Text; }
			set { receipt_Text = value; }
		}
		public virtual DictionaryEntry[] Entries { get { return new DictionaryEntry[0]; }}
	#endregion

	#region Constructors
		public PinReceipt() : base() 
		{
		}
		public PinReceipt(XmlDocument doc, IWireless_Products wp) : this()
		{
			XmlNodeList list = doc.GetElementsByTagName("new");

			if (list.Count == 0)
				throw new ApplicationException("No pins found for Product " + wp.Wireless_product_id.ToString()
								+ ", " + wp.Product_name);

			if (!list[0].Attributes.GetNamedItem("errorcode").Value.Equals("A"))
				throw new Exception("Error ordering wireless product: " + wp.Wireless_product_id.ToString());

			this.confNum         = list[0].Attributes.GetNamedItem("trconfirm").Value;
			this.pin             = list[0].Attributes.GetNamedItem("pin").Value;
			this.commission      = Convert.ToDecimal(list[0].Attributes.GetNamedItem("commission").Value);
			this.receipt_Text    = list[0].Attributes.GetNamedItem("receipt_text").Value;
			this.transactionTime = Convert.ToDateTime(list[0].Attributes.GetNamedItem("transactiontime").Value);
		}

		public PinReceipt(XmlDocument doc, IPinProduct pinProduct) : this()
		{
			XmlNodeList list = doc.GetElementsByTagName("new");

			if (list.Count == 0)
				throw new ApplicationException("No pins found for Product " + pinProduct.Product_Id.ToString()
					+ ", " + pinProduct.Product_Name);

			if (!list[0].Attributes.GetNamedItem("errorcode").Value.Equals("A"))
				throw new Exception("Error ordering wireless product: " + pinProduct.Product_Id.ToString());

			this.confNum         = list[0].Attributes.GetNamedItem("trconfirm").Value;
			this.pin             = list[0].Attributes.GetNamedItem("pin").Value;
			this.commission      = Convert.ToDecimal(list[0].Attributes.GetNamedItem("commission").Value);
			this.receipt_Text    = list[0].Attributes.GetNamedItem("receipt_text").Value;
			this.transactionTime = Convert.ToDateTime(list[0].Attributes.GetNamedItem("transactiontime").Value);
		}

		public PinReceipt(string _confNum,
							string _pin,
							decimal _commission,
							string _receipt_Text,
							DateTime _transactionTime) : base(_confNum)
		{
			confNum = _confNum;
			this.pin = _pin;
			this.commission = _commission;
			this.receipt_Text = _receipt_Text;
			this.transactionTime = _transactionTime;
		}
		public PinReceipt(string _pin, decimal _commission, string _receipt_Text) : base()
		{
			this.pin = _pin;
			this.commission = _commission;
			this.receipt_Text = _receipt_Text;
		}
	#endregion
	}
}