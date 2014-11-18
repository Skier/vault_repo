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
	public class DmdItem : DomainObj, IDmdItem
	{
	#region Data

	//	public static int TaxCnt = 0;

		static string iName = "DmdItem";
		int id;
		string dmdItemType;
		IDemand demand;
		int supplier;
		int dmdId;
		DmdItem parent;
		int parentId;
		int prod;
		int packageId;
		string priceRule;
		decimal priceAmt;
		DateTime startDate;
		DateTime endDate;
		string uOM;
		int qT;
		decimal packageDiscount;
		string status;
		decimal effPrice;
		
		DmdItem[] packageComponents; 
		DmdItem[] tagAlongs; 
		DmdItem   provMapped;  // for Prov via mapping only
		DmdTax[]  taxes;
      
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

		public string DmdItemType 
		{ 
			get { return dmdItemType; }
			set 
			{
				dmdItemType = value;
				setState();
			}
		}
		public int Supplier 
		{ 
			get { return supplier; }
		}
		public IDemand ParDemand
		{
			get 
			{
				if ((demand != null) || (dmdId < 1))
					return demand; 

				if ((uow != null) && (!uow.IsCompleted))
				{
					demand = Demand.find(uow, dmdId);
					return demand;
				}
				
				try
				{ 
					uow = new UOW();
					demand = Demand.find(uow, dmdId);
				}
				finally
				{
					uow.close();
				}
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
				setState();
			}
		}
		public IDmdItem Parent
		{
			get { return parent; }
			set
			{
				parent = (DmdItem)value;
				if (parent != null)
					parentId = parent.id;	
				setState();
			}
		}
		public int ParentId
		{
			get 
			{ 
				if (Parent == null)
					return 0;
				return Parent.Id; 
			}
		}
		public int PackageId 
		{
			get { return packageId; }
			set 
			{ 
				setState();
				packageId = value; 
			}
		}
		public IDmdItem[] Components 
		{ 
			get 
			{ 
				if (packageComponents == null)
					LoadChildrenAndTaxes(this.uow);
			
				return packageComponents; 
			}
		}
		public IDmdItem[] TagAlongs	 
		{ 
			get 
			{ 
				if (tagAlongs == null)
					LoadChildrenAndTaxes(this.uow);

				return tagAlongs; 
			} 
		}
		public int Prod
		{
			get { return prod; }
			set
			{
				setState();
				prod = value;
				this.supplier = ProdInfoCol.GetProd(prod).Supplier;
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
		public string UOM
		{
			get { return uOM; }
			set
			{
				setState();
				uOM = value;
			}
		}
		public int QT
		{
			get { return qT; }
			set
			{
				setState();
				qT = value;
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

		// Prices
		public decimal PriceAmt
		{
			get { return priceAmt; }
			set
			{
				setState();
				priceAmt = decimal.Round(value, 2);
			}
		}
		public decimal PackDiscount { get { return this.packageDiscount; }}
		public decimal EffPrice
		{
			get { return effPrice; }
			set
			{
				setState();
				effPrice = decimal.Round(value, 2);
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
		// Taxes
		public decimal TaxAmt
		{
			get 
			{ 
				decimal tot = decimal.Zero;

				for(int i = 0; i < this.Taxes.Length; i++)
					tot += Taxes[i].TaxAmount;

				return tot;  
			}
		}		

		public decimal PackageTaxAmt
		{
			get
			{
				decimal amount = this.TaxAmt;
				amount += GetTaxes(Components);
				amount += GetTaxes(TagAlongs);
				return decimal.Round(amount, 2);
			}
		}		
		public IDmdTax[] Taxes 
		{ 
			get 
			{  
				if (taxes == null)
					this.LoadChildrenAndTaxes(this.uow);
		
				if (taxes == null)
					return new IDmdTax[0];
 
				return	taxes; 
			}
		}

	#endregion	

	#region Constructors
		
		public DmdItem()
		{
			sql = new DmdItemSQL();
			id = random.Next(Int32.MinValue, -1);
			rowState = RowState.New;
			priority = 13000;
			startDate = DateTime.Now;
			uOM = "Each";
			qT = 1;
		}

		public DmdItem(IMap imap) : this()
		{
			if(imap == null)
				throw new ArgumentException("IdentityMap is required", "Identity Map");
			
			imap.add(this);   
		}
		public DmdItem(UOW uow) : this()
		{
			if(uow == null)
				throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
         			
			if(uow.Imap == null)
				throw new ArgumentException("IdentityMap is required", "Identity Map");
	
			this.uow = uow;
			this.uow.Imap.add(this);   
		}
		public DmdItem(UOW uow, IProdPrice pPrice, IDemand demand, OrderType ot) : this(uow)
		{
			if (pPrice == null)
				throw new ArgumentNullException("ProdPrice");

			if (demand == null)
				throw new ArgumentNullException("Demand");

			this.Setup(pPrice, demand);
			this.SetupItem(uow, ot);
		}
		public DmdItem(UOW uow, IProdPrice pPrice, IDemand demand, DmdItem parent, DItemType itemType, OrderType ot)
			: this(uow)
		{
			if (pPrice == null)
				throw new ArgumentNullException("ProdPrice");

			if (demand == null)
				throw new ArgumentNullException("Demand");

			if (parent == null)
				throw new ArgumentNullException("Parent di");

			this.parent = parent;
			this.dmdItemType = itemType.ToString();
			this.Setup(pPrice, demand);
			this.SetupItem(uow, ot);	
		}
		public DmdItem(UOW uow, IProdPrice pPrice, IDemand demand, DmdItem parent, OrderType ot) 
			: this(uow)
		{
			if (pPrice == null)
				throw new ArgumentNullException("ProdPrice");

			if (demand == null)
				throw new ArgumentNullException("Demand");

			if (parent == null)
				throw new ArgumentNullException("Parent di");

			this.parent = parent;
			this.Setup(pPrice, demand);
			this.SetupItem(uow, OrderType.New);	
		}
		void Setup( IProdPrice pPrice, IDemand demand)
		{
			this.prod        = pPrice.ProdId;
			this.packageId   = pPrice.PackageId;
			this.ParDemand   = demand;
			this.priceRule   = pPrice.PriceRule;
			this.priceAmt    =  this.effPrice = Decimal.Round(pPrice.UnitPrice, 2);	
			this.status      = "Pending";
		}
	#endregion	

	#region Methods
		public override void delete()
		{
			if (packageComponents != null)
				for(int i = 0; i < packageComponents.Length; i++)
					((DomainObj)packageComponents[i]).delete();

			if (tagAlongs != null)
				for(int i = 0; i < tagAlongs.Length; i++)
					((DomainObj)tagAlongs[i]).delete();

			if (taxes != null)
				for(int i = 0; i < taxes.Length; i++)
					((DomainObj)taxes[i]).delete();

			base.delete();
		}
		public IProdPrice[] GetBillable()
		{

			ArrayList ar = new ArrayList();
			ar.Add(new ProdPrice2(this));

			for (int i = 0; i < Components.Length; i++)
				if (ProdInfoCol.GetProd(Components[i].Prod).IsBillable)
					ar.Add(new ProdPrice2(Components[i]));

			for (int i = 0; i < TagAlongs.Length; i++)
				if (ProdInfoCol.GetProd(TagAlongs[i].Prod).IsBillable)
					ar.Add(new ProdPrice2(TagAlongs[i]));

			ProdPrice2[] pp = new ProdPrice2[ar.Count];
			ar.CopyTo(pp);
			return pp;
		}

		public void AdjustPrice(decimal amt)
		{
			this.priceAmt = amt;
			this.priceRule = null;
		}
		public void AdjustPackage(IUOW uow)
		{
			this.packageDiscount = 1m;	
			
			if (this.Components.Length == 0)
				return;

			if ((ProdInfoCol.GetProd(this.prod)).StartServMon > 1) // package does not start month 1 
				return;

			AdjustCompPrice();
		}
		public void RemoveTagAlongs()
		{
			if (tagAlongs == null)
				return;
			
			for (int i = 0; i < tagAlongs.Length; i++)
			{
				if (tagAlongs[i].taxes != null)
					for (int j = 0; j < tagAlongs[i].taxes.Length; j++)
						tagAlongs[i].taxes[j].deleteIt();

				tagAlongs[i].deleteIt();
			}

			this.tagAlongs = null;
		}
		protected override SqlGateway loadSql()
		{
			return new DmdItemSQL();
		}
		public override void checkExists()
		{
			if ((Id < 1))
				throw new ArgumentException("Valid row is required for update and delete");
		}
	
	#endregion	

	#region Static methods

		public static DmdItem find(UOW uow, int id)
		{
			if (uow.Imap.keyExists(DmdItem.getKey(id)))
				return (DmdItem)uow.Imap.find(DmdItem.getKey(id));
            
			DmdItem cls = new DmdItem();
			cls.uow = uow;
			cls.id = id;
			cls = (DmdItem)DomainObj.addToIMap(uow, getOne(((DmdItemSQL)cls.Sql).getKey(cls)));
			cls.uow = uow;
            
			return cls;
		}
		public static DmdItem[] getAll(UOW uow)
		{
			DmdItem[] objs = (DmdItem[])DomainObj.addToIMap(uow, (new DmdItemSQL()).getAll(uow));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Key getKey(int id)
		{
			return new Key(iName, id.ToString());
		}
		public static DmdItem[] GetDmd(UOW uow, int dmd)
		{
			DmdItem[] objs = (DmdItem[])DomainObj.addToIMap(uow, (new DmdItemSQL()).GetDmd(uow, dmd));
			for (int i = 0; i < objs.Length; i++)
			{
				objs[i].uow = uow;
				objs[i].LoadChildrenAndTaxes(uow);
			}
			return objs;
		}
		public static DmdItem[] GetDmdTopOnly(UOW uow, int dmd)
		{
			DmdItem[] objs = (DmdItem[])DomainObj.addToIMap(uow, (new DmdItemSQL()).GetDmdTopOnly(uow, dmd));
			for (int i = 0; i < objs.Length; i++)
			{
				objs[i].uow = uow;
				objs[i].LoadChildrenAndTaxes(uow);
			}
			return objs;
		}

		public static DmdItem[] ConvertTo(ArrayList ar)
		{
			if (ar == null)
				return  new DmdItem[0];

			DmdItem[] items = new DmdItem[ar.Count];
			if (ar.Count > 0)
				ar.CopyTo(items);
			return items;
		}

	
	#endregion	

	#region Implementation
		void SetupItem(IUOW uow, OrderType ot)
		{
			if (ProdInfoCol.GetProd(this.prod).IsBillable)
				this.SetupBillableItem((UOW)uow, ot);
			
			if (ProdInfoCol.GetProd(this.prod).IsProvViaMapping)
				this.SetupMappingProd();
		}
		decimal GetTaxes(IDmdItem[] subs)
		{
			if (subs == null)
				return 0m;

			decimal tax = 0m;    
			for (int i = 0; i < subs.Length; i++)
				tax += subs[i].TaxAmt;

			return decimal.Round(tax, 2);
		}		
		void LoadChildrenAndTaxes(UOW uow)
		{	
			if (this.Parent != null)
				return;

			bool local = false;
			UOW uw = uow;
			
			if (uw == null)
			{
				local = true; 
				uw = new UOW();
			}
			else if (uw.IsCompleted)
			{
				local = true; 
				uw = new UOW(uow.Imap);
			}

			GetTaxes(uw);

			DmdItem[] children = new DmdItemSQL().GetChildren(uw, this);
			
			ArrayList comps = new ArrayList();	
			ArrayList fees  = new ArrayList();
			
			for (int i = 0; i < children.Length; i++)
			{
				children[i].GetTaxes(uow);
				
				if (children[i].dmdItemType.ToLower() == DItemType.PackComp.ToString().ToLower())
					comps.Add(children[i]);

				if (children[i].dmdItemType.ToLower() == DItemType.TagAlong.ToString().ToLower())
					fees.Add(children[i]);

				if (children[i].dmdItemType.ToLower() == DItemType.Mapped.ToString().ToLower())
					this.provMapped = children[i];
			}

			packageComponents = new DmdItem[comps.Count];
			if (comps.Count > 0)
				comps.CopyTo(packageComponents);

			tagAlongs = new DmdItem[fees.Count];
			if (fees.Count > 0)
				fees.CopyTo(tagAlongs);
			
			if (local)
				uw.close();
		}
		void GetTaxes(UOW uow)
		{
			taxes = DmdTax.getDmdItemTaxes(uow, this.id);
		}
		void SetupBillableItem(UOW uow, OrderType ot)
		{	

			if (this.packageId > 0)   // nothing doing for package components 
				return;

			if (this.parent != null)  // nothing doing for fees either
				return;

			this.BuildSubs((UOW)uow, ot);		
			this.GetFees(uow, ot);
		}
//		void CheckTaxCount()
//		{
//			int imapCnt = Demand.GetObjs(uow.Imap);
//			if ( imapCnt == DmdItem.TaxCnt)
//				return;
//
//			string err = "Product: " + this.Prod 
//				+ ", Tax count:  " +   DmdItem.TaxCnt.ToString() 
//				+ ", imap count: " + imapCnt.ToString(); 
//
//			err += "";
//		}

		public void CompTaxes(UOW uow)
		{
			CompTax(uow, this); // itself
			
			if (this.parent != null) // nothing for components
				return;

			if (this.packageId != 0) // nothing for fees
				return;

			DoTaxes(uow, GetEligibleComps(this.packageComponents));
			DoTaxes(uow, GetEligibleComps(this.tagAlongs));
		}
		void DoTaxes(UOW uow, DmdItem[] itms)
		{
			for (int i = 0; i < itms.Length; i++)
			{
				if (itms[i].effPrice == 0m)
				{
					itms[i].taxes = new DmdTax[0];
					continue;
				}
				CompTax(uow, itms[i]); 
			}
		}
		void CompTax(UOW uow, DmdItem di)
		{
			di.taxes = DmdTax.ConvDomObj(uow, di, new EzTaxProxy().GetTaxProxy().ComputeTax(di.Prod, di.effPrice, Zipcode(uow), DateTime.Now)); 
		}
		DmdTax[] CombineTaxes(DmdTax[] prev, DmdTax[] curr)
		{
			if (prev == null)
				return curr;
			
			if (prev.Length == 0)
				return curr;

			if (curr == null)
				return prev;

			if (curr.Length == 0)
				return prev;

			DmdTax[] res = new DmdTax[prev.Length + curr.Length];
			if (res.Length > 0)
				prev.CopyTo(res, 0);
	
			if (curr.Length > 0)
				curr.CopyTo(res, prev.Length);
			
			return res;
		}
		string Zipcode(UOW uow) // must be a zip or country when zip is not in the Location table
		{
			Location loc = Location.find(uow, GetDemand().Loc);

			if (loc.LocType == "Zip")
				return loc.Name;

			if (loc.LocType == "Country")
				return loc.Name;

			throw new ApplicationException("Valid ZIP or country is required for tax calculations");
		}
		int Zip()
		{
			return GetDemand().Loc;
		}
		void GetDmdTaxes(UOW uow)
		{
			if (Id > 0)
				taxes = DmdTax.getDmdItemTaxes(uow, DmdId);
		}
		Demand GetDemand()
		{
			if (ParDemand == null)
				ParDemand = this.Parent.ParDemand;
			
			if (ParDemand == null)
				throw new ArgumentNullException("Demand");
	
			if (ParDemand.Loc < 1)
				throw new  ApplicationException("Demand loc must be a positive number");
			
			return (Demand)ParDemand;
		}
//		void ValidDemandException()
//		{	
//			throw new ApplicationException(
//				"A demand with valid zipcode is required for tax calculations");
//		}

		public override	void RefreshForeignKeys()
		{
			if (dmdId < 0)
				dmdId = ParDemand.Id;
			
			int level = 0;		
			DmdItem next = null;
			DmdItem current = this;

			while(true)
			{
				next = (DmdItem)current.Parent;
				if (next == null)
					break;
				level++;

				if (level > 10)
					throw new ApplicationException("Endless loop through DmdItem parent"); 
				
				current = next;
			}

			priority +=	level * 10;
			if (Parent == null)
				return;

			parentId = Parent.Id;
		}
		void AdjustCompPrice()
		{	
			this.packageDiscount = 1m;
			
			if (this.priceAmt == decimal.Zero)
				return;
			
			DmdItem[] itms = GetEligibleComps(this.packageComponents);
			CalcPackageDiscount(itms);
			Allocate(itms);
		}
	
		DmdItem[] GetEligibleComps(DmdItem[] ditms)
		{
			if (ditms == null)
				return new DmdItem[0];

			ArrayList ar = new ArrayList();

			for (int i = 0; i < ditms.Length; i++)
				if (ditms[i].priceAmt != decimal.Zero)
					ar.Add(ditms[i]);

			DmdItem[] itms = new DmdItem[ar.Count];
			if (ar.Count > 0)
				ar.CopyTo(itms);
	
			return itms;
		}	

		void CalcPackageDiscount(DmdItem[] itms)
		{
			decimal total = decimal.Zero;
			for (int i = 0; i < itms.Length; i++)
				total += itms[i].PriceAmt;
	
			if (total == decimal.Zero)
				return;

			this.packageDiscount  = decimal.Round( this.PriceAmt / total , 4); 
		}
		void Allocate(DmdItem[] itms)
		{
			if (this.packageDiscount == 1m) 
				return;

			int[] ratios = new int[itms.Length];
			for (int i = 0; i < ratios.Length; i++)
				ratios[i] = (int)(itms[i].priceAmt * 100 * this.packageDiscount);
			
			decimal[] effPrices = Money2.Allocate(PriceAmt, ratios);
			for (int i = 0; i < effPrices.Length; i++)
				itms[i].effPrice = effPrices[i];	
		}

		void BuildSubs(UOW uow, OrderType ot) // package components
		{
			if (this.ParDemand == null)
				throw new ApplicationException("Demand is required");

			if (this.packageId > 0) // no comp for comps
				return;

			if (this.parent != null) // no comp for fees
				return;

			ProdComposition[] prods = ProdInfoCol.getAllComps(this.prod, ProdComposition.COMP);
			this.packageComponents = new DmdItem[prods.Length];
			
			for(int i = 0; i < prods.Length; i++)
			{
				ProdPrice2 pp2 = 
					ProdPrice2.GetProdPrice(uow, prods[i].SubProd, ParDemand.Loc, ParDemand.Ilec);

				if (pp2 == null)
					throw new ApplicationException("Price for product " 
						                         + prods[i].SubProd.ToString()
						                         + ", location " 
						                         +  ParDemand.Loc.ToString() 
						                         + ", ILEC " 
						                         + ParDemand.Ilec.ToString() 
						                         + " is not found");
						
				pp2.PackageId = this.prod;
				string s = (ProdInfoCol.GetProd(pp2.ProdId)).ProdName;
				DmdItem di =  new DmdItem(uow, pp2, this.demand, this, DItemType.PackComp, ot);
				this.packageComponents[i] = di;
			}
		}
		void GetFees(UOW uow, OrderType ot)
		{
			IProdPrice[] fees 
				= ProdPrice2.GetFees(uow, prod, ParDemand.Loc, ParDemand.Ilec, ot); //OrderType.New);

			this.tagAlongs = new DmdItem[fees.Length];

			for (int i = 0; i < this.tagAlongs.Length; i++)
				this.tagAlongs[i] = new DmdItem(uow, fees[i], demand, this, DItemType.TagAlong, ot);
		}
		void AddToTaxes(DmdTax[] dtaxes)
		{
			if (dtaxes ==null)
				throw new ArgumentNullException("dtaxes");

			if (taxes ==null)
			{
				taxes = dtaxes;
				return;
			}
			DmdTax[] dt = new DmdTax[taxes.Length + dtaxes.Length];
	
			if (taxes.Length > 0)
				taxes.CopyTo(dt, 0);
	
			if (dtaxes.Length > 0)
				dtaxes.CopyTo(dt, taxes.Length);
			
			taxes = dt;
		}
		static DmdItem getOne(DmdItem[] acls)
		{
			if (acls.Length == 1)
				return acls[0];
            
			if (acls.Length == 0)
				throw new ArgumentException("Row not found");
            
			throw new ArgumentException("More than one row found");
		}
		static void copyAttrs(DmdItem src, DmdItem tar)
		{
			if (tar == null)
				throw new ArgumentNullException("Target object must not be null");
            
			if (src == null)
				throw new ArgumentNullException("Source object must not be null");
            
			tar.id          = src.id;
			tar.dmdItemType = src.dmdItemType;
			tar.demand      = src.demand;
			tar.supplier    = src.supplier;
			tar.parent      = src.parent;
			tar.prod        = src.prod;

			tar.priceRule   = src.priceRule;
			tar.priceAmt    = src.priceAmt;
			tar.effPrice    = src.effPrice;
			tar.startDate   = src.startDate;
			tar.endDate     = src.endDate;
			tar.uOM         = src.uOM;
			tar.qT          = src.qT;
			tar.status      = src.status;
			tar.rowState    = src.rowState;
		}
		void SetupMappingProd()
		{

			if (!ProdInfoCol.GetProd(this.prod).IsProvViaMapping)
				return;
			
			ProdComposition[] mapped = ProdInfoCol.getAllComps(this.prod, "Map");
			
			for (int i = 0; i < mapped.Length; i++)
				if ( ProdInfoCol.GetProd(mapped[i].SubProd).Supplier == demand.Ilec)
				{
					BuildMapped(mapped[i].SubProd);
					return;
				}
			
			throw new ApplicationException(string.Format("No mapping product found for product {0} and ilec {1}",
				this.prod.ToString(), demand.Ilec.ToString()));
		}
		void BuildMapped(int mappedProd)
		{
			provMapped = new DmdItem(uow);
	

			provMapped.dmdItemType = DItemType.Mapped.ToString();
			provMapped.demand      = this.demand;
			provMapped.supplier    = this.demand.Ilec;
			provMapped.parent      = this;
			provMapped.prod		   = mappedProd;
					
			provMapped.startDate   = this.startDate;
			provMapped.endDate     = this.endDate;
			provMapped.uOM         = this.uOM;
			provMapped.qT          = this.qT;
			provMapped.status      = this.status;
		}

		#endregion	

	#region SQL
		
		[Serializable]
			class DmdItemSQL : SqlGateway
		{
			public DmdItem[] getKey(DmdItem rec)
			{
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spDmdItem_Get_Id";
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
				return convert(execReader(cmd));
			}

			public override void insert(DomainObj obj)
			{
				DmdItem rec = (DmdItem)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spDmdItem_Ins";
		
				rec.RefreshForeignKeys();           				

				setParam(cmd, rec);
				cmd.Parameters["@Id"].Direction = ParameterDirection.Output;

				execScalar(cmd);
				
				rec.id = (int)cmd.Parameters["@Id"].Value;
				rec.rowState = RowState.Clean;
			}
			public override void delete(DomainObj obj)
			{
				DmdItem rec = (DmdItem)obj;
				if (rec.id < 1)
				{
					rec.rowState = RowState.Deleted;
					return;
				}
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spDmdItem_Del_Id";
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
				execScalar(cmd);
				rec.rowState = RowState.Deleted;
			}
			public override void update(DomainObj obj)
			{
				DmdItem rec = (DmdItem)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spDmdItem_Upd";
				setParam(cmd, rec);
                
				execScalar(cmd);
				rec.rowState = RowState.Clean;
			}
			public DmdItem[] GetDmd(UOW uow, int dmd)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spDmdItem_Get_Dmd";
				cmd.Parameters.Add("@Dmd", SqlDbType.Int, 0).Value = dmd;
				return convert(execReader(cmd));
			}
			public DmdItem[] GetDmdTopOnly(UOW uow, int dmd)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spDmdItem_Get_DmdTop";
				cmd.Parameters.Add("@Dmd", SqlDbType.Int, 0).Value = dmd;
				return convert(execReader(cmd));
			}

			public DmdItem[] GetChildren(UOW uow, DmdItem rec)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spDmdItem_Get_Children";
				cmd.Parameters.Add("@Parent", SqlDbType.Int, 0).Value = rec.id;
				return convert(execReader(cmd));
			}
			public DmdItem[] getAll(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spDmdItem_Get_All";
				return convert(execReader(cmd));
			}
		
	#endregion	

	#region SQL Implementation
			
			void setParam(SqlCommand cmd, DmdItem rec)
			{
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
				if (rec.dmdItemType == null)
					cmd.Parameters.Add("@DmdItemType", SqlDbType.VarChar, 25).Value = DBNull.Value;
				else
				{
					if (rec.DmdItemType.Length == 0)
						cmd.Parameters.Add("@DmdItemType", SqlDbType.VarChar, 25).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@DmdItemType", SqlDbType.VarChar, 25).Value = rec.dmdItemType;
				}
                
				// Numeric, nullable foreign key treatment:
				if (rec.dmdId == 0)
					cmd.Parameters.Add("@Demand", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Demand", SqlDbType.Int, 0).Value = rec.dmdId;

				if (rec.supplier == 0)
					cmd.Parameters.Add("@Supplier", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Supplier", SqlDbType.Int, 0).Value = rec.supplier;

				if (rec.Parent == null)
					cmd.Parameters.Add("@ParentDI", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@ParentDI", SqlDbType.Int, 0).Value = rec.Parent.Id;
				
				if (rec.Prod == 0)
					cmd.Parameters.Add("@Prod", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Prod", SqlDbType.Int, 0).Value = rec.prod;

				if (rec.PackageId == 0)
					cmd.Parameters.Add("@Package", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Package", SqlDbType.Int, 0).Value = rec.PackageId;

				if (rec.priceRule == null)
					cmd.Parameters.Add("@PriceRule", SqlDbType.VarChar, 15).Value = DBNull.Value;
				else
				{
					if (rec.PriceRule.Length == 0)
						cmd.Parameters.Add("@PriceRule", SqlDbType.VarChar, 15).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@PriceRule", SqlDbType.VarChar, 15).Value = rec.priceRule;
				}

				cmd.Parameters.Add("@PriceAmt", SqlDbType.Money, 0).Value = Decimal.Round(rec.priceAmt, 2);
				cmd.Parameters.Add("@EffPrice", SqlDbType.Money, 0).Value = Decimal.Round(rec.effPrice, 2);
				
				if (rec.startDate == DateTime.MinValue)
					cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = rec.startDate;
 
				if (rec.endDate == DateTime.MinValue)
					cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = rec.endDate;
 
				if (rec.uOM == null)
					cmd.Parameters.Add("@UOM", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.UOM.Length == 0)
						cmd.Parameters.Add("@UOM", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@UOM", SqlDbType.VarChar, 10).Value = rec.uOM;
				}
				cmd.Parameters.Add("@QT", SqlDbType.Int, 0).Value = rec.qT;
 
				if (rec.status == null)
					cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.Status.Length == 0)
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = rec.status;
				}

			}
			protected override DomainObj reader(SqlDataReader rdr)
			{
				DmdItem rec = new DmdItem();
                
				if (rdr["Id"] != DBNull.Value)
					rec.id = (int) rdr["Id"];
 
				if (rdr["DmdItemType"] != DBNull.Value)
					rec.dmdItemType = (string) rdr["DmdItemType"];
 
				if (rdr["Demand"] != DBNull.Value)
					rec.dmdId = (int) rdr["Demand"];

				if (rdr["Supplier"] != DBNull.Value)
					rec.supplier = (int) rdr["Supplier"];
 
				if (rdr["ParentDI"] != DBNull.Value)
					rec.parentId = (int) rdr["ParentDI"];
 
				if (rdr["Prod"] != DBNull.Value)
					rec.prod = (int) rdr["Prod"];
 
				if (rdr["Package"] != DBNull.Value)
					rec.packageId = (int)rdr["Package"];

				if (rdr["PriceRule"] != DBNull.Value)
					rec.priceRule = (string) rdr["PriceRule"];

				if (rdr["PriceAmt"] != DBNull.Value)
					rec.priceAmt = Decimal.Round((decimal) rdr["PriceAmt"], 2);

				if (rdr["EffPrice"] != DBNull.Value)
					rec.effPrice = Decimal.Round((decimal) rdr["EffPrice"], 2);

				if (rdr["StartDate"] != DBNull.Value)
					rec.startDate = (DateTime) rdr["StartDate"];
 
				if (rdr["EndDate"] != DBNull.Value)
					rec.endDate = (DateTime) rdr["EndDate"];
 
				if (rdr["UOM"] != DBNull.Value)
					rec.uOM = (string) rdr["UOM"];
 
				if (rdr["QT"] != DBNull.Value)
					rec.qT = (int) rdr["QT"];
 
				if (rdr["Status"] != DBNull.Value)
					rec.status = (string) rdr["Status"];
				
				rec.rowState = RowState.Clean;
				return rec;
			}
			DmdItem[] convert(DomainObj[] objs)
			{
				DmdItem[] acls  = new DmdItem[objs.Length];
				if (objs.Length > 0) 
					objs.CopyTo(acls, 0);
		
				return  acls;
			}
		}
	#endregion	
	}
}