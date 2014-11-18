using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes; 
using DPI.Interfaces;
using System.Collections;
namespace DPI.Components
{
	public class DebitCardWrapper
	{
	#region Methods
		public static string GetUniversalPath(string process, string name)	
		{
			UOW uow = null;
			SqlCommand cmd = null;
			
			if (process == null)
				throw new ArgumentException("Process is required");

			if (process.Trim().Length == 0)
				throw new ArgumentException("Process is required");

			if (name == null)
				throw new ArgumentException("Path Name is required");

			if (name.Trim().Length == 0)
				throw new ArgumentException("Path Name is required");

			try
			{
				uow = new UOW();
				cmd = GetCommand(uow, "spDebitCard_GetFilePath");

				cmd.Parameters.Add("@Process", SqlDbType.VarChar , 50).Value = process.Trim();			
				cmd.Parameters.Add("@PathName", SqlDbType.VarChar , 50).Value = name.Trim();			
	
				string[] data = GetDataNoHeader(cmd);
				
				switch (data.Length)
				{
					case 0 :
						throw new ApplicationException("No path is found");
		
					case 1 :
						return data[0];

					default :
						throw new ApplicationException("More that 1 path is found");
				}
			}
			finally
			{
				uow.close();
			}
		}
	
		public static string[] DebitCardPurposeFile(DateTime from)	
		{
			UOW uow = null;
			
			try
			{
				uow = new UOW();
				SqlCommand cmd = GetCommand(uow, "spDebitCard_DPIPurposeDailyFile");
				cmd.Parameters.Add("@ReportDate", SqlDbType.VarChar , 0).Value = from;			
				return GetData(cmd);		
			}
			finally
			{
				uow.close();
			}
		}

		public static string[] DebitCardPurposeSummaryFile(DateTime from)	
		{
			UOW uow = null;
			
			try
			{
				uow = new UOW();

				SqlCommand cmd = GetCommand(uow, "spDebitCard_PurposeSummaryFile");
				cmd.Parameters.Add("@ReportDate", SqlDbType.VarChar , 0).Value = from;			
				
				return GetData(cmd);
											
			}
			finally
			{
				uow.close();
			}
		}

		public static string[] DebitCardCommissionFile(DateTime from)	
		{
			UOW uow = null;
			
			try
			{	
				uow = new UOW();

				SqlCommand cmd = GetCommand(uow, "spDebitCard_CommissionReportFile");
				cmd.Parameters.Add("@ReportDate", SqlDbType.VarChar , 0).Value = from;			
				
				return GetData(cmd);		
			}
			finally
			{
				uow.close();
			}
		}		  
		  
		public static string[] DebitCardCommsummaryFile(DateTime from)	
		{
			UOW uow = null;
			
			try
			{	
				uow = new UOW();

				SqlCommand cmd = GetCommand(uow, "spDebitCard_CommSummaryReportFile");
				cmd.Parameters.Add("@ReportDate", SqlDbType.VarChar , 0).Value = from;			
				
				return GetData(cmd);		
			}
			finally
			{
				uow.close();
			}
		}

		public static string[] DebitCardDpiFile(DateTime from)	
		{
			UOW uow = null;
			
			try
			{	
				uow = new UOW();

				SqlCommand cmd = GetCommand(uow, "spDebitCard_DPIDailyFile");
				cmd.Parameters.Add("@ReportDate", SqlDbType.VarChar , 0).Value = from;			

				return GetData(cmd);		
			}
			finally
			{
				uow.close();
			}
		}

		public static string[] DebitCardDpiSumFile(DateTime from)	
		{
			UOW uow = null;
			
			try
			{	
				uow = new UOW();

				SqlCommand cmd = GetCommand(uow, "spDebitCard_DPISummaryFile");
				cmd.Parameters.Add("@ReportDate", SqlDbType.VarChar , 0).Value = from;			

				return GetData(cmd);		
			}
			finally
			{
				uow.close();
			}
		}

		public static string[] DebitCardAgentFile(DateTime from, int corpid)	
		{
			UOW uow = null;
			
			try
			{	
				uow = new UOW();

				SqlCommand cmd = GetCommand(uow, "spDebitCard_AgentDailyFile");
				cmd.Parameters.Add("@ReportDate", SqlDbType.VarChar , 0).Value = from;			
				cmd.Parameters.Add("@CorpID", SqlDbType.VarChar, 0).Value = corpid;		

				return GetData(cmd);		
			}
			finally
			{
				uow.close();
			}
		}

		public static string[] DebitCardAgentSummaryFile(DateTime from, int corpid)	
		{
			UOW uow = null;
			
			try
			{	
				uow = new UOW();

				SqlCommand cmd = GetCommand(uow, "spDebitCard_AgentSummaryFile");
				cmd.Parameters.Add("@ReportDate", SqlDbType.VarChar , 0).Value = from;			
				cmd.Parameters.Add("@CorpID", SqlDbType.VarChar, 0).Value = corpid;

				return GetData(cmd);		
			}
			finally
			{
				uow.close();
			}
		}

		public static int[] DebitCardGetCorporation(DateTime from)	
		{
			UOW uow = null;
			
			try
			{	
				uow = new UOW();

				SqlCommand cmd = GetCommand(uow, "spDebitCard_GetCorporation");
				cmd.Parameters.Add("@ReportDate", SqlDbType.VarChar , 0).Value = from;			

				return ConvertToInt(GetDataNoHeader(cmd));		
			}
			finally
			{
				uow.close();
			}
		}	
		#endregion

	#region Implementation
		static int[] ConvertToInt(string[] args)
		{
			int[] ints = new int[args.Length];

			for (int i = 0; i < args.Length; i++)
				ints[i] = int.Parse(args[i]);
			
			return ints;
		}
		static string[] GetDataNoHeader(SqlCommand cmd)
		{
			SqlDataReader rdr = cmd.ExecuteReader();
			ArrayList ar = new ArrayList();

			if (!rdr.HasRows)
				return new string[] {};

			while (rdr.Read())
				ar.Add(GetRow(rdr));

			string[] lines = new string[ar.Count];
			ar.CopyTo(lines);
			return lines;
		}
		static string[] GetData(SqlCommand cmd)
		{
			SqlDataReader rdr = cmd.ExecuteReader();
			ArrayList ar = new ArrayList();

			if (!rdr.HasRows)
				return new string[] {};

			ar.Add(GetHeaders(rdr));

			while (rdr.Read())
				ar.Add(GetRow(rdr));

			string[] lines = new string[ar.Count];
			ar.CopyTo(lines);
			return lines;
		}
		static string GetRow(SqlDataReader rdr)
		{
			StringBuilder sb = new StringBuilder();

			for( int i = 0; i < rdr.FieldCount; i++)
			{
				if (i > 0)
					sb.Append(",");
	
				if (rdr[i] != DBNull.Value)
					sb.Append(rdr[i].ToString());
			}
			return sb.ToString();
		}
		static string GetHeaders(SqlDataReader rdr)
		{
			StringBuilder sb = new StringBuilder();

			for( int i = 0; i < rdr.FieldCount; i++)
			{
				if (i > 0)
					sb.Append(",");
				sb.Append(rdr.GetName(i));
			}

			return sb.ToString();
		}
		static SqlCommand GetCommand(UOW uow, string proc)
		{
			SqlCommand cmd = uow.Cn.CreateCommand();

			cmd.CommandType = CommandType.StoredProcedure;	
			cmd.CommandText = proc;
			cmd.CommandTimeout = 0;

			return cmd;
		}
	#endregion
	}
}