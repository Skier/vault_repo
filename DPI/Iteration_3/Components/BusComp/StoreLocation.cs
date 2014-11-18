using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]
	public class StoreLocation : DomainObj, IStoreLocation
	{
        #region Constants

        public const int STATE_LENGTH = 2;
        public const int CITY_MAX_LENGTH = 50;

        private const string ERR_LENGTH_MSG = "{0} parameter must be {1} characters in length.";

	    #endregion

	    #region Data
		static string iName = "StoreLocation";
		string storeCode;
		string storeClass;
		string name;
		string storeNumber;
		string address;
		string city;
		string st;
		string zip;
		string phone;
		string fax;
		string manager;
		bool active;
		DateTime activeDate;
		string priceCode;
		string wireless_PriceCode;
		string notes;
		string addLocInf;
		DateTime termDate;
		string status;
		string ilec;
		int dMA;
		int corpID;
		string type;
		int internet_Channel_ID;
		bool localService;
		bool wireless;
		bool internet;
		bool smartConnect;
		decimal nET_FlatRate;
		decimal sC_FlatRate;
		decimal lS_FlatRate;
		string divisional_Manager;
		int overrideDebCardProd;
		bool debitCard;
		string restProd;
		bool isNarrowPrinter;
		bool satellite;
		bool showSource;
		bool dpiWireless;
		#endregion 
        
		#region Properties
		public override IDomKey IKey 
		{
			get { return new Key(iName, storeCode); }
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
		public string StoreClass
		{
			get { return storeClass; }
			set
			{
				setState();
				storeClass = value;
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
		public string StoreNumber
		{
			get { return storeNumber; }
			set
			{
				setState();
				storeNumber = value;
			}
		}
		public string Address
		{
			get { return address; }
			set
			{
				setState();
				address = value;
			}
		}
		public string City
		{
			get { return city; }
			set
			{
				setState();
				city = value;
			}
		}
		public string St
		{
			get { return st; }
			set
			{
				setState();
				st = value;
			}
		}
		public string Zip
		{
			get { return zip; }
			set
			{
				setState();
				zip = value;
			}
		}
		public string Phone
		{
			get { return phone; }
			set
			{
				setState();
				phone = value;
			}
		}
		public string Fax
		{
			get { return fax; }
			set
			{
				setState();
				fax = value;
			}
		}
		public string Manager
		{
			get { return manager; }
			set
			{
				setState();
				manager = value;
			}
		}
		public bool Active
		{
			get { return active; }
			set
			{
				setState();
				active = value;
			}
		}
		public DateTime ActiveDate
		{
			get { return activeDate; }
			set
			{
				setState();
				activeDate = value;
			}
		}
		public string PriceCode
		{
			get { return priceCode; }
			set
			{
				setState();
				priceCode = value;
			}
		}
		public string Wireless_PriceCode
		{
			get { return wireless_PriceCode; }
			set
			{
				setState();
				wireless_PriceCode = value;
			}
		}
		public string Notes
		{
			get { return notes; }
			set
			{
				setState();
				notes = value;
			}
		}
		public string AddLocInf
		{
			get { return addLocInf; }
			set
			{
				setState();
				addLocInf = value;
			}
		}
		public DateTime TermDate
		{
			get { return termDate; }
			set
			{
				setState();
				termDate = value;
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
		public string Ilec
		{
			get { return ilec; }
			set
			{
				setState();
				ilec = value;
			}
		}
		public int DMA
		{
			get { return dMA; }
			set
			{
				setState();
				dMA = value;
			}
		}
		public int CorpID
		{
			get { return corpID; }
			set
			{
				setState();
				corpID = value;
			}
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
		public int Internet_Channel_ID
		{
			get { return internet_Channel_ID; }
			set
			{
				setState();
				internet_Channel_ID = value;
			}
		}
		public bool LocalService
		{
			get { return localService; }
			set
			{
				setState();
				localService = value;
			}
		}
		public bool Wireless
		{
			get { return wireless; }
			set
			{
				setState();
				wireless = value;
			}
		}
		public bool Internet
		{
			get { return internet; }
			set
			{
				setState();
				internet = value;
			}
		}
		public bool SmartConnect
		{
			get { return smartConnect; }
			set
			{
				setState();
				smartConnect = value;
			}
		}
		public decimal NET_FlatRate
		{
			get { return nET_FlatRate; }
			set
			{
				setState();
				nET_FlatRate = Decimal.Round(value, 2);
			}
		}
		public decimal SC_FlatRate
		{
			get { return sC_FlatRate; }
			set
			{
				setState();
				sC_FlatRate = Decimal.Round(value, 2);
			}
		}
		public decimal LS_FlatRate
		{
			get { return lS_FlatRate; }
			set
			{
				setState();
				lS_FlatRate = Decimal.Round(value, 2);
			}
		}
		public string Divisional_Manager
		{
			get { return divisional_Manager; }
			set
			{
				setState();
				divisional_Manager = value;
			}
		}
		public int OverrideDebCardProd { get { return overrideDebCardProd; }}
		public bool DebitCard {	get { return debitCard; }}
		public string RestProdRule { get { return restProd; }} 
		public bool IsNarrowPrinter {	get { return isNarrowPrinter; }}	
		public bool Satellite {	get { return satellite; }}
		public bool ShowSource { get { return showSource; }}
		public bool DpiWireless { get { return dpiWireless; }}

		#endregion
		
		#region Constructors
        public StoreLocation()
        {
            sql = new StoreLocationSQL();
            rowState = RowState.New;
        }
        public StoreLocation(UOW uow) : this()
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
            return new StoreLocationSQL();
        }
        public override void checkExists()
        {
            if ((StoreCode ==  null))
                throw new ArgumentException("Valid row is required for update and delete");
        }
		#endregion
		
		#region	Static methods
        public static StoreLocation find(UOW uow, string storeCode)
        {
            if (uow.Imap.keyExists(StoreLocation.getKey(storeCode)))
                return (StoreLocation)uow.Imap.find(StoreLocation.getKey(storeCode));
            
            StoreLocation cls = new StoreLocation();
            cls.uow = uow;
            cls.storeCode = storeCode;
            cls = (StoreLocation)DomainObj.addToIMap(uow, getOne(((StoreLocationSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
		public static StoreLocation find(UOW uow, int corpID, string storeNumber)
		{
			StoreLocation cls = new StoreLocation();
			cls.uow = uow;
			cls.corpID = corpID;
			cls.storeNumber = storeNumber;
			cls = getOne(((StoreLocationSQL)cls.Sql).getCorpIDStoreNumber(cls));

			if (!uow.Imap.keyExists(StoreLocation.getKey(cls.storeCode)))
				cls = (StoreLocation)DomainObj.addToIMap(uow, cls);
				//cls = (StoreLocation)DomainObj.addToIMap(uow, getOne(((StoreLocationSQL)cls.Sql).getKey(cls)));
				
			return cls;
		}

        public static StoreLocation[] getAll(UOW uow)
        {
            StoreLocation[] objs = (StoreLocation[])DomainObj.addToIMap(
                uow, (new StoreLocationSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }

        /// <summary>
        /// Retreive all states codes where stores are located.
        /// </summary>
        /// <param name="uow">See <see cref="UOW"/> class.</param>
        /// <returns>Collection of state codes where stores are located.</returns>
        public static StringCollection getStates(UOW uow) 
        {
            StoreLocationSQL ds = new StoreLocationSQL();
            StringCollection states = ds.getStates(uow);

            return states;
        }

        /// <summary>
        /// Retreive all cities names in the specified state 
        /// where stores are located.
        /// </summary>
        /// <param name="uow">See <see cref="UOW"/> class.</param>
        /// <param name="state">State code the cities names are retrieved for.</param>
        /// <returns>Collection of cities names in the specified state 
        /// where stores are located.</returns>
        public static StringCollection getCitiesByState(UOW uow, string state)
        {
            if (state == null) {
                throw new ArgumentNullException("state");
            }

            if (state.Length != STATE_LENGTH) {
                throw new ArgumentException(string.Format(
                    ERR_LENGTH_MSG, "state", STATE_LENGTH), "state");
            }

            StoreLocationSQL ds = new StoreLocationSQL();
            StringCollection cities = ds.getCitiesByState(uow, state);

            return cities;
        }

        /// <summary>
        /// Retreive all store locations in the specified state and city.
        /// </summary>
        /// <param name="uow">See <see cref="UOW"/> class.</param>
        /// <param name="state">State code the store locations are retrieved for.</param>
        /// <param name="city">City name the store locations are retrieved for.</param>
        /// <returns>Array of store locations in the specified state and city.</returns>
        public static StoreLocation[] getAllByStateAndCity(UOW uow, string state, string city)
        {
            if (state == null) {
                throw new ArgumentNullException("state");
            }

            if (state.Length != STATE_LENGTH) {
                throw new ArgumentException(string.Format(
                    ERR_LENGTH_MSG, "state", STATE_LENGTH), "state");
            }

            if (city == null || city == string.Empty) {
                throw new ArgumentNullException("city");
            }

            if (city.Length > CITY_MAX_LENGTH) {
                throw new ArgumentException(string.Format(
                    ERR_LENGTH_MSG, "city", CITY_MAX_LENGTH), "city");
            }

            StoreLocationSQL ds = new StoreLocationSQL();
            StoreLocation[] locations = ds.getAllByStateAndCity(uow, state, city);
            locations = (StoreLocation[])DomainObj.addToIMap(uow, locations);

            return locations;
        }

        public static Key getKey(string storeCode)
        {
            return new Key(iName, storeCode.ToString());
        }
		public static int GetDebitCardProd(UOW uow, string storeCode)
		{
			StoreLocation sl = find(uow, storeCode);

			if (sl.overrideDebCardProd != 0)
				return sl.overrideDebCardProd;
			
			ICorporation corp = Corporation.find(uow, sl.CorpID);
 
			if (corp.DefaultDebCardProd != 0)
				return corp.DefaultDebCardProd;
			
			//check whether parent corporation has the default debit card
			if (corp.ParentId == 0)
				return 0;

			ICorporation pcorp = Corporation.find(uow, corp.ParentId);
			if (pcorp.DefaultDebCardProd != 0)
				return pcorp.DefaultDebCardProd;
			
			//return 0 if no DefaultDebCardProd
            return 0;
		}
		#endregion

		#region Implementation
        static StoreLocation getOne(StoreLocation[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("StoreLocation is not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(StoreLocation src, StoreLocation tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.storeCode = src.storeCode;
            tar.storeClass = src.storeClass;
            tar.name = src.name;
            tar.storeNumber = src.storeNumber;
            tar.address = src.address;
            tar.city = src.city;
            tar.st = src.st;
            tar.zip = src.zip;
            tar.phone = src.phone;
            tar.fax = src.fax;
            tar.manager = src.manager;
            tar.active = src.active;
            tar.activeDate = src.activeDate;
            tar.priceCode = src.priceCode;
            tar.wireless_PriceCode = src.wireless_PriceCode;
            tar.notes = src.notes;
            tar.addLocInf = src.addLocInf;
            tar.termDate = src.termDate;
            tar.status = src.status;
            tar.ilec = src.ilec;
            tar.dMA = src.dMA;
            tar.corpID = src.corpID;
            tar.type = src.type;
            tar.internet_Channel_ID = src.internet_Channel_ID;
            tar.localService = src.localService;
            tar.wireless = src.wireless;
            tar.internet = src.internet;
            tar.smartConnect = src.smartConnect;
            tar.nET_FlatRate = src.nET_FlatRate;
            tar.sC_FlatRate = src.sC_FlatRate;
            tar.lS_FlatRate = src.lS_FlatRate;
            tar.divisional_Manager = src.divisional_Manager;
			tar.overrideDebCardProd = src.overrideDebCardProd;
			tar.debitCard = src.debitCard;
			tar.restProd = src.restProd;
			tar.isNarrowPrinter = src.isNarrowPrinter;
			tar.satellite = src.satellite;
			tar.showSource = src.showSource;
			tar.dpiWireless = src.dpiWireless;

			tar.rowState = src.rowState;
        }
		#endregion
		
		#region SQL
        [Serializable]
        class StoreLocationSQL : SqlGateway
        {
            bool _gettingStates = false;
            bool _gettingCities = false;

            public StoreLocation[] getKey(StoreLocation rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spStoreLocation_Get_Id";
                cmd.Parameters.Add("@StoreCode", SqlDbType.Char, 10).Value = rec.storeCode;
                return convert(execReader(cmd));
            }
			public StoreLocation[] getCorpIDStoreNumber(StoreLocation rec)
			{
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spStoreLocation_Get_CorpIDStoreNumber";
				cmd.Parameters.Add("@CorpID", SqlDbType.Int, 0).Value = rec.corpID;
				cmd.Parameters.Add("@StoreNumber", SqlDbType.VarChar, 10).Value = rec.storeNumber;
				return convert(execReader(cmd));
			}
            public override void insert(DomainObj obj)
            {
                StoreLocation rec = (StoreLocation)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spStoreLocation_Ins";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                StoreLocation rec = (StoreLocation)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spStoreLocation_Del_Id";
                cmd.Parameters.Add("@StoreCode", SqlDbType.Char, 10).Value = rec.storeCode;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                StoreLocation rec = (StoreLocation)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spStoreLocation_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public StoreLocation[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spStoreLocation_Get_All";
                return convert(execReader(cmd));
            }

            /// <summary>
            /// Retreive all states codes where stores are located.
            /// </summary>
            /// <param name="uow">See <see cref="UOW"/> class.</param>
            /// <returns>Collection of state codes where stores are located.</returns>
            public StringCollection getStates(UOW uow)
            {
                _gettingStates = true;

                try {
                    SqlCommand cmd = makeCommand(uow);
                    cmd.CommandText = "spStoreLocation_Get_States";
                    
                    // Retrieve all states from DB. Be awere that the 
                    // result objects have just St property filled.
                    StoreLocation[] locations = convert(execReader(cmd));

                    // Fill result collection with state codes.
                    StringCollection states = new StringCollection();
                    foreach (StoreLocation location in locations) {
                        if (location.St != null) {
                            states.Add(location.St);
                        }
                    }

                    return states;
                } finally {
                    _gettingStates = false;
                }
            }

            /// <summary>
            /// Retreive all cities names in the specified state 
            /// where stores are located.
            /// </summary>
            /// <param name="uow">See <see cref="UOW"/> class.</param>
            /// <param name="state">State code the cities names are retrieved for.</param>
            /// <returns>Collection of cities names in the specified state 
            /// where stores are located.</returns>
            public StringCollection getCitiesByState(UOW uow, string state)
            {
                if (state == null) {
                    throw new ArgumentNullException("state");
                }

                if (state.Length != STATE_LENGTH) {
                    throw new ArgumentException(string.Format(
                        ERR_LENGTH_MSG, "state", STATE_LENGTH), "state");
                }

                _gettingCities = true;

                try {
                    SqlCommand cmd = makeCommand(uow);
                    cmd.CommandText = "spStoreLocation_Get_CitiesByState";
                    cmd.Parameters.Add("@State", SqlDbType.Char, STATE_LENGTH).Value = state;

                    // Retrieve all cities from DB. Be awere that the 
                    // result objects have just City property filled.
                    StoreLocation[] locations = convert(execReader(cmd));

                    // Fill result collection with cities names.
                    StringCollection cities = new StringCollection();
                    foreach (StoreLocation location in locations) {
                        if (location.City != null) {
                            cities.Add(location.City);
                        }
                    }

                    return cities;
                } finally {
                    _gettingCities = false;
                }
            }

            /// <summary>
            /// Retreive all store locations in the specified state and city.
            /// </summary>
            /// <param name="uow">See <see cref="UOW"/> class.</param>
            /// <param name="state">State code the store locations are retrieved for.</param>
            /// <param name="city">City name the store locations are retrieved for.</param>
            /// <returns>Array of store locations in the specified state and city.</returns>
            public StoreLocation[] getAllByStateAndCity(UOW uow, string state, string city)
            {
                if (state == null) {
                    throw new ArgumentNullException("state");
                }

                if (state.Length != STATE_LENGTH) {
                    throw new ArgumentException(string.Format(
                        ERR_LENGTH_MSG, "state", STATE_LENGTH), "state");
                }

                if (city == null || city == string.Empty) {
                    throw new ArgumentNullException("city");
                }

                if (city.Length > CITY_MAX_LENGTH) {
                    throw new ArgumentException(string.Format(
                        ERR_LENGTH_MSG, "city", CITY_MAX_LENGTH), "city");
                }

                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spStoreLocation_Get_AllByStateAndCity";
                cmd.Parameters.Add("@State", SqlDbType.Char, STATE_LENGTH).Value = state;
                cmd.Parameters.Add("@City", SqlDbType.VarChar, CITY_MAX_LENGTH).Value = city;

                // Retrieve all cities from DB. Be awere that the 
                // result objects have just City property filled.
                StoreLocation[] locations = convert(execReader(cmd));

                return locations;
            }

			#endregion
		
			#region Implementation
            void setParam(SqlCommand cmd, StoreLocation rec)
            {
 
                cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 10).Value = rec.storeCode;
 
                if (rec.storeClass == null)
                    cmd.Parameters.Add("@StoreClass", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.StoreClass.Length == 0)
                        cmd.Parameters.Add("@StoreClass", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@StoreClass", SqlDbType.VarChar, 10).Value = rec.storeClass;
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
 
                if (rec.storeNumber == null)
                    cmd.Parameters.Add("@StoreNumber", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.StoreNumber.Length == 0)
                        cmd.Parameters.Add("@StoreNumber", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@StoreNumber", SqlDbType.VarChar, 10).Value = rec.storeNumber;
                }
 
                if (rec.address == null)
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.Address.Length == 0)
                        cmd.Parameters.Add("@Address", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Address", SqlDbType.VarChar, 50).Value = rec.address;
                }
 
                if (rec.city == null)
                    cmd.Parameters.Add("@City", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.City.Length == 0)
                        cmd.Parameters.Add("@City", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@City", SqlDbType.VarChar, 50).Value = rec.city;
                }
 
                if (rec.st == null)
                    cmd.Parameters.Add("@State", SqlDbType.VarChar, 2).Value = DBNull.Value;
                else
                {
                    if (rec.st.Length == 0)
                        cmd.Parameters.Add("@State", SqlDbType.VarChar, 2).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@State", SqlDbType.VarChar, 2).Value = rec.st;
                }
 
                if (rec.zip == null)
                    cmd.Parameters.Add("@Zip", SqlDbType.VarChar, 5).Value = DBNull.Value;
                else
                {
                    if (rec.Zip.Length == 0)
                        cmd.Parameters.Add("@Zip", SqlDbType.VarChar, 5).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Zip", SqlDbType.VarChar, 5).Value = rec.zip;
                }
 
                if (rec.phone == null)
                    cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.Phone.Length == 0)
                        cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 10).Value = rec.phone;
                }
 
                if (rec.fax == null)
                    cmd.Parameters.Add("@Fax", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.Fax.Length == 0)
                        cmd.Parameters.Add("@Fax", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Fax", SqlDbType.VarChar, 10).Value = rec.fax;
                }
 
                if (rec.manager == null)
                    cmd.Parameters.Add("@Manager", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.Manager.Length == 0)
                        cmd.Parameters.Add("@Manager", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Manager", SqlDbType.VarChar, 50).Value = rec.manager;
                }
 
                cmd.Parameters.Add("@Active", SqlDbType.Bit, 0).Value = rec.active;
 
                if (rec.activeDate == DateTime.MinValue)
                    cmd.Parameters.Add("@ActiveDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@ActiveDate", SqlDbType.DateTime, 0).Value = rec.activeDate;
 
                if (rec.priceCode == null)
                    cmd.Parameters.Add("@PriceCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.PriceCode.Length == 0)
                        cmd.Parameters.Add("@PriceCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@PriceCode", SqlDbType.VarChar, 10).Value = rec.priceCode;
                }
 
                if (rec.wireless_PriceCode == null)
                    cmd.Parameters.Add("@Wireless_PriceCode", SqlDbType.VarChar, 7).Value = DBNull.Value;
                else
                {
                    if (rec.Wireless_PriceCode.Length == 0)
                        cmd.Parameters.Add("@Wireless_PriceCode", SqlDbType.VarChar, 7).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Wireless_PriceCode", SqlDbType.VarChar, 7).Value = rec.wireless_PriceCode;
                }
 
                if (rec.notes == null)
                    cmd.Parameters.Add("@Notes", SqlDbType.VarChar, 0).Value = DBNull.Value;
                else
                {
                    if (rec.Notes.Length == 0)
                        cmd.Parameters.Add("@Notes", SqlDbType.VarChar, 0).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Notes", SqlDbType.VarChar, 0).Value = rec.notes;
                }
 
                if (rec.addLocInf == null)
                    cmd.Parameters.Add("@AddLocInf", SqlDbType.VarChar, 0).Value = DBNull.Value;
                else
                {
                    if (rec.AddLocInf.Length == 0)
                        cmd.Parameters.Add("@AddLocInf", SqlDbType.VarChar, 0).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AddLocInf", SqlDbType.VarChar, 0).Value = rec.addLocInf;
                }
 
                if (rec.termDate == DateTime.MinValue)
                    cmd.Parameters.Add("@TermDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@TermDate", SqlDbType.DateTime, 0).Value = rec.termDate;
 
                cmd.Parameters.Add("@Status", SqlDbType.VarChar, 5).Value = rec.status;
 
                cmd.Parameters.Add("@Ilec", SqlDbType.VarChar, 3).Value = rec.ilec;
                cmd.Parameters.Add("@DMA", SqlDbType.Int, 0).Value = rec.dMA;
                
                // Numeric, nullable foreign key treatment:
                if (rec.CorpID == 0)
                    cmd.Parameters.Add("@CorpID", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@CorpID", SqlDbType.Int, 0).Value = rec.corpID;
 
                cmd.Parameters.Add("@Type", SqlDbType.VarChar, 1).Value = rec.type;
                cmd.Parameters.Add("@Internet_Channel_ID", SqlDbType.Int, 0).Value = rec.internet_Channel_ID;
 
                cmd.Parameters.Add("@LocalService", SqlDbType.Bit, 0).Value = rec.localService;
                cmd.Parameters.Add("@Wireless", SqlDbType.Bit, 0).Value = rec.wireless;
                cmd.Parameters.Add("@Internet", SqlDbType.Bit, 0).Value = rec.internet;
 
                cmd.Parameters.Add("@SmartConnect", SqlDbType.Bit, 0).Value = rec.smartConnect;
                cmd.Parameters.Add("@NET_FlatRate", SqlDbType.Decimal, 0).Value = rec.nET_FlatRate;
                cmd.Parameters.Add("@SC_FlatRate", SqlDbType.Decimal, 0).Value = rec.sC_FlatRate;
                cmd.Parameters.Add("@LS_FlatRate", SqlDbType.Decimal, 0).Value = rec.lS_FlatRate;
 
                if (rec.divisional_Manager == null)
                    cmd.Parameters.Add("@Divisional_Manager", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.Divisional_Manager.Length == 0)
                        cmd.Parameters.Add("@Divisional_Manager", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Divisional_Manager", SqlDbType.VarChar, 50).Value = rec.divisional_Manager;
                }

				if (rec.overrideDebCardProd == 0)
					cmd.Parameters.Add("@OverrideDebCardProd", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@OverrideDebCardProd", SqlDbType.Int, 0).Value = rec.overrideDebCardProd;

				cmd.Parameters.Add("@DebitCard", SqlDbType.Bit, 0).Value = rec.debitCard;

				if (rec.restProd == null)
					cmd.Parameters.Add("@OvrdRestProd", SqlDbType.VarChar, 0).Value = DBNull.Value;
				else
				{
					if (rec.restProd.Length == 0)
						cmd.Parameters.Add("@OvrdRestProd", SqlDbType.VarChar, 0).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@OvrdRestProd", SqlDbType.VarChar, 0).Value = rec.restProd;
				}
				cmd.Parameters.Add("@IsNarrowPrinter", SqlDbType.Bit, 0).Value = rec.isNarrowPrinter;
				cmd.Parameters.Add("@Satellite", SqlDbType.Bit, 0).Value = rec.satellite;
				cmd.Parameters.Add("@ShowSource", SqlDbType.Bit, 0).Value = rec.showSource;
				cmd.Parameters.Add("@DpiWireless", SqlDbType.Bit, 0).Value = rec.dpiWireless;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                StoreLocation rec = new StoreLocation();

                if (!_gettingCities) {
                    if (rdr["State"] != DBNull.Value)
                        rec.st = (string) rdr["State"];
                }

                if (!_gettingStates) {
                    if (rdr["City"] != DBNull.Value)
                        rec.city = (string) rdr["City"];
                }
                
                if (!_gettingStates && !_gettingCities) {
                    if (rdr["StoreCode"] != DBNull.Value)
                        rec.storeCode = (string) rdr["StoreCode"];
 
                    if (rdr["StoreClass"] != DBNull.Value)
                        rec.storeClass = (string) rdr["StoreClass"];
 
                    if (rdr["Name"] != DBNull.Value)
                        rec.name = (string) rdr["Name"];
 
                    if (rdr["StoreNumber"] != DBNull.Value)
                        rec.storeNumber = (string) rdr["StoreNumber"];
 
                    if (rdr["Address"] != DBNull.Value)
                        rec.address = (string) rdr["Address"];
 
                    if (rdr["Zip"] != DBNull.Value)
                        rec.zip = (string) rdr["Zip"];
 
                    if (rdr["Phone"] != DBNull.Value)
                        rec.phone = (string) rdr["Phone"];
 
                    if (rdr["Fax"] != DBNull.Value)
                        rec.fax = (string) rdr["Fax"];
 
                    if (rdr["Manager"] != DBNull.Value)
                        rec.manager = (string) rdr["Manager"];
 
                    if (rdr["Active"] != DBNull.Value)
                        rec.active = (bool) rdr["Active"];
 
                    if (rdr["ActiveDate"] != DBNull.Value)
                        rec.activeDate = (DateTime) rdr["ActiveDate"];
 
                    if (rdr["PriceCode"] != DBNull.Value)
                        rec.priceCode = (string) rdr["PriceCode"];
 
                    if (rdr["Wireless_PriceCode"] != DBNull.Value)
                        rec.wireless_PriceCode = (string) rdr["Wireless_PriceCode"];
 
                    if (rdr["Notes"] != DBNull.Value)
                        rec.notes = (string) rdr["Notes"];
 
                    if (rdr["AddLocInf"] != DBNull.Value)
                        rec.addLocInf = (string) rdr["AddLocInf"];
 
                    if (rdr["TermDate"] != DBNull.Value)
                        rec.termDate = (DateTime) rdr["TermDate"];
 
                    if (rdr["Status"] != DBNull.Value)
                        rec.status = (string) rdr["Status"];
 
                    if (rdr["Ilec"] != DBNull.Value)
                        rec.ilec = (string) rdr["Ilec"];
 
                    if (rdr["DMA"] != DBNull.Value)
                        rec.dMA = (int) rdr["DMA"];
 
                    if (rdr["CorpID"] != DBNull.Value)
                        rec.corpID = (int) rdr["CorpID"];
 
                    if (rdr["Type"] != DBNull.Value)
                        rec.type = (string) rdr["Type"];
 
                    if (rdr["Internet_Channel_ID"] != DBNull.Value)
                        rec.internet_Channel_ID = (int) rdr["Internet_Channel_ID"];
 
                    if (rdr["LocalService"] != DBNull.Value)
                        rec.localService = (bool) rdr["LocalService"];
 
                    if (rdr["Wireless"] != DBNull.Value)
                        rec.wireless = (bool) rdr["Wireless"];
 
                    if (rdr["Internet"] != DBNull.Value)
                        rec.internet = (bool) rdr["Internet"];
 
                    if (rdr["SmartConnect"] != DBNull.Value)
                        rec.smartConnect = (bool) rdr["SmartConnect"];
 
                    if (rdr["NET_FlatRate"] != DBNull.Value)
                        rec.nET_FlatRate = Decimal.Round((decimal)rdr["NET_FlatRate"], 2);
 
                    if (rdr["SC_FlatRate"] != DBNull.Value)
                        rec.sC_FlatRate = Decimal.Round((decimal)rdr["SC_FlatRate"], 2);
 
                    if (rdr["LS_FlatRate"] != DBNull.Value)
                        rec.lS_FlatRate = Decimal.Round((decimal)rdr["LS_FlatRate"], 2);
 
                    if (rdr["Divisional_Manager"] != DBNull.Value)
                        rec.divisional_Manager = (string) rdr["Divisional_Manager"];

                    if (rdr["OverrideDebCardProd"] != DBNull.Value)
                        rec.overrideDebCardProd = (int) rdr["OverrideDebCardProd"];

                    if (rdr["DebitCard"] != DBNull.Value)
                        rec.debitCard = (bool) rdr["DebitCard"];

                    if (rdr["OvrdRestProd"] != DBNull.Value)
                        rec.restProd = (string) rdr["OvrdRestProd"];

                    if (rdr["IsNarrowPrinter"] != DBNull.Value)
                        rec.isNarrowPrinter = (bool) rdr["IsNarrowPrinter"];

                    if (rdr["Satellite"] != DBNull.Value)
                        rec.satellite = (bool) rdr["Satellite"];
				
                    if (rdr["ShowSource"] != DBNull.Value)
                        rec.showSource = (bool) rdr["ShowSource"];

                    if (rdr["DpiWireless"] != DBNull.Value)
                        rec.dpiWireless = (bool) rdr["DpiWireless"];
                }
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            StoreLocation[] convert(DomainObj[] objs)
            {
                StoreLocation[] acls  = new StoreLocation[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
			#endregion
        }
    }
}
