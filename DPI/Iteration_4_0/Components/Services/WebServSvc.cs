using System;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class WebServSvc
	{
		public static IWebSvcQueue GetEntry(IMap imap)
		{
			return new WebSvcQueue(imap);
		}
		public static IWebSvcQueue GetEntry()
		{
			return new WebSvcQueue();
		}
		public static IWebSvcQueue GetEntry(UOW uow)
		{
			return new WebSvcQueue(uow);
		}
		public static void PresetPymt(UOW uow, IUser user, IPayInfo payInfo, string receitId)
		{
			if (!StoreStatsCol.GetCorporation(user.LoginStoreCode).IsPymtPostReq)
				return;

			PostPymtFactory.GetProvider(user.LoginStoreCode).PostPymt(uow, user, payInfo, receitId);
		}
		public static void PresetVoid(UOW uow, IUser user, int tranId)
		{
			if (!StoreStatsCol.GetCorporation(user.LoginStoreCode).IsPymtPostReq)
				return;

			PostPymtFactory.GetProvider(user.LoginStoreCode).PostReversal(uow, user, tranId); //tranId is verifone tran Id 
		}
		public static void SaveEntry(IWebSvcQueue entry)
		{
			UOW uow = null;

			try
			{
				uow = new UOW();
				uow.BeginTransaction();

				uow.Imap.add(entry);
				uow.commit();
			}
			finally
			{
				uow.close();
			}
		}
		public static IWebSvcQueue SetupEntry(IMap imap, string clerkId,  string method, string busObj, string busObjId)
		{
			IWebSvcQueue wq = GetEntry(imap);

			wq.WSProvider = "DPI";
			wq.QueType = WebSvcQueueType.Query.ToString();
			wq.WebMethod = method;
			wq.Status = WebSvcQueueStatus.Failed.ToString();
			
			wq.ClerkId = clerkId;			
			wq.BusObject = busObj;
			wq.BusObjId = busObjId;
			
			return wq;
		}
		public static IWebSvcQueue SetupEntry(IMap imap, string clerkId,  string method, string busObj, string busObjId, string initialMsg)
		{
			IWebSvcQueue wq = SetupEntry(imap, clerkId, method, busObj, busObjId);

			wq.InitialMsg = initialMsg;
			
			return wq;
		}
		public static IWebSvcQueue SetupEntry(IMap imap, IPayInfo pi, IUser user,  string method)
		{
			IWebSvcQueue wq = GetEntry(imap);

			wq.WSProvider = "DPI";
			wq.QueType = WebSvcQueueType.Query.ToString();
			wq.WebMethod = method;
			wq.Status = WebSvcQueueStatus.Failed.ToString();
			
			if (user != null)
			{
				wq.StoreCode = user.LoginStoreCode;
				wq.ClerkId = user.ClerkId;
			}
						
			if (pi != null)
			{
				wq.BusObject = pi.ToString();
				wq.BusObjId = pi.Id.ToString();
			}

			return wq;
		}
		public static IWebSvcQueue SetupEntry(IMap imap, IPayInfo pi, IUser user,  string method, string provider)
		{
			IWebSvcQueue wq = SetupEntry(imap, pi, user, method);
			wq.WSProvider = provider;

			return wq;
		}
		public static IWebSvcQueue SetupEntry(IMap imap, IUser user, WebSvcQueueType queType, 
											string method, string provider)
		{
			IWebSvcQueue wq = GetEntry(imap);

			wq.WSProvider = provider;
			wq.QueType = queType.ToString();
			
			wq.WebMethod = method;
			if (queType == WebSvcQueueType.Reversal)
				wq.ReversalMethod = method;
			
			wq.Status = WebSvcQueueStatus.Failed.ToString();
			
			if (user != null)
			{
				wq.StoreCode = user.LoginStoreCode;
				wq.ClerkId = user.ClerkId;
			}
						
			return wq;
		}
		public static IWebSvcQueue SetupEntry(IMap imap, IPayInfo pi, string clerkId,  string method)
		{
			IWebSvcQueue wq = GetEntry(imap);

			wq.WSProvider = "DPI";
			wq.QueType = WebSvcQueueType.Query.ToString();
			wq.WebMethod = method;
			wq.Status = WebSvcQueueStatus.Failed.ToString();
			wq.ClerkId = clerkId;		
			
			if (pi != null)
			{
				wq.BusObject = pi.ToString();
				wq.BusObjId = pi.Id.ToString();
			}

			return wq;
		}
		public static IWebSvcQueue SetupEntry(IMap imap, IUser user, string provider, string method)
		{
			IWebSvcQueue wq = GetEntry(imap);

			wq.WSProvider = provider;
			wq.QueType = WebSvcQueueType.Query.ToString();
			wq.WebMethod = method;
			wq.Status = WebSvcQueueStatus.Failed.ToString();
			wq.ClerkId = user.ClerkId;	
			
			return wq;
		}
		public static IWebSvcQueue SetupEntry(IMap imap, IUser user, IDemand dmd, string method)
		{
			IWebSvcQueue wq = GetEntry(imap);

			wq.WSProvider = "DPI";
			wq.QueType = WebSvcQueueType.Query.ToString();
			wq.WebMethod = method;
			wq.Status = WebSvcQueueStatus.Failed.ToString();
			
			if (user != null)
			{
				wq.StoreCode = user.LoginStoreCode;
				wq.ClerkId = user.ClerkId;
			}
						
			if (dmd != null)
			{
				wq.BusObject = dmd.ToString();
				wq.BusObjId = dmd.Id.ToString();
			}

			return wq;
		}
	}
}