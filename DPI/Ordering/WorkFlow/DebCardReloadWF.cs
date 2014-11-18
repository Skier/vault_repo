using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	public class DebCardReloadWF : IWorkflow
	{
		static WIP.WipStep[] steps; 
		static string name = "Debit Card Reload";
		static string imageTag = "images/subtable_header_purpose.jpg";
		/*		Properties		*/
		public string ImageTag { get { return imageTag; }}
		public string Name { get { return name; }}
		public static WIP.WipStep DebitCard1
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
		public  IWipStep FirstStep	{get { return DebitCard1; }}
		public static IWipStep GetFirst()
		{
			if (steps == null)
				new DebCardReloadWF();

			return steps[0];
		}

		
		/*		Methods		*/
		public int CurrStep(IWipStep curr)
		{
			for (int i  = 0; i < steps.Length; i++)
				if (steps[i] == curr)
					return ++i;
			
			throw new ArgumentException("Step is not found");
		}
		/*		Implementation		*/
		DebCardReloadWF()
		{
			ArrayList ar = new ArrayList();

			WIP.WipStep dcReload =		new WIP.WipStep(this, "Purchase Summary", "ReloadSummary.aspx"); // page by Plasma
			ar.Add(dcReload);

			WIP.WipStep rcpt =		new WIP.WipStep(this, "Debit Card Receipt", "Receipt.aspx"); 
			ar.Add(rcpt);

			dcReload.SetNext(rcpt);

			steps = new WIP.WipStep[ar.Count];
			ar.CopyTo(steps);
		}
		static void checkExist()
		{
			DebCardReloadWF wf;
			if (steps == null)
				wf = new DebCardReloadWF();
		}
	}
}
