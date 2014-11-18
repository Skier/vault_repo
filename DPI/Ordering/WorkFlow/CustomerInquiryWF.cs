using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	public class CustomerInquiryWF : IWorkflow
	{
	#region Member Variables
		static WIP.WipStep[] steps; 
		static string name = "Customer Inquiry";
		static string imageTag = "images/subtable_CustInq.jpg";
	
	#endregion
	
	#region Properties
		public string Name { get { return name; }}
		public string ImageTag { get { return imageTag; }}
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
	#endregion
	
	#region Constructors		
		CustomerInquiryWF()
		{
			ArrayList ar = new ArrayList();
			
			WIP.WipStep getPhone		= new WIP.WipStep(this, "Enter Phone Number", "FindCustomer.aspx"); 
			ar.Add(getPhone);

			WIP.WipStep accountSummary	= new WIP.WipStep(this, "Account Summary", "CI_AccountSummary.aspx");  
			ar.Add(accountSummary);

			WIP.WipStep recPymtSetup	= new WIP.WipStep(this, "Recurring Payment Setup", "CustRecurringPymts.aspx");
			ar.Add(recPymtSetup);

			WIP.WipStep custSetup	= new WIP.WipStep(this, "Customer Setup", "CustRecPymtsSet.aspx");
			ar.Add(custSetup);
			
			getPhone.SetNext(accountSummary);
			
			accountSummary.SetPrev(getPhone);
			accountSummary.SetNext(recPymtSetup);

			recPymtSetup.SetPrev(accountSummary);
			recPymtSetup.SetNext(custSetup);

			custSetup.SetPrev(recPymtSetup);
 
			steps = new WIP.WipStep[ar.Count];
			ar.CopyTo(steps);
		}
	#endregion

	#region Methods

		public int CurrStep(IWipStep curr)
		{
			for (int i  = 0; i < steps.Length; i++)
				if (steps[i] == curr)
					return ++i;
			
			throw new ArgumentException("Step is not found");
		}
		static void checkExist()
		{
			CustomerInquiryWF wf;
			if (steps == null)
				wf = new CustomerInquiryWF();
		}
		public static IWipStep GetFirst()
		{
			if (steps == null)
				new CustomerInquiryWF();

			return steps[0];
		}

	#endregion
	}
}			