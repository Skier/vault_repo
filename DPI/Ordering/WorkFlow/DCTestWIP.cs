using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	[Serializable]
	public class DCTestWip : WIP	
	{
	#region Data
		IPinProduct[]	pinProducts;
		IPinProduct		selectedPinProduct;
		IPayInfo		payInfo;
		IDemand			demand;
		IPinReceipt		receipt;
	#endregion	

	#region	Properties
		public override IWipStep FirstStep { get { return WorkflowFact.DCTestFirstStep();  }}
	#endregion		
	
	#region Constructors
		public DCTestWip(string user, string clerkId, string storeCode) : base(user, clerkId, storeCode)
		{
			CurrStep = (WIP.WipStep)FirstStep;
		}
	#endregion
	
	#region	Methods
		protected override object find(string attr)
		{
			if (attr == null)
				return null;

			switch(attr.ToLower())
			{
				case "pinproducts" :
					return pinProducts;

				case "selectedpinproduct" :
					return selectedPinProduct;

				case "payinfo" :
					return payInfo;

				case "demand" :
					return demand;

				case "receipt" :
					return receipt;

				default :
					throw new ArgumentException("No such property: '" + attr + "'"); 
			}
		}
		protected override void load(string attr, object obj)
		{
			if (attr == null)
				return;

			switch(attr.ToLower())
			{
				case "pinproducts" :
				{
					pinProducts = (IPinProduct[])obj;
					break;
				}
				case "selectedpinproduct" :
				{
					selectedPinProduct = (IPinProduct)obj;
					break;
				}
				case "payinfo" :
				{
					payInfo = (IPayInfo)obj;
					break;
				}
				case "demand" :
				{
					demand = (IDemand)obj;
					break;
				}
				case "receipt" :
				{
					receipt = (IPinReceipt)obj;
					break;
				}
				default :
					throw new ArgumentException("No such property: '" + attr + "'"); 
			}
		}
	#endregion
	}
}