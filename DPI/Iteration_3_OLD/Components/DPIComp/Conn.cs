using System;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;

using DPI.Interfaces;

namespace DPI.Components  
{
	public class Conn
	{
		static string env = 
		// 	Const.TST;
		  Const.DEV;
		//  Const.PROD; 
		//  Const.STG;

		/*		Properties		*/
		public static string Env 
		{ 
			get { return env;	} 
			set { env = value;	}
		} 
		
		/*		Methods		*/
		public static SqlConnection GetConn() 
		{
			SqlConnection cn = new SqlConnection(conString());
			cn.Open(); 
			return cn;
		}
		
		static void OverrideEnv()
		{	
			env = ConfigurationSettings.AppSettings["Database"];
		}

		public static string conString()
		{
			OverrideEnv();
			return ConfigurationSettings.AppSettings["DBConnString"];
			
		}
	}
}