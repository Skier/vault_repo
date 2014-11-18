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
    public class Service_Transaction : DomainObj
    {
	#region Data
        static string iName = "Service_Transaction";
        int service_Transaction_ID;
        int accNumber;
        string phNumber;
        DateTime trans_Date;
        int charge_Type_ID;
        decimal charge_Amount;
        int tP_ID;
        DateTime tP_SendDate;
        int tP_Status;
	#endregion

	#region Properties
        public override IDomKey IKey 
        {
             get { return new Key(iName, service_Transaction_ID.ToString()); }
        }
        public int Service_Transaction_ID
        {
            get { return service_Transaction_ID; }
        }
        public int AccNumber
        {
            get { return accNumber; }
            set
            {
                setState();
                accNumber = value;
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
        public DateTime Trans_Date
        {
            get { return trans_Date; }
            set
            {
                setState();
                trans_Date = value;
            }
        }
        public int Charge_Type_ID
        {
            get { return charge_Type_ID; }
            set
            {
                setState();
                charge_Type_ID = value;
            }
        }
        public decimal Charge_Amount
        {
            get { return charge_Amount; }
            set
            {
                setState();
                charge_Amount = Decimal.Round(value, 2);
            }
        }
        public int TP_ID
        {
            get { return tP_ID; }
            set
            {
                setState();
                tP_ID = value;
            }
        }
        public DateTime TP_SendDate
        {
            get { return tP_SendDate; }
            set
            {
                setState();
                tP_SendDate = value;
            }
        }
        public int TP_Status
        {
            get { return tP_Status; }
            set
            {
                setState();
                tP_Status = value;
            }
        }
	#endregion

	#region Constructors
		public Service_Transaction()
        {
            sql = new Service_TransactionSQL();
            service_Transaction_ID = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public Service_Transaction(UOW uow) : this()
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
            return new Service_TransactionSQL();
        }
        public override void checkExists()
        {
            if ((Service_Transaction_ID < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
	#endregion

	#region Static methods
		public static void CreateServiceXact(UOW uow,  CustData cd, IDemand dmd)
		{
			IDmdItem[] prods = dmd.OrderSummary(uow).GetProducts(1); // month 1
	
			for (int i = 0; i < prods.Length; i++)
			{	
				WriteServTran(uow, cd, prods[i]);
				
				if (prods[i].Parent != null)
					return;

				for (int j = 0; j < prods[i].Components.Length; j++)
					WriteServTran(uow, cd, prods[i].Components[j], true);

				for (int k = 0; k < prods[i].TagAlongs.Length; k++)
					WriteServTran(uow, cd, prods[i].TagAlongs[k]);
			}
		}
        public static Service_Transaction find(UOW uow, int service_Transaction_ID)
        {
            if (uow.Imap.keyExists(Service_Transaction.getKey(service_Transaction_ID)))
                return (Service_Transaction)uow.Imap.find(Service_Transaction.getKey(service_Transaction_ID));
            
            Service_Transaction cls = new Service_Transaction();
            cls.uow = uow;
            cls.service_Transaction_ID = service_Transaction_ID;
            cls = (Service_Transaction)DomainObj.addToIMap(uow, getOne(((Service_TransactionSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static Service_Transaction[] getAll(UOW uow)
        {
            Service_Transaction[] objs = (Service_Transaction[])DomainObj.addToIMap(uow, (new Service_TransactionSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int service_Transaction_ID)
        {
            return new Key(iName, service_Transaction_ID.ToString());
        }
	#endregion

	#region Implementation
		static string GetTaxCode(int prod)
		{
			return ProdInfoCol.GetProd(prod).TaxCode; 
		}
		static void WriteServTran(UOW uow, CustData cd, IDmdItem pp)
		{
			WriteServTran(uow, cd, pp, false);
		}
		static void WriteServTran(UOW uow, CustData cd, IDmdItem di, bool packComp)
		{
			if (GetTaxCode(di.Prod) == null)
				return;

			if (!IsMonth1(di))
				return;

			if (di.EffPrice == decimal.Zero)
				return;

			WriteSvcTran(uow, cd, di.Prod, di.EffPrice, Int32.Parse(GetTaxCode(di.Prod)));	
		}
		static bool IsMonth1(IDmdItem pp)
		{
			const int month = 1;

			int startMon = (ProdInfoCol.GetProd(pp.Prod)).StartServMon;
			int endMon   = (ProdInfoCol.GetProd(pp.Prod)).EndServMon;

			if (endMon == 0)
				endMon = int.MaxValue;

			if (startMon > month)
				return false;

			if (endMon < month)
				return false;

			return true;
		}
		static void	WriteSvcTran(UOW uow, CustData cd, int prodid, decimal amount, int taxcode)
		{
			if (cd == null)
				throw new ArgumentNullException("CustData is required");


			Service_Transaction st = new Service_Transaction(uow);

			st.AccNumber = cd.AccNumber;
			st.Charge_Amount = amount;
			st.Charge_Type_ID = prodid;
			st.PhNumber = cd.PhNumber;
			st.TP_ID = taxcode; 
			st.Trans_Date = DateTime.Now;
			st.add();
		}
        static Service_Transaction getOne(Service_Transaction[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Service_Transaction src, Service_Transaction tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.service_Transaction_ID = src.service_Transaction_ID;
            tar.accNumber = src.accNumber;
            tar.phNumber = src.phNumber;
            tar.trans_Date = src.trans_Date;
            tar.charge_Type_ID = src.charge_Type_ID;
            tar.charge_Amount = src.charge_Amount;
            tar.tP_ID = src.tP_ID;
            tar.tP_SendDate = src.tP_SendDate;
            tar.tP_Status = src.tP_Status;
            tar.rowState = src.rowState;
        }
 
	#endregion

	#region SQL
        [Serializable]
        class Service_TransactionSQL : SqlGateway
        {
            public Service_Transaction[] getKey(Service_Transaction rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spService_Transaction_Get_Id";
                cmd.Parameters.Add("@Service_Transaction_ID", SqlDbType.Int, 0).Value = rec.service_Transaction_ID;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                Service_Transaction rec = (Service_Transaction)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spService_Transaction_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Service_Transaction_ID"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.service_Transaction_ID = (int)cmd.Parameters["@Service_Transaction_ID"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Service_Transaction rec = (Service_Transaction)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spService_Transaction_Del_Id";
                cmd.Parameters.Add("@Service_Transaction_ID", SqlDbType.Int, 0).Value = rec.service_Transaction_ID;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Service_Transaction rec = (Service_Transaction)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spService_Transaction_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public Service_Transaction[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spService_Transaction_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, Service_Transaction rec)
            {
                cmd.Parameters.Add("@Service_Transaction_ID", SqlDbType.Int, 0).Value = rec.service_Transaction_ID;
                
                // Numeric, nullable foreign key treatment:
                if (rec.AccNumber == 0)
                    cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = rec.accNumber;
 
                if (rec.phNumber == null)
                    cmd.Parameters.Add("@PhNumber", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.PhNumber.Length == 0)
                        cmd.Parameters.Add("@PhNumber", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@PhNumber", SqlDbType.VarChar, 10).Value = rec.phNumber;
                }
 
                cmd.Parameters.Add("@Trans_Date", SqlDbType.DateTime, 0).Value = rec.trans_Date;
                cmd.Parameters.Add("@Charge_Type_ID", SqlDbType.Int, 0).Value = rec.charge_Type_ID;
                cmd.Parameters.Add("@Charge_Amount", SqlDbType.Decimal, 0).Value = rec.charge_Amount;
                cmd.Parameters.Add("@TP_ID", SqlDbType.Int, 0).Value = rec.tP_ID;
 
                if (rec.tP_SendDate == DateTime.MinValue)
                    cmd.Parameters.Add("@TP_SendDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@TP_SendDate", SqlDbType.DateTime, 0).Value = rec.tP_SendDate;
                cmd.Parameters.Add("@TP_Status", SqlDbType.Int, 0).Value = rec.tP_Status;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                Service_Transaction rec = new Service_Transaction();
                
                if (rdr["Service_Transaction_ID"] != DBNull.Value)
                    rec.service_Transaction_ID = (int) rdr["Service_Transaction_ID"];
 
                if (rdr["AccNumber"] != DBNull.Value)
                    rec.accNumber = (int) rdr["AccNumber"];
 
                if (rdr["PhNumber"] != DBNull.Value)
                    rec.phNumber = (string) rdr["PhNumber"];
 
                if (rdr["Trans_Date"] != DBNull.Value)
                    rec.trans_Date = (DateTime) rdr["Trans_Date"];
 
                if (rdr["Charge_Type_ID"] != DBNull.Value)
                    rec.charge_Type_ID = (int) rdr["Charge_Type_ID"];
 
                if (rdr["Charge_Amount"] != DBNull.Value)
                    rec.charge_Amount = Decimal.Round((decimal)rdr["Charge_Amount"], 2);
 
                if (rdr["TP_ID"] != DBNull.Value)
                    rec.tP_ID = (int) rdr["TP_ID"];
 
                if (rdr["TP_SendDate"] != DBNull.Value)
                    rec.tP_SendDate = (DateTime) rdr["TP_SendDate"];
 
                if (rdr["TP_Status"] != DBNull.Value)
                    rec.tP_Status = (int) rdr["TP_Status"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            Service_Transaction[] convert(DomainObj[] objs)
            {
                Service_Transaction[] acls  = new Service_Transaction[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
	#endregion
		}
    }
}