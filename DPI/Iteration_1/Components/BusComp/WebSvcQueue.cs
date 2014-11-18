using System;
using System.Data;
using System.Data.SqlClient;
using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Components
{
    [Serializable]
    public class WebSvcQueue : DomainObj, IWebSvcQueue
    {
	#region Data
        static string iName = "WebSvcQueue";

		IId dom;

        int id;
        string queType;
        string wSProvider;
        string webMethod;
        string reversalMethod;
        string storeCode;
        string clerkId;
        string busObject;
        string busObjId;
        DateTime initDate;
        DateTime lastAccessDate;
        string initReasonCode;
        string lastReasonCode;
        string initialMsg;
        string lastMsg;
        int attemps;
        string xml;
        string reversalXml;
        string status;
		string initRespXml;
		string lastRespXml;
		
	#endregion
		
	#region Properties
        
		public IId Dom
		{
			get { return dom; }
			set { dom = value; }
		}
    	public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
        }
        public string QueType
        {
            get { return queType; }
            set
            {
                setState();
                queType = value;
            }
        }
        public string WSProvider
        {
            get { return wSProvider; }
            set
            {
                setState();
                wSProvider = value;
            }
        }
        public string WebMethod
        {
            get { return webMethod; }
            set
            {
                setState();
                webMethod = value;
            }
        }
        public string ReversalMethod
        {
            get { return reversalMethod; }
            set
            {
                setState();
                reversalMethod = value;
            }
        }
        public string StoreCode
        {
            get { return storeCode; }
            set
            {
                setState();
                storeCode = value;
            }
        }
        public string ClerkId
        {
            get { return clerkId; }
            set
            {
                setState();
                clerkId = value;
            }
        }
        public string BusObject
        {
            get { return busObject; }
            set
            {
                setState();
                busObject = value;
            }
        }
        public string BusObjId
        {
            get { return busObjId; }
            set
            {
                setState();
                busObjId = value;
            }
        }
        public DateTime InitDate
        {
            get { return initDate; }
            set
            {
                setState();
                initDate = value;
            }
        }
        public DateTime LastAccessDate
        {
            get { return lastAccessDate; }
            set
            {
                setState();
                lastAccessDate = value;
            }
        }
        public string InitReasonCode
        {
            get { return initReasonCode; }
            set
            {
                setState();
                initReasonCode = value;
            }
        }
        public string LastReasonCode
        {
            get { return lastReasonCode; }
            set
            {
                setState();
                lastReasonCode = value;
            }
        }
        public string InitialMsg
        {
            get { return initialMsg; }
            set
            {
                setState();
                initialMsg = value;
            }
        }
        public string LastMsg
        {
            get { return lastMsg; }
            set
            {
                setState();
                lastMsg = value;
            }
        }
        public int Attemps { get { return attemps; }}
        public string Xml
        {
            get { return xml; }
            set
            {
                setState();
                xml = value;
            }
        }
        public string ReversalXml
        {
            get { return reversalXml; }
            set
            {
                setState();
                reversalXml = value;
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
		public string InitRespXml
		{ 
			get { return initRespXml;  }
			set 
			{ 
				setState();
				initRespXml = value; 
			}
		}
		public string LastRespXml
		{ 
			get { return lastRespXml;  } 
			set 
			{ 
				setState();
				lastRespXml = value; 
			} 
		}
       		
	#endregion
		
	#region Constructors
        
		public WebSvcQueue()
		{
			sql = new WebSvcQueueSQL();
			id = random.Next(Int32.MinValue, -1);
			rowState = RowState.New;

			priority = 50000;
			this.initDate = DateTime.Now;
			this.attemps = 1;
		} 
		public WebSvcQueue(IMap imap) : this()
		{
			if(imap == null)
				throw new ArgumentException("IdentityMap is required", "Identity Map");
            
			imap.add(this);
		}
        public WebSvcQueue(UOW uow) : this(uow.Imap)
        {
            if(uow == null)
                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");

			this.uow = uow;
        }
        
       
	#endregion
		
	#region Methods

		public void FollowUp()
		{
			WSProviderFactory.GetGateway(wSProvider.Trim().ToLower()).ProcessQueue(this);
		}		
		protected override void setState()
		{
			base.setState();
			
			if (this.id > 0)
			{
				lastAccessDate = DateTime.Now;
				this.attemps++;
			}
		}
        protected override SqlGateway loadSql()
        {
            return new WebSvcQueueSQL();
        }
		public void SaveEntry()
		{
			try
			{
				Uow = new UOW();
				uow.commit();
			}
			catch {}
			finally { uow.close(); }
		}
		public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
		public override	void RefreshForeignKeys()
		{
			if (dom == null)
				return;

			if (busObjId != null)
				if (busObjId.Trim().Length != 0)
					if (int.Parse(busObjId.Trim()) > 0)
						return;
						
			busObjId = dom.Id.ToString();
		}
	#endregion
		
	#region Static methods
		
		public static WebSvcQueue find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(WebSvcQueue.getKey(id)))
                return (WebSvcQueue)uow.Imap.find(WebSvcQueue.getKey(id));
            
            WebSvcQueue cls = new WebSvcQueue();
            cls.uow = uow;
            cls.id = id;
            cls = (WebSvcQueue)DomainObj.addToIMap(uow, getOne(((WebSvcQueueSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static WebSvcQueue[] getAll(UOW uow)
        {
            WebSvcQueue[] objs = (WebSvcQueue[])DomainObj.addToIMap(uow, (new WebSvcQueueSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
		public static IWebSvcQueue[] GetPending(UOW uow)
		{
			WebSvcQueue[] objs = (WebSvcQueue[])DomainObj.addToIMap(uow, (new WebSvcQueueSQL()).GetPending(uow));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
		public static void Post(UOW uow, IUser user, IPayInfo payInfo, string receitId)
		{
			if (!Corporation.find(uow, StoreLocation.find(uow, user.LoginStoreCode).CorpID).IsPymtPostReq)
				return;
			
			SetupWQ(uow, user, payInfo, receitId);
		}
	#endregion
		
	#region Implementation
		static void SetupWQ(UOW uow, IUser user, IPayInfo payInfo, string receitId)
		{
			IWebSvcQueue wq = new WebSvcQueue(uow);

		    // wq.WSProvider = Corporation.find(uow, StoreLocation.find(uow, user.LoginStoreCode).CorpID.ToString();
			// wq.WebMethod = Corporation.GetWebMethod(uow, provider, payInfo.ParDemand)

			wq.QueType = WebSvcQueueType.Post.ToString();
			wq.ClerkId = user.ClerkId;
			wq.StoreCode = user.LoginStoreCode;
			wq.Status = WebSvcQueueStatus.Open.ToString();
			wq.BusObject = payInfo.ToString();
			wq.BusObjId = payInfo.Id.ToString();
			
//			wq.Xml = WebSvcFactory.GetXmlReq(uow, user, provider, payInfo).ToString(); 
			wq.Xml = new PostDPIPaymentRequest(new RWPostArgs(uow, user, payInfo, receitId)).ToString();			
		}

		static void SetupWQ(UOW uow, IUser user, IPayInfo payInfo, string receitId, string method)
		{
			IWebSvcQueue wq = new WebSvcQueue(uow);

			//wq.WSProvider = "";
			wq.QueType = WebSvcQueueType.Post.ToString();
			wq.ClerkId = user.ClerkId;
			wq.StoreCode = user.LoginStoreCode;
			wq.WebMethod = method;
			wq.Status = WebSvcQueueStatus.Open.ToString();
			wq.BusObject = payInfo.ToString();
			wq.BusObjId = payInfo.Id.ToString();
			wq.Xml = new PostDPIPaymentRequest(new RWPostArgs(uow, user, payInfo, receitId)).ToString();			
		}
        static WebSvcQueue getOne(WebSvcQueue[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(WebSvcQueue src, WebSvcQueue tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.queType = src.queType;
            tar.wSProvider = src.wSProvider;
            tar.webMethod = src.webMethod;
            tar.reversalMethod = src.reversalMethod;
            tar.storeCode = src.storeCode;
            tar.clerkId = src.clerkId;
            tar.busObject = src.busObject;
            tar.busObjId = src.busObjId;
            tar.initDate = src.initDate;
            tar.lastAccessDate = src.lastAccessDate;
            tar.initReasonCode = src.initReasonCode;
            tar.lastReasonCode = src.lastReasonCode;
            tar.initialMsg = src.initialMsg;
            tar.lastMsg = src.lastMsg;
            tar.attemps = src.attemps;
            tar.xml = src.xml;
            tar.reversalXml = src.reversalXml;
            tar.status = src.status;
            tar.rowState = src.rowState;
        }

	#endregion
		
	#region SQL
		
        [Serializable]
        class WebSvcQueueSQL : SqlGateway
        {
            public WebSvcQueue[] getKey(WebSvcQueue rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWebSvcQueue_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                WebSvcQueue rec = (WebSvcQueue)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWebSvcQueue_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                WebSvcQueue rec = (WebSvcQueue)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWebSvcQueue_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                WebSvcQueue rec = (WebSvcQueue)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWebSvcQueue_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public WebSvcQueue[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spWebSvcQueue_Get_All";
                return convert(execReader(cmd));
            }
			public WebSvcQueue[] GetPending(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spWebSvcQueue_Get_All_NotCompleted";			
				
				return convert(execReader(cmd));
			}

		#endregion
		
		#region SQL Implementation

            void setParam(SqlCommand cmd, WebSvcQueue rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                if (rec.queType == null)
                    cmd.Parameters.Add("@QueType", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.QueType.Length == 0)
                        cmd.Parameters.Add("@QueType", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@QueType", SqlDbType.VarChar, 15).Value = rec.queType;
                }
 
                if (rec.wSProvider == null)
                    cmd.Parameters.Add("@WSProvider", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.WSProvider.Length == 0)
                        cmd.Parameters.Add("@WSProvider", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@WSProvider", SqlDbType.VarChar, 50).Value = rec.wSProvider;
                }
 
                if (rec.webMethod == null)
                    cmd.Parameters.Add("@WebMethod", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.WebMethod.Length == 0)
                        cmd.Parameters.Add("@WebMethod", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@WebMethod", SqlDbType.VarChar, 50).Value = rec.webMethod;
                }
 
                if (rec.reversalMethod == null)
                    cmd.Parameters.Add("@ReversalMethod", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.ReversalMethod.Length == 0)
                        cmd.Parameters.Add("@ReversalMethod", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ReversalMethod", SqlDbType.VarChar, 50).Value = rec.reversalMethod;
                }
 
                if (rec.storeCode == null)
                    cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.StoreCode.Length == 0)
                        cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 25).Value = rec.storeCode;
                }
 
                if (rec.clerkId == null)
                    cmd.Parameters.Add("@ClerkId", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.ClerkId.Length == 0)
                        cmd.Parameters.Add("@ClerkId", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ClerkId", SqlDbType.VarChar, 25).Value = rec.clerkId;
                }
 
                if (rec.busObject == null)
                    cmd.Parameters.Add("@BusObject", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.BusObject.Length == 0)
                        cmd.Parameters.Add("@BusObject", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@BusObject", SqlDbType.VarChar, 50).Value = rec.busObject;
                }
 
                if (rec.busObjId == null)
                    cmd.Parameters.Add("@BusObjId", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.BusObjId.Length == 0)
                        cmd.Parameters.Add("@BusObjId", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@BusObjId", SqlDbType.VarChar, 25).Value = rec.busObjId;
                }
 
                if (rec.initDate == DateTime.MinValue)
                    cmd.Parameters.Add("@InitDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@InitDate", SqlDbType.DateTime, 0).Value = rec.initDate;
 
                if (rec.lastAccessDate == DateTime.MinValue)
                    cmd.Parameters.Add("@LastAccessDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@LastAccessDate", SqlDbType.DateTime, 0).Value = rec.lastAccessDate;
 
                if (rec.initReasonCode == null)
                    cmd.Parameters.Add("@InitReasonCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.InitReasonCode.Length == 0)
                        cmd.Parameters.Add("@InitReasonCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@InitReasonCode", SqlDbType.VarChar, 10).Value = rec.initReasonCode;
                }
 
                if (rec.lastReasonCode == null)
                    cmd.Parameters.Add("@LastReasonCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.LastReasonCode.Length == 0)
                        cmd.Parameters.Add("@LastReasonCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@LastReasonCode", SqlDbType.VarChar, 10).Value = rec.lastReasonCode;
                }
 
                if (rec.initialMsg == null)
                    cmd.Parameters.Add("@InitialMsg", SqlDbType.VarChar, 1000).Value = DBNull.Value;
                else
                {
                    if (rec.InitialMsg.Length == 0)
                        cmd.Parameters.Add("@InitialMsg", SqlDbType.VarChar, 1000).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@InitialMsg", SqlDbType.VarChar, 1000).Value = rec.initialMsg;
                }
 
                if (rec.lastMsg == null)
                    cmd.Parameters.Add("@LastMsg", SqlDbType.VarChar, 1000).Value = DBNull.Value;
                else
                {
                    if (rec.LastMsg.Length == 0)
                        cmd.Parameters.Add("@LastMsg", SqlDbType.VarChar, 1000).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@LastMsg", SqlDbType.VarChar, 1000).Value = rec.lastMsg;
                }
                cmd.Parameters.Add("@Attemps", SqlDbType.Int, 0).Value = rec.attemps;
 
                if (rec.xml == null)
                    cmd.Parameters.Add("@Xml", SqlDbType.VarChar, 2000).Value = DBNull.Value;
                else
                {
                    if (rec.Xml.Length == 0)
                        cmd.Parameters.Add("@Xml", SqlDbType.VarChar, 2000).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Xml", SqlDbType.VarChar, 2000).Value = rec.xml;
                }
 
                if (rec.reversalXml == null)
                    cmd.Parameters.Add("@ReversalXml", SqlDbType.VarChar, 2000).Value = DBNull.Value;
                else
                {
                    if (rec.ReversalXml.Length == 0)
                        cmd.Parameters.Add("@ReversalXml", SqlDbType.VarChar, 2000).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ReversalXml", SqlDbType.VarChar, 2000).Value = rec.reversalXml;
                }
 
                if (rec.status == null)
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.Status.Length == 0)
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = rec.status;
                }
				if (rec.initRespXml == null)
					cmd.Parameters.Add("@InitRespXml", SqlDbType.VarChar, 1000).Value = DBNull.Value;
				else
				{
					if (rec.initRespXml.Length == 0)
						cmd.Parameters.Add("@InitRespXml", SqlDbType.VarChar, 1000).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@InitRespXml", SqlDbType.VarChar, 1000).Value = rec.initRespXml;
				}
				if (rec.lastRespXml == null)
					cmd.Parameters.Add("@LastRespXml", SqlDbType.VarChar, 1000).Value = DBNull.Value;
				else
				{
					if (rec.lastRespXml.Length == 0)
						cmd.Parameters.Add("@LastRespXml", SqlDbType.VarChar, 1000).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@LastRespXml", SqlDbType.VarChar, 1000).Value = rec.lastRespXml;
				}
            }

            protected override DomainObj reader(SqlDataReader rdr)
            {
                WebSvcQueue rec = new WebSvcQueue();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["QueType"] != DBNull.Value)
                    rec.queType = (string) rdr["QueType"];
 
                if (rdr["WSProvider"] != DBNull.Value)
                    rec.wSProvider = (string) rdr["WSProvider"];
 
                if (rdr["WebMethod"] != DBNull.Value)
                    rec.webMethod = (string) rdr["WebMethod"];
 
                if (rdr["ReversalMethod"] != DBNull.Value)
                    rec.reversalMethod = (string) rdr["ReversalMethod"];
 
                if (rdr["StoreCode"] != DBNull.Value)
                    rec.storeCode = (string) rdr["StoreCode"];
 
                if (rdr["ClerkId"] != DBNull.Value)
                    rec.clerkId = (string) rdr["ClerkId"];
 
                if (rdr["BusObject"] != DBNull.Value)
                    rec.busObject = (string) rdr["BusObject"];
 
                if (rdr["BusObjId"] != DBNull.Value)
                    rec.busObjId = (string) rdr["BusObjId"];
 
                if (rdr["InitDate"] != DBNull.Value)
                    rec.initDate = (DateTime) rdr["InitDate"];
 
                if (rdr["LastAccessDate"] != DBNull.Value)
                    rec.lastAccessDate = (DateTime) rdr["LastAccessDate"];
 
                if (rdr["InitReasonCode"] != DBNull.Value)
                    rec.initReasonCode = (string) rdr["InitReasonCode"];
 
                if (rdr["LastReasonCode"] != DBNull.Value)
                    rec.lastReasonCode = (string) rdr["LastReasonCode"];
 
                if (rdr["InitialMsg"] != DBNull.Value)
                    rec.initialMsg = (string) rdr["InitialMsg"];
 
                if (rdr["LastMsg"] != DBNull.Value)
                    rec.lastMsg = (string) rdr["LastMsg"];
 
                if (rdr["Attemps"] != DBNull.Value)
                    rec.attemps = (int) rdr["Attemps"];
 
                if (rdr["Xml"] != DBNull.Value)
                    rec.xml = (string) rdr["Xml"];
 
                if (rdr["ReversalXml"] != DBNull.Value)
                    rec.reversalXml = (string) rdr["ReversalXml"];
 
                if (rdr["Status"] != DBNull.Value)
                    rec.status = (string) rdr["Status"];

				if (rdr["InitRespXml"] != DBNull.Value)
					rec.initRespXml = (string) rdr["InitRespXml"];

				if (rdr["LastRespXml"] != DBNull.Value)
					rec.lastRespXml = (string) rdr["LastRespXml"];

			    rec.rowState = RowState.Clean;
                return rec;
            }
            WebSvcQueue[] convert(DomainObj[] objs)
            {
                WebSvcQueue[] acls  = new WebSvcQueue[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
	#endregion
}