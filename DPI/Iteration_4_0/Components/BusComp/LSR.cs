using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Components
{
	[Serializable]
	public class LSR
	{
		ILEC_LSR_Rule[] rules;		
		UOW uow;
		int accNumber;

		public LSR(UOW uow, int accNumber)
		{
			this.uow = uow;
			this.accNumber = accNumber;
		}
		public string GetUSOCS( )
		{
			this.uow = uow;
			this.accNumber = accNumber;

			return string.Empty;
		}
		/*		Implementation		*/
		public void GetLSRs(UOW uow)
		{
		//	CustDate cd = CustData.

		}
		void GetRules(UOW uow, string zip, string ilecCode, string st, OrderType otype, string provCat)
		{
			Organization ilec = Organization.GetILEC_ByCode(uow, ilecCode)[0];
			Location provLoc = (Location.find(uow, zip, ilec.Id))[0];
			rules = ILEC_LSR_Rule.GetRules(uow, st, ilecCode, otype, provCat);
		}
	}
}