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
    public class GQueueType : DomainObj
    {
        /*        Data        */
        static string iName = "GQueueType";
        string gQueType;
        bool isEvergreen;
        bool isToComplition;
        bool isOneShot;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, gQueType); }
        }
        public string GQueType
        {
            get { return gQueType; }
            set
            {
                setState();
                gQueType = value;
            }
        }
        public bool IsEvergreen
        {
            get { return isEvergreen; }
            set
            {
                setState();
                isEvergreen = value;
            }
        }
        public bool IsToComplition
        {
            get { return isToComplition; }
            set
            {
                setState();
                isToComplition = value;
            }
        }
        public bool IsOneShot
        {
            get { return isOneShot; }
            set
            {
                setState();
                isOneShot = value;
            }
        }
        
        /*        Constructors			*/
        public GQueueType()
        {
            sql = new GQueueTypeSQL();
            rowState = RowState.New;
        }
        public GQueueType(UOW uow) : this()
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
            return new GQueueTypeSQL();
        }
        public override void checkExists()
        {
            if ((GQueType ==  null))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static GQueueType find(UOW uow, string gQueType)
        {
            if (uow.Imap.keyExists(GQueueType.getKey(gQueType)))
                return (GQueueType)uow.Imap.find(GQueueType.getKey(gQueType));
            
            GQueueType cls = new GQueueType();
            cls.uow = uow;
            cls.gQueType = gQueType;
            cls = (GQueueType)DomainObj.addToIMap(uow, getOne(((GQueueTypeSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static GQueueType[] getAll(UOW uow)
        {
            GQueueType[] objs = (GQueueType[])DomainObj.addToIMap(uow, (new GQueueTypeSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(string gQueType)
        {
            return new Key(iName, gQueType.ToString());
        }
        /*		Implementation		*/
        static GQueueType getOne(GQueueType[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(GQueueType src, GQueueType tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.gQueType = src.gQueType;
            tar.isEvergreen = src.isEvergreen;
            tar.isToComplition = src.isToComplition;
            tar.isOneShot = src.isOneShot;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class GQueueTypeSQL : SqlGateway
        {
            public GQueueType[] getKey(GQueueType rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spGQueueType_Get_Id";
                cmd.Parameters.Add("@GQueType", SqlDbType.VarChar, 50).Value = rec.gQueType;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                GQueueType rec = (GQueueType)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spGQueueType_Ins";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                GQueueType rec = (GQueueType)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spGQueueType_Del_Id";
                cmd.Parameters.Add("@GQueType", SqlDbType.VarChar, 50).Value = rec.gQueType;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                GQueueType rec = (GQueueType)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spGQueueType_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public GQueueType[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spGQueueType_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, GQueueType rec)
            {
 
                cmd.Parameters.Add("@GQueType", SqlDbType.VarChar, 50).Value = rec.gQueType;
 
                cmd.Parameters.Add("@IsEvergreen", SqlDbType.Bit, 0).Value = rec.isEvergreen;
 
                cmd.Parameters.Add("@IsToComplition", SqlDbType.Bit, 0).Value = rec.isToComplition;
 
                cmd.Parameters.Add("@IsOneShot", SqlDbType.Bit, 0).Value = rec.isOneShot;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                GQueueType rec = new GQueueType();
                
                if (rdr["GQueType"] != DBNull.Value)
                    rec.gQueType = (string) rdr["GQueType"];
 
                if (rdr["IsEvergreen"] != DBNull.Value)
                    rec.isEvergreen = (bool) rdr["IsEvergreen"];
 
                if (rdr["IsToComplition"] != DBNull.Value)
                    rec.isToComplition = (bool) rdr["IsToComplition"];
 
                if (rdr["IsOneShot"] != DBNull.Value)
                    rec.isOneShot = (bool) rdr["IsOneShot"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            GQueueType[] convert(DomainObj[] objs)
            {
                GQueueType[] acls  = new GQueueType[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
