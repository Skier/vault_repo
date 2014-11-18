using System;
using System.Text;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;
using System.IO;

 
namespace DPI.Maint
{
	[Serializable]
	public class StoreStatsLoader
	{

	#region Constants
		const string ENV            = "env";
		const string LOGFILE        = "logfile";
		const string DEFAULTLOGPATH = @"wcstatslog.txt";
		const string MAILTO		= "alertto";
		const string MAILFROM = "alertfrom";
		const string SMTPSERVER = "smtpserver";
		const string MAILSUBJECT = "alertsubject";
		const string MAILMESSAGE = "alertmessage"; 
	#endregion

	#region Data
		static string logpath = DEFAULTLOGPATH;
		static DateTime startTime = DateTime.Now;		
		static string mailTo = "";
		static string mailFrom = "";
		static string smtpServer = "";
		static string mailSubject = "";
		static string mailMessage = "";

	#endregion

	#region Methods
		public static void Main(string[] configPath)
		{	
			if (!Validate(configPath))
				return;			

			try
			{
				//SetLoaderSetings(@"C:\work\statsloader\wcstats.txt");
				SetLoaderSetings(configPath[0]);
				LogStart();
				Load();
				ReportResults();
			}
			catch(Exception e)
			{
				SendMail(e.Message);
				TextUtil.WriteText("Exception: " + e.Message, logpath);
				TextUtil.WriteText("Excecution is terminated", logpath);
			}
		}
	#endregion
		
	#region Implementation
		static void SendMail(string exception)
		{
			try
			{
				MailMessage msg = new MailMessage();

				msg.EmailFrom = mailFrom;
				msg.AddEmailTo(mailTo);
				msg.EmailMessageType = MessageType.Text;
				msg.EmailMessage = mailMessage;
				msg.EmailMessage += " Exception: " + exception;
				msg.EmailMessage += " Environment: " + Conn.Env;
				msg.EmailSubject = mailSubject;
			
				msg.SendMail();
			}
			catch (Exception ex)
			{
				TextUtil.WriteText("Send Mail Error. Exception: " + ex.Message, logpath);
			}

		}
		static void LogStart()
		{
			TextUtil.WriteText("WCStart started: " + startTime.ToString() + ". Database to be updated: " + Conn.Env, logpath);
		}
		static bool Validate(string[] configPath)
		{
			if (configPath.Length == 0 )
			{
				TextUtil.WriteText("Parameter file not found", logpath);
				TextUtil.WriteText("Excecution was terminated abruptly", logpath);
				return false;
			}

			if (!File.Exists(configPath[0]))
			{
				TextUtil.WriteText(("File: " + configPath[0]) + "does not exist", logpath);
				TextUtil.WriteText("Excecution was terminated abruptly", logpath);
				return false;
			}
			return true;
		}
		static void Load()
		{
			UOW uow = null;

			try 
			{
				uow = new UOW();
				
				Stats stats = Corp.LoadStats(uow);
	
				uow.BeginTransaction();
				
				StoreStats2.DeleteDate(uow, DateTime.Now);
				for (int i = 0; i < stats.stores.Length; i++) 
					new StoreStats2(uow, (IStoreStats)stats.stores[i]).add();

				uow.commit();
			}
			finally
			{
				uow.close();
			}
		}
	
		static void SetLoaderSetings(string path)
		{
			string[] parsed = CombineLines(path).Split(";".ToCharArray()); // combines all lines into a string
			string env = null;

			for (int i = 0; i < parsed.Length; i++)
			{
				if (parsed[i].ToLower().IndexOf(ENV) > -1)
				{
					Conn.Env = parsed[i].Substring(
						parsed[i].IndexOf("\"") + 1, 
						parsed[i].LastIndexOf("\"") - parsed[i].IndexOf("\"") -1).ToUpper();
					env = Conn.Env;
				}
				
				if (parsed[i].ToLower().IndexOf(LOGFILE) > -1)
					logpath = parsed[i].Substring(parsed[i].IndexOf("\"") + 1, 
						parsed[i].LastIndexOf("\"") - parsed[i].IndexOf("\"") -1).ToLower();

				if (parsed[i].ToLower().IndexOf(MAILTO) > -1)
					mailTo = parsed[i].Substring(parsed[i].IndexOf("\"") + 1, 
						parsed[i].LastIndexOf("\"") - parsed[i].IndexOf("\"") -1).ToLower();
 
				if (parsed[i].ToLower().IndexOf(MAILFROM) > -1)
					mailFrom = parsed[i].Substring(parsed[i].IndexOf("\"") + 1, 
						parsed[i].LastIndexOf("\"") - parsed[i].IndexOf("\"") -1).ToLower();

				if (parsed[i].ToLower().IndexOf(SMTPSERVER) > -1)
					smtpServer = parsed[i].Substring(parsed[i].IndexOf("\"") + 1, 
						parsed[i].LastIndexOf("\"") - parsed[i].IndexOf("\"") -1).ToLower();

				if (parsed[i].ToLower().IndexOf(MAILSUBJECT) > -1)
					mailSubject = parsed[i].Substring(parsed[i].IndexOf("\"") + 1, 
						parsed[i].LastIndexOf("\"") - parsed[i].IndexOf("\"") -1).ToLower();

				if (parsed[i].ToLower().IndexOf(MAILMESSAGE) > -1)
					mailMessage = parsed[i].Substring(parsed[i].IndexOf("\"") + 1, 
						parsed[i].LastIndexOf("\"") - parsed[i].IndexOf("\"") -1).ToLower();				
				
			}
 			if (env == null)
				throw new ArgumentException("Environment parameter is incorrect or missing");

				
//			Console.WriteLine("Conn.env: {0}", Conn.Env);
//			Console.WriteLine("logpath: {0}", logpath);
		}
		static string CombineLines(string path)
		{
			string[] str = TextUtil.ReadText(path);

			if (str.Length == 0)
				throw new ArgumentException("Config file is empty");

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < str.Length; i++)
				sb.Append(str[i]);
			
			return sb.ToString();
		}
		static void ReportResults()
		{
			UOW uow = null;
			int dateTotal;
			
			try
			{
				uow = new UOW();
				dateTotal = StoreStats2.GetCount(uow, DateTime.Today);
			}
			finally
			{
				uow.close();
			}
			
			TextUtil.WriteText(dateTotal.ToString() + " new rows ware added.", logpath);
			TextUtil.WriteText("Succsessfully Completed @ :" + DateTime.Now.ToString(), logpath);
		}
	#endregion

	}
}