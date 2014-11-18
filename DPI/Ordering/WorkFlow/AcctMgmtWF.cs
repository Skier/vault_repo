using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;

namespace DPI.Ordering
{
	public class AcctMgmtWF : IWorkflow
	{
		#region Data

		static WIP.WipStep[] steps; 
		static string name = "Promo";
		static string imageTag = null;//"images/subtable_CustInq.jpg";

		#endregion
	
		#region Properties

		public string Name { get { return name; }}
		public string ImageTag { get { return imageTag; }}
		public static WIP.WipStep Registration
		{
			get 
			{ 
				checkExist();
				return steps[0]; 
			}
		}
		public int Count	{ get { return steps.Length; }}
		public IWipStep FirstStep	{get { return Registration; }}

		#endregion
	
		#region Constructors		

		AcctMgmtWF()
		{
			ArrayList ar = new ArrayList();
			
			// Find Customer
			WIP.WipStep findCustomer = new WIP.WipStep(this, "Find Customer", "FindCustomerAcctMgmt.aspx"); 
			ar.Add(findCustomer);

			// AcctMgmt
			WIP.WipStep acctMgmt = new WIP.WipStep(this, "Account Management", "AcctMgmt.aspx");  
			ar.Add(acctMgmt);
		
			findCustomer.SetNext(acctMgmt);
			acctMgmt.SetPrev(findCustomer);
			
			steps = new WIP.WipStep[ar.Count];
			ar.CopyTo(steps);
		}

		#endregion
	
		#region Methods
		public static IWipStep GetFirst()
		{
			if (steps == null)
				new AcctMgmtWF();

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
			if (steps == null)
				new AcctMgmtWF();
		}
		#endregion
	}
}			