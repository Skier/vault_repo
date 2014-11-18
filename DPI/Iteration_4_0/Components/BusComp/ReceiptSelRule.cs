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
    public class ReceiptSelRule : DomainObj
    {
        /*        Data        */
        static string iName = "ReceiptSelRule";
        int id;
        string prodGroup;
        int product;
        int wLProd;
        int exclusiveCorp;
        string exclusiveStore;
        string workflow;
        int reportId;
        DateTime effStartDate;
        DateTime effEndDate;
        string status;
		bool isCompleted;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
        }
        public string ProdGroup
        {
            get { return prodGroup; }
            set
            {
                setState();
                prodGroup = value;
            }
        }
        public int Product
        {
            get { return product; }
            set
            {
                setState();
                product = value;
            }
        }
        public int WLProd
        {
            get { return wLProd; }
            set
            {
                setState();
                wLProd = value;
            }
        }
        public int ExclusiveCorp
        {
            get { return exclusiveCorp; }
            set
            {
                setState();
                exclusiveCorp = value;
            }
        }
        public string ExclusiveStore
        {
            get { return exclusiveStore; }
            set
            {
                setState();
                exclusiveStore = value;
            }
        }
        public string Workflow
        {
            get { return workflow; }
            set
            {
                setState();
                workflow = value;
            }
        }
        public int ReportId
        {
            get { return reportId; }
            set
            {
                setState();
                reportId = value;
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
        public DateTime EffEndDate
        {
            get { return effEndDate; }
            set
            {
                setState();
                effEndDate = value;
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
		public bool IsCompleted
		{
			get { return isCompleted; }
			set
			{
				setState();
				isCompleted = value;
			}
		}

        
        /*        Constructors			*/
        public ReceiptSelRule()
        {
            sql = new ReceiptSelRuleSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public ReceiptSelRule(UOW uow) : this()
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
            return new ReceiptSelRuleSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static ReceiptSelRule find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(ReceiptSelRule.getKey(id)))
                return (ReceiptSelRule)uow.Imap.find(ReceiptSelRule.getKey(id));
            
            ReceiptSelRule cls = new ReceiptSelRule();
            cls.uow = uow;
            cls.id = id;
            cls = (ReceiptSelRule)DomainObj.addToIMap(uow, getOne(((ReceiptSelRuleSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
		public static int GetReceiptId(UOW uow, string provider, string prodGroup, int prod, int wlProd, 
									   int corp, string store, string workflow, bool isCompleted)
		{
	        return getOne(new ReceiptSelRuleSQL()
					.getReceiptId(uow, provider, prodGroup, prod, wlProd, corp, store, workflow, isCompleted)).ReportId;
		}
        public static ReceiptSelRule[] getAll(UOW uow)
        {
            ReceiptSelRule[] objs = (ReceiptSelRule[])DomainObj.addToIMap(uow, (new ReceiptSelRuleSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
        static ReceiptSelRule getOne(ReceiptSelRule[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(ReceiptSelRule src, ReceiptSelRule tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.prodGroup = src.prodGroup;
            tar.product = src.product;
            tar.wLProd = src.wLProd;

			tar.exclusiveCorp = src.exclusiveCorp;
            tar.exclusiveStore = src.exclusiveStore;
            tar.workflow = src.workflow;
            tar.reportId = src.reportId;
            
			tar.effStartDate = src.effStartDate;
            tar.effEndDate = src.effEndDate;
            tar.status = src.status;
			tar.isCompleted = src.isCompleted;  
			
			tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class ReceiptSelRuleSQL : SqlGateway
        {
            public ReceiptSelRule[] getKey(ReceiptSelRule rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spReceiptSelRuleGetId";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                ReceiptSelRule rec = (ReceiptSelRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spReceiptSelRuleIns";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                ReceiptSelRule rec = (ReceiptSelRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spReceiptSelRuleDelId";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                ReceiptSelRule rec = (ReceiptSelRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spReceiptSelRuleUpd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
			public ReceiptSelRule[] getAll(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spReceiptSelRuleGetAll";
				return convert(execReader(cmd));
			}
			public ReceiptSelRule[] getReceiptId(UOW uow, string provider, string prodGroup, int prod, int wlProd, int corp, 
				string store, string workflow, bool isCompleted)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spReceiptSelRuleGetReceiptId";
				setPatternParams(cmd, provider, prod, prodGroup, wlProd, store, corp, workflow, isCompleted);
				return convert(execReader(cmd));
			}

            /*        Implementation        */
			void setPatternParams(SqlCommand cmd, string provider, int prod, string prodGroup,  int wlProd, string store, int corp, 
				string workflow, bool isCompleted)
			{
				if (provider == null)
					cmd.Parameters.Add("@Provider", SqlDbType.VarChar, 25).Value = DBNull.Value;
				else
				{
					if (provider.Length == 0)
						cmd.Parameters.Add("@Provider", SqlDbType.VarChar, 25).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Provider", SqlDbType.VarChar, 25).Value = provider;
				}

				if (prodGroup == null)
					cmd.Parameters.Add("@ProdGroup", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (prodGroup.Length == 0)
						cmd.Parameters.Add("@ProdGroup", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@ProdGroup", SqlDbType.VarChar, 50).Value = prodGroup;
				}
                
				if (prod == 0)
					cmd.Parameters.Add("@Product", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Product", SqlDbType.Int, 0).Value = prod;
                
				// Numeric, nullable foreign key treatment:
				if (wlProd == 0)
					cmd.Parameters.Add("@WLProd", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@WLProd", SqlDbType.Int, 0).Value = wlProd;
                
				// Numeric, nullable foreign key treatment:
				if (corp == 0)
					cmd.Parameters.Add("@ExclusiveCorp", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@ExclusiveCorp", SqlDbType.Int, 0).Value = corp;
 
				if (store == null)
					cmd.Parameters.Add("@ExclusiveStore", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (store.Length == 0)
						cmd.Parameters.Add("@ExclusiveStore", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@ExclusiveStore", SqlDbType.VarChar, 10).Value = store;
				}
 
				if (workflow == null)
					cmd.Parameters.Add("@Workflow", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (workflow.Length == 0)
						cmd.Parameters.Add("@Workflow", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Workflow", SqlDbType.VarChar, 50).Value = workflow;
				}

				cmd.Parameters.Add("@Date", SqlDbType.DateTime, 0).Value = DateTime.Now;
				cmd.Parameters.Add("@IsCompleted", SqlDbType.Bit, 1).Value = isCompleted;
			}
            void setParam(SqlCommand cmd, ReceiptSelRule rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                if (rec.prodGroup == null)
                    cmd.Parameters.Add("@ProdGroup", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.ProdGroup.Length == 0)
                        cmd.Parameters.Add("@ProdGroup", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ProdGroup", SqlDbType.VarChar, 50).Value = rec.prodGroup;
                }
                
                // Numeric, nullable foreign key treatment:
                if (rec.Product == 0)
                    cmd.Parameters.Add("@Product", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Product", SqlDbType.Int, 0).Value = rec.product;
                
                // Numeric, nullable foreign key treatment:
                if (rec.WLProd == 0)
                    cmd.Parameters.Add("@WLProd", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@WLProd", SqlDbType.Int, 0).Value = rec.wLProd;
                
                // Numeric, nullable foreign key treatment:
                if (rec.ExclusiveCorp == 0)
                    cmd.Parameters.Add("@ExclusiveCorp", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@ExclusiveCorp", SqlDbType.Int, 0).Value = rec.exclusiveCorp;
 
                if (rec.exclusiveStore == null)
                    cmd.Parameters.Add("@ExclusiveStore", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.ExclusiveStore.Length == 0)
                        cmd.Parameters.Add("@ExclusiveStore", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ExclusiveStore", SqlDbType.VarChar, 10).Value = rec.exclusiveStore;
                }
 
                if (rec.workflow == null)
                    cmd.Parameters.Add("@Workflow", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.Workflow.Length == 0)
                        cmd.Parameters.Add("@Workflow", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Workflow", SqlDbType.VarChar, 50).Value = rec.workflow;
                }
                
                // Numeric, nullable foreign key treatment:
                if (rec.ReportId == 0)
                    cmd.Parameters.Add("@ReportId", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@ReportId", SqlDbType.Int, 0).Value = rec.reportId;
 
                if (rec.effStartDate == DateTime.MinValue)
                    cmd.Parameters.Add("@EffStartDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EffStartDate", SqlDbType.DateTime, 0).Value = rec.effStartDate;
 
                if (rec.effEndDate == DateTime.MinValue)
                    cmd.Parameters.Add("@EffEndDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EffEndDate", SqlDbType.DateTime, 0).Value = rec.effEndDate;
 
                if (rec.status == null)
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.Status.Length == 0)
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = rec.status;
                }

				cmd.Parameters.Add("@IsCompleted", SqlDbType.Bit, 1).Value = rec.isCompleted;

            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                ReceiptSelRule rec = new ReceiptSelRule();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["ProdGroup"] != DBNull.Value)
                    rec.prodGroup = (string) rdr["ProdGroup"];
 
                if (rdr["Product"] != DBNull.Value)
                    rec.product = (int) rdr["Product"];
 
                if (rdr["WLProd"] != DBNull.Value)
                    rec.wLProd = (int) rdr["WLProd"];
 
                if (rdr["ExclusiveCorp"] != DBNull.Value)
                    rec.exclusiveCorp = (int) rdr["ExclusiveCorp"];
 
                if (rdr["ExclusiveStore"] != DBNull.Value)
                    rec.exclusiveStore = (string) rdr["ExclusiveStore"];
 
                if (rdr["Workflow"] != DBNull.Value)
                    rec.workflow = (string) rdr["Workflow"];
 
                if (rdr["ReportId"] != DBNull.Value)
                    rec.reportId = (int) rdr["ReportId"];
 
                if (rdr["EffStartDate"] != DBNull.Value)
                    rec.effStartDate = (DateTime) rdr["EffStartDate"];
 
                if (rdr["EffEndDate"] != DBNull.Value)
                    rec.effEndDate = (DateTime) rdr["EffEndDate"];
 
				if (rdr["Status"] != DBNull.Value)
					rec.status = (string) rdr["Status"];

				if (rdr["IsCompleted"] != DBNull.Value)
					rec.isCompleted = (bool) rdr["IsCompleted"];

				//isCompleted
                rec.rowState = RowState.Clean;
                return rec;
            }
            ReceiptSelRule[] convert(DomainObj[] objs)
            {
                ReceiptSelRule[] acls  = new ReceiptSelRule[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
