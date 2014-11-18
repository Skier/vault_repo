using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using DPI.Components;
using DPI.Interfaces;
using BillSoft.EZTaxNET;
 
namespace DPI.Components
{
	[Serializable]
	public class Demand : DomainObj, IDemand, IId
	{
	#region Data
    
		static string iName = "Demand";
       
		int id;
		string dmdType;
        
		int statement;
		int consId;
		ICustInfo2 consumer;
		string consumerAgent;
		int loc;
		int ilec;
		
		bool isUnderWF;
		string workflow;
		string wFStep;
		string status;
		DateTime dmdDate;
		DateTime statusChangeDate;
		int billPayer;
		string storeCode;
		int source;

		ArrayList dmdItems;

	#endregion
        
	#region Properties

		public override IDomKey IKey 
		{
			get { return new Key(iName, id.ToString()); }
		}
		public int Id
		{
			get { return id;		}
			set { this.id = value;	}
		}
		public string DmdType
		{
			get { return dmdType; }
			set
			{
				setState();
				dmdType = value;
			}
		}
		public int Statement
		{
			get { return statement; }
			set
			{
				setState();
				statement = value;
			}
		}
		public ICustInfo2 Consumer
		{
			get 
			{
				if (consumer == null)
					if (consId > 0)
						consumer = CustInfo.find(uow, consId);
 
				return consumer; 
			}
			set 
			{ 
				consumer = (ICustInfo2)value; 
				consId = 0;
				
				if (consumer != null)
					consId = consumer.CustInfoID;
				
				setState();
			}
		}
		public int ConsId 
		{ 
			get	
			{ 
				if (Consumer == null)
					return 0;

				return Consumer.CustInfoID;
			}
			set 
			{
				if (consumer != null)
					if (consumer.CustInfoID != value)
						throw new ApplicationException(
							"Consumer id of Set propery conflicts with CustInfo already in this Demand");

				this.consId = value;
			}
		}
		public string ConsumerAgent
		{
			get { return consumerAgent; }
			set
			{
				setState();
				consumerAgent = value;
			}
		}
		public string Status
		{
			get { return status; }
			set
			{
				setState();
				statusChangeDate = DateTime.Now;
				status = value;
			}
		}
		public bool IsUnderWF
		{
			get { return isUnderWF; }
			set
			{
				setState();
				isUnderWF = value;
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
		public string WFStep
		{
			get { return wFStep; }
			set
			{
				setState();
				wFStep = value;
			}
		}
		public DateTime DmdDate
		{
			get { return dmdDate; }
			set 
			{
				setState();
				dmdDate = value;
			}
		}
		public DateTime StatusChangeDate
		{
			get {return statusChangeDate;}
			set
			{
				setState();
				statusChangeDate = value;
			}
		}
		public int BillPayer
		{
			get {return billPayer;}
			set
			{
				setState();
				billPayer = value;
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
		public int Loc 
		{ 
			get { return loc; }
			set { loc = value; }
		}
		public int Ilec 
		{ 
			get { return ilec; }
			set { ilec = value; }
		}
		public IDmdItem[] GetDmdItems()
		{
			if (dmdItems == null)
				return new DmdItem[0];

			DmdItem[] dis = new DmdItem[dmdItems.Count];
			dmdItems.CopyTo(dis);
			return dis;
		}
	
		public IDmdItem[] GetDmdItems(IUOW uow)
		{
			DmdItem[] dis = DmdItem.GetDmd((UOW)uow, this.Id);
			return dis;
		}
		public int Source
		{
			get { return source; }
			set { source = value; }
		}
	#endregion
        
	#region Constructors

		public Demand()
		{
			sql = new DemandSQL();
			id = random.Next(Int32.MinValue, -1);
			rowState = RowState.New;
			priority = 12000;
			dmdDate = DateTime.Now;
		}
		public Demand(string dmdType) : this()
		{
			if(dmdType == null)
				throw new ArgumentException("DemandType is required", "Demand Type");
			
			if (dmdType.Trim().Length == 0)
				throw new ArgumentException("DemandType is required", "Demand Type");
			
			this.dmdType = dmdType;
		}

		public Demand(IMap imap, string dmdType) : this(dmdType)
		{
			if(imap == null)
				throw new ArgumentException("IdentityMap is required", "Identity Map");	
			
			imap.add(this);
		}
		public Demand(UOW uow, string dmdType) : this(uow.Imap, dmdType)
		{
			if(uow == null)
				throw new ArgumentException("Unit Of Work is required", "Unit Of Work");

			this.uow = uow;
		}
	#endregion
        
	#region	Methods

		public override void delete()
		{
			if (dmdItems != null)
				for(int i = 0; i < dmdItems.Count; i++)
					((DomainObj)dmdItems[i]).delete();
			dmdItems = null;
			
			if (consumer != null)
				((DomainObj)consumer).delete();
			
			consumer = null;

			base.delete();
		}
		public void AddDmdItem(IDmdItem di)
		{
			if (di.Parent != null)  // only top level needs to be added
				return;

			if (dmdItems == null)
				dmdItems = new ArrayList();

			dmdItems.Add(di);		
		}
		public void ClearDmdItems()
		{
			if (dmdItems == null)
				return;

			for (int i = 0; i < dmdItems.Count; i++)
				((IDmdItem)dmdItems[i]).delete();
			
			dmdItems = null;
		}
		public void AddDmdItem(IDmdItem[] dis)
		{
			for (int i = 0; i < dis.Length; i++)
				AddDmdItem(dis[i]);		
		}
		public IOrderSum OrderSummary(IUOW uow)
		{
			return new OrderSum(this, DmdItems((UOW)uow));
		}
		public IWirelessOrderSum WirelessOrderSum(IUOW uow)
		{
			return new WirelessOrderSum(this, DmdItems((UOW)uow));
		}
		protected override SqlGateway loadSql()
		{
			return new DemandSQL();
		}
		public override void checkExists()
		{
			if ((Id < 1))
				throw new ArgumentException("Valid row is required for update and delete");
		}
		public override	void RefreshForeignKeys()
		{
			if (consId > 0)
				return;

			if (Consumer != null)
				consId = consumer.CustInfoID;
		}
		public override void removeFromIMap(IUOW uow)
		{
			// does not clear dmdItems ArrayList
			IMapObj[] objs = uow.Imap.getObjets();

			for (int i = 0; i < objs.Length; i++)
				if ((objs[i] is IDmdItem) || (objs[i] is IDmdTax)) 
					objs[i].removeFromIMap(uow);
			
			base.removeFromIMap(uow);
		}


	#endregion
        
	#region	Static methods

		public static Demand BuildDmd(UOW uow, IProdPrice[] prods, string zipcode, 
			IILECInfo ilec, string dmdType, OrderType ot)
		{
			Demand dmd = new Demand(uow, dmdType);
			dmd.Loc = Location.find(uow, zipcode).LocId;
			dmd.Ilec = ilec.OrgId;
			dmd.Status = DemandStatus.Pend.ToString();
			
			dmd.Statement = uow.Id; // Linking originated uow to dmd for tax logging

			BuildDIs(uow, dmd, prods, ot);
			return dmd;
		}
		public static Demand BuildDmd(UOW uow, IWireless_Products[] prods, string zipcode, string dmdType)
		{
			Demand dmd = new Demand(uow, dmdType);
			dmd.Loc = Location.find(uow, zipcode).LocId;
			dmd.Status = DemandStatus.Pend.ToString();
			
			dmd.Statement = uow.Id; // Linking originated uow to dmd for tax logging

			BuildDIs(uow, dmd, prods);
			return dmd;
		}

		public static IDemand[] GetForStoreCode(UOW uow, string storeCode, DateTime from, DateTime to)
		{
			Demand[] objs = (Demand[])DomainObj.addToIMap(uow, (new DemandSQL()).getForStoreCode(uow, storeCode, from, to));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;

		}
		static void BuildDIs(UOW uow, Demand dmd, IProdPrice[] prods, OrderType ot)
		{
            IProdPrice[] itms = GetSelected(prods);
				
				if (itms.Length == 0)
					throw new ApplicationException("No selected products in the order");
				
				for (int i = 0; i < itms.Length; i++)
					BuildIt(uow, dmd, itms[i], ot);
			
		}  
		static void BuildDIs(UOW uow, Demand dmd, IWireless_Products[] prods)
		{
			for (int i = 0; i < prods.Length; i++)
				BuildIt(uow, dmd, prods[i]);
			
		} 
		public static DmdItem BuildIt(UOW uow, Demand dmd, IProdPrice pp, OrderType ot)
		{
			if (pp == null)
				throw new ArgumentNullException("ProdPrice is required");

			if (dmd == null)
				throw new ArgumentNullException("Demand is required");

			DmdItem di = new DmdItem(uow, pp, dmd, ot);


			di.AdjustPackage(uow);
			di.CompTaxes(uow);
			dmd.AddDmdItem(di);
			return di;
		}
		public static DmdItem BuildIt(UOW uow, Demand dmd, IWireless_Products wp)
		{
			if (wp == null)
				throw new ArgumentNullException("Wireless Product is required");

			if (dmd == null)
				throw new ArgumentNullException("Demand is required");

			DmdItem di = new DmdItem(uow, wp, dmd);

			di.CompWLTax(uow);
			dmd.AddDmdItem(di);
			
			return di;
		}
		internal static int GetObjs(IMap imap)
		{
			IMapObj[] objs = imap.getObjets();

			if (objs == null)
				return 0;

			if (objs.Length == 0)
				return 0;

			ArrayList ar = new ArrayList(objs.Length);
			for (int i = 0; i < objs.Length; i++)
				if (objs[i] is DmdTax)
					ar.Add(objs[i]);

			DmdTax[] taxes = new DmdTax[ar.Count];
			ar.CopyTo(taxes);

			int j = 0;
			for (int i  = 0; i < taxes.Length; i++)
				j = taxes[i].DmdItemId;

			return taxes.Length;
		}		
		static IProdPrice[] GetSelected(IProdPrice[] prods)
		{
			if (prods == null)
				return new IProdPrice[0];

			if (prods.Length == 0)
				return new IProdPrice[0];

			ArrayList ar = new ArrayList();
			for (int i = 0; i < prods.Length; i++)
				if (prods[i].ProdSelState == ProdSelectionState.Selected)
						ar.Add(prods[i]);

			IProdPrice[] sel = new IProdPrice[ar.Count];
			ar.CopyTo(sel);
			return sel;
		}
		public static Demand find(UOW uow, int id)
		{
			if (uow == null)
				throw new ApplicationException("UOW is required");

			if (uow.Imap.keyExists(Demand.getKey(id)))
				return (Demand)uow.Imap.find(Demand.getKey(id));
            
			Demand cls = new Demand();
			cls.uow = uow;
			cls.id = id;
			cls = (Demand)DomainObj.addToIMap(uow, getOne(((DemandSQL)cls.Sql).getKey(cls)));
			cls.uow = uow;
            
			return cls;
		}
		public static Demand[] GetByBillPayer(UOW uow, int billPayer)
		{
			if (uow == null)
				throw new ApplicationException("UOW is required");

			Demand[] objs = (Demand[])DomainObj.addToIMap(uow, (new DemandSQL()).GetByBillPayer(uow, billPayer));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Demand[] getAll(UOW uow)
		{
			Demand[] objs = (Demand[])DomainObj.addToIMap(uow, (new DemandSQL()).getAll(uow));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Key getKey(int id)
		{
			return new Key(iName, id.ToString());
		}

	#endregion
        
	#region	Implementation
		
		DmdItem[] DmdItems(UOW uow)
		{
			if (dmdItems == null)
				if (id > 0)
				{
					DmdItem[] dis = DmdItem.GetDmdTopOnly(uow, this.id );
					dmdItems = new ArrayList(dis.Length);
					dmdItems.AddRange(dis);
				}

			return DmdItem.ConvertTo(dmdItems);
		}

		static Demand getOne(Demand[] acls)
		{
			if (acls.Length == 1)
				return acls[0];
            
			if (acls.Length == 0)
				throw new ArgumentException("Row not found");
            
			throw new ArgumentException("More than one row found");
		}
		static void copyAttrs(Demand src, Demand tar)
		{
			if (tar == null)
				throw new ArgumentNullException("Target object must not be null");
            
			if (src == null)
				throw new ArgumentNullException("Source object must not be null");
            
			tar.id = src.id;
			tar.dmdType = src.dmdType;
			tar.statement = src.statement;
			tar.consumer = src.consumer;
			tar.consumerAgent = src.consumerAgent;
			tar.loc = src.loc;
			tar.ilec = src.ilec;
			tar.status = src.status;
			tar.isUnderWF = src.isUnderWF;
			tar.workflow = src.workflow;
			tar.wFStep = src.wFStep;
			tar.rowState = src.rowState;
			tar.dmdDate = src.dmdDate;
			tar.statusChangeDate = src.statusChangeDate;
			tar.billPayer = src.billPayer;
			tar.storeCode = src.StoreCode;
			tar.source = src.source;
		}	

	#endregion
        
	#region	SQL
		
		[Serializable]
		class DemandSQL : SqlGateway
		{
			public Demand[] getKey(Demand rec)
			{
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spDemand_Get_Id";
				try
				{
					cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
					return convert(execReader(cmd));
				}
				catch (Exception e)
				{
					string m = e.Message;
					throw e;
				}
			}
			public override void insert(DomainObj obj)
			{
				Demand rec = (Demand)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spDemand_Ins";
				setParam(cmd, rec);
                
				cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
				execScalar(cmd);
				rec.id = (int)cmd.Parameters["@Id"].Value;
				rec.rowState = RowState.Clean;
			}
			public override void delete(DomainObj obj)
			{
				if (((Demand)obj).id < 1)
				{
					((Demand)obj).rowState =  RowState.Deleted;
					return;
				}
				
				SqlCommand cmd = getCommand((Demand)obj);
				cmd.CommandText = "spDemand_Del_Id";
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = ((Demand)obj).id;
                
				execScalar(cmd);
				((Demand)obj).rowState = RowState.Deleted;
			}
			public override void update(DomainObj obj)
			{
				Demand rec = (Demand)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spDemand_Upd";
				setParam(cmd, rec);
                
				execScalar(cmd);
				rec.rowState = RowState.Clean;
			}
			public Demand[] getAll(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spDemand_Get_All";
				return convert(execReader(cmd));
			}
			public Demand[] GetByBillPayer(UOW uow, int billPayer)
			{
				SqlCommand cmd = makeCommand(uow);
							
				cmd.CommandText = "spDemand_Get_By_BillPayer";
				cmd.Parameters.Add("@BillPayer", SqlDbType.Int, 0).Value = billPayer;				
				
				return convert(execReader(cmd));
			}
			public Demand[] getForStoreCode(UOW uow, string storeCode, DateTime from, DateTime to)
			{
				SqlCommand cmd = makeCommand(uow);
							
				cmd.CommandText = "spDemand_Get_Storecode_Dates";
				cmd.Parameters.Add("@StoreCode", SqlDbType.Char, 10).Value = storeCode;				
				cmd.Parameters.Add("@From", SqlDbType.DateTime, 0).Value = from;
				cmd.Parameters.Add("@To", SqlDbType.DateTime, 0).Value = to;
			
				return execReader1(cmd);
			}
		
			#endregion
     
	#region	Implementation

			void setParam(SqlCommand cmd, Demand rec)
			{
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
				if (rec.dmdType == null)
					cmd.Parameters.Add("@DmdType", SqlDbType.VarChar, 25).Value = DBNull.Value;
				else
				{
					if (rec.DmdType.Length == 0)
						cmd.Parameters.Add("@DmdType", SqlDbType.VarChar, 25).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@DmdType", SqlDbType.VarChar, 25).Value = rec.dmdType;
				}
                
				// Numeric, nullable foreign key treatment:
				if (rec.Statement == 0)
					cmd.Parameters.Add("@Statement", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Statement", SqlDbType.Int, 0).Value = rec.statement;
				
				if (rec.consId == 0)
					cmd.Parameters.Add("@Consumer", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Consumer", SqlDbType.Int, 0).Value = rec.consId;

				if (rec.consumerAgent == null)
					cmd.Parameters.Add("@ConsumerAgent", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@ConsumerAgent", SqlDbType.VarChar, 10).Value = rec.consumerAgent;
				
				if (rec.ilec == 0) 
					cmd.Parameters.Add("@Ilec", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Ilec", SqlDbType.Int, 0).Value = rec.ilec;

				if ( rec.loc == 0)
					cmd.Parameters.Add("@Loc", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Loc", SqlDbType.Int, 0).Value = rec.loc;

				if (rec.status == null)
					cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = DBNull.Value;
				else
				{
					if (rec.Status.Length == 0)
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = rec.status;
				}
				cmd.Parameters.Add("@IsUnderWF", SqlDbType.Bit, 0).Value = rec.isUnderWF;
 
				if (rec.workflow == null)
					cmd.Parameters.Add("@Workflow", SqlDbType.VarChar, 25).Value = DBNull.Value;
				else
				{
					if (rec.Workflow.Length == 0)
						cmd.Parameters.Add("@Workflow", SqlDbType.VarChar, 25).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Workflow", SqlDbType.VarChar, 25).Value = rec.workflow;
				}
 
				if (rec.wFStep == null)
					cmd.Parameters.Add("@WFStep", SqlDbType.VarChar, 25).Value = DBNull.Value;
				else
				{
					if (rec.WFStep.Length == 0)
						cmd.Parameters.Add("@WFStep", SqlDbType.VarChar, 25).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@WFStep", SqlDbType.VarChar, 25).Value = rec.wFStep;
				}	
				
				if (rec.dmdDate == DateTime.MinValue)
					cmd.Parameters.Add("@DmdDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@DmdDate", SqlDbType.DateTime, 0).Value = rec.dmdDate;
			
				if (rec.statusChangeDate == DateTime.MinValue)
					cmd.Parameters.Add("@StatusChangeDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@StatusChangeDate", SqlDbType.DateTime, 0).Value = rec.statusChangeDate;
			
				if ( rec.billPayer == 0)
					cmd.Parameters.Add("@BillPayer", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@BillPayer", SqlDbType.Int, 0).Value = rec.billPayer;

				if (rec.storeCode == null)
					cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.StoreCode.Length == 0)
						cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 10).Value = rec.StoreCode;
				}
				
				if ( rec.source == 0)
					cmd.Parameters.Add("@Source", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Source", SqlDbType.Int, 0).Value = rec.source;
			}

			protected override DomainObj reader(SqlDataReader rdr)
			{
				Demand rec = new Demand();
                
				if (rdr["Id"] != DBNull.Value)
					rec.id = (int) rdr["Id"];
 
				if (rdr["DmdType"] != DBNull.Value)
					rec.dmdType = (string) rdr["DmdType"];
 
				if (rdr["Statement"] != DBNull.Value)
					rec.statement = (int) rdr["Statement"];
 
				if (rdr["Consumer"] != DBNull.Value)
					rec.consId = (int) rdr["Consumer"];
 
				if (rdr["ConsumerAgent"] != DBNull.Value)
					rec.consumerAgent = (string) rdr["ConsumerAgent"];

				if (rdr["Ilec"] != DBNull.Value)
					rec.ilec = (int) rdr["Ilec"];
				
				if (rdr["Loc"] != DBNull.Value)
					rec.loc = (int) rdr["Loc"];
 
				if (rdr["Status"] != DBNull.Value)
					rec.status = (string) rdr["Status"];
 
				if (rdr["IsUnderWF"] != DBNull.Value)
					rec.isUnderWF = (bool) rdr["IsUnderWF"];
 
				if (rdr["Workflow"] != DBNull.Value)
					rec.workflow = (string) rdr["Workflow"];
 
				if (rdr["WFStep"] != DBNull.Value)
					rec.wFStep = (string) rdr["WFStep"];
 
				if (rdr["DmdDate"] != DBNull.Value)
					rec.dmdDate = (DateTime)rdr["DmdDate"];

				if (rdr["StatusChangeDate"] != DBNull.Value)
					rec.statusChangeDate = (DateTime)rdr["StatusChangeDate"];

				if (rdr["BillPayer"] != DBNull.Value)
					rec.billPayer = (int) rdr["BillPayer"];

				if (rdr["StoreCode"] != DBNull.Value)
					rec.storeCode = (string) rdr["StoreCode"];
				
				if (rdr["Source"] != DBNull.Value)
					rec.source = (int) rdr["Source"];

				rec.rowState = RowState.Clean;

				return rec;
			}
			Demand[] execReader1(SqlCommand cmd)
			{
				ArrayList ar = new ArrayList();
				SqlDataReader rdr = cmd.ExecuteReader();
			
				try
				{
					while(rdr.Read())
						ar.Add(reader1(rdr));
			
					Demand[] dmds = new Demand[ar.Count];
					ar.CopyTo(dmds);
					return dmds;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
			
					if (cmd.Transaction != null)
						if (cmd.Transaction.Connection != null)
							cmd.Transaction.Rollback();
							
					throw e;
				}
				finally
				{
					if (!rdr.IsClosed)
						rdr.Close();
				}
			}	
			Demand[] convert(DomainObj[] objs)
			{
				Demand[] acls  = new Demand[objs.Length];
				objs.CopyTo(acls, 0);
				return  acls;
			}
			Demand reader1(SqlDataReader rdr)
			{
				Demand rec = new Demand();
                
				if (rdr["Id"] != DBNull.Value)
					rec.id = (int) rdr["Id"];
 
				if (rdr["DmdType"] != DBNull.Value)
					rec.dmdType = (string) rdr["DmdType"];
 
				if (rdr["Statement"] != DBNull.Value)
					rec.statement = (int) rdr["Statement"];
 
				if (rdr["Consumer"] != DBNull.Value)
					rec.consId = (int) rdr["Consumer"];
 
				if (rdr["ConsumerAgent"] != DBNull.Value)
					rec.consumerAgent = (string) rdr["ConsumerAgent"];

				if (rdr["Ilec"] != DBNull.Value)
					rec.ilec = (int) rdr["Ilec"];
				
				if (rdr["Loc"] != DBNull.Value)
					rec.loc = (int) rdr["Loc"];
 
				if (rdr["Status"] != DBNull.Value)
					rec.status = (string) rdr["Status"];
 
				if (rdr["IsUnderWF"] != DBNull.Value)
					rec.isUnderWF = (bool) rdr["IsUnderWF"];
 
				if (rdr["Workflow"] != DBNull.Value)
					rec.workflow = (string) rdr["Workflow"];
 
				if (rdr["WFStep"] != DBNull.Value)
					rec.wFStep = (string) rdr["WFStep"];
 
				if (rdr["DmdDate"] != DBNull.Value)
					rec.dmdDate = (DateTime)rdr["DmdDate"];

				if (rdr["StatusChangeDate"] != DBNull.Value)
					rec.statusChangeDate = (DateTime)rdr["StatusChangeDate"];

				if (rdr["BillPayer"] != DBNull.Value)
					rec.billPayer = (int) rdr["BillPayer"];

				if (rdr["StoreCode"] != DBNull.Value)
					rec.storeCode = (string) rdr["StoreCode"];

				if (rdr["Source"] != DBNull.Value)
					rec.source = (int) rdr["Source"];

				return rec;
			}
		}
	
		#endregion
	}
}