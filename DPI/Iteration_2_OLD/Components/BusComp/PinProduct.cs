using System;
using System.Xml;
using System.Collections;
using System.Text;

using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]  
	public class PinProduct : IPinProduct
	{
	#region Data
		int			product_Id;
		string		product_Name;
		decimal		price;
		string		expiration;
		int			unlimited;
		string		upc;
		string		reqItems;
	#endregion

	#region Properties
		public int Product_Id
		{ 
			get { return product_Id;  }
			set { product_Id = value; }
		}
		public string Product_Name
		{
			get { return product_Name;  }
			set { product_Name = value; }
		}
		public decimal Price
		{
			get { return price;  }
			set { price = value; }
		}
		public string Expiration
		{
			get { return expiration;  }
			set { expiration = value; }
		}
		public int	Unlimited
		{ 
			get { return unlimited;  }
			set { unlimited = value; }
		}
		public string	Upc
		{ 
			get { return upc;  }
			set { upc = value; }
		}
		public string	ReqItems
		{ 
			get { return reqItems;  }
			set { reqItems = value; }
		}
		
	#endregion

	#region Constructors
		public PinProduct(){}
		public PinProduct(int _product_Id, string _product_Name, decimal _price, string _expiration)
		{
			this.product_Id = _product_Id;
			this.product_Name = _product_Name;
			this.price = _price;			
			this.expiration = _expiration;
		}
		public PinProduct(IWireless_Products prod) : this( prod.Wireless_product_id, prod.Product_name, prod.Price, prod.Expiration)
		{
		}
		public PinProduct(XmlNode list)
		{
			this.product_Id		= Convert.ToInt32(list.Attributes.GetNamedItem("wireless_product_id").Value);
			this.product_Name	= list.Attributes.GetNamedItem("product_name").Value;
			this.price			= Convert.ToDecimal(list.Attributes.GetNamedItem("price").Value);
			this.expiration		= list.Attributes.GetNamedItem("expiration").Value;
		}		
		public PinProduct(int _product_Id, string _product_Name, decimal _price, string _expiration, string reqItems)
			: this(_product_Id, _product_Name, _price, _expiration)
		{
			this.reqItems = reqItems;
		}
	#endregion

	#region Methods
		public static PinProduct[] GetPinProducts(XmlDocument doc, int vendorID)
		{
			XmlNodeList list = doc.GetElementsByTagName("new");

			ArrayList al = new ArrayList();

			for (int i = 0; i < list.Count; i++)
			{   
				if (!list[i].Attributes.GetNamedItem("errorcode").Value.Equals("A"))				
					throw new Exception("Error loading wireless products for vendor: " + vendorID);								
					
				al.Add(new PinProduct(list[i]));
			}  
			PinProduct[] pinProducts = new PinProduct[al.Count];
			al.CopyTo(pinProducts);
			return (pinProducts);		
		}
		public static PinProduct[] GetPinProducts(Wireless_Products[] prods, int vendorID)
		{
			PinProduct[] pp = new PinProduct[prods.Length];

			for (int i = 0; i < prods.Length; i++)
				pp[i] = new PinProduct(prods[i]); 

			return pp;		
		}
		public static void AddProdId(UOW uow, IPinProduct[] pinProds, int vendorId, string storeCode)
		{
			Wireless_Products[] wps = Wireless_Products.GetAllByVendor(uow, vendorId, storeCode);

			for (int i = 0; i < pinProds.Length; i++)
				SetProdId(pinProds[i], wps); 
		}
		public static void SetProdId(IPinProduct pinProd, Wireless_Products[] wps)
		{
			for (int i = 0; i < wps.Length; i++)
				if (pinProd.Upc == wps[i].UniProdName)
				{
					pinProd.Product_Id	= wps[i].Wireless_product_id;
					pinProd.Price		= wps[i].Price;
					return;
				}
			
			throw new ApplicationException("Wireless Product : " + pinProd.Product_Name + " not found");		
		}
		public static string GetAllProducts(UOW uow, string storeCode, string prodCategory)
		{
			IWireless_Products[] wps = Wireless_Products.GetAllProducts(uow, storeCode, prodCategory);
 
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < wps.Length; i++)
				sb.Append(wps[i].Vendor_id.ToString() + "," + 
						  wps[i].Wireless_product_id.ToString() + "," + 
						  wps[i].Product_name + "," + 
						  wps[i].Price.ToString() + "," + 
						  wps[i].Expiration + ";");

			return sb.ToString();
		}
	#endregion
	}
}