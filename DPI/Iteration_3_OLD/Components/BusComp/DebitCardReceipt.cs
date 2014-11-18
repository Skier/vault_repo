using System;
using System.Collections;
using System.Text;

using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Components
{
	[Serializable]
	public class DebitCardReceipt : Receipt, IDebitCardReceipt
	{
		bool isApproved;
		public bool IsApproved { get { return isApproved; }}

		public string[] Msg	
		{
			get 
			{
				return IsApproved ?  
					     new string [] { "Thank you for purchasing.", 
						  			     "The credit card reload is successful.", 
										 "To activate you account, please do this."}
					  :  new string [] { "The Host System has declined this transaction" }; }
		}

			public DebitCardReceipt(int dmd, string confNumber, bool isApproved) : base(dmd, confNumber, 0)
		{
		//  PurposeTransactionService pts = new PurposeTransactionService();
		//  string response = pts.ProcessTransaction(xmlMessage, 1000);

            this.isApproved = isApproved;
		}
	}
}