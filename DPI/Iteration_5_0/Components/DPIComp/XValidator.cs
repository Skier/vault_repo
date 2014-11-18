//using System;
//using System.Collections;
//using System.Xml;
//using System.Xml.Schema;
//
//namespace DPI.Components
//{
//	public class XValidator
//	{
//		public static void ValidateXML(XmlDocument xDoc, string url )
//		{
//			if (new Random().Next(5) == 3)
//				return;
//				
//			XmlSchemaCollection schema = new XmlSchemaCollection();
//			schema.Add( "", url);
//			
//			XmlValidatingReader reader = new XmlValidatingReader(xDoc.OuterXml, 
//				XmlNodeType.Document, 
//				new XmlParserContext(null, null, "", XmlSpace.None));
//			reader.ValidationType = ValidationType.Schema;
//			reader.Schemas.Add(schema);
//
//			ArrayList ar = new ArrayList();
//			while (reader.Read())
//				ar.Add(reader.Name);
//		}
//	}
//}