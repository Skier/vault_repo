using System;

using DPI.Interfaces;
 
namespace DPI.Components
{
	public class NewOrderDTO : INewOrderDTO
	{
		OrderType orderType;
		IAddr mailAddr;
		IAddr svcAddr; 
		ICustInfo2 cust;
		IDemand dmd;
		string zipcode;
		string iLEC;
		string transNum;
		IPayInfo payInfo;			
		IReceipt receipt;


		public OrderType OrderType 
		{ 
			get { return orderType; } 
			set {orderType = value; }
		}
		public IAddr MailAddr
		{
			get { return mailAddr;  } 
			set { mailAddr = value; }
		}
		public IAddr SvcAddr 
		{
			get { return svcAddr;  } 
			set { svcAddr = value; }
		}
		public ICustInfo2 Cust
		{
			get { return cust;  } 
			set { cust = value; }
		}
		public IDemand Dmd		
		{
			get { return dmd;  } 
			set { dmd = value; }
		}
		public string Zipcode
		{
			get { return zipcode;  } 
			set { zipcode = value; }
		}
		public string ILEC
		{
			get { return iLEC;  } 
			set { iLEC = value; }
		}
		public string TransNum
		{
			get { return transNum;  } 
			set { transNum = value; }
		}
		public IPayInfo PayInfo
		{
			get { return payInfo;  } 
			set { payInfo = value; }
		}
		public IReceipt Receipt
		{
			get { return receipt;  } 
			set { receipt = value; }
		}
	}
}			