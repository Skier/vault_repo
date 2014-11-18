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
	public class ProdTypeCol
	{
		/*        Data        */
		static ProdType[] pts;
		static DateTime lastLoad;

		/*		Properties		*/
		static ProdType[] PTypes
		{
			get 
			{
				if (DateTime.Now.AddMinutes(- Const.REF_INTERVAL) > lastLoad)
					LoadData();
				
				return pts;
			}
		}
		ProdTypeCol() 
		{
			LoadData();
			OperationMessenger.RefreshData += new EventHandler(OnRefresh);
		}
		public static ProdType GetProdType(string prodType)
		{
			if (pts == null)
				new ProdTypeCol();

			for (int i = 0; i < pts.Length; i++)
				if (pts[i].PrdType.Trim().ToLower() == prodType.Trim().ToLower())
					return pts[i];
			
			throw new ApplicationException("Can't find ProdType '" + prodType + "'") ;
		}
		public static ProdType[] GetProdTypes()
		{
			if (pts == null)
				new ProdTypeCol();

			ProdType[] res = new ProdType[pts.Length];
			for (int i = 0; i < res.Length; i++)
				res[i] = pts[i];

			return res;
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
				uow.Service = "ProdTypeCol.LoadData()";
				pts = ProdType.getAll(uow);
				lastLoad = DateTime.Now;
			}
			finally
			{
				uow.close();
			}
		}
	}
}