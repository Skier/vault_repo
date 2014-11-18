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
    public class Wireless_Custdata : DomainObj, IWireless_Custdata
    {
    #region Data
        static string iName = "Wireless_Custdata";
        int iD;
        string eSN;
        string phNumber;
        string subscriberId;
        string nameFirst;
        string nameLast;
        string addr1;
        string addr2;
        string city;
        string state;
        string zip;
        string email;
        string contactNumber;
    #endregion
        
    #region Properties
        public override IDomKey IKey 
        {
             get { return new Key(iName, iD.ToString()); }
        }
        public int ID
        {
            get { return iD; }
        }
        public string ESN
        {
            get { return eSN; }
            set
            {
                setState();
                eSN = value;
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
        public string SubscriberId
        {
            get { return subscriberId; }
            set
            {
                setState();
                subscriberId = value;
            }
        }
        public string NameFirst
        {
            get { return nameFirst; }
            set
            {
                setState();
                nameFirst = value;
            }
        }
        public string NameLast
        {
            get { return nameLast; }
            set
            {
                setState();
                nameLast = value;
            }
        }
        public string Addr1
        {
            get { return addr1; }
            set
            {
                setState();
                addr1 = value;
            }
        }
        public string Addr2
        {
            get { return addr2; }
            set
            {
                setState();
                addr2 = value;
            }
        }
        public string City
        {
            get { return city; }
            set
            {
                setState();
                city = value;
            }
        }
        public string State
        {
            get { return state; }
            set
            {
                setState();
                state = value;
            }
        }
        public string Zip
        {
            get { return zip; }
            set
            {
                setState();
                zip = value;
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
        public string ContactNumber
        {
            get { return contactNumber; }
            set
            {
                setState();
                contactNumber = value;
            }
        }
    #endregion
        
    #region Constructors
        public Wireless_Custdata()
        {
            sql = new Wireless_CustdataSQL();
            iD = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public Wireless_Custdata(UOW uow) : this()
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
            return new Wireless_CustdataSQL();
        }
        public override void checkExists()
        {
            if ((ID < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
    #endregion

    #region	Static methods
        public static Wireless_Custdata find(UOW uow, int iD)
        {
            if (uow.Imap.keyExists(Wireless_Custdata.getKey(iD)))
                return (Wireless_Custdata)uow.Imap.find(Wireless_Custdata.getKey(iD));
            
            Wireless_Custdata cls = new Wireless_Custdata();
            cls.uow = uow;
            cls.iD = iD;
            cls = (Wireless_Custdata)DomainObj.addToIMap(uow, getOne(((Wireless_CustdataSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static Wireless_Custdata[] getAll(UOW uow)
        {
            Wireless_Custdata[] objs = (Wireless_Custdata[])DomainObj.addToIMap(uow, (new Wireless_CustdataSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
		public static IWireless_Custdata[] GetByPhoneOrEsn(UOW uow, string phoneOrEsn)
		{
			Wireless_Custdata[] objs = (Wireless_Custdata[])DomainObj.addToIMap(uow, (new Wireless_CustdataSQL()).GetByPhoneOrEsn(uow, phoneOrEsn));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return (IWireless_Custdata[])objs;
		}
        public static Key getKey(int iD)
        {
            return new Key(iName, iD.ToString());
        }
    #endregion

    #region Implementation
        static Wireless_Custdata getOne(Wireless_Custdata[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Wireless_Custdata src, Wireless_Custdata tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.iD = src.iD;
            tar.eSN = src.eSN;
            tar.phNumber = src.phNumber;
            tar.subscriberId = src.subscriberId;
            tar.nameFirst = src.nameFirst;
            tar.nameLast = src.nameLast;
            tar.addr1 = src.addr1;
            tar.addr2 = src.addr2;
            tar.city = src.city;
            tar.state = src.state;
            tar.zip = src.zip;
            tar.email = src.email;
            tar.contactNumber = src.contactNumber;
            tar.rowState = src.rowState;
        }
    #endregion
 
    #region	SQL
        [Serializable]
        class Wireless_CustdataSQL : SqlGateway
        {
            public Wireless_Custdata[] getKey(Wireless_Custdata rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWireless_Custdata_Get_Id";
                cmd.Parameters.Add("@ID", SqlDbType.Int, 0).Value = rec.iD;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                Wireless_Custdata rec = (Wireless_Custdata)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWireless_Custdata_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@ID"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.iD = (int)cmd.Parameters["@ID"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Wireless_Custdata rec = (Wireless_Custdata)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWireless_Custdata_Del_Id";
                cmd.Parameters.Add("@ID", SqlDbType.Int, 0).Value = rec.iD;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Wireless_Custdata rec = (Wireless_Custdata)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWireless_Custdata_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public Wireless_Custdata[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spWireless_Custdata_Get_All";
                return convert(execReader(cmd));
            }
			public Wireless_Custdata[] GetByPhoneOrEsn(UOW uow, string phoneOrEsn)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spWireless_Custdata_Get_ByPhoneOrEsn";
				cmd.Parameters.Add("@PhoneOrEsn", SqlDbType.Int, 0).Value = phoneOrEsn;

				return convert(execReader(cmd));
			}
		
        #region Implementation
            void setParam(SqlCommand cmd, Wireless_Custdata rec)
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int, 0).Value = rec.iD;
 
                cmd.Parameters.Add("@ESN", SqlDbType.VarChar, 25).Value = rec.eSN;
 
                if (rec.phNumber == null)
                    cmd.Parameters.Add("@PhNumber", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.PhNumber.Length == 0)
                        cmd.Parameters.Add("@PhNumber", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@PhNumber", SqlDbType.VarChar, 10).Value = rec.phNumber;
                }
 
                if (rec.subscriberId == null)
                    cmd.Parameters.Add("@SubscriberId", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.SubscriberId.Length == 0)
                        cmd.Parameters.Add("@SubscriberId", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@SubscriberId", SqlDbType.VarChar, 10).Value = rec.subscriberId;
                }
 
                if (rec.nameFirst == null)
                    cmd.Parameters.Add("@NameFirst", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.NameFirst.Length == 0)
                        cmd.Parameters.Add("@NameFirst", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@NameFirst", SqlDbType.VarChar, 50).Value = rec.nameFirst;
                }
 
                if (rec.nameLast == null)
                    cmd.Parameters.Add("@NameLast", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.NameLast.Length == 0)
                        cmd.Parameters.Add("@NameLast", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@NameLast", SqlDbType.VarChar, 50).Value = rec.nameLast;
                }
 
                if (rec.addr1 == null)
                    cmd.Parameters.Add("@Addr1", SqlDbType.VarChar, 200).Value = DBNull.Value;
                else
                {
                    if (rec.Addr1.Length == 0)
                        cmd.Parameters.Add("@Addr1", SqlDbType.VarChar, 200).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Addr1", SqlDbType.VarChar, 200).Value = rec.addr1;
                }
 
                if (rec.addr2 == null)
                    cmd.Parameters.Add("@Addr2", SqlDbType.VarChar, 100).Value = DBNull.Value;
                else
                {
                    if (rec.Addr2.Length == 0)
                        cmd.Parameters.Add("@Addr2", SqlDbType.VarChar, 100).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Addr2", SqlDbType.VarChar, 100).Value = rec.addr2;
                }
 
                if (rec.city == null)
                    cmd.Parameters.Add("@City", SqlDbType.VarChar, 100).Value = DBNull.Value;
                else
                {
                    if (rec.City.Length == 0)
                        cmd.Parameters.Add("@City", SqlDbType.VarChar, 100).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@City", SqlDbType.VarChar, 100).Value = rec.city;
                }
 
                if (rec.state == null)
                    cmd.Parameters.Add("@State", SqlDbType.VarChar, 2).Value = DBNull.Value;
                else
                {
                    if (rec.State.Length == 0)
                        cmd.Parameters.Add("@State", SqlDbType.VarChar, 2).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@State", SqlDbType.VarChar, 2).Value = rec.state;
                }
 
                if (rec.zip == null)
                    cmd.Parameters.Add("@Zip", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.Zip.Length == 0)
                        cmd.Parameters.Add("@Zip", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Zip", SqlDbType.VarChar, 10).Value = rec.zip;
                }
 
                if (rec.email == null)
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = DBNull.Value;
                else
                {
                    if (rec.Email.Length == 0)
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = rec.email;
                }
 
                if (rec.contactNumber == null)
                    cmd.Parameters.Add("@ContactNumber", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.ContactNumber.Length == 0)
                        cmd.Parameters.Add("@ContactNumber", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ContactNumber", SqlDbType.VarChar, 10).Value = rec.contactNumber;
                }
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                Wireless_Custdata rec = new Wireless_Custdata();
                
                if (rdr["ID"] != DBNull.Value)
                    rec.iD = (int) rdr["ID"];
 
                if (rdr["ESN"] != DBNull.Value)
                    rec.eSN = (string) rdr["ESN"];
 
                if (rdr["PhNumber"] != DBNull.Value)
                    rec.phNumber = (string) rdr["PhNumber"];
 
                if (rdr["SubscriberId"] != DBNull.Value)
                    rec.subscriberId = (string) rdr["SubscriberId"];
 
                if (rdr["NameFirst"] != DBNull.Value)
                    rec.nameFirst = (string) rdr["NameFirst"];
 
                if (rdr["NameLast"] != DBNull.Value)
                    rec.nameLast = (string) rdr["NameLast"];
 
                if (rdr["Addr1"] != DBNull.Value)
                    rec.addr1 = (string) rdr["Addr1"];
 
                if (rdr["Addr2"] != DBNull.Value)
                    rec.addr2 = (string) rdr["Addr2"];
 
                if (rdr["City"] != DBNull.Value)
                    rec.city = (string) rdr["City"];
 
                if (rdr["State"] != DBNull.Value)
                    rec.state = (string) rdr["State"];
 
                if (rdr["Zip"] != DBNull.Value)
                    rec.zip = (string) rdr["Zip"];
 
                if (rdr["Email"] != DBNull.Value)
                    rec.email = (string) rdr["Email"];
 
                if (rdr["ContactNumber"] != DBNull.Value)
                    rec.contactNumber = (string) rdr["ContactNumber"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            Wireless_Custdata[] convert(DomainObj[] objs)
            {
                Wireless_Custdata[] acls  = new Wireless_Custdata[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        #endregion
        }
    #endregion
    }
}
