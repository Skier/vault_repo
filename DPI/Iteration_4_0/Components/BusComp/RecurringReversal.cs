//using System;
//using System.Collections;
//using System.Data;
//using System.Text;
//using System.Xml;
//using System.Threading;
//using System.Windows.Forms;
//using System.Data.SqlClient;
//using System.Data.SqlTypes;
//using DPI.Interfaces;
//
//namespace DPI.Components
//{
//	[Serializable]  
//	public class RecurringReversal
//	{
//		/*        Data        */
////		static DateTime lastStart;
//		static RecurringReversal rr;
//
//		/*		Constructors		*/
//		RecurringReversal() 
//		{
//	//		OperationMessenger.RefreshData += new EventHandler(new RecurringReversal().OnRefresh);	
//		}
//		
//		/*		Methods				*/
//		public static void Main()
//		{
//			rr = new RecurringReversal();
//			rr.StartThred();
//		}
//		public void GetWebQueues()
//		{
//		}
//		
//		/*		Implementation		*/
////		void OnRefresh(object sender, EventArgs ea)
////		{
////			if (lastStart != DateTime.MinValue)
////				return;
////
////			rr = new RecurringReversal();
////			
////			Thread t = new Thread( new ThreadStart(StartThred));
////			//			t.IsBackground = true;
////			t.Name = "RefreshThread";
////			t.Start();	
////		}
//		void StartThred()
//		{
//			int count = 0;
//			try
//			{				
//				while(true)
//				{
//					Thread t = new Thread( new ThreadStart(StartReversal));
//					t.Name = WebSvcQueueType.Reversal.ToString();
//					t.Start();
//					string name = Thread.CurrentThread.Name;
//					
//					Thread.Sleep(2000);
//					count++;
//					
//					if (count > 10)
//						break;
//				}
//			}
//			catch (Exception ex)
//			{
//				LogError("RecurringReversal.StartThred", "AutoReversal", ex.Message);
//			}
//		}
//		void StartReversal()
//		{
//			UOW uow = null;
//
//			try
//			{
//			//	uow.Service = "RecurringReversal.StartReversal()";
//				string name = Thread.CurrentThread.Name;
//				uow = new UOW();
//				
//				IWebSvcQueue[] queue = WebSvcQueue.GetReversalQueue(uow);
//				for (int i = 0; i < queue.Length; i++)
//					Reversal(queue[i]);
//
//			//	uow.commit();
//
//			}
//			catch(Exception ex)
//			{
//				LogError("RecurringReversal.StartReversal", "AutoReversal", ex.Message);
//			}
//			finally
//			{
//				uow.close();
//			}
//		}	
//
//		static void Reversal(IWebSvcQueue entry)
//		{
//			switch (entry.ReversalMethod)
//			{
//				case "PurposeDebitCard.EnrollReversal" :
//				{
//				//	SetResponse(PurposeProxy.ApplyForEnrollReversal(ConvertToXml(entry.ReversalXml)), entry);						
//					break;
//				}
//				case "PurposeDebitCard.ReloadReversal" :
//				{
//				//	SetResponse(PurposeProxy.ApplyForReloadReversal(ConvertToXml(entry.ReversalXml)), entry);	
//					break;
//				}
//			}
//		}
////		static bool ValidateReversal(IWebSvcQueue que)
////		{
////			if (que.QueType != WebSvcQueueType.Reversal.ToString())
////				return false;
////			
////			if (que.ReversalXml == null)
////				return false;
////
////			if (que.ReversalXml.Length < 1)
////				return false;
////		
////			return true;
////		}
//		static XmlDocument ConvertToXml(string msg)
//		{
//			XmlDocument xDoc = new XmlDocument();
//			xDoc.LoadXml(msg);
//
//			return xDoc;
//		}
//		static void LogError(string subSys, string user, string message)
//		{
//			UOW uow = new UOW();
//			DPI_Err_Log eLog = new DPI_Err_Log(uow);
//            
//			eLog.Subsys = subSys;
//			eLog.DPI_User = user;
//			eLog.DateTime = DateTime.Now;
//			eLog.Message = message;
//			eLog.add();
//		}
//		static void SetResponse(PurposeDCResponse resp, IWebSvcQueue que)
//		{
//			que.Status = WebSvcQueueStatus.Open.ToString();
//
//			if (resp.RespCode == "00")
//				que.Status = WebSvcQueueStatus.Reversed.ToString();
//			
//			que.Attemps += 1;
//		}
//	}
//}