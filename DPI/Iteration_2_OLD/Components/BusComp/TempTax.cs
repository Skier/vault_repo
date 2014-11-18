//using System;
//using System.Collections;
//using System.Data;
//using System.Text;
//using System.Windows.Forms;
//using System.Data.SqlClient;
//using System.Data.SqlTypes; 
//using DPI.Components;
//using DPI.Interfaces;
// 
//namespace DPI.Components
//{	
//	public class TempTax : IDmdTax, ISummable
//	{
//		IDmdItem dmdItm;
//		string taxId;
//		int taxProd;
//		decimal taxAmount;
//		string taxCode;
// 
//		public int Id { get { return int.MinValue; }}
//		public IDmdItem DmdItm 
//		{
//			get { return dmdItm; }
//			set { dmdItm = value; }
//		}
//		public string TaxId 
//		{
//			get { return taxId; }
//			set { taxId = value; }
//		}
//		public int TaxProd
//		{
//			get { return taxProd; }
//			set { taxProd = value; }
//		}
//		public string SumType
//		{
//			get { return TaxId; }
//			set { TaxId = value; }
//		}
//		public decimal Amount 
//		{
//			get { return TaxAmount; }
//			set { TaxAmount = value; } 
//		}
//		public decimal TaxAmount
//		{
//			get { return decimal.Round(taxAmount, 2); }
//			set { taxAmount = decimal.Round(value, 2); }
//		}
//		public string TaxCode
//		{
//			get { return taxCode; }
//			set { taxCode = value; }
//		}
//
//	}
//}