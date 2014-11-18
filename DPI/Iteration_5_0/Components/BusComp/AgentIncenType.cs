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
    public class AgentIncenType : DomainObj, IAgentIncenType
    {
        /*        Data        */
        static string iName = "AgentIncenType";
        string incentiveType;
        bool isRegReq;
        bool isEarlyRegAllowed;
        bool isOnePerStore;
        bool isOnePerPeriod;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, incentiveType); }
        }
        public string IncentiveType
        {
            get { return incentiveType; }
            set
            {
                setState();
                incentiveType = value;
            }
        }
        public bool IsRegReq
        {
            get { return isRegReq; }
            set
            {
                setState();
                isRegReq = value;
            }
        }
        public bool IsEarlyRegAllowed
        {
            get { return isEarlyRegAllowed; }
            set
            {
                setState();
                isEarlyRegAllowed = value;
            }
        }
        public bool IsOnePerStore
        {
            get { return isOnePerStore; }
            set
            {
                setState();
                isOnePerStore = value;
            }
        }
        public bool IsOnePerPeriod
        {
            get { return isOnePerPeriod; }
            set
            {
                setState();
                isOnePerPeriod = value;
            }
        }
        
        /*        Constructors			*/
        public AgentIncenType()
        {
            sql = new AgentIncenTypeSQL();
            rowState = RowState.New;
        }
        public AgentIncenType(UOW uow) : this()
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
            return new AgentIncenTypeSQL();
        }
        public override void checkExists()
        {
            if ((IncentiveType ==  null))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static AgentIncenType find(UOW uow, string incentiveType)
        {
            if (uow.Imap.keyExists(AgentIncenType.getKey(incentiveType)))
                return (AgentIncenType)uow.Imap.find(AgentIncenType.getKey(incentiveType));
            
            AgentIncenType cls = new AgentIncenType();
            cls.uow = uow;
            cls.incentiveType = incentiveType;
            cls = (AgentIncenType)DomainObj.addToIMap(uow, getOne(((AgentIncenTypeSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static AgentIncenType[] getAll(UOW uow)
        {
            AgentIncenType[] objs = (AgentIncenType[])DomainObj.addToIMap(uow, (new AgentIncenTypeSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(string incentiveType)
        {
            return new Key(iName, incentiveType.ToString());
        }
        /*		Implementation		*/
        static AgentIncenType getOne(AgentIncenType[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(AgentIncenType src, AgentIncenType tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.incentiveType = src.incentiveType;
            tar.isRegReq = src.isRegReq;
            tar.isEarlyRegAllowed = src.isEarlyRegAllowed;
            tar.isOnePerStore = src.isOnePerStore;
            tar.isOnePerPeriod = src.isOnePerPeriod;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class AgentIncenTypeSQL : SqlGateway
        {
            public AgentIncenType[] getKey(AgentIncenType rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAgentIncenType_Get_Id";
                cmd.Parameters.Add("@IncentiveType", SqlDbType.VarChar, 50).Value = rec.incentiveType;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                AgentIncenType rec = (AgentIncenType)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAgentIncenType_Ins";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                AgentIncenType rec = (AgentIncenType)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAgentIncenType_Del_Id";
                cmd.Parameters.Add("@IncentiveType", SqlDbType.VarChar, 50).Value = rec.incentiveType;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                AgentIncenType rec = (AgentIncenType)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAgentIncenType_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public AgentIncenType[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spAgentIncenType_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, AgentIncenType rec)
            {
 
                cmd.Parameters.Add("@IncentiveType", SqlDbType.VarChar, 50).Value = rec.incentiveType;
 
                cmd.Parameters.Add("@IsRegReq", SqlDbType.Bit, 0).Value = rec.isRegReq;
 
                cmd.Parameters.Add("@IsEarlyRegAllowed", SqlDbType.Bit, 0).Value = rec.isEarlyRegAllowed;
 
                cmd.Parameters.Add("@IsOnePerStore", SqlDbType.Bit, 0).Value = rec.isOnePerStore;
 
                cmd.Parameters.Add("@IsOnePerPeriod", SqlDbType.Bit, 0).Value = rec.isOnePerPeriod;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                AgentIncenType rec = new AgentIncenType();
                
                if (rdr["IncentiveType"] != DBNull.Value)
                    rec.incentiveType = (string) rdr["IncentiveType"];
 
                if (rdr["IsRegReq"] != DBNull.Value)
                    rec.isRegReq = (bool) rdr["IsRegReq"];
 
                if (rdr["IsEarlyRegAllowed"] != DBNull.Value)
                    rec.isEarlyRegAllowed = (bool) rdr["IsEarlyRegAllowed"];
 
                if (rdr["IsOnePerStore"] != DBNull.Value)
                    rec.isOnePerStore = (bool) rdr["IsOnePerStore"];
 
                if (rdr["IsOnePerPeriod"] != DBNull.Value)
                    rec.isOnePerPeriod = (bool) rdr["IsOnePerPeriod"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            AgentIncenType[] convert(DomainObj[] objs)
            {
                AgentIncenType[] acls  = new AgentIncenType[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
