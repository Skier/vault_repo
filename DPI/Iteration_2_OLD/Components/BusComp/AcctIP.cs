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
    public class AcctIP : DomainObj
    {
        /*        Data        */
        static string iName = "AcctIP";
        int id;
        int corpId;
        string privateIP;
        int autoLoginAcct;
        string autoLoginPw;
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
        public int CorpId
        {
            get { return corpId; }
            set
            {
                setState();
                corpId = value;
            }
        }
        public string PrivateIP
        {
            get { return privateIP; }
            set
            {
                setState();
                privateIP = value;
            }
        }
        public int AutoLoginAcct
        {
            get { return autoLoginAcct; }
            set
            {
                setState();
                autoLoginAcct = value;
            }
        }
        public string AutoLoginPw
        {
            get { return autoLoginPw; }
            set
            {
                setState();
                autoLoginPw = value;
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
        public AcctIP()
        {
            sql = new AcctIPSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public AcctIP(UOW uow) : this()
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
            return new AcctIPSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static AcctIP find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(AcctIP.getKey(id)))
                return (AcctIP)uow.Imap.find(AcctIP.getKey(id));
            
            AcctIP cls = new AcctIP();
            cls.uow = uow;
            cls.id = id;
            cls = (AcctIP)DomainObj.addToIMap(uow, getOne(((AcctIPSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
		public static AcctIP getUsingIP(UOW uow, int corp, string ip)
		{
			if (corp == 0)
				throw new ArgumentException("Corporation is required");

			if (ip == null)
				throw new ArgumentNullException("IP address is required");

			AcctIP[] objs 
				= (AcctIP[])DomainObj.addToIMap(uow, (new AcctIPSQL()).getUsingIP(uow, corp, TrimIP(ip)));
			
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;

			if (objs.Length == 0)
				return null;

			return objs[0];
		}
        public static AcctIP[] getAll(UOW uow)
        {
            AcctIP[] objs = (AcctIP[])DomainObj.addToIMap(uow, (new AcctIPSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
		static string TrimIP(string ip)
		{
			return ip.Substring(0, ip.LastIndexOfAny(".".ToCharArray())) + ".%";
		}
        /*		Implementation		*/
        static AcctIP getOne(AcctIP[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(AcctIP src, AcctIP tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.corpId = src.corpId;
            tar.privateIP = src.privateIP;
            tar.autoLoginAcct = src.autoLoginAcct;
            tar.autoLoginPw = src.autoLoginPw;
            tar.status = src.status;
        }
 
        /*		SQL		*/
        [Serializable]
        class AcctIPSQL : SqlGateway
        {
            public AcctIP[] getKey(AcctIP rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAcctIP_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                AcctIP rec = (AcctIP)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAcctIP_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                AcctIP rec = (AcctIP)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAcctIP_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                AcctIP rec = (AcctIP)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAcctIP_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
			public AcctIP[] getUsingIP(UOW uow, int corp, string ip)
			{
				SqlCommand cmd = makeCommand(uow);

				cmd.CommandText = "dbo.spAcctIP_Get_Using_IP";
				cmd.Parameters.Add("@CorpId", SqlDbType.Int, 0).Value = corp;
				cmd.Parameters.Add("@PrivateIP", SqlDbType.VarChar, 15).Value = ip;

				return convert(execReader(cmd));
			}
            public AcctIP[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spAcctIP_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, AcctIP rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                // Numeric, nullable foreign key treatment:
                if (rec.CorpId == 0)
                    cmd.Parameters.Add("@CorpId", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@CorpId", SqlDbType.Int, 0).Value = rec.corpId;
 
                cmd.Parameters.Add("@PrivateIP", SqlDbType.VarChar, 15).Value = rec.privateIP;
                
                // Numeric, nullable foreign key treatment:
                if (rec.AutoLoginAcct == 0)
                    cmd.Parameters.Add("@AutoLoginAcct", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@AutoLoginAcct", SqlDbType.Int, 0).Value = rec.autoLoginAcct;
 
                if (rec.autoLoginPw == null)
                    cmd.Parameters.Add("@AutoLoginPw", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.AutoLoginPw.Length == 0)
                        cmd.Parameters.Add("@AutoLoginPw", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AutoLoginPw", SqlDbType.VarChar, 50).Value = rec.autoLoginPw;
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
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                AcctIP rec = new AcctIP();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["CorpId"] != DBNull.Value)
                    rec.corpId = (int) rdr["CorpId"];
 
                if (rdr["PrivateIP"] != DBNull.Value)
                    rec.privateIP = (string) rdr["PrivateIP"];
 
                if (rdr["AutoLoginAcct"] != DBNull.Value)
                    rec.autoLoginAcct = (int) rdr["AutoLoginAcct"];
 
                if (rdr["AutoLoginPw"] != DBNull.Value)
                    rec.autoLoginPw = (string) rdr["AutoLoginPw"];
 
                if (rdr["Status"] != DBNull.Value)
                    rec.status = (string) rdr["Status"];
 
                 rec.rowState = RowState.Clean;
                return rec;
            }
            AcctIP[] convert(DomainObj[] objs)
            {
                AcctIP[] acls  = new AcctIP[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
