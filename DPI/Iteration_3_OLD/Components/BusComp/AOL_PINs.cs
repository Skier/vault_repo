using System;
using System.Data;
using System.Data.SqlClient;
using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Components
{
    [Serializable]
    public class AOL_PINs : DomainObj
    {
        /*        Data        */
        static string iName = "AOL_PINs";
        int id;
        string pIN;
        DateTime createDate;
        DateTime issueDate;
        bool active;
        string wireless_product_id;
        DateTime expirationDate;
        string batchID;
        string pricePlanID;
        decimal vendorPrice;
        string serialNumber;
        string pinType;
        string status;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
        }
        public string PIN
        {
            get { return pIN; }
            set
            {
                setState();
                pIN = value;
            }
        }
        public DateTime CreateDate
        {
            get { return createDate; }
            set
            {
                setState();
                createDate = value;
            }
        }
        public DateTime IssueDate
        {
            get { return issueDate; }
            set
            {
                setState();
                issueDate = value;
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
        public string Wireless_product_id
        {
            get { return wireless_product_id; }
            set
            {
                setState();
                wireless_product_id = value;
            }
        }
        public DateTime ExpirationDate
        {
            get { return expirationDate; }
            set
            {
                setState();
                expirationDate = value;
            }
        }
        public string BatchID
        {
            get { return batchID; }
            set
            {
                setState();
                batchID = value;
            }
        }
        public string PricePlanID
        {
            get { return pricePlanID; }
            set
            {
                setState();
                pricePlanID = value;
            }
        }
        public decimal VendorPrice
        {
            get { return vendorPrice; }
            set
            {
                setState();
                vendorPrice = Decimal.Round(value, 2);
            }
        }
        public string SerialNumber
        {
            get { return serialNumber; }
            set
            {
                setState();
                serialNumber = value;
            }
        }
        public string PinType
        {
            get { return pinType; }
            set
            {
                setState();
                pinType = value;
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
        
        /*        Constructors			*/
        public AOL_PINs()
        {
            sql = new AOL_PINsSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public AOL_PINs(UOW uow) : this()
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
            return new AOL_PINsSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static AOL_PINs find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(AOL_PINs.getKey(id)))
                return (AOL_PINs)uow.Imap.find(AOL_PINs.getKey(id));
            
            AOL_PINs cls = new AOL_PINs();
            cls.uow = uow;
            cls.id = id;
            cls = (AOL_PINs)DomainObj.addToIMap(uow, getOne(((AOL_PINsSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
		public static AOL_PINs ReservePIN(UOW uow, int wirelessProd)
		{
			AOL_PINs cls 
				= (AOL_PINs)DomainObj.addToIMap(uow, getOne(new AOL_PINsSQL().ReservePIN(uow, wirelessProd)));
			
			cls.uow = uow;
			return cls;
		}
        public static AOL_PINs[] getAll(UOW uow)
        {
            AOL_PINs[] objs = (AOL_PINs[])DomainObj.addToIMap(uow, (new AOL_PINsSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
        static AOL_PINs getOne(AOL_PINs[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(AOL_PINs src, AOL_PINs tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.pIN = src.pIN;
            tar.createDate = src.createDate;
            tar.issueDate = src.issueDate;
            tar.active = src.active;
            tar.wireless_product_id = src.wireless_product_id;
            tar.expirationDate = src.expirationDate;
            tar.batchID = src.batchID;
            tar.pricePlanID = src.pricePlanID;
            tar.vendorPrice = src.vendorPrice;
            tar.serialNumber = src.serialNumber;
            tar.pinType = src.pinType;
            tar.status = src.status;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class AOL_PINsSQL : SqlGateway
        {
            public AOL_PINs[] getKey(AOL_PINs rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAOL_PINs_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                AOL_PINs rec = (AOL_PINs)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAOL_PINs_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                AOL_PINs rec = (AOL_PINs)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAOL_PINs_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                AOL_PINs rec = (AOL_PINs)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAOL_PINs_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public AOL_PINs[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spAOL_PINs_Get_All";
                return convert(execReader(cmd));
            }
			public AOL_PINs[] ReservePIN(UOW uow, int wirelessProd)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spAOL_ReservePin";
                cmd.Parameters.Add("@WLProd", SqlDbType.Int, 0).Value = wirelessProd;
				return convert(execReader(cmd));
			}

            /*        Implementation        */
            void setParam(SqlCommand cmd, AOL_PINs rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                cmd.Parameters.Add("@PIN", SqlDbType.VarChar, 255).Value = rec.pIN;
 
                cmd.Parameters.Add("@CreateDate", SqlDbType.DateTime, 0).Value = rec.createDate;
 
                if (rec.issueDate == DateTime.MinValue)
                    cmd.Parameters.Add("@IssueDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@IssueDate", SqlDbType.DateTime, 0).Value = rec.issueDate;
 
                cmd.Parameters.Add("@Active", SqlDbType.Bit, 0).Value = rec.active;
 
                cmd.Parameters.Add("@Wireless_product_id", SqlDbType.VarChar, 255).Value = rec.wireless_product_id;
 
                cmd.Parameters.Add("@ExpirationDate", SqlDbType.DateTime, 0).Value = rec.expirationDate;
 
                cmd.Parameters.Add("@BatchID", SqlDbType.VarChar, 50).Value = rec.batchID;
 
                cmd.Parameters.Add("@PricePlanID", SqlDbType.VarChar, 50).Value = rec.pricePlanID;
                cmd.Parameters.Add("@VendorPrice", SqlDbType.Decimal, 0).Value = rec.vendorPrice;
 
                cmd.Parameters.Add("@SerialNumber", SqlDbType.VarChar, 50).Value = rec.serialNumber;
 
                if (rec.pinType == null)
                    cmd.Parameters.Add("@PinType", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.PinType.Length == 0)
                        cmd.Parameters.Add("@PinType", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@PinType", SqlDbType.VarChar, 25).Value = rec.pinType;
                }
 
                if (rec.status == null)
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.Status.Length == 0)
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = rec.status;
                }
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                AOL_PINs rec = new AOL_PINs();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["PIN"] != DBNull.Value)
                    rec.pIN = (string) rdr["PIN"];
 
                if (rdr["CreateDate"] != DBNull.Value)
                    rec.createDate = (DateTime) rdr["CreateDate"];
 
                if (rdr["IssueDate"] != DBNull.Value)
                    rec.issueDate = (DateTime) rdr["IssueDate"];
 
                if (rdr["Active"] != DBNull.Value)
                    rec.active = (bool) rdr["Active"];
 
                if (rdr["Wireless_product_id"] != DBNull.Value)
                    rec.wireless_product_id = (string) rdr["Wireless_product_id"];
 
                if (rdr["ExpirationDate"] != DBNull.Value)
                    rec.expirationDate = (DateTime) rdr["ExpirationDate"];
 
                if (rdr["BatchID"] != DBNull.Value)
                    rec.batchID = (string) rdr["BatchID"];
 
                if (rdr["PricePlanID"] != DBNull.Value)
                    rec.pricePlanID = (string) rdr["PricePlanID"];
 
                if (rdr["VendorPrice"] != DBNull.Value)
                    rec.vendorPrice = Decimal.Round((decimal)rdr["VendorPrice"], 2);
 
                if (rdr["SerialNumber"] != DBNull.Value)
                    rec.serialNumber = (string) rdr["SerialNumber"];
 
                if (rdr["PinType"] != DBNull.Value)
                    rec.pinType = (string) rdr["PinType"];
 
                if (rdr["Status"] != DBNull.Value)
                    rec.status = (string) rdr["Status"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            AOL_PINs[] convert(DomainObj[] objs)
            {
                AOL_PINs[] acls  = new AOL_PINs[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
