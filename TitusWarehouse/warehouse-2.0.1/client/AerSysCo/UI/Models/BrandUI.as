package AerSysCo.UI.Models
{
	import AerSysCo.Server.Brand;
	
	[Bindable]
	public class BrandUI
	{
		public var brandId:int;
		public var code:String;
		public var brandName:String;
		
		public function populateFromBrand(value:Brand):void 
		{
			this.brandId = value.brandId;
			this.code = value.code;
			this.brandName = value.brandName;
		}
		
		public function toBrand():Brand 
		{
			var result:Brand = new Brand();
			result.brandId = this.brandId;
			result.code = this.code;
			result.brandName = this.brandName;
			
			return result;
		}
	}
}