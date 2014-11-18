using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Components;
using DPI.Interfaces;
using DPI.Components.EPSolutions;
 
namespace DPI.Components
{
	[Serializable]
	public class EnergyEnrollment : DomainObj
	{
		#region Data
		static string iName = "EnergyEnrollment";
		int iD;
		int accountNumber;
		string accountName;
		string serviceProviderName;
		string serviceProviderPhone;
		decimal initPrepayAmt;
		int customer;
		string address1;
		string address2;
		string city;
		string state;
		string zip;
		string zip4;
		DateTime startDate;
		DateTime enrollDate;
		DateTime modifyDate;
		string status;
		IEnergy_CustData engCustomer;
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
		public int AccountNumber
		{
			get { return accountNumber; }
			set
			{
				setState();
				accountNumber = value;
			}
		}
		public string AccountName
		{
			get { return accountName; }
			set
			{
				setState();
				accountName = value;
			}
		}
		public string ServiceProviderName
		{
			get { return serviceProviderName; }
			set
			{
				setState();
				serviceProviderName = value;
			}
		}
		public string ServiceProviderPhone
		{
			get { return serviceProviderPhone; }
			set
			{
				setState();
				serviceProviderPhone = value;
			}
		}
		public decimal InitPrepayAmt
		{
			get { return initPrepayAmt; }
			set
			{
				setState();
				initPrepayAmt = Decimal.Round(value, 2);
			}
		}
		public int Customer
		{
			get { return customer; }
			set
			{
				setState();
				customer = value;
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
		public DateTime StartDate
		{
			get { return startDate; }
			set
			{
				setState();
				startDate = value;
			}
		}
		public DateTime EnrollDate
		{
			get { return enrollDate; }
			set
			{
				setState();
				enrollDate = value;
			}
		}
		public DateTime ModifyDate
		{
			get { return modifyDate; }
			set
			{
				setState();
				modifyDate = value;
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
		public IEnergy_CustData EngCustomer
		{
			get { return engCustomer;  }
			set
			{
				setState();
				engCustomer = value;
			}
		}
		#endregion
        
		#region Constructors
		public EnergyEnrollment()
		{
			sql = new EnergyEnrollmentSQL();
			iD = random.Next(Int32.MinValue, -1);
			rowState = RowState.New;
		}
		public EnergyEnrollment(UOW uow) : this()
		{
			if(uow == null)
				throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
			if(uow.Imap == null)
				throw new ArgumentException("IdentityMap is required", "Identity Map");
            
			this.uow = uow;
			this.uow.Imap.add(this);
		}
		public EnergyEnrollment(UOW uow, EnrollmentResponse er) : this(uow)
		{
			if (er == null)
				return;

			if (er.Enrollment == null)
				return;

			this.accountName			= er.Enrollment.AccountName;
			this.accountNumber			= int.Parse(er.Enrollment.AccountNumber);
			this.enrollDate				= DateTime.Now;
			this.initPrepayAmt			= er.Enrollment.PrepaymentRequired;
			this.serviceProviderName	= er.Enrollment.ServiceProviderName;
			this.serviceProviderPhone	= er.Enrollment.ServiceProviderPhone;
			this.startDate				= DateTime.Parse(er.Enrollment.StartDate);
		}
		#endregion
        
		#region Methods
		protected override SqlGateway loadSql()
		{
			return new EnergyEnrollmentSQL();
		}
		public override void checkExists()
		{
			if ((ID < 1))
				throw new ArgumentException("Valid row is required for update and delete");
		}
		public override	void RefreshForeignKeys()
		{
			if (customer > 0)
				return;

			if (engCustomer == null)
				return;

			customer = engCustomer.ID;
		}
		#endregion

		#region	Static methods
		public static EnergyEnrollment find(UOW uow, int iD)
		{
			if (uow.Imap.keyExists(EnergyEnrollment.getKey(iD)))
				return (EnergyEnrollment)uow.Imap.find(EnergyEnrollment.getKey(iD));
            
			EnergyEnrollment cls = new EnergyEnrollment();
			cls.uow = uow;
			cls.iD = iD;
			cls = (EnergyEnrollment)DomainObj.addToIMap(uow, getOne(((EnergyEnrollmentSQL)cls.Sql).getKey(cls)));
			cls.uow = uow;
            
			return cls;
		}
		public static EnergyEnrollment[] getAll(UOW uow)
		{
			EnergyEnrollment[] objs = (EnergyEnrollment[])DomainObj.addToIMap(uow, (new EnergyEnrollmentSQL()).getAll(uow));
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
		static EnergyEnrollment getOne(EnergyEnrollment[] acls)
		{
			if (acls.Length == 1)
				return acls[0];
            
			if (acls.Length == 0)
				throw new ArgumentException("Row not found");
            
			throw new ArgumentException("More than one row found");
		}
		static void copyAttrs(EnergyEnrollment src, EnergyEnrollment tar)
		{
			if (tar == null)
				throw new ArgumentNullException("Target object must not be null");
            
			if (src == null)
				throw new ArgumentNullException("Source object must not be null");
            
			tar.iD = src.iD;
			tar.accountNumber = src.accountNumber;
			tar.accountName = src.accountName;
			tar.serviceProviderName = src.serviceProviderName;
			tar.serviceProviderPhone = src.serviceProviderPhone;
			tar.initPrepayAmt = src.initPrepayAmt;
			tar.customer = src.customer;
			tar.address1 = src.address1;
			tar.address2 = src.address2;
			tar.city = src.city;
			tar.state = src.state;
			tar.zip = src.zip;
			tar.zip4 = src.zip4;
			tar.startDate = src.startDate;
			tar.enrollDate = src.enrollDate;
			tar.modifyDate = src.modifyDate;
			tar.status = src.status;
			tar.rowState = src.rowState;
		}
		#endregion
 
		#region	SQL
		[Serializable]
			class EnergyEnrollmentSQL : SqlGateway
		{
			public EnergyEnrollment[] getKey(EnergyEnrollment rec)
			{
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spEnergyEnrollment_Get_Id";
				cmd.Parameters.Add("@ID", SqlDbType.Int, 0).Value = rec.iD;
				return convert(execReader(cmd));
			}
			public override void insert(DomainObj obj)
			{
				EnergyEnrollment rec = (EnergyEnrollment)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spEnergyEnrollment_Ins";
				setParam(cmd, rec);
                
				cmd.Parameters["@ID"].Direction = ParameterDirection.Output;
				execScalar(cmd);
				rec.iD = (int)cmd.Parameters["@ID"].Value;
				rec.rowState = RowState.Clean;
			}
			public override void delete(DomainObj obj)
			{
				EnergyEnrollment rec = (EnergyEnrollment)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spEnergyEnrollment_Del_Id";
				cmd.Parameters.Add("@ID", SqlDbType.Int, 0).Value = rec.iD;
                
				execScalar(cmd);
				rec.rowState = RowState.Deleted;
			}
			public override void update(DomainObj obj)
			{
				EnergyEnrollment rec = (EnergyEnrollment)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spEnergyEnrollment_Upd";
				setParam(cmd, rec);
                
				execScalar(cmd);
				rec.rowState = RowState.Clean;
			}
			public EnergyEnrollment[] getAll(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spEnergyEnrollment_Get_All";
				return convert(execReader(cmd));
			}
			#region Implementation
			void setParam(SqlCommand cmd, EnergyEnrollment rec)
			{
				cmd.Parameters.Add("@ID", SqlDbType.Int, 0).Value = rec.iD;
 
				cmd.Parameters.Add("@AccountNumber", SqlDbType.Int, 0).Value = rec.accountNumber;
 
				if (rec.accountName == null)
					cmd.Parameters.Add("@AccountName", SqlDbType.VarChar, 60).Value = DBNull.Value;
				else
				{
					if (rec.AccountName.Length == 0)
						cmd.Parameters.Add("@AccountName", SqlDbType.VarChar, 60).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@AccountName", SqlDbType.VarChar, 60).Value = rec.accountName;
				}
 
				if (rec.serviceProviderName == null)
					cmd.Parameters.Add("@ServiceProviderName", SqlDbType.VarChar, 20).Value = DBNull.Value;
				else
				{
					if (rec.ServiceProviderName.Length == 0)
						cmd.Parameters.Add("@ServiceProviderName", SqlDbType.VarChar, 20).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@ServiceProviderName", SqlDbType.VarChar, 20).Value = rec.serviceProviderName;
				}
 
				if (rec.serviceProviderPhone == null)
					cmd.Parameters.Add("@ServiceProviderPhone", SqlDbType.VarChar, 14).Value = DBNull.Value;
				else
				{
					if (rec.ServiceProviderPhone.Length == 0)
						cmd.Parameters.Add("@ServiceProviderPhone", SqlDbType.VarChar, 14).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@ServiceProviderPhone", SqlDbType.VarChar, 14).Value = rec.serviceProviderPhone;
				}
				cmd.Parameters.Add("@InitPrepayAmt", SqlDbType.Decimal, 0).Value = rec.initPrepayAmt;
				cmd.Parameters.Add("@Customer", SqlDbType.Int, 0).Value = rec.customer;
 
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
 
				if (rec.startDate == DateTime.MinValue)
					cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = rec.startDate;
 
				if (rec.enrollDate == DateTime.MinValue)
					cmd.Parameters.Add("@EnrollDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@EnrollDate", SqlDbType.DateTime, 0).Value = rec.enrollDate;
 
				if (rec.modifyDate == DateTime.MinValue)
					cmd.Parameters.Add("@ModifyDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@ModifyDate", SqlDbType.DateTime, 0).Value = rec.modifyDate;
 
				if (rec.status == null)
					cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = DBNull.Value;
				else
				{
					if (rec.Status.Length == 0)
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = rec.status;
				}
			}
			protected override DomainObj reader(SqlDataReader rdr)
			{
				EnergyEnrollment rec = new EnergyEnrollment();
                
				if (rdr["ID"] != DBNull.Value)
					rec.iD = (int) rdr["ID"];
 
				if (rdr["AccountNumber"] != DBNull.Value)
					rec.accountNumber = (int) rdr["AccountNumber"];
 
				if (rdr["AccountName"] != DBNull.Value)
					rec.accountName = (string) rdr["AccountName"];
 
				if (rdr["ServiceProviderName"] != DBNull.Value)
					rec.serviceProviderName = (string) rdr["ServiceProviderName"];
 
				if (rdr["ServiceProviderPhone"] != DBNull.Value)
					rec.serviceProviderPhone = (string) rdr["ServiceProviderPhone"];
 
				if (rdr["InitPrepayAmt"] != DBNull.Value)
					rec.initPrepayAmt = Decimal.Round((decimal)rdr["InitPrepayAmt"], 2);
 
				if (rdr["Customer"] != DBNull.Value)
					rec.customer = (int) rdr["Customer"];
 
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
 
				if (rdr["StartDate"] != DBNull.Value)
					rec.startDate = (DateTime) rdr["StartDate"];
 
				if (rdr["EnrollDate"] != DBNull.Value)
					rec.enrollDate = (DateTime) rdr["EnrollDate"];
 
				if (rdr["ModifyDate"] != DBNull.Value)
					rec.modifyDate = (DateTime) rdr["ModifyDate"];
 
				if (rdr["Status"] != DBNull.Value)
					rec.status = (string) rdr["Status"];
 
				rec.rowState = RowState.Clean;
				return rec;
			}
			EnergyEnrollment[] convert(DomainObj[] objs)
			{
				EnergyEnrollment[] acls  = new EnergyEnrollment[objs.Length];
				objs.CopyTo(acls, 0);
				return  acls;
			}
			#endregion
		}
		#endregion
	}
}
