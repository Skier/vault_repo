using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	[Serializable]
	public class PendOrdersWIP : WIP
	{
		/*		Data		*/
		int tran;
		DateTime date;
		int acctNumber;
		decimal ldAmount;
		decimal localAmount;
		Transaction_Type_Id tranType;
	
		/*		Properties		*/
		public override IWipStep FirstStep { get { return WorkflowFact.PendOrdersFirstStep(); }}
		protected override object find(string attr)
		{
			if (attr == null)
				return null;

			switch(attr.ToLower())
			{
				case "tran" :
					return tran;
					break;

				case "date" :
					return date;
					break;

				case "acctNumber" :
					return acctNumber;
					break;

				case "ldAmount" :
					return ldAmount;
					break;

				case "salesidrequired" :
					return true;
					break;

				case "localAmount" :
					return localAmount;
					break;

				case "trantype" :
					return tranType;
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
				case "tran" :
					tran = (int)obj;
					break;
				
				case "date" :
					date = (DateTime)obj;
					break;
				
				case "acctNumber" :
					acctNumber = (int)obj;
					break;
				
				case "ldAmount" :
					ldAmount = (decimal)obj;
					break;
				
				case "localAmount" :
					localAmount = (decimal)obj;
					break;
				case "trantype" :
					tranType = (Transaction_Type_Id)obj;
					break;

				default :
					throw new ArgumentException("No such property: '" + attr + "'"); 
			}
		}

		/*		Constructors		*/
		public PendOrdersWIP(string user, string clerkId, string storeCode) : base(user, clerkId, storeCode)
		{
			CurrStep = (WIP.WipStep)FirstStep;
		}
	}
}