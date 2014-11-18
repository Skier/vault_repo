using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	public class MonthlyPaymentWF : IWorkflow
	{
		static WIP.WipStep[] steps; 
		static string name = "Monthly Payment";
		static string imageTag = "images/subtable_MonthlyPay.jpg"; 
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
				new MonthlyPaymentWF();

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
		MonthlyPaymentWF()
		{
			ArrayList ar = new ArrayList();
		
			WIP.WipStep findCustomer   = new WIP.WipStep(this, "Enter Phone Number", "FindCustomer.aspx"); 
			WIP.WipStep accountSummary = new WIP.WipStep(this, "Customer Payment", "AccountSummary.aspx");  
			WIP.WipStep receipt		   = new WIP.WipStep(this, "Receipt", "Receipt.aspx"); 		
		
			ar.Add(findCustomer);		
			ar.Add(accountSummary);
			ar.Add(receipt);
	
			findCustomer.SetNext(accountSummary);
			accountSummary.SetPrev(findCustomer); 
			accountSummary.SetNext(receipt); 
 
			steps = new WIP.WipStep[ar.Count];
			ar.CopyTo(steps);
		}
		static void checkExist()
		{
			MonthlyPaymentWF wf;
			if (steps == null)
				wf = new MonthlyPaymentWF();
		}
	}
}			