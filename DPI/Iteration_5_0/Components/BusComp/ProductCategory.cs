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
    public class ProductCategory : DomainObj, IProductCategory
    {
        /*        Data        */
        static string iName = "ProductCategory";
        string prodCategory;
        bool isLocal;
        bool isWireless;
        bool isInternet;
        bool isDebitCard;
		bool isSatellite;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, prodCategory); }
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
        public bool IsLocal
        {
            get { return isLocal; }
            set
            {
                setState();
                isLocal = value;
            }
        }
        public bool IsWireless
        {
            get { return isWireless; }
            set
            {
                setState();
                isWireless = value;
            }
        }
        public bool IsInternet
        {
            get { return isInternet; }
            set
            {
                setState();
                isInternet = value;
            }
        }
        public bool IsDebitCard
        {
            get { return isDebitCard; }
            set
            {
                setState();
                isDebitCard = value;
            }
        }
		public bool IsSatellite
		{
			get { return isSatellite; }
			set
			{
				setState();
				isSatellite = value;
			}
		}
        
        /*        Constructors			*/
        public ProductCategory()
        {
            sql = new ProductCategorySQL();
            rowState = RowState.New;
        }
        public ProductCategory(UOW uow) : this()
        {
            if(uow == null)
                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
            if(uow.Imap == null)
                throw new ArgumentException("IdentityMap is required", "Identity Map");
            
            this.uow = uow;
            this.uow.Imap.add(this);
        }
        
        /*        Methods        */
        protected override SqlGateway loadSql()
        {
            return new ProductCategorySQL();
        }
        public override void checkExists()
        {
            if ((ProdCategory ==  null))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
		public bool Compare(IProductCategory pcat)
		{
			if (pcat.IsDebitCard != this.isDebitCard)
				return false;

			if (pcat.IsInternet!= this.IsInternet)
				return false;

			if (pcat.IsLocal != this.IsLocal)
				return false;

			if (pcat.IsWireless != this.IsWireless)
				return false;

			if (pcat.IsSatellite != this.IsSatellite)
				return false;

			return true;
		}
        public static ProductCategory find(UOW uow, string prodCategory)
        {
            if (uow.Imap.keyExists(ProductCategory.getKey(prodCategory)))
                return (ProductCategory)uow.Imap.find(ProductCategory.getKey(prodCategory));
            
            ProductCategory cls = new ProductCategory();
            cls.uow = uow;
            cls.prodCategory = prodCategory;
            cls = (ProductCategory)DomainObj.addToIMap(uow, getOne(((ProductCategorySQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static ProductCategory[] getAll(UOW uow)
        {
            ProductCategory[] objs = (ProductCategory[])DomainObj.addToIMap(uow, (new ProductCategorySQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(string prodCategory)
        {
            return new Key(iName, prodCategory.ToString());
        }
        /*		Implementation		*/
        static ProductCategory getOne(ProductCategory[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(ProductCategory src, ProductCategory tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.prodCategory = src.prodCategory;
            tar.isLocal = src.isLocal;
            tar.isWireless = src.isWireless;
            tar.isInternet = src.isInternet;
            tar.isDebitCard = src.isDebitCard;
			tar.isSatellite = src.isSatellite;

            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class ProductCategorySQL : SqlGateway
        {
            public ProductCategory[] getKey(ProductCategory rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProductCategory_Get_Id";
                cmd.Parameters.Add("@ProdCategory", SqlDbType.VarChar, 50).Value = rec.prodCategory;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                ProductCategory rec = (ProductCategory)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProductCategory_Ins";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                ProductCategory rec = (ProductCategory)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProductCategory_Del_Id";
                cmd.Parameters.Add("@ProdCategory", SqlDbType.VarChar, 50).Value = rec.prodCategory;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                ProductCategory rec = (ProductCategory)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProductCategory_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public ProductCategory[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spProductCategory_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, ProductCategory rec)
            {
 
                cmd.Parameters.Add("@ProdCategory", SqlDbType.VarChar, 50).Value = rec.prodCategory;
 
                cmd.Parameters.Add("@IsLocal", SqlDbType.Bit, 0).Value = rec.isLocal;
 
                cmd.Parameters.Add("@IsWireless", SqlDbType.Bit, 0).Value = rec.isWireless;
 
                cmd.Parameters.Add("@IsInternet", SqlDbType.Bit, 0).Value = rec.isInternet;
 
                cmd.Parameters.Add("@IsDebitCard", SqlDbType.Bit, 0).Value = rec.isDebitCard;

				cmd.Parameters.Add("@IsSatellite", SqlDbType.Bit, 0).Value = rec.isSatellite;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                ProductCategory rec = new ProductCategory();
                
                if (rdr["ProdCategory"] != DBNull.Value)
                    rec.prodCategory = (string) rdr["ProdCategory"];
 
                if (rdr["IsLocal"] != DBNull.Value)
                    rec.isLocal = (bool) rdr["IsLocal"];
 
                if (rdr["IsWireless"] != DBNull.Value)
                    rec.isWireless = (bool) rdr["IsWireless"];
 
                if (rdr["IsInternet"] != DBNull.Value)
                    rec.isInternet = (bool) rdr["IsInternet"];
 
                if (rdr["IsDebitCard"] != DBNull.Value)
                    rec.isDebitCard = (bool) rdr["IsDebitCard"];

				if (rdr["IsSatellite"] != DBNull.Value)
					rec.isSatellite = (bool) rdr["IsSatellite"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            ProductCategory[] convert(DomainObj[] objs)
            {
                ProductCategory[] acls  = new ProductCategory[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
