using System;
using DPI.Interfaces;
using DPI.Components;
using DPI.Services;

namespace DPI.ClientComp
{
	public class Ranking
	{
		protected string[] attrs;
		protected string[] stores;
		protected int[] ranks;
		protected  string rankTitle;
		protected  string rankText;
		protected StatType stype;

		public string RankTitle { get { return rankTitle; }}
		public string RankText  { get { return rankText; }}		
		public string[] Attrs   { get { return attrs; }}
		public string[] Stores  { get { return stores; }}
		public int[]    Ranks   { get { return ranks; }}

		public Ranking(StatType stype)
		{
			this.stype = stype;
		}
		public static Ranking GetRanking(StatType stype, string storeCode)
		{
			switch(stype)
			{
				case StatType.Active :
				{
					return new ActiveCustRanking(storeCode);
				}
				case StatType.New :
				{
					return new NewCustRanking(storeCode);
				}
				case StatType.Revenues :
				{
					return new RevenueRanking(storeCode);
				}
				default :
					return null;
			}
		}

		protected void GetStores(IStoreStats[] stats, int size)
		{
			stores = new string[size];

			for (int i = 0 ; i < stores.Length; i ++)
				stores[i] = stats[i].StoreNumber;
		}
	}

	public class ActiveCustRanking : Ranking
	{
		public ActiveCustRanking(string storeCode) : base(StatType.Active)
		{
			rankText  = "Active Customer Ranking";
			rankTitle = "Active Customers";
	
			IStoreStats stat = StoreSvc.GetStoreStats(storeCode);
			ICorp corp = StoreSvc.GetCorp(stat.CorpId);
	
			int size = GetSize(corp);

			GetRanks(corp, size);
			GetStores(corp.Top10Active, size);
			GetAttrs(corp, size);
		}
	
		int GetSize(ICorp corp)
		{
			for (int size = 0 ; size < corp.Top10Active.Length; size ++)
				if(corp.Top10Active[size].ActiveCustRank == 0)
					return size;

			return corp.Top10Active.Length;
		}

		void GetAttrs(ICorp corp, int size)
		{
			attrs = new string[size];
			for (int i = 0 ; i < attrs.Length; i ++)
				attrs[i] = corp.Top10Active[i].ActiveCust.ToString();
		}

		void GetRanks(ICorp corp, int size)
		{
			ranks = new int[size];
			for (int i = 0 ; i < ranks.Length; i ++)
				ranks[i] = corp.Top10Active[i].ActiveCustRank;
		}
	}
	public class NewCustRanking : Ranking
	{
		public NewCustRanking(string storeCode) : base(StatType.New)
		{
			rankText  = "New Sales Rank"; 
			rankTitle = "New Customers";;
	
			IStoreStats stat = StoreSvc.GetStoreStats(storeCode);
			ICorp corp = StoreSvc.GetCorp(stat.CorpId);

			int size = GetSize(corp);

			GetStores(corp.Top10NewCust, size);
			GetAttrs(corp, size);
			GetRanks(corp, size);
		}	
		
		int GetSize(ICorp corp)
		{
			for (int size = 0 ; size < corp.Top10NewCust.Length; size ++)
				if(corp.Top10NewCust[size].MDT_NewCustRank == 0)
					return size;

			return corp.Top10NewCust.Length;
		}
	
		void GetAttrs(ICorp corp, int size)
		{
			attrs = new string[size];
			for (int i = 0 ; i < attrs.Length; i ++)
				attrs[i] = corp.Top10NewCust[i].MDT_NewCust.ToString();
		}

		
		void GetRanks(ICorp corp, int size)
		{
			ranks = new int[size];
			for (int i = 0 ; i < ranks.Length; i ++)
				ranks[i] = corp.Top10NewCust[i].MDT_NewCustRank;
		}
	}

	public class RevenueRanking : Ranking
	{
		public RevenueRanking(string storeCode) : base(StatType.Revenues)
		{
			rankText  = "Revenue Ranking";
			rankTitle = "Revenue";
	
			IStoreStats stat = StoreSvc.GetStoreStats(storeCode);
			ICorp corp = StoreSvc.GetCorp(stat.CorpId);

			int size = GetSize(corp);
			GetStores(corp.Top10Revenue, size);
			GetAttrs(corp, size);
			GetRanks(corp, size);
		}	

		int GetSize(ICorp corp)
		{
			for (int size = 0 ; size < corp.Top10Revenue.Length; size ++)
				if(corp.Top10Revenue[size].RevenueRank == 0)
					return size;

			return corp.Top10Revenue.Length;
		}

		void GetAttrs(ICorp corp, int size)
		{
			attrs = new string[size];
			for (int i = 0 ; i < attrs.Length; i ++)
				attrs[i] = corp.Top10Revenue[i].LocalRevenue.ToString();
		}
		
		void GetRanks(ICorp corp, int size)
		{
			ranks = new int[size];
			for (int i = 0 ; i < ranks.Length; i ++)
				ranks[i] = corp.Top10Revenue[i].RevenueRank;
		}
	}

}