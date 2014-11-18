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
    public class UserAccountExtension : DomainObj, IUserAccountExtension
    {
        /*        Data        */
        static string iName = "UserAccountExtension";
        int id;
        int acctId;
        string userName;
        string password;
        string entityName;
        string applicationName;
        string url;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
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
        public string UserName
        {
            get { return userName; }
            set
            {
                setState();
                userName = value;
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
        public string EntityName
        {
            get { return entityName; }
            set
            {
                setState();
                entityName = value;
            }
        }
        public string ApplicationName
        {
            get { return applicationName; }
            set
            {
                setState();
                applicationName = value;
            }
        }
        public string Url
        {
            get { return url; }
            set
            {
                setState();
                url = value;
            }
        }
        
        /*        Constructors			*/
        public UserAccountExtension()
        {
            sql = new UserAccountExtensionSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public UserAccountExtension(UOW uow) : this()
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
            return new UserAccountExtensionSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static UserAccountExtension find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(UserAccountExtension.getKey(id)))
                return (UserAccountExtension)uow.Imap.find(UserAccountExtension.getKey(id));
            
            UserAccountExtension cls = new UserAccountExtension();
            cls.uow = uow;
            cls.id = id;
            cls = (UserAccountExtension)DomainObj.addToIMap(uow, getOne(((UserAccountExtensionSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static UserAccountExtension[] getAll(UOW uow)
        {
            UserAccountExtension[] objs = (UserAccountExtension[])DomainObj.addToIMap(uow, (new UserAccountExtensionSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
		public static IUserAccountExtension GetExtendedUser(UOW uow, int acctId, string entityName)
		{
			UserAccountExtension obj = (UserAccountExtension)DomainObj.addToIMap(uow, getOne((new UserAccountExtensionSQL()).GetExtendedUser(uow, acctId, entityName)));
			obj.uow = uow;
			return obj;
		}
        /*		Implementation		*/
        static UserAccountExtension getOne(UserAccountExtension[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(UserAccountExtension src, UserAccountExtension tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.acctId = src.acctId;
            tar.userName = src.userName;
            tar.password = src.password;
            tar.entityName = src.entityName;
            tar.applicationName = src.applicationName;
            tar.url = src.url;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class UserAccountExtensionSQL : SqlGateway
        {
            public UserAccountExtension[] getKey(UserAccountExtension rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spUserAccountExtension_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                UserAccountExtension rec = (UserAccountExtension)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spUserAccountExtension_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                UserAccountExtension rec = (UserAccountExtension)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spUserAccountExtension_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                UserAccountExtension rec = (UserAccountExtension)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spUserAccountExtension_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public UserAccountExtension[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spUserAccountExtension_Get_All";
                return convert(execReader(cmd));
            }
			public UserAccountExtension[] GetExtendedUser(UOW uow, int acctId, string entityName)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spUserAccountExtension_Get_By_Entity";
				cmd.Parameters.Add("@AcctId", SqlDbType.Int, 0).Value = acctId;
				cmd.Parameters.Add("@EntityName", SqlDbType.VarChar, 50).Value = entityName;
				return convert(execReader(cmd));
			}
			//GetExtendedUser
            /*        Implementation        */
            void setParam(SqlCommand cmd, UserAccountExtension rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                cmd.Parameters.Add("@AcctId", SqlDbType.Int, 0).Value = rec.acctId;
 
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = rec.userName;
 
                cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = rec.password;
 
                cmd.Parameters.Add("@EntityName", SqlDbType.VarChar, 50).Value = rec.entityName;
 
                cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 50).Value = rec.applicationName;
 
                if (rec.url == null)
                    cmd.Parameters.Add("@Url", SqlDbType.VarChar, 4000).Value = DBNull.Value;
                else
                {
                    if (rec.Url.Length == 0)
                        cmd.Parameters.Add("@Url", SqlDbType.VarChar, 4000).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Url", SqlDbType.VarChar, 4000).Value = rec.url;
                }
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                UserAccountExtension rec = new UserAccountExtension();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["AcctId"] != DBNull.Value)
                    rec.acctId = (int) rdr["AcctId"];
 
                if (rdr["UserName"] != DBNull.Value)
                    rec.userName = (string) rdr["UserName"];
 
                if (rdr["Password"] != DBNull.Value)
                    rec.password = (string) rdr["Password"];
 
                if (rdr["EntityName"] != DBNull.Value)
                    rec.entityName = (string) rdr["EntityName"];
 
                if (rdr["ApplicationName"] != DBNull.Value)
                    rec.applicationName = (string) rdr["ApplicationName"];
 
                if (rdr["Url"] != DBNull.Value)
                    rec.url = (string) rdr["Url"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            UserAccountExtension[] convert(DomainObj[] objs)
            {
                UserAccountExtension[] acls  = new UserAccountExtension[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
