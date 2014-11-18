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
	public class ProdSubClassCol
	{
		/*        Data        */
		static ProdSubClassInfo[] subs;
		static DateTime lastLoad;

		/*		Properties		*/
		static ProdSubClassInfo[] Subs
		{
			get 
			{
				if (DateTime.Now.AddMinutes(- Const.REF_INTERVAL) > lastLoad)
					LoadData();
				
				return subs;
			}
		}
		/*		Constructors		*/
		ProdSubClassCol() 
		{
			LoadData();
			OperationMessenger.RefreshData += new EventHandler(OnRefresh);
		}
		/*		Methods		*/
		public static ProdSubClassInfo GetSubClass(string prodSubClass)
		{
			if (subs == null)
				new ProdSubClassCol();

			for (int i = 0; i < Subs.Length; i++)
				if (Subs[i].SubClass.Trim().ToLower() == prodSubClass.Trim().ToLower())
					return Subs[i].Clone();
			
			throw new ApplicationException("Can't find ProdSubClass '" + prodSubClass + "'") ;
		}
		public static ProdSubClassInfo[] GetSubClasses()
		{
			if (subs == null)
				new ProdSubClassCol();

			ProdSubClassInfo[] res = new ProdSubClassInfo[Subs.Length];
			for (int i = 0; i < res.Length; i++)
				res[i] = Subs[i].Clone();

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
				uow.Service = "ProdSubClassCol.LoadData()";
				subs = ProdSubClassInfo.Conv(ProdSubClass.getAll(uow));
				lastLoad = DateTime.Now;
			}
			finally
			{
				uow.close();
			}
		}
	}
}