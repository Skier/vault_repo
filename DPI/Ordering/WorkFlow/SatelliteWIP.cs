using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	[Serializable]
	public class SatelliteWip : WIP	
	{
		#region Data
		IPinProduct[]	pinProducts;
		IPinProduct		selectedPinProduct;
		IPayInfo[]		payInfos;
		IDemand			demand;
		IPinReceipt		receipt;
		#endregion	

		#region	Properties
		public override IWipStep FirstStep { get { return WorkflowFact.SatelliteFirstStep();  }}
		#endregion		
	
		#region Constructors
		public SatelliteWip(string user, string clerkId, string storeCode) : base(user, clerkId, storeCode)
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
					break;

				case "selectedpinproduct" :
					return selectedPinProduct;
					break;

				case "payinfos" :
					return payInfos;
					break;

				case "demand" :
					return demand;
					break;

				case "salesidrequired" :
					return true;
					break;

				case "receipt" :
					return receipt;
					break;

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
					if (obj == null)
					{
						pinProducts = null;
						return;
					}
					pinProducts = (IPinProduct[])obj;
					break;
				
				case "selectedpinproduct" :
					if (obj == null)
					{
						selectedPinProduct = null;
						return;
					}

					selectedPinProduct = (IPinProduct)obj;
					break;
				
				case "payinfos" :
					if (obj == null)
					{
						payInfos = null;
						return;
					}
					payInfos = (IPayInfo[])obj;
					break;
				
				case "demand" :
					if (obj == null)
					{
						demand = null;
						return;
					}

					demand = (IDemand)obj;
					break;
				
				case "receipt" :
					if (obj == null)
					{
						receipt = null;
						return;
					}

					receipt = (IPinReceipt)obj;
					break;
				
				default :
					throw new ArgumentException("No such property: '" + attr + "'"); 
			}
		}
		#endregion
	}
}