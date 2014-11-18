using DPI.Interfaces;

namespace DPI.Ordering
{

	public class WorkflowFact
	{
		public static IWipStep GetNewOrderFirstStep(bool isConfReq)
		{
			if (isConfReq)
				return NewOrderPendWF.GetFirst(); 
		
			return NewOrderWF.GetFirst(); 
		}
		public static IWipStep GetNewPaymentFirstStep()
		{
			return NewPaymentWF.GetFirst(); 
		}
		public static IWipStep CustomerInquiryFirstStep()
		{
			return CustomerInquiryWF.GetFirst(); 
		}
		public static IWipStep DebCardReloadFirstStep()
		{
			return DebCardReloadWF.GetFirst(); 
		}
		public static IWipStep DebCardFirstStep()
		{
			return DebCardWF.GetFirst(); 
		}
		public static IWipStep InternetFirstStep()
		{
			return InternetWF.GetFirst(); 
		}
		public static IWipStep SatelliteFirstStep()
		{
			return SatelliteWF.GetFirst(); 
		}
		public static IWipStep LongDistanceFirstStep()
		{
			return LongDistanceWF.GetFirst(); 
		}
		public static IWipStep MonthlyPaymentFirstStep(bool isConfReq)
		{
			if (isConfReq)
				return MonthlyPymtPendWF.GetFirst(); 
		
			return MonthlyPaymentWF.GetFirst(); 
		}
		public static IWipStep PriceLookupFirstStep()
		{
			return PriceLookupWF.GetFirst(); 
		}
		public static IWipStep ReprintReceiptFirstStep()
		{
			return ReprintReceiptWF.GetFirst(); 
		}
		public static IWipStep ReversalVoidFirstStep()
		{
			return ReversalVoidWF.GetFirst(); 
		}
		public static IWipStep PendOrdersFirstStep()
		{
			return PendOrdersWF.GetFirst();
		}
		public static IWipStep WirelessFirstStep()
		{
			return WirelessWF.GetFirst(); 
		}
		public static IWipStep Wireless2FirstStep()
		{
			return WirelessWF2.GetFirst(); 
		}
		public static IWipStep DebCardRedeemFirstStep()
		{
			return DebCardRedeemWF.GetFirst(); 
		}	

		public static IWipStep DCTestFirstStep()
		{
			return DCTestWF.GetFirst(); 
		}	
		public static IWipStep PromoFirstStep()
		{
			return PromoWF.GetFirst(); 
		}	

		public static IWipStep UpdateCustomerFirstStep()
		{
			return UpdateCustomerWF.GetFirst();
		}

		public static IWipStep AcctMgmtFirstStep()
		{
			return AcctMgmtWF.GetFirst();
		}

		public static IWipStep WLActivationWFFirstStep()
		{
			return WLActivationWF.GetFirst();
		}
	}
}