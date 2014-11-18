using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	[Serializable]
	public class DebCardReloadWip : WIP
	{
		/*		Data		*/
		IProdPrice     debCard;
		IProdPrice[]   fees;
		IDemand		   demand;
		IPayInfo       payInfo;
		ICustInfo      custInfo;
		IReceipt       receipt;
		ICardApp       debCardApp;
		IDmdItem       dItem;
		IWireless_Transactions tran;
		
		/*		Properties		*/
		public override IWipStep FirstStep { get { return WorkflowFact.DebCardReloadFirstStep(); }}
		protected override object find(string attr)
		{
			if (attr == null)
				return null;

			switch(attr.ToLower())
			{
				case "debcard" :
					return debCard;
				
				case "fees" :
					return fees;

				case "demand" :
					return demand;

				case "payinfo" :
					return payInfo;
				
				case "custinfo" :
					return custInfo;

				case "receipt" :
					return receipt;

				case "ordertype" :
					return OrderType.Add;

				case "debcardapp" :
					return debCardApp;

				case "ditem" :
					return dItem;

				case "tran" :
					return tran;
					
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
				case "fees" :
				{
					fees = (IProdPrice[])obj;
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
					custInfo = (ICustInfo)obj;
					break;
				}
				case "receipt" :
				{
					receipt = (IReceipt)obj;
					break;
				}
				case "debcardapp" :
				{
					debCardApp = (ICardApp)obj;
					break;
				}
				case "ditem" :
				{
					dItem = (IDmdItem)obj;
					break;
				}
				case "tran" :
				{	
					tran = (IWireless_Transactions)obj;
					break;
				}
				default :
					throw new ArgumentException("No such property: '" + attr + "'");
			}
		}
		/*		Constructors		*/
		public DebCardReloadWip(string user, string clerkId, string storeCode) : base(user, clerkId, storeCode)
		{
			CurrStep = (WIP.WipStep)FirstStep;
		}
	}
}