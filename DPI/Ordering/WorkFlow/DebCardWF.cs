using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	public class DebCardWF : IWorkflow
	{
		static WIP.WipStep[] steps; 
		static string name = "Debit Card Enrollment";
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
		public IWipStep FirstStep	{get { return DebitCard1; }}
		/*		Methods		*/
		public static IWipStep GetFirst()
		{
			if (steps == null)
				new DebCardWF();

			return steps[0];
		}
		public int CurrStep(IWipStep curr)
		{
			for (int i  = 0; i < steps.Length; i++)
				if (steps[i] == curr)
					return ++i;
			
			throw new ArgumentException("Step is not found");
		}
		/*		Implementation		*/
		DebCardWF()
		{
			ArrayList ar = new ArrayList();
			
			WIP.WipStep purchSum =		new WIP.WipStep(this, "Purchase Summary", "DCSummary.aspx");
			ar.Add(purchSum);
		
			WIP.WipStep custInfo =		new WIP.WipStep(this, "Customer Info", "DCApp.aspx");  
			ar.Add(custInfo);

			WIP.WipStep rcpt =		new WIP.WipStep(this, "Debit Card Receipt", "Receipt.aspx"); 
			ar.Add(rcpt);

			purchSum.SetNext(custInfo);

			custInfo.SetNext(rcpt);
			custInfo.SetPrev(purchSum);

			steps = new WIP.WipStep[ar.Count];
			ar.CopyTo(steps);
		}
		static void checkExist()
		{
			DebCardWF wf;
			if (steps == null)
				wf = new DebCardWF();
		}
	}
}			