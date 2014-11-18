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
    public class CustConversion : DomainObj, ICustConversion
    {
        /*        Data        */
        static string iName = "CustConversion";
        string convName;
        int exclCorp;
        string exclAgent;
        DateTime startDate;
        DateTime endDate;
        string status;
        string description;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, convName); }
        }
        public string ConvName
        {
            get { return convName; }
            set
            {
                setState();
                convName = value;
            }
        }
        public int ExclCorp
        {
            get { return exclCorp; }
            set
            {
                setState();
                exclCorp = value;
            }
        }
        public string ExclAgent
        {
            get { return exclAgent; }
            set
            {
                setState();
                exclAgent = value;
            }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                setState();
                startDate = value;
            }
        }
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                setState();
                endDate = value;
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
        public string Description
        {
            get { return description; }
            set
            {
                setState();
                description = value;
            }
        }
        
        /*        Constructors			*/
        public CustConversion()
        {
            sql = new CustConversionSQL();
            rowState = RowState.New;
        }
        public CustConversion(UOW uow) : this()
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
            return new CustConversionSQL();
        }
        public override void checkExists()
        {
            if ((ConvName ==  null))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static CustConversion find(UOW uow, string convName)
        {
            if (uow.Imap.keyExists(CustConversion.getKey(convName)))
                return (CustConversion)uow.Imap.find(CustConversion.getKey(convName));
            
            CustConversion cls = new CustConversion();
            cls.uow = uow;
            cls.convName = convName;
            cls = (CustConversion)DomainObj.addToIMap(uow, getOne(((CustConversionSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static CustConversion[] getAll(UOW uow)
        {
            CustConversion[] objs = (CustConversion[])DomainObj.addToIMap(uow, (new CustConversionSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
		public static CustConversion[] getStore(UOW uow, string storeCode, DateTime date)
		{
			CustConversion[] objs = (CustConversion[])DomainObj.addToIMap(
				uow, (new CustConversionSQL()).getStore(uow, storeCode, date));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static CustConversion[] getCorp(UOW uow, int corp, DateTime date)
		{
			CustConversion[] objs = (CustConversion[])DomainObj.addToIMap(
				uow, (new CustConversionSQL()).getCorp(uow, corp, date));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
        public static Key getKey(string convName)
        {
            return new Key(iName, convName.ToString());
        }
        /*		Implementation		*/
        static CustConversion getOne(CustConversion[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(CustConversion src, CustConversion tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.convName = src.convName;
            tar.exclCorp = src.exclCorp;
            tar.exclAgent = src.exclAgent;
            tar.startDate = src.startDate;
            tar.endDate = src.endDate;
            tar.status = src.status;
            tar.description = src.description;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class CustConversionSQL : SqlGateway
        {
            public CustConversion[] getKey(CustConversion rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCustConversion_Get_Id";
                cmd.Parameters.Add("@ConvName", SqlDbType.VarChar, 50).Value = rec.convName;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                CustConversion rec = (CustConversion)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCustConversion_Ins";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                CustConversion rec = (CustConversion)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCustConversion_Del_Id";
                cmd.Parameters.Add("@ConvName", SqlDbType.VarChar, 50).Value = rec.convName;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                CustConversion rec = (CustConversion)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCustConversion_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public CustConversion[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spCustConversion_Get_All";
                return convert(execReader(cmd));
            }
			public CustConversion[] getStore(UOW uow, string storeCode, DateTime date)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spCustConversion_Get_Store";
				cmd.Parameters.Add("@ExclAgent", SqlDbType.VarChar, 10).Value = storeCode;
				cmd.Parameters.Add("@Date", SqlDbType.DateTime, 0).Value = date;
				
				return convert(execReader(cmd));
			}
			public CustConversion[] getCorp(UOW uow, int corp, DateTime date)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spCustConversion_Get_Corp";
				cmd.Parameters.Add("@ExclCorp", SqlDbType.Int, 4).Value = corp;
				cmd.Parameters.Add("@Date", SqlDbType.DateTime, 0).Value = date;
				
				return convert(execReader(cmd));
			}

            /*        Implementation        */
            void setParam(SqlCommand cmd, CustConversion rec)
            {
 
                cmd.Parameters.Add("@ConvName", SqlDbType.VarChar, 50).Value = rec.convName;
                
                // Numeric, nullable foreign key treatment:
                if (rec.ExclCorp == 0)
                    cmd.Parameters.Add("@ExclCorp", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@ExclCorp", SqlDbType.Int, 0).Value = rec.exclCorp;
 
                if (rec.exclAgent == null)
                    cmd.Parameters.Add("@ExclAgent", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.ExclAgent.Length == 0)
                        cmd.Parameters.Add("@ExclAgent", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ExclAgent", SqlDbType.VarChar, 10).Value = rec.exclAgent;
                }
 
                if (rec.startDate == DateTime.MinValue)
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = rec.startDate;
 
                if (rec.endDate == DateTime.MinValue)
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = rec.endDate;
 
                if (rec.status == null)
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.Status.Length == 0)
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = rec.status;
                }
 
                if (rec.description == null)
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.Description.Length == 0)
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 50).Value = rec.description;
                }
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                CustConversion rec = new CustConversion();
                
                if (rdr["ConvName"] != DBNull.Value)
                    rec.convName = (string) rdr["ConvName"];
 
                if (rdr["ExclCorp"] != DBNull.Value)
                    rec.exclCorp = (int) rdr["ExclCorp"];
 
                if (rdr["ExclAgent"] != DBNull.Value)
                    rec.exclAgent = (string) rdr["ExclAgent"];
 
                if (rdr["StartDate"] != DBNull.Value)
                    rec.startDate = (DateTime) rdr["StartDate"];
 
                if (rdr["EndDate"] != DBNull.Value)
                    rec.endDate = (DateTime) rdr["EndDate"];
 
                if (rdr["Status"] != DBNull.Value)
                    rec.status = (string) rdr["Status"];
 
                if (rdr["Description"] != DBNull.Value)
                    rec.description = (string) rdr["Description"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            CustConversion[] convert(DomainObj[] objs)
            {
                CustConversion[] acls  = new CustConversion[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
