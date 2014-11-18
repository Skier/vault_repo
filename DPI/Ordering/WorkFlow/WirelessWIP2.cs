using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	[Serializable]
	public class WirelessWip2 : WIP	
	{
	#region Data
		IPinProduct[]	pinProducts;
		IPinProduct		selectedPinProduct;
		IDemand			demand;
		IPayInfo		payInfo;
		IPinReceipt		receipt;
		ICellPhoneInfo		cell;
		bool				isCompleted;
		
	#endregion	

	#region	Properties
		public override IWipStep FirstStep { get { return WorkflowFact.Wireless2FirstStep(); }}
	#endregion		

	#region Constructors
		public WirelessWip2(IUser user) : base(user.DisplayName,user.ClerkId, user.LoginStoreCode)
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
									
				case "iscompleted" :
					return isCompleted;
				
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
				case "iscompleted" :
				{
					isCompleted = (bool)obj;
					break;
				}

				default :
					throw new ArgumentException("No such property: '" + attr + "'"); 
			}
		}
		#endregion
	}
}