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
    public class PriceCat : DomainObj
    {
        /*        Data        */
        static string iName = "PriceCat";
        string category;
        bool isSupplierReq;
        bool isOrderTypeReq;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, category); }
        }
        public string Category
        {
            get { return category; }
            set
            {
                setState();
                category = value;
            }
        }
        public bool IsSupplierReq
        {
            get { return isSupplierReq; }
            set
            {
                setState();
                isSupplierReq = value;
            }
        }
        public bool IsOrderTypeReq
        {
            get { return isOrderTypeReq; }
            set
            {
                setState();
                isOrderTypeReq = value;
            }
        }
        
        /*        Constructors			*/
        public PriceCat()
        {
            sql = new PriceCatSQL();
            rowState = RowState.New;
        }
        public PriceCat(UOW uow) : this()
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
            return new PriceCatSQL();
        }
        public override void checkExists()
        {
            if ((Category ==  null))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static PriceCat find(UOW uow, string category)
        {
            if (uow.Imap.keyExists(PriceCat.getKey(category)))
                return (PriceCat)uow.Imap.find(PriceCat.getKey(category));
            
            PriceCat cls = new PriceCat();
            cls.uow = uow;
            cls.category = category;
            cls = (PriceCat)DomainObj.addToIMap(uow, getOne(((PriceCatSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static PriceCat[] getAll(UOW uow)
        {
            PriceCat[] objs = (PriceCat[])DomainObj.addToIMap(uow, (new PriceCatSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(string category)
        {
            return new Key(iName, category.ToString());
        }
        /*		Implementation		*/
        static PriceCat getOne(PriceCat[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(PriceCat src, PriceCat tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.category = src.category;
            tar.isSupplierReq = src.isSupplierReq;
            tar.isOrderTypeReq = src.isOrderTypeReq;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class PriceCatSQL : SqlGateway
        {
            public PriceCat[] getKey(PriceCat rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPriceCat_Get_Id";
                cmd.Parameters.Add("@Category", SqlDbType.VarChar, 50).Value = rec.category;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                PriceCat rec = (PriceCat)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPriceCat_Ins";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                PriceCat rec = (PriceCat)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPriceCat_Del_Id";
                cmd.Parameters.Add("@Category", SqlDbType.VarChar, 50).Value = rec.category;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                PriceCat rec = (PriceCat)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPriceCat_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public PriceCat[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spPriceCat_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, PriceCat rec)
            {
 
                cmd.Parameters.Add("@Category", SqlDbType.VarChar, 50).Value = rec.category;
 
                cmd.Parameters.Add("@IsSupplierReq", SqlDbType.Char, 1).Value = (rec.isSupplierReq == true) ? "T" : "F";
 
                cmd.Parameters.Add("@IsOrderTypeReq", SqlDbType.Char, 1).Value = (rec.isOrderTypeReq == true) ? "T" : "F";
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                PriceCat rec = new PriceCat();
                
                if (rdr["Category"] != DBNull.Value)
                    rec.category = (string) rdr["Category"];
 
                if (rdr["IsSupplierReq"] != DBNull.Value)
                    rec.isSupplierReq = (string) rdr["IsSupplierReq"] == "T" ?  true : false;
 
                if (rdr["IsOrderTypeReq"] != DBNull.Value)
                    rec.isOrderTypeReq = (string) rdr["IsOrderTypeReq"] == "T" ?  true : false;
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            PriceCat[] convert(DomainObj[] objs)
            {
                PriceCat[] acls  = new PriceCat[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
