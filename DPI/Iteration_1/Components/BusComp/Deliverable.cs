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
    public class Deliverable : DomainObj
    {

	#region Data
        static string iName = "Deliverable";
        
		int dlvId;
        int predDlvId;
        string dlvType;
        string aRType;

        int prodId;
        int locId;
        int supplier;
     
		int dmdItemId;
		DmdItem dmdItem;
        
		int stmtId;
		Statement stmt;   
		int servPeriod;

		DateTime effDate;
        DateTime effEndDate;
        DateTime datePosted;
        
		int price;
		string priceRule;
        
		decimal amt;
        int qt;
        string uOM;
        decimal taxSurAmt;
        decimal totalAmt;
        decimal availForXferBal;
        string adjReasonCode;
        string cSR;
			
	#endregion
        
	#region Properties
        public override IDomKey IKey 
        {
             get { return new Key(iName, dlvId.ToString()); }
        }
        public int DlvId
        {
            get { return dlvId; }
        }
        public int PredDlvId
        {
            get { return predDlvId; }
            set
            {
                setState();
                predDlvId = value;
            }
        }
        public string DlvType
        {
            get { return dlvType; }
            set
            {
                setState();
                dlvType = value;
            }
        }
        public string ARType
        {
            get { return aRType; }
            set
            {
                setState();
                aRType = value;
            }
        }
        public Statement Stmt
        {
            get 
			{
				if (stmt == null)
					if (stmtId > 0)
						Statement.find(uow, stmtId);
 				return stmt; 
			}
            set
            {
                stmt = value;
				if (stmt != null)
					stmtId = stmt.StmtId;  

				setState();
            }
        }
		public int ServPeriod 
		{
			get { return servPeriod;  }
			set { servPeriod = value; }
		}
		public DmdItem DmdItm 
		{
			get 
			{
				if (dmdItem == null)
					if (dmdItemId > 0)
						DmdItem.find(uow, dmdItemId);
				return dmdItem;
			}
			set 
			{
				dmdItem = value;
				if (dmdItem.Id > 0)
					dmdItemId = dmdItem.Id;
				setState();
			}
		}
        public int ProdId
        {
            get { return prodId; }
            set
            {
                setState();
                prodId = value;
            }
        }
        public int LocId
        {
            get { return locId; }
            set
            {
                setState();
                locId = value;
            }
        }
        public int Supplier
        {
            get { return supplier; }
            set
            {
                setState();
                supplier = value;
            }
        }
        public DateTime EffDate
        {
            get { return effDate; }
            set
            {
                setState();
                effDate = value;
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
        public DateTime DatePosted
        {
            get { return datePosted; }
            set
            {
                setState();
                datePosted = value;
            }
        }
        public string PriceRule
        {
            get { return priceRule; }
            set
            {
                setState();
                priceRule = value;
            }
        }
		public int Price
		{
			get { return price; }
			set
			{
				setState();
				price = value;
			}
		}
        public decimal Amt
        {
            get { return amt; }
            set
            {
                setState();
                amt = Decimal.Round(value, 2);
            }
        }
        public int Qt
        {
            get { return qt; }
            set
            {
                setState();
                qt = value;
            }
        }
        public string UOM
        {
            get { return uOM; }
            set
            {
                setState();
                uOM = value;
            }
        }
        public decimal TaxSurAmt
        {
            get { return taxSurAmt; }
            set
            {
                setState();
                taxSurAmt = Decimal.Round(value, 2);
            }
        }
        public decimal TotalAmt
        {
            get { return totalAmt; }
            set
            {
                setState();
                totalAmt = Decimal.Round(value, 2);
            }
        }
        public decimal AvailForXferBal
        {
            get { return availForXferBal; }
            set
            {
                setState();
                availForXferBal = Decimal.Round(value, 2);
            }
        }
        public string AdjReasonCode
        {
            get { return adjReasonCode; }
            set
            {
                setState();
                adjReasonCode = value;
            }
        }
        public string CSR
        {
            get { return cSR; }
            set
            {
                setState();
                cSR = value;
            }
        }
			
	#endregion
        
	#region Constructors
        public Deliverable()
        {
            sql = new DeliverableSQL();
            dlvId = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
			priority = 14000;
        }
        public Deliverable(UOW uow) : this()
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
		public void Setup(UOW uow, DmdItem dmdItem)
		{
			this.dmdItem   = dmdItem;
			this.Amt       = dmdItem.PriceAmt;
			this.prodId    = dmdItem.Prod;
	//		this.PriceRule = dmdItem.PriceRule; open issue: Does dlv need Price, PriceRule, or both?
			this.UOM       = dmdItem.UOM;

			decimal tax = decimal.Zero;
			
			for (int i = 0; i < dmdItem.Taxes.Length; i++)
			{
				if ( dmdItem.Taxes[i].TaxAmount == decimal.Zero)
					continue;

				DlvTax dt = new DlvTax(uow);
				dt.Dlv = this;
				dt.TaxAmount = dmdItem.Taxes[i].TaxAmount;
				dt.TaxId     = dmdItem.Taxes[i].TaxId;
				tax+= dt.TaxAmount; 
			}	

			this.taxSurAmt = decimal.Round(tax, 2);
			this.totalAmt  = decimal.Round(this.Amt + this.taxSurAmt, 2);
		}
        protected override SqlGateway loadSql()
        {
            return new DeliverableSQL();
        }
        public override void checkExists()
        {
            if ((DlvId < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
					
	#endregion

	#region Static methods
        public static Deliverable find(UOW uow, int dlvId)
        {
            if (uow.Imap.keyExists(Deliverable.getKey(dlvId)))
                return (Deliverable)uow.Imap.find(Deliverable.getKey(dlvId));
            
            Deliverable cls = new Deliverable();
            cls.uow = uow;
            cls.dlvId = dlvId;
            cls = (Deliverable)DomainObj.addToIMap(uow, getOne(((DeliverableSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static Deliverable[] getAll(UOW uow)
        {
            Deliverable[] objs = (Deliverable[])DomainObj.addToIMap(uow, (new DeliverableSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int dlvId)
        {
            return new Key(iName, dlvId.ToString());
        }
	#endregion
		
	#region Implementation
		public override	void RefreshForeignKeys()
		{
			if (dmdItem != null)
				this.dmdItemId = dmdItem.Id;
	
			if (this.stmt != null)
				this.stmtId = stmt.StmtId;
		}
		static Deliverable getOne(Deliverable[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Deliverable src, Deliverable tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.dlvId = src.dlvId;
            tar.servPeriod = src.servPeriod;
            tar.predDlvId = src.predDlvId;
            tar.dlvType = src.dlvType;
            tar.aRType = src.aRType;
            tar.stmtId = src.stmtId;
            tar.prodId = src.prodId;
            tar.locId = src.locId;
            tar.supplier = src.supplier;
            tar.dmdItemId = src.dmdItemId;
            tar.effDate = src.effDate;
            tar.effEndDate = src.effEndDate;
            tar.datePosted = src.datePosted;
            tar.priceRule = src.priceRule;
			tar.price = src.price;
            tar.amt = src.amt;
            tar.qt = src.qt;
            tar.uOM = src.uOM;
            tar.taxSurAmt = src.taxSurAmt;
            tar.totalAmt = src.totalAmt;
            tar.availForXferBal = src.availForXferBal;
            tar.adjReasonCode = src.adjReasonCode;
            tar.cSR = src.cSR;
            tar.rowState = src.rowState;
        }
			
	#endregion
 
	#region SQL		*/
        [Serializable]
        class DeliverableSQL : SqlGateway
        {
            public Deliverable[] getKey(Deliverable rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spDeliverable_Get_Id";
                cmd.Parameters.Add("@DlvId", SqlDbType.Int, 0).Value = rec.dlvId;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                Deliverable rec = (Deliverable)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spDeliverable_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@DlvId"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.dlvId = (int)cmd.Parameters["@DlvId"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Deliverable rec = (Deliverable)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spDeliverable_Del_Id";
                cmd.Parameters.Add("@DlvId", SqlDbType.Int, 0).Value = rec.dlvId;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Deliverable rec = (Deliverable)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spDeliverable_Upd";
                rec.ver++;
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public Deliverable[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spDeliverable_Get_All";
                return convert(execReader(cmd));
            }
			
	#endregion

	#region Implementation        */
            void setParam(SqlCommand cmd, Deliverable rec)
            {
                cmd.Parameters.Add("@DlvId", SqlDbType.Int, 0).Value = rec.dlvId;
				
				if (rec.servPeriod == 0)
					cmd.Parameters.Add("@ServPeriod", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@ServPeriod", SqlDbType.Int, 0).Value = rec.servPeriod;
                
				cmd.Parameters.Add("@PredDlvId", SqlDbType.Int, 0).Value = rec.predDlvId;
 
                if (rec.dlvType == null)
                    cmd.Parameters.Add("@DlvType", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.DlvType.Length == 0)
                        cmd.Parameters.Add("@DlvType", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@DlvType", SqlDbType.VarChar, 15).Value = rec.dlvType;
                }
 
                if (rec.aRType == null)
                    cmd.Parameters.Add("@ARType", SqlDbType.VarChar, 2).Value = DBNull.Value;
                else
                {
                    if (rec.ARType.Length == 0)
                        cmd.Parameters.Add("@ARType", SqlDbType.VarChar, 2).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ARType", SqlDbType.VarChar, 2).Value = rec.aRType;
                }
                
                // Numeric, nullable foreign key treatment:
                if (rec.stmt == null)
                    cmd.Parameters.Add("@StmtId", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@StmtId", SqlDbType.Int, 0).Value = rec.stmt.StmtId;

                cmd.Parameters.Add("@ProdId", SqlDbType.Int, 0).Value = rec.prodId;
                cmd.Parameters.Add("@LocId", SqlDbType.Int, 0).Value = rec.locId;
                cmd.Parameters.Add("@Supplier", SqlDbType.Int, 0).Value = rec.supplier;
                
                // Numeric, nullable foreign key treatment:
                if (rec.dmdItem == null)
                    cmd.Parameters.Add("@DmdItem", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@DmdItem", SqlDbType.Int, 0).Value = rec.dmdItem.Id;
 
                if (rec.effDate == DateTime.MinValue)
                    cmd.Parameters.Add("@EffDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EffDate", SqlDbType.DateTime, 0).Value = rec.effDate;
 
                if (rec.effEndDate == DateTime.MinValue)
                    cmd.Parameters.Add("@EffEndDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EffEndDate", SqlDbType.DateTime, 0).Value = rec.effEndDate;
 
                if (rec.datePosted == DateTime.MinValue)
                    cmd.Parameters.Add("@DatePosted", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@DatePosted", SqlDbType.DateTime, 0).Value = rec.datePosted;
                
				cmd.Parameters.Add("@Price", SqlDbType.Int, 0).Value = rec.price;
 
				if (rec.priceRule == null)
					cmd.Parameters.Add("@PriceRule", SqlDbType.VarChar, 15).Value = DBNull.Value;
				else
				{
					if (rec.priceRule.Length == 0)
						cmd.Parameters.Add("@PriceRule", SqlDbType.VarChar, 15).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@PriceRule", SqlDbType.VarChar, 15).Value = rec.priceRule;
				}                  
				
				cmd.Parameters.Add("@Amt", SqlDbType.Decimal, 0).Value = rec.amt;
                cmd.Parameters.Add("@Qt", SqlDbType.Int, 0).Value = rec.qt;
 
                if (rec.uOM == null)
                    cmd.Parameters.Add("@UOM", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.UOM.Length == 0)
                        cmd.Parameters.Add("@UOM", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@UOM", SqlDbType.VarChar, 50).Value = rec.uOM;
                }
                cmd.Parameters.Add("@TaxSurAmt", SqlDbType.Decimal, 0).Value = rec.taxSurAmt;
                cmd.Parameters.Add("@TotalAmt", SqlDbType.Decimal, 0).Value = rec.totalAmt;
                cmd.Parameters.Add("@AvailForXferBal", SqlDbType.Decimal, 0).Value = rec.availForXferBal;
 
                if (rec.adjReasonCode == null)
                    cmd.Parameters.Add("@AdjReasonCode", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.AdjReasonCode.Length == 0)
                        cmd.Parameters.Add("@AdjReasonCode", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AdjReasonCode", SqlDbType.VarChar, 15).Value = rec.adjReasonCode;
                }
 
                if (rec.cSR == null)
                    cmd.Parameters.Add("@CSR", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.CSR.Length == 0)
                        cmd.Parameters.Add("@CSR", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@CSR", SqlDbType.VarChar, 15).Value = rec.cSR;
                }
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                Deliverable rec = new Deliverable();
                
                if (rdr["DlvId"] != DBNull.Value)
                    rec.dlvId = (int) rdr["DlvId"];
 
                if (rdr["ServPeriod"] != DBNull.Value)
                    rec.servPeriod = (int) rdr["ServPeriod"];
 
                if (rdr["PredDlvId"] != DBNull.Value)
                    rec.predDlvId = (int) rdr["PredDlvId"];
 
                if (rdr["DlvType"] != DBNull.Value)
                    rec.dlvType = (string) rdr["DlvType"];
 
                if (rdr["ARType"] != DBNull.Value)
                    rec.aRType = (string) rdr["ARType"];
 
                if (rdr["StmtId"] != DBNull.Value)
                    rec.stmtId = (int) rdr["StmtId"];
 
                if (rdr["ProdId"] != DBNull.Value)
                    rec.prodId = (int) rdr["ProdId"];
 
                if (rdr["LocId"] != DBNull.Value)
                    rec.locId = (int) rdr["LocId"];
 
                if (rdr["Supplier"] != DBNull.Value)
                    rec.supplier = (int) rdr["Supplier"];
 
                if (rdr["DmdItem"] != DBNull.Value)
                    rec.dmdItemId = (int) rdr["DmdItem"];
 
                if (rdr["EffDate"] != DBNull.Value)
                    rec.effDate = (DateTime) rdr["EffDate"];
 
                if (rdr["EffEndDate"] != DBNull.Value)
                    rec.effEndDate = (DateTime) rdr["EffEndDate"];
 
                if (rdr["DatePosted"] != DBNull.Value)
                    rec.datePosted = (DateTime) rdr["DatePosted"];
 
				if (rdr["PriceRule"] != DBNull.Value)
					rec.priceRule = (string) rdr["PriceRule"];
				
				if (rdr["Price"] != DBNull.Value)
					rec.price = (int) rdr["Price"];
 
                if (rdr["Amt"] != DBNull.Value)
                    rec.amt = Decimal.Round((decimal)rdr["Amt"], 2);
 
                if (rdr["Qt"] != DBNull.Value)
                    rec.qt = (int) rdr["Qt"];
 
                if (rdr["UOM"] != DBNull.Value)
                    rec.uOM = (string) rdr["UOM"];
 
                if (rdr["TaxSurAmt"] != DBNull.Value)
                    rec.taxSurAmt = Decimal.Round((decimal)rdr["TaxSurAmt"], 2);
 
                if (rdr["TotalAmt"] != DBNull.Value)
                    rec.totalAmt = Decimal.Round((decimal)rdr["TotalAmt"], 2);
 
                if (rdr["AvailForXferBal"] != DBNull.Value)
                    rec.availForXferBal = Decimal.Round((decimal)rdr["AvailForXferBal"], 2);
 
                if (rdr["AdjReasonCode"] != DBNull.Value)
                    rec.adjReasonCode = (string) rdr["AdjReasonCode"];
 
                if (rdr["CSR"] != DBNull.Value)
                    rec.cSR = (string) rdr["CSR"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            Deliverable[] convert(DomainObj[] objs)
            {
                Deliverable[] acls  = new Deliverable[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }

		#endregion
        
		}
    }
}
