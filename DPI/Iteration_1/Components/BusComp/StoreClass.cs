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
    public class StoreClass : DomainObj
    {
        /*        Data        */
        static string iName = "StoreClass";
        string storeCls;
        string isDirectSeller;
        string isPymtStation;
        string isPriceLookup;
        string description;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, storeCls); }
        }
        public string StoreCls
        {
            get { return storeCls; }
            set
            {
                setState();
                storeCls = value;
            }
        }
        public string IsDirectSeller
        {
            get { return isDirectSeller; }
            set
            {
                setState();
                isDirectSeller = value;
            }
        }
        public string IsPymtStation
        {
            get { return isPymtStation; }
            set
            {
                setState();
                isPymtStation = value;
            }
        }
        public string IsPriceLookup
        {
            get { return isPriceLookup; }
            set
            {
                setState();
                isPriceLookup = value;
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                setState();
                description = value;
            }
        }
        
        /*        Constructors			*/
        public StoreClass()
        {
            sql = new StoreClassSQL();
            rowState = RowState.New;
        }
        public StoreClass(UOW uow) : this()
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
            return new StoreClassSQL();
        }
        public override void checkExists()
        {
            if ((StoreCls ==  null))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static StoreClass find(UOW uow, string storeCls)
        {
            if (uow.Imap.keyExists(StoreClass.getKey(storeCls)))
                return (StoreClass)uow.Imap.find(StoreClass.getKey(storeCls));
            
            StoreClass cls = new StoreClass();
            cls.uow = uow;
            cls.storeCls = storeCls;
            cls = (StoreClass)DomainObj.addToIMap(uow, getOne(((StoreClassSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static StoreClass[] getAll(UOW uow)
        {
            StoreClass[] objs = (StoreClass[])DomainObj.addToIMap(uow, (new StoreClassSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(string storeCls)
        {
            return new Key(iName, storeCls.ToString());
        }
        /*		Implementation		*/
        static StoreClass getOne(StoreClass[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(StoreClass src, StoreClass tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.storeCls = src.storeCls;
            tar.isDirectSeller = src.isDirectSeller;
            tar.isPymtStation = src.isPymtStation;
            tar.isPriceLookup = src.isPriceLookup;
            tar.description = src.description;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class StoreClassSQL : SqlGateway
        {
            public StoreClass[] getKey(StoreClass rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spStoreClass_Get_Id";
                cmd.Parameters.Add("@StoreCls", SqlDbType.VarChar, 10).Value = rec.storeCls;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                StoreClass rec = (StoreClass)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spStoreClass_Ins";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                StoreClass rec = (StoreClass)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spStoreClass_Del_Id";
                cmd.Parameters.Add("@StoreCls", SqlDbType.VarChar, 10).Value = rec.storeCls;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                StoreClass rec = (StoreClass)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spStoreClass_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public StoreClass[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spStoreClass_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, StoreClass rec)
            {
				cmd.Parameters.Add("@StoreCls", SqlDbType.VarChar, 10).Value = rec.storeCls;
				
				if (rec.isDirectSeller == null)
					cmd.Parameters.Add("@IsDirectSeller", SqlDbType.Char, 1).Value =  DBNull.Value;
				else
					if (rec.isDirectSeller.Length == 0)
						cmd.Parameters.Add("@IsDirectSeller", SqlDbType.Char, 1).Value =  DBNull.Value;
					else
			            cmd.Parameters.Add("@IsDirectSeller", SqlDbType.Char, 1).Value = rec.isDirectSeller;
 
                if (rec.isPymtStation == null)
					cmd.Parameters.Add("@IsPymtStation", SqlDbType.Char, 1).Value = DBNull.Value;
				else
					if (rec.isPymtStation.Length == 0)
						cmd.Parameters.Add("@IsPymtStation", SqlDbType.Char, 1).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@IsPymtStation", SqlDbType.Char, 1).Value = rec.isPymtStation;
 
        		cmd.Parameters.Add("@IsPriceLookup", SqlDbType.Char, 1).Value = rec.isPriceLookup;
 
                if (rec.description == null)
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar, 250).Value = DBNull.Value;
                else
                {
                    if (rec.Description.Length == 0)
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 250).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 250).Value = rec.description;
                }
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                StoreClass rec = new StoreClass();
                
                if (rdr["StoreCls"] != DBNull.Value)
                    rec.storeCls = (string) rdr["StoreCls"];
 
                if (rdr["IsDirectSeller"] != DBNull.Value)
                    rec.isDirectSeller = ((string) rdr["IsDirectSeller"]).Trim();
 
                if (rdr["IsPymtStation"] != DBNull.Value)
                    rec.isPymtStation = ((string) rdr["IsPymtStation"]).Trim();
 
                if (rdr["IsPriceLookup"] != DBNull.Value)
                    rec.isPriceLookup = ((string) rdr["IsPriceLookup"]).Trim();
 
                if (rdr["Description"] != DBNull.Value)
                    rec.description = (string) rdr["Description"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            StoreClass[] convert(DomainObj[] objs)
            {
                StoreClass[] acls  = new StoreClass[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
