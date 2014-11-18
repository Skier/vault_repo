package AerSysCo.Service
{
    import mx.rpc.Fault;
    import mx.rpc.Responder;
    import mx.rpc.AsyncToken;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;

    import flexTense.core.BaseFaultEvent;
    import AerSysCo.Server.WarehouseService;
    import AerSysCo.Server.Context;
    import AerSysCo.Server.GetSalesRepResultEvent;
    import AerSysCo.Server.GetCatalogPackageResultEvent;
    import AerSysCo.Server.GetItemsByCategoryResultEvent;
    import AerSysCo.Server.GetItemsByModelResultEvent;
    import AerSysCo.Server.ShoppingCartDetail;
    import AerSysCo.Server.ShoppingCartDetailCollection;
    import AerSysCo.Server.ShippingAddress;
    import AerSysCo.Server.ShoppingCart;
    import AerSysCo.Server.ShoppingCartShipment;
    import AerSysCo.Server.ShoppingCartShipmentCollection;
    import AerSysCo.Server.ASCUser;
    import AerSysCo.Server.Customer;
    import AerSysCo.Server.OrderFilter;

    public class WarehouseStorage implements IWarehouseStorage
    {
        private static var _instance:WarehouseStorage = null;
        
        public static function getInstance():IWarehouseStorage {
            if ( null == _instance ) {
                var ws:WarehouseService = new WarehouseService();
                _instance = new WarehouseStorage(ws);
            }
            return _instance;
        }

        private var _warehouseService:WarehouseService = null;      

        public function WarehouseStorage(service:WarehouseService) {
            if ( null == _instance ) {
                this._warehouseService = service;
            } else {
                throw new Error("Singleton.");
            }
        }
        
        public function getSalesRep(context:Context, responder:Responder):void {
            var ra:ResponderAdapter = new ResponderAdapter(responder, "getSalesRep");
            _warehouseService.getSalesRep.addResponder(ra.onResult);
            _warehouseService.getSalesRep.addFaulter(ra.onFailure);
            _warehouseService.getSalesRep.send(context);
        }

        public function getCatalogPackage(context:Context, responder:Responder):void {
            var ra:ResponderAdapter = new ResponderAdapter(responder, "getCatalogPackage");
            _warehouseService.getCatalogPackage.addResponder(ra.onResult);
            _warehouseService.getCatalogPackage.addFaulter(ra.onFailure);
            _warehouseService.getCatalogPackage.send(context);
        }

        public function getItemsByCategory(context:Context, customerId :int, categoryId:int, responder:Responder):void {
            var ra:ResponderAdapter = new ResponderAdapter(responder, "getItemsByCategory");
            _warehouseService.getItemsByCategory.addResponder(ra.onResult);
            _warehouseService.getItemsByCategory.addFaulter(ra.onFailure);
            _warehouseService.getItemsByCategory.send(context, customerId, categoryId);
        }
        
        public function getItemsByModel(context:Context, customerId :int, modelId:int, responder:Responder):void {
            var ra:ResponderAdapter = new ResponderAdapter(responder, "getItemsByModel");
            _warehouseService.getItemsByModel.addResponder(ra.onResult);
            _warehouseService.getItemsByModel.addFaulter(ra.onFailure);
            _warehouseService.getItemsByModel.send(context, customerId, modelId);
        }

        public function searchItems(context:Context, customerId :int, search :String, responder:Responder):void {
            var ra:ResponderAdapter = new ResponderAdapter(responder, "searchItems");
            _warehouseService.searchItems.addResponder(ra.onResult);
            _warehouseService.searchItems.addFaulter(ra.onFailure);
            _warehouseService.searchItems.send(context, customerId, search);
        }
        
        public function getShoppingCart(context:Context, customerId:int, responder:Responder):void {
            var ra:ResponderAdapter = new ResponderAdapter(responder, "getShoppingCart");
            _warehouseService.getShoppingCart.addResponder(ra.onResult);
            _warehouseService.getShoppingCart.addFaulter(ra.onFailure);
            _warehouseService.getShoppingCart.send(context, customerId);
        }
        
        public function saveShoppingCartDetail(context:Context, shoppingCartDetail:ShoppingCartDetail, version:int, responder:Responder):void {
            var ra:ResponderAdapter = new ResponderAdapter(responder, "saveShoppingCartDetail");
            _warehouseService.saveShoppingCartDetail.addResponder(ra.onResult);
            _warehouseService.saveShoppingCartDetail.addFaulter(ra.onFailure);
            _warehouseService.saveShoppingCartDetail.send(context, shoppingCartDetail, version);
        }
        
        public function saveShoppingCartDetails(context:Context, shoppingCartDetails:ShoppingCartDetailCollection, version:int, responder:Responder):void {
            var ra:ResponderAdapter = new ResponderAdapter(responder, "saveShoppingCartDetails");
            _warehouseService.saveShoppingCartDetails.addResponder(ra.onResult);
            _warehouseService.saveShoppingCartDetails.addFaulter(ra.onFailure);
            _warehouseService.saveShoppingCartDetails.send(context, shoppingCartDetails, version);
        }

        public function removeShoppingCartDetail(context:Context, shoppingCartDetailId:int, version:int, responder:Responder):void {
            var ra:ResponderAdapter = new ResponderAdapter(responder, "removeShoppingCartDetail");
            _warehouseService.removeShoppingCartDetail.addResponder(ra.onResult);
            _warehouseService.removeShoppingCartDetail.addFaulter(ra.onFailure);
            _warehouseService.removeShoppingCartDetail.send(context, shoppingCartDetailId, version);
        }

        public function removeShoppingCartDetails(context:Context, shoppingCartDetails:ShoppingCartDetailCollection, version:int, responder:Responder):void {
            var ra:ResponderAdapter = new ResponderAdapter(responder, "removeShoppingCartDetails");
            _warehouseService.removeShoppingCartDetails.addResponder(ra.onResult);
            _warehouseService.removeShoppingCartDetails.addFaulter(ra.onFailure);
            _warehouseService.removeShoppingCartDetails.send(context, shoppingCartDetails, version);
        }

        public function removeAllShoppingCartDetails(context:Context, shoppingCartId:int, version:int, responder:Responder):void {
            var ra:ResponderAdapter = new ResponderAdapter(responder, "removeAllShoppingCartDetails");
            _warehouseService.removeAllShoppingCartDetails.addResponder(ra.onResult);
            _warehouseService.removeAllShoppingCartDetails.addFaulter(ra.onFailure);
            _warehouseService.removeAllShoppingCartDetails.send(context, shoppingCartId, version);
        }

        public function getAddressesByCustomerId(context:Context, customerId:int, responder:Responder):void {
            var ra:ResponderAdapter = new ResponderAdapter(responder, "getAddressesByCustomerId");
            _warehouseService.getAddressesByCustomerId.addResponder(ra.onResult);
            _warehouseService.getAddressesByCustomerId.addFaulter(ra.onFailure);
            _warehouseService.getAddressesByCustomerId.send(context, customerId);
        }

        public function saveShippingAddress(context:Context, address:ShippingAddress, responder:Responder):void {
            var ra:ResponderAdapter = new ResponderAdapter(responder, "saveShippingAddress");
            _warehouseService.saveShippingAddress.addResponder(ra.onResult);
            _warehouseService.saveShippingAddress.addFaulter(ra.onFailure);
            _warehouseService.saveShippingAddress.send(context, address);
        }

        public function getShippingOptions(context:Context, shoppingCartId:int, responder:Responder):void {
            var ra:ResponderAdapter = new ResponderAdapter(responder, "getShippingOptions");
            _warehouseService.getShippingOptions.addResponder(ra.onResult);
            _warehouseService.getShippingOptions.addFaulter(ra.onFailure);
            _warehouseService.getShippingOptions.send(context, shoppingCartId);
        }
        
        public function saveShoppingCart(context:Context, shoppingCart:ShoppingCart, responder:Responder):void {
            var ra:ResponderAdapter = new ResponderAdapter(responder, "saveShoppingCart");
            _warehouseService.saveShoppingCart.addResponder(ra.onResult);
            _warehouseService.saveShoppingCart.addFaulter(ra.onFailure);
            _warehouseService.saveShoppingCart.send(context, shoppingCart);
        }
        
        public function saveShoppingCartShipment(context:Context, shoppingCartShipment:ShoppingCartShipment, version:int, responder:Responder):void {
            var ra:ResponderAdapter = new ResponderAdapter(responder, "saveShoppingCartShipment");
            _warehouseService.saveShoppingCartShipment.addResponder(ra.onResult);
            _warehouseService.saveShoppingCartShipment.addFaulter(ra.onFailure);
            _warehouseService.saveShoppingCartShipment.send(context, shoppingCartShipment, version);
        }
        
        public function saveShoppingCartShipments(context:Context, shoppingCartShipments:ShoppingCartShipmentCollection, version:int, responder:Responder):void {
            var ra:ResponderAdapter = new ResponderAdapter(responder, "saveShoppingCartShipments");
            _warehouseService.saveShoppingCartShipments.addResponder(ra.onResult);
            _warehouseService.saveShoppingCartShipments.addFaulter(ra.onFailure);
            _warehouseService.saveShoppingCartShipments.send(context, shoppingCartShipments, version);
        }
        public function checkInShoppingCart(context:Context, cartId:int, version:int, responder:Responder):void {
            var ra:ResponderAdapter = new ResponderAdapter(responder, "checkInShoppingCart");
            _warehouseService.checkInShoppingCart.addResponder(ra.onResult);
            _warehouseService.checkInShoppingCart.addFaulter(ra.onFailure);
            _warehouseService.checkInShoppingCart.send(context, cartId, version);
        }

        public function login(username:String, password:String, responder:Responder):void {
            var ra:ResponderAdapter = new ResponderAdapter(responder, "login");
            _warehouseService.login.addResponder(ra.onResult);
            _warehouseService.login.addFaulter(ra.onFailure);
            _warehouseService.login.send(username, password);
        }
        
        public function getCustomer(customerId:int, responder:Responder):void {
        	var ra:ResponderAdapter = new ResponderAdapter(responder, "getCustomer");
            _warehouseService.getCustomer.addResponder(ra.onResult);
            _warehouseService.getCustomer.addFaulter(ra.onFailure);
            _warehouseService.getCustomer.send(customerId);
        }

        public function getCustomers(user:ASCUser, responder:Responder):void {
        	var ra:ResponderAdapter = new ResponderAdapter(responder, "getCustomers");
            _warehouseService.getCustomers.addResponder(ra.onResult);
            _warehouseService.getCustomers.addFaulter(ra.onFailure);
            _warehouseService.getCustomers.send(user);
        }

        public function saveCustomer(user:ASCUser, customer:Customer, responder:Responder):void {
        	var ra:ResponderAdapter = new ResponderAdapter(responder, "saveCustomer");
            _warehouseService.saveCustomer.addResponder(ra.onResult);
            _warehouseService.saveCustomer.addFaulter(ra.onFailure);
            _warehouseService.saveCustomer.send(user, customer);
        }

        public function getOrders(context:Context, filter:OrderFilter, responder:Responder):void {
        	var ra:ResponderAdapter = new ResponderAdapter(responder, "getOrders");
            _warehouseService.getOrders.addResponder(ra.onResult);
            _warehouseService.getOrders.addFaulter(ra.onFailure);
            _warehouseService.getOrders.send(context, filter);
        }

        public function getOrder(context:Context, orderId:int, responder:Responder):void {
        	var ra:ResponderAdapter = new ResponderAdapter(responder, "getOrder");
            _warehouseService.getOrder.addResponder(ra.onResult);
            _warehouseService.getOrder.addFaulter(ra.onFailure);
            _warehouseService.getOrder.send(context, orderId);
        }

        public function checkUniquePONumber(context:Context, customerId:int, poNumber:String, responder:Responder):void {
        	var ra:ResponderAdapter = new ResponderAdapter(responder, "checkUniquePONumber");
            _warehouseService.checkUniquePONumber.addResponder(ra.onResult);
            _warehouseService.checkUniquePONumber.addFaulter(ra.onFailure);
            _warehouseService.checkUniquePONumber.send(context, customerId, poNumber);
        }
        
		public function checkZipCode(context:Context, zip:String, country:String, responder:Responder):void {
            var ra:ResponderAdapter = new ResponderAdapter(responder, "checkZipCode");
            _warehouseService.checkZip.addResponder(ra.onResult);
            _warehouseService.checkZip.addFaulter(ra.onFailure);
            _warehouseService.checkZip.send(context, zip, country);
		}

		public function getOrderPDF(context:Context, orderId:int, responder:Responder):void {
            var ra:ResponderAdapter = new ResponderAdapter(responder, "getOrderPDF");
            _warehouseService.getOrderPDF.addResponder(ra.onResult);
            _warehouseService.getOrderPDF.addFaulter(ra.onFailure);
            _warehouseService.getOrderPDF.send(context, orderId);
		}


    }
}
