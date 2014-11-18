using System;
using System.Configuration;
using System.Collections;
using System.Threading;

using BillSoft.EZTaxNET;
using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Components
{
	public class TaxWrapper
	{	
	#region Data
		static EZTax ezTax;
		static bool ezTaxStarted = false;
		static Hashtable sessionPool;
		const int POOL_SIZE = 10;
		static file_path paths;
	#endregion

	#region Properties	
		public static EZTax EZTax
		{
			get 
			{
				if (ezTax == null)
					new TaxWrapper();

				return ezTax;
			}
		}
		static file_path Paths 
		{
			get 
			{
				if (paths == null)
					paths =  GetPaths();
				
				return paths;
			}
		}
		static Int16 IsLoggingReq 
		{	
			get	
			{
				return EZTaxConst.EZ_TRUE;
			}
		}		
	#endregion

	#region Constructors
		TaxWrapper() 
		{		
			lock ((object)ezTaxStarted)
			{
				ezTax = new EZTax();

				EZTax.EZTaxStart();
				MakeSessionPool();
				
				ezTaxStarted = true;
				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.EZTaxWrapper.ToString(), "N/A", "EZTax started");
			}
		}
	#endregion

	#region Methods
		public static int   AcquireSession()
		{
			lock (sessionPool)
			{
				foreach (object item in sessionPool.Keys)
				{
					if ((bool)sessionPool[(int)item] == false)
					{
						sessionPool[(int)item] = true;
						return (int)item;
					}
				}

				Thread.Sleep(100); //instead of creating new session wait for 100 ms and try again
				
				foreach (object item in sessionPool.Keys)
				{
					if ((bool)sessionPool[(int)item] == false)
					{
						sessionPool[(int)item] = true;
						return (int)item;
					}
				}

				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.EZTaxWrapper.ToString(), "N/A", 
					"No more predefined sessions (" 
					+ POOL_SIZE.ToString() 
					+ ") are available. Trying to acquire a new session dynamically");
				
				int session = AddSession();
				sessionPool[session] = true;
				return session;
			}
		}

		public static void  ReleaseSession(int sessionID)
		{
			lock (sessionPool)
			{
				if (sessionPool.ContainsKey(sessionID))
				{
					sessionPool[sessionID] = false;
					return;
				}
			}
			throw new ArgumentException("Invalid EazyTax session ID = " + sessionID.ToString());		
		}

		public static void  Dispose()
		{
			try
			{
				lock (sessionPool)
					foreach (object item in sessionPool.Keys)
					{
						int sessionID = (int) item;
						EZTax.EZTaxClose(sessionID, EZTaxConst.ZIPCODE);
						ezTax.EZTaxExitSession(sessionID);
					}

				EZTax.EZTaxExit();
			}
			catch {}
		}

	#endregion

	#region Implementation
		static void MakeSessionPool()
		{
			sessionPool = new Hashtable(POOL_SIZE);	
	
			lock (sessionPool)
				for (int i = 0; i < POOL_SIZE; i++)
					AddSession();
		}
		static int AddSession()
		{
			int sessionID = -1;
		
			try	
			{
				if (!ezTax.EZTaxInit(IsLoggingReq, EZTaxConst.EZ_TRUE, EZTaxConst.CACHE_ALL, Paths, out sessionID))
				{
					DPI_Err_Log.AddLogEntry(ErrLogSubSystems.EZTaxWrapper.ToString(),  "N/A", "EZTax.EZTaxInit() failed");
					throw new Exception("EZTax.EZTaxInit() failed");
				}

				EZTax.EZTaxOpen(sessionID, EZTaxConst.ZIPCODE, EZTaxConst.CACHE_ALL);
				sessionPool.Add(sessionID, false);

				return sessionID;
			}
			catch (Exception ex)
			{
				DPI_Err_Log.AddLogEntry("TaxWrapper",  "N/A", "EZTaxInit.AddSession() --  " + ex.Message);
				throw new Exception("EZTax.EZTaxInit() failed");
			}
		}
		static file_path GetPaths()
		{
			file_path paths = new file_path();
			paths.EZTax_customer_key = Const.EazyTaxDir + "cust_key";

			paths.EZTax_log          = Const.EazyTaxDir + "EZTax.log"; 
			paths.EZTax_status       = Const.EazyTaxDir + "EZTax.sta";			
			paths.EZTax_data         = Const.EazyTaxDir + "EZTax.dat";
			paths.EZTax_location     = Const.EazyTaxDir + "EZDesc.dat";

			paths.EZTax_temp_file    = Const.EazyTaxDir + "Temp77777.dat";
			paths.EZTax_over         = ""; 
			paths.EZTax_IDX          = Const.EazyTaxDir + "EZTax.idx";
			paths.EZTax_DLL          = Const.EazyTaxDir + "EZTax.dll";
			
			paths.EZTax_npanxx       = Const.EazyTaxDir + "EZTax.npa";
			paths.EZTax_zip          = Const.EazyTaxDir + "EZZIP.dat";
			paths.EZTax_pcode        = Const.EazyTaxDir + "EZTax.pcd";
			paths.EZTax_jcode        = Const.EazyTaxDir + "EZTax.jtp";
			
			return paths;
		}
	#endregion
	}
}