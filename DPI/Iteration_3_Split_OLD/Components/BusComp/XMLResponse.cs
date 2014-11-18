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
#region Purpose Responses
	public class PurposeDCResponse : IDebitCardResponse
	{
		#region Data

		string xnode;
		string casenum;
		string date;
		string mid;
		string reasoncode;
		string respcode;
		string resptext;
		int stan;
		string tid;
		string name;
		string tran;
		string tranid;
		
		#endregion
		
		#region Properties

		public string Casenum		{ get { return casenum;		} }
		public string Date			{ get { return date;		} }
		public string Mid			{ get { return mid;			} }
		public string ReasonCode	{ get { return reasoncode;	} }
		public string RespCode		{ get { return respcode;	} }
		public string RespText		{ get { return resptext;	} }
		public int Stan				{ get { return stan;		} }
		public string Tid			{ get { return tid;			} }
		public string Name			{ get { return name;		} }
		public string Tran			{ get { return tran;		} }	
		public string TranId		{ get { return tranid;		} }
		public string Xnode			{ get { return xnode;		} }
		
		#endregion

		#region Constructors

		public static PurposeDCResponse XmlErrorResp(string step)
		{
			PurposeDCResponse pr = new PurposeDCResponse();
			pr.respcode = "99";  
			pr.resptext = step + " web method. Failed xml validation";

			return pr;
		}
		PurposeDCResponse() {}
		public PurposeDCResponse(XmlNode xNode)
		{
			xnode = xNode.OuterXml;
			XmlTextReader xReader = new XmlTextReader(xNode.OuterXml, XmlNodeType.Element, null);
			while (xReader.Read())
				if (xReader.NodeType == XmlNodeType.Element)
					while (xReader.MoveToNextAttribute())
					{
						switch (xReader.Name.Trim().ToLower())
						{
							case "casenum" :
								casenum = xReader.Value;
								break;
							case "date" :
								date = xReader.Value;
								break;
							case "mid" :
								mid = xReader.Value;
								break;
							case "reasoncode" :
								reasoncode = xReader.Value;
								break;
							case "respcode" :
								respcode = xReader.Value;
								break;
							case "resptext" :
								resptext = xReader.Value;
								break;
							case "Stan" :
								stan = int.Parse(xReader.Value);
								break;
							case "tid" :
								tid = xReader.Value;
								break;
							case "name" :
								name = xReader.Value;
								break;
							case "tran" :
								tran = xReader.Value;
								break;
							case "tranid" :
								tranid = xReader.Value;
								break;
						}
					}
		}
		#endregion
	}
#endregion

#region Rentway Responses

	public class PostDPIPaymentResponse
	{
		#region Data

		int transactionId;
		int confirmationNumber;
		string error;
		
		#endregion
		
		#region Properties

		public int TransactionId		{ get { return transactionId;		} }
		public int ConfirmationNumber	{ get { return confirmationNumber;	} }
		public string Error				{ get { return error;				} }		
		
		#endregion

		#region Constructors

		public static PostDPIPaymentResponse XmlErrorResp(string step)
		{
			PostDPIPaymentResponse rr = new PostDPIPaymentResponse();
			rr.error = step + " web method. Failed xml validation";

			return rr;
		}
		PostDPIPaymentResponse() {}
		public PostDPIPaymentResponse(XmlNode xNode)
		{
			XmlTextReader xReader = new XmlTextReader(xNode.OuterXml, XmlNodeType.Element, null);
			while (xReader.Read())
				if (xReader.NodeType == XmlNodeType.Element)
					switch (xReader.Name.Trim().ToLower())
					{
						case "transactionId" :
						{
							transactionId = int.Parse(xReader.Value);
							break;
						}
						case "confirmationNumber" :
						{
							confirmationNumber = int.Parse(xReader.Value);
							break;
						}
						case "error" :
						{
							error = xReader.Value;
							break;						
						}
					}					
		}
		#endregion
	}
	public class ReverseDPIPaymentResponse
	{
		#region Data

		int transactionId;
		int confirmationNumber;
		string error;
		
		#endregion
		
		#region Properties

		public int TransactionId		{ get { return transactionId;		} }
		public int ConfirmationNumber	{ get { return confirmationNumber;	} }
		public string Error				{ get { return error;				} }		
		
		#endregion

		#region Constructors

		public static ReverseDPIPaymentResponse XmlErrorResp(string step)
		{
			ReverseDPIPaymentResponse rr = new ReverseDPIPaymentResponse();
			rr.error = step + " web method. Failed xml validation";

			return rr;
		}
		ReverseDPIPaymentResponse() {}
		public ReverseDPIPaymentResponse(XmlNode xNode)
		{
			XmlTextReader xReader = new XmlTextReader(xNode.OuterXml, XmlNodeType.Element, null);
			while (xReader.Read())
				if (xReader.NodeType == XmlNodeType.Element)
					switch (xReader.Name.Trim().ToLower())
					{
						case "transactionId" :
							transactionId = int.Parse(xReader.Value);
							break;
						case "confirmationNumber" :
							confirmationNumber = int.Parse(xReader.Value);
							break;
						case "error" :
							error = xReader.Value;
							break;						
					}					
		}
		#endregion
	}

	public class DailyDPITransactionsResponse
	{
	#region Data
		int transactionId;
		int confirmation;
		decimal salesCommissionAmount;
		decimal totalSaleAmount;
		string serviceType;
		decimal taxAmount;
		int receiptId;		
	#endregion
		
	#region Properties
		public int		TransactionId			{ get { return transactionId;			}}
		public int		Confirmation			{ get { return confirmation;			}}
		public decimal  SalesCommissionAmount	{ get { return salesCommissionAmount;	}}
		public decimal	TotalSaleAmount			{ get { return totalSaleAmount;			}}
		public string	ServiceType				{ get { return serviceType;				}}
		public decimal	TaxAmount				{ get { return taxAmount;				}}
		public int		ReceiptId				{ get { return receiptId;				}}
	#endregion

	#region Constructors		
		DailyDPITransactionsResponse() {}
		public DailyDPITransactionsResponse(XmlNode xNode)
		{			
			XmlTextReader xReader = new XmlTextReader(xNode.OuterXml, XmlNodeType.Element, null);
			while (xReader.Read())
				if (xReader.NodeType == XmlNodeType.Element)
					switch (xReader.Name.Trim().ToLower())
					{
						case "transactionid" :
							transactionId = int.Parse(xReader.Value);
							break;
						case "confirmation" :
							confirmation = int.Parse(xReader.Value);
							break;
						case "salescommissionamount" :
							salesCommissionAmount = decimal.Parse(xReader.Value);
							break;						
						case "totalsaleamount" :
							totalSaleAmount = decimal.Parse(xReader.Value);
							break;												
						case "servicetype" :
							serviceType = xReader.Value;
							break;						
						case "taxamount" :
							taxAmount = decimal.Parse(xReader.Value);
							break;		
						case "receiptid" :
							receiptId = int.Parse(xReader.Value);
							break;
					}					
		}
	#endregion
	}
	
#endregion

#region SlingShot Responses
	public class SlingShotResp : ISlingShotResp
	{
	#region Data
		string	message;
		string	errorDetail;
		string	aCode;
		int		manId;
		int		code;
		string	tranId;
		string	action;
		string	ver;
		IPinProduct[] pinProducts;
	#endregion

	#region Properties
		public string Message	  { get { return message;  }}
		public string ErrorDetail { get { return errorDetail; }}
		public string ACode       { get { return aCode;  }}
		public int	  ManId       {	get { return manId;  }}
		public int	  Code        { get { return code;  }}
		public string TranId	  { get { return tranId;  }}
		public string Action	  { get { return action;  }}
		public string Ver	      { get { return ver;  }}
		public IPinProduct[] PinProducts { get { return pinProducts; }}
	#endregion

	#region Constructors
		public SlingShotResp(XmlNode xNode)
		{
			SetElementsAttrs(new XmlTextReader(xNode.OuterXml, XmlNodeType.Element, null));
			SetProducts(xNode);
		}		
	#endregion

	#region Implementation
		void SetElementsAttrs(XmlTextReader xReader)
		{
			while (xReader.Read())
				if (xReader.NodeType == XmlNodeType.Element)
				{
					switch (xReader.Name.Trim().ToLower())
					{
						case "message" :
							message = xReader.ReadInnerXml();
							break;
						case "errordetail" :
							errorDetail = xReader.ReadInnerXml();
							break;						
					}
					while (xReader.MoveToNextAttribute())
						switch (xReader.Name.Trim().ToLower())
						{
							case "manid" :
							{
								manId = int.Parse(xReader.Value);
								break;
							}
							case "code" :
							{
								code = int.Parse(xReader.Value);
								break;
							}
							case "tranid" :
							{
								tranId = xReader.Value;
								break;
							}
							case "action" :
							{
								action = xReader.Value;
								break;
							}
							case "acode" :
							{
								aCode = xReader.Value;
								break;
							}
							case "ver" :
							{
								ver = xReader.Value;
								break;
							}
						}					
				}
		}
		void SetProducts(XmlNode xNode)
		{
			XmlDocument xDoc = new XmlDocument();
			xDoc.LoadXml(xNode.OuterXml);
			XmlNodeList xList = xDoc.GetElementsByTagName("sku");

			ArrayList ar = new ArrayList();
			for (int i = 0; i < xList.Count; i++)
				ar.Add(AddProduct(xList[i]));

			pinProducts = new PinProduct[ar.Count];
			ar.CopyTo(pinProducts);
		}
		IPinProduct AddProduct(XmlNode xNode)
		{
			IPinProduct pin = new PinProduct();
			
			pin.Unlimited		= int.Parse(xNode.Attributes["unlimited"].Value);
			pin.Upc				= xNode.Attributes["upc"].Value;
			pin.Product_Id		= 1;
			
			pin.Product_Name	= xNode.Attributes["name"].Value;
			pin.Price			= decimal.Parse(xNode.Attributes["price"].Value);
			pin.Expiration		= pin.Unlimited == 1 ? xNode.Attributes["days"].Value: "";			
			
			return pin;
		}
	#endregion
	}	
#endregion
}