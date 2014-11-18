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


	public class XMLPurpose : XMLMessage
	{
	#region Enums

		protected enum RequestAttr { clerkid, mid, stan, tid, tran }
		protected enum AmountsAttr { fee, total }
		protected enum CardAttr { entered, expd, pan, trk2 }
		protected enum CardHolderAttr { attempts, coaflag, compbin, language, microloan, ph, ssn }
		protected enum IdDataAttr { accuracy, dob, docno, expd, namef, namel, namem, addr1, city, state, zip }
		protected enum IdModeAttr { accuracy, addr1, city, dob, docno, expd, namef, namel, namem, state, zip, }
		protected enum ResponseAttr { casenum, date, reasoncode, respcode, resptext, stan, tid, tran, tranid }
		protected enum Elements { request, amts, card, ch, iddata, idmode, pin, pinconfirm, response }

	#endregion

	#region Elements
		protected class XMLElementRequest 
		{
			#region Data

			string clerkid;
			string mid;
			string stan;
			const string tid = "abcd";
			const string tran = Const.ENROLL;

			#endregion
	
			#region Properties

			public string Clerkid	
			{ 
				get { return clerkid;	}
				set { clerkid = value;	}
			}
			public string Mid
			{
				get { return mid;		}
				set { mid = value;		}
			}
			public string Tid  { get { return tid;  }}
			public string Stan { get { return stan; }}
			public string Tran { get { return tran;	}}


			#endregion
	
			#region Constructors

			public XMLElementRequest(string xml)
			{
				// <Request clerkid="1" mid="1234" stan="0005678" tid="345567" tran="enroll">
				// becomes this:
				// <Request clerkid="1" mid="1234" stan="0005678" tid="345567" tran="enroll"> </request>"
				string wellFormed = xml + " </request>";
			}
			public XMLElementRequest()
			{
			}
			public XMLElementRequest(IDemand dmd)
			{
				Setup(dmd);
			}
		
			#endregion
			
			#region Methods

			public void Setup(IDemand dmd)
			{
				clerkid = dmd.ConsumerAgent;
				mid = dmd.StoreCode;
			}
		

			#endregion

		}

		protected class XMLElementAmts
		{
			#region Data

			string fee;
			string total;
			string balance;
		
			#endregion
	
			#region Properties
			public string Fee	
			{ 
				get { return fee;	}
				set { fee = value;	}
			}
			public string Total
			{
				get { return total;		}
				set { total = value;		}
			}
			public string Balance
			{
				get { return balance;		}
				set { balance = value;		}
			}
	
			#endregion

			#region Constructors

			public XMLElementAmts(string xml)
			{
				string wellFormed = xml + " </request>";
			}
			public XMLElementAmts()
			{
			}
			public XMLElementAmts(IOrderSum sum)
			{
				Setup(sum);
			}
		
			#endregion
			
			#region Methods

			public void Setup(IOrderSum sum)
			{
				int mon = 1;
				total =sum.GetTotalAmtDue(mon).ToString();
				fee = sum.GetFeeAmt(mon).ToString();
			}

			#endregion

		}

		protected class XMLElementCard
		{
			#region Data

			const string entered = "MANUAL";
			string expd;
			string pan;
			string trk2;

			#endregion
	
			#region Properties

			public string Entered { get { return entered; }}
			public string Expd
			{
				get { return expd;		}
				set { expd = value;		}
			}
			public string Pan
			{
				get { return pan;		}
				set { pan = value;		}
			}
			public string Trk2
			{
				get { return trk2;		}
				set { trk2 = value;		}
			}

			#endregion

			#region Constructors
			public XMLElementCard(string xml)
			{
				string wellFormed = xml + " </request>";
			}
			public XMLElementCard()
			{
			}
			public XMLElementCard(IDebCardApp app)
			{
				Setup(app);
			}
		
			#endregion
			
			#region Methods

			public void Setup(IDebCardApp app)
			{
				pan = app.CardNum.ToString();
			}
		

			#endregion

		}

		protected class XMLElementCH
		{
			#region Data

			const string attempts = "1";
			const string coaflag = "1";
			string compbin;
			string ph;
			string ssn;

			#endregion
	
			#region Properties

			public string Attempts	{ get { return attempts; }}
			public string Coaflag   { get { return coaflag;    }}
			public string Compbin
			{
				get { return compbin;		}
				set { compbin = value;		}
			}
			public string Ph
			{
				get { return ph;		}
				set { ph = value;		}
			}
			public string Ssn
			{
				get { return ssn;		}
				set { ssn = value;		}
			}

			#endregion

			#region Constructors
			public XMLElementCH(string xml)
			{
				string wellFormed = xml + " </request>";
			}
			public XMLElementCH()
			{
			}
			public XMLElementCH(IDebCardApp app, ICustInfo2 ci)
			{
				Setup(app, ci);
			}
		
			#endregion
			
			#region Methods

			public void Setup(IDebCardApp app, ICustInfo2 ci)
			{
				compbin = app.PrevCard;
				ph = ci.Contact;
				ssn = ci.Ssn;
			}
		

			#endregion

		}

		protected class XMLElementIdData
		{
			#region Data

			string accuracy;
			string dob;
			string docno;
			string expd;
			string namef;
			string namel;
			string namem;
			string addr1;
			string city;
			string state;
			string zip;

			#endregion
	
			#region Properties

			public string Accuracy	
			{ 
				get { return accuracy;	}
				set { accuracy = value;	}
			}
			public string Dob
			{
				get { return dob;		}
				set { dob = value;		}
			}
			public string Docno
			{
				get { return docno;		}
				set { docno = value;		}
			}
			public string Expd
			{
				get { return expd;		}
				set { expd = value;		}
			}
			public string Namef
			{
				get { return namef;		}
				set { namef = value;		}
			}
			public string Namel
			{
				get { return namel;		}
				set { namel = value;		}
			}
			public string Namem
			{
				get { return namem;		}
				set { namem = value;		}
			}
			public string Addr1
			{
				get { return addr1;		}
				set { addr1 = value;		}
			}
			public string City
			{
				get { return city;		}
				set { city = value;		}
			}
			public string State
			{
				get { return state;		}
				set { state = value;		}
			}
			public string Zip
			{
				get { return zip;		}
				set { zip = value;		}
			}

			#endregion

			#region Constructors
			public XMLElementIdData(string xml)
			{
				string wellFormed = xml + " </request>";
			}
			public XMLElementIdData()
			{
			}
			public XMLElementIdData(ICustInfo2 ci)
			{
				Setup(ci);
			}
		
			#endregion
			
			#region Methods

			public void Setup(ICustInfo2 ci)
			{
				dob = ci.Dob.ToShortDateString();
				docno = ci.IDNumber;
				expd = ci.IDExpirationDate.ToShortDateString();
				namef = ci.FirstName;
				namel = ci.LastName;
				if (ci.MailAddr == null)
				{
					addr1 = ci.MailAddr.FormattedStreetAddress;
					city = ci.MailAddr.City;
					state = ci.MailAddr.State;
					zip = ci.MailAddr.Zipcode;
				}
			}
		

			#endregion


		}

		protected class XMLElementIdMode
		{

			#region Data

			const string accuracy = "M";
			const string addr1 = "M";
			const string city = "M";
			const string dob = "M";
			const string docno = "M";
			const string expd = "M";
			const string namef = "M";
			const string namel = "M";
			const string namem = "M";
			const string state = "M";
			const string zip = "M";

			#endregion
	
			#region Properties

			public string Accuracy	{ get { return accuracy;}}
			public string Addr1		{ get { return addr1;	}}
		
			public string City		{ get { return city;	}}
			public string Dob		{ get { return dob;	}}
			public string Docno		{ get { return docno;	}}
			public string Expd		{ get { return expd;	}}
			public string Namef		{ get { return namef;	}}
			public string Namel		{ get { return namel;	}}
			public string Namem		{ get { return namem;	}}
			public string State		{ get { return state;	}}
			public string Zip		{ get { return zip;	}}

			#endregion

			public XMLElementIdMode()
			{
			}

		}

		protected class XMLElementResponse
		{
			#region Data

			string casenum;
			string date;
			string reasoncode;
			string respcode;
			string resptext;
			string stan;
			string tid;
			string tran;
			string tranid;

			#endregion
	
			#region Properties

			public string Casenum	
			{ 
				get { return casenum;	}
				set { casenum = value;	}
			}
			public string Date
			{
				get { return date;		}
				set { date = value;		}
			}
			public string Reasoncode
			{
				get { return reasoncode;		}
				set { reasoncode = value;		}
			}
			public string Respcode
			{
				get { return respcode;		}
				set { respcode = value;		}
			}
			public string Resptext
			{
				get { return resptext;		}
				set { resptext = value;		}
			}
			public string Stan
			{
				get { return stan;		}
				set { stan = value;		}
			}
			public string Tid
			{
				get { return tid;		}
				set { tid = value;		}
			}
			public string Tran
			{
				get { return tran;		}
				set { tran = value;		}
			}
			public string Tranid
			{
				get { return tranid;		}
				set { tranid = value;		}
			}		

			#endregion

		}

	#endregion

	#region Methods		
		protected void WriteElementRequest(KeyVal[] pars)
		{			
			StartXmlDocument(Elements.request.ToString(), pars);			
		}
		// IDemand dmd, DebitCardApp app or IPaymentInfo3 payInfo, DebitCardApp app
		protected XmlDocument GetEnrollReqMsg(string storeCode, string clerkId, ICustInfo2 custInfo, DebitCardApp app, IPaymentInfo2 payInfo)
		{
			
			StartXmlDocument();
			WriteElementRequest(storeCode, clerkId);
			WriteElementAmts(payInfo);
			WriteElementCard(app);
			WriteElementCH(custInfo, app);
			WriteElementIdData(custInfo);
			WriteElementIdMode();
			EndXmlDocument();

			return XmlDocumentMessage;
		}

	
	#endregion
		
		#region Implementation

//		void CreateMessage(IProdPrice debitCard, IDemand dmd, ICustInfo2 custInfo, 
//			IProdPrice[] fees, IPaymentInfo2 payInfo,  
//			DebitCardApp app, string storeCode, string clerkId)
//		{						
//			
//			this.StartXmlDocument(Elements.request.ToString(), GetReqAtrNames(), GetReqAtrVals(storeCode, clerkId));
//			this.EndXmlDocument();
//		}
		void WriteElementRequest(string storeCode, string clerkId)
		{
			//			XmlWriter.WriteStartElement(Elements.request.ToString());
			//
			//			XmlWriter.WriteAttributeString(RequestAttr.clerkid.ToString(), user.ClerkId);
			//			XmlWriter.WriteAttributeString(RequestAttr.mid.ToString(), user.StoreCode);
			//			XmlWriter.WriteAttributeString(RequestAttr.tid.ToString(), user.StoreCode + "01");
			//			XmlWriter.WriteAttributeString(RequestAttr.tran.ToString(), "ENROLL");
			//			
			//			XmlWriter.WriteEndElement();
		//	StartXmlDocument(Elements.request.ToString(), GetReqAtrNames(), GetReqAtrVals(storeCode, clerkId));
			//			EndXmlDocument();
		}
		void WriteElementAmts(IPaymentInfo2 payInfo)
		{
			XmlWriter.WriteStartElement(Elements.amts.ToString());

			XmlWriter.WriteAttributeString(AmountsAttr.fee.ToString(), payInfo.LocalAmountPaid.ToString());
			XmlWriter.WriteAttributeString(AmountsAttr.total.ToString(), payInfo.LocalAmountPaid.ToString());

			XmlWriter.WriteEndElement();
		}
		void WriteElementCard(DebitCardApp app)
		{
			XmlWriter.WriteStartElement(Elements.card.ToString());
			
			XmlWriter.WriteAttributeString(CardAttr.entered.ToString(), "MANUAL");
			//XmlWriter.WriteAttributeString(CardAttr.expd.ToString(), "");
			XmlWriter.WriteAttributeString(CardAttr.pan.ToString(), app.CardNum);
			
			XmlWriter.WriteEndElement();
		}
		void WriteElementCH(ICustInfo2 custInfo, DebitCardApp app)
		{
			XmlWriter.WriteStartElement(Elements.ch.ToString());
			
			XmlWriter.WriteAttributeString(CardHolderAttr.attempts.ToString(), "1");
			XmlWriter.WriteAttributeString(CardHolderAttr.coaflag.ToString(), "1");
			XmlWriter.WriteAttributeString(CardHolderAttr.compbin.ToString(), app.CardNum);
			XmlWriter.WriteAttributeString(CardHolderAttr.ph.ToString(), custInfo.Contact);
			XmlWriter.WriteAttributeString(CardHolderAttr.ssn.ToString(), custInfo.Ssn);
						
			XmlWriter.WriteEndElement();
		}
		void WriteElementIdData(ICustInfo2 custInfo)
		{
			XmlWriter.WriteStartElement(Elements.iddata.ToString());
			
			XmlWriter.WriteAttributeString(IdDataAttr.dob.ToString(), custInfo.Dob.ToShortDateString());
			XmlWriter.WriteAttributeString(IdDataAttr.docno.ToString(), custInfo.IDState + custInfo.IDNumber);
			XmlWriter.WriteAttributeString(IdDataAttr.expd.ToString(), custInfo.IDExpirationDate.ToShortDateString());
			XmlWriter.WriteAttributeString(IdDataAttr.namef.ToString(), custInfo.FirstName);
			XmlWriter.WriteAttributeString(IdDataAttr.namel.ToString(), custInfo.LastName);
			XmlWriter.WriteAttributeString(IdDataAttr.namem.ToString(), "");
			XmlWriter.WriteAttributeString(IdDataAttr.addr1.ToString(), custInfo.MailAddr.FormattedStreetAddress);
			XmlWriter.WriteAttributeString(IdDataAttr.city.ToString(), custInfo.MailAddr.City);
			XmlWriter.WriteAttributeString(IdDataAttr.state.ToString(), custInfo.MailAddr.State);
			XmlWriter.WriteAttributeString(IdDataAttr.zip.ToString(), custInfo.MailAddr.Zipcode);
						
			XmlWriter.WriteEndElement();
		}
		void WriteElementIdMode()
		{
			XmlWriter.WriteStartElement(Elements.idmode.ToString());
			
			XmlWriter.WriteAttributeString(IdModeAttr.accuracy.ToString(), "M");
			XmlWriter.WriteAttributeString(IdModeAttr.addr1.ToString(), "M");
			XmlWriter.WriteAttributeString(IdModeAttr.city.ToString(), "M");
			XmlWriter.WriteAttributeString(IdModeAttr.dob.ToString(), "M");
			XmlWriter.WriteAttributeString(IdModeAttr.docno.ToString(), "M");
			XmlWriter.WriteAttributeString(IdModeAttr.expd.ToString(), "M");
			XmlWriter.WriteAttributeString(IdModeAttr.namef.ToString(), "M");
			XmlWriter.WriteAttributeString(IdModeAttr.namel.ToString(), "M");
			XmlWriter.WriteAttributeString(IdModeAttr.namem.ToString(), "M");
			XmlWriter.WriteAttributeString(IdModeAttr.state.ToString(), "M");
			XmlWriter.WriteAttributeString(IdModeAttr.zip.ToString(), "M");
			
			
			XmlWriter.WriteEndElement();
		}		
		string[] GetReqAtrNames()
		{
			string[] rootAtrNames = {RequestAttr.clerkid.ToString(), 
										RequestAttr.mid.ToString(), 
										RequestAttr.tid.ToString(), RequestAttr.tran.ToString()};

			return rootAtrNames;
		}
		string[] GetReqAtrVals(string storeCode, string clerkId)
		{
			string[] rootAtrVals = {clerkId, storeCode, storeCode + "01", "ENROLL" };

			return rootAtrVals;
		}

		#endregion
	}


	
    public class XMLResponseMsg : XMLMessage
	{
	#region Data

		string response;
		string balance;
		
	#endregion
		
	#region Properties

		public string Response	
		{ 
			get { return response;	}
			set { response = value;	}
		}
		public string Balance
		{
			get { return balance;		}
			set { balance = value;		}
		}		
		
	#endregion
	}
}