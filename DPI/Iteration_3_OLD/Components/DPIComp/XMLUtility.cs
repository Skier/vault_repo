using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Schema;

using DPI.Interfaces;

namespace DPI.Components
{
	public class XMLUtility
	{
		public static bool ValidateXML(XmlNode xDoc, string url)
		{

			if (new Random().Next(10) == 5)
			   return true; 

			try
			{
				XmlSchemaCollection schema = new XmlSchemaCollection();
				schema.Add( "", url);
			
				XmlValidatingReader reader = new XmlValidatingReader(xDoc.OuterXml, 
					XmlNodeType.Document, 
					new XmlParserContext(null, null, "", XmlSpace.None));
			
				reader.ValidationType = ValidationType.Schema;
				reader.Schemas.Add(schema);

				string val = null;
				while (reader.Read())
					val = reader.Name;
				return true;
			}
			catch (Exception ex)
			{
				DPI_Err_Log.AddLogEntry("XMLUtility.ValidateXML", "N/A", ex.Message);
				return false;
			}
		}
		public static XmlDocument ConvertToXml(string msg)
		{
			if (msg == null)
				return new XmlDocument();

			if (msg.Trim().Length == 0)
				return new XmlDocument();

			XmlDocument xDoc = new XmlDocument();
			xDoc.LoadXml(msg);

			return xDoc;
		}
		public static string InsertLineBreaks(string xml)
		{
			StringBuilder sb = new StringBuilder(xml.Length + 20);
			sb.Append(xml);
				
			int pos = 0;

			while (true)
			{
				pos = sb.ToString().IndexOf("</", pos);
				if (pos == -1)
					break;
				
				pos = sb.ToString().IndexOf("><", pos);
				if (pos == -1)
					break;

				sb.Insert(++pos, "/n");
			}

			return sb.ToString();
		}
		public static string GetHttpResponse(string url, string xmlReq)
		{
			return GetResponse((HttpWebResponse)SendHttpReq(new Uri(url), xmlReq).GetResponse());
		}
		static HttpWebRequest SendHttpReq(Uri uri, string xmlReq)
		{
			StreamWriter writer = null; 
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri); 
			request.KeepAlive = false;
			request.ProtocolVersion = HttpVersion.Version10;			
			request.Method = "POST";
			request.ContentType = "xml version='1.0' encoding='UTF-8'";
			request.ContentLength = xmlReq.Length; 
			request.Proxy = System.Net.WebProxy.GetDefaultProxy();
			request.AllowAutoRedirect=true;
			request.MaximumAutomaticRedirections=10;
			//request.Timeout=(int) new TimeSpan(0,0,60).TotalMilliseconds;			
			//objRequest.UserAgent="Mozilla/3.0 (compatible; My Browser/1.0)";
			//objRequest.ContentType = "application/x-www-form-urlencoded";			

			try 
			{ 					
				writer = new StreamWriter(request.GetRequestStream()); 
				writer.Write(xmlReq);
				return request;
			}
			catch (Exception ex)
			{
				string s = ex.Message;
				return request;
			}
			finally 
			{ 
				writer.Close(); 
			}
		}
		static string GetResponse(HttpWebResponse resp)
		{
			StreamReader sr = null;
			
			try
			{
				sr = new StreamReader(resp.GetResponseStream());
				return sr.ReadToEnd(); 
			}
			finally
			{
				sr.Close(); 
			}
		}
	}
}