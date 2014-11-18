using System;
using DPI.Interfaces;
using System.Collections;

namespace DPI.Components
{ 
	public class OrderedProduct : IOrderedProduct
	{
		public ProdPrice prod;  // the ordered product

		public ProdPrice[] fees;  // any fees associated with this product
		public ProdTax[] taxesOnFees;
		public decimal taxesOnFeesAmt;
		
		ProdTax[] taxes;
		public decimal taxesAmt;

		public string action;  // "Add" or "Remove" (?) Only for UI to display.
		decimal totalAmt;
		string zipcode;
		public Component[] components;

		
		public OrderedProduct (UOW uow, ProdPrice pp, string zip, string ilecCode, OrderType type, int svcMonth)
		{
			ProdComposition[] pca;
			int locid;
			int ilecid;
			Location l;

			this.zipcode=zip;
			this.prod=pp;
			
			Console.WriteLine("location info {0}", zip);

			l=Location.find(uow, zip);
			Console.WriteLine("got locid");
			locid=l.LocId;
			ilecid=ILECInfo.Find(uow, ilecCode).OrgId;

			//action = // ** TODO:  must set this so cust knows if we're adding or deleting the product!

			// get fees for this product and order type, at this location.
			// Note: Fees are untaxed at this point in the process.
			Console.WriteLine("fees");
			fees=ProductFee.getFeesForProd(uow, pp.ProdId, zip, ilecCode, type);

			// get package components
			Console.WriteLine("components");
			pca=ProdComposition.getAllVisiblePackageComp(uow, pp.ProdId);
			Console.WriteLine("got components");
			
			if (pca.Length>0)  // it's a package!  Tax components, not package.
			{
				taxes = new ProdTax[0];
				// now get non-package prices for each component
				decimal componentSum=pp.PriceAmt;
				decimal discountPercentage=1;
				components=Component.getPrices(uow, locid, ilecid, pca, ref componentSum, svcMonth);

				if (componentSum != 0)
					discountPercentage=pp.PriceAmt/componentSum;
				Console.WriteLine("tax components");
				TaxComponents(uow, discountPercentage, svcMonth);
			}
			else  // not a package
			{
				this.components = new Component[0];
				Console.WriteLine("single prod taxes");
				taxes=Tax.findTaxes(uow, zip, pp.TaxCode);
				ProdTax.SetTaxedValues(taxes, pp.PriceAmt);
			}
			this.taxesAmt=this.SumTaxAmts(taxes);

			Console.WriteLine("tax fees");
			this.taxesOnFees=new ProdTax[0];
			TaxFees(uow, fees, svcMonth);

			Console.WriteLine("calctotal");
			CalcTotalAmt();
		}

		internal OrderedProduct(){}

	
		public IProdPrice Prod {get {return this.prod;}}  // the ordered product
		public IProdPrice[] Components 
		{
			get	
			{
				ArrayList al = new ArrayList(components.Length);

				for (int x=0; x<components.Length; x++)
				{
					ProdPrice c=components[x].priceInfo;
					al.Add(new ProdPrice(c.id, c.package, false, 0, c.isRecurring,
						c.priceRule, c.priceType, ProdSelectionState.Unavailable, true));
				}
				return (IProdPrice[])al.ToArray(typeof(IProdPrice));
			}		
		}
		public IProdPrice[] Fees {get {	return fees;}}  // any fees associated with this product
		public IProdTax[] Taxes 
		{
			get 
			{
				if (this.prod.StartServMon == 1)
					return taxes;
				else
					return new IProdTax[0];
			}
		}
		public string Action {get {return action;}}  // "Add," "Remove," etc.  Only for UI to display.
		public decimal TotalAmt 
		{
			get 
			{
				if (this.prod.StartServMon == 1)
					return totalAmt;
				else
					return 0;
			}
		}

		decimal SumTaxAmts(ProdTax[] pta)
		{
			decimal ret=0;
			for (int i=0; i<pta.Length; i++)
			{
				ret += pta[i].TaxAmt;
			}
			return ret;
		}

		decimal SumProdPriceAmts(ProdPrice[] ppa)
		{
			decimal ret=0;
			for (int i=0; i<ppa.Length; i++)
			{
				ret += ppa[i].PriceAmt;
			}
			return ret;
		}

		void TaxFees(UOW uow, ProdPrice[] fees, int svcmonth)
		{
			this.taxesOnFeesAmt=0;
			for (int i=0; i<fees.Length; i++)
			{
				ProdTax[] pta;
				pta = Tax.findTaxes(uow, zipcode, fees[i].TaxCode);
				Console.WriteLine("Back from findtaxes");

				ProdTax.SetTaxedValues(pta, fees[i].PriceAmt);
				Console.WriteLine("Back from settaxedvalues");
				taxesOnFees = ProdTax.SumTaxes(taxesOnFees, pta);
				Console.WriteLine("Back from sumtaxes");

				Product product;
				product = Product.find(uow, fees[i].ProdId);
				if (product.StartServMon > svcmonth || (product.EndServMon>0 && product.EndServMon < svcmonth))
					continue;
				// Fee charged right away?
				for(int j=0; j<taxesOnFees.Length; j++)
				{
					taxesOnFees[j].TaxedProdId=fees[i].ProdId;
					taxesOnFeesAmt+= pta[j].TaxAmt;
				}
			}
		}
		void CalcTotalAmt()
		{
			this.totalAmt = this.prod.PriceAmt
				+ this.taxesAmt 
				+ this.taxesOnFeesAmt 
				+ SumProdPriceAmts(this.fees);
		}

		void TaxComponents(UOW uow, decimal percentage, int svcmonth)
		{
			UOW uow2 = new UOW();

			for (int i=0; i<components.Length; i++)  
				//  ASSUMPTION! Not writing this recursively since we're not supposed to have packages in packages. 
			{
				Product product;
				product = Product.find(uow2, components[i].priceInfo.ProdId);
				if (product.StartServMon > svcmonth || (product.EndServMon>0 && product.EndServMon < svcmonth))
					continue;  // skip it for now, we don't charge for it until later.

				ProdTax[] itemTaxes;
				// calculate the correct percentage the original price, for taxing.
				components[i].priceInPackage = components[i].priceInfo.PriceAmt * percentage;
				// what are the taxes on the component? 
				itemTaxes=Tax.findTaxes(uow2, zipcode, components[i].priceInfo.TaxCode);
				Console.WriteLine("tax count on component {0} is {1}", components[i].priceInfo.ProdId, itemTaxes.Length);
				// now set the actual dollar amounts of the taxes
				ProdTax.SetTaxedValues(itemTaxes, components[i].priceInPackage);
				Console.WriteLine("Back from setTaxedValues");
				// store them in the component itself in case we need to report them later.
				components[i].discountedTaxes=itemTaxes;

				this.taxes=ProdTax.SumTaxes(this.taxes, itemTaxes);
				Console.WriteLine("Back from SumTaxes");
			}

		}


		/*****
		 *  Need to be able to remove a fee from an assembled orderedproduct 
		 * because some fees will only be charged once per order.  That logic 
		 * is handled by the ordersummary object.
		 */
		public void RemoveFee (UOW uow, int prodid, int svcMonth)
		{
			ArrayList al = new ArrayList();
			for (int i=0; i<fees.Length; i++)
			{
				if (fees[i].ProdId != prodid)
					al.Add(fees[i]);
			}
			fees=new ProdPrice[al.Count];
			al.CopyTo(fees);

			this.TaxFees(uow, fees, svcMonth);
			this.CalcTotalAmt();
		}
	}
    [Serializable] 
	public class Component 
	{
		public ProdPrice priceInfo;
		public decimal priceInPackage;
		public ProdTax[] discountedTaxes;


		public static Component[] getPrices(UOW uow, int loc, int ilec, ProdComposition[] pca, ref decimal componentSum, int svcMonth)
		{
			Component[] comps=new Component[pca.Length];
			ProdPrice pp;
			componentSum = 0;
				
			for (int i=0; i<pca.Length; i++)
			{

				Component comp = new Component();
				pp = ProdPrice.getPriceForProd(uow, pca[i].SubProd, loc, ilec);
				comp.priceInfo = pp;
				comp.priceInPackage = pp.PriceAmt;
				if (pp.StartServMon == 1)	// ***** ASSUMPTION: This is only used for an item being ordered NOW, whether new, change or whatever.
					componentSum=componentSum + pp.unitPrice;
				comps[i] = comp;
			}
			return comps;			
		}



		#region IProdPrice Members
		/***
		public int  PackageId // to compile - alex
		{
			get { return priceInfo.PackageId; }
			set { priceInfo.PackageId = value; }
		}
		public decimal PriceAmt
		{
			get
			{
				return 0;
			}
		}

		public string Description
		{
			get
			{
				return priceInfo.Description;
			}
		}

		public bool IsPurchased
		{
			get
			{
				return false;
			}
		}

		public string ProdName
		{
			get
			{
				return priceInfo.ProdName;
			}
		}

		public string ProdType
		{
			get
			{
				return priceInfo.ProdType;
			}
		}

		public DPI.Interfaces.ProdSelectionState ProdSelState
		{
			get
			{
				return DPI.Interfaces.ProdSelectionState.Unavailable;
			}
			set
			{
				// no-op
			}
		}

		public int ProdId
		{
			get
			{
				return priceInfo.ProdId;
			}
		}

		public bool Locked
		{
			get
			{
				return true;
			}
		}

		***/
		#endregion
	}

}
