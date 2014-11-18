using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	[Serializable]
	public class WirelessWip : WIP	
	{
	#region Data
		IPinProduct[]	pinProducts;
		IPinProduct		selectedPinProduct;
		IDemand			demand;
		IPayInfo		payInfo;
		IPinReceipt		receipt;
		ICellPhoneInfo		cell;
		
	#endregion	

	#region	Properties
		public override IWipStep FirstStep { get { return WorkflowFact.WirelessFirstStep(); }}
	#endregion		

	#region Constructors
		public WirelessWip(string user, string clerkId, string storeCode) : base(user, clerkId, storeCode)
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

				case "salesidrequired" :
					return true;
				
				case "receipt" :
					return receipt;

				case "demand" :
					return demand;

				case "phoneinfo" :
					return cell;
				
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
					if (obj == null)
					{
						selectedPinProduct = null;
						break;
					}
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
				case "phoneinfo" :
				{
					if (obj == null)
					{
						cell = null;
						break;
					}
					cell = (ICellPhoneInfo)obj;
					break;
				}
				default :
					throw new ArgumentException("No such property: '" + attr + "'"); 
			}
		}
	#endregion
	}
}