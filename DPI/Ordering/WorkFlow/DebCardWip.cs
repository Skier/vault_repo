using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	[Serializable]
	public class DebCardWip : WIP
	{
	#region Data 
		IProdPrice			debCard;
		IDemand				demand;
		IPayInfo			payInfo;
		ICustInfo2			custInfo;
		IDebitCardReceipt	receipt;
		ICardApp			debCardApp;
		IDmdItem			dItem;

		string cardNumber;
	#endregion
		
	#region Methods
		public override IWipStep FirstStep { get { return WorkflowFact.DebCardFirstStep(); }}
		protected override object find(string attr)
		{
			if (attr == null)
				return null;

			switch(attr.ToLower())
			{
				case "debcard" :
					return debCard;

				case "demand" :
					return demand;

				case "payinfo" :
					return payInfo;
				
				case "custinfo" :
					return custInfo;

				case "receipt" :
					return receipt;

				case "cardnumber" :
					return cardNumber;
					
				case "ordertype" :
					return OrderType.New;

				case "debcardapp" :
					return debCardApp;

				case "ditem" :
					return dItem;

				case "tran" :
					return null;

				case "salesidrequired" :
					return true;

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
				case "debcard" :
				{
					debCard = (IProdPrice)obj;
					break;
				}
				case "demand" :
				{
					demand = (IDemand)obj;
					break;
				}
				case "payinfo" :
				{
					payInfo = (IPayInfo)obj;
					break; 
				}
				case "custinfo" :
				{
					custInfo = (ICustInfo2)obj;
					break;
				}
				case "receipt" :
				{
					receipt = (IDebitCardReceipt)obj;
					break;
				}
				case "debcardapp" :
				{
					debCardApp = (ICardApp)obj;
					break;
				}
				case "cardnumber" :
				{
					cardNumber = (string)obj;
					break;
				}
				case "ditem" :
				{
					dItem = (IDmdItem)obj;
					break;
				}

				default :
					throw new ArgumentException("No such property: '" + attr + "'");
			}
		}
	#endregion

	#region Constructors
		public DebCardWip(string user, string clerkId, string storeCode) : base(user, clerkId, storeCode)
		{
			CurrStep = (WIP.WipStep)FirstStep;
		}
	#endregion
	}
}