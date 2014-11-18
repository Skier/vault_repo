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
    public class CorpIP : DomainObj
    {
        /*        Data        */
        static string iName = "CorpIP";
        string publicIP;
        int corpId;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, publicIP); }
        }
        public string PublicIP
        {
            get { return publicIP; }
            set
            {
                setState();
                publicIP = value;
            }
        }
        public int CorpId
        {
            get { return corpId; }
            set
            {
                setState();
                corpId = value;
            }
        }
        
        /*        Constructors			*/
        public CorpIP()
        {
            sql = new CorpIPSQL();
            rowState = RowState.New;
        }
        public CorpIP(UOW uow) : this()
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
            return new CorpIPSQL();
        }
        public override void checkExists()
        {
            if ((PublicIP ==  null))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static CorpIP find(UOW uow, string publicIP)
        {
            if (uow.Imap.keyExists(CorpIP.getKey(publicIP)))
                return (CorpIP)uow.Imap.find(CorpIP.getKey(publicIP));
            
            CorpIP cls = new CorpIP();
            cls.uow = uow;
            cls.publicIP = publicIP;
            cls = (CorpIP)DomainObj.addToIMap(uow, getOne(((CorpIPSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static CorpIP[] getAll(UOW uow)
        {
            CorpIP[] objs = (CorpIP[])DomainObj.addToIMap(uow, (new CorpIPSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(string publicIP)
        {
            return new Key(iName, publicIP.ToString());
        }
        /*		Implementation		*/
        static CorpIP getOne(CorpIP[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(CorpIP src, CorpIP tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.publicIP = src.publicIP;
            tar.corpId = src.corpId;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class CorpIPSQL : SqlGateway
        {
            public CorpIP[] getKey(CorpIP rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCorpIP_Get_Id";
                cmd.Parameters.Add("@PublicIP", SqlDbType.VarChar, 15).Value = rec.publicIP;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                CorpIP rec = (CorpIP)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCorpIP_Ins";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                CorpIP rec = (CorpIP)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCorpIP_Del_Id";
                cmd.Parameters.Add("@PublicIP", SqlDbType.VarChar, 15).Value = rec.publicIP;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                CorpIP rec = (CorpIP)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCorpIP_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public CorpIP[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spCorpIP_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, CorpIP rec)
            {
 
                cmd.Parameters.Add("@PublicIP", SqlDbType.VarChar, 15).Value = rec.publicIP;
                
                // Numeric, nullable foreign key treatment:
                if (rec.CorpId == 0)
                    cmd.Parameters.Add("@CorpId", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@CorpId", SqlDbType.Int, 0).Value = rec.corpId;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                CorpIP rec = new CorpIP();
                
                if (rdr["PublicIP"] != DBNull.Value)
                    rec.publicIP = (string) rdr["PublicIP"];
 
                if (rdr["CorpId"] != DBNull.Value)
                    rec.corpId = (int) rdr["CorpId"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            CorpIP[] convert(DomainObj[] objs)
            {
                CorpIP[] acls  = new CorpIP[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
