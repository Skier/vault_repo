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
	public class Energy_CustData : DomainObj, IEnergy_CustData
	{
		#region Data
		static string iName = "Energy_CustData";
		int iD;
		string siteCode;
		string quoteId;
		int accountNumber;
		string pin;
		string address1;
		string address2;
		string city;
		string state;
		string zip;
		string zip4;
		string nameFirst;
		string nameLast;
		string nameMiddle;
		string ph1;
		string ph2;
		string email;
		string fax;
		string preferedContactMethod;
		string ssn;
		string dL;
		string dlState;
		DateTime dOB;
		string permitName;
		string customerNumberRef;
		string doingBusAs;
		bool specialNeedsReq;
		bool lowIncomeCustomer;
		string language;
		string status;
		DateTime dateInserted;
		DateTime dateModified;
		//Service address and StartDate will not be saved in Energy_Custdata. It will be saved in EnergyEnrollment
		string		sAddress1;
		string		sAddress2;
		string		sCity;
		string		sState;
		string		sZip;
		string		sZip4;
		DateTime	serviceStartDate;
		#endregion
        
		#region Properties
		public override IDomKey IKey 
		{
			get { return new Key(iName, iD.ToString()); }
		}
		public int ID
		{
			get { return iD; }
		}
		public string SiteCode
		{
			get { return siteCode; }
			set
			{
				setState();
				siteCode = value;
			}
		}
		public string QuoteId
		{
			get { return quoteId; }
			set
			{
				setState();
				quoteId = value;
			}
		}
		public int AccountNumber
		{
			get { return accountNumber; }
			set
			{
				setState();
				accountNumber = value;
			}
		}
		public string Pin
		{
			get { return pin; }
			set
			{
				setState();
				pin = value;
			}
		}
		public string Address1
		{
			get { return address1; }
			set
			{
				setState();
				address1 = value;
			}
		}
		public string Address2
		{
			get { return address2; }
			set
			{
				setState();
				address2 = value;
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
		public string State
		{
			get { return state; }
			set
			{
				setState();
				state = value;
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
		public string Zip4
		{
			get { return zip4; }
			set
			{
				setState();
				zip4 = value;
			}
		}
		public string NameFirst
		{
			get { return nameFirst; }
			set
			{
				setState();
				nameFirst = value;
			}
		}
		public string NameLast
		{
			get { return nameLast; }
			set
			{
				setState();
				nameLast = value;
			}
		}
		public string NameMiddle
		{
			get { return nameMiddle; }
			set
			{
				setState();
				nameMiddle = value;
			}
		}
		public string Ph1
		{
			get { return ph1; }
			set
			{
				setState();
				ph1 = value;
			}
		}
		public string Ph2
		{
			get { return ph2; }
			set
			{
				setState();
				ph2 = value;
			}
		}
		public string Email
		{
			get { return email; }
			set
			{
				setState();
				email = value;
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
		public string PreferedContactMethod
		{
			get { return preferedContactMethod; }
			set
			{
				setState();
				preferedContactMethod = value;
			}
		}
		public string Ssn
		{
			get { return ssn; }
			set
			{
				setState();
				ssn = value;
			}
		}
		public string DL
		{
			get { return dL; }
			set
			{
				setState();
				dL = value;
			}
		}
		public string DlState
		{
			get { return dlState; }
			set
			{
				setState();
				dlState = value;
			}
		}
		public DateTime DOB
		{
			get { return dOB; }
			set
			{
				setState();
				dOB = value;
			}
		}
		public string PermitName
		{
			get { return permitName; }
			set
			{
				setState();
				permitName = value;
			}
		}
		public string CustomerNumberRef
		{
			get { return customerNumberRef; }
			set
			{
				setState();
				customerNumberRef = value;
			}
		}
		public string DoingBusAs
		{
			get { return doingBusAs; }
			set
			{
				setState();
				doingBusAs = value;
			}
		}
		public bool SpecialNeedsReq
		{
			get { return specialNeedsReq; }
			set
			{
				setState();
				specialNeedsReq = value;
			}
		}
		public bool LowIncomeCustomer
		{
			get { return lowIncomeCustomer; }
			set
			{
				setState();
				lowIncomeCustomer = value;
			}
		}
		public string Language
		{
			get { return language; }
			set
			{
				setState();
				language = value;
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
		public DateTime DateInserted
		{
			get { return dateInserted; }
			set
			{
				setState();
				dateInserted = value;
			}
		}
		public DateTime DateModified
		{
			get { return dateModified; }
			set
			{
				setState();
				dateModified = value;
			}
		}
		public string SAddress1
		{
			get { return sAddress1; }
			set
			{
				setState();
				sAddress1 = value;
			}
		}
		public string SAddress2
		{
			get { return sAddress2; }
			set
			{
				setState();
				sAddress2 = value;
			}
		}
		public string SCity
		{
			get { return sCity; }
			set
			{
				setState();
				sCity = value;
			}
		}
		public string SState
		{
			get { return sState; }
			set
			{
				setState();
				sState = value;
			}
		}
		public string SZip
		{
			get { return sZip; }
			set
			{
				setState();
				sZip = value;
			}
		}
		public string SZip4
		{
			get { return sZip4; }
			set
			{
				setState();
				sZip4 = value;
			}
		}		
		public DateTime	ServiceStartDate
		{ 
			get { return serviceStartDate; }
			set
			{
				setState();
				serviceStartDate = value;
			}
		}
		public string FullMailingAddress
		{
			get { return GetFullMailingAddress(); }
		}
		public string FullServiceAddress
		{
			get { return GetFullServiceAddress(); }
		}

		#endregion
        
		#region Constructors
		public Energy_CustData()
		{
			sql = new Energy_CustDataSQL();
			iD = random.Next(Int32.MinValue, -1);
			rowState = RowState.New;
		}
		public Energy_CustData(UOW uow) : this()
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
			return new Energy_CustDataSQL();
		}
		public override void checkExists()
		{
			if ((ID < 1))
				throw new ArgumentException("Valid row is required for update and delete");
		}
		private string GetFullMailingAddress()
		{
			StringBuilder sb = new StringBuilder();
			
			sb.Append(this.address1);
			
			if (this.address2 != null)
				sb.Append(" " + this.address2);
			
			sb.Append(", " + this.city);
			sb.Append(", " + this.state);
			sb.Append(", " + this.zip);

			if (this.zip4 != null)
				sb.Append("-" + this.zip4);
			
			return sb.ToString();
		}
		private string GetFullServiceAddress()
		{
			StringBuilder sb = new StringBuilder();
			
			sb.Append(this.sAddress1);
			
			if (this.sAddress2 != null)
				sb.Append(" " + this.sAddress2);
			
			sb.Append(", " + this.sCity);
			sb.Append(", " + this.sState);
			sb.Append(", " + this.sZip);

			if (this.sZip4 != null)
				sb.Append("-" + this.sZip4);
			
			return sb.ToString();
		}
		#endregion

		#region	Static methods
		public static Energy_CustData find(UOW uow, int iD)
		{
			if (uow.Imap.keyExists(Energy_CustData.getKey(iD)))
				return (Energy_CustData)uow.Imap.find(Energy_CustData.getKey(iD));
            
			Energy_CustData cls = new Energy_CustData();
			cls.uow = uow;
			cls.iD = iD;
			cls = (Energy_CustData)DomainObj.addToIMap(uow, getOne(((Energy_CustDataSQL)cls.Sql).getKey(cls)));
			cls.uow = uow;
            
			return cls;
		}
		public static Energy_CustData[] getAll(UOW uow)
		{
			Energy_CustData[] objs = (Energy_CustData[])DomainObj.addToIMap(uow, (new Energy_CustDataSQL()).getAll(uow));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Energy_CustData[] SearchCustomer(UOW uow, string phNumber, string lastName, string address, string city, string state, string zip, int accNumber)
		{
			Energy_CustData[] objs = (Energy_CustData[])DomainObj.addToIMap(uow, (new Energy_CustDataSQL()).SearchCustomer(uow, phNumber, lastName, address, city, state, zip, accNumber));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Key getKey(int iD)
		{
			return new Key(iName, iD.ToString());
		}
		#endregion

		#region Implementation
		static Energy_CustData getOne(Energy_CustData[] acls)
		{
			if (acls.Length == 1)
				return acls[0];
            
			if (acls.Length == 0)
				throw new ArgumentException("Row not found");
            
			throw new ArgumentException("More than one row found");
		}
		static void copyAttrs(Energy_CustData src, Energy_CustData tar)
		{
			if (tar == null)
				throw new ArgumentNullException("Target object must not be null");
            
			if (src == null)
				throw new ArgumentNullException("Source object must not be null");
            
			tar.iD = src.iD;
			tar.siteCode = src.siteCode;
			tar.quoteId = src.quoteId;
			tar.accountNumber = src.accountNumber;
			tar.pin = src.pin;
			tar.address1 = src.address1;
			tar.address2 = src.address2;
			tar.city = src.city;
			tar.state = src.state;
			tar.zip = src.zip;
			tar.zip4 = src.zip4;
			tar.nameFirst = src.nameFirst;
			tar.nameLast = src.nameLast;
			tar.nameMiddle = src.nameMiddle;
			tar.ph1 = src.ph1;
			tar.ph2 = src.ph2;
			tar.email = src.email;
			tar.fax = src.fax;
			tar.preferedContactMethod = src.preferedContactMethod;
			tar.ssn = src.ssn;
			tar.dL = src.dL;
			tar.dlState = src.dlState;
			tar.dOB = src.dOB;
			tar.permitName = src.permitName;
			tar.customerNumberRef = src.customerNumberRef;
			tar.doingBusAs = src.doingBusAs;
			tar.specialNeedsReq = src.specialNeedsReq;
			tar.lowIncomeCustomer = src.lowIncomeCustomer;
			tar.language = src.language;
			tar.status = src.status;
			tar.dateInserted = src.dateInserted;
			tar.dateModified = src.dateModified;
			tar.rowState = src.rowState;
		}
		#endregion
 
		#region	SQL
		[Serializable]
			class Energy_CustDataSQL : SqlGateway
		{
			public Energy_CustData[] getKey(Energy_CustData rec)
			{
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spEnergy_CustData_Get_Id";
				cmd.Parameters.Add("@ID", SqlDbType.Int, 0).Value = rec.iD;
				return convert(execReader(cmd));
			}
			public override void insert(DomainObj obj)
			{
				Energy_CustData rec = (Energy_CustData)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spEnergy_CustData_Ins";
				setParam(cmd, rec);
                
				cmd.Parameters["@ID"].Direction = ParameterDirection.Output;
				execScalar(cmd);
				rec.iD = (int)cmd.Parameters["@ID"].Value;
				rec.rowState = RowState.Clean;
			}
			public override void delete(DomainObj obj)
			{
				Energy_CustData rec = (Energy_CustData)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spEnergy_CustData_Del_Id";
				cmd.Parameters.Add("@ID", SqlDbType.Int, 0).Value = rec.iD;
                
				execScalar(cmd);
				rec.rowState = RowState.Deleted;
			}
			public override void update(DomainObj obj)
			{
				Energy_CustData rec = (Energy_CustData)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spEnergy_CustData_Upd";
				setParam(cmd, rec);
                
				execScalar(cmd);
				rec.rowState = RowState.Clean;
			}
			public Energy_CustData[] getAll(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spEnergy_CustData_Get_All";
				return convert(execReader(cmd));
			}
			public Energy_CustData[] SearchCustomer(UOW uow, string phNumber, string lastName, string address, string city, string state, string zip, int accNumber)
			{
				SqlCommand cmd = makeCommand(uow);				
				cmd.CommandText = "spEnergy_CustData_SearchCustomer";
				SetSearchParam(cmd, phNumber, lastName, address, city, state, zip, accNumber);
				
				return convert(execReader(cmd));
			}
			#region Implementation
			void setParam(SqlCommand cmd, Energy_CustData rec)
			{
				cmd.Parameters.Add("@ID", SqlDbType.Int, 0).Value = rec.iD;
 
				cmd.Parameters.Add("@SiteCode", SqlDbType.VarChar, 25).Value = rec.siteCode;
 
				if (rec.quoteId == null)
					cmd.Parameters.Add("@QuoteId", SqlDbType.VarChar, 15).Value = DBNull.Value;
				else
				{
					if (rec.QuoteId.Length == 0)
						cmd.Parameters.Add("@QuoteId", SqlDbType.VarChar, 15).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@QuoteId", SqlDbType.VarChar, 15).Value = rec.quoteId;
				} 
				
				cmd.Parameters.Add("@AccountNumber", SqlDbType.Int, 0).Value = rec.accountNumber;
				
				if (rec.pin == null)
					cmd.Parameters.Add("@Pin", SqlDbType.VarChar, 15).Value = DBNull.Value;
				else
				{
					if (rec.Pin.Length == 0)
						cmd.Parameters.Add("@Pin", SqlDbType.VarChar, 15).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Pin", SqlDbType.VarChar, 15).Value = rec.pin;
				}
 
				if (rec.address1 == null)
					cmd.Parameters.Add("@Address1", SqlDbType.VarChar, 200).Value = DBNull.Value;
				else
				{
					if (rec.Address1.Length == 0)
						cmd.Parameters.Add("@Address1", SqlDbType.VarChar, 200).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Address1", SqlDbType.VarChar, 200).Value = rec.address1;
				}
 
				if (rec.address2 == null)
					cmd.Parameters.Add("@Address2", SqlDbType.VarChar, 100).Value = DBNull.Value;
				else
				{
					if (rec.Address2.Length == 0)
						cmd.Parameters.Add("@Address2", SqlDbType.VarChar, 100).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Address2", SqlDbType.VarChar, 100).Value = rec.address2;
				}
 
				if (rec.city == null)
					cmd.Parameters.Add("@City", SqlDbType.VarChar, 100).Value = DBNull.Value;
				else
				{
					if (rec.City.Length == 0)
						cmd.Parameters.Add("@City", SqlDbType.VarChar, 100).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@City", SqlDbType.VarChar, 100).Value = rec.city;
				}
 
				if (rec.state == null)
					cmd.Parameters.Add("@State", SqlDbType.VarChar, 2).Value = DBNull.Value;
				else
				{
					if (rec.State.Length == 0)
						cmd.Parameters.Add("@State", SqlDbType.VarChar, 2).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@State", SqlDbType.VarChar, 2).Value = rec.state;
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
 
				if (rec.zip4 == null)
					cmd.Parameters.Add("@Zip4", SqlDbType.VarChar, 4).Value = DBNull.Value;
				else
				{
					if (rec.Zip4.Length == 0)
						cmd.Parameters.Add("@Zip4", SqlDbType.VarChar, 4).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Zip4", SqlDbType.VarChar, 4).Value = rec.zip4;
				}
 
				if (rec.nameFirst == null)
					cmd.Parameters.Add("@NameFirst", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.NameFirst.Length == 0)
						cmd.Parameters.Add("@NameFirst", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@NameFirst", SqlDbType.VarChar, 50).Value = rec.nameFirst;
				}
 
				if (rec.nameLast == null)
					cmd.Parameters.Add("@NameLast", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.NameLast.Length == 0)
						cmd.Parameters.Add("@NameLast", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@NameLast", SqlDbType.VarChar, 50).Value = rec.nameLast;
				}
 
				if (rec.nameMiddle == null)
					cmd.Parameters.Add("@NameMiddle", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.NameMiddle.Length == 0)
						cmd.Parameters.Add("@NameMiddle", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@NameMiddle", SqlDbType.VarChar, 50).Value = rec.nameMiddle;
				}
 
				if (rec.ph1 == null)
					cmd.Parameters.Add("@Ph1", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.Ph1.Length == 0)
						cmd.Parameters.Add("@Ph1", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Ph1", SqlDbType.VarChar, 10).Value = rec.ph1;
				}
 
				if (rec.ph2 == null)
					cmd.Parameters.Add("@Ph2", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.Ph2.Length == 0)
						cmd.Parameters.Add("@Ph2", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Ph2", SqlDbType.VarChar, 10).Value = rec.ph2;
				}
 
				if (rec.email == null)
					cmd.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = DBNull.Value;
				else
				{
					if (rec.Email.Length == 0)
						cmd.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = rec.email;
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
 
				if (rec.preferedContactMethod == null)
					cmd.Parameters.Add("@PreferedContactMethod", SqlDbType.VarChar, 15).Value = DBNull.Value;
				else
				{
					if (rec.PreferedContactMethod.Length == 0)
						cmd.Parameters.Add("@PreferedContactMethod", SqlDbType.VarChar, 15).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@PreferedContactMethod", SqlDbType.VarChar, 15).Value = rec.preferedContactMethod;
				}
 
				if (rec.ssn == null)
					cmd.Parameters.Add("@Ssn", SqlDbType.VarChar, 9).Value = DBNull.Value;
				else
				{
					if (rec.Ssn.Length == 0)
						cmd.Parameters.Add("@Ssn", SqlDbType.VarChar, 9).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Ssn", SqlDbType.VarChar, 9).Value = rec.ssn;
				}
 
				if (rec.dL == null)
					cmd.Parameters.Add("@DL", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.DL.Length == 0)
						cmd.Parameters.Add("@DL", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@DL", SqlDbType.VarChar, 50).Value = rec.dL;
				}
 
				if (rec.dlState == null)
					cmd.Parameters.Add("@DlState", SqlDbType.VarChar, 2).Value = DBNull.Value;
				else
				{
					if (rec.DlState.Length == 0)
						cmd.Parameters.Add("@DlState", SqlDbType.VarChar, 2).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@DlState", SqlDbType.VarChar, 2).Value = rec.dlState;
				}
 
				if (rec.dOB == DateTime.MinValue)
					cmd.Parameters.Add("@DOB", SqlDbType.DateTime, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@DOB", SqlDbType.DateTime, 0).Value = rec.dOB;
 
				if (rec.permitName == null)
					cmd.Parameters.Add("@PermitName", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.PermitName.Length == 0)
						cmd.Parameters.Add("@PermitName", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@PermitName", SqlDbType.VarChar, 50).Value = rec.permitName;
				}
 
				if (rec.customerNumberRef == null)
					cmd.Parameters.Add("@CustomerNumberRef", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.CustomerNumberRef.Length == 0)
						cmd.Parameters.Add("@CustomerNumberRef", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@CustomerNumberRef", SqlDbType.VarChar, 50).Value = rec.customerNumberRef;
				}
 
				if (rec.doingBusAs == null)
					cmd.Parameters.Add("@DoingBusAs", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.DoingBusAs.Length == 0)
						cmd.Parameters.Add("@DoingBusAs", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@DoingBusAs", SqlDbType.VarChar, 50).Value = rec.doingBusAs;
				}
 
				cmd.Parameters.Add("@SpecialNeedsReq", SqlDbType.Bit, 0).Value = rec.specialNeedsReq;
 
				cmd.Parameters.Add("@LowIncomeCustomer", SqlDbType.Bit, 0).Value = rec.lowIncomeCustomer;
 
				if (rec.language == null)
					cmd.Parameters.Add("@Language", SqlDbType.VarChar, 15).Value = DBNull.Value;
				else
				{
					if (rec.Language.Length == 0)
						cmd.Parameters.Add("@Language", SqlDbType.VarChar, 15).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Language", SqlDbType.VarChar, 15).Value = rec.language;
				}
 
				if (rec.status == null)
					cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = DBNull.Value;
				else
				{
					if (rec.Status.Length == 0)
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = rec.status;
				}
 
				if (rec.dateInserted == DateTime.MinValue)
					cmd.Parameters.Add("@DateInserted", SqlDbType.DateTime, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@DateInserted", SqlDbType.DateTime, 0).Value = rec.dateInserted;
 
				if (rec.dateModified == DateTime.MinValue)
					cmd.Parameters.Add("@DateModified", SqlDbType.DateTime, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@DateModified", SqlDbType.DateTime, 0).Value = rec.dateModified;
			}
			void SetSearchParam(SqlCommand cmd, string phNumber, string lastName, string address, string city, string state, string zip, int accNumber)
			{
				cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = accNumber;
				cmd.Parameters.Add("@Address", SqlDbType.VarChar, 200).Value = address;
				cmd.Parameters.Add("@City", SqlDbType.VarChar, 100).Value = city;
				cmd.Parameters.Add("@State", SqlDbType.VarChar, 2).Value = state;
				cmd.Parameters.Add("@Zip", SqlDbType.VarChar, 5).Value = zip;
				cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = lastName;
				cmd.Parameters.Add("@PhNumber", SqlDbType.VarChar, 10).Value = phNumber;
			}
				protected override DomainObj reader(SqlDataReader rdr)
			{
				Energy_CustData rec = new Energy_CustData();
                
				if (rdr["ID"] != DBNull.Value)
					rec.iD = (int) rdr["ID"];
 
				if (rdr["SiteCode"] != DBNull.Value)
					rec.siteCode = (string) rdr["SiteCode"];
 
				if (rdr["QuoteId"] != DBNull.Value)
					rec.quoteId = (string) rdr["QuoteId"];
 
				if (rdr["AccountNumber"] != DBNull.Value)
					rec.accountNumber = (int) rdr["AccountNumber"];

				if (rdr["Pin"] != DBNull.Value)
					rec.pin = (string) rdr["Pin"];
 
				if (rdr["Address1"] != DBNull.Value)
					rec.address1 = (string) rdr["Address1"];
 
				if (rdr["Address2"] != DBNull.Value)
					rec.address2 = (string) rdr["Address2"];
 
				if (rdr["City"] != DBNull.Value)
					rec.city = (string) rdr["City"];
 
				if (rdr["State"] != DBNull.Value)
					rec.state = (string) rdr["State"];
 
				if (rdr["Zip"] != DBNull.Value)
					rec.zip = (string) rdr["Zip"];
 
				if (rdr["Zip4"] != DBNull.Value)
					rec.zip4 = (string) rdr["Zip4"];
 
				if (rdr["NameFirst"] != DBNull.Value)
					rec.nameFirst = (string) rdr["NameFirst"];
 
				if (rdr["NameLast"] != DBNull.Value)
					rec.nameLast = (string) rdr["NameLast"];
 
				if (rdr["NameMiddle"] != DBNull.Value)
					rec.nameMiddle = (string) rdr["NameMiddle"];
 
				if (rdr["Ph1"] != DBNull.Value)
					rec.ph1 = (string) rdr["Ph1"];
 
				if (rdr["Ph2"] != DBNull.Value)
					rec.ph2 = (string) rdr["Ph2"];
 
				if (rdr["Email"] != DBNull.Value)
					rec.email = (string) rdr["Email"];
 
				if (rdr["Fax"] != DBNull.Value)
					rec.fax = (string) rdr["Fax"];
 
				if (rdr["PreferedContactMethod"] != DBNull.Value)
					rec.preferedContactMethod = (string) rdr["PreferedContactMethod"];
 
				if (rdr["Ssn"] != DBNull.Value)
					rec.ssn = (string) rdr["Ssn"];
 
				if (rdr["DL"] != DBNull.Value)
					rec.dL = (string) rdr["DL"];
 
				if (rdr["DlState"] != DBNull.Value)
					rec.dlState = (string) rdr["DlState"];
 
				if (rdr["DOB"] != DBNull.Value)
					rec.dOB = (DateTime) rdr["DOB"];
 
				if (rdr["PermitName"] != DBNull.Value)
					rec.permitName = (string) rdr["PermitName"];
 
				if (rdr["CustomerNumberRef"] != DBNull.Value)
					rec.customerNumberRef = (string) rdr["CustomerNumberRef"];
 
				if (rdr["DoingBusAs"] != DBNull.Value)
					rec.doingBusAs = (string) rdr["DoingBusAs"];
 
				if (rdr["SpecialNeedsReq"] != DBNull.Value)
					rec.specialNeedsReq = (bool) rdr["SpecialNeedsReq"];
 
				if (rdr["LowIncomeCustomer"] != DBNull.Value)
					rec.lowIncomeCustomer = (bool) rdr["LowIncomeCustomer"];
 
				if (rdr["Language"] != DBNull.Value)
					rec.language = (string) rdr["Language"];
 
				if (rdr["Status"] != DBNull.Value)
					rec.status = (string) rdr["Status"];
 
				if (rdr["DateInserted"] != DBNull.Value)
					rec.dateInserted = (DateTime) rdr["DateInserted"];
 
				if (rdr["DateModified"] != DBNull.Value)
					rec.dateModified = (DateTime) rdr["DateModified"];
 
				rec.rowState = RowState.Clean;
				return rec;
			}
			Energy_CustData[] convert(DomainObj[] objs)
			{
				Energy_CustData[] acls  = new Energy_CustData[objs.Length];
				objs.CopyTo(acls, 0);
				return  acls;
			}
			#endregion
		}
		#endregion
	}
}
