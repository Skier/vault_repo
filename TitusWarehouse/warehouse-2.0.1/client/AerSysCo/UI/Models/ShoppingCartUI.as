package AerSysCo.UI.Models
{
	import mx.collections.ArrayCollection;
	import AerSysCo.Server.ShoppingCart;
	import AerSysCo.Server.ShoppingCartShipment;
	import AerSysCo.Service.WarehouseStorage;
	import AerSysCo.Server.Context;
	import AerSysCo.Server.ShoppingCartDetailCollection;
	import mx.rpc.Responder;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.events.FaultEvent;
	import AerSysCo.Server.ShoppingCartDetail;
	import mx.controls.Alert;
	import AerSysCo.Server.Category;
	import AerSysCo.Server.ShippingAddress;
	import AerSysCo.UI.MainController;
	import mx.collections.ListCollectionView;
	import AerSysCo.Server.ShoppingCartDetailListResult;
	import AerSysCo.Server.RequestResult;
	import AerSysCo.Server.ShoppingCartDetailResult;
	import AerSysCo.Server.VersionResult;
	import flash.events.EventDispatcher;
    import com.affilia.components.NumberUtilities;	
	
	
	[Event(name="versionConflict", type="flash.events.Event")]

	[Bindable]
	public class ShoppingCartUI extends EventDispatcher
	{
	    public var shoppingCartId:int;
	    public var customerId:int;
	    public var orderDate:Date;
	    public var repAccountNo:String;
	    public var shippingAddressId:int;
	    public var brandId:int;
	    public var ipAddress:String;
	    public var isActive:Boolean;
	    public var total:Number;
	    public var shippingTotalAllWarehouses:Number;
	    public var grandTotal:Number;
	    public var phone:String;
	    public var fax:String;
	    public var email:String;
	    public var salesPerson:String;
	    public var jobsiteContactPh:String;
	    public var markOrder:String;
	    public var deliveryRequest:String;
	    
	    public var totalItems:int;

	    public var version:int;

	    public var shippingAddress:ShippingAddressUI;
	    public var customer:CustomerUI;

	    public var shipments:ArrayCollection;
	    public var summaryItems:ArrayCollection;
	    public var shipmentsFiltered:ListCollectionView;
	    
	    public function ShoppingCartUI() 
	    {
		    shipments = new ArrayCollection();
		    summaryItems = new ArrayCollection();
		    shipmentsFiltered = new ListCollectionView(shipments);
		    shipmentsFiltered.filterFunction = detailsExists;
	    }
	    
		public function populateFromShoppingCart(value:ShoppingCart, cascade:Boolean = true):void 
		{
			this.shoppingCartId = value.shoppingCartId;
			this.customerId = value.customerId;
			this.orderDate = value.orderDate;
			this.repAccountNo = value.repAccountNo;
			this.shippingAddressId = value.shippingAddressId;
			this.brandId = value.brandId;
			this.ipAddress = value.ipAddress;
			this.isActive = value.isActive;
			this.total = value.total;
			this.shippingTotalAllWarehouses = value.shippingTotalAllWarehouses;
			this.grandTotal = value.grandTotal;
			this.phone = value.phone;
			this.fax = value.fax;
			this.email = value.email;
			this.salesPerson = value.salesPerson;
			this.jobsiteContactPh = value.jobsiteContactPh;
			this.markOrder = value.markOrder;
			this.deliveryRequest = value.deliveryRequest;
			this.version = value.version;
			
			if (cascade && value.shippingAddress) 
			{
				var sa:ShippingAddressUI = new ShippingAddressUI();
				sa.populateFromShippingAddress(value.shippingAddress, cascade);
				this.shippingAddress = sa;
			}
			
			if (cascade && value.customer) 
			{
				var c:CustomerUI = new CustomerUI();
				c.populateFromCustomer(value.customer, cascade);
				this.customer = c;
			}
			
			if (cascade && value.shipments) 
			{
				this.shipments.removeAll();
				for each (var s:ShoppingCartShipment in value.shipments.toArray()) 
				{
					var sUI:ShoppingCartShipmentUI = new ShoppingCartShipmentUI();
					sUI.populateFromShoppingcartShipment(s, cascade);
					this.shipments.addItem(sUI);
				}
			}
			
			refreshSummaryItems();
			shipmentsFiltered.refresh();
		}
		
		public function toShoppingCart():ShoppingCart 
		{
			var result:ShoppingCart = new ShoppingCart();
			
			result.shoppingCartId = this.shoppingCartId;
			result.customerId = this.customerId;
			result.orderDate = this.orderDate;
			result.repAccountNo = this.repAccountNo;
			result.shippingAddressId = this.shippingAddressId;
			result.brandId = this.brandId;
			result.ipAddress = this.ipAddress;
			result.isActive = this.isActive;
			result.total = this.total;
			result.shippingTotalAllWarehouses = this.shippingTotalAllWarehouses;
			result.grandTotal = this.grandTotal;
			result.phone = this.phone;
			result.fax = this.fax;
			result.email = this.email;
			result.salesPerson = this.salesPerson;
			result.jobsiteContactPh = this.jobsiteContactPh;
			result.markOrder = this.markOrder;
			result.deliveryRequest = this.deliveryRequest;
	    	result.version = this.version;

	    	result.dateCreated = new Date();
	    	result.lastUpdateDate = new Date();
	    	result.orderDate = new Date();
 			
			if (this.shippingAddress) 
			{
				result.shippingAddress = this.shippingAddress.toShippingAddress();
			}
			
			if (this.customer) 
			{
				result.customer = this.customer.toCustomer();
			}

			if (this.shipments) 
			{
				result.shipments.removeAll();
				for each (var s:ShoppingCartShipmentUI in this.shipments) 
				{
					result.shipments.addItem(s.toShoppingCartShipment());
				}
			}
			
			// workaround
			result.shipments = null;

			return result;
		}
		
		public function dispatchVersionConflict():void 
		{
			dispatchEvent(new Event("versionConflict"));
		}
		
		public function addItems(items:ArrayCollection):void 
		{
			if (!items)
				return;
			
			var context:Context = MainController.getInstance().model.context;
			var collection:ShoppingCartDetailCollection = new ShoppingCartDetailCollection();

			for (var i:int = 0; i < items.length; i++) 
			{
				var sd:ShoppingCartDetailUI = ShoppingCartDetailUI(items[i]);
				var shipment:ShoppingCartShipmentUI = getShipmentByWarehouseId(sd.inventory.warehouseId);

				var existingDetail:ShoppingCartDetailUI = shipment.getShoppingCartDetail(sd.modelItemId);
				if (existingDetail) 
				{
					sd.qtyNeeded += existingDetail.qtyNeeded;
					sd.qtyOrdered += existingDetail.qtyOrdered;
					sd.shoppingCartDetailId = existingDetail.shoppingCartDetailId;
				} else 
				{
					sd.shoppingCartDetailId = 0;
				}

				sd.shoppingCartId = this.shoppingCartId;
				sd.shoppingCartShipmentId = shipment.shoppingCartShipmentId;

				collection.addItem(sd.toShoppingCartDetail());
			}
			
			WarehouseStorage.getInstance().saveShoppingCartDetails(context, collection, this.version,
				new Responder(
					function (event:ResultEvent):void 
					{
						var scdResult:ShoppingCartDetailListResult = event.result as ShoppingCartDetailListResult;
						var scdc:ShoppingCartDetailCollection;
						if (scdResult.result.status == RequestResultUI.SUCCESS) 
						{
							scdc = scdResult.details;
						} else 
						{
							if (scdResult.result.status == RequestResultUI.ERROR) 
							{
								Alert.show(scdResult.result.message);
							} else if (scdResult.result.status == RequestResultUI.VERSION) 
							{
								dispatchVersionConflict();
							} else 
							{
								Alert.show("Server Internal Error. Please contact Site Administrator.");
							}

							return;
						}
						
						for each (var scd:ShoppingCartDetail in scdc.toArray()) 
						{
							var scdUI:ShoppingCartDetailUI = new ShoppingCartDetailUI();
							scdUI.populateFromShoppingCartDetail(scd, true);
							addShoppingCartDetail(scdUI);
						}
						
						version = scdResult.version;
			
						refreshSummaryItems();
						shipmentsFiltered.refresh();
					}, 
					function (event:FaultEvent):void 
					{
						Alert.show("Error adding products to Shopping cart: " + event.fault.message);
					}
				)
			);
		}
		
		public function removeSummaryItem(summaryItem:ShoppingCartSummaryUI):void 
		{
			var items:ArrayCollection = new ArrayCollection();
			
			for each (var s:ShoppingCartShipmentUI in shipments) 
			{
				for each (var d:ShoppingCartDetailUI in s.details) 
				{
					if (d.modelItemId == summaryItem.modelItem.modelItemId) 
					{
						items.addItem(d);
					}
				}
			}
			
			removeItems(items);
		}
		
		public function removeItems(items:ArrayCollection):void 
		{
			if (!items)
				return;
			
			var context:Context = MainController.getInstance().model.context;
			var collection:ShoppingCartDetailCollection = new ShoppingCartDetailCollection();

			for each (var sd:ShoppingCartDetailUI in items) 
			{
				collection.addItem(sd.toShoppingCartDetail());
			}
			
			WarehouseStorage.getInstance().removeShoppingCartDetails(context, collection, this.version,
				new Responder(
					function (event:ResultEvent):void 
					{
						var vr:VersionResult = event.result as VersionResult;
						if (vr.result.status != RequestResultUI.SUCCESS) 
						{
							if (vr.result.status == RequestResultUI.ERROR) 
							{
								Alert.show(vr.result.message);
							} else if (vr.result.status == RequestResultUI.VERSION) 
							{
								dispatchVersionConflict();
							} else 
							{
								Alert.show("Server Internal Error. Please contact Site Administrator.");
							}

							return;
						}
						
						for each (var scd:ShoppingCartDetailUI in items) 
						{
							removeShoppingCartDetail(scd);
						}
			
						refreshSummaryItems();
						
						for each (var s:ShoppingCartShipmentUI in shipmentsFiltered) 
						{
							if (s.details.length == 0) 
							{
								shipmentsFiltered.refresh();
								break;
							}
						}
						
						version = vr.version;
					}, 
					function (event:FaultEvent):void 
					{
						Alert.show("Error removing products from Shopping cart: " + event.fault.faultString);
					}
				)
			);
		}
		
		public function removeAll():void 
		{
			var context:Context = MainController.getInstance().model.context;

			WarehouseStorage.getInstance().removeAllShoppingCartDetails(context, shoppingCartId, this.version,
				new Responder(
					function (event:ResultEvent):void 
					{
						var vr:VersionResult = event.result as VersionResult;
						if (vr.result.status != RequestResultUI.SUCCESS) 
						{
							if (vr.result.status == RequestResultUI.ERROR) 
							{
								Alert.show(vr.result.message);
							} else if (vr.result.status == RequestResultUI.VERSION) 
							{
								dispatchVersionConflict();
							} else 
							{
								Alert.show("Server Internal Error. Please contact Site Administrator.");
							}

							return;
						}
						
						version = vr.version;
						
						removeAllShoppingCartDetail();
						refreshSummaryItems();
						shipmentsFiltered.refresh();
					}, 
					function (event:FaultEvent):void 
					{
						Alert.show("Error removing products from Shopping cart: " + event.fault.faultString);
					}
				)
			);

			refreshSummaryItems();
		}
		
		public function updateItem(item:ShoppingCartDetailUI):void 
		{
			if (!item)
				return;
			
			var context:Context = MainController.getInstance().model.context;

			WarehouseStorage.getInstance().saveShoppingCartDetail(context, item.toShoppingCartDetail(), this.version,
				new Responder(
					function (event:ResultEvent):void 
					{
						var scdResult:ShoppingCartDetailResult = event.result as ShoppingCartDetailResult;
						if (scdResult.result.status == RequestResultUI.SUCCESS) 
						{
							version = scdResult.version;
							refreshSummaryItems();
						} else 
						{
							item.getMemento();

							if (scdResult.result.status == RequestResultUI.ERROR) 
							{
								Alert.show(scdResult.result.message);
							} else if (scdResult.result.status == RequestResultUI.VERSION) 
							{
								dispatchVersionConflict();
							} else 
							{
								Alert.show("Server Internal Error. Please contact Site Administrator.");
							}
						}
					}, 
					function (event:FaultEvent):void 
					{
						item.getMemento();
						Alert.show("Error updating quantity: " + event.fault.faultString);
					}
				)
			);
		}
		
		public function getQty(modelItemId:int, warehouseId:int):int 
		{
			var result:int = 0;
			
			for each (var s:ShoppingCartShipmentUI in shipments) 
			{
				for each (var d:ShoppingCartDetailUI in s.details) 
				{
					if (d.modelItemId == modelItemId && s.warehouseId == warehouseId) 
					{
						result += d.qtyNeeded;
					}
				}
			}
			
			return result;
		}
		
		private function addShoppingCartDetail(item:ShoppingCartDetailUI):void 
		{
			var shipment:ShoppingCartShipmentUI = getShipment(item.shoppingCartShipmentId);
			
			if (shipment == null) 
				throw new Error("Shopping Cart must have Shipments for each warehouse !");
			
			shipment.addShoppingCartDetail(item);
		}
		
		private function removeShoppingCartDetail(item:ShoppingCartDetailUI):void 
		{
			var shipment:ShoppingCartShipmentUI = getShipment(item.shoppingCartShipmentId);
			var idx:int;
			
			if (shipment == null) 
				throw new Error("ShoppingCartUI:removeShoppingCartDetail: Shipment is null!");
			
			idx = shipment.details.getItemIndex(item);
			if (idx > -1) 
			{
				shipment.details.removeItemAt(idx);
			}
		}
		
		private function removeAllShoppingCartDetail():void 
		{
			for each (var shipment:ShoppingCartShipmentUI in shipments) 
			{
				shipment.details.removeAll();
			}
			
			refreshSummaryItems();
		}
		
		public function updateTotals():void 
		{
			total = 0;
			totalItems = 0;
			shippingTotalAllWarehouses = 0;
			grandTotal = 0;
			
			for each (var shipment:ShoppingCartShipmentUI in shipments) 
			{
				shipment.total = 0;
				shipment.totalItems = 0;
				shipment.grandTotal = 0;
				
				for each (var detail:ShoppingCartDetailUI in shipment.details) 
				{
					detail.qtyOrdered = detail.qtyNeeded;
					
					shipment.totalItems += detail.qtyNeeded;
					shipment.total += (detail.qtyNeeded * NumberUtilities.round(detail.price, .001));
				}
                shipment.total = NumberUtilities.round(shipment.total, .01);
				
				shipment.grandTotal = shipment.total + shipment.shippingTotal;

				total += shipment.total;
				totalItems += shipment.totalItems;
				shippingTotalAllWarehouses += shipment.shippingTotal;
				grandTotal = total + shippingTotalAllWarehouses;
			}
		}
		
		private function refreshSummaryItems():void 
		{
			summaryItems.removeAll();
			
			for each (var s:ShoppingCartShipmentUI in this.shipments) 
			{
				for each (var d:ShoppingCartDetailUI in s.details) 
				{
					var si:ShoppingCartSummaryUI = getSummaryItem(d.modelItemId);
					if (si != null) 
					{
						si.qtyOrdered += d.qtyOrdered;
						si.qtyNeeded += d.qtyNeeded;
					} else 
					{
						si = new ShoppingCartSummaryUI();
						si.modelName = d.modelName;
						si.qtyNeeded = d.qtyNeeded;
						si.qtyOrdered = d.qtyOrdered;
						si.modelItem = d.modelItem;

						summaryItems.addItem(si);
					}
				}
			}

			updateTotals();
		}
		
		private function getSummaryItem(modelItemId:int):ShoppingCartSummaryUI 
		{
			for each (var si:ShoppingCartSummaryUI in this.summaryItems) 
			{
				if (si.modelItem.modelItemId == modelItemId) 
				{
					return si;
				}
			}
			
			return null;
		}
		
		public function getShipment(shipmentId:int):ShoppingCartShipmentUI 
		{
			for each (var s:ShoppingCartShipmentUI in shipments) 
			{
				if (s.shoppingCartShipmentId == shipmentId) 
				{
					return s;
				}
			}
			
			return null;
		}
		
		private function getShipmentByWarehouseId(warehouseId:int):ShoppingCartShipmentUI 
		{
			for each (var s:ShoppingCartShipmentUI in shipments) 
			{
				if (s.warehouseId == warehouseId) 
				{
					return s;
				}
			}
			
			return null;
		}
		
		private function getShoppingCartDetail(modelItemId:int, shipmentId:int):ShoppingCartDetailUI 
		{
			var s:ShoppingCartShipmentUI = getShipment(shipmentId);
			
			if (s != null) 
			{
				for each (var d:ShoppingCartDetailUI in s.details) 
				{
					if (d.modelItemId == modelItemId) 
					{
						return d;
					}
				}
			}
			
			return null;
		}
		
		private function detailsExists(s:ShoppingCartShipmentUI):Boolean 
		{
			if (s.details.length > 0) 
			{
				return true;
			} else 
			{
				return false;
			}
		}
		
	}
}