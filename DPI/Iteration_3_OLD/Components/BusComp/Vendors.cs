using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Components
{
	[Serializable]
	public class Vendors : DomainObj, IVendors
	{
	#region Data
		static string iName = "Vendors";
		int	   vendor_id;
		string vendor_name;
		string vendor_address;
		string vendor_city;
		string vendor_state;
		string vendor_zip;
		string vendor_phone;
		string product_type;
		string prodCategory;
		string status;
		string defaultWSProvider; 
		bool   isNpaNxxReq;
		string prodSubCategory;
		#endregion
        
	#region Properties
		public override IDomKey IKey 
		{
			get { return new Key(iName, vendor_id.ToString()); }
		}
		public int Vendor_id
		{
			get { return vendor_id; }
		}
		public string Vendor_name
		{
			get { return vendor_name; }
			set
			{
				setState();
				vendor_name = value;
			}
		}
		public string Vendor_address
		{
			get { return vendor_address; }
			set
			{
				setState();
				vendor_address = value;
			}
		}
		public string Vendor_city
		{
			get { return vendor_city; }
			set
			{
				setState();
				vendor_city = value;
			}
		}
		public string Vendor_state
		{
			get { return vendor_state; }
			set
			{
				setState();
				vendor_state = value;
			}
		}
		public string Vendor_zip
		{
			get { return vendor_zip; }
			set
			{
				setState();
				vendor_zip = value;
			}
		}
		public string Vendor_phone
		{
			get { return vendor_phone; }
			set
			{
				setState();
				vendor_phone = value;
			}
		}
		public string Product_type
		{
			get { return product_type; }
			set
			{
				setState();
				product_type = value;
			}
		}
		public string ProdCategory
		{
			get { return prodCategory; }
			set
			{
				setState();
				prodCategory = value;
			}
		}
		public string Status
		{
			get { return status; }
			set
			{
				setState();
				status = value;
			}
		}
		public string DefaultWSProvider
		{
			get { return defaultWSProvider; }
			set
			{
				setState();
				defaultWSProvider = value;
			}
		}
		public bool IsNpaNxxReq { get { return isNpaNxxReq; } }
		public string ProdSubCategory { get {return prodSubCategory; }}
		
	#endregion
        
	#region Constructors
        public Vendors()
        {
            sql = new VendorsSQL();
            vendor_id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public Vendors(UOW uow) : this()
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
        protected override SqlGateway loadSql()
        {
            return new VendorsSQL();
        }
        public override void checkExists()
        {
            if ((Vendor_id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static Vendors find(UOW uow, int vendor_id)
        {
            if (uow.Imap.keyExists(Vendors.getKey(vendor_id)))
                return (Vendors)uow.Imap.find(Vendors.getKey(vendor_id));
            
            Vendors cls = new Vendors();
            cls.uow = uow;
            cls.vendor_id = vendor_id;
            cls = (Vendors)DomainObj.addToIMap(uow, getOne(((VendorsSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static Vendors[] getAll(UOW uow)
        {
            Vendors[] objs = (Vendors[])DomainObj.addToIMap(uow, (new VendorsSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
		public static IVendors[] GetNpaNxxVendors(UOW uow)
		{
			Vendors[] objs = (Vendors[])DomainObj.addToIMap(uow, (new VendorsSQL()).GetNpaNxxVendors(uow));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static IVendors[] GetVendorsByStore(UOW uow, string storeCode)
		{
			Vendors[] objs = (Vendors[])DomainObj.addToIMap(uow, (new VendorsSQL()).GetVendorsByStore(uow, storeCode));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Key getKey(int vendor_id)
        {
            return new Key(iName, vendor_id.ToString());
        }
	#endregion
        
	#region Implementation
        static Vendors getOne(Vendors[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Vendors src, Vendors tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.vendor_id = src.vendor_id;
            tar.vendor_name = src.vendor_name;
            tar.vendor_address = src.vendor_address;
            tar.vendor_city = src.vendor_city;
            tar.vendor_state = src.vendor_state;
            tar.vendor_zip = src.vendor_zip;
            tar.vendor_phone = src.vendor_phone;
            tar.product_type = src.product_type;
            tar.prodCategory = src.prodCategory;
			tar.status = src.status;
			tar.defaultWSProvider = src.defaultWSProvider;
			tar.isNpaNxxReq = src.isNpaNxxReq;
			tar.prodSubCategory = src.prodSubCategory;
			
            tar.rowState = src.rowState;
        }
	#endregion
        
	#region SQL
        [Serializable]
        class VendorsSQL : SqlGateway
        {
            public Vendors[] getKey(Vendors rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spVendors_Get_Id";
                cmd.Parameters.Add("@Vendor_id", SqlDbType.Int, 0).Value = rec.vendor_id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                Vendors rec = (Vendors)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spVendors_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Vendor_id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.vendor_id = (int)cmd.Parameters["@Vendor_id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Vendors rec = (Vendors)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spVendors_Del_Id";
                cmd.Parameters.Add("@Vendor_id", SqlDbType.Int, 0).Value = rec.vendor_id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Vendors rec = (Vendors)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spVendors_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public Vendors[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spVendors_Get_All";
                return convert(execReader(cmd));
            }
			public Vendors[] GetNpaNxxVendors(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spVendors_Get_NpaNxxVendors";
				return convert(execReader(cmd));
			}
			public Vendors[] GetVendorsByStore(UOW uow, string storeCode)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spVendors_Get_By_Store";
				cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 10).Value = storeCode;

				return convert(execReader(cmd));
			}
	#endregion
       
	#region SQL Implementation
            void setParam(SqlCommand cmd, Vendors rec)
            {
                cmd.Parameters.Add("@Vendor_id", SqlDbType.Int, 0).Value = rec.vendor_id;
 
                if (rec.vendor_name == null)
                    cmd.Parameters.Add("@Vendor_name", SqlDbType.VarChar, 100).Value = DBNull.Value;
                else
                {
                    if (rec.Vendor_name.Length == 0)
                        cmd.Parameters.Add("@Vendor_name", SqlDbType.VarChar, 100).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Vendor_name", SqlDbType.VarChar, 100).Value = rec.vendor_name;
                }
 
                if (rec.vendor_address == null)
                    cmd.Parameters.Add("@Vendor_address", SqlDbType.VarChar, 120).Value = DBNull.Value;
                else
                {
                    if (rec.Vendor_address.Length == 0)
                        cmd.Parameters.Add("@Vendor_address", SqlDbType.VarChar, 120).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Vendor_address", SqlDbType.VarChar, 120).Value = rec.vendor_address;
                }
 
                if (rec.vendor_city == null)
                    cmd.Parameters.Add("@Vendor_city", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.Vendor_city.Length == 0)
                        cmd.Parameters.Add("@Vendor_city", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Vendor_city", SqlDbType.VarChar, 50).Value = rec.vendor_city;
                }
 
                if (rec.vendor_state == null)
                    cmd.Parameters.Add("@Vendor_state", SqlDbType.VarChar, 2).Value = DBNull.Value;
                else
                {
                    if (rec.Vendor_state.Length == 0)
                        cmd.Parameters.Add("@Vendor_state", SqlDbType.VarChar, 2).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Vendor_state", SqlDbType.VarChar, 2).Value = rec.vendor_state;
                }
 
                if (rec.vendor_zip == null)
                    cmd.Parameters.Add("@Vendor_zip", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.Vendor_zip.Length == 0)
                        cmd.Parameters.Add("@Vendor_zip", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Vendor_zip", SqlDbType.VarChar, 10).Value = rec.vendor_zip;
                }
 
                if (rec.vendor_phone == null)
                    cmd.Parameters.Add("@Vendor_phone", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.Vendor_phone.Length == 0)
                        cmd.Parameters.Add("@Vendor_phone", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Vendor_phone", SqlDbType.VarChar, 15).Value = rec.vendor_phone;
                }
 
                if (rec.product_type == null)
                    cmd.Parameters.Add("@Product_type", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.Product_type.Length == 0)
                        cmd.Parameters.Add("@Product_type", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Product_type", SqlDbType.VarChar, 10).Value = rec.product_type;
                }
 
                if (rec.prodCategory == null)
                    cmd.Parameters.Add("@ProdCategory", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.ProdCategory.Length == 0)
                        cmd.Parameters.Add("@ProdCategory", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ProdCategory", SqlDbType.VarChar, 50).Value = rec.prodCategory;
                }

				if (rec.status == null)
					cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = DBNull.Value;
				else
				{
					if (rec.status.Length == 0)
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = rec.status;
				}

				if (rec.defaultWSProvider == null)
					cmd.Parameters.Add("@DefaultWSProvider", SqlDbType.VarChar, 25).Value = DBNull.Value;
				else
				{
					if (rec.defaultWSProvider.Length == 0)
						cmd.Parameters.Add("@DefaultWSProvider", SqlDbType.VarChar, 25).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@DefaultWSProvider", SqlDbType.VarChar, 25).Value = rec.defaultWSProvider;
				}
				cmd.Parameters.Add("@IsNpaNxxReq", SqlDbType.Bit, 0).Value = rec.isNpaNxxReq;				
				
				if (rec.prodSubCategory == null)
					cmd.Parameters.Add("@ProdSubCategory", SqlDbType.VarChar, 25).Value = DBNull.Value;
				else
				{
					if (rec.prodSubCategory.Length == 0)
						cmd.Parameters.Add("@ProdSubCategory", SqlDbType.VarChar, 25).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@ProdSubCategory", SqlDbType.VarChar, 25).Value = rec.prodSubCategory;
				}
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                Vendors rec = new Vendors();
                
                if (rdr["Vendor_id"] != DBNull.Value)
                    rec.vendor_id = (int) rdr["Vendor_id"];
 
                if (rdr["Vendor_name"] != DBNull.Value)
                    rec.vendor_name = (string) rdr["Vendor_name"];
 
                if (rdr["Vendor_address"] != DBNull.Value)
                    rec.vendor_address = (string) rdr["Vendor_address"];
 
                if (rdr["Vendor_city"] != DBNull.Value)
                    rec.vendor_city = (string) rdr["Vendor_city"];
 
                if (rdr["Vendor_state"] != DBNull.Value)
                    rec.vendor_state = (string) rdr["Vendor_state"];
 
                if (rdr["Vendor_zip"] != DBNull.Value)
                    rec.vendor_zip = (string) rdr["Vendor_zip"];
 
                if (rdr["Vendor_phone"] != DBNull.Value)
                    rec.vendor_phone = (string) rdr["Vendor_phone"];
 
                if (rdr["Product_type"] != DBNull.Value)
                    rec.product_type = (string) rdr["Product_type"];
 
                if (rdr["ProdCategory"] != DBNull.Value)
                    rec.prodCategory = (string) rdr["ProdCategory"];

				if (rdr["Status"] != DBNull.Value)
					rec.status = (string) rdr["Status"];

				if (rdr["DefaultWSProvider"] != DBNull.Value)
					rec.defaultWSProvider = (string) rdr["DefaultWSProvider"];

				if (rdr["IsNpaNxxReq"] != DBNull.Value)
					rec.isNpaNxxReq = (bool) rdr["IsNpaNxxReq"];

				if (rdr["ProdSubCategory"] != DBNull.Value)
					rec.prodSubCategory = (string) rdr["ProdSubCategory"];


				rec.rowState = RowState.Clean;
                return rec;
            }
            Vendors[] convert(DomainObj[] objs)
            {
                Vendors[] acls  = new Vendors[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
		#endregion
        }
    }
}
