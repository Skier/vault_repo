
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
	public class CustInfo: DomainObj, ICustInfo2, IAcctInfo
	{
		/*        Data        */
		static string iName = "CustInfo";
		int			custInfoID;
		string		custInfoType;
		string		status;
		string		lastName;
		string		firstName;
		string		birthday;
		string		email;
		string		contact;
		string		contact2;
		string		prevPhone;
		string		prevILEC;
		int			servAddID;
		int			mailAddID;
		int			accNumber;
		IAddr2		saddr;
		IAddr2		maddr;
		string		phNumber;
		string		idNumber;
		DateTime	idExpirationDate;
		string		ssn;
		DateTime	dob;
		string		idType;
		string		idState;
        
	#region IAcctInfo Properties
		public decimal DueAmt        { get { return	0m; }}
		public decimal CurrCharges   { get { return	0m; }}
		public string PhNumber       
		{ 
			get { return phNumber;		}
			set { phNumber = value;		}
		}
		public DateTime DueDate      { get { return	DateTime.MinValue;	}}
		public string PhNumFormated  { get { return	"";	}}
		public decimal CustDataBal   { get { return	0m; }}
		public bool IsActive	     { get { return	false;	}}
		public DateTime DiscoDate    { get { return	DateTime.MinValue;	}}
		public decimal BalForward    { get { return	0m; }}
		public decimal PastDueAmt    { get { return	0m; }}
	#endregion

	#region CustInfo Properties
		
		public override IDomKey IKey { get { return new Key(iName, custInfoID.ToString()); }}
		public int CustInfoID
		{
			get { return custInfoID; }
		}
		public string CustInfoType
		{
			get { return custInfoType; }
			set
			{
				setState();
				custInfoType = value;
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
		public string LastName
		{
			get { return lastName; }
			set
			{
				setState();
				lastName = value;
			}
		}
		public string FirstName
		{
			get { return firstName; }
			set
			{
				setState();
				firstName = value;
			}
		}
		public string Birthday
		{
			get { return birthday; }
			set
			{
				setState();
				birthday = value;
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
		public string Contact
		{
			get { return contact; }
			set
			{
				setState();
				contact = value;
			}
		}
		public string Contact2
		{
			get { return contact2; }
			set
			{
				setState();
				contact2 = value;
			}
		}
		public string PrevPhone
		{
			get { return prevPhone; }
			set
			{
				setState();
				prevPhone = value;
			}
		}
		public string PrevILEC
		{
			get { return prevILEC; }
			set
			{
				setState();
				prevILEC = value;
			}
		}
		public int ServAddID
		{
			get {
				if (ServAddr != null)
					return saddr.AddressID;

				return servAddID; 
			}
			set
			{
				servAddID = value;
			}
		}
		public IAddr2 ServAddr
		{
			get 
			{
				if (saddr == null)
					if ( servAddID > 0)
						saddr = CustAddress.find(uow, servAddID);

				return saddr;
			}
			set 
			{
				setState();
				saddr = value;
			}
		}

		public int MailAddID
		{
			get { return mailAddID; }
			set
			{
				setState();
				mailAddID = value;
			}
		}
		public IAddr2 MailAddr
		{
			get 
			{
				if (maddr == null)
					if ( mailAddID > 0)
						maddr = CustAddress.find(uow, mailAddID);

				return maddr;
			}
			set 
			{
				setState();
				maddr = value;
			}
		}
		public int AccNumber
		{
			get {return accNumber;}
			set
			{
				setState();
				accNumber = value;
			}
		}

		public string FormattedName
		{
			get
			{
				if ((firstName == null) && (lastName == null))
					return "Unknown";
			
				return Formatted(firstName) + " " + Formatted(lastName);								
							
			}
		}

		public string IDNumber
		{
			get { return idNumber; }
			set
			{
				setState();
				idNumber = value;
			}
		}
		
		public DateTime IDExpirationDate
		{
			get { return idExpirationDate; }
			set
			{
				setState();
				idExpirationDate = value;
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
		
		public DateTime Dob
		{
			get { return dob; }
			set
			{
				setState();
				dob = value;
			}
		}

		public string IDType
		{
			get { return idType; }
			set
			{
				setState();
				idType = value;
			}
		}

		public string IDState
		{
			get { return idState; }
			set
			{
				setState();
				idState = value;
			}
		}

		#endregion

		 
		/*        Constructors			*/
		public CustInfo()
		{
			sql = new CustInfoSQL();
			custInfoID = random.Next(Int32.MinValue, -1);
			rowState = RowState.New;
			priority = 10000;
		}
		public CustInfo(IMap imap) : this()
		{
			if(imap == null)
				throw new ArgumentException("IdentityMap is required", "Identity Map");
            
			imap.add(this);
		}
		public CustInfo(UOW uow) : this(uow.Imap)
		{
			if(uow == null)
				throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
		}
		public CustInfo(IMap imap, string custInfoType) : this(imap)
		{
			if (custInfoType == null)
				throw new ArgumentNullException("CustInfoType");

			if (custInfoType.Trim().Length == 0)
				throw new ArgumentException("CustInfoType is required");

			this.custInfoType = custInfoType;
		}
		public CustInfo(UOW uow, string custInfoType) : this(uow.Imap, custInfoType)
		{
			if(uow == null)
				throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
		}
		/*        Methods        */
		protected override SqlGateway loadSql()
		{
			return new CustInfoSQL();
		}
		public override void checkExists()
		{
			if ((CustInfoID < 1))
				throw new ArgumentException("Valid row is required for update and delete");
		}
		/*		Static methods		*/
		public static CustInfo find(UOW uow, int custInfoID)
		{
			if (uow.Imap.keyExists(CustInfo.getKey(custInfoID)))
				return (CustInfo)uow.Imap.find(CustInfo.getKey(custInfoID));
            
			CustInfo cls = new CustInfo();
			cls.uow = uow;
			cls.custInfoID = custInfoID;
			cls = (CustInfo)DomainObj.addToIMap(uow, getOne(((CustInfoSQL)cls.Sql).getKey(cls)));
			cls.uow = uow;
            
			return cls;
		}
		public static CustInfo[] getAll(UOW uow)
		{
			CustInfo[] objs = (CustInfo[])DomainObj.addToIMap(uow, (new CustInfoSQL()).getAll(uow));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Key getKey(int custInfoID)
		{
			return new Key(iName, custInfoID.ToString());
		}
		public static CustInfo GetCustByPayInfo(UOW uow, int payInfo)
		{
			return new CustInfoSQL().getByPayInfo(uow, payInfo);
		}
		
		public override	void RefreshForeignKeys()
		{
			RefreshMailAddr();
			RefreshServAddr();
		}
		/*        Implementation        */
		void RefreshMailAddr()
		{
			if (mailAddID > 0)
				return;

			if (maddr != null)
				mailAddID = maddr.AddressID;
		}
		void RefreshServAddr()
		{
			if (servAddID > 0)
				return;

			if (saddr != null)
				servAddID = saddr.AddressID;
		}
		string Formatted(string name) 
		{ 
			return CapitalizeName(name);
		}
	    public static string CapitalizeName(string name) {
            string fname = "Unknown";
					
            if (name != null)
                fname = name.Trim();		
					
            if (fname.Length < 2)
                return fname.ToUpper();
		 
            return fname[0].ToString().ToUpper() + fname.Substring(1).ToLower();
        }
		static CustInfo getOne(CustInfo[] acls)
		{
			if (acls.Length == 1)
				return acls[0];
            
			if (acls.Length == 0)
				throw new ArgumentException("Row not found");
            
			throw new ArgumentException("More than one row found");
		}
		static void copyAttrs(CustInfo src, CustInfo tar)
		{
			if (tar == null)
				throw new ArgumentNullException("Target object must not be null");
            
			if (src == null)
				throw new ArgumentNullException("Source object must not be null");
            
			tar.custInfoID = src.custInfoID;
			tar.custInfoType = src.custInfoType;
			tar.status = src.status;
			tar.lastName = src.lastName;
			tar.firstName = src.firstName;
			tar.birthday = src.birthday;
			tar.email = src.email;
			tar.contact = src.contact;
			tar.contact2 = src.contact2;
			tar.prevPhone = src.prevPhone;
			tar.prevILEC = src.prevILEC;
			tar.servAddID = src.servAddID;
			tar.mailAddID = src.mailAddID;
			tar.accNumber = src.accNumber;
			tar.rowState = src.rowState;
			tar.idNumber = src.idNumber;
			tar.idExpirationDate = src.idExpirationDate;
			tar.ssn = src.ssn;
			tar.dob = src.dob;
			tar.idType = src.idType;
			tar.idState = src.idState;
			tar.phNumber = src.phNumber;
		}
 
		/*		SQL		*/
		[Serializable]
			class CustInfoSQL : SqlGateway
		{
			public CustInfo[] getKey(CustInfo rec)
			{
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spCustInfo_Get_Id";
				cmd.Parameters.Add("@CustInfoID", SqlDbType.Int, 0).Value = rec.custInfoID;
				return convert(execReader(cmd));
			}
			public override void insert(DomainObj obj)
			{
				CustInfo rec = (CustInfo)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spCustInfo_Ins";
				setParam(cmd, rec);
                
				cmd.Parameters["@CustInfoID"].Direction = ParameterDirection.Output;
				execScalar(cmd);
				rec.custInfoID = (int)cmd.Parameters["@CustInfoID"].Value;
				rec.rowState = RowState.Clean;
			}
			public override void delete(DomainObj obj)
			{
				CustInfo rec = (CustInfo)obj;
				if (rec.custInfoID < 1)
					return;

				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spCustInfo_Del_Id";
				cmd.Parameters.Add("@CustInfoID", SqlDbType.Int, 0).Value = rec.custInfoID;
                
				execScalar(cmd);
				rec.rowState = RowState.Deleted;
			}
			public override void update(DomainObj obj)
			{
				CustInfo rec = (CustInfo)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spCustInfo_Upd";
				setParam(cmd, rec);
                
				execScalar(cmd);
				rec.rowState = RowState.Clean;
			}
			public CustInfo getByPayInfo(UOW uow, int payInfo)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spCustInfo_Get_By_PayInfo";
				cmd.Parameters.Add("@PayInfo", SqlDbType.Int, 0).Value = payInfo;
				CustInfo[] res = convert(execReader(cmd));
				
				if (res.Length == 0)
					return null;

				return res[0];
			}
			public CustInfo[] getAll(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spCustInfo_Get_All";
				return convert(execReader(cmd));
			}
			/*        Implementation        */
			void setParam(SqlCommand cmd, CustInfo rec)
			{
				cmd.Parameters.Add("@CustInfoID", SqlDbType.Int, 0).Value = rec.custInfoID;
 
				if (rec.custInfoType == null)
					cmd.Parameters.Add("@CustInfoType", SqlDbType.VarChar, 20).Value = DBNull.Value;
				else
				{
					if (rec.CustInfoType.Length == 0)
						cmd.Parameters.Add("@CustInfoType", SqlDbType.VarChar, 20).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@CustInfoType", SqlDbType.VarChar, 20).Value = rec.custInfoType;
				}
 
				if (rec.status == null)
					cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = DBNull.Value;
				else
				{
					if (rec.Status.Length == 0)
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = rec.status;
				}
 
				if (rec.lastName == null)
					cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 30).Value = DBNull.Value;
				else
				{
					if (rec.LastName.Length == 0)
						cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 30).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 30).Value = rec.lastName;
				}
 
				if (rec.firstName == null)
					cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 25).Value = DBNull.Value;
				else
				{
					if (rec.FirstName.Length == 0)
						cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 25).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 25).Value = rec.firstName;
				}
 
				if (rec.birthday == null)
					cmd.Parameters.Add("@Birthday", SqlDbType.VarChar, 25).Value = DBNull.Value;
				else
				{
					if (rec.Birthday.Length == 0)
						cmd.Parameters.Add("@Birthday", SqlDbType.VarChar, 25).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Birthday", SqlDbType.VarChar, 25).Value = rec.birthday;
				}
 
				if (rec.email == null)
					cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.Email.Length == 0)
						cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = rec.email;
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
 
				if (rec.contact2 == null)
					cmd.Parameters.Add("@Contact2", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.Contact2.Length == 0)
						cmd.Parameters.Add("@Contact2", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Contact2", SqlDbType.VarChar, 50).Value = rec.contact2;
				}
 
				if (rec.prevPhone == null)
					cmd.Parameters.Add("@PrevPhone", SqlDbType.VarChar, 15).Value = DBNull.Value;
				else
				{
					if (rec.PrevPhone.Length == 0)
						cmd.Parameters.Add("@PrevPhone", SqlDbType.VarChar, 15).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@PrevPhone", SqlDbType.VarChar, 15).Value = rec.prevPhone;
				}
 
				if (rec.prevILEC == null)
					cmd.Parameters.Add("@PrevILEC", SqlDbType.VarChar, 3).Value = DBNull.Value;
				else
				{
					if (rec.PrevILEC.Length == 0)
						cmd.Parameters.Add("@PrevILEC", SqlDbType.VarChar, 3).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@PrevILEC", SqlDbType.VarChar, 3).Value = rec.prevILEC;
				}
                
				// Numeric, nullable foreign key treatment:
				if (rec.ServAddID == 0)
					cmd.Parameters.Add("@ServAddID", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@ServAddID", SqlDbType.Int, 0).Value = rec.servAddID;
                
				// Numeric, nullable foreign key treatment:
				if (rec.MailAddID == 0)
					cmd.Parameters.Add("@MailAddID", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@MailAddID", SqlDbType.Int, 0).Value = rec.mailAddID;
				
				// Numeric, nullable foreign key treatment:
				if (rec.accNumber == 0)
					cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = rec.accNumber;

				if (rec.idNumber == null)
					cmd.Parameters.Add("@IDNumber", SqlDbType.Char, 20).Value = DBNull.Value;
				else
				{
					if (rec.idNumber.Length == 0)
						cmd.Parameters.Add("@IDNumber", SqlDbType.Char, 20).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@IDNumber", SqlDbType.Char, 20).Value = rec.idNumber;
				}

				if (rec.idExpirationDate == DateTime.MinValue)
					cmd.Parameters.Add("@IDExpirationDate", SqlDbType.DateTime).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@IDExpirationDate", SqlDbType.DateTime).Value = rec.idExpirationDate;

				if (rec.ssn == null)
					cmd.Parameters.Add("@SSN", SqlDbType.Char, 9).Value = DBNull.Value;
				else
				{
					if (rec.ssn.Length == 0)
						cmd.Parameters.Add("@SSN", SqlDbType.Char, 9).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@SSN", SqlDbType.Char, 9).Value = rec.ssn;
				}

				if (rec.dob == DateTime.MinValue)
					cmd.Parameters.Add("@DOB", SqlDbType.DateTime).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@DOB", SqlDbType.DateTime).Value = rec.dob;

				if (rec.idType == null)
					cmd.Parameters.Add("@IDType", SqlDbType.Char, 30).Value = DBNull.Value;
				else
				{
					if (rec.idType.Length == 0)
						cmd.Parameters.Add("@IDType", SqlDbType.Char, 30).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@IDType", SqlDbType.Char, 30).Value = rec.idType;
				}
			
				if (rec.idState == null)
					cmd.Parameters.Add("@IDState", SqlDbType.Char, 10).Value = DBNull.Value;
				else
				{
					if (rec.idState.Length == 0)
						cmd.Parameters.Add("@IDState", SqlDbType.Char, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@IDState", SqlDbType.Char, 10).Value = rec.idState;
				}
				if (rec.phNumber == null)
					cmd.Parameters.Add("@PhNumber", SqlDbType.Char, 10).Value = DBNull.Value;
				else
				{
					if (rec.phNumber.Length == 0)
						cmd.Parameters.Add("@PhNumber", SqlDbType.Char, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@PhNumber", SqlDbType.Char, 10).Value = rec.phNumber;
				}
			}

			protected override DomainObj reader(SqlDataReader rdr)
			{
				CustInfo rec = new CustInfo();
                
				if (rdr["CustInfoID"] != DBNull.Value)
					rec.custInfoID = (int) rdr["CustInfoID"];
 
				if (rdr["CustInfoType"] != DBNull.Value)
					rec.custInfoType = (string) rdr["CustInfoType"];
 
				if (rdr["Status"] != DBNull.Value)
					rec.status = (string) rdr["Status"];
 
				if (rdr["LastName"] != DBNull.Value)
					rec.lastName = (string) rdr["LastName"];
 
				if (rdr["FirstName"] != DBNull.Value)
					rec.firstName = (string) rdr["FirstName"];
 
				if (rdr["Birthday"] != DBNull.Value)
					rec.birthday = (string) rdr["Birthday"];
 
				if (rdr["Email"] != DBNull.Value)
					rec.email = (string) rdr["Email"];
 
				if (rdr["Contact"] != DBNull.Value)
					rec.contact = (string) rdr["Contact"];
 
				if (rdr["Contact2"] != DBNull.Value)
					rec.contact2 = (string) rdr["Contact2"];
 
				if (rdr["PrevPhone"] != DBNull.Value)
					rec.prevPhone = (string) rdr["PrevPhone"];
 
				if (rdr["PrevILEC"] != DBNull.Value)
					rec.prevILEC = (string) rdr["PrevILEC"];
 
				if (rdr["ServAddID"] != DBNull.Value)
					rec.servAddID = (int) rdr["ServAddID"];
 
				if (rdr["MailAddID"] != DBNull.Value)
					rec.mailAddID = (int) rdr["MailAddID"];

				if (rdr["AccNumber"] != DBNull.Value)
					rec.AccNumber = (int) rdr["AccNumber"];

				if (rdr["IDNumber"] != DBNull.Value)
					rec.idNumber = (string) rdr["IDNumber"];

				if (rdr["IDExpirationDate"] != DBNull.Value)
					rec.idExpirationDate = (DateTime) rdr["IDExpirationDate"];

				if (rdr["SSN"] != DBNull.Value)
					rec.ssn = (string)rdr["SSN"];
 
				if (rdr["DOB"] != DBNull.Value)
					rec.dob = (DateTime) rdr["DOB"];

				if (rdr["IDType"] != DBNull.Value)
					rec.idType = (string) rdr["IDType"];

				if (rdr["IDState"] != DBNull.Value)
					rec.idState = (string) rdr["IDState"];

				if (rdr["PhNumber"] != DBNull.Value)
					rec.phNumber = (string) rdr["PhNumber"];

				rec.rowState = RowState.Clean;
				return rec;
			}
			CustInfo[] convert(DomainObj[] objs)
			{
				CustInfo[] acls  = new CustInfo[objs.Length];
				objs.CopyTo(acls, 0);
				return  acls;
			}
		}
		
	}
}





//using System;
//using System.Text;
//using System.Collections;
//using DPI.Interfaces;
//
//namespace DPI.Components
//{
//	[Serializable]  
////	public class CustInfo: ICustInfo
////	{
////		/*		Data		*/
//////		string lastName; 
//////		string firstName;
//////		string birthday;
//////		string email;
//////		string contact;
//////		string contact2;
//////		string prevPhone;
//////		string prevILEC;
////
////		public string PrevPhone
////		{ 
////			get { return prevPhone; } 
////			set { prevPhone = value;}
////		}
////		public string PrevILEC
////		{ 
////			get { return prevILEC; }
////			set { prevILEC = value; }
////		}
////		/*		Properties		*/
////		public string LastName 
////		{
////			get { return lastName; }
////			set { lastName = value; }
////		}
////		public string FirstName 
////		{
////			get { return firstName; }
////			set { firstName = value; }
////		}
////		public string 
///Birthday
////		{
////			get { return birthday; }
////			set { birthday = value; }
////		}
////		public string Email 
////		{
////			get { return email; }
////			set { email = value; }
////		}
////		public string Contact 
////		{
////			get { return contact; }
////			set { contact = value; }
////		}
////		public string Contact2 
////		{
////			get { return contact2; }
////			set { contact2 = value; }
////		}
////		string Formatted(string name) 
////		{ 
////			string fname = "Unknown";
////			
////			if (name != null)
////				fname = name.Trim();		
////			
////			if (fname.Length < 2)
////				return fname.ToUpper();
//// 
////			return fname[0].ToString().ToUpper() + fname.Substring(1).ToLower();
////		}
////		public string FormattedName
////		{
////			get
////			{
////				if ((firstName == null) && (lastName == null))
////					return "Unknown";
////
////				return Formatted(firstName) + " " + Formatted(lastName);								
////				
////			}
////		}
////	}
//}