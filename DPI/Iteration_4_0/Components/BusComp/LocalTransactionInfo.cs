using System;
using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]
	public class LocalTransactionInfo : ILocalTransactionInfo
	{
		#region Member Variables
		int transaction_Id;
		string trConfirm;
		int accNumber;
		Transaction_Type_Id transaction_Type_Id;
		string phNumber;
		DateTime payDate;
		string payTime;
		decimal localAmount;
		decimal ldAmount;
		decimal tax;
		#endregion

		#region Properties
		public int Transaction_Id
		{
			get { return transaction_Id; }
			set { transaction_Id = value; }
		}
		public string TrConfirm
		{
			get { return trConfirm; }
			set { trConfirm = value; }
		}
		public int AccNumber
		{
			get { return accNumber; }
			set { accNumber = value; }
		}
		public Transaction_Type_Id Transaction_Type_Id
		{
			get { return transaction_Type_Id; }
			set { transaction_Type_Id = value; }
		}
			
		public string PhNumber				
		{
			get { return phNumber; }
			set { phNumber = value; }
		}
		
		public DateTime PayDate
		{
			get 
			{
				return payDate;
			}
			set
			{
				payDate = value;
			}
		}

		public string PayTime
		{
			get 
			{
				return payTime;
			}
			set
			{
				payTime = value;
			}
		}
		public decimal LocalAmount
		{
			get { return localAmount; }
			set { localAmount = value; }
		}
		public decimal LDAmount
		{
			get { return ldAmount; }
			set { ldAmount = value; }
		}
		public decimal Tax
		{
			get { return tax; }
			set { tax = value; }
		}
		#endregion
	}
}
