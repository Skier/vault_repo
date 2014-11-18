using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	public class WLActivationWF : IWorkflow
	{
	#region Data
	
		static WIP.WipStep[] steps; 
		static string name = "Wireless Activation";
		static string imageTag = "images/subtable_CustInq.jpg";
	
	#endregion
	
	#region Properties

		public string Name { get { return name; }}
		public string ImageTag { get { return imageTag; }}
		public static WIP.WipStep Activate
		{
			get 
			{ 
				checkExist();
				return steps[0]; 
			}
		}
		public int Count	{ get { return steps.Length; }}
		public IWipStep FirstStep	{get { return Activate; }}

	#endregion
	
	#region Constructors		
		WLActivationWF()
		{
			ArrayList ar = new ArrayList();
			
			WIP.WipStep activate =	new WIP.WipStep(this, "Activate", "WirelessActivation.aspx"); 
			ar.Add(activate);

			WIP.WipStep activationInfo =	new WIP.WipStep(this, "Activation Info", "WLActivationInfo.aspx"); 
			ar.Add(activationInfo);

			WIP.WipStep checkActivation =	new WIP.WipStep(this, "Check Activation", "WLCheckActivation.aspx"); 
			ar.Add(checkActivation);

			WIP.WipStep receipt =			new WIP.WipStep(this, "Receipt", "ReceiptViewer.aspx");  
			ar.Add(receipt);

			activate.SetNext(activationInfo);

			activationInfo.SetNext(checkActivation);
			
			checkActivation.SetNext(receipt);
 
			steps = new WIP.WipStep[ar.Count];
			ar.CopyTo(steps);
		}
	#endregion
	
	#region Methods
		public static IWipStep GetFirst()
		{
			if (steps == null)
				new WLActivationWF();

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
			WLActivationWF wf;
			if (steps == null)
				wf = new WLActivationWF();
		}
	#endregion
	}
}			