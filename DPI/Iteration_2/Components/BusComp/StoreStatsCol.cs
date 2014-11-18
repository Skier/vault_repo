using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]  
	public class StoreStatsCol
	{
		/*        Data        */
		static Hashtable stats;
		static Corp[] corps;
		static DateTime lastLoad;
		static ICorporation[] corporations;

		/*		Properties		*/
		static Corp[] Corps
		{
			get 
			{
				if (DateTime.Now.AddMinutes(- Const.REF_INTERVAL) > lastLoad)
					LoadData();
				
				return corps;
			}
		}
		
		
		/*		Constructors		*/
		StoreStatsCol() 
		{
			LoadData();
			OperationMessenger.RefreshData += new EventHandler(OnRefresh);
		}
		
		
		/*		Methods		*/
		public static Corp[] GetCorp()
		{
			if (corps == null)
				new StoreStatsCol();

			return corps;
		}
		public static Corp GetCorp(int id)
		{
			if (corps == null)
				new StoreStatsCol();

			for( int i = 0; i < corps.Length; i++)
				if (corps[i].CorpId == id)
					return corps[i];
			
			return null;
		}
		public static ICorporation GetCorporation(int id)
		{
			if (corporations == null)
				new StoreStatsCol();

			for( int i = 0; i < corporations.Length; i++)
				if (corporations[i].CorpID == id)
					return corporations[i];
			
			throw new ArgumentException("Corporation " + id.ToString() + " is not found");
		}
		public static ICorporation GetCorporation(string storeCode)
		{
			return GetCorporation((GetStoreStat(storeCode)).CorpId);//.ActualCorpId);
		}
		public static IStoreStats GetStoreStat(string store)
		{	
			if (store == null)
				throw new ArgumentNullException("Store Code is required");
			
			if (store == string.Empty)
				throw new ArgumentNullException("Store Code is required");
			
			if (stats == null)
				new StoreStatsCol();
			
			if (stats.ContainsKey(store.Trim().ToLower()))
				return (IStoreStats)stats[store.Trim().ToLower()];
			
			UOW uow = null;

			try
			{
				uow = new UOW();
				DPI_Err_Log.AddLogEntry(uow, "StoreStatCol-Error", "Unknown", "Can't find store stats for " + store);
				return null;
			}
			finally
			{
				uow.close();
			}

			//throw new ApplicationException("Can't find Store '" + store + "'") ;
		}

		
		/*		Implementation		*/
		static void OnRefresh(object sender, EventArgs ea)
		{
			LoadData();
		}
		static void LoadData()
		{
			UOW uow = null;

			try
			{
				uow = new UOW();
				uow.Service = "StoreStatsCol.LoadData()";
				Stats sts =  Corp.BuildStats(uow);
				
				if ((sts.corps.Length == 0) || (sts.stores.Length == 0))
				{
					DPI_Err_Log.AddLogEntry(uow, "StoreStatCol-Error", "Unknown", "StoreStats are missing");
					throw new ArgumentNullException("StoreStats");
				}

				stats = new Hashtable(sts.stores.Length * 2);

				for (int i = 0; i < sts.stores.Length; i++)
					if (sts.stores[i].StoreCode != null)
						stats.Add(sts.stores[i].StoreCode.Trim().ToLower(), sts.stores[i]);
				
				corps = sts.corps;

				corporations = Corporation.getAll(uow);
				lastLoad = DateTime.Now;
				DPI_Err_Log.AddLogEntry(uow, ErrLogSubSystems.StoreStatCol.ToString(), "Unknown", "StoreStats loaded at:" + lastLoad.ToString());
			}
			catch(Exception e)
			{
				string s = e.Message;
			}
			finally
			{
				uow.close();
			}
		}
	}
}