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
    public class WirelessZipCode : DomainObj
    {
    #region Data
        static string iName = "WirelessZipCode";
        string zipcode;
        string zip_Postal_City;
        string state;
        string sPCS_Customer_Service_ID;
    #endregion
        
    #region Properties
        public override IDomKey IKey 
        {
             get { return new Key(iName, zipcode); }
        }
        public string Zipcode
        {
            get { return zipcode; }
            set
            {
                setState();
                zipcode = value;
            }
        }
        public string Zip_Postal_City
        {
            get { return zip_Postal_City; }
            set
            {
                setState();
                zip_Postal_City = value;
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
        public string SPCS_Customer_Service_ID
        {
            get { return sPCS_Customer_Service_ID; }
            set
            {
                setState();
                sPCS_Customer_Service_ID = value;
            }
        }
    #endregion
        
    #region Constructors
        public WirelessZipCode()
        {
            sql = new WirelessZipCodeSQL();
            rowState = RowState.New;
        }
        public WirelessZipCode(UOW uow) : this()
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
            return new WirelessZipCodeSQL();
        }
        public override void checkExists()
        {
            if ((Zipcode ==  null))
                throw new ArgumentException("Valid row is required for update and delete");
        }
    #endregion

    #region	Static methods
        public static WirelessZipCode find(UOW uow, string zipcode)
        {
            if (uow.Imap.keyExists(WirelessZipCode.getKey(zipcode)))
                return (WirelessZipCode)uow.Imap.find(WirelessZipCode.getKey(zipcode));
            
            WirelessZipCode cls = new WirelessZipCode();
            cls.uow = uow;
            cls.zipcode = zipcode;
            cls = (WirelessZipCode)DomainObj.addToIMap(uow, getOne(((WirelessZipCodeSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static WirelessZipCode[] getAll(UOW uow)
        {
            WirelessZipCode[] objs = (WirelessZipCode[])DomainObj.addToIMap(uow, (new WirelessZipCodeSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
		public static bool MatchZipCode(UOW uow, string zipCode)
		{
			return new WirelessZipCodeSQL().MatchZipCode(uow, zipCode) == 1;
		}
        public static Key getKey(string zipcode)
        {
            return new Key(iName, zipcode.ToString());
        }
    #endregion

    #region Implementation
        static WirelessZipCode getOne(WirelessZipCode[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(WirelessZipCode src, WirelessZipCode tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.zipcode = src.zipcode;
            tar.zip_Postal_City = src.zip_Postal_City;
            tar.state = src.state;
            tar.sPCS_Customer_Service_ID = src.sPCS_Customer_Service_ID;
            tar.rowState = src.rowState;
        }
    #endregion
 
    #region	SQL
        [Serializable]
        class WirelessZipCodeSQL : SqlGateway
        {
            public WirelessZipCode[] getKey(WirelessZipCode rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWirelessZipCode_Get_Id";
                cmd.Parameters.Add("@Zipcode", SqlDbType.Char, 5).Value = rec.zipcode;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                WirelessZipCode rec = (WirelessZipCode)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWirelessZipCode_Ins";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                WirelessZipCode rec = (WirelessZipCode)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWirelessZipCode_Del_Id";
                cmd.Parameters.Add("@Zipcode", SqlDbType.Char, 5).Value = rec.zipcode;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                WirelessZipCode rec = (WirelessZipCode)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWirelessZipCode_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public WirelessZipCode[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spWirelessZipCode_Get_All";
                return convert(execReader(cmd));
            }
			public int MatchZipCode(UOW uow, string zipCode)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spWirelessZipCodeMatch";
				cmd.Parameters.Add("@Zipcode", SqlDbType.Char, 5).Value = zipCode;
				
				return execScalarInt(cmd);
			}

        #region Implementation
            void setParam(SqlCommand cmd, WirelessZipCode rec)
            {
 
                cmd.Parameters.Add("@Zipcode", SqlDbType.VarChar, 5).Value = rec.zipcode;
 
                if (rec.zip_Postal_City == null)
                    cmd.Parameters.Add("@Zip_Postal_City", SqlDbType.VarChar, 100).Value = DBNull.Value;
                else
                {
                    if (rec.Zip_Postal_City.Length == 0)
                        cmd.Parameters.Add("@Zip_Postal_City", SqlDbType.VarChar, 100).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Zip_Postal_City", SqlDbType.VarChar, 100).Value = rec.zip_Postal_City;
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
 
                if (rec.sPCS_Customer_Service_ID == null)
                    cmd.Parameters.Add("@SPCS_Customer_Service_ID", SqlDbType.VarChar, 100).Value = DBNull.Value;
                else
                {
                    if (rec.SPCS_Customer_Service_ID.Length == 0)
                        cmd.Parameters.Add("@SPCS_Customer_Service_ID", SqlDbType.VarChar, 100).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@SPCS_Customer_Service_ID", SqlDbType.VarChar, 100).Value = rec.sPCS_Customer_Service_ID;
                }
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                WirelessZipCode rec = new WirelessZipCode();
                
                if (rdr["Zipcode"] != DBNull.Value)
                    rec.zipcode = (string) rdr["Zipcode"];
 
                if (rdr["Zip_Postal_City"] != DBNull.Value)
                    rec.zip_Postal_City = (string) rdr["Zip_Postal_City"];
 
                if (rdr["State"] != DBNull.Value)
                    rec.state = (string) rdr["State"];
 
                if (rdr["SPCS_Customer_Service_ID"] != DBNull.Value)
                    rec.sPCS_Customer_Service_ID = (string) rdr["SPCS_Customer_Service_ID"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            WirelessZipCode[] convert(DomainObj[] objs)
            {
                WirelessZipCode[] acls  = new WirelessZipCode[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        #endregion
        }
    #endregion
    }
}
