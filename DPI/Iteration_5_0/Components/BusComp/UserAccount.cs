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
	public class UserAccount : DomainObj, IUserAccount
	{
		/*        Data        */
		static string iName = "UserAccount";
		int			acctId;
		string		acctType;
		string		acctName;
		string		password;
		string		storeCode;
		int			corpId;
		string		clerkId;
		string		displayName;
		string		jobTitle;
		string		status;
		bool		isCertRequired;
		bool		isCertWithStoreReq;
		DateTime	expiration;
		bool		passwordReset;
        
		/*        Properties        */
		public override IDomKey IKey 
		{
			get { return new Key(iName, acctId.ToString()); }
		}
		public int AcctId
		{
			get { return acctId; }
			set
			{
				setState();
				acctId = value;
			}
		}
		public string AcctType
		{
			get { return acctType; }
			set
			{
				setState();
				acctType = value;
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
		public string Password
		{
			get { return password; }
			set
			{
				setState();
				password = value;
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
		public int CorpId
		{
			get { return corpId; }
			set
			{
				setState();
				corpId = value;
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
		public string DisplayName
		{
			get { return displayName; }
			set
			{
				setState();
				displayName = value;
			}
		}
		public string JobTitle
		{
			get { return jobTitle; }
			set
			{
				setState();
				jobTitle = value;
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
		public bool IsCertRequired
		{ 
			get { return isCertRequired; }
			set 
			{ 
				setState();
				isCertRequired = value; 
			}
		}
		public bool IsCertWithStoreReq 
		{
			get { return isCertWithStoreReq; }
			set 
			{ 
				setState();
				isCertWithStoreReq = value; 
			}
		}
		public DateTime Expiration
		{
			get { return expiration;		}
			set 
			{ 
				setState();
				expiration = value;		
			}
		}
		public bool PasswordReset
		{
			get { return passwordReset;		}
			set 
			{
				setState();
				passwordReset = value;
			}
		}
        /*        Constructors			*/
        public UserAccount()
        {
            sql = new UserAccountSQL();
            acctId = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public UserAccount(UOW uow) : this()
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
            return new UserAccountSQL();
        }
        public override void checkExists()
        {
            if ((AcctId < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
		public PermissionRule SetPermissionState(UOW uow, DPI.Interfaces.IUser user)
		{				
			if (this.acctType == null)
				throw new ArgumentNullException("acctType");
			
			PermissionRule rule = new PermissionRule();
			
			rule.AcctType = this.acctType;
			rule.JobTitle = user.JobTitle;
			UserAcctType type = UserAcctType.find(uow, this.acctType);
			user.Role = type.Role;

			if (type.IsStoreBased)
				return StoreBasedRule(uow, rule, user.LoginStoreCode);

			return rule;
		}
		PermissionRule StoreBasedRule(UOW uow, PermissionRule rule, string certStoreCode)
		{
			rule.JobTitle = this.jobTitle;

			string scode = certStoreCode;
			if (scode == null)
				scode = this.storeCode;
			
			StoreLocation store   = StoreLocation.find(uow, scode);


			if (!store.Active ) 
			{				
				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.UserAccount.ToString(), "N/A", "Store is not active. StoreCode = " + scode);

				throw new ApplicationException("Inactive store. Login failed");
			}
			// store based states			
			rule.IsInternetSeller     = store.Internet     ? "T" : "F";
			rule.IsLocalServiceSeller = store.LocalService ? "T" : "F";
			rule.IsWirelessSeller     = store.Wireless     ? "T" : "F";
			rule.IsDebCardSeller      = store.DebitCard    ? "T" : "F";
			rule.IsSatelliteSeller    = store.Satellite    ? "T" : "F";
			rule.IsDpiWirelessSeller  = store.DpiWireless  ? "T" : "F";
			rule.IsDpiEnergySeller	  = store.DpiEnergy    ? "T" : "F";
 
			if (store.StoreClass == null)
			{
				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.UserAccount.ToString(), "N/A", "StoreClass is null. StoreCode = " + scode);
			
				throw new ApplicationException("StoreClass can't be null. Login failed");
			}
			StoreClass sclass   = StoreClass.find(uow, store.StoreClass);

			// store class based states			
			rule.IsDirectSeller = sclass.IsDirectSeller;
			rule.IsPymtStation  = sclass.IsPymtStation;
			rule.IsPriceLookup  = sclass.IsPriceLookup;
			

			return rule;
		}
        /*		Static methods		*/
        public static UserAccount find(UOW uow, int acctId)
        {
            if (uow.Imap.keyExists(UserAccount.getKey(acctId)))
                return (UserAccount)uow.Imap.find(UserAccount.getKey(acctId));
            
            UserAccount cls = new UserAccount();
            cls.uow = uow;
            cls.acctId = acctId;
            cls = (UserAccount)DomainObj.addToIMap(uow, getOne(((UserAccountSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
		public static UserAccount[] GetByName(UOW uow, string acctName)
		{
			UserAccount[] objs = (UserAccount[])DomainObj.addToIMap(uow, 
				(new UserAccountSQL()).GetByName(uow, acctName));
			
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			
			return objs;
		}
		public static bool ValidateUA(UOW uow, string acctName, string pw)
		{
			UserAccount ua = getOne(GetByName(uow, acctName));

			if (ua.password == pw)
				return true;

			return false;
		}
        public static UserAccount[] getAll(UOW uow)
        {
            UserAccount[] objs = (UserAccount[])DomainObj.addToIMap(uow, (new UserAccountSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int acctId)
        {
            return new Key(iName, acctId.ToString());
        }
		public bool Login(UOW uow, string acctName, object password, ref DPI.Interfaces.IUser user,
			out IPermission[] permissions, out string msg)
		{		
			permissions = new Permission[0];
			msg = null;
			
			if (!VaidateCorp(user)) //checks whether Certificate and UserAccount are of the same Corp.
			{
				msg = "Certificate and UserAccount belong to different corporations.  Please correct"; 
				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.UserAccount.ToString(), acctName, "Certificate and UserAccount belong to different corporations"); 

				return false;
			}

			if (user.LoginStoreCode == null)
				user.LoginStoreCode = this.StoreCode;

			user.AcctId = this.AcctId;
			user.DisplayName = this.DisplayName;
			user.ClerkId = this.ClerkId;
			user.AcctType = this.AcctType;
			user.JobTitle = this.JobTitle;
			
			if (password == null)
			{
				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.UserAccount.ToString(), acctName, "Password is null"); 
				
				return false;
			}
			if (this.password.ToString() != password.ToString()) // incorrect password
			{
				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.UserAccount.ToString(), acctName, "Incorrect password"); 
				
				return false;
			}
			if (this.Status.Trim().ToLower() != "active") // hardcoded policy
			{
				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.UserAccount.ToString(), acctName, 
						"Inactive UserAccount. Id = " + this.acctId.ToString()); 
				return false;
			}
			permissions = PermissionRule.GetPermissions(uow, SetPermissionState(uow, user));
			return true;
		}
		/*		Implementation		*/
		bool VaidateCorp(IUser user)
		{
			if (user.LoginStoreCode == null)
				return true;

			if (user.LoginStoreCode == string.Empty)
				return true;

			if (user.LoginStoreCode.Trim().ToLower() == this.storeCode.Trim().ToLower())
				return true;

			if (this.storeCode != null)
				if (this.storeCode != string.Empty)
					return VerifyCorp(user.LoginStoreCode);
		
			if (this.corpId > 0)
				return VerifyCorp(this.corpId, user.LoginStoreCode);
			
			return true; // Can't determine Corp of UserAccount
		}
		bool VerifyCorp(int corp, string loginStoreCode)
		{
			if (corp == StoreStatsCol.GetStoreStat(loginStoreCode).CorpId)
				return true;
			return false;
		}
		bool VerifyCorp(string loginStoreCode)
		{
			if (StoreStatsCol.GetStoreStat(this.storeCode).CorpId  == StoreStatsCol.GetStoreStat(loginStoreCode).CorpId )
				return true;
			return false;
		}
        static UserAccount getOne(UserAccount[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(UserAccount src, UserAccount tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.acctId             = src.acctId;
            tar.acctType           = src.acctType;
            tar.acctName           = src.acctName;
            tar.password           = src.password;
            tar.storeCode          = src.storeCode;
            tar.corpId             = src.corpId;
            tar.clerkId            = src.clerkId;
            tar.displayName        = src.displayName;
            tar.jobTitle           = src.jobTitle;
            tar.status             = src.status;
            tar.rowState           = src.rowState;
			tar.isCertRequired     = src.isCertRequired;
			tar.isCertWithStoreReq = src.isCertWithStoreReq;
			tar.expiration		   = src.expiration;
			tar.passwordReset	   = src.passwordReset;
        }
 
        /*		SQL		*/
        [Serializable]
        class UserAccountSQL : SqlGateway
        {
            public UserAccount[] getKey(UserAccount rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spUserAccount_Get_Id";
                cmd.Parameters.Add("@AcctId", SqlDbType.Int, 0).Value = rec.acctId;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                UserAccount rec = (UserAccount)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spUserAccount_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@AcctId"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.acctId = (int)cmd.Parameters["@AcctId"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                UserAccount rec = (UserAccount)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spUserAccount_Del_Id";
                cmd.Parameters.Add("@AcctId", SqlDbType.Int, 0).Value = rec.acctId;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                UserAccount rec = (UserAccount)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spUserAccount_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public UserAccount[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spUserAccount_Get_All";
                return convert(execReader(cmd));
            }
			public UserAccount[] GetByName(UOW uow, string name)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spUserAccount_Get_ByName";
				cmd.Parameters.Add("@AcctName", SqlDbType.VarChar, 20).Value = name;
				return convert(execReader(cmd));
			}
            /*        Implementation        */
			void setParam(SqlCommand cmd, UserAccount rec)
			{
				cmd.Parameters.Add("@AcctId", SqlDbType.Int, 0).Value = rec.acctId;
 
				if (rec.acctType == null)
					cmd.Parameters.Add("@AcctType", SqlDbType.VarChar, 20).Value = DBNull.Value;
				else
				{
					if (rec.AcctType.Length == 0)
						cmd.Parameters.Add("@AcctType", SqlDbType.VarChar, 20).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@AcctType", SqlDbType.VarChar, 20).Value = rec.acctType;
				}
 
				if (rec.acctName == null)
					cmd.Parameters.Add("@AcctName", SqlDbType.VarChar, 15).Value = DBNull.Value;
				else
				{
					if (rec.AcctName.Length == 0)
						cmd.Parameters.Add("@AcctName", SqlDbType.VarChar, 15).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@AcctName", SqlDbType.VarChar, 15).Value = rec.acctName;
				}
 
				if (rec.password == null)
					cmd.Parameters.Add("@Password", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.Password.Length == 0)
						cmd.Parameters.Add("@Password", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Password", SqlDbType.VarChar, 10).Value = rec.password;
				}
 
				if (rec.storeCode == null)
					cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.StoreCode.Length == 0)
						cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 10).Value = rec.storeCode;
				}
				cmd.Parameters.Add("@CorpId", SqlDbType.Int, 0).Value = rec.corpId;
 
				if (rec.clerkId == null)
					cmd.Parameters.Add("@ClerkId", SqlDbType.VarChar, 20).Value = DBNull.Value;
				else
				{
					if (rec.ClerkId.Length == 0)
						cmd.Parameters.Add("@ClerkId", SqlDbType.VarChar, 20).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@ClerkId", SqlDbType.VarChar, 20).Value = rec.clerkId;
				}
 
				if (rec.displayName == null)
					cmd.Parameters.Add("@DisplayName", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.DisplayName.Length == 0)
						cmd.Parameters.Add("@DisplayName", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@DisplayName", SqlDbType.VarChar, 50).Value = rec.displayName;
				}
 
				if (rec.jobTitle == null)
					cmd.Parameters.Add("@JobTitle", SqlDbType.VarChar, 30).Value = DBNull.Value;
				else
				{
					if (rec.JobTitle.Length == 0)
						cmd.Parameters.Add("@JobTitle", SqlDbType.VarChar, 30).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@JobTitle", SqlDbType.VarChar, 30).Value = rec.jobTitle;
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

				cmd.Parameters.Add("@IsCertRequired", SqlDbType.Bit, 1).Value = rec.isCertRequired;
				cmd.Parameters.Add("@IsCertWithStoreReq", SqlDbType.Bit, 1).Value = rec.isCertWithStoreReq;

				if (rec.expiration == DateTime.MinValue)
					cmd.Parameters.Add("@Expiration", SqlDbType.DateTime, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Expiration", SqlDbType.DateTime, 0).Value = rec.expiration;

				cmd.Parameters.Add("@PasswordReset", SqlDbType.Bit, 1).Value = rec.passwordReset;				
			}
            protected override DomainObj reader(SqlDataReader rdr)
            {
                UserAccount rec = new UserAccount();
                
                if (rdr["AcctId"] != DBNull.Value)
                    rec.acctId = (int) rdr["AcctId"];
 
                if (rdr["AcctType"] != DBNull.Value)
                    rec.acctType = (string) rdr["AcctType"];
 
                if (rdr["AcctName"] != DBNull.Value)
                    rec.acctName = (string) rdr["AcctName"];
 
                if (rdr["Password"] != DBNull.Value)
                    rec.password = (string) rdr["Password"];
 
                if (rdr["StoreCode"] != DBNull.Value)
                    rec.storeCode = (string) rdr["StoreCode"];
 
                if (rdr["CorpId"] != DBNull.Value)
                    rec.corpId = (int) rdr["CorpId"];
 
                if (rdr["ClerkId"] != DBNull.Value)
                    rec.clerkId = (string) rdr["ClerkId"];
 
                if (rdr["DisplayName"] != DBNull.Value)
                    rec.displayName = (string) rdr["DisplayName"];
 
                if (rdr["JobTitle"] != DBNull.Value)
                    rec.jobTitle = (string) rdr["JobTitle"];
 
                if (rdr["Status"] != DBNull.Value)
                    rec.status = (string) rdr["Status"];
				
				if (rdr["IsCertRequired"] != DBNull.Value)
					rec.isCertRequired = (bool)rdr["IsCertRequired"];
				
				if (rdr["IsCertWithStoreReq"] != DBNull.Value)
					rec.isCertWithStoreReq = (bool)rdr["IsCertWithStoreReq"];

				if (rdr["Expiration"] != DBNull.Value)
					rec.expiration = (DateTime)rdr["Expiration"];

				if (rdr["PasswordReset"] != DBNull.Value)
					rec.passwordReset = (bool)rdr["PasswordReset"];

 
                rec.rowState = RowState.Clean;
                return rec;
            }
            UserAccount[] convert(DomainObj[] objs)
            {
                UserAccount[] acls  = new UserAccount[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
