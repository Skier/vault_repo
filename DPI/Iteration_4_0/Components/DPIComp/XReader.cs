using System;
using System.Xml;
using System.Collections;
using DPI.Interfaces;

namespace DPI.Components
{	
	public class XReader
	{
		XmlNode xnode;

		public XReader(XmlNode xnode)
		{ 
			this.xnode = xnode; 
		}
		public string this[string name]
		{
			get 
			{
				XmlTextReader xReader = new XmlTextReader(xnode.OuterXml, XmlNodeType.Element, null);
		
				while (xReader.Read())
				{
					if (xReader.NodeType != XmlNodeType.Element)
						continue;

					while (xReader.MoveToNextAttribute())
						if (xReader.Name.Trim().ToLower() == name.Trim().ToLower()) 
							return xReader.Value;
				}

				throw new ArgumentException("Xml doc '" + xnode.Name + "' does not contain attribute '" + name +  "'");
			}
		}
	}
}