using System;
using DPI.Components;
using DPI.Interfaces; 

namespace DPI.Components
{
	[Serializable]
	public class ProdInfo : IProdInfo
	{
		/*        Data        */
		int id;
		string prodType;
		string prodName;
		string prodSubClass;
		bool isComponentOnly;
		bool isBillable;
		bool isProvisionable;
		bool isProvViaMapping;
		bool isAgentVisible;
		string provCategory;
		int supplier;
		int vendor;
		string taxCode;
		int startServMon;
		int endServMon;
		int mappingProd;
		string description;
		string status;
		//	string webDescription;
		string billText;
		string ordSumryStartMon2;
		bool  isPreselectedWebOrderL2;
		bool suppressZeroPriceProd;
		bool isExcludedFromTotalL2;
		bool displayUnclickMessage;
		string prodCategory;


		/*        Properties        */
		public int Id                   { get { return id; }}
		public string ProdName          { get { return prodName; }}
		public string BillText          { get { return billText; }}
		public string ProdType          { get { return prodType; }}
		public string ProdSubClass      { get { return prodSubClass; } }
		public string Status			{ get { return status; }}
		public string ProdCategory      { get { return prodCategory; }} 
		public bool IsComponentOnly	    { get { return isComponentOnly; }}
		public bool IsBillable          { get { return isBillable; }}
		public bool IsProvisionable     { get { return isProvisionable; }}
		public bool IsProvViaMapping    { get { return isProvViaMapping; }}
		public string ProvCategory      { get { return provCategory; } }
		public int Supplier	            { get { return supplier; }}
		public int Vendor               { get { return vendor; }}
		public string TaxCode           { get { return taxCode; }}
		public int StartServMon         { get { return startServMon; }}
		public int EndServMon           { get { return endServMon; }}
		public int MappingProd          { get { return mappingProd; }	}
		public string Description       { get { return description; }}
		public bool IsAgentVisible      { get { return isAgentVisible; }}
		
		public string OrdSumryStartMon2     { get { return ordSumryStartMon2; }}
		public bool IsPreselectedWebOrderL2 { get { return isPreselectedWebOrderL2; }}
		public bool SuppressZeroPriceProd   { get { return suppressZeroPriceProd; }}
		public bool IsExcludedFromTotalL2   { get { return isExcludedFromTotalL2; }}
		public bool DisplayUnclickMessage   { get { return displayUnclickMessage; }}

		/*		Constructors		*/
		public ProdInfo(Product prod)
		{
			id				  = prod.Id;
			prodName		  = prod.ProdName;
			billText		  = prod.BillText;
			prodType		  = prod.ProdType;
			prodSubClass	  = prod.ProdSubClass;
			status			  = prod.Status;
			prodCategory      = prod.ProdCategory;

			isComponentOnly	  = prod.IsComponentOnly;
			isBillable		  = prod.IsBillable;
			isProvisionable   = prod.IsProvisionable;
			isProvViaMapping  = prod.IsProvViaMapping;
			isAgentVisible	  = prod.IsAgentVisible;
			provCategory	  = prod.ProvCategory;
			supplier		  = prod.Supplier;
			vendor			  = prod.Vendor;
			taxCode			  = prod.TaxCode;
			startServMon	  = prod.StartServMon;
			endServMon        = prod.EndServMon;
			mappingProd       = prod.MappingProd;
			description       = prod.WebDescription;

			ordSumryStartMon2       = prod.OrdSumryStartMon2;
			isPreselectedWebOrderL2 = prod.IsPreselectedWebOrderL2; 
			suppressZeroPriceProd   = prod.SuppressZeroPriceProd;
			isExcludedFromTotalL2   = prod.IsExcludedFromTotalL2;
			displayUnclickMessage   = prod.DisplayUnclickMessage;

		}
		public ProdInfo(ProdInfo prod)
		{
			id					= prod.Id;
			prodName			= prod.ProdName;
			billText			= prod.BillText;
			prodType			= prod.ProdType;
			prodSubClass		= prod.ProdSubClass;
			status              = prod.Status; 
			prodCategory        = prod.ProdCategory;
			isComponentOnly		= prod.IsComponentOnly;
			isBillable			= prod.IsBillable;
			isProvisionable		= prod.IsProvisionable;
			isProvViaMapping	= prod.IsProvViaMapping;
			isAgentVisible		= prod.IsAgentVisible;
			provCategory		= prod.ProvCategory;
			supplier			= prod.Supplier;
			vendor				= prod.Vendor;
			taxCode				= prod.TaxCode;
			startServMon		= prod.StartServMon;
			endServMon			= prod.EndServMon;
			mappingProd			= prod.MappingProd;
			description			= prod.description;
			
			ordSumryStartMon2	    = prod.OrdSumryStartMon2;
			isPreselectedWebOrderL2 = prod.IsPreselectedWebOrderL2; 
			suppressZeroPriceProd   = prod.SuppressZeroPriceProd;
			isExcludedFromTotalL2   = prod.IsExcludedFromTotalL2;
			displayUnclickMessage   = prod.DisplayUnclickMessage;
		}
		/*		Methods		*/
		public ProdInfo Clone()
		{
			return new ProdInfo(this);
		}
		public static ProdInfo[] Conv(Product[] prod)
		{
			ProdInfo[] pi = new ProdInfo[prod.Length];
			for (int i = 0; i < pi.Length; i++)
				pi[i] = new ProdInfo(prod[i]);

			return pi;
		}
		
		
		//	string status;
		//		string prodCode;
		//		string oldPriceCode;
		//		string description;
		//		string eligibilityCriteria;
		//		bool isTaxExempt;
		//		DateTime startDate;
		//		DateTime endDate;
		//		string acctCode;
		//		string compCode;
		//		string deptCode;
		//		int predId;
		//		bool isAgentVisible;		
		/*
		public string ProdName
		{
			get { return prodName; }
			set
			{

				prodName = value;
			}
		}
		public string ProdCode 
		{
			get { return prodCode; }

		}
		public string OldPriceCode
		{
			get { return oldPriceCode; }
			set
			{

				oldPriceCode = value;
			}
		}
		public string Description
		{
			get { return description; }
			set
			{

				description = value;
			}
		}
		public string EligibilityCriteria
		{
			get { return eligibilityCriteria; }
			set
			{

				eligibilityCriteria = value;
			}
		}

		public bool IsTaxExempt
		{
			get { return isTaxExempt; }
			set
			{

				isTaxExempt = value;
			}
		}	
		
		public string Status { get { return status; }}
		

		public DateTime StartDate
		{
			get { return startDate; }
			set
			{

				startDate = value;
			}
		}
		public DateTime EndDate
		{
			get { return endDate; }
			set
			{

				endDate = value;
			}
		}
		public string AcctCode
		{
			get { return acctCode; }
			set
			{

				acctCode = value;
			}
		}
		public string CompCode
		{
			get { return compCode; }
			set
			{

				compCode = value;
			}
		}
		public string DeptCode
		{
			get { return deptCode; }
			set
			{

				deptCode = value;
			}
		}
		public int PredId
		{
			get { return predId; }
			set
			{

				predId = value;
			}
		}
		public bool IsAgentVisible
		{
			get { return isAgentVisible; }
			set
			{

				isAgentVisible = value;
			}
		}		
		*/

	}
}