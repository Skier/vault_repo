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
	public class DmdProdTypeCol
	{
		/*        Data        */
		static IDmdProdTypeRule[] rules;
		static DateTime lastLoad;

		/*		Properties		*/
		static IDmdProdTypeRule[] Rules
		{
			get 
			{
				if (DateTime.Now.AddMinutes(- Const.REF_INTERVAL) > lastLoad)
					LoadData();
				
				return rules;
			}
		}
		DmdProdTypeCol() 
		{
			LoadData();
			OperationMessenger.RefreshData += new EventHandler(OnRefresh);
		}
		public static bool IsExcluded(string prodType, string dmdType)
		{
			if (rules == null)
				new DmdProdTypeCol();

			for (int i = 0; i < rules.Length; i++)
				if (    (rules[i].ProdType.Trim().ToLower() == prodType.Trim().ToLower())
					 && (rules[i].DmdType.Trim().ToLower() == dmdType.Trim().ToLower()))
					return true;
		
			return false;
		}
		public static IDmdProdTypeRule[] GetRules()
		{
			if (rules == null)
				new DmdProdTypeCol();

			IDmdProdTypeRule[] res = new IDmdProdTypeRule[rules.Length];
			for (int i = 0; i < rules.Length; i++)
				res[i] = rules[i];

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
				rules = DmdProdTypeRule.getAll(uow);
				lastLoad = DateTime.Now;
			}
			finally
			{
				uow.close();
			}
		}
	}
}