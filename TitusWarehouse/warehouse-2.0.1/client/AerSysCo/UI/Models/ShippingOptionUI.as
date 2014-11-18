package AerSysCo.UI.Models
{
	import AerSysCo.Server.ShippingOption;
	
	[Bindable]
	public class ShippingOptionUI
	{
	    public var shippingTypeId:int;
	    public var shippigType:ShippingTypeUI;
	    public var isApplicable:Boolean;
	    public var cost:Number;

		public function populateFromShippingOption(value:ShippingOption, cascade:Boolean = true):void 
		{
			this.shippingTypeId = value.shippingTypeId;
			this.isApplicable = value.isApplicable;
			this.cost = value.cost;
			
			if (cascade && value.shippigType) 
			{
				var st:ShippingTypeUI = new ShippingTypeUI();
				st.populateFromShippingType(value.shippigType, cascade);
				this.shippigType = st;
			}
		}
		
		public function toShippingOption():ShippingOption 
		{
			var result:ShippingOption = new ShippingOption();
			
			result.shippingTypeId = this.shippingTypeId;
			result.isApplicable = this.isApplicable;
			result.cost = this.cost;
			
			if (shippigType) 
			{
				result.shippigType = shippigType.toShippingType();
			}

			return result;
		}
		
	}
}