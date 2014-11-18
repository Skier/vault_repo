using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;
using System.IO;

 
namespace DPI.Maint
{
	[Serializable]
	public class CGMUtil
	{

	#region		Constants
		const string ENV            = "env";
		const string LOGFILE        = "logfile";
		const string DEFAULTLOGPATH = @"wccgmlog.txt";
		const string MAILTO		= "alertto";
		const string MAILFROM = "alertfrom";
		const string SMTPSERVER = "smtpserver";
		const string MAILSUBJECT = "alertsubject";
		const string MAILMESSAGE = "alertmessage"; 
		const string FILE_NAME_LINE_TYPE = " CGM Line Type ID Mapping.txt";
		const string FILE_NAME_LINEDISC = " CGM Line Disconnect_Reconnect File.txt";
		const string FILE_NAME_SUBSCRIBER = "  SUBSCRIBER DATA - USOC - Level Customer Charges.txt";
		const string IS_OVERRIDE_REPORT_DATE = "isoverridereportdate";
		const string REPORT_MONTH				= "reportmonth";
		const string REPORT_YEAR				= "reportyear";
		const string OUTPUT_PATH				= "outputpath";
	#endregion

	#region         Data        
		static string logpath = DEFAULTLOGPATH;
		static DateTime startTime = DateTime.Now;		
		static string mailTo = "omar.azad@dpiteleconnect.com";
		static string mailFrom = "omar.azad@dpiteleconnect.com";
		static string smtpServer = "mail.dpiteleconnect.com";
		static string mailSubject = "WC CGM-ALERT";
		static string mailMessage = "";
		static int month = DateTime.Now.AddMonths(-1).Month; 
		static int year = DateTime.Now.AddMonths(-1).Year;
		static DateTime from = new DateTime(year, month, 1);
		static string outPutPath = "";		
	#endregion

	#region   Main

		public static void Main(string[] configPath)
		{	
			if (!Validate(configPath))
				return;			

			try
			{
				SetCGMSetings(configPath[0]);
				//SetCGMSetings(@"c:\wccgm.txt");
				LogStart();
				CreateCGMFiles(configPath);
				LogEnd();
			}
			catch(Exception e)
			{
				SendMail(e.Message);
				TextUtil.WriteText("Exception: " + e.Message, logpath);
				TextUtil.WriteText("Excecution is terminated", logpath);
			}			
		}
	#endregion

	#region   Static Methods
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
		static void CreateCGMFiles(string[] configPath)
		{
			try
			{				
				//query # 2
				TextUtil.WriteText("Writting query #2 (CGM SUBSCRIBER DATA USOC Level Customer Charges) for the Month : " + month.ToString() + " and Year: " + year.ToString(), logpath);
				
				string str = CGMWrapper.SubscriberDataUSOC(month, year);
				TextUtil.WriteText(str, outPutPath
										+ month.ToString() 
										+ " - " + year.ToString() 
										+ " - " + FILE_NAME_SUBSCRIBER);
				TextUtil.WriteText("Finished query #2.", logpath);
				//Query # 3
				TextUtil.WriteText("Writting query #3 (CGM LineType ID Maping) from: " + from.ToShortDateString(), logpath);
				str = CGMWrapper.LineTypeIDMapping(from);				
				TextUtil.WriteText(str, outPutPath 
									    + from.Month.ToString() 
									    + " - " + from.Year.ToString() 
									    + " - " + FILE_NAME_LINE_TYPE);

				TextUtil.WriteText("Finished query #3.", logpath);
				
				//Query # 4
				TextUtil.WriteText("Writting query #4 (CGM Line Disconnect Reconnect File) for the Month : " + month.ToString() + " and Year: " + year.ToString(), logpath);
				str = CGMWrapper.LineDisconnectReconnectFile(month, year);				
				TextUtil.WriteText(str, outPutPath
										+ from.Month.ToString() 
										+ " - " + from.Year.ToString() 
										+ " - " + FILE_NAME_LINEDISC);

				TextUtil.WriteText("Finished query #4.", logpath);
			}
			catch (Exception ex)
			{
				throw new ArgumentException("Error: " + ex.Message);
			}			
		}
	
		static void SetCGMSetings(string path)
		{
			string[] parsed = CombineLines(path).Split(";".ToCharArray()); // combines all lines into a string
			string env = null;
			string overRideReportDate = null;
			string reportMonth        = null;
			string reportYear		  = null;

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
				
				if (parsed[i].ToLower().IndexOf(IS_OVERRIDE_REPORT_DATE) > -1)
					overRideReportDate = parsed[i].Substring(parsed[i].IndexOf("\"") + 1, 
						parsed[i].LastIndexOf("\"") - parsed[i].IndexOf("\"") -1).ToLower();

				if (parsed[i].ToLower().IndexOf(REPORT_MONTH) > -1)
					reportMonth = parsed[i].Substring(parsed[i].IndexOf("\"") + 1, 
						parsed[i].LastIndexOf("\"") - parsed[i].IndexOf("\"") -1).ToLower();

				if (parsed[i].ToLower().IndexOf(REPORT_YEAR) > -1)
					reportYear = parsed[i].Substring(parsed[i].IndexOf("\"") + 1, 
						parsed[i].LastIndexOf("\"") - parsed[i].IndexOf("\"") -1).ToLower();

				if (parsed[i].ToLower().IndexOf(OUTPUT_PATH) > -1)
					outPutPath = parsed[i].Substring(parsed[i].IndexOf("\"") + 1, 
						parsed[i].LastIndexOf("\"") - parsed[i].IndexOf("\"") -1).ToLower();
			}
			if (overRideReportDate == null)
				throw new ArgumentException("overRideReportDate parameter is incorrect or missing");

			if (overRideReportDate.Trim().ToLower() == "yes")
			{
				month = int.Parse(reportMonth);
				year  = int.Parse(reportYear);
				from = new DateTime(year, month, 1);
			}

			if (env == null)
				throw new ArgumentException("Environment parameter is incorrect or missing");
	
		}
		static void LogStart()
		{
			TextUtil.WriteText("CGM started: " + startTime.ToString() + ". Database: " + Conn.Env, logpath);
		}		
		static void LogEnd()
		{
			TextUtil.WriteText("CGM finished: " + DateTime.Now.ToString() + ". Database: " + Conn.Env, logpath);
		}
		static string CombineLines(string path)
		{
			string[] str = TextUtil.ReadText(path);

			if (str.Length == 0)
				throw new ArgumentException("Config file is empty");

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < str.Length; i++)
			{
				if (!str[i].Trim().StartsWith("***")) //discard lines starts with ***
					sb.Append(str[i]);
			}
			
			return sb.ToString();
		}
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

//				Smtp smtp = new Smtp();
//
//				smtp.SmtpServer = smtpServer;
//			
//				smtp.SendEmail(msg);
			}
			catch (Exception ex)
			{
				TextUtil.WriteText("Send Mail Error. Exception: " + ex.Message, logpath);
			}

		}
	#endregion

	}
}