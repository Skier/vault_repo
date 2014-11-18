using System;
using System.Xml;
using System.Data;
using System.Text;
using System.Collections;
using System.Data.SqlClient;

using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Components
{
	[Serializable]
	public class Wireless_Products : DomainObj, IWireless_Products
	{
		#region Data
		static string iName = "Wireless_Products";
		int wireless_product_id;
		string product_name;		
		int supplier_id;
		int vendor_id;
		string vendor_Name; //exceptional case
		string soc;
		string expiration;
		decimal price;
		DateTime start_date;
		DateTime end_date;
		string receipt_text;
		int product_commission_percent;
		decimal product_commission_flat;
		int prodId;
		string overrideWSProvider;
		string uniProdName;
		bool isActivationReq;
		bool isPhoneReq;
		string reqItems;
		bool isXml;
		bool isPerValidationReq;

		#endregion        

		#region Properties
		public override IDomKey IKey 
		{
			get { return new Key(iName, wireless_product_id.ToString()); }
		}
		public int Wireless_product_id
		{
			get { return wireless_product_id; }
		}
		public string Product_name
		{
			get { return product_name; }
			set
			{
				setState();
				product_name = value;
			}
		}
		public string UniProdName
		{
			get { return uniProdName; }
			set
			{
				setState();
				uniProdName = value;
			}
		}
		public int Supplier_id
		{
			get { return supplier_id; }
			set
			{
				setState();
				supplier_id = value;
			}
		}
		public int Vendor_id
		{
			get { return vendor_id; }
			set
			{
				setState();
				vendor_id = value;
			}
		}
		public string Vendor_Name
		{
			get { return vendor_Name; }
			set { vendor_Name = value; } //no setState since we will not save in DB
		}
		public string Soc
		{
			get { return soc; }
			set
			{
				setState();
				soc = value;
			}
		}
		public string Expiration
		{
			get { return expiration; }
			set
			{
				setState();
				expiration = value;
			}
		}
		public decimal Price
		{
			get { return price; }
			set
			{
				setState();
				price = Decimal.Round(value, 2);
			}
		}
		public DateTime Start_date
		{
			get { return start_date; }
			set
			{
				setState();
				start_date = value;
			}
		}
		public DateTime End_date
		{
			get { return end_date; }
			set
			{
				setState();
				end_date = value;
			}
		}
		public string Receipt_text
		{
			get { return receipt_text; }
			set
			{
				setState();
				receipt_text = value;
			}
		}
		public int Product_commission_percent
		{
			get { return product_commission_percent; }
			set
			{
				setState();
				product_commission_percent = value;
			}
		}
		public decimal Product_commission_flat
		{
			get { return product_commission_flat; }
			set
			{
				setState();
				product_commission_flat = Decimal.Round(value, 2);
			}
		}
		public decimal CommissionAmt
		{
			get 
			{
				if (product_commission_percent > 0)
					return decimal.Round(price * product_commission_percent/100, 2);
			
				return decimal.Round(Product_commission_flat, 2);
			}
		}
		public int ProdId
		{
			get { return prodId; }
			set
			{
				setState();
				prodId = value;
			}
		}
		public string OverrideWSProvider
		{
			get { return overrideWSProvider; }
			set
			{
				setState();
				overrideWSProvider = value;
			}
		}
		public bool		IsActivationReq		{ get { return isActivationReq;}}
		public bool		IsPhoneReq			{ get { return isPhoneReq;}}
		public string	ReqItems			{ get { return reqItems;}}
		public bool		IsXml				{ get { return isXml;}}
		public bool		IsPerValidationReq  { get { return isPerValidationReq; }}
		#endregion        

		#region Constructors			
		public Wireless_Products()
		{
			sql = new Wireless_ProductsSQL();
			wireless_product_id = random.Next(Int32.MinValue, -1);
			rowState = RowState.New;
		}
		public Wireless_Products(UOW uow) : this()
		{
			if(uow == null)
				throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
			if(uow.Imap == null)
				throw new ArgumentException("IdentityMap is required", "Identity Map");
            
			this.uow = uow;
			this.uow.Imap.add(this);
		}
		#endregion        

		#region Methods

		public  string GetReceipt(bool pass, DictionaryEntry[] entries) 
		{
			if (!isXml)
				return receipt_text;

			return new Receipt(pass, receipt_text).GetReceiptText(entries);
		}
		protected override SqlGateway loadSql()
		{
			return new Wireless_ProductsSQL();
		}
		public override void checkExists()
		{
			if ((Wireless_product_id < 1))
				throw new ArgumentException("Valid row is required for update and delete");
		}
		/*		Static methods		*/

		public static Wireless_Products find(UOW uow, int wireless_product_id)
		{
			if (uow.Imap.keyExists(Wireless_Products.getKey(wireless_product_id)))
				return (Wireless_Products)uow.Imap.find(Wireless_Products.getKey(wireless_product_id));
            
			Wireless_Products cls = new Wireless_Products();
			cls.uow = uow;
			cls.wireless_product_id = wireless_product_id;
			cls = (Wireless_Products)DomainObj.addToIMap(uow, getOne(((Wireless_ProductsSQL)cls.Sql).getKey(cls)));
			cls.uow = uow;
            
			return cls;
		}
		public static Wireless_Products[] getAll(UOW uow)
		{
			Wireless_Products[] objs = (Wireless_Products[])DomainObj.addToIMap(uow, (new Wireless_ProductsSQL()).getAll(uow));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Wireless_Products[] GetAllByVendor(UOW uow, int vendorId, string storeCode)
		{
			Wireless_Products[] objs = (Wireless_Products[])DomainObj.addToIMap(uow, (new Wireless_ProductsSQL()).GetAllByVendor(uow, vendorId, storeCode));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Wireless_Products[] GetAllProducts(UOW uow, string storeCode, string prodCategory)
		{
			Wireless_Products[] objs = (Wireless_Products[])DomainObj.addToIMap(uow, (new Wireless_ProductsSQL()).GetAllProducts(uow, storeCode, prodCategory));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Wireless_Products[] GetDpiWLMainProds(UOW uow, bool isRecharge, bool isInternational)
		{
			Wireless_Products[] objs = (Wireless_Products[])DomainObj.addToIMap(uow, (new Wireless_ProductsSQL()).GetDpiWLMainProds(uow, isRecharge, isInternational));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Wireless_Products[] GetDpiWLOptionalProds(UOW uow, int vendor)
		{
			Wireless_Products[] objs = (Wireless_Products[])DomainObj.addToIMap(uow, (new Wireless_ProductsSQL()).GetDpiWLOptionalProds(uow, vendor));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Wireless_Products[] GetBySocPrice(UOW uow, string storeCode, string soc, decimal price)
		{
			Wireless_Products[] objs = (Wireless_Products[])DomainObj.addToIMap(uow, (new Wireless_ProductsSQL()).GetBySocPrice(uow, storeCode, soc, price));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Key getKey(int wireless_product_id)
		{
			return new Key(iName, wireless_product_id.ToString());
		}

		#endregion        

		#region Implementation
		static Wireless_Products getOne(Wireless_Products[] acls)
		{
			if (acls.Length == 1)
				return acls[0];
            
			if (acls.Length == 0)
				throw new ArgumentException("Row not found");
            
			throw new ArgumentException("More than one row found");
		}
		static void copyAttrs(Wireless_Products src, Wireless_Products tar)
		{
			if (tar == null)
				throw new ArgumentNullException("Target object must not be null");
            
			if (src == null)
				throw new ArgumentNullException("Source object must not be null");
            
			tar.wireless_product_id = src.wireless_product_id;
			tar.product_name = src.product_name;			
			tar.supplier_id = src.supplier_id;
			tar.vendor_id = src.vendor_id;
			tar.soc = src.soc;
			tar.expiration = src.expiration;
			tar.price = src.price;
			tar.start_date = src.start_date;
			tar.end_date = src.end_date;
			tar.receipt_text = src.receipt_text;
			tar.product_commission_percent = src.product_commission_percent;
			tar.product_commission_flat = src.product_commission_flat;
			tar.prodId = src.prodId;
			tar.rowState = src.rowState;
			tar.overrideWSProvider = src.overrideWSProvider;
			tar.uniProdName = src.uniProdName;
			tar.isActivationReq = src.isActivationReq;
			tar.isPhoneReq = src.isPhoneReq;
			tar.reqItems = src.reqItems;
			tar.isXml = src.isXml;
			tar.isPerValidationReq = src.isPerValidationReq;
		}
		#endregion        

		#region SQL
		[Serializable]
			class Wireless_ProductsSQL : SqlGateway
		{
			public Wireless_Products[] getKey(Wireless_Products rec)
			{
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spWireless_Products_Get_Id";
				cmd.Parameters.Add("@Wireless_product_id", SqlDbType.Int, 0).Value = rec.wireless_product_id;
				return convert(execReader(cmd));
			}
			public override void insert(DomainObj obj)
			{
				Wireless_Products rec = (Wireless_Products)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spWireless_Products_Ins";
				setParam(cmd, rec);
                
				cmd.Parameters["@Wireless_product_id"].Direction = ParameterDirection.Output;
				execScalar(cmd);
				rec.wireless_product_id = (int)cmd.Parameters["@Wireless_product_id"].Value;
				rec.rowState = RowState.Clean;
			}
			public override void delete(DomainObj obj)
			{
				Wireless_Products rec = (Wireless_Products)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spWireless_Products_Del_Id";
				cmd.Parameters.Add("@Wireless_product_id", SqlDbType.Int, 0).Value = rec.wireless_product_id;
                
				execScalar(cmd);
				rec.rowState = RowState.Deleted;
			}
			public override void update(DomainObj obj)
			{
				Wireless_Products rec = (Wireless_Products)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spWireless_Products_Upd";
				setParam(cmd, rec);
                
				execScalar(cmd);
				rec.rowState = RowState.Clean;
			}
			public Wireless_Products[] getAll(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spWireless_Products_Get_All";
				return convert(execReader(cmd));
			}
			public Wireless_Products[] GetAllByVendor(UOW uow, int vendorId, string storeCode)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spWireless_Products_Get_All_By_Vendor";
				cmd.Parameters.Add("@Vendor_id", SqlDbType.Int, 0).Value = vendorId;
				cmd.Parameters.Add("@Storecode", SqlDbType.VarChar, 10).Value = storeCode;

				return convert(execReader(cmd));
			}
			public Wireless_Products[] GetDpiWLMainProds(UOW uow, bool isRecharge, bool isInternational)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spWirelessGetMainProducts ";
				cmd.Parameters.Add("@IsRecharge", SqlDbType.Bit, 0).Value = isRecharge;
				cmd.Parameters.Add("@IsInternational", SqlDbType.Bit, 0).Value = isInternational;
			
				return execReader1(cmd);
			}
			public Wireless_Products[] GetDpiWLOptionalProds(UOW uow, int vendor)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spWirelessGetOptionalProducts ";				
				cmd.Parameters.Add("@Vendorid", SqlDbType.Int, 0).Value = vendor;		
			
				return execReader1(cmd);
			}
			public Wireless_Products[] GetBySocPrice(UOW uow, string storeCode, string soc, decimal price)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spWireless_Products_Get_By_Soc_Price";
				
				cmd.Parameters.Add("@Storecode", SqlDbType.VarChar, 10).Value = storeCode;
				cmd.Parameters.Add("@SOC", SqlDbType.VarChar, 50).Value = soc;
				cmd.Parameters.Add("@Price", SqlDbType.Decimal, 0).Value = price;
				

				return convert(execReader(cmd));
			}
			public Wireless_Products[] GetAllProducts(UOW uow, string storeCode, string prodCategory)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spWireless_Products_Get_All_By_Store";
				cmd.Parameters.Add("@ProdCategory", SqlDbType.VarChar, 50).Value = prodCategory;
				cmd.Parameters.Add("@Storecode", SqlDbType.VarChar, 10).Value = storeCode;

				return convert(execReader(cmd));
			}			

			#endregion        

			#region SQL Implementation
			void setParam(SqlCommand cmd, Wireless_Products rec)
			{
				cmd.Parameters.Add("@Wireless_product_id", SqlDbType.Int, 0).Value = rec.wireless_product_id;
 
				if (rec.product_name == null)
					cmd.Parameters.Add("@Product_name", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.Product_name.Length == 0)
						cmd.Parameters.Add("@Product_name", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Product_name", SqlDbType.VarChar, 50).Value = rec.product_name;
				}
				if (rec.uniProdName == null)
					cmd.Parameters.Add("@UniProdName", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.uniProdName.Length == 0)
						cmd.Parameters.Add("@UniProdName", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@UniProdName", SqlDbType.VarChar, 50).Value = rec.uniProdName;
				}
				cmd.Parameters.Add("@Supplier_id", SqlDbType.Int, 0).Value = rec.supplier_id;
				cmd.Parameters.Add("@Vendor_id", SqlDbType.Int, 0).Value = rec.vendor_id;
 
				if (rec.soc == null)
					cmd.Parameters.Add("@Soc", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.Soc.Length == 0)
						cmd.Parameters.Add("@Soc", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Soc", SqlDbType.VarChar, 50).Value = rec.soc;
				}
 
				if (rec.expiration == null)
					cmd.Parameters.Add("@Expiration", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.Expiration.Length == 0)
						cmd.Parameters.Add("@Expiration", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Expiration", SqlDbType.VarChar, 50).Value = rec.expiration;
				}
				cmd.Parameters.Add("@Price", SqlDbType.Decimal, 0).Value = rec.price;
 
				if (rec.start_date == DateTime.MinValue)
					cmd.Parameters.Add("@Start_date", SqlDbType.DateTime, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Start_date", SqlDbType.DateTime, 0).Value = rec.start_date;
 
				if (rec.end_date == DateTime.MinValue)
					cmd.Parameters.Add("@End_date", SqlDbType.DateTime, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@End_date", SqlDbType.DateTime, 0).Value = rec.end_date;
 
				if (rec.receipt_text == null)
					cmd.Parameters.Add("@Receipt_text", SqlDbType.VarChar, 1600).Value = DBNull.Value;
				else
				{
					if (rec.Receipt_text.Length == 0)
						cmd.Parameters.Add("@Receipt_text", SqlDbType.VarChar, 1600).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Receipt_text", SqlDbType.VarChar, 1600).Value = rec.receipt_text;
				}
				cmd.Parameters.Add("@Product_commission_percent", SqlDbType.Int, 0).Value = rec.product_commission_percent;
				cmd.Parameters.Add("@Product_commission_flat", SqlDbType.Decimal, 0).Value = rec.product_commission_flat;
				cmd.Parameters.Add("@ProdId", SqlDbType.Int, 0).Value = rec.prodId;


				if (rec.overrideWSProvider == null)
					cmd.Parameters.Add("@OverrideWSProvider", SqlDbType.VarChar, 25).Value = DBNull.Value;
				else
				{
					if (rec.overrideWSProvider.Length == 0)
						cmd.Parameters.Add("@OverrideWSProvider", SqlDbType.VarChar, 25).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@OverrideWSProvider", SqlDbType.VarChar, 25).Value = rec.overrideWSProvider;
				}
				cmd.Parameters.Add("@IsActivationReq", SqlDbType.Bit, 0).Value = rec.isActivationReq;
				cmd.Parameters.Add("@IsPhoneReq", SqlDbType.Bit, 0).Value = rec.isPhoneReq;

				if (rec.reqItems == null)
					cmd.Parameters.Add("@ReqItems", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.reqItems.Length == 0)
						cmd.Parameters.Add("@ReqItems", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@ReqItems", SqlDbType.VarChar, 50).Value = rec.reqItems;
				} 
				cmd.Parameters.Add("@IsXml", SqlDbType.Bit, 0).Value = rec.isXml;
				cmd.Parameters.Add("@IsPerValidationReq", SqlDbType.Bit, 0).Value = rec.isPerValidationReq;
			}
			protected override DomainObj reader(SqlDataReader rdr)
			{
				Wireless_Products rec = new Wireless_Products();
                
				if (rdr["Wireless_product_id"] != DBNull.Value)
					rec.wireless_product_id = (int) rdr["Wireless_product_id"];
 
				if (rdr["Product_name"] != DBNull.Value)
					rec.product_name = (string) rdr["Product_name"];

				if (rdr["UniProdName"] != DBNull.Value)
					rec.uniProdName = (string) rdr["UniProdName"];
 
				if (rdr["Supplier_id"] != DBNull.Value)
					rec.supplier_id = (int) rdr["Supplier_id"];
 
				if (rdr["Vendor_id"] != DBNull.Value)
					rec.vendor_id = (int) rdr["Vendor_id"];
 
				if (rdr["Soc"] != DBNull.Value)
					rec.soc = (string) rdr["Soc"];
 
				if (rdr["Expiration"] != DBNull.Value)
					rec.expiration = (string) rdr["Expiration"];
 
				if (rdr["Price"] != DBNull.Value)
					rec.price = Decimal.Round((decimal)rdr["Price"], 2);
 
				if (rdr["Start_date"] != DBNull.Value)
					rec.start_date = (DateTime) rdr["Start_date"];
 
				if (rdr["End_date"] != DBNull.Value)
					rec.end_date = (DateTime) rdr["End_date"];
 
				if (rdr["Receipt_text"] != DBNull.Value)
					rec.receipt_text = (string) rdr["Receipt_text"];
 
				if (rdr["Product_commission_percent"] != DBNull.Value)
					rec.product_commission_percent = (int) rdr["Product_commission_percent"];
 
				if (rdr["Product_commission_flat"] != DBNull.Value)
					rec.product_commission_flat = Decimal.Round((decimal)rdr["Product_commission_flat"], 2);
 
				if (rdr["ProdId"] != DBNull.Value)
					rec.prodId = (int) rdr["ProdId"];

				if (rdr["OverrideWSProvider"] != DBNull.Value)
					rec.overrideWSProvider = (string) rdr["OverrideWSProvider"];

				if (rdr["IsActivationReq"] != DBNull.Value)
					rec.isActivationReq = (bool) rdr["IsActivationReq"];

				if (rdr["IsPhoneReq"] != DBNull.Value)
					rec.isPhoneReq = (bool) rdr["IsPhoneReq"];

				if (rdr["ReqItems"] != DBNull.Value)
					rec.reqItems = (string) rdr["ReqItems"];

				if (rdr["IsXml"] != DBNull.Value)
					rec.isXml = (bool) rdr["IsXml"];

				if (rdr["IsPerValidationReq"] != DBNull.Value)
					rec.isPerValidationReq = (bool) rdr["IsPerValidationReq"];

				rec.rowState = RowState.Clean;
				return rec;
			}
			Wireless_Products[] convert(DomainObj[] objs)
			{
				Wireless_Products[] acls  = new Wireless_Products[objs.Length];
				objs.CopyTo(acls, 0);
				return  acls;
			}
			Wireless_Products[] execReader1(SqlCommand cmd)
			{
				ArrayList ar = new ArrayList();
				SqlDataReader rdr = cmd.ExecuteReader();
			
				try
				{
					while(rdr.Read())
						ar.Add(reader1(rdr));
			
					Wireless_Products[] prods = new Wireless_Products[ar.Count];
					ar.CopyTo(prods);
					return prods;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
			
					if (cmd.Transaction != null)
						if (cmd.Transaction.Connection != null)
							cmd.Transaction.Rollback();
							
					throw e;
				}
				finally
				{
					if (!rdr.IsClosed)
						rdr.Close();
				}
			}	
			Wireless_Products reader1(SqlDataReader rdr)
			{
				Wireless_Products rec = new Wireless_Products();
                
				if (rdr["Wireless_product_id"] != DBNull.Value)
					rec.wireless_product_id = (int) rdr["Wireless_product_id"];
 
				if (rdr["Product_name"] != DBNull.Value)
					rec.product_name = (string) rdr["Product_name"];

				if (rdr["Vendor_id"] != DBNull.Value)
					rec.vendor_id = (int) rdr["Vendor_id"];

				if (rdr["Vendor_Name"] != DBNull.Value)
					rec.vendor_Name = (string) rdr["Vendor_Name"];
 
				if (rdr["Soc"] != DBNull.Value)
					rec.soc = (string) rdr["Soc"];
 
				if (rdr["Expiration"] != DBNull.Value)
					rec.expiration = (string) rdr["Expiration"];
 
				if (rdr["Price"] != DBNull.Value)
					rec.price = Decimal.Round((decimal)rdr["Price"], 2);
 
				if (rdr["Receipt_text"] != DBNull.Value)
					rec.receipt_text = (string) rdr["Receipt_text"];
 
				
				rec.rowState = RowState.Clean;
				return rec;
			}
		}


		#endregion        

		#region Receipt      
		
		[Serializable]
		class Receipt
		{ 
		#region Data
			string header;
			string success;
			string failure;
			string footer;
			bool pass;
		#endregion

		#region Properties
			public bool Pass { get { return pass; }}
		#endregion

		#region Constructors
			public Receipt(bool pass, string rctTxt)
			{
				header = success = failure = footer = string.Empty; 
				this.pass = pass;
				SetElementsAttrs(new XmlTextReader( XMLUtility.ConvertToXml(rctTxt).OuterXml, XmlNodeType.Element, null));
			}		
		#endregion

		#region Methods
			public virtual string GetReceiptText(DictionaryEntry[] entries)
			{
				StringBuilder sb = new  StringBuilder();

				sb.Append(Header());
				sb.Append(SuccessFailure());
				sb.Append(Footer());

				return SubstituteVars(sb.ToString(), entries);
			}
			#endregion

			#region Implementation

			protected virtual string SubstituteVars(string txt, DictionaryEntry[] entries)
			{
				StringBuilder sb = new StringBuilder(txt);
				
				for (int i = 0; i < entries.Length; i++)
					sb.Replace(((string)entries[i].Key).Trim().ToLower(), (string)entries[i].Value);

				return sb.ToString();
			}
			protected virtual string Header()
			{
				return header + "\n";
			}
			protected virtual string SuccessFailure()
			{
				if (pass)
					return success + "\n";
		
				return failure + "\n";
			}
			protected virtual string Footer()
			{
				return footer.Replace("\\n", "\n");
			}
			protected virtual void SetElementsAttrs(XmlTextReader xReader)
			{
				while (xReader.Read())
					if (xReader.NodeType == XmlNodeType.Element)
					{
						while (xReader.MoveToNextAttribute())
							switch (xReader.Name.Trim().ToLower())
							{
								case "header" :
								{
									header = xReader.Value;
									break;
								}
								case "success" :
								{
									success = xReader.Value;
									break;
								}
								case "failure" :
								{
									failure = xReader.Value;
									break;
								}
								case "footer" :
								{
									footer = xReader.Value;
									break;
								}
								default :
								{
									DPI_Err_Log.AddLogEntry("WirelessProdReceipt", "", "Uknown Xml attribute: " + xReader.Name);
									break;
								}
							}					
					}
				#endregion
			}
		}
	}
	#endregion
}