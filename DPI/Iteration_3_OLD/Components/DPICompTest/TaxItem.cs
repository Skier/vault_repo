using System;
using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]
	public class TaxItem : IDelTax
	{
		/*		Data		*/
		int id;
		int dlvId;
		string taxId;
		decimal taxAmt;

		/*		Properties		*/
		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		public int DlvId
		{
			get { return dlvId; }
			set { dlvId = value; }
		}
		public string TaxId 
		{
			get { return taxId; }
			set { taxId = value; }
		}
		public decimal TaxAmt 
		{
			get { return taxAmt; }
			set { taxAmt = Decimal.Round(value, 2); }
		}
	}
}