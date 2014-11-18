package AerSysCo.UI.Catalog
{
	import mx.rpc.Responder;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.events.FaultEvent;
	import AerSysCo.Server.Context;
	import AerSysCo.UI.MainController;
	import AerSysCo.Server.WarehouseCollection;
	import AerSysCo.Server.CatalogPackage;
	import AerSysCo.Events.CategoryEvent;
	import AerSysCo.UI.Models.CategoryUI;
	import AerSysCo.UI.Models.ModelUI;
	import AerSysCo.Service.WarehouseStorage;
	import mx.controls.Alert;
	import AerSysCo.Server.Warehouse;
	import AerSysCo.UI.Models.WarehouseUI;
	import AerSysCo.Server.CatalogItemCollection;
	import AerSysCo.Server.CatalogItem;
	import AerSysCo.UI.Models.CatalogItemUI;
	import AerSysCo.UI.Models.CustomerUI;
	import mx.collections.ArrayCollection;
	import AerSysCo.UI.Models.ShoppingCartUI;
	import AerSysCo.UI.Models.ShoppingCartDetailUI;
	import AerSysCo.UI.Models.InventoryUI;
	import mx.core.Application;
	import flash.display.DisplayObject;
	import flash.events.Event;
	import AerSysCo.Server.CatalogItemResult;
	import AerSysCo.Server.CatalogPackageResult;
	import AerSysCo.UI.Models.RequestResultUI;
	
	[Bindable]
	public class CatalogController
	{
		public var model:CatalogModel;
		public var view:CatalogView;

// singleton section begin
		private static var instance:CatalogController;
		
		public function CatalogController()
		{
			if (instance != null) 
			{
				throw new Error("singleton!");
			}
		}
		
		public static function getInstance():CatalogController 
		{
			if (instance == null) 
			{
				instance = new CatalogController();
			}
			
			return instance;
		}
// singleton section end

		public function init(currentCustomer:CustomerUI, view:CatalogView):void 
		{
			this.model = new CatalogModel();
			this.view = view;
			
			this.model.currentCustomer = currentCustomer;
			this.model.searchString = null;
			
			loadCatalog();
		}
		
		public function getProductsByCategory(item:Object):void 
		{
			if (item is CategoryUI) 
			{
				populateCatalogByCategory((item as CategoryUI));
			} else if (item is ModelUI)
			{
				populateCatalogByModel((item as ModelUI));
			} else 
			{
				Alert.show("Unknown object selected");
			}
		}
		
		public function search(value:String):void 
		{
			var context:Context = MainController.getInstance().model.context;
			if (context == null)
			{
	        	trace("MainController:search - context is null");
				return;
			}
			
			model.productList.removeAll();
			
			model.isBusy = true;
			
            WarehouseStorage.getInstance().searchItems(context, model.currentCustomer.customerId, value,  
            	new Responder(
            		function (event:ResultEvent):void 
            		{
						model.isBusy = false;

			        	var cicResult:CatalogItemResult = event.result as CatalogItemResult;
			        	var cic:CatalogItemCollection;
			        	if (cicResult.result.status == RequestResultUI.SUCCESS) 
			        	{
			        		cic = cicResult.items;
			        	} else if (cicResult.result.status == RequestResultUI.ERROR) 
			        	{
			        		Alert.show(cicResult.result.message);
			        		return;
			        	} else 
			        	{
			        		Alert.show("Server Error. Please contact Site Administrator.");
			        		return;
			        	}

			        	refreshProductsList(cic.toArray());
            		},
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
			            Alert.show(event.fault.message);
					}
				)
			);
		}

		public function getWarehouse(warehouseId:int):WarehouseUI 
		{
			for each (var w:WarehouseUI in model.warehouseList) 
			{
				if (warehouseId == w.warehouseId) 
				{
					return w;
				}
			}
			
			return null;
		}
		
		public function addCatalogItemsToShoppingCart(items:ArrayCollection):void 
		{
			var sc:ShoppingCartUI = model.currentCustomer.shoppingCart;
			
			var scItems:ArrayCollection = new ArrayCollection();
//			var zeroItems:ArrayCollection = new ArrayCollection();
			var addedTotal:Number = 0;

			for each (var ci:CatalogItemUI in items) 
			{
				var sci:ShoppingCartDetailUI = new ShoppingCartDetailUI();
				
				sci.sku = ci.sku;
				sci.modelItemId = ci.modelItem.modelItemId;
				sci.modelItem = ci.modelItem;
//				sci.price = ci.price;
				sci.qtyNeeded = ci.qtyNeeded;
				
				addedTotal += (ci.price * ci.qtyNeeded);
				
				for each (var inv:InventoryUI in ci.inventories) 
				{
					if (inv.warehouseId == ci.currentWarehouse.warehouseId) 
					{
						sci.inventory = inv;
						break;
					}
				}
				
				if (sci.inventory) 
				{
					ci.isAvailable = true;
					scItems.addItem(sci);
				} else
				{
					ci.isAvailable = false;
				}
			}
			
			if ((model.currentCustomer.dayBalance - model.currentCustomer.shoppingCart.total - addedTotal) < 0) 
			{
				Alert.show("You have exceeded your daily allowed limit. Please remove some items from your shopping cart.");
				return;
			}
	
			var listPopUp:ProductsListView = ProductsListView.open(Application.application as DisplayObject, items);
			listPopUp.messageText = "The following products have been added to your Shopping Cart:"

			sc.addItems(scItems);
		}

		private function loadCatalog():void 
		{
			var context:Context = MainController.getInstance().model.context;
			if (context == null)
			{
	        	trace("MainController:loadCatalog - context is null");
				return;
			}
			
			model.isBusy = true;
			
            WarehouseStorage.getInstance().getCatalogPackage(context, 
            	new Responder(
            		function (event:ResultEvent):void 
            		{
						model.isBusy = false;

			        	var catalogPackageResult:CatalogPackageResult = event.result as CatalogPackageResult;
			        	var catalogPackage:CatalogPackage;
			        	if (catalogPackageResult.result.status == RequestResultUI.SUCCESS) 
			        	{
			        		catalogPackage = catalogPackageResult.pack;
			        	} else if (catalogPackageResult.result.status == RequestResultUI.ERROR) 
			        	{
			        		Alert.show(catalogPackageResult.result.message);
							view.dispatchEvent(new Event("categoriesLoadFault"));
							return;
			        	} else 
			        	{
			        		Alert.show("Server Error. Please contact Site Administrator.");
			        		return;
			        	}
			
			        	populateCategoryList(catalogPackage);
			        	populateWarehouseList(catalogPackage.warehouseList);
                        model.defaultCategory = catalogPackage.defaultCategory;
			        	view.setHomeView();
            		},
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
						view.dispatchEvent(new Event("categoriesLoadFault"));
					}
				)
			);
		}
		
        private function populateWarehouseList(wc:WarehouseCollection):void 
        {
			model.warehouseList.removeAll();
			
        	for each (var w:Warehouse in wc.toArray()) {
        		var wUI:WarehouseUI = new WarehouseUI();
        		wUI.populateFromWarehouse(w, true);
        		model.warehouseList.addItem(wUI);
        		
        		if (MainController.getInstance().model.currentCustomer.defaultWarehouseId == wUI.warehouseId) 
        		{
        			model.currentWarehouse = wUI
        		}
        	}
        }
        
        private function populateCategoryList(cp:CatalogPackage):void 
        {
        	model.rootCategory = CategoryUI.createFromCatalogPackage(cp);
        }

		private function populateCatalogByCategory(category:CategoryUI):void 
		{
			
			model.isBusy = true;
			
            WarehouseStorage.getInstance().getItemsByCategory(null, 
                MainController.getInstance().model.currentCustomer.customerId,
        		category.categoryId, 
        		new Responder(
        			function(event:ResultEvent):void 
        			{
						model.isBusy = false;

			        	var cicResult:CatalogItemResult = event.result as CatalogItemResult;
			        	var cic:CatalogItemCollection;
			        	if (cicResult.result.status == RequestResultUI.SUCCESS) 
			        	{
			        		cic = cicResult.items;
			        	} else if (cicResult.result.status == RequestResultUI.ERROR) 
			        	{
			        		Alert.show(cicResult.result.message);
			        		return;
			        	} else 
			        	{
			        		Alert.show("Server Error. Please contact Site Administrator.");
			        		return;
			        	}
			        	
			        	refreshProductsList(cic.toArray());
        			},
        			function(event:FaultEvent):void 
        			{
						model.isBusy = false;
			            Alert.show(event.fault.message);
        			}
        		)
            );
		}	

		private function populateCatalogByModel(catalogModel:ModelUI):void 
		{
			model.isBusy = true;
			
            WarehouseStorage.getInstance().getItemsByModel(null,
        		MainController.getInstance().model.currentCustomer.customerId,
        		catalogModel.modelId, 
        		new Responder(
        			function(event:ResultEvent):void 
        			{
						model.isBusy = false;

			        	var cicResult:CatalogItemResult = event.result as CatalogItemResult;
			        	var cic:CatalogItemCollection;
			        	if (cicResult.result.status == RequestResultUI.SUCCESS) 
			        	{
			        		cic = cicResult.items;
			        	} else if (cicResult.result.status == RequestResultUI.ERROR)
			        	{
			        		Alert.show(cicResult.result.message);
			        		return;
			        	} else 
			        	{
			        		Alert.show("Server Error. Please contact Site Administrator.");
			        		return;
			        	}
			        	
			        	refreshProductsList(cic.toArray());
        			},
        			function(event:FaultEvent):void 
        			{
						model.isBusy = false;
			            Alert.show(event.fault.message);
        			}
        		)
        	);
		}	
        
        private function refreshProductsList(collection:Array):void 
        {
        	model.productList.removeAll();
        	
        	for each (var item:CatalogItem in collection)
        	{
        		var newItem:CatalogItemUI = new CatalogItemUI();
        			newItem.populateFromCatalogItem(item, true);
        			newItem.updateQtyAvailable(model.currentWarehouse, model.currentCustomer);
        		
        		model.productList.addItem(newItem);
        	}
        }
        

	}
}