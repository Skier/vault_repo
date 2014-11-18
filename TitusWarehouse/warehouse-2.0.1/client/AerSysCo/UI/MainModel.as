package AerSysCo.UI
{
	import AerSysCo.UI.Models.UserUI;
	import AerSysCo.UI.Models.CustomerUI;
	import mx.collections.ArrayCollection;
	import AerSysCo.Server.Context;
	import AerSysCo.UI.Models.ShoppingCartUI;
	import AerSysCo.UI.Models.CategoryUI;
	
	[Bindable]
	public class MainModel
	{
	    public static const LOCAL_SHARED_OBJECT_NAME:String = "WarehouseLocalStorage";

		public var context:Context;
		
		public var user:UserUI;

		public var currentCustomer:CustomerUI;
		public var shoppingCart:ShoppingCartUI;
	}
}