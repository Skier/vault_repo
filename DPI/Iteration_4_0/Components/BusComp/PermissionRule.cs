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
    public class PermissionRule : DomainObj
    {
        /*        Data        */
        static string iName = "PermissionRule";
        int id;
        string status;
        DateTime startingDate;
        DateTime endDate;
        string isDirectSeller;
        string isPymtStation;
        string isPriceLookup;
        string isLocalServiceSeller;
        string isInternetSeller;
        string isWirelessSeller;
        string jobTitle;
        string acctType;
        string permission;
		string isDebCardSeller;
		string isSatelliteSeller;
		string isDpiWirelessSeller;
		string isDpiEnergySeller;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
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
        public DateTime StartingDate
        {
            get { return startingDate; }
            set
            {
                setState();
                startingDate = value;
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
        public string IsDirectSeller
        {
            get { return isDirectSeller; }
            set
            {
                setState();
                isDirectSeller = value;
            }
        }
        public string IsPymtStation
        {
            get { return isPymtStation; }
            set
            {
                setState();
                isPymtStation = value;
            }
        }
        public string IsPriceLookup
        {
            get { return isPriceLookup; }
            set
            {
                setState();
                isPriceLookup = value;
            }
        }
        public string IsLocalServiceSeller
        {
            get { return isLocalServiceSeller; }
            set
            {
                setState();
                isLocalServiceSeller = value;
            }
        }
        public string IsInternetSeller
        {
            get { return isInternetSeller; }
            set
            {
                setState();
                isInternetSeller = value;
            }
        }
        public string IsWirelessSeller
        {
            get { return isWirelessSeller; }
            set
            {
                setState();
                isWirelessSeller = value;
            }
        }
        public string JobTitle
        {
            get { return jobTitle; }
            set
            {
                setState();
                jobTitle = value;
            }
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
        public string Permisn
        {
            get { return permission; }
            set
            {
                setState();
                permission = value;
            }
        }
		public string IsDebCardSeller
		{
			get { return isDebCardSeller; }
			set
			{
				setState();
				isDebCardSeller = value;
			}
		}
		public string IsSatelliteSeller
		{
			get { return isSatelliteSeller; }
			set
			{
				setState();
				isSatelliteSeller = value;
			}
		}
		public string IsDpiWirelessSeller
		{
			get { return isDpiWirelessSeller; }
			set
			{
				setState();
				isDpiWirelessSeller = value;
			}
		}
		public string IsDpiEnergySeller
		{
			get { return isDpiEnergySeller; }
			set
			{
				setState();
				isDpiEnergySeller = value;
			}
		}
		
        /*        Constructors			*/
        public PermissionRule()
        {
            sql = new PermissionRuleSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public PermissionRule(UOW uow) : this()
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
            return new PermissionRuleSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static PermissionRule find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(PermissionRule.getKey(id)))
                return (PermissionRule)uow.Imap.find(PermissionRule.getKey(id));
            
            PermissionRule cls = new PermissionRule();
            cls.uow = uow;
            cls.id = id;
            cls = (PermissionRule)DomainObj.addToIMap(uow, getOne(((PermissionRuleSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static PermissionRule[] getAll(UOW uow)
        {
            PermissionRule[] objs = (PermissionRule[])DomainObj.addToIMap(uow, (new PermissionRuleSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
		internal static Permission[] GetPermissions(UOW uow, PermissionRule states)
		{
			Hashtable hashtable = new Hashtable(); 
            
			PermissionRule[] rules = PermissionRule.getAll(uow);
			for(int i = 0; i < rules.Length; i++)
				if (Match(states, rules[i]))
					if (!hashtable.ContainsKey(rules[i].permission))
						hashtable.Add(rules[i].permission, rules[i].permission);

			Permission[] perms = new Permission[hashtable.Count];
			IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
			int j = 0;

			while(enumerator.MoveNext())
				if (enumerator.Value != null)
					perms[j++] = Permission.find(uow, (string)enumerator.Value);

			return perms;
		}

		/*		Implementation		*/
		static bool Match(PermissionRule user, PermissionRule rule)
		{
			if ((rule.IsDirectSeller != null) && (rule.IsDirectSeller != user.IsDirectSeller))
				return false;

			if ((rule.IsInternetSeller  != null) && (rule.IsInternetSeller != user.IsInternetSeller))
				return false;

			if ((rule.IsLocalServiceSeller != null) && (rule.IsLocalServiceSeller != user.IsLocalServiceSeller))
				return false;

			if ((rule.IsPriceLookup != null) && (rule.IsPriceLookup != user.IsPriceLookup))
				return false;

			if ((rule.IsPymtStation != null) && (rule.IsPymtStation != user.IsPymtStation))
				return false;

			if ((rule.IsWirelessSeller != null) && (rule.IsWirelessSeller != user.IsWirelessSeller))
				return false;

			if ((rule.JobTitle != null) && (rule.JobTitle != user.JobTitle))
				return false;

			if ((rule.AcctType != null) && (rule.AcctType != user.AcctType))
				return false;
			
			if ((rule.IsDebCardSeller != null) && (rule.IsDebCardSeller != user.IsDebCardSeller))
				return false;

			if ((rule.IsSatelliteSeller != null) && (rule.IsSatelliteSeller != user.IsSatelliteSeller))
				return false;

			if ((rule.IsDpiWirelessSeller != null) && (rule.IsDpiWirelessSeller != user.IsDpiWirelessSeller))
				return false;

			if ((rule.IsDpiEnergySeller != null) && (rule.IsDpiEnergySeller != user.IsDpiEnergySeller))
				return false;

			return true;

		}
        static PermissionRule getOne(PermissionRule[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(PermissionRule src, PermissionRule tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.status = src.status;
            tar.startingDate = src.startingDate;
            tar.endDate = src.endDate;
            tar.isDirectSeller = src.isDirectSeller;
            tar.isPymtStation = src.isPymtStation;
            tar.isPriceLookup = src.isPriceLookup;
            tar.isLocalServiceSeller = src.isLocalServiceSeller;
            tar.isInternetSeller = src.isInternetSeller;
            tar.isWirelessSeller = src.isWirelessSeller;
            tar.jobTitle = src.jobTitle;
            tar.acctType = src.acctType;
            tar.permission = src.permission;
            tar.rowState = src.rowState;
			tar.isDebCardSeller = src.isDebCardSeller;
			tar.isSatelliteSeller = src.isSatelliteSeller;
			tar.isDpiWirelessSeller = src.isDpiWirelessSeller;
			tar.isDpiEnergySeller = src.isDpiEnergySeller;
        }
 
        /*		SQL		*/
        [Serializable]
        class PermissionRuleSQL : SqlGateway
        {
            public PermissionRule[] getKey(PermissionRule rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPermissionRule_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                PermissionRule rec = (PermissionRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPermissionRule_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                PermissionRule rec = (PermissionRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPermissionRule_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                PermissionRule rec = (PermissionRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPermissionRule_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public PermissionRule[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spPermissionRule_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, PermissionRule rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = rec.status;
 
                cmd.Parameters.Add("@StartingDate", SqlDbType.DateTime, 0).Value = rec.startingDate;
 
                if (rec.endDate == DateTime.MinValue)
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = rec.endDate;
 
                cmd.Parameters.Add("@IsDirectSeller", SqlDbType.Char, 1).Value = rec.isDirectSeller;
 
                cmd.Parameters.Add("@IsPymtStation", SqlDbType.Char, 1).Value = rec.isPymtStation;
 
                cmd.Parameters.Add("@IsPriceLookup", SqlDbType.Char, 1).Value = rec.isPriceLookup;
 
                cmd.Parameters.Add("@IsLocalServiceSeller", SqlDbType.Char, 1).Value = rec.isLocalServiceSeller;
 
                cmd.Parameters.Add("@IsInternetSeller", SqlDbType.Char, 1).Value = rec.isInternetSeller;
 
                cmd.Parameters.Add("@IsWirelessSeller", SqlDbType.Char, 1).Value = rec.isWirelessSeller;
 
                if (rec.jobTitle == null)
                    cmd.Parameters.Add("@JobTitle", SqlDbType.VarChar, 30).Value = DBNull.Value;
                else
                {
                    if (rec.JobTitle.Length == 0)
                        cmd.Parameters.Add("@JobTitle", SqlDbType.VarChar, 30).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@JobTitle", SqlDbType.VarChar, 30).Value = rec.jobTitle;
                }
 
                if (rec.acctType == null)
                    cmd.Parameters.Add("@AcctType", SqlDbType.VarChar, 20).Value = DBNull.Value;
                else
                {
                    if (rec.AcctType.Length == 0)
                        cmd.Parameters.Add("@AcctType", SqlDbType.VarChar, 20).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AcctType", SqlDbType.VarChar, 20).Value = rec.acctType;
                }
 
                cmd.Parameters.Add("@Permission", SqlDbType.VarChar, 50).Value = rec.permission;
				cmd.Parameters.Add("@IsDebCardSeller", SqlDbType.Char, 1).Value = rec.isDebCardSeller;
				cmd.Parameters.Add("@IsSatelliteSeller", SqlDbType.Char, 1).Value = rec.isSatelliteSeller;
				cmd.Parameters.Add("@IsDpiWirelessSeller", SqlDbType.Char, 1).Value = rec.isDpiWirelessSeller;
                //Removed to allow using old DB			    
				//cmd.Parameters.Add("@IsDpiEnergySeller", SqlDbType.Char, 1).Value = rec.isDpiEnergySeller;
            }
			protected override DomainObj reader(SqlDataReader rdr)
			{
				PermissionRule rec = new PermissionRule();
                
				if (rdr["Id"] != DBNull.Value)
					rec.id = (int) rdr["Id"];
 
				if (rdr["Status"] != DBNull.Value)
					rec.status = (string) rdr["Status"];
 
				if (rdr["StartingDate"] != DBNull.Value)
					rec.startingDate = (DateTime) rdr["StartingDate"];
 
				if (rdr["EndDate"] != DBNull.Value)
					rec.endDate = (DateTime) rdr["EndDate"];
				
				/*  ---------------- states -------------- */
				if (rdr["IsDirectSeller"] != DBNull.Value)
					if (((string)rdr["IsDirectSeller"]).Trim().Length > 0)
						rec.isDirectSeller = ((string)rdr["IsDirectSeller"]).Trim();
 
				if (rdr["IsPymtStation"] != DBNull.Value)
					if (((string)rdr["IsPymtStation"]).Trim().Length > 0)
						rec.isPymtStation = ((string) rdr["IsPymtStation"]).Trim();
 
				if (rdr["IsPriceLookup"] != DBNull.Value)
					if (((string)rdr["IsPriceLookup"]).Trim().Length > 0)
						rec.isPriceLookup = ((string) rdr["IsPriceLookup"]).Trim();
 
				if (rdr["IsLocalServiceSeller"] != DBNull.Value)
					if (((string)rdr["IsLocalServiceSeller"]).Trim().Length > 0)
						rec.isLocalServiceSeller = ((string) rdr["IsLocalServiceSeller"]).Trim();
 
				if (rdr["IsInternetSeller"] != DBNull.Value)
					if (((string)rdr["IsInternetSeller"]).Trim().Length > 0)
						rec.isInternetSeller = ((string) rdr["IsInternetSeller"]).Trim();
 
				if (rdr["IsWirelessSeller"] != DBNull.Value)
					if (((string)rdr["IsWirelessSeller"]).Trim().Length > 0)
						rec.isWirelessSeller = ((string) rdr["IsWirelessSeller"]).Trim();
 
				if (rdr["IsDebCardSeller"] != DBNull.Value)
					if (((string)rdr["IsDebCardSeller"]).Trim().Length > 0)
						rec.isDebCardSeller = ((string) rdr["IsDebCardSeller"]).Trim();

				if (rdr["IsSatelliteSeller"] != DBNull.Value)
					if (((string)rdr["IsSatelliteSeller"]).Trim().Length > 0)
						rec.isSatelliteSeller = ((string) rdr["IsSatelliteSeller"]).Trim();

				if (rdr["IsDpiWirelessSeller"] != DBNull.Value)
					if (((string)rdr["IsDpiWirelessSeller"]).Trim().Length > 0)
						rec.isDpiWirelessSeller = ((string) rdr["IsDpiWirelessSeller"]).Trim();

			    //Removed to allow using old DB			    
//				if (rdr["IsDpiEnergySeller"] != DBNull.Value)
//					if (((string)rdr["IsDpiEnergySeller"]).Trim().Length > 0)
//						rec.isDpiEnergySeller = ((string) rdr["IsDpiEnergySeller"]).Trim();

				if (rdr["JobTitle"] != DBNull.Value)
					if (((string)rdr["JobTitle"]).Trim().Length > 0)
						rec.jobTitle = (string) rdr["JobTitle"];
 
				if (rdr["AcctType"] != DBNull.Value)
					if (((string)rdr["AcctType"]).Trim().Length > 0)
						rec.acctType = (string) rdr["AcctType"];
 
				if (rdr["Permission"] != DBNull.Value)
					rec.permission = (string) rdr["Permission"];
 
				rec.rowState = RowState.Clean;
				return rec;
			}
            PermissionRule[] convert(DomainObj[] objs)
            {
                PermissionRule[] acls  = new PermissionRule[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
