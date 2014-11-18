using System;
using System.Xml;
using System.IO;
using System.Text;

using DPI.Interfaces;

namespace DPI.Components
{
	public class XMessage 
	{
		
	#region Data

		MemoryStream xmlStream;
		XmlTextWriter xmlWriter;
		XmlDocument xmlDocument = null;

	#endregion
	
	#region Properties

		protected XmlTextWriter XmlWriter     { get	{ return xmlWriter; }}
		public string XmlStringMessage	      {	get { return xmlDocument.DocumentElement.OuterXml; }}
		public XmlDocument XmlDocumentMessage {	get { return xmlDocument; }}

	#endregion
	
	#region Methods	
				
		public XmlDocument StartXmlDocument(XElement rootElement)
		{
			xmlStream = new MemoryStream();
			xmlWriter = new XmlTextWriter(xmlStream, Encoding.UTF8);
			xmlWriter.WriteStartDocument();
			
			xmlWriter.WriteStartElement(rootElement.Name);

			for (int i = 0; i < rootElement.Attrs.Length; i++)
				xmlWriter.WriteAttributeString(rootElement.Attrs[i].Key, rootElement.Attrs[i].Val);

			for (int i = 0; i < rootElement.Children.Length; i++)
				WriteElement(rootElement.Children[i]);

			EndXmlDocument(); //close document
			return xmlDocument;
		}

		public void EndXmlDocument()
		{
			xmlWriter.WriteEndElement(); //Write end document element
			xmlWriter.WriteEndDocument();									
			xmlWriter.Flush();				
			SetXmlDocument();
			xmlWriter.Close();
		}
		 
		//Method begins XML document with outer element of rootElement		
		public void StartXmlDocument(string rootElement)
		{
			xmlStream = new MemoryStream();
			xmlWriter = new XmlTextWriter(xmlStream, Encoding.UTF8);
			xmlWriter.WriteStartDocument();
			xmlWriter.WriteStartElement(rootElement);
		}
		
		protected void StartXmlDocument(string rootElement, KeyVal[] pars)
		{
			xmlStream = new MemoryStream();
			xmlWriter = new XmlTextWriter(xmlStream, Encoding.UTF8);
			xmlWriter.WriteStartDocument();
			xmlWriter.WriteStartElement(rootElement);
		
			for (int i = 0; i < pars.Length; i++)
				xmlWriter.WriteAttributeString(pars[i].Key, pars[i].Val);
		}
		protected void StartXmlDocument()
		{
			xmlStream = new MemoryStream();
			xmlWriter = new XmlTextWriter(xmlStream, Encoding.UTF8);
			xmlWriter.WriteStartDocument();			
		}

	#endregion

	#region Implementation					

		void SetXmlDocument()
		{
			xmlStream.Position = 0;
			xmlDocument = new XmlDocument();			
			xmlDocument.Load(xmlStream);			
		}
		void WriteElement(XElement element)
		{
			if ((element.Val != null) && element.Val.Trim().Length > 0)
			{
				XmlWriter.WriteElementString(element.Name, element.Val);
				return;
			}
		
			XmlWriter.WriteStartElement(element.Name);
			for (int i = 0; i < element.Attrs.Length; i++)
				xmlWriter.WriteAttributeString(element.Attrs[i].Key, element.Attrs[i].Val);

			for (int i = 0; i < element.Children.Length; i++)
				WriteElement(element.Children[i]);

			XmlWriter.WriteEndElement();
		}
	#endregion
	}
}