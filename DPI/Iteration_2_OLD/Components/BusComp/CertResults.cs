using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Interfaces;
 
namespace DPI.Components
{
    [Serializable]
    public class CertResult : DomainObj, ICertResult
    {
        /*        Data        */
        static string iName = "CertResult";
        int id;
        string type;
        string storeCode;
        string coworker;
        string name;
        DateTime certDate;
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
        public string Type
        {
            get { return type; }
            set
            {
                setState();
                type = value;
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
        public string Coworker
        {
            get { return coworker; }
            set
            {
                setState();
                coworker = value;
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                setState();
                name = value;
            }
        }
        public DateTime CertDate
        {
            get { return certDate; }
            set
            {
                setState();
                certDate = value;
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
        public CertResult()
        {
            sql = new CertResultSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public CertResult(UOW uow) : this()
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
            return new CertResultSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static CertResult find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(CertResult.getKey(id)))
                return (CertResult)uow.Imap.find(CertResult.getKey(id));
            
            CertResult cls = new CertResult();
            cls.uow = uow;
            cls.id = id;
            cls = (CertResult)DomainObj.addToIMap(uow, getOne(((CertResultSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static CertResult[] getAll(UOW uow)
        {
            CertResult[] objs = (CertResult[])DomainObj.addToIMap(uow, (new CertResultSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
		public static CertResult[] getAll_Store(UOW uow, string storeCode)
		{
			CertResult[] objs = (CertResult[])DomainObj.addToIMap(uow, (new CertResultSQL()).getAll_Store(uow, storeCode));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static CertResult[] getAll_Corp(UOW uow, int corpId)
		{
			CertResult[] objs = (CertResult[])DomainObj.addToIMap(uow, (new CertResultSQL()).getAll_Corp(uow, corpId));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
        static CertResult getOne(CertResult[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(CertResult src, CertResult tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.type = src.type;
            tar.storeCode = src.storeCode;
            tar.coworker = src.coworker;
            tar.name = src.name;
            tar.certDate = src.certDate;
            tar.status = src.status;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class CertResultSQL : SqlGateway
        {
            public CertResult[] getKey(CertResult rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCertResults_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                CertResult rec = (CertResult)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCertResults_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                CertResult rec = (CertResult)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCertResults_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                CertResult rec = (CertResult)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCertResults_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public CertResult[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spCertResults_Get_All";
                return convert(execReader(cmd));
            }
			public CertResult[] getAll_Store(UOW uow, string storeCode)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spCertResults_Get_StoreCode";
				cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 10).Value = storeCode;
				return convert(execReader(cmd));
			}
			public CertResult[] getAll_Corp(UOW uow, int corpId)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spCertResults_Get_Corp";
				cmd.Parameters.Add("@CorpId", SqlDbType.Int, 0).Value = corpId;
				return convert(execReader(cmd));
			}

            /*        Implementation        */
            void setParam(SqlCommand cmd, CertResult rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                if (rec.type == null)
                    cmd.Parameters.Add("@Type", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.Type.Length == 0)
                        cmd.Parameters.Add("@Type", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Type", SqlDbType.VarChar, 15).Value = rec.type;
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
 
                if (rec.coworker == null)
                    cmd.Parameters.Add("@Coworker", SqlDbType.VarChar, 20).Value = DBNull.Value;
                else
                {
                    if (rec.Coworker.Length == 0)
                        cmd.Parameters.Add("@Coworker", SqlDbType.VarChar, 20).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Coworker", SqlDbType.VarChar, 20).Value = rec.coworker;
                }
 
                if (rec.name == null)
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.Name.Length == 0)
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = rec.name;
                }
 
                if (rec.certDate == DateTime.MinValue)
                    cmd.Parameters.Add("@CertDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@CertDate", SqlDbType.DateTime, 0).Value = rec.certDate;
 
                if (rec.status == null)
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.Status.Length == 0)
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = rec.status;
                }
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                CertResult rec = new CertResult();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["Type"] != DBNull.Value)
                    rec.type = (string) rdr["Type"];
 
                if (rdr["StoreCode"] != DBNull.Value)
                    rec.storeCode = (string) rdr["StoreCode"];
 
                if (rdr["Coworker"] != DBNull.Value)
                    rec.coworker = (string) rdr["Coworker"];
 
                if (rdr["Name"] != DBNull.Value)
                    rec.name = (string) rdr["Name"];
 
                if (rdr["CertDate"] != DBNull.Value)
                    rec.certDate = (DateTime) rdr["CertDate"];
 
                if (rdr["Status"] != DBNull.Value)
                    rec.status = (string) rdr["Status"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            CertResult[] convert(DomainObj[] objs)
            {
                CertResult[] acls  = new CertResult[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
