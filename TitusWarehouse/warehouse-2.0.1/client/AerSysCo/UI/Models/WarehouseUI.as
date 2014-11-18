package AerSysCo.UI.Models
{
	import AerSysCo.Server.Warehouse;
	
	[Bindable]
	public class WarehouseUI
	{
	    public var warehouseId:int;
	    public var warehouseName:String;
	    public var warehouseCode:String;
	    public var name:String;
	    public var address1:String;
	    public var address2:String;
	    public var city:String;
	    public var state:String;
	    public var zip:String;
	    public var country:String;

		public function populateFromWarehouse(value:Warehouse, cascade:Boolean = false):void 
		{
		    this.warehouseId = value.warehouseId;
		    this.warehouseName = value.warehouseName;
		    this.warehouseCode = value.warehouseCode;
		    this.name = value.name;
		    this.address1 = value.address1;
		    this.address2 = value.address2;
		    this.city = value.city;
		    this.state = value.state;
		    this.zip = value.zip;
		    this.country = value.country;
		}
		
		public function toWarehouse():Warehouse 
		{
			var result:Warehouse = new Warehouse();
			
		    result.warehouseId = this.warehouseId;
		    result.warehouseName = this.warehouseName;
		    result.warehouseCode = this.warehouseCode;
		    result.name = this.name;
		    result.address1 = this.address1;
		    result.address2 = this.address2;
		    result.city = this.city;
		    result.state = this.state;
		    result.zip = this.zip;
		    result.country = this.country;

			return result;
		}
	}
}