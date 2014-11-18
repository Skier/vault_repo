using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	public class ReprintReceiptWF : IWorkflow
	{
	#region Member Variables
		static WIP.WipStep[] steps; 
		static string name = "Reprint Receipt";
		static string imageTag = "images/subtable_header_ReprintReceipt.jpg";
	#endregion

	#region	Properties
		public string Name { get { return name; }}
		public string ImageTag { get { return imageTag; }}
		public static WIP.WipStep GetZip
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
		public IWipStep FirstStep	{get { return GetZip; }}
	#endregion
		
	#region	Constructors
		ReprintReceiptWF()
		{
			ArrayList ar = new ArrayList();
			
			WIP.WipStep getDate =		new WIP.WipStep(this, "Enter Date", "ReprintReceiptsDate.aspx"); 
			ar.Add(getDate);

			WIP.WipStep sumReceipts =	new WIP.WipStep(this, "Select Receipt to Print", "ReprintReceipts.aspx");  
			ar.Add(sumReceipts);

			WIP.WipStep outReceipt =	new WIP.WipStep(this, "Receipt Reprint", "Receipt.aspx");
			ar.Add(outReceipt);

			getDate.SetNext(sumReceipts);

			sumReceipts.SetNext(outReceipt);
			sumReceipts.SetPrev(getDate);
			
			outReceipt.SetPrev(sumReceipts);
 
			steps = new WIP.WipStep[ar.Count];
			ar.CopyTo(steps);
		}
	#endregion
		
	#region	Methods
		public static IWipStep GetFirst()
		{
			if (steps == null)
				new ReprintReceiptWF();

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
			ReprintReceiptWF wf;
			if (steps == null)
				wf = new ReprintReceiptWF();
		}
	#endregion	
	}
}			