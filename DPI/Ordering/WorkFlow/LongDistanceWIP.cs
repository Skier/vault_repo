//LongDistanceWIP
using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	[Serializable]
	public class LongDistanceWIP : WIP	
	{
		/*		Data		*/
		string phone;
		IAcctInfo acctInfo;
		ICustInfoExt custInfoExt;
		IProdPrice[] prods;
		IPayInfo payInfo;
		IReceipt  receipt;
		IDemand	dmd;
		bool isHighTouchDisallowed;

		public bool IsHighTouchDisallowed  { get { return false; }}
		public override IWipStep FirstStep { get { return WorkflowFact.LongDistanceFirstStep(); }}
		
		/*		Constructors		*/
		public LongDistanceWIP(string user, string clerkId, string storeCode) : base(user, clerkId, storeCode)
		{
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
				
				case "demand" :
					return dmd;

				case "ishightouchdisallowed" :
					return isHighTouchDisallowed;
					
				case "salesidrequired" :
					return false;
					
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
				case "demand" :
				{
					dmd = (IDemand)obj;
					break;
				}
				case "ishightouchdisallowed" :
				{
					isHighTouchDisallowed = false;
					break;
				}
				default :
					throw new ArgumentException("No such property: '" + attr + "'"); 
			}
		}

		/*		Implementation		*/
	}
}