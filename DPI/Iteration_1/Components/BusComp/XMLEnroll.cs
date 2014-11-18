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
//
//	public class XMLEnroll : XMLPurpose
//	{
//	#region Data
//
//		XMLPurpose.XMLElementRequest req;
//		XMLPurpose.XMLElementAmts amts;
//		XMLPurpose.XMLElementCard card;
//		XMLPurpose.XMLElementCH ch;
//		XMLPurpose.XMLElementIdData iddata;
//		XMLPurpose.XMLElementIdMode idmode;
//
//	#endregion
//	
//	#region Properties
//
//		public XMLElementRequest Req
//		{ 
//			get { return req;	}
//			set { req = value;	}
//		}
//		public XMLElementAmts Amts
//		{
//			get { return amts;		}
//			set { amts = value;		}
//		}
//		public XMLElementCard Card
//		{
//			get { return card;		}
//			set { card = value;		}
//		}
//		public XMLElementCH Ch
//		{
//			get { return ch;		}
//			set { ch = value;		}
//		}
//		public XMLElementIdData Iddata
//		{
//			get { return iddata;		}
//			set { iddata = value;		}
//		}
//		public XMLElementIdMode Idmode
//		{
//			get { return idmode;		}
//			set { idmode = value;		}
//		}
//		
//	#endregion
//
//	#region Constructors
//
//		public XMLEnroll()
//		{
//		}
//		public XMLEnroll(string xml)
//		{
//		}
//		public XMLEnroll(XmlDocument doc)
//		{
//
//		}
//		public XMLEnroll(IOrderSum sum, IDebCardApp app, ICustInfo2 ci, IDemand dmd)
//		{
//			CreateMessage(sum, app, ci, dmd);			
//		}
//		#endregion
//
//	#region Implementations
//
//		void CreateMessage(IOrderSum sum, IDebCardApp app, ICustInfo2 ci, IDemand dmd)
//		{
//			Initialize(sum, app, ci, dmd);
//			Create();
//		}
//		void Initialize(IOrderSum sum, IDebCardApp app, ICustInfo2 ci, IDemand dmd)
//		{
//			req = new XMLElementRequest(dmd);
//			amts = new XMLElementAmts(sum);
//			card = new XMLElementCard(app);
//			ch = new XMLElementCH(app, ci);
//			iddata = new XMLElementIdData(ci);
//			idmode = new XMLElementIdMode();
//		}
//
//
//		void Create()
//		{
//			StartXmlDocument();
//			WriteElementRequest();
//			WriteElementAmts();
//			WriteElementCard();
//			WriteElementCH();
//			WriteElementIdData();
//			WriteElementIdMode();
//			EndXmlDocument();
//		}
//		void WriteElementRequest()
//		{			
//			StartXmlDocument(Elements.request.ToString(), GetReqAtrNames(), GetReqAtrVals());			
//		}
//		void WriteElementAmts()
//		{
//			XmlWriter.WriteStartElement(Elements.amts.ToString());
//
//			XmlWriter.WriteAttributeString(AmountsAttr.fee.ToString(), amts.Fee);
//			XmlWriter.WriteAttributeString(AmountsAttr.total.ToString(), amts.Total);
//
//			XmlWriter.WriteEndElement();
//		}
//		void WriteElementCard()
//		{
//			XmlWriter.WriteStartElement(Elements.card.ToString());
//			
//			XmlWriter.WriteAttributeString(CardAttr.entered.ToString(), card.Entered);
//			//XmlWriter.WriteAttributeString(CardAttr.expd.ToString(), "");
//			XmlWriter.WriteAttributeString(CardAttr.pan.ToString(), card.Pan);
//			
//			XmlWriter.WriteEndElement();
//		}
//		void WriteElementCH()
//		{
//			XmlWriter.WriteStartElement(Elements.ch.ToString());
//			
//			XmlWriter.WriteAttributeString(CardHolderAttr.attempts.ToString(), ch.Attempts);
//			XmlWriter.WriteAttributeString(CardHolderAttr.coaflag.ToString(), ch.Coaflag);
//			XmlWriter.WriteAttributeString(CardHolderAttr.compbin.ToString(), ch.Compbin);
//			XmlWriter.WriteAttributeString(CardHolderAttr.ph.ToString(), ch.Ph);
//			XmlWriter.WriteAttributeString(CardHolderAttr.ssn.ToString(), ch.Ssn);
//						
//			XmlWriter.WriteEndElement();
//		}
//		void WriteElementIdData()
//		{
//			XmlWriter.WriteStartElement(Elements.iddata.ToString());
//			
//			XmlWriter.WriteAttributeString(IdDataAttr.dob.ToString(), iddata.Dob);
//			XmlWriter.WriteAttributeString(IdDataAttr.docno.ToString(), iddata.Docno);
//			XmlWriter.WriteAttributeString(IdDataAttr.expd.ToString(), iddata.Expd);
//			XmlWriter.WriteAttributeString(IdDataAttr.namef.ToString(), iddata.Namef);
//			XmlWriter.WriteAttributeString(IdDataAttr.namel.ToString(), iddata.Namel);
//			XmlWriter.WriteAttributeString(IdDataAttr.namem.ToString(), iddata.Namem);
//			XmlWriter.WriteAttributeString(IdDataAttr.addr1.ToString(), iddata.Addr1);
//			XmlWriter.WriteAttributeString(IdDataAttr.city.ToString(), iddata.City);
//			XmlWriter.WriteAttributeString(IdDataAttr.state.ToString(), iddata.State);
//			XmlWriter.WriteAttributeString(IdDataAttr.zip.ToString(), iddata.Zip);
//						
//			XmlWriter.WriteEndElement();
//		}
//		void WriteElementIdMode()
//		{
//			XmlWriter.WriteStartElement(Elements.idmode.ToString());
//			
//			XmlWriter.WriteAttributeString(IdModeAttr.accuracy.ToString(), idmode.Accuracy);
//			XmlWriter.WriteAttributeString(IdModeAttr.addr1.ToString(), idmode.Addr1);
//			XmlWriter.WriteAttributeString(IdModeAttr.city.ToString(), idmode.City);
//			XmlWriter.WriteAttributeString(IdModeAttr.dob.ToString(), idmode.Dob);
//			XmlWriter.WriteAttributeString(IdModeAttr.docno.ToString(), idmode.Docno);
//			XmlWriter.WriteAttributeString(IdModeAttr.expd.ToString(), idmode.Expd);
//			XmlWriter.WriteAttributeString(IdModeAttr.namef.ToString(), idmode.Namef);
//			XmlWriter.WriteAttributeString(IdModeAttr.namel.ToString(), idmode.Namel);
//			XmlWriter.WriteAttributeString(IdModeAttr.namem.ToString(), idmode.Namem);
//			XmlWriter.WriteAttributeString(IdModeAttr.state.ToString(), idmode.State);
//			XmlWriter.WriteAttributeString(IdModeAttr.zip.ToString(), idmode.Zip);
//			
//			
//			XmlWriter.WriteEndElement();
//		}		
//		string[] GetReqAtrNames()
//		{
//			string[] rootAtrNames = {RequestAttr.clerkid.ToString(), 
//										RequestAttr.mid.ToString(), 
//										RequestAttr.stan.ToString(),
//										RequestAttr.tid.ToString(),
//										RequestAttr.tran.ToString()};
//
//			return rootAtrNames;
//		}
//		string[] GetReqAtrVals()
//		{
//			string[] rootAtrVals = {req.Clerkid, req.Mid, req.Stan, req.Tid, req.Tran };
//
//			return rootAtrVals;
//		}
//
//	#endregion
//	}
//}