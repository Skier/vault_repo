//using System;
//using System.Collections;
//using DPI.ClientComp;
//using DPI.Interfaces;
//using DPI.Components;
//
//namespace DPI.Ordering
//{
//	public class DebCardReloadRedeemWF : IWorkflow
//	{
//		static WIP.WipStep[] steps; 
//		static string name = "Debit Card Redeem";
//		static string imageTag = "images/subtable_header_purpose.jpg";
//		/*		Properties		*/
//		public string ImageTag { get { return imageTag; }}
//		public string Name { get { return name; }}
//		public static WIP.WipStep DebitCard1
//		{
//			get 
//			{ 
//				checkExist();
//				return steps[0]; 
//			}
//		}
//		public int Count
//		{
//			get { return steps.Length; } 
//		}
//		public  IWipStep FirstStep	{get { return DebitCard1; }}
//		public static IWipStep GetFirst()
//		{
//			if (steps == null)
//				new DebCardReloadRedeemWF();
//
//			return steps[0];
//		}
//
//		
//		/*		Methods		*/
//		public int CurrStep(IWipStep curr)
//		{
//			for (int i  = 0; i < steps.Length; i++)
//				if (steps[i] == curr)
//					return ++i;
//			
//			throw new ArgumentException("Step is not found");
//		}
//		/*		Implementation		*/
//		DebCardReloadRedeemWF()
//		{
//			ArrayList ar = new ArrayList();
//
//			WIP.WipStep dcRedeem = new WIP.WipStep(this, "Debit Card Redemption", "DebCardRedeem.aspx"); 
//			ar.Add(dcReload);
//
//			steps = new WIP.WipStep[ar.Count];
//			ar.CopyTo(steps);
//		}
//		static void checkExist()
//		{
//			DebCardReloadRedeemWF wf;
//			if (steps == null)
//				wf = new DebCardReloadRedeemWF();
//		}
//	}
//}
