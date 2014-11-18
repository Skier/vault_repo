//using System;
//using System.Collections;
//using System.Data;
//using System.Text;
//using System.Windows.Forms;
//using System.Data.SqlClient;
//using System.Data.SqlTypes;
//using DPI.Interfaces;
//
//namespace DPI.Components
//{
//	[Serializable]  
//	public class WebQueTypeCol
//	{
//		/*        Data        */
//		static WebQueType[] qts;
//		static DateTime lastLoad;
//
//		/*		Properties		*/
//		WebQueTypeCol() 
//		{
//			LoadData();
//			OperationMessenger.RefreshData += new EventHandler(OnRefresh);
//		}
//		public static IWebQueType GetQueType(string queType)
//		{
//			if (qts == null)
//				new WebQueTypeCol();
//
//			for (int i = 0; i < qts.Length; i++)
//				if (qts[i].QueType.Trim().ToLower() == queType.Trim().ToLower())
//					return qts[i];
//			
//			throw new ApplicationException("Can't find WebQueType '" + queType + "'") ;
//		}
//		/*		Implementation		*/
//		static void OnRefresh(object sender, EventArgs ea)
//		{
//			LoadData();
//		}
//		static void LoadData()
//		{
//			UOW uow = null;
//
//			try
//			{
//				uow = new UOW();
//				uow.Service = "QueTypeCol.LoadData()";
//				qts = WebQueType.getAll(uow);
//				lastLoad = DateTime.Now;
//			}
//			finally
//			{
//				uow.close();
//			}
//		}
//	}
//}