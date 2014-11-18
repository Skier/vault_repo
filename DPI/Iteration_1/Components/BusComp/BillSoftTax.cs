using System;
using System.Configuration;
using System.Collections;
using BillSoft.EZTaxNET;


using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Components
{
	public class BillSoftTax : IBillSoftTax
	{
	#region Data
		EZTax eztax = null;
		int session = -1;
	#endregion

	#region Properties
	#endregion

	#region Constructors
		
		public BillSoftTax(UOW uow)
		{
			eztax    = TaxWrapper.EZTax;	
			session  = TaxWrapper.AcquireSession();
		}
	
	#endregion
		
	#region Methods
		public void ReleaseSession()
		{
			TaxWrapper.ReleaseSession(session);
			session = -1; 
		}
		public IDmdTax[] ComputeTax(IUOW uow, int prod, decimal priceAmt, string zip, DateTime date) // add logging
		{
			TaxCode tc  =  ProdInfoCol.GetTaxCode(prod);
							
			if (!ValidateTaxCode(tc))
				return new IDmdTax[0];
			LogInput(uow, prod, tc, priceAmt, zip); 
			BillSoftArg args = SetupArgs(priceAmt, tc, zip, date);
					
			ArrayList ar = new ArrayList();	// keeps dmditem taxes
			ar.AddRange(TaxWithZipCode((UOW)uow, args));
		
			if (tc.BillSoftTran == 7 && tc.BillSoftServ == 5)
				ar.AddRange(LocalServiceExtras((UOW)uow, args));
		
			return ConvertToDTaxes((UOW)uow, ar, prod);
		}

	#endregion

	#region Implementation
		taxes_tbl[]  LocalServiceExtras(UOW uow, BillSoftArg args)
		{
			//	TaxWithZipCode(usa, tx, zip, 7, 43, date, 0m,    0, 0);
			ArrayList ar = new ArrayList();
			//Local Lines
			args.servType = 21;
			args.tranType = 7;
			args.tranAmt = 0m;
			args.lines = 1;
			args.locs = 0;
			
			ar.AddRange(TaxWithZipCode(uow, args));
						
			//Local Locations
			args.servType = 23;
			args.tranType = 7;
			args.tranAmt = 0m;
			args.lines = 0;
			args.locs = 1;
			ar.AddRange(TaxWithZipCode(uow, args));

			taxes_tbl fccCharge = new taxes_tbl();

			fccCharge.rate = (float)Const.FCC_ChargeAmt;
			fccCharge.tax_amount =  (double)Const.FCC_ChargeAmt;
			fccCharge.tax_type = Const.FCC_ChargeType;
			ar.Add(fccCharge);

			//Local FCC Subs scriber Line Fee
			args.servType = 20;
			args.tranType = 7;
			args.tranAmt = Const.FCC_ChargeAmt;
			args.lines = 0;
			args.locs = 0;
		
			ar.AddRange(TaxWithZipCode(uow, args));

			return ConvertToBillSoftTaxes(ar);
		}
	
		BillSoftArg SetupArgs(decimal taxableAmt, TaxCode tc, string zip, DateTime date)
		{
			BillSoftArg args = new BillSoftArg();

			args.zipcode  = zip; 
			args.tranDate = date;
			args.servType = (short) tc.BillSoftServ;
			args.tranType = (short) tc.BillSoftTran;

			args.tranAmt  =	taxableAmt;
			args.lines    = 0;
			args.locs     = 0;

			return args;
		}
		bool ValidateTaxCode(TaxCode tc)
		{
			if (tc == null)
				return false;
					
			if (tc.BillSoftServ == 0)
				return false;

			if (tc.BillSoftTran == 0)
				return false;

			return true;
		}
		taxes_tbl[] TaxWithZipCode(UOW uow, BillSoftArg args)
		{
			int	err_code;

			taxes_tbl[] taxes = eztax.EZTaxZip(session, GetZip(uow, args), out err_code);

			if (err_code != 0)
				throw new ApplicationException("Error EZTaxZip = " + err_code.ToString());
					
			return taxes;
		}
	

		zip_code GetZip(UOW uow, BillSoftArg args)
		{
			zip_code zip = new zip_code();
			zip.tax_data = GetTaxData(uow, args);
			zip.zip_addr = GetAddress(uow, args); 

			return zip;
		}

		uint ToBillSoftDate(DateTime date)
		{
			uint year  = (uint) date.Year * 10000;
			uint month = (uint)date.Month * 100;
			uint day   = (uint)date.Day;
			return year + month + day;
		}
		EZTax_data GetTaxData(UOW uow, BillSoftArg args)
		{
			EZTax_data td = new EZTax_data();

			td.business   = EZTaxConst.EZ_FALSE;
			td.sale       = EZTaxConst.EZ_TRUE;
			td.regulated  = EZTaxConst.EZ_TRUE;
			td.incorp     = EZTaxConst.EZ_TRUE;

			td.trans_type = args.tranType;
			td.srv_type   = args.servType;
			td.lines      = args.lines;
			td.locations  = args.locs;

			td.date       = ToBillSoftDate(args.tranDate);
			td.charge     = (float)args.tranAmt;
			td.s_exempt   = IntPtr.Zero;
			
			td.cust_no    = "Zip: " + args.zipcode;
			td.inv_no	  = (uint)uow.Id; 
			td.srv_lvl_no = (uint)(DateTime.Now.Hour * 10000 + DateTime.Now.Minute * 100 + DateTime.Now.Second);	
			td.optional   = (uint)((DateTime.Today.Year - 2000) * 10000 + DateTime.Today.Month * 100 + DateTime.Today.Day);
					
			return td;
		}
		zip_address GetAddress(UOW uow, BillSoftArg args)
		{
			zip_address addr = new zip_address();

			addr.country_ISO = "USA";
			addr.zip_code    = args.zipcode;
			addr.state_abv   = DmaZip.getState(uow, args.zipcode); 

			return addr;
		}

	#endregion

	#region Logging
		static void LogInput(IUOW uow, int prod, TaxCode tc, decimal taxable, string zip)
		{
			string message 
				= "Uow " + uow.Id.ToString()
				+ ", DPI Product " + prod.ToString()
				+ ", Tran Type " + tc.BillSoftTran.ToString()
				+ ", Serv Type " + tc.BillSoftServ.ToString()
				+ ", Amount " + taxable.ToString("C")
				+ ", Zip " + zip;

			DPI_Err_Log.AddLogEntry(ErrLogSubSystems.EZTaxWrapper.ToString(), "N/A", message);
		}

		static void LogTax(IUOW uow, taxes_tbl tax, int prod)
		{
			DPI_Err_Log.AddLogEntry(
				ErrLogSubSystems.EZTaxWrapper.ToString(), 
				"N/A", 
				"Uow " + uow.Id.ToString()
					   + ", DPI Product "  + prod.ToString() 
					   + ", Billsoft Tax Id " + tax.tax_type.ToString()
					   + ", Tax Amt " + Decimal.Round((decimal)tax.tax_amount, 2).ToString("C"));
		}
	#endregion

	#region Converters

		static IDmdTax[] ConvertToDTaxes(UOW uow, taxes_tbl[] taxes, int prod)
		{
			IDmdTax[] dtaxes = new IDmdTax[taxes.Length];

			for (int i = 0; i < dtaxes.Length; i++)
			{
				dtaxes[i] = new DmdTax(uow);
				dtaxes[i].TaxProd = prod;
				dtaxes[i].TaxAmount = (decimal)taxes[i].tax_amount;
				dtaxes[i].TaxId = taxes[i].tax_type.ToString();
				LogTax(uow, taxes[i], prod);
			}

			return dtaxes;
		}

		static IDmdTax[] ConvertToDTaxes(UOW uow, ArrayList ar, int prod)
		{
			taxes_tbl[] taxes = new taxes_tbl[ar.Count];
			ar.CopyTo(taxes);
			return ConvertToDTaxes(uow, taxes, prod);
		}

		static taxes_tbl[] ConvertToBillSoftTaxes(ArrayList ar)
		{
			if (ar == null)
				throw new ArgumentException("Missing ar");

			taxes_tbl[]  taxes = new taxes_tbl[ar.Count];
			ar.CopyTo(taxes);
			return taxes;
		}
		

	#endregion

	#region BillSoftArg
		class BillSoftArg
		{
		#region Data
			string _country; 
			string _state;
			public string zipcode; 
			public short tranType;
			public short servType;
			public DateTime tranDate;
			public decimal tranAmt;
			public short lines;
			public short locs;
		#endregion

		#region Properties
			public string country
			{
				get { return _country.ToUpper(); }
				set { _country = value; }
			}
			public string state
			{
				get { return _state.ToUpper();}
				set { _state = value;		  }
			}
		#endregion
		}
	#endregion
	}
}