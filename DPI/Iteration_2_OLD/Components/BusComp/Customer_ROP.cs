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
    public class Customer_ROP : DomainObj, ICustomer_ROP
    {
    #region Data
        static string iName = "Customer_ROP";
        int id;
        DateTime dateInserted;
        DateTime dateModified;
        string userId;
        int accNumber;
        string billingFirstName;
        string billingLastName;
        string billingAddress;
        string billingCity;
        string billingState;
        string billingZip;
        string phNumber;
		string emailAddress;
        bool active;
        int accountTypeId;
        string bAccNumber;
        string bRouteNumber;
        string dLStateNumber;
        string expirationMonthYear;
        string cVV2;
        int priority;
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
        public DateTime DateInserted
        {
            get { return dateInserted; }
            set
            {
                setState();
                dateInserted = value;
            }
        }
        public DateTime DateModified
        {
            get { return dateModified; }
            set
            {
                setState();
                dateModified = value;
            }
        }
        public string UserId
        {
            get { return userId; }
            set
            {
                setState();
                userId = value;
            }
        }
        public int AccNumber
        {
            get { return accNumber; }
            set
            {
                setState();
                accNumber = value;
            }
        }
        public string BillingFirstName
        {
            get { return billingFirstName; }
            set
            {
                setState();
                billingFirstName = value;
            }
        }
        public string BillingLastName
        {
            get { return billingLastName; }
            set
            {
                setState();
                billingLastName = value;
            }
        }
        public string BillingAddress
        {
            get { return billingAddress; }
            set
            {
                setState();
                billingAddress = value;
            }
        }
        public string BillingCity
        {
            get { return billingCity; }
            set
            {
                setState();
                billingCity = value;
            }
        }
        public string BillingState
        {
            get { return billingState; }
            set
            {
                setState();
                billingState = value;
            }
        }
        public string BillingZip
        {
            get { return billingZip; }
            set
            {
                setState();
                billingZip = value;
            }
        }
        public string PhNumber
        {
            get { return phNumber; }
            set
            {
                setState();
                phNumber = value;
            }
        }
		public string EmailAddress
		{
			get { return emailAddress; }
			set
			{
				setState();
				emailAddress = value;
			}
		}
        public bool Active
        {
            get { return active; }
            set
            {
                setState();
                active = value;
            }
        }
        public int AccountTypeId
        {
            get { return accountTypeId; }
            set
            {
                setState();
                accountTypeId = value;
            }
        }
        public string BAccNumber
        {
            get { return bAccNumber; }
            set
            {
                setState();
                bAccNumber = value;
            }
        }
        public string BRouteNumber
        {
            get { return bRouteNumber; }
            set
            {
                setState();
                bRouteNumber = value;
            }
        }
        public string DLStateNumber
        {
            get { return dLStateNumber; }
            set
            {
                setState();
                dLStateNumber = value;
            }
        }
        public string ExpirationMonthYear
        {
            get { return expirationMonthYear; }
            set
            {
                setState();
                expirationMonthYear = value;
            }
        }
        public string CVV2
        {
            get { return cVV2; }
            set
            {
                setState();
                cVV2 = value;
            }
        }
        public int Priority
        {
            get { return priority; }
            set
            {
                setState();
                priority = value;
            }
        }
    #endregion
        
    #region Constructors
        public Customer_ROP()
        {
            sql = new Customer_ROPSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public Customer_ROP(UOW uow) : this()
        {
            if(uow == null)
                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
            if(uow.Imap == null)
                throw new ArgumentException("IdentityMap is required", "Identity Map");
            
            this.uow = uow;
            this.uow.Imap.add(this);
        }
    #endregion
        
    #region Methods
        protected override SqlGateway loadSql()
        {
            return new Customer_ROPSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
    #endregion

    #region	Static methods
        public static Customer_ROP find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(Customer_ROP.getKey(id)))
                return (Customer_ROP)uow.Imap.find(Customer_ROP.getKey(id));
            
            Customer_ROP cls = new Customer_ROP();
            cls.uow = uow;
            cls.id = id;
            cls = (Customer_ROP)DomainObj.addToIMap(uow, getOne(((Customer_ROPSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
		public static ICustomer_ROP[] GetCustROPByAccount(UOW uow, int accNumber)
		{
			Customer_ROP[] objs = (Customer_ROP[])DomainObj.addToIMap(uow, (new Customer_ROPSQL()).GetCustROPByAccount(uow, accNumber));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
        public static Customer_ROP[] getAll(UOW uow)
        {
            Customer_ROP[] objs = (Customer_ROP[])DomainObj.addToIMap(uow, (new Customer_ROPSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
    #endregion

    #region Implementation
        static Customer_ROP getOne(Customer_ROP[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Customer_ROP src, Customer_ROP tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.dateInserted = src.dateInserted;
            tar.dateModified = src.dateModified;
            tar.userId = src.userId;
            tar.accNumber = src.accNumber;
            tar.billingFirstName = src.billingFirstName;
            tar.billingLastName = src.billingLastName;
            tar.billingAddress = src.billingAddress;
            tar.billingCity = src.billingCity;
            tar.billingState = src.billingState;
            tar.billingZip = src.billingZip;
            tar.phNumber = src.phNumber;
			tar.emailAddress = src.emailAddress;
            tar.active = src.active;
            tar.accountTypeId = src.accountTypeId;
            tar.bAccNumber = src.bAccNumber;
            tar.bRouteNumber = src.bRouteNumber;
            tar.dLStateNumber = src.dLStateNumber;
            tar.expirationMonthYear = src.expirationMonthYear;
            tar.cVV2 = src.cVV2;
            tar.priority = src.priority;
            tar.rowState = src.rowState;
        }
    #endregion
 
    #region	SQL
        [Serializable]
        class Customer_ROPSQL : SqlGateway
        {
            public Customer_ROP[] getKey(Customer_ROP rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCustomer_ROP_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                Customer_ROP rec = (Customer_ROP)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCustomer_ROP_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Customer_ROP rec = (Customer_ROP)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCustomer_ROP_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Customer_ROP rec = (Customer_ROP)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCustomer_ROP_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public Customer_ROP[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spCustomer_ROP_Get_All";
                return convert(execReader(cmd));
            }
			public Customer_ROP[] GetCustROPByAccount(UOW uow, int accNumber)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spCustomer_ROP_Get_ByAccount";
				cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = accNumber;
				return convert(execReader(cmd));
			}
        #region Implementation
            void setParam(SqlCommand cmd, Customer_ROP rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                //cmd.Parameters.Add("@DateInserted", SqlDbType.DateTime, 0).Value = rec.dateInserted;
 
                cmd.Parameters.Add("@DateModified", SqlDbType.DateTime, 0).Value = DateTime.Now;
 
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50).Value = rec.userId;
                
                // Numeric, nullable foreign key treatment:
                if (rec.AccNumber == 0)
                    cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = rec.accNumber;
 
                cmd.Parameters.Add("@BillingFirstName", SqlDbType.VarChar, 25).Value = rec.billingFirstName;
 
                cmd.Parameters.Add("@BillingLastName", SqlDbType.VarChar, 30).Value = rec.billingLastName;
 
                cmd.Parameters.Add("@BillingAddress", SqlDbType.VarChar, 100).Value = rec.billingAddress;
 
                cmd.Parameters.Add("@BillingCity", SqlDbType.VarChar, 40).Value = rec.billingCity;
 
                cmd.Parameters.Add("@BillingState", SqlDbType.VarChar, 2).Value = rec.billingState;
 
                cmd.Parameters.Add("@BillingZip", SqlDbType.VarChar, 5).Value = rec.billingZip;
 
                if (rec.phNumber == null)
                    cmd.Parameters.Add("@PhNumber", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.PhNumber.Length == 0)
                        cmd.Parameters.Add("@PhNumber", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@PhNumber", SqlDbType.VarChar, 10).Value = rec.phNumber;
                }

				if (rec.emailAddress == null)
					cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.emailAddress.Length == 0)
						cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = rec.EmailAddress;
				}
 
                cmd.Parameters.Add("@Active", SqlDbType.Bit, 0).Value = rec.active;
                cmd.Parameters.Add("@AccountTypeId", SqlDbType.Int, 0).Value = rec.accountTypeId;
 
                cmd.Parameters.Add("@BAccNumber", SqlDbType.VarChar, 50).Value = rec.bAccNumber;
 
                if (rec.bRouteNumber == null)
                    cmd.Parameters.Add("@BRouteNumber", SqlDbType.VarChar, 20).Value = DBNull.Value;
                else
                {
                    if (rec.BRouteNumber.Length == 0)
                        cmd.Parameters.Add("@BRouteNumber", SqlDbType.VarChar, 20).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@BRouteNumber", SqlDbType.VarChar, 20).Value = rec.bRouteNumber;
                }
 
                if (rec.dLStateNumber == null)
                    cmd.Parameters.Add("@DLStateNumber", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.DLStateNumber.Length == 0)
                        cmd.Parameters.Add("@DLStateNumber", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@DLStateNumber", SqlDbType.VarChar, 50).Value = rec.dLStateNumber;
                }
 
                if (rec.expirationMonthYear == null)
                    cmd.Parameters.Add("@ExpirationMonthYear", SqlDbType.VarChar, 6).Value = DBNull.Value;
                else
                {
                    if (rec.ExpirationMonthYear.Length == 0)
                        cmd.Parameters.Add("@ExpirationMonthYear", SqlDbType.VarChar, 6).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ExpirationMonthYear", SqlDbType.VarChar, 6).Value = rec.expirationMonthYear;
                }
 
                if (rec.cVV2 == null)
                    cmd.Parameters.Add("@CVV2", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.CVV2.Length == 0)
                        cmd.Parameters.Add("@CVV2", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@CVV2", SqlDbType.VarChar, 50).Value = rec.cVV2;
                }
                cmd.Parameters.Add("@Priority", SqlDbType.Int, 0).Value = rec.priority;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                Customer_ROP rec = new Customer_ROP();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["DateInserted"] != DBNull.Value)
                    rec.dateInserted = (DateTime) rdr["DateInserted"];
 
                if (rdr["DateModified"] != DBNull.Value)
                    rec.dateModified = (DateTime) rdr["DateModified"];
 
                if (rdr["UserId"] != DBNull.Value)
                    rec.userId = (string) rdr["UserId"];
 
                if (rdr["AccNumber"] != DBNull.Value)
                    rec.accNumber = (int) rdr["AccNumber"];
 
                if (rdr["BillingFirstName"] != DBNull.Value)
                    rec.billingFirstName = (string) rdr["BillingFirstName"];
 
                if (rdr["BillingLastName"] != DBNull.Value)
                    rec.billingLastName = (string) rdr["BillingLastName"];
 
                if (rdr["BillingAddress"] != DBNull.Value)
                    rec.billingAddress = (string) rdr["BillingAddress"];
 
                if (rdr["BillingCity"] != DBNull.Value)
                    rec.billingCity = (string) rdr["BillingCity"];
 
                if (rdr["BillingState"] != DBNull.Value)
                    rec.billingState = (string) rdr["BillingState"];
 
                if (rdr["BillingZip"] != DBNull.Value)
                    rec.billingZip = (string) rdr["BillingZip"];
 
                if (rdr["PhNumber"] != DBNull.Value)
                    rec.phNumber = (string) rdr["PhNumber"];

				if (rdr["EmailAddress"] != DBNull.Value)
					rec.emailAddress = (string) rdr["EmailAddress"];
 
                if (rdr["Active"] != DBNull.Value)
                    rec.active = (bool) rdr["Active"];
 
				if (rdr["AccountTypeId"] != DBNull.Value)
					rec.accountTypeId = int.Parse(rdr["AccountTypeId"].ToString());
				
                if (rdr["BAccNumber"] != DBNull.Value)
                    rec.bAccNumber = (string) rdr["BAccNumber"];
 
                if (rdr["BRouteNumber"] != DBNull.Value)
                    rec.bRouteNumber = (string) rdr["BRouteNumber"];
 
                if (rdr["DLStateNumber"] != DBNull.Value)
                    rec.dLStateNumber = (string) rdr["DLStateNumber"];
 
                if (rdr["ExpirationMonthYear"] != DBNull.Value)
                    rec.expirationMonthYear = (string) rdr["ExpirationMonthYear"];
 
                if (rdr["CVV2"] != DBNull.Value)
                    rec.cVV2 = (string) rdr["CVV2"];
 
                if (rdr["Priority"] != DBNull.Value)
                    rec.priority = int.Parse(rdr["Priority"].ToString());
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            Customer_ROP[] convert(DomainObj[] objs)
            {
                Customer_ROP[] acls  = new Customer_ROP[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        #endregion
        }
    #endregion
    }
}
