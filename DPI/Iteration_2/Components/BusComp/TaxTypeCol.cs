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
	public class TaxTypeCol
	{
		/*        Data        */
		static Customer_Tax_Type[] taxes;
		static DateTime lastLoad;

		/*		Properties		*/
		static Customer_Tax_Type[] Taxes
		{
			get 
			{
				if (DateTime.Now.AddMinutes(-20) > lastLoad)
					LoadData();
				
				return taxes;
			}
		}
		/*		Constructors		*/
		TaxTypeCol() 
		{
			LoadData();
			OperationMessenger.RefreshData += new EventHandler(OnRefresh);
		}
		/*		Methods		*/
		public static string GetTaxDescr(string taxType)
		{
			if (taxes == null)
				new TaxTypeCol();

			for (int i = 0; i < Taxes.Length; i++)
				if (taxes[i].Surcharge_Indicator.Trim().ToLower() == taxType.Trim().ToLower())
					return taxes[i].Description;
			
			return "Unknown Tax type '" + taxType + "'";
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
				uow.Service = "TaxTypeCol.LoadData()";
				taxes = Customer_Tax_Type.getAll(uow);
				lastLoad = DateTime.Now;
			}
			finally
			{
				uow.close();
			}
		}
	}
}