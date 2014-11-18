using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.Ordering
{
	[Serializable]
	public class PromoWip : WIP	
	{
	#region Data
		IAgentRegistration ar;
		IAddr2 ca;
	#endregion	

	#region	Properties
		public override IWipStep FirstStep { get { return WorkflowFact.PromoFirstStep();  }}
	#endregion		
	
	#region Constructors
		public PromoWip(string user, string clerkId, string storeCode) : base(user, clerkId, storeCode)
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
				case "address" :
					return this.ca;

				case "agentregistration" :
					return ar;

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
				case "address" :
				{
					ca = (IAddr2)obj;
					break;
				}
				case "agentregistration" :
				{
					ar = (IAgentRegistration)obj;
					break;
				}
				default :
					throw new ArgumentException("No such property: '" + attr + "'"); 
			}
		}
	#endregion
	}
}