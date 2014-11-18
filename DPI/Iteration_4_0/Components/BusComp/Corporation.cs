using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using DPI.Interfaces;
 
namespace DPI.Components
{
	[Serializable]
	public class Corporation : DomainObj, ICorporation
	{
	#region Data
		static string iName = "Corporation";

		int corpID;
		int parentId;  
		int defaultDebCardProd;
		
		string name;
		string address;
		string city;
		string st;
		
		string zip;
		string fax;
		string phone;
		string contact;
	
		string webOrderingTrainingPage;
		string pymtTypeRule;
		string defRestProd;
		
		DateTime date_Created;

		bool rAC_WF;		
		bool requestClerkId;
        bool skipStoreStats;

		bool usePapentForStoreStats;
		bool remLeadZerosFromStoreNum;		
		bool isPymtPostReq;
		bool allowLocalConv;
		bool isDpiWirelessTax;
		bool isDpiEngInstantPay;
		
	#endregion
	
	#region Properties
		public bool RequestClerkId
		{
			get { return requestClerkId; }
			set
			{
				setState();
				requestClerkId = value;
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
		public string Fax
		{
			get { return fax; }
			set
			{
				setState();
				fax = value;
			}
		}
		public string Contact
		{
			get { return contact; }
			set
			{
				setState();
				contact = value;
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
		public override IDomKey IKey 
		{
			get { return new Key(iName, corpID.ToString()); }
		}
		public int CorpID
		{
			get { return corpID; }
		}
		public DateTime Date_Created
		{
			get { return date_Created; }
			set
			{
				setState();
				date_Created = value;
			}
		}
		public bool RAC_WF 
		{ 
			get { return rAC_WF; }
			set 
			{
				setState();
				rAC_WF = value;
			}
		}

		public int ParentId
		{
			get	{ return parentId;	}
			set	
			{ 
				setState();
				parentId = value;
			}
		}

		public bool SkipStoreStats
		{
			get { return skipStoreStats; }
			set
			{
				setState();
				skipStoreStats = value;
			}
		}
		public bool UsePapentForStoreStats
		{
			get { return usePapentForStoreStats;	}
			set
			{
				setState();
				usePapentForStoreStats = value;
			}
		}
		public string WebOrderingTrainingPage
		{
			get { return webOrderingTrainingPage; }
			set { webOrderingTrainingPage = value; }
		}
		public int DefaultDebCardProd
		{
			get { return defaultDebCardProd; }
		}
		public bool IsPymtPostReq
		{
			get { return isPymtPostReq; }
			set
			{
				setState();
				isPymtPostReq = value;
			}
		}
		public string PymtTypeRule { get { return pymtTypeRule; } }
		public bool RemLeadZerosFromStoreNum { get { return remLeadZerosFromStoreNum; }}
		public string DefaulRestProdRule { get { return defRestProd; }}
		public bool AllowLocalConv { get { return allowLocalConv; }}
		public bool IsDpiWirelessTax { get { return isDpiWirelessTax; }}
		public bool IsDpiEngInstantPay { get { return isDpiEngInstantPay; }}

	#endregion

	#region Constructors
        public Corporation()
        {
            sql = new CorporationSQL();
            corpID = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public Corporation(UOW uow) : this()
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
            return new CorporationSQL();
        }
        public override void checkExists()
        {
            if ((CorpID < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
		#endregion

	#region Static methods
		public static bool IsRentway(string storeCode)
		{
			return StoreStatsCol.GetCorporation(storeCode).CorpID == 27;
		}
        public static Corporation find(UOW uow, int corpID)
        {
            if (uow.Imap.keyExists(Corporation.getKey(corpID)))
                return (Corporation)uow.Imap.find(Corporation.getKey(corpID));
            
            Corporation cls = new Corporation();
            cls.uow = uow;
            cls.corpID = corpID;
            cls = (Corporation)DomainObj.addToIMap(uow, getOne(((CorporationSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static Corporation[] getAll(UOW uow)
        {
            Corporation[] objs = (Corporation[])DomainObj.addToIMap(uow, (new CorporationSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int corpID)
        {
            return new Key(iName, corpID.ToString());
        }
		#endregion

	#region Implementation
        static Corporation getOne(Corporation[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Corporation src, Corporation tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.name = src.name;
            tar.address = src.address;
            tar.city = src.city;
            tar.st = src.st;
            tar.zip = src.zip;
            tar.fax = src.fax;
            tar.contact = src.contact;
            tar.phone = src.phone;
            tar.corpID = src.corpID;
            tar.date_Created = src.date_Created;
            tar.rowState = src.rowState;
			tar.rAC_WF = src.rAC_WF;
			tar.requestClerkId = src.requestClerkId;
			tar.webOrderingTrainingPage = src.webOrderingTrainingPage;
			tar.skipStoreStats = src.skipStoreStats;
			tar.usePapentForStoreStats = src.usePapentForStoreStats;
			tar.defaultDebCardProd = src.defaultDebCardProd;
			tar.isPymtPostReq = src.isPymtPostReq;
			tar.pymtTypeRule = src.pymtTypeRule;
			tar.remLeadZerosFromStoreNum = src.remLeadZerosFromStoreNum;
			tar.defRestProd = src.defRestProd;
			tar.allowLocalConv = src.allowLocalConv;
			tar.isDpiWirelessTax = src.isDpiWirelessTax;
			tar.isDpiEngInstantPay = src.isDpiEngInstantPay;
        }
 
		#endregion

	#region SQL
        [Serializable]
        class CorporationSQL : SqlGateway
        {
            public Corporation[] getKey(Corporation rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCorporation_Get_Id2";
                cmd.Parameters.Add("@CorpID", SqlDbType.Int, 0).Value = rec.corpID;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                Corporation rec = (Corporation)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCorporation_Ins2";
                setParam(cmd, rec);
                
                cmd.Parameters["@CorpID"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.corpID = (int)cmd.Parameters["@CorpID"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Corporation rec = (Corporation)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCorporation_Del_Id";
                cmd.Parameters.Add("@CorpID", SqlDbType.Int, 0).Value = rec.corpID;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Corporation rec = (Corporation)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCorporation_Upd2";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public Corporation[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spCorporation_Get_All2";
                return convert(execReader(cmd));
            }
			#endregion
	
	#region Implementation
            void setParam(SqlCommand cmd, Corporation rec)
            {
 
                if (rec.name == null)
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.Name.Length == 0)
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = rec.name;
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
                    cmd.Parameters.Add("@State", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.st.Length == 0)
                        cmd.Parameters.Add("@State", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@State", SqlDbType.VarChar, 50).Value = rec.st;
                }
 
                if (rec.zip == null)
                    cmd.Parameters.Add("@Zip", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.Zip.Length == 0)
                        cmd.Parameters.Add("@Zip", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Zip", SqlDbType.VarChar, 50).Value = rec.zip;
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
 
                if (rec.contact == null)
                    cmd.Parameters.Add("@Contact", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.Contact.Length == 0)
                        cmd.Parameters.Add("@Contact", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Contact", SqlDbType.VarChar, 50).Value = rec.contact;
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
                cmd.Parameters.Add("@CorpID", SqlDbType.Int, 0).Value = rec.corpID;
 
                if (rec.date_Created == DateTime.MinValue)
                    cmd.Parameters.Add("@Date_Created", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Date_Created", SqlDbType.DateTime, 0).Value = rec.date_Created;

				cmd.Parameters.Add("@RAC_WF", SqlDbType.Bit, 0).Value = rec.rAC_WF;

				cmd.Parameters.Add("@ParentId", SqlDbType.Int, 0).Value = rec.parentId;

				cmd.Parameters.Add("@SkipStoreStats", SqlDbType.Bit, 1).Value = rec.skipStoreStats;

				cmd.Parameters.Add("@UsePapentForStoreStats", SqlDbType.Bit, 1).Value = rec.usePapentForStoreStats;
				
				cmd.Parameters.Add("@RequestClerkId", SqlDbType.Bit, 1).Value = rec.requestClerkId;
				 
				if (rec.webOrderingTrainingPage == null)
					cmd.Parameters.Add("@WebOrderingTrainingPage", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.webOrderingTrainingPage.Length == 0)
						cmd.Parameters.Add("@WebOrderingTrainingPage", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@WebOrderingTrainingPage", SqlDbType.VarChar, 50).Value = rec.WebOrderingTrainingPage;
				}
				cmd.Parameters.Add("@IsPymtPostReq", SqlDbType.Bit, 1).Value = rec.isPymtPostReq;
				
				if (rec.pymtTypeRule == null)
					cmd.Parameters.Add("@PymtTypeRule", SqlDbType.VarChar, 25).Value = DBNull.Value;
				else
				{
					if (rec.pymtTypeRule.Length == 0)
						cmd.Parameters.Add("@PymtTypeRule", SqlDbType.VarChar, 25).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@PymtTypeRule", SqlDbType.VarChar, 25).Value = rec.pymtTypeRule;
				}
				cmd.Parameters.Add("@RemLeadZerosFromStoreNum", SqlDbType.Bit, 1).Value = rec.remLeadZerosFromStoreNum;


				if (rec.defRestProd == null)
					cmd.Parameters.Add("@DefRestProd", SqlDbType.VarChar, 25).Value = DBNull.Value;
				else
				{
					if (rec.defRestProd.Length == 0)
						cmd.Parameters.Add("@DefRestProd", SqlDbType.VarChar, 25).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@DefRestProd", SqlDbType.VarChar, 25).Value = rec.defRestProd;
				}
				cmd.Parameters.Add("@AllowLocalConv", SqlDbType.Bit, 1).Value = rec.allowLocalConv;
				cmd.Parameters.Add("@IsDpiWirelessTax", SqlDbType.Bit, 1).Value = rec.isDpiWirelessTax;
                //Removed to allow using old DB			    
				//cmd.Parameters.Add("@IsDpiEngInstantPay", SqlDbType.Bit, 1).Value = rec.isDpiEngInstantPay;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                Corporation rec = new Corporation();
                
                if (rdr["Name"] != DBNull.Value)
                    rec.name = (string) rdr["Name"];
 
                if (rdr["Address"] != DBNull.Value)
                    rec.address = (string) rdr["Address"];
 
                if (rdr["City"] != DBNull.Value)
                    rec.city = (string) rdr["City"];
 
                if (rdr["State"] != DBNull.Value)
                    rec.st = (string) rdr["State"];
 
                if (rdr["Zip"] != DBNull.Value)
                    rec.zip = (string) rdr["Zip"];
 
                if (rdr["Fax"] != DBNull.Value)
                    rec.fax = (string) rdr["Fax"];
 
                if (rdr["Contact"] != DBNull.Value)
                    rec.contact = (string) rdr["Contact"];
 
                if (rdr["Phone"] != DBNull.Value)
                    rec.phone = (string) rdr["Phone"];
 
                if (rdr["CorpID"] != DBNull.Value)
                    rec.corpID = (int) rdr["CorpID"];
 
                if (rdr["Date_Created"] != DBNull.Value)
                    rec.date_Created = (DateTime) rdr["Date_Created"];

				if (rdr["RAC_WF"] != DBNull.Value)
					rec.rAC_WF = (bool) rdr["RAC_WF"];

				if (rdr["ParentId"] != DBNull.Value)
					rec.parentId = (int) rdr["ParentId"];

				if (rdr["SkipStoreStats"] != DBNull.Value)
					rec.skipStoreStats = (bool) rdr["SkipStoreStats"];

				if (rdr["UsePapentForStoreStats"] != DBNull.Value)
					rec.usePapentForStoreStats = (bool) rdr["UsePapentForStoreStats"];

				if (rdr["RequestClerkId"] != DBNull.Value)
					rec.requestClerkId = (bool) rdr["RequestClerkId"];
 
				if (rdr["WebOrderingTrainingPage"] != DBNull.Value)
					rec.webOrderingTrainingPage = (string) rdr["WebOrderingTrainingPage"];

				if (rdr["DefaultDebCardProd"] != DBNull.Value)
					rec.defaultDebCardProd = (int) rdr["DefaultDebCardProd"];

				if (rdr["IsPymtPostReq"] != DBNull.Value)
					rec.isPymtPostReq = (bool) rdr["IsPymtPostReq"];

				if (rdr["PymtTypeRule"] != DBNull.Value)
					rec.pymtTypeRule = (string) rdr["PymtTypeRule"];

				if (rdr["RemLeadZerosFromStoreNum"] != DBNull.Value)
					rec.remLeadZerosFromStoreNum = (bool) rdr["RemLeadZerosFromStoreNum"];

				if (rdr["DefRestProd"] != DBNull.Value)
					rec.defRestProd = (string) rdr["DefRestProd"];

				if (rdr["AllowLocalConv"] != DBNull.Value)
					rec.allowLocalConv = (bool) rdr["AllowLocalConv"];

				if (rdr["IsDpiWirelessTax"] != DBNull.Value)
					rec.isDpiWirelessTax = (bool) rdr["IsDpiWirelessTax"];

//Removed to allow using old DB			                    
//				if (rdr["IsDpiEngInstantPay"] != DBNull.Value)
//					rec.isDpiEngInstantPay = (bool) rdr["IsDpiEngInstantPay"];

                rec.rowState = RowState.Clean;
                return rec;
            }
            Corporation[] convert(DomainObj[] objs)
            {
                Corporation[] acls  = new Corporation[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
		}		
	#endregion
	}
}
