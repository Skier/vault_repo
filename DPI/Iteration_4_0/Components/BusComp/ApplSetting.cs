using System;
using System.Collections;
using System.Data;
using System.Text;

using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Components
{
	public class ApplSetting
	{
		public static string GetPurposeUrl()
		{
			try
			{
				string url = GetUrl("PurposeWebServiceURL");
				return url;
			}
			catch (Exception ex)
			{
				DPI_Err_Log.AddLogEntry("GetPurposeUrl()", "N/A", ex.Message + ", " + ex.StackTrace);
				throw ex;
			}
		}
		public static string GetPurposeXsdUrl()
		{
			try
			{
				string url = GetUrl("PurposeXsdURL");
				return url;
			}
			catch (Exception ex)
			{
				DPI_Err_Log.AddLogEntry("GetPurposeUrl()", "N/A", ex.Message + ", " + ex.StackTrace);
				throw ex;
			}
		}
		static string GetUrl(string urlName)
		{
			return System.Configuration.ConfigurationSettings.AppSettings[urlName]; 
		}
	}
}