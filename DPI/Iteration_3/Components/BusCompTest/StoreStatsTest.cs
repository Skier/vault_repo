//using System;
//using System.Collections;
//using NUnit.Framework;
//
//using DPI.Interfaces;
//using DPI.Components;
//
//namespace DPI.ComponentsTests
//{
//	[TestFixture]
//	public class StoreStatsTest
//	{
//		public static void Main()
//		{
//			StoreStatsTest t = new StoreStatsTest();
//			t.GetCorpStats();
//		}
//		[Test]
//		public void GetCorpStats()
//		{
//			UOW uow = new UOW();		
//			Stats sts = Corp.BuildStats(uow);
//			IStoreStats[] stats = sts.stores;
//			 
//			Assertion.Assert(stats.Length > 0);
//
//			int corp = 27;
//			ArrayList ar = new ArrayList(1000);		
//			for (int i = 0; i < stats.Length; i++)
//				if (stats[i].CorpId == corp)
//					ar.Add(stats[i]);
//		
//			StoreStats[] s = new StoreStats[ar.Count];
//			ar.CopyTo(s);
//		
//			int[] keys = new int[s.Length];
//					
//			for (int i = 0; i < keys.Length; i++)
//				keys[i] = s[i].ActiveCustRank;
//		
//			Array.Sort(keys, s);
//			
//			bool foundActive = false; 
//			bool foundMTD = false; 
//
//			for (int i = 0; i < s.Length; i++)
//			{
//				Assertion.Assert(s[i].TotalStores > 0);
//
//				if (s[i].ActiveCust > 0)
//				{
//					foundActive = true;
//					//	Console.WriteLine("Store {0}: act cust {1}, act rank {2}", s[i].StoreCode,
//					//		s[i].ActiveCust, s[i].ActiveCustRank + " out of " + s[i].TotalStores );
//				}
//				else
//				{
//					//	Console.WriteLine("Store {0}: act cust 0, act rank N/A", s[i].StoreCode);
//				}
//				if (s[i].MDT_NewCust > 0)
//				{
//					//	Console.WriteLine("Store {0}: new cust {1}, new rank {2}", s[i].StoreCode,
//					//		s[i].MDT_NewCust, s[i].MDT_NewCustRank + " out of " + s[i].TotalStores );
//					
//					foundMTD = true;
//				}
//				else
//				{
//					//	Console.WriteLine("Store {0}: new cust 0, new rank N/A", s[i].StoreCode);
//				}		
//				corp++;
//			}
//			Assertion.Assert(foundActive);
//		}
//	}
//}