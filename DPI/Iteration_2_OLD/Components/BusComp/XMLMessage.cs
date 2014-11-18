//using System;
//using System.Xml;
//using System.IO;
//using System.Text;
//using DPI.Interfaces;
//
//namespace DPI.Components
//{
//	public class XMLMessage 
//	{
//		
//	#region Member Variables
//
//		MemoryStream xmlStream;
//		XmlTextWriter xmlWriter;
//		XmlDocument xmlDocument = null;
//
//	#endregion
//	
//	#region Properties
//
//		protected XmlTextWriter XmlWriter     { get	{ return xmlWriter; }}
//		public string XmlStringMessage	      {	get { return xmlDocument.DocumentElement.OuterXml; }}
//		public XmlDocument XmlDocumentMessage {	get { return xmlDocument; }}
//
//	#endregion
//	
//	#region Methods					
//		
//		protected void StartXmlDocument()
//		{
//			xmlStream = new MemoryStream();
//			xmlWriter = new XmlTextWriter(xmlStream, Encoding.UTF8);
//			xmlWriter.WriteStartDocument();			
//		}
//		//Method begins XML document with outer element of rootElement		
//		public void StartXmlDocument(string rootElement)
//		{
//			xmlStream = new MemoryStream();
//			xmlWriter = new XmlTextWriter(xmlStream, Encoding.UTF8);
//			xmlWriter.WriteStartDocument();
//			xmlWriter.WriteStartElement(rootElement);
//		}
//		protected void StartXmlDocument(string rootElement, KeyVal[] pars)
//		{
//			xmlStream = new MemoryStream();
//			xmlWriter = new XmlTextWriter(xmlStream, Encoding.UTF8);
//			xmlWriter.WriteStartDocument();
//			xmlWriter.WriteStartElement(rootElement);
//		
//			for (int i = 0; i < pars.Length; i++)
//				xmlWriter.WriteAttributeString(pars[i].Key, pars[i].Val);
//		}
//		public XmlDocument StartXmlDocument(XElement rootElement)
//		{
//			xmlStream = new MemoryStream();
//			xmlWriter = new XmlTextWriter(xmlStream, Encoding.UTF8);
//			xmlWriter.WriteStartDocument();
//			
//			xmlWriter.WriteStartElement(rootElement.Name);
//
//			for (int i = 0; i < rootElement.Attrs.Length; i++)
//				xmlWriter.WriteAttributeString(rootElement.Attrs[i].Key, rootElement.Attrs[i].Val);
//
//			for (int i = 0; i < rootElement.Children.Length; i++)
//				WriteElement(rootElement.Children[i]);
//
////			xmlWriter.WriteEndElement(); //close root element
//
//			EndXmlDocument(); //close document
//			return xmlDocument;
//			}
////		void WriteChild(XElement parent)
////		{
////			xmlWriter.WriteStartElement(parent.Name);
////
////			for (int i = 0; i < parent.Attrs.Length; i++)
////				xmlWriter.WriteAttributeString(parent.Attrs[i].Key, parent.Attrs[i].Val);
////
////			for (int i = 0; i < parent.Children.Length; i++)
////				 WriteChild(parent.Children[i]);
////
////			xmlWriter.WriteEndElement();
////		}
//		void WriteElement(XElement element)
//		{
//			XmlWriter.WriteStartElement(element.Name);
//			
//			for (int i = 0; i < element.Attrs.Length; i++)
//				xmlWriter.WriteAttributeString(element.Attrs[i].Key, element.Attrs[i].Val);
//
//			for (int i = 0; i < element.Children.Length; i++)
//				WriteElement(element.Children[i]);
//
//			XmlWriter.WriteEndElement();
//		}
//
////		protected void StartXmlDocument(string rootElement, string[] rootAtrNames, string[] rootAtrVals)
////		{
////			xmlStream = new MemoryStream();
////			xmlWriter = new XmlTextWriter(xmlStream, Encoding.UTF8);
////			xmlWriter.WriteStartDocument();
////			xmlWriter.WriteStartElement(rootElement);
////			
////			if (rootAtrNames.Length != rootAtrVals.Length)
////				throw new ArgumentException("Number of Root Attribute Names and values must be equal",
////					"XMLMessage.StartXmlDocument");
////			
////			for (int i = 0; i < rootAtrNames.Length; i++)
////				xmlWriter.WriteAttributeString(rootAtrNames[i], rootAtrVals[i]);
////		}
//		public void EndXmlDocument()
//		{
//			xmlWriter.WriteEndElement(); //Write end document element
//			xmlWriter.WriteEndDocument();									
//			xmlWriter.Flush();				
//			SetXmlDocument();
//			xmlWriter.Close();
//		}
//
//	#endregion
//
//	#region Implementation					
//
//		void SetXmlDocument()
//		{
//			xmlStream.Position = 0;
//			xmlDocument = new XmlDocument();			
//			xmlDocument.Load(xmlStream);			
//		}
//
//	#endregion
//	}
//}
