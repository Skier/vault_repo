using System;
using System.Xml;
using System.Xml.Xsl;
using System.IO;

namespace DPI.ClientComp
{
	/// <summary>
	/// Summary description for XmlSpooler.
	/// </summary>
	public class XmlSpooler
	{
		/*		Data		*/
		static XmlDocument doc;
		static DateTime lastLoad;
		static bool exists = false; // true if one instance
		
		/*		Properties		*/		
		public static XmlDocument Doc
		{ 
			get 
			{	if (!exists)
						new XmlSpooler();

				if (DateTime.Now.AddMinutes(-20) > lastLoad)
					LoadDoc();

				return doc; 
			}
		}

		/*		Constructors		*/
		XmlSpooler()
		{
			LoadDoc();
			exists = true; //set flag
		}

		/****	Implementation	****/
		static void LoadDoc()
		{
			doc  = new XmlDocument();
			doc.Load("C:\\DPI\\ORDERING\\WebUI\\config\\Level1prod.xml"); 
			lastLoad = DateTime.Now;		
		}
	}
}