using System;
using DPI.ClientComp;
using DPI.Interfaces;

namespace DPI.Ordering
{
	[Serializable]
	public class WirelessActivationWIP : WIP	
	{
	#region Data
		IPinProduct			selectedPinProduct;
		IDemand				demand;
		IPayInfo			payInfo;
		ICellPhoneInfo		cell;
		ICellPhoneReceipt	receipt;
		int                 chkActAttempts;
		bool				isCompleted;
		string				confNum;
	#endregion	

	#region	Properties
		public override IWipStep FirstStep { get { return WorkflowFact.WLActivationWFFirstStep(); }}
	#endregion		

	#region Constructors
		public WirelessActivationWIP(IUser user) : base(user.DisplayName, user.ClerkId, user.LoginStoreCode)
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

				case "checkactattempts" :
					return chkActAttempts;

				case "iscompleted" :
					return isCompleted;

				case "confnum" :
					return confNum;

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
				
				case "selectedpinproduct" :
					if (obj == null)
					{
						selectedPinProduct = null;
						return;
					}
					selectedPinProduct = (IPinProduct)obj;
					break;
				
				case "payinfo" :
					if (obj == null)
					{
						payInfo = null;
						return;
					}
					payInfo = (IPayInfo)obj;
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
					receipt = (ICellPhoneReceipt)obj;
					break;
				
				case "phoneinfo" :
					if (obj == null)
					{
						cell = null;
						return;
					}
					cell = (ICellPhoneInfo)obj;
					break;
				
				case "checkactattempts" :
					chkActAttempts = (int)obj;
					break;
				
				case "iscompleted" :
					isCompleted = (bool)obj;
					break;
				
				case "confnum" :
					confNum = (string)obj;
					break;

			default :
				throw new ArgumentException("No such property: '" + attr + "'"); 
			}
		}
		#endregion
	}
}