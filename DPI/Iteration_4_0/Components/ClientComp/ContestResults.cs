using System;
using System.Configuration;

using DPI.Interfaces;
using DPI.Components;
using DPI.Services;

namespace DPI.ClientComp
{
	public class ContestResults
	{
		public static string GetContestResultsUrl(IUser user)
		{
			string path = (string) new System.Configuration.AppSettingsReader().GetValue("CONTESTPATH",typeof(string));
			string[] pathArr = path.Split(';');

			for(int i = 0; i < pathArr.Length; i++)
				if ( int.Parse(pathArr[i].Substring(0,3)) == StoreSvc.GetCorporation(user.LoginStoreCode).CorpID )
					return pathArr[i].Substring(3,(pathArr[i].Length - 3));

			return null;
		}
		public static bool AllowIncentives(IUser user)
		{
			try
			{	
				string userCorp = StoreSvc.GetCorporation(user.LoginStoreCode).CorpID.ToString().Trim().ToLower();
				string[] excluded 
					= ((string)(new AppSettingsReader().GetValue("EXCLUDED_FROM_INCENTIVES", typeof(string)))).Split(',');

				for (int i = 0; i < excluded.Length; i++)
					if (excluded[i].Trim().ToLower() == userCorp)
						return false;

				return true;
			}
			catch
			{
				return true;
			}
		}
	}
}