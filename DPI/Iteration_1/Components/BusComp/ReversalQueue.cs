//using System;
//using System.Collections;
//using System.Data;
//using System.Text;
//using System.Windows.Forms;
//using System.Data.SqlClient;
//using System.Data.SqlTypes;
//using System.Xml;
//
//using DPI.Components;
//using DPI.Interfaces;
// 
//namespace DPI.Components
//{
//    [Serializable]
//    public class ReversalQueue : DomainObj
//    {
//        /*        Data        */ 
//        static string iName = "ReversalQueue";
//        int id;
//        string reversalType;
//        int reversalProvider;
//        DateTime initDateTime;
//        DateTime compDateTime;
//        int attemps;
//        int demand;
//        string reasonCode;
//        string reasonText;
//        string status;
//        string xml;
//        
//        /*        Properties        */
//        public override IDomKey IKey 
//        {
//             get { return new Key(iName, id.ToString()); }
//        }
//        public int Id
//        {
//            get { return id; }
//        }
//        public string ReversalType
//        {
//            get { return reversalType; }
//            set
//            {
//                setState();
//                reversalType = value;
//            }
//        }
//        public int ReversalProvider
//        {
//            get { return reversalProvider; }
//            set
//            {
//                setState();
//                reversalProvider = value;
//            }
//        }
//        public DateTime InitDateTime
//        {
//            get { return initDateTime; }
//            set
//            {
//                setState();
//                initDateTime = value;
//            }
//        }
//        public DateTime CompDateTime
//        {
//            get { return compDateTime; }
//            set
//            {
//                setState();
//                compDateTime = value;
//            }
//        }
//        public int Attemps
//        {
//            get { return attemps; }
//            set
//            {
//                setState();
//                attemps = value;
//            }
//        }
//        public int Demand
//        {
//            get { return demand; }
//            set
//            {
//                setState();
//                demand = value;
//            }
//        }
//        public string ReasonCode
//        {
//            get { return reasonCode; }
//            set
//            {
//                setState();
//                reasonCode = value;
//            }
//        }
//        public string ReasonText
//        {
//            get { return reasonText; }
//            set
//            {
//                setState();
//                reasonText = value;
//            }
//        }
//        public string Status
//        {
//            get { return status; }
//            set
//            {
//                setState();
//                status = value;
//            }
//        }
//        public string Xml
//        {
//            get { return xml; }
//            set
//            {
//                setState();
//                xml = value;
//            }
//        }
//        
//        /*        Constructors			*/
//        public ReversalQueue()
//        {
//            sql = new ReversalQueueSQL();
//            id = random.Next(Int32.MinValue, -1);
//            rowState = RowState.New;
//			priority = 20000;
//			initDateTime = DateTime.Now;
//        }
//        public ReversalQueue(UOW uow) : this()
//        {
//            if(uow == null)
//                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
//            
//            if(uow.Imap == null)
//                throw new ArgumentException("IdentityMap is required", "Identity Map");
//            
//            this.uow = uow;
//            this.uow.Imap.add(this);
//        }
//		public ReversalQueue(UOW uow, string reversalType) : this(uow)
//		{
//			this.reversalType = reversalType;
//		}
//		public ReversalQueue(UOW uow,  string reversalType, IDemand demand, XmlNode node)
//						: this(uow, reversalType)
//		{
//			this.demand = demand.Id;
//			this.reversalProvider = DebitCardFactory.GetProviderId(demand);
//			this.status = "Active";		
//			
//			if (node != null)
//				 SetupResponse(node, demand);
//		}        
//        /*        Methods        */
//		void SetupResponse(XmlNode node, IDemand demand)
//		{
//			IDebitCardResponse resp = DebitCardFactory.GetRespObj(demand, node);
//			
//			this.reasonCode = resp.RespCode;
//			this.reasonText = resp.RespText;
//			this.xml = node.OuterXml;
//		}
//        protected override SqlGateway loadSql()
//        {
//            return new ReversalQueueSQL();
//        }
//        public override void checkExists()
//        {
//            if ((Id < 1))
//                throw new ArgumentException("Valid row is required for update and delete");
//        }
//        /*		Static methods		*/
//        public static ReversalQueue find(UOW uow, int id)
//        {
//            if (uow.Imap.keyExists(ReversalQueue.getKey(id)))
//                return (ReversalQueue)uow.Imap.find(ReversalQueue.getKey(id));
//            
//            ReversalQueue cls = new ReversalQueue();
//            cls.uow = uow;
//            cls.id = id;
//            cls = (ReversalQueue)DomainObj.addToIMap(uow, getOne(((ReversalQueueSQL)cls.Sql).getKey(cls)));
//            cls.uow = uow;
//            
//            return cls;
//        }
//        public static ReversalQueue[] getAll(UOW uow)
//        {
//            ReversalQueue[] objs = (ReversalQueue[])DomainObj.addToIMap(uow, (new ReversalQueueSQL()).getAll(uow));
//            for (int i = 0; i < objs.Length; i++)
//                objs[i].uow = uow;
//            return objs;
//        }
//        public static Key getKey(int id)
//        {
//            return new Key(iName, id.ToString());
//        }
//        /*		Implementation		*/
//        static ReversalQueue getOne(ReversalQueue[] acls)
//        {
//            if (acls.Length == 1)
//                return acls[0];
//            
//            if (acls.Length == 0)
//                throw new ArgumentException("Row not found");
//            
//            throw new ArgumentException("More than one row found");
//        }
//        static void copyAttrs(ReversalQueue src, ReversalQueue tar)
//        {
//            if (tar == null)
//                throw new ArgumentNullException("Target object must not be null");
//            
//            if (src == null)
//                throw new ArgumentNullException("Source object must not be null");
//            
//            tar.id = src.id;
//            tar.reversalType = src.reversalType;
//            tar.reversalProvider = src.reversalProvider;
//            tar.initDateTime = src.initDateTime;
//            tar.compDateTime = src.compDateTime;
//            tar.attemps = src.attemps;
//            tar.demand = src.demand;
//            tar.reasonCode = src.reasonCode;
//            tar.reasonText = src.reasonText;
//            tar.status = src.status;
//            tar.xml = src.xml;
//            tar.rowState = src.rowState;
//        }
// 
//        /*		SQL		*/
//        [Serializable]
//        class ReversalQueueSQL : SqlGateway
//        {
//            public ReversalQueue[] getKey(ReversalQueue rec)
//            {
//                SqlCommand cmd = getCommand(rec);
//                cmd.CommandText = "spReversalQueue_Get_Id";
//                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
//                return convert(execReader(cmd));
//            }
//            public override void insert(DomainObj obj)
//            {
//                ReversalQueue rec = (ReversalQueue)obj;
//                SqlCommand cmd = getCommand(rec);
//                cmd.CommandText = "spReversalQueue_Ins";
//                setParam(cmd, rec);
//                
//                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
//                execScalar(cmd);
//                rec.id = (int)cmd.Parameters["@Id"].Value;
//                rec.rowState = RowState.Clean;
//            }
//            public override void delete(DomainObj obj)
//            {
//                ReversalQueue rec = (ReversalQueue)obj;
//                SqlCommand cmd = getCommand(rec);
//                cmd.CommandText = "spReversalQueue_Del_Id";
//                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
//                
//                execScalar(cmd);
//                rec.rowState = RowState.Deleted;
//            }
//            public override void update(DomainObj obj)
//            {
//                ReversalQueue rec = (ReversalQueue)obj;
//                SqlCommand cmd = getCommand(rec);
//                cmd.CommandText = "spReversalQueue_Upd";
//                setParam(cmd, rec);
//                
//                execScalar(cmd);
//                rec.rowState = RowState.Clean;
//            }
//            public ReversalQueue[] getAll(UOW uow)
//            {
//                SqlCommand cmd = makeCommand(uow);
//                cmd.CommandText = "spReversalQueue_Get_All";
//                return convert(execReader(cmd));
//            }
//            /*        Implementation        */
//            void setParam(SqlCommand cmd, ReversalQueue rec)
//            {
//                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
// 
//                cmd.Parameters.Add("@ReversalType", SqlDbType.VarChar, 25).Value = rec.reversalType;
//                cmd.Parameters.Add("@ReversalProvider", SqlDbType.Int, 0).Value = rec.reversalProvider;
// 
//                if (rec.initDateTime == DateTime.MinValue)
//                    cmd.Parameters.Add("@InitDateTime", SqlDbType.DateTime, 0).Value = DBNull.Value;
//                else
//                    cmd.Parameters.Add("@InitDateTime", SqlDbType.DateTime, 0).Value = rec.initDateTime;
// 
//                if (rec.compDateTime == DateTime.MinValue)
//                    cmd.Parameters.Add("@CompDateTime", SqlDbType.DateTime, 0).Value = DBNull.Value;
//                else
//                    cmd.Parameters.Add("@CompDateTime", SqlDbType.DateTime, 0).Value = rec.compDateTime;
//                cmd.Parameters.Add("@Attemps", SqlDbType.Int, 0).Value = rec.attemps;
//                cmd.Parameters.Add("@Demand", SqlDbType.Int, 0).Value = rec.demand;
// 
//                if (rec.reasonCode == null)
//                    cmd.Parameters.Add("@ReasonCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
//                else
//                {
//                    if (rec.ReasonCode.Length == 0)
//                        cmd.Parameters.Add("@ReasonCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
//                    else
//                        cmd.Parameters.Add("@ReasonCode", SqlDbType.VarChar, 10).Value = rec.reasonCode;
//                }
// 
//                if (rec.reasonText == null)
//                    cmd.Parameters.Add("@ReasonText", SqlDbType.VarChar, 250).Value = DBNull.Value;
//                else
//                {
//                    if (rec.ReasonText.Length == 0)
//                        cmd.Parameters.Add("@ReasonText", SqlDbType.VarChar, 250).Value = DBNull.Value;
//                    else
//                        cmd.Parameters.Add("@ReasonText", SqlDbType.VarChar, 250).Value = rec.reasonText;
//                }
// 
//                if (rec.status == null)
//                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
//                else
//                {
//                    if (rec.Status.Length == 0)
//                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
//                    else
//                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = rec.status;
//                }
// 
//                if (rec.xml == null)
//                    cmd.Parameters.Add("@Xml", SqlDbType.VarChar, 4000).Value = DBNull.Value;
//                else
//                {
//                    if (rec.Xml.Length == 0)
//                        cmd.Parameters.Add("@Xml", SqlDbType.VarChar, 4000).Value = DBNull.Value;
//                    else
//                        cmd.Parameters.Add("@Xml", SqlDbType.VarChar, 4000).Value = rec.xml;
//                }
//            }
//            protected override DomainObj reader(SqlDataReader rdr)
//            {
//                ReversalQueue rec = new ReversalQueue();
//                
//                if (rdr["Id"] != DBNull.Value)
//                    rec.id = (int) rdr["Id"];
// 
//                if (rdr["ReversalType"] != DBNull.Value)
//                    rec.reversalType = (string) rdr["ReversalType"];
// 
//                if (rdr["ReversalProvider"] != DBNull.Value)
//                    rec.reversalProvider = (int) rdr["ReversalProvider"];
// 
//                if (rdr["InitDateTime"] != DBNull.Value)
//                    rec.initDateTime = (DateTime) rdr["InitDateTime"];
// 
//                if (rdr["CompDateTime"] != DBNull.Value)
//                    rec.compDateTime = (DateTime) rdr["CompDateTime"];
// 
//                if (rdr["Attemps"] != DBNull.Value)
//                    rec.attemps = (int) rdr["Attemps"];
// 
//                if (rdr["Demand"] != DBNull.Value)
//                    rec.demand = (int) rdr["Demand"];
// 
//                if (rdr["ReasonCode"] != DBNull.Value)
//                    rec.reasonCode = (string) rdr["ReasonCode"];
// 
//                if (rdr["ReasonText"] != DBNull.Value)
//                    rec.reasonText = (string) rdr["ReasonText"];
// 
//                if (rdr["Status"] != DBNull.Value)
//                    rec.status = (string) rdr["Status"];
// 
//                if (rdr["Xml"] != DBNull.Value)
//                    rec.xml = (string) rdr["Xml"];
// 
//                rec.rowState = RowState.Clean;
//                return rec;
//            }
//            ReversalQueue[] convert(DomainObj[] objs)
//            {
//                ReversalQueue[] acls  = new ReversalQueue[objs.Length];
//                objs.CopyTo(acls, 0);
//                return  acls;
//            }
//        }
//    }
//}
