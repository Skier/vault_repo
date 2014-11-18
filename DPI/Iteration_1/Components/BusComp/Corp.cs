using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Data.SqlClient; 
using System.Data.SqlTypes;
using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Components
{
	public struct Stats
	{
		public readonly Corp[] corps;
		public readonly IStoreStats[] stores;
		
		/*		Constructors		*/
		public Stats(Corp[] corps, IStoreStats[] stores)
		{
			this.corps = corps;
			this.stores = stores;
		}
	}
	public class Corp : ICorp
	{
		/*		Data		*/
		const int Top10 = 25;		
		int corpId;
		int totalStores;
		
		Hashtable stores;
		static Hashtable corporations;

		IStoreStats[] top10Active;
		IStoreStats[] top10NewCust;
		IStoreStats[] top10Revenue;

		/*		Properties		*/
		public int CorpId { get { return corpId; }}		
		public IStoreStats[] Top10Active 
		{ 
			get 
			{ 
				if (top10Active == null)
					return new IStoreStats[0];

				return top10Active; 
			}
		}
		public IStoreStats[] Top10NewCust 
		{
			get 
			{ 
				if (top10NewCust == null)
					return new IStoreStats[0];

				return top10NewCust; 
			}
		}
		public IStoreStats[] Top10Revenue 
		{
			get 
			{ 
				if (top10Revenue == null)
					return new IStoreStats[0];

				return top10Revenue; 
			}
		}

		public int TotalStores
		{ 
			get { return totalStores; }
			set { totalStores = value; }
		}

		public static DateTime FromDate 
		{
			get { return new DateTime(DateTime.Now.Year, DateTime.Now.Month,1).AddDays(-1); }
			//get { return new DateTime(DateTime.Now.Year, DateTime.Now.Month,1).AddDays(-45); }
		}
		public static DateTime ToDate
		{
			get 
			{ 
				DateTime dt = DateTime.Now;
				return new DateTime(dt.Year, dt.Month, dt.Day); 
			}
		}	
		public IStoreStats[] Stores 
		{ 
			get 
			{ 
				ICollection col     =  stores.Values;
				IStoreStats[] strs = new IStoreStats[col.Count];
				col.CopyTo(strs, 0);

				return strs; 
			}
		}

		
		/*		Constructors		*/
		public Corp()
		{
			stores = new Hashtable();
		}
		public Corp(int corpId) : this()
		{
			this.corpId = corpId;
		}
		public Corp(int corpId, IStoreStats[] top10Active,	IStoreStats[] top10NewCust,
					IStoreStats[] top10Revenue,	int totalStores)
		{
			this.corpId = corpId;
			
			this.top10Active = top10Active;
			this.top10NewCust = top10NewCust;
			this.top10Revenue = top10Revenue;

			this.totalStores = totalStores;
			stores = new Hashtable();
		}
		

		/*		Static Methods		*/
		public static Stats BuildStats(UOW uow) // used during retrival
		{
			IStoreStats[] stores = StoreStats2.getAll_Date(uow, ToDate);
			Corp corp = new Corp(stores[0].CorpId);
			ArrayList arCorps = new ArrayList(1000);
				
			for (int i = 0; i < stores.Length; i++)
			{
				if (corp.CorpId != stores[i].CorpId)
				{
					corp.RestoreRanking();	
					arCorps.Add(corp);
					corp = new Corp(stores[i].CorpId); 
				}
				corp.AddStore(stores[i]);
			}

			corp.RestoreRanking();	
			arCorps.Add(corp);
			Corp[] corpors = new Corp[arCorps.Count];
			arCorps.CopyTo(corpors);

			return new Stats(corpors, stores);
		}


		/*		Methods		*/
		public void AddStore(IStoreStats store)
		{
			stores.Add(store.StoreCode.Trim().ToLower(), store);
		}
		public static Stats LoadStats(UOW uow)   // used by loader
		{
			Hashtable corps = new Hashtable(10000); // keeps corps
			corporations = new Hashtable(500);

			LoadCorporations(uow);
			LoadActCust(corps, uow);
			LoadLocalRev(corps, uow);
			LoadNewCust(corps, uow);
			LoadWirelessRev(corps, uow);

		
			Stats stats = new Stats(GetCorps(corps), RankAndConvert(uow, corps));

			//PopulateActualCorpID(uow, ref stats);

			return stats;
		}

		public void AddRevenue(string storeCode, decimal local, string storeNumber, decimal ld, int actualCorp)
		{
			IStoreStats store  = FindStore(storeCode, actualCorp, storeNumber);
			store.LocalRevenue = local;
			store.LdRevenue    = ld;
		}
		public void AddActCust(string storeCode, int actCust, string storeNumber, int actualCorp)
		{
			FindStore(storeCode, actualCorp, storeNumber).ActiveCust  = actCust;
		}
		public void AddNewCust(string storeCode, int newCust, string storeNumber, int actualCorp)
		{
			FindStore(storeCode, actualCorp, storeNumber).MDT_NewCust = newCust;
		}
		public void AddWireless(string storeCode, decimal wireless, string storeNumber, int actualCorp)
		{
			FindStore(storeCode, actualCorp, storeNumber).WirelessRevenue = wireless;
		}
		public IStoreStats[] RankStores()
		{
			IStoreStats[] stores = Stores;
		
			RankActCust(ref stores);
			RankMTDsales(ref stores);
			RankRevenues(ref stores);
	
			return stores;
		}

		/*		Implementation		*/
		static void MakeTopRev(Corp corp)
		{
			IStoreStats[] stores = corp.Stores;
			int[] keys = new int[ stores.Length];

			for (int i = 0; i < keys.Length; i++)
				keys[i] = stores[i].RevenueRank;
			
			Array.Sort(keys, stores);
			
			int max = Top10;
			if (keys.Length < max)
				max = keys.Length;

			corp.top10Revenue = new IStoreStats[max];
			
			for (int i = 0; i < max; i++)
				corp.top10Revenue[i] = stores[i];
		}
 	
		static void MakeTopMtd(Corp corp)
		{
			IStoreStats[] stores = corp.Stores;
			int[] keys = new int[ stores.Length];

			for (int i = 0; i < keys.Length; i++)
				keys[i] = stores[i].MDT_NewCustRank;
			
			Array.Sort(keys, stores);
			
			int max = Top10;
			if (keys.Length < max)
				max = keys.Length;

			corp.top10NewCust = new IStoreStats[max];
			
			for (int i = 0; i < max; i++)
				corp.top10NewCust[i] = stores[i];
		}
 	
		static Corp[] GetCorps(Hashtable htable)
		{
			ArrayList ar = new ArrayList(2000);
			IDictionaryEnumerator iter = htable.GetEnumerator();

			while(iter.MoveNext())
				if (iter.Value != null)
					ar.Add((Corp)iter.Value);
			
			Corp[] corps = new Corp[ar.Count];
			ar.CopyTo(corps);
			return corps;
		}
		static IStoreStats[] RankAndConvert(UOW uow, Hashtable corps)
		{
			ArrayList ar = new ArrayList(10000);
			IDictionaryEnumerator iter = corps.GetEnumerator();
 
			while(iter.MoveNext())
				if (iter.Value != null)
					ar.AddRange(((Corp)iter.Value).RankStores());
			
			IStoreStats[] stores = new IStoreStats[ar.Count];
			ar.CopyTo(stores);
			return stores;
		}
		static void LoadNewCust(Hashtable hashtable, UOW uow)
		{
			IStoreStats[] stats = StoreStats2.GetNewCust(uow, FromDate, ToDate); 

			for (int i = 0; i < stats.Length; i++)
			{
				SetParentCorp(stats[i]);
				LoadStat(stats[i], hashtable);
				Corp corp = (Corp)hashtable[stats[i].CorpId];
				corp.AddNewCust(stats[i].StoreCode,  stats[i].MDT_NewCust,stats[i].StoreNumber, stats[i].ActualCorpId);
			}
		}
		static void LoadLocalRev(Hashtable hashtable, UOW uow)
		{
			IStoreStats[] stats = StoreStats2.GetRevenues(uow, FromDate, ToDate); 

			for (int i = 0; i < stats.Length; i++)
			{
				SetParentCorp(stats[i]);
				LoadStat(stats[i], hashtable);
				((Corp)hashtable[stats[i].CorpId]).AddRevenue(stats[i].StoreCode, stats[i].Revenue, 
					stats[i].StoreNumber, stats[i].LdRevenue, stats[i].ActualCorpId);
			}
		}
		static void LoadStat(IStoreStats store, Hashtable corps)
		{
			if (corps.ContainsKey(store.CorpId))
				return;
	
			corps.Add(store.CorpId, new Corp(store.CorpId));
		}
		static void LoadActCust(Hashtable corps, UOW uow)
		{
			IStoreStats[] stats = StoreStats2.GetActiveCust(uow);

			for (int i = 0; i < stats.Length; i++)
			{
				SetParentCorp(stats[i]);
				LoadStat(stats[i], corps);
				
				Corp corp = (Corp)corps[stats[i].CorpId];

				corp.AddActCust(stats[i].StoreCode, 
								stats[i].ActiveCust, 
								stats[i].StoreNumber, 
								stats[i].ActualCorpId);
			}
		}
		static void LoadWirelessRev(Hashtable hashtable, UOW uow)
		{
			IStoreStats[] stats = StoreStats2.GetWirelessRevenue(uow, FromDate, ToDate);

			for (int i = 0; i < stats.Length; i++)
			{
				SetParentCorp(stats[i]);
				LoadStat(stats[i], hashtable);
				((Corp)hashtable[stats[i].CorpId]).AddWireless(stats[i].StoreCode, stats[i].WirelessRevenue, stats[i].StoreNumber, stats[i].ActualCorpId);
			}
		}
		
		static void SetParentCorp(IStoreStats store)
		{	
			store.ActualCorpId = store.CorpId;
			ICorporation firm = (ICorporation)corporations[store.CorpId];
			
			if (firm.ParentId ==  0)
				return;
			
			if (!firm.UsePapentForStoreStats)
				return;

			store.CorpId = firm.ParentId;
		}
		static void LoadCorporations(UOW uow)
		{
			ICorporation[] firms = Corporation.getAll(uow);
			for (int i = 0; i < firms.Length; i++)
					corporations.Add(firms[i].CorpID, firms[i]);
		}
		static int GetParentCorpId(UOW uow, int corpId)
		{
			Corporation corp = Corporation.find(uow, corpId);
			if (corp.UsePapentForStoreStats)
				return corp.ParentId;

			return corpId;
		}
			
		void RestoreRanking()
		{
			IStoreStats[] stores = Stores;
			RestoreTopActive(stores);
			RestoreTopNew(stores);
			RestoreTopRev(stores);
		}
		void RestoreTopRev(IStoreStats[] stores)
		{
			ArrayList top = new ArrayList(Top10);
		
			for (int i = 0; i < stores.Length; i++)
				if (stores[i].RevenueRank > 0)
					top.Add(stores[i]);

			if (top.Count == 0)
			{
				top10Revenue = new IStoreStats[0];
				return;
			}

			IStoreStats[] revStores = new IStoreStats[top.Count];
			top.CopyTo(revStores);				

			int[] keys = new int[revStores.Length];
			for (int i = 0; i < keys.Length; i++)
				keys[i] = revStores[i].RevenueRank;
			
			Array.Sort(keys, revStores);	

			int max = revStores.Length;
			if (max > Top10)
				max = Top10;

			top10Revenue = new IStoreStats[max];
			for (int i = 0; i < top10Revenue.Length; i++)
				top10Revenue[i] = revStores[i];
		}

		void RestoreTopNew(IStoreStats[] stores)
		{
			ArrayList top = new ArrayList(Top10);
		
			for (int i = 0; i < stores.Length; i++)
				if (stores[i].MDT_NewCustRank > 0)
					top.Add(stores[i]);

			if (top.Count == 0)
			{
				this.top10NewCust = new IStoreStats[0];
				return;
			}

			IStoreStats[] newStores = new IStoreStats[top.Count];
			top.CopyTo(newStores);				

			int[] keys = new int[newStores.Length];
			for (int i = 0; i < keys.Length; i++)
				keys[i] = newStores[i].MDT_NewCustRank;
			
			Array.Sort(keys, newStores);	

			int max = newStores.Length;
			if (max > Top10)
				max = Top10;

			top10NewCust = new IStoreStats[max];
			for (int i = 0; i < top10NewCust.Length; i++)
				top10NewCust[i] = newStores[i];
		}

		void RestoreTopActive(IStoreStats[] stores)
		{
			ArrayList topActive = new ArrayList(Top10);
		
			for (int i = 0; i < stores.Length; i++)
				if (stores[i].ActiveCustRank > 0)
					topActive.Add(stores[i]);

			if (topActive.Count == 0)
			{
				top10Active = new IStoreStats[0];
				return;
			}
			
			IStoreStats[] actStores = new IStoreStats[topActive.Count];
			topActive.CopyTo(actStores);				

			int[] keys = new int[actStores.Length];
			for (int i = 0; i < keys.Length; i++)
				keys[i] = actStores[i].ActiveCustRank;
			Array.Sort(keys, actStores);	

			int max = actStores.Length;
			if (max > Top10)
				max = Top10;

			top10Active = new IStoreStats[max];
			for (int i = 0; i < top10Active.Length; i++)
				top10Active[i] = actStores[i];
		}

		int getCorpTotalStore( IStoreStats[] allStores, int corpId)
		{
			int corpStores = 0;
			for(int i = 0; i < allStores.Length; i++)
			{
				if(corpId == allStores[i].CorpId)
				corpStores++;			
			}
			return corpStores;
		}
	
		void RankActCust(ref IStoreStats[] stores)
		{
			SortActiveCust(ref stores);

			if (stores[0].ActiveCust == 0)
			{
				top10Active = new IStoreStats[0];
				return;
			}

			CalcRank(ref stores);
		}	

		void CalcRank(ref IStoreStats[] stores)
		{
			int rank = 1;

			stores[0].ActiveCustRank = rank;

			for (int i = 1; i < stores.Length; i++)
			{
				if (stores[i].ActiveCust == 0)
				{
					stores[i].ActiveCustRank = 0;
					continue;
				}
				if (stores[i].ActiveCust == stores[i-1].ActiveCust)
					stores[i].ActiveCustRank = stores[i-1].ActiveCustRank;
				else
					stores[i].ActiveCustRank = ++rank;
			}

			SetTop10Active(stores);
		}

		void SortActiveCust(ref IStoreStats[] stores)
		{
			int[] act = new int[stores.Length];
			for(int i = 0; i < stores.Length; i++)
				act[i] = stores[i].ActiveCust;

			Array.Sort(act, stores);
			Array.Reverse(stores, 0, stores.Length);
		}

		void SetTop10Active(IStoreStats[] stores)
		{
			ArrayList ar = new ArrayList();
			
			int max = Top10 > stores.Length ? stores.Length : Top10;

			for (int i = 0; i < max; i++)
				if (stores[i].ActiveCust > 0)
					ar.Add(stores[i]);
		
			top10Active = new IStoreStats[ar.Count];
			ar.CopyTo(top10Active);	
		}
		
		void SetTop10NewCust(IStoreStats[] stores)
		{
			ArrayList ar = new ArrayList();
			
			int max = Top10 > stores.Length ? stores.Length : Top10;

			for (int i = 0; i < max; i++)
				if (stores[i].MDT_NewCust > 0)
					ar.Add(stores[i]);
		
			top10NewCust = new IStoreStats[ar.Count];
			ar.CopyTo(top10NewCust);	
		}
		void SetTop10Revenue(IStoreStats[] stores)
		{
			ArrayList ar = new ArrayList();
			
			int max = Top10 > stores.Length ? stores.Length : Top10;

			for (int i = 0; i < max; i++)
				if (stores[i].RevenueRank > 0)
					ar.Add(stores[i]);
		
			top10Revenue = new IStoreStats[ar.Count];
			ar.CopyTo(top10Revenue);	
		}
		IStoreStats FindStore(string storeCode, int actualCorp, string storeNumber)
		{
			if (stores.ContainsKey(storeCode))
				return (IStoreStats)stores[storeCode];
		
			IStoreStats store = new StoreStats2(storeCode, corpId, storeNumber, actualCorp);
			stores.Add(storeCode, store);

			return store;
		}
		void RankRevenues(ref IStoreStats[] stores)
		{
			decimal[] mtd = new decimal[stores.Length];  //sort keys

			for(int i = 0; i < mtd.Length; i++)
				mtd[i] = stores[i].Revenue;

			Array.Sort(mtd, stores);
			Array.Reverse(stores, 0, stores.Length);

			if (!(stores[0].Revenue > 0m))
			{
				top10Revenue = new IStoreStats[0];
				return;
			}
			
			int rank = 1;
			stores[0].RevenueRank = rank;

			for (int i = 1; i < stores.Length; i++)
			{
				if (!(stores[i].Revenue > 0m))
				{
					stores[i].RevenueRank = 0;
					continue;
				}
				if (stores[i].Revenue == stores[i-1].Revenue)
					stores[i].RevenueRank = stores[i-1].RevenueRank;
				else
					stores[i].RevenueRank = ++rank;
			}

			SetTop10Revenue(stores);
		}
		
		void RankMTDsales(ref IStoreStats[] stores)
		{
			int rank = 1;

			int[] mtd = new int[stores.Length];  //sort keys
			for(int i = 0; i < mtd.Length; i++)
				mtd[i] = stores[i].MDT_NewCust;

			Array.Sort(mtd, stores);
			Array.Reverse(stores, 0, stores.Length);

			stores[0].MDT_NewCustRank = rank;

			for (int i = 1; i < stores.Length; i++)
			{
			
				if (stores[i].MDT_NewCust == 0)
				{
					stores[i].MDT_NewCustRank = 0;
					continue;
				}
				if (stores[i].MDT_NewCust == stores[i-1].MDT_NewCust)
					stores[i].MDT_NewCustRank = stores[i-1].MDT_NewCustRank;
				else
					stores[i].MDT_NewCustRank = ++rank;
			}

			SetTop10NewCust(stores);
		}
	}
}