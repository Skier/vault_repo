using System;
using DPI.Interfaces;

namespace DPI.ClientComp
{
	[Serializable]  
	public class PaymentInfo //: IPaymentInfo
	{
		DateTime payDate; 
		decimal localAmountDue;
		decimal localAmountPaid;
		decimal ldAmount;  
		decimal amountTendered;		
		PaymentType paymentType;

		/*		Properties		*/
		public DateTime PayDate        { get { return payDate; }}
		public decimal LocalAmountDue  { get { return (localAmountDue > 0) ? localAmountDue : 0; }}
		public decimal LocalAmountPaid { get { return localAmountPaid; }}
		public decimal LdAmount        { get { return ldAmount; }}		
		public decimal TotalAmountDue  { get { return localAmountPaid + ldAmount; }} 
		public decimal AmountTendered  { get { return amountTendered; }}		
		public decimal ChangeAmount    { get { return amountTendered - localAmountPaid - ldAmount; }}		
		public PaymentType PaymentType { get { return paymentType; }}  

		/*		Constructors		*/
		public PaymentInfo(DateTime payDate, 
			decimal localAmountDue,
			decimal localAmountPaid,
			decimal ldAmount,
			decimal amountTendered,			
			PaymentType paymentType)
		{
			this.payDate = payDate;
			this.localAmountDue = localAmountDue;
			this.localAmountPaid = localAmountPaid;
			this.ldAmount = ldAmount;
			this.amountTendered = amountTendered;
			this.paymentType = paymentType;
		}
		public PaymentInfo(decimal localAmountDue,
			decimal localAmountPaid,
			decimal ldAmount,
			decimal amountTendered,			
			PaymentType paymentType) : this (
			DateTime.Now, localAmountDue, localAmountPaid, ldAmount, amountTendered, paymentType)
		{
		}
	}
}