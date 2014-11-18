package AerSysCo.UI.Models
{
	import AerSysCo.Server.ShippingType;
	
	[Bindable]
	public class ShippingTypeUI
	{
	    public var shippingTypeId:int;
	    public var shippingType:String;
	    public var shippingCode:String;

		public function populateFromShippingType(value:ShippingType, cascade:Boolean = true):void 
		{
			this.shippingTypeId = value.shippingTypeId;
			this.shippingType = value.shippingType;
			this.shippingCode = value.shippingCode;
		}
		
		public function toShippingType():ShippingType 
		{
			var result:ShippingType = new ShippingType();
			
			result.shippingTypeId = this.shippingTypeId;
			result.shippingType = this.shippingType;
			result.shippingCode = this.shippingCode;

			return result;
		}
		
	}
}