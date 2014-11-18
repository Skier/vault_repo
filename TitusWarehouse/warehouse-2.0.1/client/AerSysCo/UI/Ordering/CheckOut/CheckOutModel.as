package AerSysCo.UI.Ordering.CheckOut
{
	import AerSysCo.UI.Models.ShippingAddressUI;
	import AerSysCo.UI.Models.CustomerUI;
	
	[Bindable]
	public class CheckOutModel
	{
		public static const FLOW_STATE_SHIPMENT_ADDRESS:int = 0;
		public static const FLOW_STATE_SHIPPING_OPTIONS:int = 1;
		public static const FLOW_STATE_CHECKOUT_SUMMARY:int = 2;
		
		public var currentFlowState:int;
		
		public var currentCustomer:CustomerUI;
		public var currentShippingAddress:ShippingAddressUI;
		
	}
}