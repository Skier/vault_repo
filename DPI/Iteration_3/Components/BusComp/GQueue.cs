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
    public class GQueue : DomainObj
    {
        /*        Data        */
        static string iName = "GQueue";
        int id;
        string gQueType;
        string svcProvider;
        string method;
        string xmlMessage;
        string agent;
        string clerkid;
        int predecessor;
        string initiator;
        DateTime startDateTime;
        DateTime lastDateTime;
        int timeInterval;
        DateTime expires;
        int accessCnt;
        int maxCnt;
        string status;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
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
        public string SvcProvider
        {
            get { return svcProvider; }
            set
            {
                setState();
                svcProvider = value;
            }
        }
        public string Method
        {
            get { return method; }
            set
            {
                setState();
                method = value;
            }
        }
        public string XmlMessage
        {
            get { return xmlMessage; }
            set
            {
                setState();
                xmlMessage = value;
            }
        }
        public string Agent
        {
            get { return agent; }
            set
            {
                setState();
                agent = value;
            }
        }
        public string Clerkid
        {
            get { return clerkid; }
            set
            {
                setState();
                clerkid = value;
            }
        }
        public int Predecessor
        {
            get { return predecessor; }
            set
            {
                setState();
                predecessor = value;
            }
        }
        public string Initiator
        {
            get { return initiator; }
            set
            {
                setState();
                initiator = value;
            }
        }
        public DateTime StartDateTime
        {
            get { return startDateTime; }
            set
            {
                setState();
                startDateTime = value;
            }
        }
        public DateTime LastDateTime
        {
            get { return lastDateTime; }
            set
            {
                setState();
                lastDateTime = value;
            }
        }
        public int TimeInterval
        {
            get { return timeInterval; }
            set
            {
                setState();
                timeInterval = value;
            }
        }
        public DateTime Expires
        {
            get { return expires; }
            set
            {
                setState();
                expires = value;
            }
        }
        public int AccessCnt
        {
            get { return accessCnt; }
            set
            {
                setState();
                accessCnt = value;
            }
        }
        public int MaxCnt
        {
            get { return maxCnt; }
            set
            {
                setState();
                maxCnt = value;
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
        
        /*        Constructors			*/
        public GQueue()
        {
            sql = new GQueueSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public GQueue(UOW uow) : this()
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
            return new GQueueSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static GQueue find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(GQueue.getKey(id)))
                return (GQueue)uow.Imap.find(GQueue.getKey(id));
            
            GQueue cls = new GQueue();
            cls.uow = uow;
            cls.id = id;
            cls = (GQueue)DomainObj.addToIMap(uow, getOne(((GQueueSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static GQueue[] getAll(UOW uow)
        {
            GQueue[] objs = (GQueue[])DomainObj.addToIMap(uow, (new GQueueSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
		public static GQueue[] getActive(UOW uow)
		{
			GQueue[] objs = (GQueue[])DomainObj.addToIMap(uow, (new GQueueSQL()).getActive(uow));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
		public void FollowUp(ISvcFactory factory)
		{
			factory.GetProvider(svcProvider).FireAway(Method, null);
		}
		/*		Implementation		*/
        static GQueue getOne(GQueue[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(GQueue src, GQueue tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.gQueType = src.gQueType;
            tar.svcProvider = src.svcProvider;
            tar.method = src.method;
            tar.xmlMessage = src.xmlMessage;
            tar.agent = src.agent;
            tar.clerkid = src.clerkid;
            tar.predecessor = src.predecessor;
            tar.initiator = src.initiator;
            tar.startDateTime = src.startDateTime;
            tar.lastDateTime = src.lastDateTime;
            tar.timeInterval = src.timeInterval;
            tar.expires = src.expires;
            tar.accessCnt = src.accessCnt;
            tar.maxCnt = src.maxCnt;
            tar.status = src.status;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class GQueueSQL : SqlGateway
        {
            public GQueue[] getKey(GQueue rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spGQueue_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                GQueue rec = (GQueue)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spGQueue_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                GQueue rec = (GQueue)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spGQueue_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                GQueue rec = (GQueue)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spGQueue_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public GQueue[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spGQueue_Get_All";
                return convert(execReader(cmd));
            }
			public GQueue[] getActive(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spGQueue_Active";
				return convert(execReader(cmd));
			}
            /*        Implementation        */
            void setParam(SqlCommand cmd, GQueue rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                cmd.Parameters.Add("@GQueType", SqlDbType.VarChar, 25).Value = rec.gQueType;
 
                if (rec.svcProvider == null)
                    cmd.Parameters.Add("@SvcProvider", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.SvcProvider.Length == 0)
                        cmd.Parameters.Add("@SvcProvider", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@SvcProvider", SqlDbType.VarChar, 50).Value = rec.svcProvider;
                }
 
                if (rec.method == null)
                    cmd.Parameters.Add("@Method", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.Method.Length == 0)
                        cmd.Parameters.Add("@Method", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Method", SqlDbType.VarChar, 50).Value = rec.method;
                }
 
                if (rec.xmlMessage == null)
                    cmd.Parameters.Add("@XmlMessage", SqlDbType.VarChar, 300).Value = DBNull.Value;
                else
                {
                    if (rec.XmlMessage.Length == 0)
                        cmd.Parameters.Add("@XmlMessage", SqlDbType.VarChar, 300).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@XmlMessage", SqlDbType.VarChar, 300).Value = rec.xmlMessage;
                }
 
                if (rec.agent == null)
                    cmd.Parameters.Add("@Agent", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.Agent.Length == 0)
                        cmd.Parameters.Add("@Agent", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Agent", SqlDbType.VarChar, 10).Value = rec.agent;
                }
 
                if (rec.clerkid == null)
                    cmd.Parameters.Add("@Clerkid", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.Clerkid.Length == 0)
                        cmd.Parameters.Add("@Clerkid", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Clerkid", SqlDbType.VarChar, 25).Value = rec.clerkid;
                }
                cmd.Parameters.Add("@Predecessor", SqlDbType.Int, 0).Value = rec.predecessor;
 
                if (rec.initiator == null)
                    cmd.Parameters.Add("@Initiator", SqlDbType.VarChar, 100).Value = DBNull.Value;
                else
                {
                    if (rec.Initiator.Length == 0)
                        cmd.Parameters.Add("@Initiator", SqlDbType.VarChar, 100).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Initiator", SqlDbType.VarChar, 100).Value = rec.initiator;
                }
 
                if (rec.startDateTime == DateTime.MinValue)
                    cmd.Parameters.Add("@StartDateTime", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@StartDateTime", SqlDbType.DateTime, 0).Value = rec.startDateTime;
 
                if (rec.lastDateTime == DateTime.MinValue)
                    cmd.Parameters.Add("@LastDateTime", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@LastDateTime", SqlDbType.DateTime, 0).Value = rec.lastDateTime;
                cmd.Parameters.Add("@TimeInterval", SqlDbType.Int, 0).Value = rec.timeInterval;
 
                if (rec.expires == DateTime.MinValue)
                    cmd.Parameters.Add("@Expires", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Expires", SqlDbType.DateTime, 0).Value = rec.expires;
                cmd.Parameters.Add("@AccessCnt", SqlDbType.Int, 0).Value = rec.accessCnt;
                cmd.Parameters.Add("@MaxCnt", SqlDbType.Int, 0).Value = rec.maxCnt;
 
                cmd.Parameters.Add("@Status", SqlDbType.VarChar, 50).Value = rec.status;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                GQueue rec = new GQueue();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["GQueType"] != DBNull.Value)
                    rec.gQueType = (string) rdr["GQueType"];
 
                if (rdr["SvcProvider"] != DBNull.Value)
                    rec.svcProvider = (string) rdr["SvcProvider"];
 
                if (rdr["Method"] != DBNull.Value)
                    rec.method = (string) rdr["Method"];
 
                if (rdr["XmlMessage"] != DBNull.Value)
                    rec.xmlMessage = (string) rdr["XmlMessage"];
 
                if (rdr["Agent"] != DBNull.Value)
                    rec.agent = (string) rdr["Agent"];
 
                if (rdr["Clerkid"] != DBNull.Value)
                    rec.clerkid = (string) rdr["Clerkid"];
 
                if (rdr["Predecessor"] != DBNull.Value)
                    rec.predecessor = (int) rdr["Predecessor"];
 
                if (rdr["Initiator"] != DBNull.Value)
                    rec.initiator = (string) rdr["Initiator"];
 
                if (rdr["StartDateTime"] != DBNull.Value)
                    rec.startDateTime = (DateTime) rdr["StartDateTime"];
 
                if (rdr["LastDateTime"] != DBNull.Value)
                    rec.lastDateTime = (DateTime) rdr["LastDateTime"];
 
                if (rdr["TimeInterval"] != DBNull.Value)
                    rec.timeInterval = (int) rdr["TimeInterval"];
 
                if (rdr["Expires"] != DBNull.Value)
                    rec.expires = (DateTime) rdr["Expires"];
 
                if (rdr["AccessCnt"] != DBNull.Value)
                    rec.accessCnt = (int) rdr["AccessCnt"];
 
                if (rdr["MaxCnt"] != DBNull.Value)
                    rec.maxCnt = (int) rdr["MaxCnt"];
 
                if (rdr["Status"] != DBNull.Value)
                    rec.status = (string) rdr["Status"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            GQueue[] convert(DomainObj[] objs)
            {
                GQueue[] acls  = new GQueue[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
