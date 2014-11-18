//using System;
//using System.Collections;
//using System.Data;
//using System.Text;
//using System.Data.SqlClient; 
//using System.Data.SqlTypes;
//using DPI.Components;
//using DPI.Interfaces;
// 
//namespace DPI.Components
//{
//	//	public struct Stats
//	//	{
//	//		public readonly Corp[] corps;
//	//		public readonly IStoreStats[] stores;
//	//		/*		Constructors		*/
//	//		public Stats(Corp[] corps, IStoreStats[] stores) 
//	//		{
//	//			this.corps = corps;
//	//			this.stores = stores;
//	//		}
//	//	}
//	public class CorpUtil : ICorp
//	{
//		int corpId;
//		Hashtable hashtable;
//		const int Top10 = 25;
//		
//		IStoreStats[] top10Active;
//		IStoreStats[] top10NewCust;
//		IStoreStats[] top10Revenue; 
//
//		/*		Properties		*/
//		public int CorpId { get { return corpId; }}
//		public IStoreStats[] Top10Active { get { return top10Active; }}
//		public IStoreStats[] Top10NewCust { get { return top10NewCust; }}
//		public IStoreStats[] Top10Revenue { get { return top10Revenue; }}
//		public static DateTime FromDate 
//		{
//			get 
//			{
//				//return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);
//				return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-45);
//			}
//		}
//		public static DateTime ToDate 	{get { return DateTime.Now.AddDays(1); }}
//		public IStoreStats[] Stores 
//		{ 
//			get 
//			{ 
//				ICollection col     =  hashtable.Values;
//				IStoreStats[] stores = new IStoreStats[col.Count];
//				col.CopyTo(stores, 0);
//
//				return stores; 
//			}
//		}
//		public int TotalStores { get { return 0; }}
//		/*		Constructors		*/
//		public CorpUtil(int corpId)
//		{
//			this.corpId = corpId;
//			hashtable = new Hashtable();
//		}
//		
//		/*		Methods		*/
//		public static Stats BuildStats(UOW uow)
//		{
//			Hashtable corps = new Hashtable();
//			
//			LoadActCust(ref corps, uow);
//			LoadLocalRev(ref corps, uow);
//			LoadNewCust(ref corps, uow);
//			LoadWirelessRev(ref corps, uow);
//		
//			Stats stats = new Stats(GetCorps(corps), Convert(uow, corps));
//
//			return stats;
//		}
//		
//		/*		Static implementation		*/
//		static Corp[] GetCorps(Hashtable htable)
//		{
//			ArrayList ar = new ArrayList(2000);
//			IDictionaryEnumerator iter = htable.GetEnumerator();
//
//			while(iter.MoveNext())
//				if (iter.Value != null)
//					ar.Add((Corp)iter.Value);
//			
//			Corp[] corps = new Corp[ar.Count];
//			ar.CopyTo(corps);
//
//			return corps;
//		}
//		static IStoreStats[] Convert(UOW uow, Hashtable corps)
//		{
//			ArrayList ar = new ArrayList(10000);
//			IDictionaryEnumerator iter = corps.GetEnumerator();
//
//			while(iter.MoveNext())
//				if (iter.Value != null)
//				{
//					if (iter.Value is Corp)
//						ar.AddRange(((Corp)iter.Value).GetStores());
//					else
//					{
//						object o = iter.Value;
//					}
//				}
//			IStoreStats[] stores = new IStoreStats[ar.Count];
//			ar.CopyTo(stores);
//			return stores;
//		}
//		static void LoadNewCust(ref Hashtable hashtable, UOW uow)
//		{
//			IStoreStats[] stats = StoreStats2.GetNewCust(uow, FromDate, ToDate); 
//
//			for (int i = 0; i < stats.Length; i++)
//			{
//				if (!hashtable.ContainsKey(stats[i].CorpId))
//					hashtable.Add(stats[i].CorpId, new Corp(stats[i].CorpId));
//				
//				((Corp)hashtable[stats[i].CorpId]).AddNewCust(stats[i].StoreCode, stats[i].MDT_NewCust,stats[i].StoreNumber);
//			}
//		}
//		static void LoadLocalRev(ref Hashtable hashtable, UOW uow)
//		{
//			IStoreStats[] stats = StoreStats2.GetRevenues(uow, FromDate, ToDate); 
//
//			for (int i = 0; i < stats.Length; i++)
//			{
//				if (!hashtable.ContainsKey(stats[i].CorpId))
//					hashtable.Add(stats[i].CorpId, new Corp(stats[i].CorpId));
//				
//				((Corp)hashtable[stats[i].CorpId]).AddRevenue(stats[i].StoreCode, stats[i].Revenue,
//					stats[i].StoreNumber, stats[i].LdRevenue);
//			}
//		}
//		static void LoadActCust(ref Hashtable hashtable, UOW uow)
//		{
//			IStoreStats[] stats = StoreStats2.GetActiveCust(uow);
//
//			for (int i = 0; i < stats.Length; i++)
//			{
//				if (!hashtable.ContainsKey(stats[i].CorpId))
//					hashtable.Add(stats[i].CorpId, new Corp(stats[i].CorpId));
//				
//				//object o = hashtable[stats[i].CorpId];
//				((Corp)hashtable[stats[i].CorpId]).AddActCust(stats[i].StoreCode, stats[i].ActiveCust, stats[i].StoreNumber);
//			}
//		}
//		static void LoadWirelessRev(ref Hashtable hashtable, UOW uow)
//		{
//			IStoreStats[] stats = StoreStats2.GetWirelessRevenue(uow, FromDate, ToDate);
//
//			for (int i = 0; i < stats.Length; i++)
//			{
//				if (!hashtable.ContainsKey(stats[i].CorpId))
//					hashtable.Add(stats[i].CorpId, new Corp(stats[i].CorpId));
//				
//				((Corp)hashtable[stats[i].CorpId]).AddWireless(stats[i].StoreCode, stats[i].WirelessRevenue, stats[i].StoreNumber);
//			}
//		}
//		
//		/*		Instance implementation		*/
//	
//		public void AddRevenue(string storeCode, decimal local, string storeNumber, decimal ld)
//		{
//			IStoreStats store = FindStore(storeCode);
//			store.StoreNumber = storeNumber;
//			store.LocalRevenue = local;
//			store.LdRevenue    = ld;
//		}
//		public void AddActCust(string storeCode, int actCust, string storeNumber)
//		{
//			IStoreStats store  = FindStore(storeCode);
//			store.StoreNumber = storeNumber;	
//			store.ActiveCust  = actCust;
//		}
//		public void AddNewCust(string storeCode, int newCust, string storeNumber)
//		{
//			IStoreStats store = FindStore(storeCode);
//			store.StoreNumber = storeNumber;	
//			store.MDT_NewCust = newCust;
//		}
//		public void AddWireless(string storeCode, decimal wireless, string storeNumber)
//		{
//			IStoreStats store = FindStore(storeCode);
//			store.StoreNumber = storeNumber;	
//			store.WirelessRevenue = wireless;
//		}
//		public IStoreStats[] GetStores()
//		{
//			IStoreStats[] stores = (IStoreStats[])Stores;
////			for (int i = 0; i < stores.Length; i++)
////				stores[i].TotalStores = stores.Length;
//		
//			RankActCust(ref stores);
//			RankMTDsales(ref stores);
//			RankRevenue(ref stores);
//			return stores;
//		}
//		void RankRevenue(ref IStoreStats[] stores)
//		{
//			IStoreStats[] top10Revenue = new IStoreStats[10];
//
//			decimal[] act = new decimal[stores.Length];
//			for(int i = 0; i < stores.Length; i++)
//				act[i] = decimal.MaxValue - stores[i].Revenue;
//
//			Array.Sort(act, stores);
//			
//			int rank = 1;
//			stores[0].RevenueRank = rank;
//			
//			for (int i = 1; i < stores.Length; i++)
//			{
//				if (stores[i].Revenue == stores[i-1].Revenue)
//					stores[i].RevenueRank = stores[i-1].RevenueRank;
//				else
//					stores[i].RevenueRank = ++rank;
//			}
//			SetTop10Revenue(stores);
//		}		
//		
//		void RankActCust(ref IStoreStats[] stores)
//		{
//			IStoreStats[] top10Active = new IStoreStats[10];
//
//			int[] act = new int[stores.Length];
//			for(int i = 0; i < stores.Length; i++)
//				act[i] = int.MaxValue - stores[i].ActiveCust;
//
//			Array.Sort(act, stores);
//			
//			int rank = 1;
//			stores[0].ActiveCustRank = rank;
//			
//			for (int i = 1; i < stores.Length; i++)
//			{
//				if (stores[i].ActiveCust == stores[i-1].ActiveCust)
//					stores[i].ActiveCustRank = stores[i-1].ActiveCustRank;
//				else
//					stores[i].ActiveCustRank = ++rank;
//			}
//			SetTop10Active(stores);
//		}
//		void SetTop10Revenue(IStoreStats[] stores)
//		{
//			ArrayList ar = new ArrayList();
//			
//			int max = Top10 > stores.Length ? stores.Length : Top10;
//
//			for (int i = 0; i < max; i++)
//				if (stores[i].Revenue > 0)
//					ar.Add(stores[i]);
//		
//			top10Revenue = new IStoreStats[ar.Count];
//			ar.CopyTo(top10Revenue);	
//		}
//		void SetTop10Active(IStoreStats[] stores)
//		{
//			ArrayList ar = new ArrayList();
//			
//			int max = Top10 > stores.Length ? stores.Length : Top10;
//
//			for (int i = 0; i < max; i++)
//				if (stores[i].ActiveCust > 0)
//					ar.Add(stores[i]);
//		
//			top10Active = new IStoreStats[ar.Count];
//			ar.CopyTo(top10Active);	
//		}
//		void SetTop10NewCust(IStoreStats[] stores)
//		{
//			ArrayList ar = new ArrayList();
//			
//			int max = Top10 > stores.Length ? stores.Length : Top10;
//
//			for (int i = 0; i < max; i++)
//				if (stores[i].MDT_NewCust > 0)
//					ar.Add(stores[i]);
//		
//			top10NewCust = new IStoreStats[ar.Count];
//			ar.CopyTo(top10NewCust);	
//		}
//		IStoreStats FindStore(string storeCode)
//		{
//			if (hashtable.ContainsKey(storeCode))
//				return (IStoreStats)hashtable[storeCode];
//		
//			IStoreStats store = new StoreStats2(storeCode);
//			store.CorpId = this.corpId;
//			hashtable.Add(storeCode, store);
//			return store;
//		}
//		void RankMTDsales(ref IStoreStats[] stores)
//		{
//			int rank = 1;
//
//			int[] mtd = new int[stores.Length];
//			for(int i = 0; i < mtd.Length; i++)
//				mtd[i] = int.MaxValue - stores[i].MDT_NewCust;
//
//			Array.Sort(mtd, stores);
//			stores[0].MDT_NewCustRank = rank;
//
//			for (int i = 1; i < stores.Length; i++)
//			{
//		//		stores[i].TotalStores = stores.Length;
//
//				if (stores[i].MDT_NewCust == stores[i-1].MDT_NewCust)
//					stores[i].MDT_NewCustRank = stores[i-1].MDT_NewCustRank;
//				else
//					stores[i].MDT_NewCustRank = ++rank;
//			}
//
//			SetTop10NewCust(stores);
//		}
//	}
//}