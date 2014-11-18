using System;
using System.Collections;

using DPI.Interfaces;
using DPI.Components.EPSolutions;

namespace DPI.Components
{
	[Serializable]
	public class EnergyItems : IEnergyItems
	{
		#region Data
		decimal prepayAmount;
		IKeyVal[] items;
		INamePrice[] pricedItems;
		#endregion

		#region Properties
		public decimal PrepayAmount			
		{ 
			get { return prepayAmount;  } 
			set { prepayAmount = value; } 
		}
		public IKeyVal[] Items					
		{
			get { return items;  } 
			set { items = value; }
		}
		public INamePrice[] PricedItems					
		{
			get { return pricedItems;  } 
			set { pricedItems = value; }
		}
		#endregion
		public EnergyItems(Quote quote)
		{
			this.prepayAmount = quote.PrepaymentRequired;

			SetItems(quote);
			SetPricedItems(quote);
		}
		public EnergyItems(Balance balance)
		{
			this.prepayAmount = balance.AccountBalance;
			
			SetPricedItems(balance);
		}
		private void SetItems(Quote quote)
		{
			ArrayList ar = new ArrayList();
			
			ar.Add(new KeyVal("Estimated Usage", quote.EstimatedUsage.ToString()));
			ar.Add(new KeyVal("Rate Per KWH", quote.RatePerKwh.ToString("C")));
			
			for (int i = 0; i < quote.EventCharges.Length; i++)
				ar.Add(new KeyVal(quote.EventCharges[i].ChargeDescription, quote.EventCharges[i].ChargeAmount.ToString("C")));

			for (int i = 0; i < quote.NonRecurringCharges.Length; i++)
				ar.Add(new KeyVal(quote.NonRecurringCharges[i].ChargeDescription, quote.NonRecurringCharges[i].ChargeAmount.ToString("C")));

			for (int i = 0; i < quote.RecurringCharges.Length; i++)
				ar.Add(new KeyVal(quote.RecurringCharges[i].ChargeDescription, quote.RecurringCharges[i].ChargeAmount.ToString("C")));

			IKeyVal[] keyVals = new KeyVal[ar.Count];

			ar.CopyTo(keyVals);

			this.items = keyVals;

		}
		private void SetPricedItems(Quote quote)
		{
			ArrayList ar = new ArrayList();
			
			ar.Add(new NamePrice("Rate Per KWH", quote.RatePerKwh));
			
			for (int i = 0; i < quote.EventCharges.Length; i++)
				ar.Add(new NamePrice(quote.EventCharges[i].ChargeDescription, quote.EventCharges[i].ChargeAmount));

			for (int i = 0; i < quote.NonRecurringCharges.Length; i++)
				ar.Add(new NamePrice(quote.NonRecurringCharges[i].ChargeDescription, quote.NonRecurringCharges[i].ChargeAmount));

			for (int i = 0; i < quote.RecurringCharges.Length; i++)
				ar.Add(new NamePrice(quote.RecurringCharges[i].ChargeDescription, quote.RecurringCharges[i].ChargeAmount));

			INamePrice[] np = new NamePrice[ar.Count];

			ar.CopyTo(np);

			this.pricedItems = np;

		}
		private void SetPricedItems(Balance balance)
		{
			ArrayList ar = new ArrayList();

			ar.Add(new NamePrice("Account Balance", balance.AccountBalance));
			
			INamePrice[] np = new NamePrice[ar.Count];
			ar.CopyTo(np);

			this.pricedItems = np;

		}
	}
}