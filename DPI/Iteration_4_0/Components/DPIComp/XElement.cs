using System;
using System.Xml;

namespace DPI.Components
{
	public class XElement
	{		
		protected KeyVal[] attrs = new KeyVal[0];
		protected XElement[] children = new XElement[0];
		protected string name;
		protected string val;

		/*		Properties		*/
		public string Name { get { return name; }}
		public string Val 
		{ 
			get { return val;	}
			set { val = value;	}
		}
		public KeyVal[] Attrs { get { return attrs; }}
		public XElement[] Children { get { return children; }}

		public string this[string attr]
		{
			get 
			{
				for (int i = 0; i < attrs.Length; i++)
					if (attrs[i].Key.ToLower() == attr.ToLower())
						return attrs[i].Val;
				
				throw new ArgumentException("No such attribute: " + attr);
			}
		}
		public XElement this[string elemt, bool ele]
		{
			get 
			{
				for (int i = 0; i < children.Length; i++)
					if (children[i].Name.ToLower() == elemt.ToLower())
						return children[i];
				
				throw new ArgumentException("No such element: " + elemt);
			}
		}

		/*		Constructors		*/
		public XElement()
		{
		}
		public XElement(string name)
		{
			this.name = name;
		}
		public XElement(string name, string val) : this(name)
		{
			this.val = val;
		}
		public XElement(string name, KeyVal[] attrs) : this(name)
		{
			this.attrs = attrs;
		}
		public XElement(string name, KeyVal[] attrs, XElement[] children) : this(name, attrs)
		{
			this.children = children;
		}
		/*		Methods		*/
		public XmlDocument ToXmlDoc()
		{
			return new XMessage().StartXmlDocument(this);
		}
		public override string ToString()
		{
			return ToXmlDoc().OuterXml;
		}

		public static XmlDocument GetErrorDoc(string text)
		{
			return new XElement("Error").ToXmlDoc();
		}
		
	}
	public class XePurpose : XElement
	{
		public static readonly string purposeMid =  "999003";
	}
	
}