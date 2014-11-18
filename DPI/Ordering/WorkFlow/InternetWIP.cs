using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	[Serializable]
	public class InternetWip : WIP	
	{
		#region Data
		IPinProduct[]	pinProducts;
		IPinProduct		selectedPinProduct;
		IPayInfo		payInfo;
		IDemand			demand;
		IPinReceipt		receipt;
		#endregion	

		#region	Properties
		public override IWipStep FirstStep { get { return WorkflowFact.InternetFirstStep();  }}
		#endregion		
	
		#region Constructors
		public InternetWip(string user, string clerkId, string storeCode) : base(user, clerkId, storeCode)
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

				case "salesidrequired" :
					return true;

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
					if (obj == null)
					{
						pinProducts = null;
						return;
					}
					pinProducts = (IPinProduct[])obj;
					break;
				}
				case "selectedpinproduct" :
				{
					if (obj == null)
					{
						selectedPinProduct = null;
						return;
					}

					selectedPinProduct = (IPinProduct)obj;
					break;
				}
				case "payinfo" :
				{
					if (obj == null)
					{
						payInfo = null;
						return;
					}
					payInfo = (IPayInfo)obj;
					break;
				}
				case "demand" :
				{
					if (obj == null)
					{
						demand = null;
						return;
					}

					demand = (IDemand)obj;
					break;
				}
				case "receipt" :
				{
					if (obj == null)
					{
						receipt = null;
						return;
					}

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