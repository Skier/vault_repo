using System;
using System.Text;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;
using System.IO;
 
namespace DPI.Maint
{
	public class DebitCardInvoices
	{	
	#region Constants
		const string AGENT_PATH = "AgentExtranet";
		const string ACCT_PATH = "Finance";
		const string PROCESS = "DebitCard";
		const string AGENT_ROOT = @"agent_root\DebitCard\";
		const string NO_TRANS = "ZERO(0) DEBIT CARD Transactions";
	#endregion

	#region Methods
		public static void Main(string[] reportDate)
		{	
			try
			{
				for (int i = 0 ; i < reportDate.Length ; i++)
				{
					DPI_Err_Log.AddLogEntry( "Debitcard", "Nightly DTS DebitCard Job", 
						"REPORTS WERE REGENERATED FOR Previous Report Date:  " + reportDate[i].ToString());

					CreateDebitCardFiles(DateTime.Parse(reportDate[i]));
				}
				
				if (reportDate.Length == 0)
					CreateDebitCardFiles(DateTime.Today);
			}
			catch(Exception e)
			{
				DPI_Err_Log.AddLogEntry("Debitcard", "Nightly DTS DebitCard Job", 
					"Exception " + e.Message);
			}
		}		
	#endregion

	#region Implementation
		static void CreateDebitCardFiles(DateTime reportDate)
		{
			DPI_Err_Log.AddLogEntry( "Debitcard", "Nightly DTS DebitCard Job", "Started executing all DEBITCARD reports ");

			WritePurposeFiles(reportDate);
			WriteDPIDaily(reportDate);
			WriteDay2(reportDate);  // Commission file once a months, on day 2
			WriteAgentsFiles(reportDate);				
		}
		static void WriteAgentsFiles(DateTime reportDate)
		{
			int[] corps = DebitCardWrapper.DebitCardGetCorporation(reportDate);
			
			if (corps.Length == 0)
			{
				DPI_Err_Log.AddLogEntry( "Debitcard",  
					"Nightly DTS DebitCard Job", 
					"NO RECORDS FOR Daily Agent reports ");
				return;	
			}

			for (int i = 0 ; i < corps.Length ; i++ )
				WriteAgentsFiles(reportDate, corps[i]);
		}
		static void WriteAgentsFiles(DateTime reportDate, int corpId)
		{
			WriteDailyDetailFile(reportDate, corpId);
			WriteDailySummaryFile(reportDate, corpId);
		}
		static void WriteDailyDetailFile(DateTime date, int corpId)
		{
			string[] lines = DebitCardWrapper.DebitCardAgentFile(date, corpId);	

			TextUtil.WriteTextOverride(lines, GetFinancePath(date, "DD", corpId));
			if (corpId == 27)
				return;
			TextUtil.WriteTextOverride(lines, GetExtranetPath(date, "DD", corpId));
		}
		static void WriteDailySummaryFile(DateTime date, int corpId)
		{
			string[] lines = DebitCardWrapper.DebitCardAgentSummaryFile(date, corpId);
			
			TextUtil.WriteTextOverride(lines, GetFinancePath(date, "DS", corpId));
			if (corpId == 27)
				return;
			TextUtil.WriteTextOverride(lines, GetExtranetPath(date, "DS", corpId));
		}

		static void WriteDay2(DateTime reportDate)
		{
			if (reportDate.Day != 2)
				return;
			
			WriteMonthlyCommissionFile(reportDate);
			WriteDebitCardSummaryFile(reportDate);
		}
		static void WriteMonthlyCommissionFile(DateTime date)
		{
			string[] lines = DebitCardWrapper.DebitCardCommissionFile(date);	
			
			if (lines.Length == 0)
			{
				DPI_Err_Log.AddLogEntry( "Debitcard",  
					"Nightly DTS DebitCard Job", 
					"NO RECORDS FOR Monthly Commission reports ");	

				lines = new string[] { NO_TRANS };
			}	
			
			string dPIAccntngFolder = DebitCardWrapper.GetUniversalPath(PROCESS, ACCT_PATH ) + @"agent_root\DebitCard\";
			TextUtil.WriteTextOverride(lines, TextUtil.Folder(dPIAccntngFolder) + "DDCommRpt" + GetFileName(date));
		}
		static void WriteDebitCardSummaryFile(DateTime date)
		{
			string dPIAccntngFolder = DebitCardWrapper.GetUniversalPath(PROCESS, ACCT_PATH ) + @"agent_root\DebitCard\";
			string path = TextUtil.Folder(dPIAccntngFolder) + "DSCommRpt" + GetFileName(date);			
			string[] lines = DebitCardWrapper.DebitCardCommsummaryFile(date);	
			if (lines.Length == 0)
				lines =  new string[] { NO_TRANS };

			TextUtil.WriteTextOverride(lines, path);
		}
		static void WritePurposeFiles(DateTime reportDate)
		{			
			//DPI's copy of Purpose file DTS performs this task of Purpose files 
			//Create Daily Purpose File

			string dPIAccntngFolder = DebitCardWrapper.GetUniversalPath(PROCESS, ACCT_PATH ) + @"agent_root\DebitCard\";
			string[] lines = DebitCardWrapper.DebitCardPurposeFile(reportDate);

			if (lines.Length == 0)
			{	
				DPI_Err_Log.AddLogEntry( "Debitcard", 
					"Nightly DTS DebitCard Job", 
					"NO RECORDS FOR Purpose reports ");
				lines = new string[] { NO_TRANS };
				// Do we need to write zero tran file?
				//return;
			}

			string path =  TextUtil.Folder(dPIAccntngFolder) + "PURPOSE" +  GetFileName(reportDate);
			TextUtil.WriteTextOverride(lines, path);

			WritePuprposeSummary(reportDate);
		}
		static void WritePuprposeSummary(DateTime date)
		{	
			string folder = DebitCardWrapper.GetUniversalPath(PROCESS, ACCT_PATH ) + @"agent_root\DebitCard\";
			string path = TextUtil.Folder(folder) + "PURPOSESUMMARY" + GetFileName(date);
			string[] lines = DebitCardWrapper.DebitCardPurposeSummaryFile(date);
			if (lines.Length == 0)			
				lines = new string[] { NO_TRANS };
			
			TextUtil.WriteTextOverride(lines, path);
		}

		static void WriteDPIDaily(DateTime reportDate)
		{
			//Create Daily DPI Accounting File
			string[] lines = DebitCardWrapper.DebitCardDpiFile(reportDate);	
			
			if (lines.Length == 0)
			{	
				DPI_Err_Log.AddLogEntry( "Debitcard", 
					"Nightly DTS DebitCard Job", 
					"NO RECORDS FOR Daily Accounting reports ");
				lines = new string[] { NO_TRANS };
				// do we need to create zero tran files?
				//return;				
			}
			string path = TextUtil.Folder(GetAgentRoot()) + "DD" + GetFileName(reportDate);
			TextUtil.WriteTextOverride(lines, path);

			WriteDPISummary(reportDate);
		}
		static void WriteDPISummary(DateTime reportDate)
		{
			string[] lines = DebitCardWrapper.DebitCardDpiSumFile(reportDate);	
			string path =  TextUtil.Folder(GetAgentRoot()) + "DS" + GetFileName(reportDate);
			if (lines.Length == 0)
				lines = new string[] { NO_TRANS };

			TextUtil.WriteTextOverride(lines, path);
		}
		static string GetFinancePath(DateTime date, string fileType, int corpId)
		{
			string agentFolder  = DebitCardWrapper.GetUniversalPath(PROCESS, ACCT_PATH ) + @"agent_root\" ;
			agentFolder = TextUtil.Folder(agentFolder + corpId.ToString()+ "\\" + "DebitCard\\" );
			
			return agentFolder + fileType + corpId.ToString()+ GetFileName(date);
		}
		static string GetExtranetPath(DateTime date, string fileType, int corpId)
		{
			string externalAgentFolder = DebitCardWrapper.GetUniversalPath(PROCESS, AGENT_PATH);
			externalAgentFolder = TextUtil.Folder(externalAgentFolder + corpId.ToString() + "\\" + "DebitCard\\" );
			
			return externalAgentFolder + fileType + corpId.ToString()+ GetFileName(date);
		}		
		static string GetAgentRoot(int corpId)
		{
			string path = DebitCardWrapper.GetUniversalPath(PROCESS, ACCT_PATH) 
				       + AGENT_ROOT 
				       + corpId.ToString() 
				       + "\\" + "DebitCard\\";
            
			return TextUtil.Folder(path);
		}
		static string GetAgentRoot()
		{
			return DebitCardWrapper.GetUniversalPath(PROCESS, ACCT_PATH ) + AGENT_ROOT;
		}
		static string GetFileName(DateTime reportDate)
		{
			return reportDate.ToString("MM/dd/yyyy").Replace("/", "") + ".CSV";
		}
	#endregion
	}
}