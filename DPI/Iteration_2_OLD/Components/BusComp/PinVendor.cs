using System;
using System.Xml;
using System.Collections;

using DPI.Interfaces;

namespace DPI.Components
{
	public class PinVendor : IPinVendor
	{
	#region Data
		string id;
		string name;
	#endregion
	
	#region Properties
		public string Id   
		{ 
			get { return id;  }
			set { id = value; }
		}
		public string Name 
		{ 
			get { return name; }
			set {name = value; }
		}
	#endregion

	#region Constructors
		public PinVendor(string _id, string _name)
		{
			this.id = _id;
			this.name = _name;
		}
		public PinVendor() 	{}
	#endregion

	#region Methods

		public static IPinVendor[] GetDebitCardVendors(UOW uow, XmlDocument doc)
		{
			Verify(doc);

			ArrayList al = new ArrayList();
			XmlNodeList list = doc.GetElementsByTagName("new");				

			for (int i = 0; i < list.Count; i++)
				if (FilterDebitCardWireless(uow, int.Parse(list[i].Attributes.GetNamedItem("vendor_id").Value)))
					al.Add(new PinVendor(list[i].Attributes.GetNamedItem("vendor_id").Value,
						list[i].Attributes.GetNamedItem("vendor_name").Value));	

			return ConvertToPinVendor(al);
		}


		public static IPinVendor[] GetPinVendors(UOW uow, XmlDocument doc)
		{
			Verify(doc);

			ArrayList al = new ArrayList();
			XmlNodeList list = doc.GetElementsByTagName("new");				

			for (int i = 0; i < list.Count; i++)
				if (FilterInternet(uow, int.Parse(list[i].Attributes.GetNamedItem("vendor_id").Value)))
					al.Add(new PinVendor(list[i].Attributes.GetNamedItem("vendor_id").Value,
						list[i].Attributes.GetNamedItem("vendor_name").Value));	

			return ConvertToPinVendor(al);
		}
		public static IPinVendor[] GetWirelessVendors(UOW uow)
		{
			return new IPinVendor[0];
		}
		public static IPinVendor[] GetPinVendors(IVendors[] vendors)
		{
			ArrayList al = new ArrayList();

			for (int i = 0; i < vendors.Length; i++)
				al.Add(new PinVendor(vendors[i].Vendor_id.ToString(), vendors[i].Vendor_name));

			return ConvertToPinVendor(al);
		}
		public static IPinVendor[] GetWirelessVendors(UOW uow, XmlDocument doc)
		{
			Verify(doc);

			ArrayList al = new ArrayList();
			XmlNodeList list = doc.GetElementsByTagName("new");				

			for (int i = 0; i < list.Count; i++)
				if (FilterWireless(uow, int.Parse(list[i].Attributes.GetNamedItem("vendor_id").Value)))
					al.Add(new PinVendor(list[i].Attributes.GetNamedItem("vendor_id").Value,
						list[i].Attributes.GetNamedItem("vendor_name").Value));	

			return ConvertToPinVendor(al);
		}
		public static IPinVendor[] GetWirelessVendors(UOW uow, IVendors[] vendors)
		{
			ArrayList al = new ArrayList();

			for (int i = 0; i < vendors.Length; i++)
				if (FilterWireless(uow, vendors[i].Vendor_id))
					al.Add(new PinVendor(vendors[i].Vendor_id.ToString(), vendors[i].Vendor_name));

			return ConvertToPinVendor(al);
		}
		public static IPinVendor[] GetInternetVendors(UOW uow, IVendors[] vendors)
		{
			ArrayList al = new ArrayList();

			for (int i = 0; i < vendors.Length; i++)
				if (FilterInternet(uow, vendors[i].Vendor_id))
					al.Add(new PinVendor(vendors[i].Vendor_id.ToString(), vendors[i].Vendor_name));

			return ConvertToPinVendor(al);
		}
		public static IPinVendor[] GetDpiWirelessVendors(UOW uow, IVendors[] vendors)
		{
			ArrayList al = new ArrayList();

			for (int i = 0; i < vendors.Length; i++)
				if (vendors[i].ProdCategory == ProdCategory.DpiWireless.ToString())
					al.Add(new PinVendor(vendors[i].Vendor_id.ToString(), vendors[i].Vendor_name));

			return ConvertToPinVendor(al);
		}		

		public static IPinVendor[] GetDpiWLVendorsByProdCategory(UOW uow, IVendors[] vendors, ProdCategory prodCategory)
		{
			ArrayList al = new ArrayList();

			for (int i = 0; i < vendors.Length; i++)
				if (vendors[i].ProdCategory == prodCategory.ToString())
					al.Add(new PinVendor(vendors[i].Vendor_id.ToString(), vendors[i].Vendor_name));

			return ConvertToPinVendor(al);
		}

	#endregion 

	#region Implementation
		static void Verify(XmlDocument doc)
		{
			XmlNodeList list = doc.GetElementsByTagName("new");				

			for (int i=0; i < list.Count; i++)
				if (!list[i].Attributes.GetNamedItem("errorcode").Value.Equals("A"))					
					throw new Exception("Error loading wireless vendors.");
		}
		static IPinVendor[] ConvertToPinVendor(ArrayList ar)
		{
			IPinVendor[] pinVendors = new PinVendor[ar.Count];
			ar.CopyTo(pinVendors);				
			return pinVendors;
		}

		static bool FilterInternet(UOW uow, int vend)
		{
			return ProductCategory.find(uow, Vendors.find(uow, vend).ProdCategory).IsInternet;
		}
		static bool FilterWireless(UOW uow, int vend)
		{
			return ProductCategory.find(uow, Vendors.find(uow, vend).ProdCategory).IsWireless;
		}
		static bool FilterDebitCardWireless(UOW uow, int vend)
		{
			return ProductCategory.find(uow, Vendors.find(uow, vend).ProdCategory).IsDebitCard;
		}
	#endregion
	}
}