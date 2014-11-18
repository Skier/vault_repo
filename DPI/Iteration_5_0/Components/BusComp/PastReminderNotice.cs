using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DPI.Interfaces;

namespace DPI.Components
{
	public class PastReminderNotice : IPastReminderNotice
	{
	    #region Static Members

        public static string GetCustomerBillFileName(int accNumber, DateTime billDate, string phNumber)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(accNumber.ToString());
            sb.Append("_");
            sb.Append(String.Format("{0:MMddyyyy}", billDate));
            sb.Append("_");
            sb.Append(phNumber);
            sb.Append(".pdf");

            return sb.ToString();
        }

	    #endregion

	    #region Member Variables		
		int accNumber;
		string phNumber;
		string nameLast;
		DateTime bill_Date;
		#endregion
		#region Properties
		public int AccNumber 
		{
			get {return accNumber;}			
		}
		public string PhNumber
		{
			get {return phNumber;}			
		}
		public string NameLast
		{
			get {return nameLast;}			
		}
		public DateTime Bill_Date
		{
			get {return bill_Date;}			
		}	
		public string Filename
		{
			get { return GetCustomerBillFileName(this.accNumber, this.bill_Date, this.phNumber); }
		}
		#endregion
		#region Methods
		//Returns latest reminder notice for customer
		public static PastReminderNotice GetReminderNotice(UOW _uow, int _accNumber)
		{
			return (new PastReminderNoticeSQL().GetPastReminderNotice(_uow, _accNumber));
		}
		//Returns all previous reminder notices for customer
		public static PastReminderNotice[] GetPastReminderNotices(UOW _uow, int _accNumber)
		{
			return (new PastReminderNoticeSQL()).GetPastReminderNotices(_uow, _accNumber);
		}
		#endregion
		#region Implementation
		class PastReminderNoticeSQL
		{           
			public PastReminderNotice GetPastReminderNotice(UOW _uow, int _accNumber)
			{
				SqlCommand cmd = _uow.Cn.CreateCommand();
				PastReminderNotice pastReminderNotice = null;
				try 
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandText = "spBilling_PastReminderNotice";
					cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = _accNumber;
					System.Data.SqlClient.SqlDataReader sqlDataReader = cmd.ExecuteReader();
					if (sqlDataReader.HasRows)
					{
						sqlDataReader.Read();
						pastReminderNotice = reader(sqlDataReader, new PastReminderNotice());
					}
					else
					{
						return null;
					}
				}
				finally
				{
					_uow.close();
				}
				return pastReminderNotice;
			}
			public PastReminderNotice[] GetPastReminderNotices(UOW _uow, int _accNumber)
			{
				SqlCommand cmd = _uow.Cn.CreateCommand();				
				ArrayList arrayList = new ArrayList();
				try 
				{
					cmd.CommandType = CommandType.StoredProcedure;			
					cmd.CommandText = "spBilling_PastReminderNotice";
					cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = _accNumber;				
					System.Data.SqlClient.SqlDataReader sqlDataReader = cmd.ExecuteReader();
				
					while (sqlDataReader.Read())
						arrayList.Add(reader(sqlDataReader, new PastReminderNotice()));
				}
				finally
				{
					_uow.close();
				}
				PastReminderNotice[] pastReminderNotices = new PastReminderNotice[arrayList.Count];
				arrayList.CopyTo(pastReminderNotices);
				return pastReminderNotices;
			}
			PastReminderNotice reader(SqlDataReader rdr, PastReminderNotice rec)
			{				
				if (rdr["AccNumber"] != DBNull.Value)
					rec.accNumber = (int) rdr["AccNumber"];

				if (rdr["PhNumber"] != DBNull.Value)
					rec.phNumber = (string) rdr["PhNumber"]; 
				
				if (rdr["NameLast"] != DBNull.Value)
					rec.nameLast = (string) rdr["NameLast"]; 
				
				if (rdr["Bill_Date"] != DBNull.Value)
					rec.bill_Date = (DateTime) rdr["Bill_Date"]; 
				return rec;
			}
			#endregion
		}
	}	
}
