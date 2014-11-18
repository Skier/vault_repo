using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	public class PendOrdersWF : IWorkflow
	{

	#region Member Variables
		static WIP.WipStep[] steps; 
		static string name = "Pending/Orders";
		static string imageTag = "images/subtable_header_RevVoid.jpg";
	#endregion

	#region Properties
		public string Name { get { return name; }}
		public string ImageTag { get { return imageTag; }}

		public static WIP.WipStep VoidAccount
		{
			get 
			{ 
				checkExist();
				return steps[0]; 
			}
		}
		public int Count { get { return steps.Length; }}
		public IWipStep FirstStep	{get { return VoidAccount; }}
	#endregion

	#region Constructors		
		PendOrdersWF()
		{
			ArrayList ar = new ArrayList();
			
			WIP.WipStep pendOrders =	new WIP.WipStep(this, "PendingOrders", "PendingOrders.aspx"); 
			ar.Add(pendOrders);

			WIP.WipStep pendConf =	new WIP.WipStep(this, "PendingConfirmation", "PendingConf.aspx");  
			ar.Add(pendConf);

			pendOrders.SetNext(pendConf);						
 
			steps = new WIP.WipStep[ar.Count];
			ar.CopyTo(steps);
		}
	#endregion

	#region Methods
		public static IWipStep GetFirst()
		{
			if (steps == null)
				new PendOrdersWF();

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
			PendOrdersWF wf;
			if (steps == null)
				wf = new PendOrdersWF();
		}
	#endregion
	}
}