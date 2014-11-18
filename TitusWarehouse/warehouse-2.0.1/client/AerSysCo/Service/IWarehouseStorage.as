package AerSysCo.Service
{
    import mx.rpc.Responder;
    
    import AerSysCo.Server.Context;
    import AerSysCo.Server.ShoppingCartDetail;
    import AerSysCo.Server.ShoppingCartDetailCollection;
    import AerSysCo.Server.ShippingAddress;
    import AerSysCo.Server.ShoppingCart;
    import AerSysCo.Server.ShoppingCartShipment;
    import AerSysCo.Server.ShoppingCartShipmentCollection;
    import AerSysCo.Server.ASCUser;
    import AerSysCo.Server.Customer;
    import AerSysCo.Server.OrderFilter;
    
    public interface IWarehouseStorage
    {
        function getSalesRep(context:Context, responder:Responder):void;

        function getCatalogPackage(context:Context, responder:Responder):void;

        function getItemsByCategory(context:Context, customerId :int, categoryId:int, responder:Responder):void;

        function getItemsByModel(context:Context, customerId :int, modelId:int, responder:Responder):void;
        
        function searchItems(context:Context, customerId :int, search :String, responder:Responder):void;

        function getShoppingCart(context:Context, customerId:int, responder:Responder):void;

        function saveShoppingCartDetail(context:Context, shoppingCartDetail:ShoppingCartDetail, version:int, responder:Responder):void;
        
        function saveShoppingCartDetails(context:Context, shoppingCartDetails:ShoppingCartDetailCollection, version:int, responder:Responder):void;

        function removeShoppingCartDetail(context:Context, shoppingCartDetailId:int, version:int, responder:Responder):void;

        function removeShoppingCartDetails(context:Context, shoppingCartDetails:ShoppingCartDetailCollection, version:int, responder:Responder):void;

        function removeAllShoppingCartDetails(context:Context, shoppingCartId:int, version:int, responder:Responder):void;
        
        function saveShippingAddress(context:Context, shippingAddress:ShippingAddress, responder:Responder):void;
        
        function getAddressesByCustomerId(context:Context, customerId:int, responder:Responder):void;
        
        function getShippingOptions(context:Context, shoppingCartId:int, responder:Responder):void;
        
        function saveShoppingCart(context:Context, shoppingCart:ShoppingCart, responder:Responder):void;
        
        function saveShoppingCartShipment(context:Context, shoppingCartShipment:ShoppingCartShipment, version:int,  responder:Responder):void;

        function saveShoppingCartShipments(context:Context, shoppingCartShipments:ShoppingCartShipmentCollection, version:int, responder:Responder):void;
        
        function checkInShoppingCart(context:Context, cartId:int, version:int, responder:Responder):void;
        
        function login(username:String, password:String, responder:Responder):void;

        function getCustomer(customerId:int, responder:Responder):void;
        
        function getCustomers(user:ASCUser, responder:Responder):void;
        
        function saveCustomer(user:ASCUser, customer:Customer, responder:Responder):void;

        function getOrders(context:Context, filter:OrderFilter, responder:Responder):void;
        
        function getOrder(context:Context, orderId:int, responder:Responder):void;
        
		function checkUniquePONumber(context:Context, customerId:int, poNumber:String, responder:Responder):void;
		
		function checkZipCode(contex:Context, zip:String, country:String, responder:Responder):void;

		function getOrderPDF(context:Context, orderId:int, responder:Responder):void;

    }
}
