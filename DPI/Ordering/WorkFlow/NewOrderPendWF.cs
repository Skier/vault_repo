using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	public class NewOrderPendWF : WorkflowFact, IWorkflow
	{
		static WIP.WipStep[] steps; 
		static string name = "New Order";
		static string imageTag = "images/subtable_NewOrder1.jpg";

		/*		Properties		*/
		public string ImageTag { get { return imageTag; }}
		public string Name { get { return name; }}
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
		public static IWipStep GetFirst()
		{
			if (steps == null)
				new NewOrderPendWF();
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
		NewOrderPendWF()
		{
			ArrayList ar = new ArrayList();
            		
			WIP.WipStep getZip =		new WIP.WipStep(this, "Enter Zipcode", "GetZip.aspx"); 
			ar.Add(getZip);

			WIP.WipStep ilecInfo =		new WIP.WipStep(this, "Select Local Service Provider", "IlecInfo.aspx");  
			ar.Add(ilecInfo);

			WIP.WipStep prodL1 =		new WIP.WipStep(this, "Select Local Service", "ProductL1.aspx");
			ar.Add(prodL1);

			WIP.WipStep prodL2 =		new WIP.WipStep(this, "Select features", "ProductL2.aspx"); 
			ar.Add(prodL2);

			WIP.WipStep orderDetails =	new WIP.WipStep(this, "Order Summary", "OrderSumPend.aspx"); 
			ar.Add(orderDetails);

			WIP.WipStep servAddr =		new ServAddrStep(this, "Enter Service Address", "ServiceAddr.aspx"); 
			ar.Add(servAddr);

			WIP.WipStep mailAddr =		new WIP.WipStep(this, "Enter Mailing Address", "MailingAddr.aspx");  
			ar.Add(mailAddr);

			WIP.WipStep orderConf =		new WIP.WipStep(this, "Confirm Order", "OrderConf.aspx"); 
			ar.Add(orderConf);

			WIP.WipStep receipt =		new WIP.WipStep(this, "Receipt", "Receipt.aspx"); 
			ar.Add(receipt);

			getZip.SetNext(ilecInfo);
			getZip.SetSkip(prodL1);

			ilecInfo.SetNext(prodL1);
			ilecInfo.SetPrev(getZip);
			
			prodL1.SetNext(prodL2);
			prodL1.SetPrev(ilecInfo);
			prodL1.SetRework(getZip);


			prodL2.SetNext(orderDetails);
			prodL2.SetPrev(prodL1);

			orderDetails.SetNext(servAddr); 
			orderDetails.SetPrev(prodL2); 
 
			servAddr.SetNext(mailAddr);
			servAddr.SetSkip(orderConf);

			mailAddr.SetNext(orderConf);  
			mailAddr.SetPrev(servAddr);

			orderConf.SetNext(receipt);  
			orderConf.SetPrev(servAddr); 
 
			steps = new WIP.WipStep[ar.Count];
			ar.CopyTo(steps);

		}
		static void checkExist()
		{
			NewOrderPendWF wf;
			if (steps == null)
				wf = new NewOrderPendWF();
		}
	}
}			