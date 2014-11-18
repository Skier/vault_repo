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
    public class UserAcctType :  DomainObj, IUserAcctType
    {
        /*        Data        */
        static string iName = "UserAcctType";
        string acctType;
        string isAutoLoginOnly;
		string isStoreBased;
		string description;
		bool requestClerkId;
		string role;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, acctType); }
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
        public bool IsAutoLoginOnly
        {
            get 
			{
				return isAutoLoginOnly.Trim().ToUpper() == "T";
			}
            set
            {
                setState();
                isAutoLoginOnly = value ? "T" : "F";
            }
        }
		public bool IsStoreBased
		{
			get 
			{ 
				return isStoreBased.Trim().ToUpper() == "T";
			}
			set
			{
				setState();
				isStoreBased = value ? "T" : "F";			}
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

		public bool RequestClerkId
		{
			get { return requestClerkId; }
			set
			{
				setState();
				requestClerkId = value;
			}
		}
		public string Role
		{
			get { return role; }
			set
			{
				setState();
				role = value;
			}
		}


        /*        Constructors			*/
        public UserAcctType()
        {
            sql = new UserAcctTypeSQL();
            rowState = RowState.New;
        }
        public UserAcctType(UOW uow) : this()
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
            return new UserAcctTypeSQL();
        }
        public override void checkExists()
        {
            if ((AcctType ==  null))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static UserAcctType find(UOW uow, string acctType)
        {
            if (uow.Imap.keyExists(UserAcctType.getKey(acctType)))
                return (UserAcctType)uow.Imap.find(UserAcctType.getKey(acctType));
            
            UserAcctType cls = new UserAcctType();
            cls.uow = uow;
            cls.acctType = acctType;
            cls = (UserAcctType)DomainObj.addToIMap(uow, getOne(((UserAcctTypeSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;	
            
            return cls;
        }
        public static UserAcctType[] getAll(UOW uow)
        {
            UserAcctType[] objs = (UserAcctType[])DomainObj.addToIMap(uow, (new UserAcctTypeSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(string acctType)
        {
            return new Key(iName, acctType.ToString());
        }
        /*		Implementation		*/
        static UserAcctType getOne(UserAcctType[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(UserAcctType src, UserAcctType tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.acctType = src.acctType;
            tar.isAutoLoginOnly = src.isAutoLoginOnly;
			tar.isStoreBased = src.isStoreBased;
       //     tar.description = src.description;
            tar.rowState = src.rowState;
			tar.requestClerkId = src.requestClerkId;
			tar.role = src.role;
        }
 
        /*		SQL		*/
        [Serializable]
        class UserAcctTypeSQL : SqlGateway
        {
            public UserAcctType[] getKey(UserAcctType rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spUserAcctType_Get_Id";
                cmd.Parameters.Add("@AcctType", SqlDbType.VarChar, 20).Value = rec.acctType;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                UserAcctType rec = (UserAcctType)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spUserAcctType_Ins";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                UserAcctType rec = (UserAcctType)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spUserAcctType_Del_Id";
                cmd.Parameters.Add("@AcctType", SqlDbType.VarChar, 20).Value = rec.acctType;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                UserAcctType rec = (UserAcctType)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spUserAcctType_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public UserAcctType[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spUserAcctType_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, UserAcctType rec)
            {
 
                cmd.Parameters.Add("@AcctType", SqlDbType.VarChar, 20).Value = rec.acctType;

				if (rec.isAutoLoginOnly == null)
					cmd.Parameters.Add("@IsAutoLoginOnly", SqlDbType.Char, 1).Value = DBNull.Value;
				else
					if (rec.isAutoLoginOnly.Length == 0)
						cmd.Parameters.Add("@IsAutoLoginOnly", SqlDbType.Char, 1).Value = DBNull.Value;
					else
		                cmd.Parameters.Add("@IsAutoLoginOnly", SqlDbType.Char, 1).Value = rec.isAutoLoginOnly;

				if (rec.isStoreBased == null)
					cmd.Parameters.Add("@isStoreBased", SqlDbType.Char, 1).Value = DBNull.Value;
				else
					if (rec.isStoreBased.Length == 0)
					cmd.Parameters.Add("@isStoreBased", SqlDbType.Char, 1).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@isStoreBased", SqlDbType.Char, 1).Value = rec.isStoreBased;

 
              if (rec.description == null)
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar, 250).Value = DBNull.Value;                
			 else
                {
                    if (rec.Description.Length == 0)
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 250).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 250).Value = rec.description;
                }				
				
			cmd.Parameters.Add("@RequestClerkId", SqlDbType.Bit, 1).Value = rec.requestClerkId;				
		
			if (rec.role == null)
					cmd.Parameters.Add("@Role", SqlDbType.VarChar, 25).Value = DBNull.Value;                
				else
				{
					if (rec.role.Length == 0)
						cmd.Parameters.Add("@role", SqlDbType.VarChar, 25).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Role", SqlDbType.VarChar, 25).Value = rec.role;
				}
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                UserAcctType rec = new UserAcctType();
                
                if (rdr["AcctType"] != DBNull.Value)
                    rec.acctType = (string) rdr["AcctType"];
 
				if (rdr["IsAutoLoginOnly"] != DBNull.Value)
					rec.isAutoLoginOnly = ((string) rdr["IsAutoLoginOnly"]).Trim().ToUpper();
 
				if (rdr["IsStoreBased"] != DBNull.Value)
					rec.isStoreBased = ((string) rdr["IsStoreBased"]).Trim().ToUpper();
 
//				if (rdr["Description"] != DBNull.Value)
//                    rec.description = (string) rdr["Description"];

				if (rdr["RequestClerkId"] != DBNull.Value)
					rec.requestClerkId = (bool)rdr["RequestClerkId"];

				if (rdr["Role"] != DBNull.Value)
					rec.role = ((string) rdr["Role"]).Trim();

 
                rec.rowState = RowState.Clean;
                return rec;
            }
            UserAcctType[] convert(DomainObj[] objs)
            {
                UserAcctType[] acls  = new UserAcctType[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}