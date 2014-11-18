//using System;
//using System.Xml;
//
//namespace DPI.Components
//{
//	public class XElement
//	{		
//		protected KeyVal[] attrs = new KeyVal[0];
//		protected XElement[] children = new XElement[0];
//		protected string name;
//		protected const string purposeMid =  "999003";
//
//		/*		Properties		*/
//		public string Name { get { return name; }} 
//		public KeyVal[] Attrs { get { return attrs; }}
//		public XElement[] Children { get { return children; }}
//
//		public string this[string attr]
//		{
//			get 
//			{
//				for (int i = 0; i < attrs.Length; i++)
//					if (attrs[i].Key.ToLower() == attr.ToLower())
//						return attrs[i].Val;
//				
//				throw new ArgumentException("No such attribute: " + attr);
//				}
//		}
//		public XElement this[string elemt, bool ele]
//		{
//			get 
//			{
//				for (int i = 0; i < children.Length; i++)
//					if (children[i].Name.ToLower() == elemt.ToLower())
//						return children[i];
//				
//				throw new ArgumentException("No such element: " + elemt);
//			}
//		}
//
//		/*		Constructors		*/
//		public XElement()
//		{
//
//		}
//		public XElement(string name)
//		{
//			this.name = name;
//		}
//		public XElement(string name, KeyVal[] attrs) : this(name)
//		{
//			this.attrs = attrs;
//		}
//		public XElement(string name, KeyVal[] attrs, XElement[] children) : this(name, attrs)
//		{
//			this.children = children;
//		}
//		/*		Methods		*/
//		public XmlDocument ToXmlDoc()
//		{
//			return new XMLMessage().StartXmlDocument(this);
//		}
//		
//	}
//	public class KeyVal
//	{
//		public readonly string Key;
//		public readonly string Val;
//		public KeyVal(string key, string val)
//		{
//			this.Key = key;
//			this.Val = val;
//		}
//	}
//}