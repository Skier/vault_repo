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
    public class DmdTax : DomainObj, IDmdTax, ISummable
	{
        /*        Data        */
        static string iName = "DmdTax";
        int id;
        DmdItem dmdItem;
		int dmdItemId;
        string taxId;
		int taxProd;
        decimal taxAmount;
		string taxCode;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
        }
		public int DmdItemId 
		{
			get 
			{
				if (DmdItm != null)
					return DmdItm.Id;

				return 0;
			}
		}
		public IDmdItem DmdItm
        {
            get 
			{
				if (dmdItem == null)
					if (dmdItemId > 0)
						dmdItem = DmdItem.find(uow, dmdItemId);

				return dmdItem; 
			}
            set
            {
                setState();
                dmdItem = (DmdItem)value;
				this.dmdItemId = dmdItem.Id;
            }
        }
        public string TaxId
        {
            get { return taxId; }
            set
            {
                setState();
                taxId = value;
            }
        }
		public int TaxProd
		{
			get { return taxProd; }
			set { taxProd = value; }
		}
        public decimal TaxAmount
        {
            get { return taxAmount; }
            set
            {
                setState();
                taxAmount = Decimal.Round(value, 2);
            }
        }
		public string TaxCode { get { return taxCode; } set { taxCode = value;}}
		public string Description { get { return TaxDescription.TaxTypeToString(TaxId); }}
 		#region ISummable Members

		public string SumType { get	{ return taxId; }}
		public decimal Amount
		{
			get {
				return taxAmount; 
			}

			set	{ 
				taxAmount = value;
			}
		}
		#endregion       
        /*        Constructors			*/
        public DmdTax()
        {
            sql = new DmdTaxSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
			
			priority = 16000;
        }
        public DmdTax(IUOW uow) : this()
        {
            if(uow == null)
                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
            if(uow.Imap == null)
                throw new ArgumentException("IdentityMap is required", "Identity Map");
            
            this.uow = (UOW)uow;
            this.uow.Imap.add(this);
        }
		public DmdTax(IUOW uow, IDmdItem dmdItem, IProdTax tax) : this(uow)
		{
			DmdItm = dmdItem;
			taxId =  tax.TaxType; 
			taxProd = this.dmdItem.Prod;
            taxAmount = decimal.Round(tax.TaxAmt, 2);
			taxCode = tax.TaxCode;
		}

        /*        Methods        */
        protected override SqlGateway loadSql()
        {
            return new DmdTaxSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        
		#region Static methods
		public static DmdTax[] ConvDomObj(UOW uow, DmdItem di, IDmdTax[] taxes)
		{
			if (taxes == null)
				return new DmdTax[0];

			if (taxes.Length == 0)
				return new DmdTax[0];
			
			ArrayList ar = new ArrayList(taxes.Length);

			for (int i = 0; i < taxes.Length; i++)
			{
				if (taxes[i].TaxAmount == 0m)
					continue;

				DmdTax dTax = new DmdTax(uow);

				dTax.DmdItm = di;
				dTax.TaxId = taxes[i].TaxId;
				dTax.TaxProd = taxes[i].TaxProd;
				dTax.TaxAmount = taxes[i].TaxAmount;
				dTax.TaxCode = taxes[i].TaxCode;

				ar.Add(dTax);
			}
			DmdTax[] dTaxes = new DmdTax[ar.Count];
			ar.CopyTo(dTaxes);

			return dTaxes;
		}
		public static DmdTax[] FilterConvert(IDmdItem di,IDmdTax[] taxes)
		{
			if (taxes == null)
				return new DmdTax[0];

			if (taxes.Length == 0)
				return new DmdTax[0];

			ArrayList ar = new ArrayList(taxes.Length);
			for (int i = 0; i < taxes.Length; i++)
				if (taxes[i].TaxAmount != 0m)
				{
					taxes[i].DmdItm = di;
					ar.Add(taxes[i]);
				}
			IDmdTax[] txs = new IDmdTax[ar.Count];
			ar.CopyTo(txs);

			return Convert(txs);
		}
		public static DmdTax[] Convert(IDmdTax[] taxes)
		{
			DmdTax[] txs = new DmdTax[taxes.Length];
			for (int i = 0; i < txs.Length; i++)
				txs[i] = (DmdTax)taxes[i];
			
			return txs;
		}
        public static DmdTax find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(DmdTax.getKey(id)))
                return (DmdTax)uow.Imap.find(DmdTax.getKey(id));
            
            DmdTax cls = new DmdTax();
            cls.uow = uow;
            cls.id = id;
            cls = (DmdTax)DomainObj.addToIMap(uow, getOne(((DmdTaxSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
		public static DmdTax[] getDmdItemTaxes(UOW uow, int dmdItem)
		{
			DmdTax[] objs 
				= (DmdTax[])DomainObj.addToIMap(uow, (new DmdTaxSQL()).getDmdItemTaxes(uow, dmdItem));
			
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			
			return objs;
		}
        public static DmdTax[] getAll(UOW uow)
        {
            DmdTax[] objs = (DmdTax[])DomainObj.addToIMap(uow, (new DmdTaxSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
		#endregion
        /*		Implementation		*/
		public override	void RefreshForeignKeys()
		{
			if (dmdItem != null)
				dmdItemId = DmdItm.Id;
		}
        static DmdTax getOne(DmdTax[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(DmdTax src, DmdTax tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id        = src.id;
            tar.dmdItem   = src.dmdItem;
			tar.dmdItemId = src.dmdItemId;
            tar.taxId     = src.taxId;
			tar.taxProd   = src.taxProd;
            tar.taxAmount = src.taxAmount;
            tar.rowState  = src.rowState;
			tar.taxCode   = src.taxCode;
        }
         /*		SQL		*/
        [Serializable]
        class DmdTaxSQL : SqlGateway
        {
            public DmdTax[] getKey(DmdTax rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spDmdTax_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                DmdTax rec = (DmdTax)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spDmdTax_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                DmdTax rec = (DmdTax)obj;
				if (rec.id < 1)
				{
					rec.rowState = RowState.Deleted;
					return;
				}

				SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spDmdTax_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                DmdTax rec = (DmdTax)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spDmdTax_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public DmdTax[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spDmdTax_Get_All";
                return convert(execReader(cmd));
            }
			public DmdTax[] getDmdItemTaxes(UOW uow, int dmdItem)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spDmdTax_Get_DmdItem";
                cmd.Parameters.Add("@DmdItem", SqlDbType.Int, 0).Value = dmdItem;
				return convert(execReader(cmd));
			}

            /*        Implementation        */
            void setParam(SqlCommand cmd, DmdTax rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                // Numeric, nullable foreign key treatment:
                cmd.Parameters.Add("@DmdItem", SqlDbType.Int, 0).Value = rec.DmdItemId;
 
				cmd.Parameters.Add("@TaxId", SqlDbType.VarChar, 3).Value = rec.taxId;
				cmd.Parameters.Add("@TaxProd", SqlDbType.Int, 0).Value = rec.taxProd;
				cmd.Parameters.Add("@TaxAmount", SqlDbType.Decimal, 0).Value = rec.taxAmount;
				
				if (rec.taxCode == null)
					cmd.Parameters.Add("@TaxCode", SqlDbType.VarChar, 5).Value = DBNull.Value;
				else
				{
					if (rec.taxCode.Trim().Length == 0)
						cmd.Parameters.Add("@TaxCode", SqlDbType.VarChar, 15).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@TaxCode", SqlDbType.VarChar, 15).Value = rec.taxCode.Trim();
				}

			}
            protected override DomainObj reader(SqlDataReader rdr)
            {
                DmdTax rec = new DmdTax();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["DmdItem"] != DBNull.Value)
                    rec.dmdItemId = (int) rdr["DmdItem"];
 
                if (rdr["TaxId"] != DBNull.Value)
                    rec.taxId = (string) rdr["TaxId"];

				if (rdr["TaxProd"] != DBNull.Value)
					rec.taxProd = (int) rdr["TaxProd"];

                if (rdr["TaxAmount"] != DBNull.Value)
                    rec.taxAmount = Decimal.Round((decimal)rdr["TaxAmount"], 2);

				if (rdr["TaxCode"] != DBNull.Value)
					rec.taxCode = (string)rdr["TaxCode"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            DmdTax[] convert(DomainObj[] objs)
            {
				if (objs == null)
					return new DmdTax[0];

				if (objs.Length == 0)
					return new DmdTax[0];

                DmdTax[] acls  = new DmdTax[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
		}

	}
}
