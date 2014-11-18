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
    public class AgentRegistration : DomainObj, IAgentRegistration
    {
	#region Data
        static string iName = "AgentRegistration";
        
		int id;
        string regType;
        int exclusiveIncentive;
        DateTime regDate;
        DateTime effStartDate;
        DateTime effEnddate;
        
		bool isAgreed;
        string firstName;
        string lastName;
        int title;
        string phone;
        string email;
        
		int confNum;
        int userAcct;
        string storeCode;
        int corpId;
        string status;

		int addressId;
		IAddr2 address;	
	#endregion

	#region Properties
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
        }
        public string RegType
        {
            get { return regType; }
            set
            {
                setState();
                regType = value;
            }
        }
        public int ExclusiveIncentive
        {
            get { return exclusiveIncentive; }
            set
            {
                setState();
                exclusiveIncentive = value;
            }
        }
        public DateTime RegDate
        {
            get { return regDate; }
            set
            {
                setState();
                regDate = value;
            }
        }
        public DateTime EffStartDate
        {
            get { return effStartDate; }
            set
            {
                setState();
                effStartDate = value;
            }
        }
        public DateTime EffEnddate
        {
            get { return effEnddate; }
            set
            {
                setState();
                effEnddate = value;
            }
        }
        public bool IsAgreed
        {
            get { return isAgreed; }
            set
            {
                setState();
                isAgreed = value;
            }
        }
        public string FirstName
        {
            get { return firstName; }
            set
            {
                setState();
                firstName = value;
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                setState();
                lastName = value;
            }
        }
        public int Title
        {
            get { return title; }
            set
            {
                setState();
                title = value;
            }
        }
        public string Phone
        {
            get { return phone; }
            set
            {
                setState();
                phone = value;
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                setState();
                email = value;
            }
        }
		
        public int AddrId
        {
            get 
			{ 
				if (Address != null)
					return address.AddressID;

				return addressId; 
			}
            set
            {
                setState();
                addressId = value;
            }
        }
		public IAddr2 Address
		{
			get 
			{
				if (address == null)
					if ( addressId > 0)
						address = CustAddress.find(uow, addressId);

				return address; 
			}
			set { address = value; }
		}
        public int ConfNum
        {
            get { return confNum; }
            set
            {
                setState();
                confNum = value;
            }
        }
        public int UserAcct
        {
            get { return userAcct; }
            set
            {
                setState();
                userAcct = value;
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
        public string Status
        {
            get { return status; }
            set
            {
                setState();
                status = value;
            }
        }
	#endregion
        
	#region Constructors
		public AgentRegistration()
        {
            sql = new AgentRegistrationSQL();
            id = random.Next(Int32.MinValue, -1);
			priority = 20000;
            rowState = RowState.New;
        }
        public AgentRegistration(UOW uow) : this()
        {
            if(uow == null)
                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
            if(uow.Imap == null)
                throw new ArgumentException("IdentityMap is required", "Identity Map");
            
            this.uow = uow;
            this.uow.Imap.add(this);
        }
		public AgentRegistration(IMap imap) : this()
		{
			if(imap == null)
				throw new ArgumentException("IdentityMap is required", "Identity Map");
            
			imap.add(this);
		}
	#endregion
        
	#region Methods
        protected override SqlGateway loadSql()
        {
            return new AgentRegistrationSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static AgentRegistration find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(AgentRegistration.getKey(id)))
                return (AgentRegistration)uow.Imap.find(AgentRegistration.getKey(id));
            
            AgentRegistration cls = new AgentRegistration();
            cls.uow = uow;
            cls.id = id;
            cls = (AgentRegistration)DomainObj.addToIMap(uow, getOne(((AgentRegistrationSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static AgentRegistration[] getAll(UOW uow)
        {
            AgentRegistration[] objs = (AgentRegistration[])DomainObj.addToIMap(uow, (new AgentRegistrationSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
		public override	void RefreshForeignKeys()
		{
			RefreshAddr();
		}
	#endregion

	#region Implementation
		void RefreshAddr()
		{
			if (addressId > 0)
				return;

			if (this.address != null)
				addressId = address.AddressID;
		}
        static AgentRegistration getOne(AgentRegistration[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(AgentRegistration src, AgentRegistration tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.regType = src.regType;
            tar.exclusiveIncentive = src.exclusiveIncentive;
            tar.regDate = src.regDate;
            tar.effStartDate = src.effStartDate;
            tar.effEnddate = src.effEnddate;
            tar.isAgreed = src.isAgreed;
            tar.firstName = src.firstName;
            tar.lastName = src.lastName;
            tar.title = src.title;
            tar.phone = src.phone;
            tar.email = src.email;
            tar.addressId = src.addressId;
            tar.confNum = src.confNum;
            tar.userAcct = src.userAcct;
            tar.storeCode = src.storeCode;
            tar.corpId = src.corpId;
            tar.status = src.status;
            tar.rowState = src.rowState;
        }
	#endregion
 
	#region SQL
        [Serializable]
        class AgentRegistrationSQL : SqlGateway
        {
            public AgentRegistration[] getKey(AgentRegistration rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAgentRegistration_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                AgentRegistration rec = (AgentRegistration)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAgentRegistration_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                AgentRegistration rec = (AgentRegistration)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAgentRegistration_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                AgentRegistration rec = (AgentRegistration)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAgentRegistration_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public AgentRegistration[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spAgentRegistration_Get_All";
                return convert(execReader(cmd));
            }
		#endregion

		#region SQL Implementation
            void setParam(SqlCommand cmd, AgentRegistration rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                if (rec.regType == null)
                    cmd.Parameters.Add("@RegType", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.RegType.Length == 0)
                        cmd.Parameters.Add("@RegType", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@RegType", SqlDbType.VarChar, 50).Value = rec.regType;
                }
                
                // Numeric, nullable foreign key treatment:
                if (rec.ExclusiveIncentive == 0)
                    cmd.Parameters.Add("@ExclusiveIncentive", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@ExclusiveIncentive", SqlDbType.Int, 0).Value = rec.exclusiveIncentive;
 
                if (rec.regDate == DateTime.MinValue)
                    cmd.Parameters.Add("@RegDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@RegDate", SqlDbType.DateTime, 0).Value = rec.regDate;
 
                if (rec.effStartDate == DateTime.MinValue)
                    cmd.Parameters.Add("@EffStartDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EffStartDate", SqlDbType.DateTime, 0).Value = rec.effStartDate;
 
                if (rec.effEnddate == DateTime.MinValue)
                    cmd.Parameters.Add("@EffEnddate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EffEnddate", SqlDbType.DateTime, 0).Value = rec.effEnddate;
 
                cmd.Parameters.Add("@IsAgreed", SqlDbType.Bit, 0).Value = rec.isAgreed;
 
                if (rec.firstName == null)
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 100).Value = DBNull.Value;
                else
                {
                    if (rec.FirstName.Length == 0)
                        cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 100).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 100).Value = rec.firstName;
                }
 
                if (rec.lastName == null)
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 100).Value = DBNull.Value;
                else
                {
                    if (rec.LastName.Length == 0)
                        cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 100).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 100).Value = rec.lastName;
                }
                cmd.Parameters.Add("@Title", SqlDbType.Int, 0).Value = rec.title;
 
                if (rec.phone == null)
                    cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 20).Value = DBNull.Value;
                else
                {
                    if (rec.Phone.Length == 0)
                        cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 20).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 20).Value = rec.phone;
                }
 
                if (rec.email == null)
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.Email.Length == 0)
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = rec.email;
                }
                
                // Numeric, nullable foreign key treatment:
                if (rec.AddrId == 0)
                    cmd.Parameters.Add("@MailAddr", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@MailAddr", SqlDbType.Int, 0).Value = rec.addressId;
                cmd.Parameters.Add("@ConfNum", SqlDbType.Int, 0).Value = rec.confNum;
                cmd.Parameters.Add("@UserAcct", SqlDbType.Int, 0).Value = rec.userAcct;
 
                if (rec.storeCode == null)
                    cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.StoreCode.Length == 0)
                        cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 50).Value = rec.storeCode;
                }
                cmd.Parameters.Add("@CorpId", SqlDbType.Int, 0).Value = rec.corpId;
 
                if (rec.status == null)
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.Status.Length == 0)
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 50).Value = rec.status;
                }
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                AgentRegistration rec = new AgentRegistration();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["RegType"] != DBNull.Value)
                    rec.regType = (string) rdr["RegType"];
 
                if (rdr["ExclusiveIncentive"] != DBNull.Value)
                    rec.exclusiveIncentive = (int) rdr["ExclusiveIncentive"];
 
                if (rdr["RegDate"] != DBNull.Value)
                    rec.regDate = (DateTime) rdr["RegDate"];
 
                if (rdr["EffStartDate"] != DBNull.Value)
                    rec.effStartDate = (DateTime) rdr["EffStartDate"];
 
                if (rdr["EffEnddate"] != DBNull.Value)
                    rec.effEnddate = (DateTime) rdr["EffEnddate"];
 
                if (rdr["IsAgreed"] != DBNull.Value)
                    rec.isAgreed = (bool) rdr["IsAgreed"];
 
                if (rdr["FirstName"] != DBNull.Value)
                    rec.firstName = (string) rdr["FirstName"];
 
                if (rdr["LastName"] != DBNull.Value)
                    rec.lastName = (string) rdr["LastName"];
 
                if (rdr["Title"] != DBNull.Value)
                    rec.title = (int) rdr["Title"];
 
                if (rdr["Phone"] != DBNull.Value)
                    rec.phone = (string) rdr["Phone"];
 
                if (rdr["Email"] != DBNull.Value)
                    rec.email = (string) rdr["Email"];
 
                if (rdr["MailAddr"] != DBNull.Value)
                    rec.addressId = (int) rdr["MailAddr"];
 
                if (rdr["ConfNum"] != DBNull.Value)
                    rec.confNum = (int) rdr["ConfNum"];
 
                if (rdr["UserAcct"] != DBNull.Value)
                    rec.userAcct = (int) rdr["UserAcct"];
 
                if (rdr["StoreCode"] != DBNull.Value)
                    rec.storeCode = (string) rdr["StoreCode"];
 
                if (rdr["CorpId"] != DBNull.Value)
                    rec.corpId = (int) rdr["CorpId"];
 
                if (rdr["Status"] != DBNull.Value)
                    rec.status = (string) rdr["Status"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            AgentRegistration[] convert(DomainObj[] objs)
            {
                AgentRegistration[] acls  = new AgentRegistration[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
	#endregion
    }
}