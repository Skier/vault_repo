package com.titus.catalog.view.cart
{
	import com.titus.catalog.model.cart.Cart;
	
	import flash.events.Event;
	import flash.events.EventDispatcher;
	
	import mx.events.DynamicEvent;
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.RemoteObject;
	
	public class CartService extends EventDispatcher
	{
		private static const PACKAGE_CREATE_RESULT:String = "cartPackageCreateResult";
		private static const PACKAGE_CREATE_FAULT:String = "cartPackageCreateFault";

		private static var instance:CartService;
		
		private var service:RemoteObject;
		
		public static function getInstance():CartService
		{
			if (!instance)
				instance = new CartService();
	
			return instance;
		}
		
		public function CartService()
		{
			if (instance)
				throw new Error("CartService is singleton!");
		}
		
		private function getService():RemoteObject 
		{
			if (service == null) 
			{
	        	service = new RemoteObject("GenericDestination");
	        	service.source = "Titus.ECatalog.Service.CatalogService";
			}

			return service;
		}

		public function getPackage(cart:Cart):AsyncToken 
		{
	        var asyncToken:AsyncToken = getService().PrepareCartPackage(cart.items.toArray());
	        asyncToken.addResponder(
	        	new Responder(
	        		function (event:ResultEvent):void 
	        		{
	        			var evt:DynamicEvent = new DynamicEvent(PACKAGE_CREATE_RESULT);
	        			evt.packageUrl = event.result as String;
	        			dispatchEvent(evt);
	        		},
	        		function (event:FaultEvent):void 
	        		{
	        			dispatchEvent(new Event(PACKAGE_CREATE_FAULT));
	        		}));
		
			return asyncToken;
		}
		
	}
}