using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	public class InternetWF : IWorkflow
	{
	#region Data
		static WIP.WipStep[] steps; 
		static string name = "Internet";
		static string imageTag = "images/subtable_CustInq.jpg";
	#endregion
	
	#region Properties
		public string Name { get { return name; }}
		public string ImageTag { get { return imageTag; }}
		public static WIP.WipStep PurchaseProduct
		{
			get 
			{ 
				checkExist();
				return steps[0]; 
			}
		}
		public int Count	{ get { return steps.Length; }}
		public IWipStep FirstStep	{get { return PurchaseProduct; }}
	#endregion
	
	#region Constructors		
		InternetWF()
		{
			ArrayList ar = new ArrayList();
			
			WIP.WipStep purchaseProduct =	new WIP.WipStep(this, "Internet", "Internet.aspx"); 
			ar.Add(purchaseProduct);

			WIP.WipStep receipt =			new WIP.WipStep(this, "Receipt", "Receipt.aspx");  
			ar.Add(receipt);

			purchaseProduct.SetNext(receipt);						
 
			steps = new WIP.WipStep[ar.Count];
			ar.CopyTo(steps);
		}
	#endregion
	
	#region Methods
		public static IWipStep GetFirst()
		{
			if (steps == null)
				new InternetWF();

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
			InternetWF wf;
			if (steps == null)
				wf = new InternetWF();
		}
	#endregion
	}
}			