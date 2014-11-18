using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;
using DPI.Services;

namespace DPI.Ordering
{
	[Serializable]
	public class MonthlyPaymentWip : WIP	
	{
		/*		Data		*/
		string        phone;
		IAcctInfo     acctInfo;
		ICustInfoExt  custInfoExt;
		IProdPrice[]  prods;
		IPayInfo payInfo;
		IReceipt      receipt;
		IDemand		  demand;
		bool          isHighTouch;
		bool		  isHighTouchDisallowed = false;	
		bool          isConfReq; // this particular order is pending
		
		public override IWipStep FirstStep { get { return WorkflowFact.MonthlyPaymentFirstStep(isConfReq); }}
		/*		Constructors		*/
		public MonthlyPaymentWip(IUser user) : base(user.DisplayName, user.ClerkId, user.LoginStoreCode)
		{
			isConfReq = isHighTouch = StoreSvc.IsRac_WF(user);
			CurrStep = (WIP.WipStep)FirstStep;
		}
		/*		Methods		*/
		protected override object find(string attr)
		{
			if (attr == null)
				return null;

			switch(attr.ToLower())
			{
				case "phone" :
					return phone;

				case "acctinfo" :
					return acctInfo;

				case "custinfoext" :
					return custInfoExt;

				case "prods" :
					return prods;

				case "payinfo" :
					return payInfo;

				case "receipt" :
					return receipt;

				case "ishightouchdisallowed" :
					return isHighTouchDisallowed;

				case "ishightouch" :
					return isHighTouch;

				case "demand" :
					return demand;
					
				case "salesidrequired" :
					return true;
					
				case "isconfreq" :
					return isConfReq;

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
				case "phone" :
				{
					phone = (string)obj;
					break;
				}
				case "acctinfo" :
				{
					acctInfo = (IAcctInfo)obj;
					break;
				}
				case "custinfoext" :
				{
					custInfoExt = (ICustInfoExt)obj;
					break;
				}
				case "prods" :
				{
					prods = (IProdPrice[])obj;
					break;
				}
				case "payinfo" :
				{
					payInfo = (IPayInfo)obj;
					break;
				}
				case "receipt" :
				{
					receipt = (IReceipt)obj;
					break;
				}
				case "ishightouch" :
				{
					isHighTouch = (bool)obj;
					break;
				}
				case "demand" :
				{
					demand = (IDemand)obj;
					break;
				}
				case "isconfreq" :
				{
					isConfReq = (bool)obj;
					break;
				}

				default :
					throw new ArgumentException("No such property: '" + attr + "'"); 
			}
		}

		/*		Implementation		*/
	}
}