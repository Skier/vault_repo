using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	public class ReversalVoidWF : IWorkflow
	{

	#region Member Variables
		static WIP.WipStep[] steps; 
		static string name = "Reversal/Void";
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
		ReversalVoidWF()
		{
			ArrayList ar = new ArrayList();
			
			WIP.WipStep selectAccount =	new WIP.WipStep(this, "ReversalVoid", "ReversalVoid.aspx"); 
			ar.Add(selectAccount);

			WIP.WipStep voidConf =	new WIP.WipStep(this, "VoidConfirmation", "VoidConf.aspx");  
			ar.Add(voidConf);

			selectAccount.SetNext(voidConf);						
 
			steps = new WIP.WipStep[ar.Count];
			ar.CopyTo(steps);
		}
	#endregion

	#region Methods
		public static IWipStep GetFirst()
		{
			if (steps == null)
				new ReversalVoidWF();

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
			ReversalVoidWF wf;
			if (steps == null)
				wf = new ReversalVoidWF();
		}
	#endregion
	}
}