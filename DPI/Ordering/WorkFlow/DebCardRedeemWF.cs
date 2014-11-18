using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	public class DebCardRedeemWF : IWorkflow
	{
		static WIP.WipStep[] steps; 
		static string name = "Debit Card Enroll";
		static string imageTag = @"images/subtable_header_RedeemDebCard.jpg";	

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
				new DebCardRedeemWF();

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
		DebCardRedeemWF()
		{
			ArrayList ar = new ArrayList();
			
			WIP.WipStep redeem =	new WIP.WipStep(this, "Redeem Debit Card", "DebCardRedeem.aspx");
			ar.Add(redeem);

			WIP.WipStep sumry =	new WIP.WipStep(this, "Redeem Debit Card", "DCRentway.aspx");
			ar.Add(sumry);

			WIP.WipStep custInfo =	new WIP.WipStep(this, "Customer Info", "DCApp.aspx");  
			ar.Add(custInfo);

			WIP.WipStep rcpt =		new WIP.WipStep(this, "Debit Card Receipt", "Receipt.aspx"); 
			ar.Add(rcpt);

			redeem.SetNext(sumry);

			sumry.SetNext(custInfo);
			sumry.SetPrev(redeem);

			custInfo.SetNext(rcpt);
			custInfo.SetPrev(sumry);

			steps = new WIP.WipStep[ar.Count];
			ar.CopyTo(steps);
			steps = new WIP.WipStep[ar.Count];
			ar.CopyTo(steps);
		}
		static void checkExist()
		{
			DebCardRedeemWF wf;
			if (steps == null)
				wf = new DebCardRedeemWF();
		}
	}
}			