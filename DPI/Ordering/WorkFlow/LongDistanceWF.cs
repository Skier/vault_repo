using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	public class LongDistanceWF : IWorkflow
	{
		static WIP.WipStep[] steps; 
		static string name = "LD Calling Card";
		static string imageTag = "images/subtable_header_LDCCard.jpg"; 
		public string ImageTag { get { return imageTag; }}

		/*		Properties		*/
		public string Name { get { return name; }}
		public static WIP.WipStep GetPhone
		{
			get 
			{ 
				checkExist();
				return steps[0]; 
			}
		}
		public int Count	{ get { return steps.Length; }}
		public IWipStep FirstStep	{get { return GetPhone; }}
		
		/*		Methods		*/
		public static IWipStep GetFirst()
		{
			if (steps == null)
				new LongDistanceWF();

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
		LongDistanceWF()
		{
			ArrayList ar = new ArrayList();

			WIP.WipStep LDCard =	 new WIP.WipStep(this, "Choose Product", "LDCard.aspx"); 
			ar.Add(LDCard);

			WIP.WipStep findCustomer =	 new WIP.WipStep(this, "Enter Phone Number", "FindCustomer.aspx"); 
			ar.Add(findCustomer);

			WIP.WipStep accountSummary = new WIP.WipStep(this, "Customer Payment", "AccountSummary.aspx");  
			ar.Add(accountSummary);

			WIP.WipStep receipt =		 new WIP.WipStep(this, "Receipt", "Receipt.aspx"); 
			ar.Add(receipt);

			LDCard.SetNext(findCustomer);

			findCustomer.SetNext(accountSummary);
			findCustomer.SetPrev(LDCard);

			accountSummary.SetPrev(findCustomer); 
			accountSummary.SetNext(receipt); 
			
 
			steps = new WIP.WipStep[ar.Count];
			ar.CopyTo(steps);
		}
		static void checkExist()
		{
			LongDistanceWF wf;
			if (steps == null)
				wf = new LongDistanceWF();
		}
	}
}			