using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	public class PriceLookupWF : IWorkflow
	{
	#region Member Variables
		static WIP.WipStep[] steps; 
		static string name = "Price Lookup";
		static string imageTag = "images/subtable_PPLookup.jpg";
	#endregion
	
	#region	Properties
		public string Name { get { return name; }}
		public string ImageTag { get { return imageTag; }}
		public static WIP.WipStep GetZip
		{
			get 
			{ 
				checkExist();
				return steps[0]; 
			}
		}
		public int Count
		{
			get { return steps.Length; } 
		}
		public IWipStep FirstStep	{get { return GetZip; }}
	#endregion
	
	#region	Constructors
		PriceLookupWF()
		{
			ArrayList ar = new ArrayList();
			
			WIP.WipStep getZip =		new WIP.WipStep(this, "Enter Zipcode", "GetZip.aspx"); 
			ar.Add(getZip);

			WIP.WipStep ilecInfo =		new WIP.WipStep(this, "Select Local Service Provider", "IlecInfo.aspx");  
			ar.Add(ilecInfo);

			WIP.WipStep prodL1 =		new WIP.WipStep(this, "Select Local Service", "ProductL1.aspx");
			ar.Add(prodL1);

			WIP.WipStep prodL2 =		new WIP.WipStep(this, "Select features", "ProductL2.aspx"); 
			ar.Add(prodL2);

			WIP.WipStep orderSummary =	new WIP.WipStep(this, "Order Summary", "PL_OrdSummary.aspx"); 
			ar.Add(orderSummary);

			getZip.SetNext(ilecInfo);
			getZip.SetSkip(prodL1);

			ilecInfo.SetNext(prodL1);
			ilecInfo.SetPrev(getZip);
			
			prodL1.SetNext(prodL2);
			prodL1.SetPrev(ilecInfo);
			prodL1.SetRework(getZip);

			prodL2.SetNext(orderSummary);
			prodL2.SetPrev(prodL1);
			
			orderSummary.SetPrev(prodL2);  
 
			steps = new WIP.WipStep[ar.Count];
			ar.CopyTo(steps);
		}
	#endregion
	
	#region	Methods
		public static IWipStep GetFirst()
		{
			if (steps == null)
				new PriceLookupWF();

			return steps[0];
		}
		public int CurrStep(IWipStep curr)
		{
			for (int i  = 0; i < steps.Length; i++)
				if (steps[i] == curr)
					return ++i;
			
			throw new ArgumentException("Step is not found");
		}
		static void checkExist()
		{
			PriceLookupWF wf;
			if (steps == null)
				wf = new PriceLookupWF();
		}
	#endregion	
	}
}			