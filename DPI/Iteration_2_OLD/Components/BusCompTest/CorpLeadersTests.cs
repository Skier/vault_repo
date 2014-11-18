//using System;
//using System.Data;
//using System.Data.SqlClient;
//using NUnit.Framework;
//using DPI.Components;
//using DPI.Interfaces;
//
//namespace DPI.ComponentsTests
//{
//	[TestFixture]
//	public class CorpLeadersTests
//	{
//		/*		Data		*/
//		int id=1;
//		string storeCode = "NCRW0434RW";
//		string storeNumber = "446";
//		int corpid = 27;
//		DateTime statDate = new DateTime(2005, 2, 18);
//		string statType = "ActCust";
//		int rank = 2;
//		int rankValue = 175;
//        
//		/*		Constructors		*/
//		public CorpLeadersTests()
//		{
//			// try { cleanup(); } 	catch {}
//			// Console.WriteLine("Cleanup completed");
//		}
//        
//		/*		Methods		*/
//		public static void Main()
//		{
//			CorpLeadersTests test = new CorpLeadersTests();
//            
//			// UOW Tests
//			test.addCorpLeaders();
//			test.findCorpLeaders();
//			test.saveCorpLeaders();
//			test.findAllCorpLeaderses();
//            
//			try
//			{
//				test.delCorpLeaders();
//			}
//			catch(ArgumentException ae)
//			{
//				Console.WriteLine("Expected exception: delCorpLeaders:" + ae.Message);
//			}
//			catch(Exception e)
//			{
//				Console.WriteLine("Error: delCorpLeaders: " + e.Message);
//			}
//            
//		}
//		[Test]
//		public void addCorpLeaders()
//		{
//			UOW uow = new UOW();
//			uow.Service = "addCorpLeaders";
//			CorpLeaders cls = new CorpLeaders(uow);
//            
//			cls.StoreCode = this.storeCode;
//			cls.StoreNumber = this.storeNumber;
//			cls.Corpid = this.corpid;
//			cls.StatDate = this.statDate;
//			cls.StatType = this.statType;
//			cls.Rank = this.rank;
//			cls.RankValue = this.rankValue;
//        
//			uow.commit();
//			this.id = cls.Id;
//            
//			uow = new UOW();
//			uow.Service = "addCorpLeaders - assert";
//			cls = CorpLeaders.find(uow, this.id);
//			Assertion.Assert(cls.StoreNumber == this.storeNumber);
//			uow.close();
//		}
//		[Test]
//		public void findCorpLeaders()
//		{
//			UOW uow = new UOW();
//			uow.Service = "findCorpLeaders";
//            
//			CorpLeaders cls = CorpLeaders.find(uow, this.id);
//			Assertion.Assert(cls.Id == this.id);
//			uow.close();
//		}
//		[Test]
//		public void saveCorpLeaders()
//		{
//			UOW uow = new UOW();
//			uow.Service = "saveCorpLeaders";
//			CorpLeaders cls = CorpLeaders.find(uow, this.id);
//            
//			cls.StoreCode = this.storeCode;
//			cls.StoreNumber = this.storeNumber;
//			cls.Corpid = this.corpid;
//			cls.StatDate = this.statDate;
//			cls.StatType = this.statType;
//			cls.Rank = this.rank;
//			cls.RankValue = this.rankValue;
//			cls.StoreCode += " saved";
//			this.storeCode = cls.StoreCode;
//                
//			uow.commit();
//            
//			uow = new UOW();
//			uow.Service = "saveCorpLeaders - assert";
//            
//			cls = CorpLeaders.find(uow, this.id);
//			Assertion.Assert(cls.StoreCode == this.storeCode);
//			uow.close();
//		}
//		[Test]
//		public void findAllCorpLeaderses()
//		{
//			UOW uow = new UOW();
//			uow.Service = "findAllCorpLeaderses";
//			CorpLeaders[] objs = CorpLeaders.getAll(uow);
//			Assertion.Assert(objs.Length > 0);
//			uow.close();
//		}
//		[Test]
//		[ExpectedException(typeof(ArgumentException))]
//		public void delCorpLeaders()
//		{
//			UOW uow = new UOW();
//			uow.Service = "delCorpLeaders";
//			CorpLeaders cls = CorpLeaders.find(uow, this.id);
//			cls.delete();
//            
//			uow.commit();
//            
//			uow = new UOW();
//			uow.Service = "delCorpLeaders - assert";
//			cls = CorpLeaders.find(uow, this.id);
//			Assertion.Assert((cls.Id == 0));
//			uow.close();
//		}
//    }
//}
