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
    public class TempAutologin : DomainObj, ITempAutologin
    {
        /*        Data        */
        static string iName = "TempAutologin";
        int id;
        string acctName;
        string pW;
		string token;
		string storeCode;
		string transactionType;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
            set
            {
                setState();
                id = value;
            }
        }
        public string AcctName
        {
            get { return acctName; }
            set
            {
                setState();
                acctName = value;
            }
        }
        public string PW
        {
            get { return pW; }
            set
            {
                setState();
                pW = value;
            }
        }
		public string Token
		{
			get { return token; }
			set
			{
				setState();
				token = value;
			}
		}
		public string StoreCode			{ get { return storeCode;		}}
		public string TransactionType	{ get { return transactionType;	}} 

        /*        Constructors			*/
        public TempAutologin()
        {
			id = new Random().Next(1, 100000000);
			DPI_Err_Log.AddLogEntry(ErrLogSubSystems.Logon.ToString(), "N/A", "Random temp key = :" + id.ToString());

            sql = new TempAutologinSQL();
            rowState = RowState.New;
        }
        public TempAutologin(UOW uow, string acctName, string pW) : this()
        {
            if(uow == null)
                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
            if(uow.Imap == null)
                throw new ArgumentException("IdentityMap is required", "Identity Map");
                       
            this.uow = uow;
			this.acctName = acctName;
			this.pW = pW;
			this.uow.Imap.add(this);
        }
        
		public TempAutologin(UOW uow, string acctName, string pW, string token, string storeCode) : this(uow, acctName, pW)
		{
			this.token = token;
			this.storeCode = storeCode;
		}
		public TempAutologin(UOW uow, string acctName, string pW, string token, string storeCode, string transactionType) : this(uow, acctName, pW, token, storeCode)
		{
			this.transactionType = transactionType;
		}
        
        /*        Methods        */
        protected override SqlGateway loadSql()
        {
            return new TempAutologinSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static TempAutologin find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(TempAutologin.getKey(id)))
                return (TempAutologin)uow.Imap.find(TempAutologin.getKey(id));
            
            TempAutologin cls = new TempAutologin();
            cls.uow = uow;
            cls.id = id;
            cls = (TempAutologin)DomainObj.addToIMap(uow, getOne(((TempAutologinSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
		public static void Save(UOW uow, string acctName, object password, string token, int tempKey)
		{
			TempAutologin cls = new TempAutologin();
			cls.uow = uow;
			cls.id = tempKey;
			cls.acctName = acctName;
			cls.pW = (string)password;
			cls.token = token;

			TempAutologinSQL tsql = new TempAutologinSQL();
			tsql.insert(cls);
			
			cls.uow = uow;
		}
		public static void Save(UOW uow, string acctName, object password, string token, string storeCode, int tempKey)
		{
			TempAutologin cls = new TempAutologin();
			cls.uow = uow;
			cls.id = tempKey;
			cls.acctName = acctName;
			cls.pW = (string)password;
			cls.token = token;
			cls.storeCode = storeCode;

			TempAutologinSQL tsql = new TempAutologinSQL();
			tsql.insert(cls);
			
			cls.uow = uow;
		}
//		public static TempAutologin GetAutoLogonById(UOW uow, int tempKey)
//		{
////			TempAutologin[] objs = (TempAutologin)DomainObj.addToIMap(uow, 
////									(new TempAutologinSQL().GetById(uow, tempKey)));
////			
////			for (int i = 0; i < objs.Length; i++)
////				objs[i].uow = uow;
////			
////			return objs;	
//		
//			TempAutologinSQL tsql = new TempAutologinSQL();
//			TempAutologin[] tals = tsql.GetById(uow, tempKey);
//
//			return getOne(tals);			
//		}
        public static TempAutologin[] getAll(UOW uow)
        {
            TempAutologin[] objs = (TempAutologin[])DomainObj.addToIMap(uow, (new TempAutologinSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
        static TempAutologin getOne(TempAutologin[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("TempAutologin: More than one row found for Id: " + acls[0].id.ToString());
        }
        static void copyAttrs(TempAutologin src, TempAutologin tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.acctName = src.acctName;
            tar.pW = src.pW;
			tar.token = src.token;
			tar.storeCode = src.storeCode;
			tar.transactionType = src.transactionType;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class TempAutologinSQL : SqlGateway
        {
            public TempAutologin[] getKey(TempAutologin rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spTempAutologin_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                TempAutologin rec = (TempAutologin)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spTempAutologin_Ins";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                TempAutologin rec = (TempAutologin)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spTempAutologin_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                TempAutologin rec = (TempAutologin)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spTempAutologin_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public TempAutologin[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spTempAutologin_Get_All";
                return convert(execReader(cmd));
            }
//			public TempAutologin[] GetById(UOW uow, int tempKey)
//			{
//				SqlCommand cmd = makeCommand(uow);
//				cmd.CommandText = "spAutoLogon_Get_Id";
//				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = tempKey;
//				return convert(execReader(cmd));
//			}
            /*        Implementation        */
			void setParam(SqlCommand cmd, TempAutologin rec)
			{
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
				cmd.Parameters.Add("@AcctName", SqlDbType.VarChar, 15).Value = rec.acctName;
 
				cmd.Parameters.Add("@PW", SqlDbType.VarChar, 10).Value = rec.pW;
			
				if (rec.token == null)
					cmd.Parameters.Add("@Token", SqlDbType.VarChar, 25).Value = DBNull.Value;
				else
				{
					if (rec.token.Length > 0 )
						cmd.Parameters.Add("@Token", SqlDbType.VarChar, 25).Value = rec.token;
					else
						cmd.Parameters.Add("@Token", SqlDbType.VarChar, 25).Value = DBNull.Value;
				}
				if (rec.storeCode == null)
					cmd.Parameters.Add("@StoreCode", SqlDbType.Char, 10).Value = DBNull.Value;
				else
				{
					if (rec.storeCode.Length > 0 )
						cmd.Parameters.Add("@StoreCode", SqlDbType.Char, 10).Value = rec.storeCode;
					else
						cmd.Parameters.Add("@StoreCode", SqlDbType.Char, 10).Value = DBNull.Value;
				}
				if (rec.transactionType == null)
					cmd.Parameters.Add("@TransactionType", SqlDbType.VarChar, 15).Value = DBNull.Value;
				else
				{
					if (rec.transactionType.Length > 0 )
						cmd.Parameters.Add("@TransactionType", SqlDbType.VarChar, 15).Value = rec.transactionType;
					else
						cmd.Parameters.Add("@TransactionType", SqlDbType.VarChar, 15).Value = DBNull.Value;
				}
			}
						
            protected override DomainObj reader(SqlDataReader rdr)
            {
                TempAutologin rec = new TempAutologin();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["AcctName"] != DBNull.Value)
                    rec.acctName = (string) rdr["AcctName"];
 
                if (rdr["PW"] != DBNull.Value)
                    rec.pW = (string) rdr["PW"];
 
				if (rdr["Token"] != DBNull.Value)
					rec.token = (string) rdr["Token"];

				if (rdr["StoreCode"] != DBNull.Value)
					rec.storeCode = (string) rdr["StoreCode"];

				if (rdr["TransactionType"] != DBNull.Value)
					rec.transactionType = (string) rdr["TransactionType"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            TempAutologin[] convert(DomainObj[] objs)
            {
                TempAutologin[] acls  = new TempAutologin[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
