using System;
using System.Text;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;
using System.IO;

 
namespace DPI.Maint
{
	public class DelimiterChanger
	{

		/*		Constants		*/		
		const string DEFAULTLOGPATH = @"DelimiterLog.txt";
		/*        Data        */
		static string logpath = DEFAULTLOGPATH;
		static string prevDelimiter = ",";
		static string currentDelimiter = "|";

		public static void Main(string[] filePath)
		{
			if (!Validate(filePath))
				return;

			ChangeDelimiter(filePath[0]);
			//ChangeDelimiter(@"c:\queryresult.txt");
				
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
		static void ChangeDelimiter(string path)
		{
			string[] rdrs = TextUtil.ReadText(path);
			ArrayList ar = new ArrayList();
			for (int i = 0; i < rdrs.Length; i++)
				ar.Add(rdrs[i].Replace(prevDelimiter, currentDelimiter));
 
			string [] wts = new string[ar.Count];
			ar.CopyTo(wts);
			TextUtil.TruncateFile(path);
			TextUtil.WriteText(wts, path);			
		}
	}
}