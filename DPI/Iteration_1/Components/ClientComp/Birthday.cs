using System;
using System.Text.RegularExpressions;
using DPI.Interfaces;
 
namespace DPI.ClientComp
{
	public class Birthday
	{
		static string dbEmpty = @"__/__/____";
		static string uiEmpty = "";
		public static bool IsValid(string bday)
		{
			string s = bday.Trim();

			Regex regex;

			switch (s.Length)
			{
				case 5 :
				{
					regex = new Regex(@"^\d\d/\d\d");
					break;
				}	
				case 10:
				{
					regex = new Regex(@"\d\d/\d\d/\d\d\d\d");
					break;;
				}	
				default :
					return false;
			}
			if (!regex.IsMatch(s))
				return false;
			
			return true;
		}
		public static string Show(string dbBday)
		{	
			if  (dbBday == null)
				return uiEmpty;

			if (dbBday.Trim() == dbEmpty)
				return uiEmpty;
			
			if (!IsValid(dbBday))
				return uiEmpty;
				
			return dbBday;
		}	
		public static string Store(string uiBday)
		{
			string s = dbEmpty;
			if (!(uiBday == null))
			{
				s = uiBday.Trim();			
				if (IsValid(s))
				{
					s = (s.Length == 5) ? s + @"/____" : s;
				}
			}			
			return s;
		}
	}
}