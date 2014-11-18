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
	public class XmlEleEnrollReq : XePurpose//XElement
	{
		public XmlEleEnrollReq(IPayInfo payInfo, ICardApp app)
		{
			name = "request";
			attrs = PurposeAttr.MakeAttr(payInfo, "ENROLL");
			SetupElmnts(payInfo, app);			
		} 
		/*		Implementation		*/
		void SetupElmnts(IPayInfo payInfo, ICardApp app)
		{
			children = new XElement[5];
			int i = 0;

			children[i++] = new XMLElementAmts(payInfo.ParDemand);
			children[i++] = new XMLElementCH(app, payInfo.ParDemand.Consumer);
			children[i++] = new XMLElementIdData(payInfo.ParDemand.Consumer, app);
			children[i++] = new XMLElementIdMode();
			children[i++] = new XMLElementCard(app);			
		}
	}
	public class XmlEleReloadReq : XePurpose //XElement
	{
		public XmlEleReloadReq(IPayInfo payInfo, ICardApp app)
		{
			name = "request";
			attrs = PurposeAttr.MakeAttr(payInfo, "RELOAD");
			SetupEles(payInfo, app);			
		}
		/*		Implementation		*/
		void SetupEles(IPayInfo payInfo, ICardApp app)
		{
			children = new XElement[2];
			int i = 0;

			children[i++] = new XMLElementAmts(payInfo.ParDemand);
			children[i++] = new CardReloadXml(app);			
		}
	}
	public class XmlEleRevReq : XePurpose //XElement
	{
		public XmlEleRevReq(XElement revReq, IPayInfo payInfo)
		{
			name = "request";
			attrs = PurposeAttr.MakeAttr(payInfo, "REVERSAL");
			SetupEles(revReq);			
		}
		/*		Implementation		*/
		void SetupEles(XElement revReq)
		{
			children = new XElement[1];
			int i = 0;
			children[i++] = revReq;			
		}
	}
	public class PurposeAttr
	{
		static internal KeyVal[] MakeAttr(IPayInfo payInfo, string request)
		{
			KeyVal[] attrs = new KeyVal[5];
			
			int i = 0;
			attrs[i++] = new KeyVal("tran",    request);		
			attrs[i++] = new KeyVal("tid",     payInfo.ParDemand.StoreCode);
			attrs[i++] = new KeyVal("mid",     XePurpose.purposeMid);	
			attrs[i++] = new KeyVal("clerkid", payInfo.ParDemand.ConsumerAgent);
			attrs[i++] = new KeyVal("stan",    payInfo.Id.ToString());

			return attrs;
		}
	}

#region Elements
	
	public class XMLElementAmts : XElement
	{
		public XMLElementAmts(IDemand dmd)
		{
			name = "amts";
			IOrderSum sum = dmd.OrderSummary(null);

			attrs = new KeyVal[2];
			int i = 0;

			attrs[i++] = new KeyVal("total", Money2.ToPennies(sum.GetProdAmt())); // per barbara 7/28/05
						                   //Money2.ToPennies(sum.GetTotalAmtDue())); 
			attrs[i++] = new KeyVal("fee", "00"); // per barbara 7/28/05
			                              //Money2.ToPennies(sum.GetFeeAmt())); 
		}
	}
	public class CardReloadXml : XElement
	{
		public CardReloadXml(ICardApp app)
		{
			name = "card";
			attrs = new KeyVal[2];
			int i = 0;

			attrs[i++] = new KeyVal("entered",  "MANUAL");
			attrs[i++] = new KeyVal("pan", app.CardNum);
		}
	}
	public class XMLElementCard : XElement
	{
		public XMLElementCard(ICardApp app)
		{
			name = "card";
			attrs = new KeyVal[2];
			int i = 0;

			attrs[i++] = new KeyVal("entered",  "MANUAL");
		//	attrs[i++] = new KeyVal("expd", "");
			attrs[i++] = new KeyVal("pan", app.CardNum);
		//	attrs[i++] = new KeyVal("trk2", "");//"12342563783663636");
		}
	}
	public class XMLElementCH : XElement
	{
		public XMLElementCH(ICardApp app, ICustInfo2 ci)
		{
			name = "ch";		

			attrs = new KeyVal[5];
			if (app.PrevCard != null)
				attrs = new KeyVal[6];

			int i = 0;

			attrs[i++] = new KeyVal("attempts", "1");
			attrs[i++] = new KeyVal("coaflag",  "1");
	
			if (app.PrevCard != null)
				attrs[i++] = new KeyVal("compbin",  app.PrevCard);

			attrs[i++] = new KeyVal("language", "EN");
			attrs[i++] = new KeyVal("ph",       ci.PhNumber);
			attrs[i++] = new KeyVal("ssn",      ci.Ssn);		

		}
		public XMLElementCH()
		{
			name = "ch";		
				
			attrs = new KeyVal[1];
			int i = 0;

			attrs[i++] = new KeyVal("attempts",  "1");			
		}
		
	}
	public class XMLElementIdData : XElement
	{
		public XMLElementIdData(ICustInfo2 ci, ICardApp app)
		{
			name = "iddata";		
			if (ci.MailAddr == null)
				throw new ArgumentException("Cust Mail address is required");				

			attrs = new KeyVal[9];
			int i = 0;

			attrs[i++] = new KeyVal("accuracy",  "0");
			attrs[i++] = new KeyVal("dob",       ci.Dob.ToString("MM-dd-yyyy"));
			attrs[i++] = new KeyVal("namef",     ci.FirstName);
			attrs[i++] = new KeyVal("namel",     ci.LastName);

			attrs[i++] = new KeyVal("addr1",     ci.MailAddr.FormattedStreetAddress);
			attrs[i++] = new KeyVal("city",      ci.MailAddr.City);
			attrs[i++] = new KeyVal("state",     ci.MailAddr.State);
			attrs[i++] = new KeyVal("zip",       ci.MailAddr.Zipcode);
			attrs[i++] = new KeyVal("verified",  app.Verified ? "1" : "0");
		}	
	}

	public class XMLElementIdMode : XElement
	{
		public XMLElementIdMode()
		{
			name = "idmode";		
				
			attrs = new KeyVal[11];
			int i = 0;

			attrs[i++] = new KeyVal("accuracy",  "S");
			attrs[i++] = new KeyVal("addr1",  "S");
			attrs[i++] = new KeyVal("city",  "E");
			attrs[i++] = new KeyVal("dob",  "E");
			attrs[i++] = new KeyVal("docno",  "E");
			attrs[i++] = new KeyVal("expd",  "E");
			attrs[i++] = new KeyVal("namef",  "E");
			attrs[i++] = new KeyVal("namel",  "E");
			attrs[i++] = new KeyVal("namem",  "E");
			attrs[i++] = new KeyVal("state",  "E");
			attrs[i++] = new KeyVal("zip",  "E");
		}
	}
	public class XMLElementVendorId  : XElement
	{
		public XMLElementVendorId()
		{
			name = "vendorid";		
				
			attrs = new KeyVal[1];
			int i = 0;

			attrs[i++] = new KeyVal("name",  "DPI");
		}
	}
#endregion
}