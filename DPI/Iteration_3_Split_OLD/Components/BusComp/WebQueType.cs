using System;
using System.Data;
using System.Data.SqlClient;
using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Components
{
    [Serializable]
    public class WebQueType : DomainObj, IWebQueType
    {
        /*        Data        */
        static string iName = "WebQueType";
        
		string queType;
        bool isReversal;
        bool isPost;
        bool isReadOnly;
		bool isEvergreen;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, queType); }
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
        public bool IsReversal
        {
            get { return isReversal; }
            set
            {
                setState();
                isReversal = value;
            }
        }
        public bool IsPost
        {
            get { return isPost; }
            set
            {
                setState();
                isPost = value;
            }
        }
        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set
            {
                setState();
                isReadOnly = value;
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

        /*        Constructors			*/
        public WebQueType()
        {
            sql = new WebQueTypeSQL();
            rowState = RowState.New;
        }
        public WebQueType(UOW uow) : this()
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
            return new WebQueTypeSQL();
        }
        public override void checkExists()
        {
            if ((QueType ==  null))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static WebQueType find(UOW uow, string queType)
        {
            if (uow.Imap.keyExists(WebQueType.getKey(queType)))
                return (WebQueType)uow.Imap.find(WebQueType.getKey(queType));
            
            WebQueType cls = new WebQueType();
            cls.uow = uow;
            cls.queType = queType;
            cls = (WebQueType)DomainObj.addToIMap(uow, getOne(((WebQueTypeSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static WebQueType[] getAll(UOW uow)
        {
            WebQueType[] objs = (WebQueType[])DomainObj.addToIMap(uow, (new WebQueTypeSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(string queType)
        {
            return new Key(iName, queType.ToString());
        }
        /*		Implementation		*/
        static WebQueType getOne(WebQueType[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(WebQueType src, WebQueType tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.queType = src.queType;
            tar.isReversal = src.isReversal;
            tar.isPost = src.isPost;
            tar.isReadOnly = src.isReadOnly;
            tar.rowState = src.rowState;
			tar.isEvergreen = src.isEvergreen;
        }
 
        /*		SQL		*/
        [Serializable]
        class WebQueTypeSQL : SqlGateway
        {
            public WebQueType[] getKey(WebQueType rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWebQueType_Get_Id";
                cmd.Parameters.Add("@QueType", SqlDbType.VarChar, 15).Value = rec.queType;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                WebQueType rec = (WebQueType)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWebQueType_Ins";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                WebQueType rec = (WebQueType)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWebQueType_Del_Id";
                cmd.Parameters.Add("@QueType", SqlDbType.VarChar, 15).Value = rec.queType;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                WebQueType rec = (WebQueType)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWebQueType_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public WebQueType[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spWebQueType_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, WebQueType rec)
            {
                cmd.Parameters.Add("@QueType", SqlDbType.VarChar, 15).Value = rec.queType;
				cmd.Parameters.Add("@IsReversal", SqlDbType.Bit, 0).Value = rec.isReversal;
                cmd.Parameters.Add("@IsPost", SqlDbType.Bit, 0).Value = rec.isPost;

				cmd.Parameters.Add("@IsReadOnly", SqlDbType.Bit, 0).Value = rec.isReadOnly;
				cmd.Parameters.Add("@IsEvergreen", SqlDbType.Bit, 0).Value = rec.isEvergreen;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                WebQueType rec = new WebQueType();
                
                if (rdr["QueType"] != DBNull.Value)
                    rec.queType = (string) rdr["QueType"];
 
                if (rdr["IsReversal"] != DBNull.Value)
                    rec.isReversal = (bool) rdr["IsReversal"];
 
                if (rdr["IsPost"] != DBNull.Value)
                    rec.isPost = (bool) rdr["IsPost"];
 
                if (rdr["IsReadOnly"] != DBNull.Value)
                    rec.isReadOnly = (bool) rdr["IsReadOnly"];

				if (rdr["IsEvergreen"] != DBNull.Value)
					rec.isEvergreen = (bool) rdr["IsEvergreen"];

                rec.rowState = RowState.Clean;
                return rec;
            }
            WebQueType[] convert(DomainObj[] objs)
            {
                WebQueType[] acls  = new WebQueType[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
