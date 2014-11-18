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
	public class PayInfo : DomainObj, IPayInfo, IId
	{
	#region Data
		static string iName = "PayInfo";
		protected string payClass;
		protected int id;
		protected IDemand demand;
		protected int dmdId;
		protected DateTime pymtDate;
		protected int pymtType;
		protected string status;		
		
		protected string confNumber;
		protected string vFConf;
		protected bool isConfReq;

		protected IPayInfoTran tran;	
		protected int tranNumber;
		protected int payInfoSource;
		
		protected string checkNumber;
		protected string checkName;

		//  Amounts
		protected decimal totAmtDue;		
		protected decimal amtPaid;
		protected decimal amtTendered;

		// Local only
		protected decimal localDue;
		protected decimal localPaid;

		protected decimal amtLDDue; // ld due
		protected decimal amdLDPaid; // ld paid
	#endregion

	#region Properties
		public override IDomKey IKey 
		{
			get { return new Key(iName, id.ToString()); }
		}
		public int Id
		{
			get { return id; }
		}
		public IDemand ParDemand
		{
			get 
			{
				if (demand == null)
					if (dmdId > 0)
						demand = Demand.find(uow, dmdId);
 
				return demand; 
			}
			set 
			{ 
				demand = (Demand)value; 
				dmdId = 0;
				
				if (demand != null)
					dmdId = demand.Id;
				
				setState();
			}
		}
		public int DmdId 
		{ 
			get	
			{ 
				if (ParDemand == null)
					return 0;

				return ParDemand.Id;
			}
			set 
			{
				if (demand != null)
					if (demand.Id != value)
						throw new ApplicationException(
							"Demand id of Set propery conflicts with Demand already in this DmdItem");

				this.dmdId = value;
			}
		}
		public DateTime PayDate
		{
			get { return pymtDate; }
			set
			{
				setState();
				pymtDate = value;
			}
		}
		
		public PaymentType PaymentType
		{
			get { return (PaymentType)pymtType; }
			set
			{
				setState();
				pymtType = (int)value;
			}
		}

		public PayInfoSource PayInfoSource
		{
			get 
			{
				if (tran == null)
					return PayInfoSource.Unknown;
			
				return tran.Source;
			}
			set
			{
				setState();
				payInfoSource = (int)value;
			}
		}
		public string ConfNumber
		{
			get { return confNumber; }
			set
			{
				setState();
				confNumber = value;
			}
		}
		public string VFConf
		{
			get { return vFConf; }
			set
			{
				setState();
				vFConf = value;
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

		public bool IsConfReq 
		{	
			get { return isConfReq;	}
			set { isConfReq = value; }
		}
		public string PayClass { get { return payClass; }}
	
		/*        Amounts Properties        */
		public virtual decimal LocalAmountDue
		{
			get { throw new ApplicationException("LocalAmountDue is not supported"); }
			set { throw new ApplicationException("LocalAmountDue is not supported");}
		}
		public virtual decimal LocalAmountPaid
		{
			get { throw new ApplicationException("LocalAmountPaid is not supported"); }
			set { throw new ApplicationException("LocalAmountPaid is not supported");}
		}

		public virtual decimal LdAmount
		{
			get { throw new ApplicationException("LdAmount is not supported"); }
			set { throw new ApplicationException("LdAmount is not supported");}
		}
		public virtual decimal LdAmountDue
		{
			get { throw new ApplicationException("LdAmountDue is not supported"); }
			set { throw new ApplicationException("LdAmountDue is not supported");}
		}		
		
		public virtual decimal AmountTendered
		{
			get { return amtTendered; }
			set
			{
				setState();
				amtTendered = Decimal.Round(value, 2);
			}
		}
		
		public virtual decimal TotalAmountPaid 
		{ 
			get { return amtPaid; }
			set 
			{
				setState();
				amtPaid = Decimal.Round(value, 2);
			}
		}
		public virtual decimal TotalAmountDue  
		{ 
			get { return this.totAmtDue; }
			set 
			{
				setState();
				this.totAmtDue = decimal.Round(value, 2);
			}
		} 
		public virtual decimal ChangeAmount
		{ 
			get 
			{ 
				if (amtTendered < amtPaid)
					return decimal.Zero;

				return amtTendered - amtPaid; 
			}
		}				
		public virtual decimal TotalAmtPaid
		{
			get { return amtPaid; }
			set
			{
				setState();
				amtPaid = Decimal.Round(value, 2);
				// do other business
			}
		}
		public int	TranNumber
		{ 
			get 
			{
				if (tran == null)					
						return 0;

				return tran.TranNumber;
			}
			set
			{
				setState();
				tranNumber = value;
			}
		}
		public IPayInfoTran Tran
		{
			get 
			{
				if (tran != null)
					return tran; 
				
				if ((tranNumber > 0) && (payInfoSource != (int)PayInfoSource.Unknown))
					tran = TranFactory.GetTran(tranNumber, (PayInfoSource)payInfoSource);
				
				return tran;
			}
			set
			{	
				setState();
				
				tran = value;	
				tranNumber = 0;
				payInfoSource = (int)PayInfoSource.Unknown;				

				if (tran == null)
					return;
				
				tranNumber = tran.TranNumber;
				payInfoSource = (int)tran.Source;
			}
		}
		public string CheckNumber
		{ 
			get { return checkNumber;	} 
			set
			{
				setState();
				checkNumber = value;
			}
		}
		public string CheckName
		{ 
			get { return checkName; } 
			set
			{
				setState();
				checkName = value;
			}
		}


	#endregion

	#region Public Constructors
		public static PayInfo GetPayInfo(string pClass)
		{
			PayInfoClass payClass = Conv(pClass);
			switch(payClass)
			{
				case PayInfoClass.PayInfo :
					return new PayInfo(); 

				case PayInfoClass.PayInfoLocal :
					return new PayInfoLocal();
			}
			return null;
		}
		public static PayInfo GetPayInfo(UOW uow, string pClass)
		{
			PayInfoClass payClass = Conv(pClass);
			switch(payClass)
			{
				case PayInfoClass.PayInfo :
					return new PayInfo(uow); 

				case PayInfoClass.PayInfoLocal :
					return new PayInfoLocal(uow);
			}
			return null;
		}
		public static PayInfo GetPayInfo(IMap imap, string pClass)
		{
			PayInfoClass payClass = Conv(pClass);
			switch(payClass)
			{
				case PayInfoClass.PayInfo :
					return new PayInfo(imap); 

				case PayInfoClass.PayInfoLocal :
					return new PayInfoLocal(imap);
			}
			return null;
		}
	#endregion

	#region Protected Constructors
		protected PayInfo()
		{
			sql = new PayInfoSQL();
			id = random.Next(Int32.MinValue, -1);
			priority = 17000;
			rowState = RowState.New;
			
			payClass = PayInfoClass.PayInfo.ToString();
			status = PaymentStatus.Incomplete.ToString();
			pymtDate = DateTime.Now;
		}
		protected PayInfo(UOW uow) : this()
		{
			if(uow == null)
				throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
			if(uow.Imap == null)
				throw new ArgumentException("IdentityMap is required", "Identity Map");
            
			this.uow = uow;
			this.uow.Imap.add(this);
		}
		protected PayInfo(IMap imap) : this()
		{
          
			if(imap == null)
				throw new ArgumentException("IdentityMap is required", "Identity Map");
            
			imap.add(this);
		}
        	
		static PayInfoClass Conv(string payClass)
		{
			if (payClass == PayInfoClass.PayInfo.ToString())
				return PayInfoClass.PayInfo;

			if (payClass == PayInfoClass.PayInfoLocal.ToString())
				return PayInfoClass.PayInfoLocal;

			throw new ArgumentException("Unknow PayInfo class: " + payClass);
		}
		
	#endregion

	#region Methods  
		public virtual string Validate()
		{
			if (this.amtPaid > this.amtTendered) 
				if (this.amtTendered == 0m)
					return "Please enter Amount Collected";

			if (this.amtPaid > this.amtTendered)
				return "Amount Paid cannot be more than Amount Collected";

			//			if (this.amtPaid < this.LdAmountDue) 
			//				return "Amount Paid cannot be less than Long Distance Calling Card amount";

			return string.Empty;
		}
		//		public void UpdateFromWireless(IUOW uow, string confNum)
		//		{
		//			IWireless_Transactions wlt 
		//				= Wireless_Transactions.find((UOW)uow, confNum, ParDemand.StoreCode, TotalAmountPaid);
		//
		//			TranNumber = wlt.Wireless_Transaction_ID;
		//			VFConf = wlt.TrConfirm.ToString();
		//			PayInfoSource = PayInfoSource.Wireless; 	
		//		}
		//		public void UpdateFromVerifone(IUOW uow, IReceipt rcpt)
		//		{
		//			IVerifone_Transaction vt 
		//				= Verifone_Transaction.find((UOW)uow, rcpt.AccNumber, rcpt.ConfNum);
		//
		//			if (vt == null)
		//				return;
		//
		//			TranNumber = vt.Verifone_Transaction_ID;
		//			VFConf = vt.TrConfirm.ToString();
		//			PayInfoSource = PayInfoSource.Verifone;
		//		}
		//		public decimal GetComAmt(IUOW uow, string storeCode)
		//		{
		//			if (Tran == null)
		//				return decimal.Zero;
		//
		//			return tran.ComAmount;
		//		}
		//		public decimal GetComAmt(IUOW uow, string storeCode)
		//		{
		//			if (this.PayInfoSource == PayInfoSource.Wireless)
		//				return Wireless_Transactions.GetCommissionAmt((UOW)uow, vFConf, storeCode, amtPaid);
		//
		//			if (this.PayInfoSource == PayInfoSource.Verifone)
		//				return this.amtPaid/10; //return Verifone_Transaction.GetCommissionAmt((UOW)uow, int.Parse(vFConf), ParDemand.BillPayer.ToString());
		// 
		//			if (this.PayInfoSource == PayInfoSource.FinProd)
		//				return FinancialProdTrans.find((UOW)uow, this.ParDemand).ComAmt;				
		//
		//			return 0m;
		//		}
		//		
		public override void checkExists()
		{
			if ((Id < 1))
				throw new ArgumentException("Valid row is required for update and delete");
		}
		public virtual void SetAmts(decimal amtPaid, decimal localDue, decimal amtLDDue, decimal amtTendered)
		{
			setState();
			this.amtPaid = amtPaid;
			this.amtTendered = amtTendered;
		}
		public virtual void PayInFull(decimal amtTendered)
		{
			this.TotalAmtPaid = this.totAmtDue;
			this.amtTendered = amtTendered;
		}
		public virtual void PayInFull(decimal localDue, decimal amtLDDue, decimal amtTendered)
		{
			throw new ApplicationException("PayInFull(localDue, amtLDDue, amtTendered) is not supported");
		}

		public override	void RefreshForeignKeys()
		{
			RefreshDemand();
			RefreshTran();
		}	

	#endregion

	#region Static methods
		public static PayInfo Sum(PayInfo[] pymt, PaymentStatus ps)
		{
			if (pymt == null)
				return null;
			
			if (pymt.Length == 0)
				return null;
	
			if (pymt.Length == 1)
				return pymt[0];

			PayInfo pi = PayInfo.GetPayInfo(pymt[0].payClass);
			
			for (int i = 0; i < pymt.Length; i++)
				if (pymt[i].Status.Trim().ToLower() == ps.ToString().ToLower())
				{
					// direct	
					pi.amtLDDue = pi.amdLDPaid += pymt[i].LdAmount;
					pi.localPaid += pymt[i].LocalAmountPaid;
					pi.localDue += pymt[i].LocalAmountDue;
					pi.amtPaid += pymt[i].TotalAmountPaid;		

					// via properties
					pi.AmountTendered += pymt[i].AmountTendered;
					pi.ParDemand = pymt[i].ParDemand;				
					pi.PaymentType = pymt[i].PaymentType;
					pi.ConfNumber = pymt[i].ConfNumber;
					pi.VFConf = pymt[i].VFConf;
					pi.Status = pymt[i].Status;
				}

			return pi;
		}
		public static PayInfo find(UOW uow, int id)
		{
			if (uow.Imap.keyExists(PayInfo.getKey(id)))
				return (PayInfo)uow.Imap.find(PayInfo.getKey(id));
            
			PayInfo cls = new PayInfo();
			cls.uow = uow;
			cls.id = id;
			cls = (PayInfo)DomainObj.addToIMap(uow, getOne(((PayInfoSQL)cls.Sql).getKey(cls)));
			
			cls.uow = uow;
			cls.GetDmd(uow); 
            
			return cls;
		}
		public static PayInfo[] getAll(UOW uow)
		{
			PayInfo[] objs = (PayInfo[])DomainObj.addToIMap(uow, (new PayInfoSQL()).getAll(uow));
			for (int i = 0; i < objs.Length; i++)
			{
				objs[i].uow = uow;
				objs[i].GetDmd(uow);
			}
			return objs;
		}
		public static PayInfo[] getDmdPayInfo(UOW uow, int dmd)
		{
			PayInfo[] objs = (PayInfo[])DomainObj.addToIMap(uow, (new PayInfoSQL()).getDmdPayInfo(uow, dmd));
			for (int i = 0; i < objs.Length; i++)
			{
				objs[i].uow = uow;
				objs[i].GetDmd(uow);
			}
			return objs;
		}
		public static PayInfo[] getStorePayInfo(UOW uow, string storeCode, PaymentStatus paymentStatus)
		{
			PayInfo[] pis = (PayInfo[])DomainObj.addToIMap(uow, (new PayInfoSQL()).getStorePayInfo(uow, storeCode, paymentStatus));
			for (int i = 0; i < pis.Length; i++)
			{
				pis[i].uow = uow;
				pis[i].GetDmd(uow);
			}
			return pis;
		}
		public static PayInfo[] getTranNumberPayInfo(UOW uow, int tran, PayInfoSource source)
		{
			PayInfo[] pis = (PayInfo[])DomainObj.addToIMap(uow, (new PayInfoSQL()).getTranNumPayInfo(uow, tran, source));
			for (int i = 0; i < pis.Length; i++)
			{
				pis[i].uow = uow;
				pis[i].GetDmd(uow);
			}
			return pis;
		}

		// getTranNumPayInfo(
		public static Key getKey(int id)
		{
			return new Key(iName, id.ToString());
		}
	#endregion

	#region Implementation
		protected virtual void RefreshDemand()
		{
			if (dmdId > 0)
				return;

			if (ParDemand == null)
				return;

			dmdId = ParDemand.Id;
		}
		protected virtual void RefreshTran()
		{
			if (tranNumber > 0)
				return;

			if (Tran == null)
				return;

			tranNumber = tran.TranNumber;
			this.payInfoSource = (int)tran.Source;
		}
		protected override SqlGateway loadSql()
		{
			return new PayInfoSQL();
		}

		protected virtual void Recalc()
		{
		}
		protected virtual decimal ApplyToLD(decimal amt)
		{
			return 0m;
		}
		protected virtual decimal ApplyToLocal(decimal amt)
		{
			return 0m;
		}
		static PayInfo getOne(PayInfo[] acls)
		{
			if (acls.Length == 1)
				return acls[0];
            
			if (acls.Length == 0)
				throw new ArgumentException("Row not found");
            
			throw new ArgumentException("More than one row found");
		}
		static void copyAttrs(PayInfo src, PayInfo tar)
		{
			if (tar == null)
				throw new ArgumentNullException("Target object must not be null");
            
			if (src == null)
				throw new ArgumentNullException("Source object must not be null");
            
			tar.id = src.id;
			tar.demand   = src.demand;
			tar.pymtDate = src.pymtDate;
			tar.localDue = src.localDue;
			tar.localPaid = src.localPaid;
			tar.amdLDPaid = src.amdLDPaid;
			tar.amtTendered = src.amtTendered;
			tar.pymtType = src.pymtType;
			tar.payInfoSource = src.payInfoSource;
			tar.confNumber = src.confNumber;
			tar.status = src.status;
			tar.vFConf = src.vFConf;
			tar.rowState = src.rowState;
			tar.amtPaid = src.amtPaid;
			tar.amtLDDue = src.amtLDDue;
			tar.totAmtDue = src.totAmtDue;
			tar.payClass = src.payClass;
			tar.tranNumber = src.tranNumber;
			tar.checkNumber = src.checkNumber;
			tar.checkName = src.checkName;

		}
 
		void GetDmd(UOW uow)
		{
			if (this.dmdId > 0)
				this.demand = Demand.find(uow, this.dmdId); 
		}
	#endregion

	#region SQL
		[Serializable]
			class PayInfoSQL : SqlGateway
		{
			public PayInfo[] getKey(PayInfo rec)
			{
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spPaymentInfo_Get_Id2";
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
				return convert(execReader(cmd));
			}
			public override void insert(DomainObj obj)
			{
				PayInfo rec = (PayInfo)obj;
				rec.RefreshForeignKeys();    
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spPaymentInfo_Ins2";

				setParam(cmd, rec);
                
				cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
				execScalar(cmd);
				rec.id = (int)cmd.Parameters["@Id"].Value;
				rec.rowState = RowState.Clean;
			}
			public override void delete(DomainObj obj)
			{
				PayInfo rec = (PayInfo)obj;
				if (rec.id < 1)
					return;

				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spPaymentInfo_Del_Id";
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
				execScalar(cmd);
				rec.rowState = RowState.Deleted;
			}
			public override void update(DomainObj obj)
			{
				PayInfo rec = (PayInfo)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spPaymentInfo_Upd2";
				setParam(cmd, rec);
                
				execScalar(cmd);
				rec.rowState = RowState.Clean;
			}
			public PayInfo[] getAll(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spPaymentInfo_Get_All2";
				return convert(execReader(cmd));
			}
			public PayInfo[] getDmdPayInfo(UOW uow, int dmd)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spPaymentInfo_Get_Dmd2";
				cmd.Parameters.Add("@Dmd", SqlDbType.Int, 0).Value = dmd;
				return convert(execReader(cmd));
			}
			public PayInfo[] getStorePayInfo(UOW uow, string storeCode, PaymentStatus paymentStatus)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spPaymentInfo_Get_Store2";
				cmd.Parameters.Add("@StoreCode", SqlDbType.Char, 10).Value = storeCode;
				cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = paymentStatus.ToString();
				return convert(execReader(cmd));
			}
			public PayInfo[] getTranNumPayInfo(UOW uow, int tran, PayInfoSource source)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spPaymentInfo_Get_Tran";
				cmd.Parameters.Add("@Tran", SqlDbType.Int, 4).Value = tran;
				cmd.Parameters.Add("@TranSource", SqlDbType.Int, 4).Value = (int)source;
				return convert(execReader(cmd));
			}
		#endregion

		#region SQL Implementation
			void setParam(SqlCommand cmd, PayInfo rec)
			{
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
				// Numeric, nullable foreign key treatment:
				if (rec.dmdId == 0)
					cmd.Parameters.Add("@Dmd", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Dmd", SqlDbType.Int, 0).Value = rec.dmdId;
 
				if (rec.pymtDate == DateTime.MinValue)
					cmd.Parameters.Add("@PymtDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@PymtDate", SqlDbType.DateTime, 0).Value = rec.pymtDate;
				cmd.Parameters.Add("@PymtType", SqlDbType.Int, 0).Value = rec.pymtType;
				cmd.Parameters.Add("@TranSource", SqlDbType.Int, 0).Value = rec.payInfoSource;

				cmd.Parameters.Add("@AmtTendered", SqlDbType.Decimal, 0).Value = rec.amtTendered;				
				cmd.Parameters.Add("@AmtPaid", SqlDbType.Decimal, 0).Value = rec.amtPaid;
				cmd.Parameters.Add("@LocalDue", SqlDbType.Decimal, 0).Value = rec.localDue;
				cmd.Parameters.Add("@LocalPaid", SqlDbType.Decimal, 0).Value = rec.localPaid;
				cmd.Parameters.Add("@AmtLdDue", SqlDbType.Decimal, 0).Value = rec.amtLDDue;
				cmd.Parameters.Add("@LDamount", SqlDbType.Decimal, 0).Value = rec.amdLDPaid;
				cmd.Parameters.Add("@TotAmtDue", SqlDbType.Decimal, 0).Value = rec.totAmtDue;

				if (rec.confNumber == null)
					cmd.Parameters.Add("@ConfNumber", SqlDbType.VarChar, 20).Value = DBNull.Value;
				else
				{
					if (rec.ConfNumber.Length == 0)
						cmd.Parameters.Add("@ConfNumber", SqlDbType.VarChar, 20).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@ConfNumber", SqlDbType.VarChar, 20).Value = rec.confNumber;
				}
 
				if (rec.vFConf == null)
					cmd.Parameters.Add("@VFConf", SqlDbType.VarChar, 20).Value = DBNull.Value;
				else
				{
					if (rec.vFConf.Length == 0)
						cmd.Parameters.Add("@VFConf", SqlDbType.VarChar, 20).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@VFConf", SqlDbType.VarChar, 20).Value = rec.vFConf;
				}
 
				if (rec.status == null)
					cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = DBNull.Value;
				else
				{
					if (rec.status.Length == 0)
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = rec.status;
				}
				cmd.Parameters.Add("@IsConfReq", SqlDbType.Bit, 0).Value = rec.isConfReq;

				if (rec.tranNumber == 0)
					cmd.Parameters.Add("@TranNumber", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@TranNumber", SqlDbType.Int, 0).Value = rec.tranNumber;

				if (rec.checkNumber == null)
					cmd.Parameters.Add("@CheckNumber", SqlDbType.VarChar, 15).Value = DBNull.Value;
				else
				{
					if (rec.checkNumber.Length == 0)
						cmd.Parameters.Add("@CheckNumber", SqlDbType.VarChar, 15).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@CheckNumber", SqlDbType.VarChar, 15).Value = rec.checkNumber;
				}

				if (rec.checkName == null)
					cmd.Parameters.Add("@CheckName", SqlDbType.VarChar, 15).Value = DBNull.Value;
				else
				{
					if (rec.checkName.Length == 0)
						cmd.Parameters.Add("@CheckName", SqlDbType.VarChar, 15).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@CheckName", SqlDbType.VarChar, 15).Value = rec.checkName;
				}
				
				if (rec.payClass == null)
					throw new ApplicationException("PayInfo.PayClass is required");
				
				if (rec.payClass.Length == 0)
					throw new ApplicationException("PayInfo.PayClass is required");
				
				cmd.Parameters.Add("@PayClass", SqlDbType.VarChar, 20).Value = rec.payClass;				
			}
			protected override DomainObj reader(SqlDataReader rdr)
			{

				string pclass = PayInfoClass.PayInfoLocal.ToString(); // default

				if (rdr["PayClass"] != DBNull.Value)
					pclass = (string)rdr["PayClass"];	

				PayInfo rec = GetPayInfo(pclass);

				if (rdr["Id"] != DBNull.Value)
					rec.id = (int) rdr["Id"];
 
				if (rdr["Dmd"] != DBNull.Value)
					rec.dmdId = (int) rdr["Dmd"];
 
				if (rdr["PymtDate"] != DBNull.Value)
					rec.pymtDate = (DateTime) rdr["PymtDate"];
 
				if (rdr["PymtType"] != DBNull.Value)
					rec.pymtType = (int) rdr["PymtType"];
				
				if (rdr["TranSource"] != DBNull.Value)
					rec.payInfoSource = (int) rdr["TranSource"];

				if (rdr["ConfNumber"] != DBNull.Value)
					rec.confNumber = (string) rdr["ConfNumber"];
				 
				if (rdr["VFConf"] != DBNull.Value)
					rec.vFConf = (string) rdr["VFConf"];

				if (rdr["Status"] != DBNull.Value)
					rec.status = (string) rdr["Status"];
				
				if (rdr["IsConfReq"] != DBNull.Value)
					rec.isConfReq = (bool) rdr["IsConfReq"];

				if (rdr["TranNumber"] != DBNull.Value)
					rec.tranNumber = (int) rdr["TranNumber"];

				if (rdr["CheckNumber"] != DBNull.Value)
					rec.checkNumber = (string) rdr["CheckNumber"];

				if (rdr["CheckName"] != DBNull.Value)
					rec.checkName = (string) rdr["CheckName"];

				// amounts

				if (rdr["AmtPaid"] != DBNull.Value)
					rec.amtPaid = Decimal.Round((decimal)rdr["AmtPaid"], 2);
				
				if (rdr["AmtTendered"] != DBNull.Value)
					rec.amtTendered = Decimal.Round((decimal)rdr["AmtTendered"], 2);

				if (rdr["TotAmtDue"] != DBNull.Value)
					rec.totAmtDue = Decimal.Round((decimal)rdr["TotAmtDue"], 2);

				// local only
				
				if (rdr["LocalDue"] != DBNull.Value)
					rec.localDue = Decimal.Round((decimal)rdr["LocalDue"], 2);
 
				if (rdr["LocalPaid"] != DBNull.Value)
					rec.localPaid = Decimal.Round((decimal)rdr["LocalPaid"], 2);
 
				if (rdr["LDamount"] != DBNull.Value)
					rec.amdLDPaid = Decimal.Round((decimal)rdr["LDamount"], 2);


				if (rdr["AmtLdDue"] != DBNull.Value)
					rec.amtLDDue = Decimal.Round((decimal)rdr["AmtLdDue"], 2);

				rec.rowState = RowState.Clean;
				return rec;
			}
			PayInfo[] convert(DomainObj[] objs)
			{
				PayInfo[] acls  = new PayInfo[objs.Length];
				objs.CopyTo(acls, 0);
				return  acls;
			}
		}
		#endregion
	}
}