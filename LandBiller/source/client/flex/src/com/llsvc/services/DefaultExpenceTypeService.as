package com.llsvc.services
{
	import com.llsvc.domain.DefaultExpenceType;
	import com.llsvc.domain.vo.defaultexpencetypeVO;
	
	import flash.events.EventDispatcher;
	import flash.events.IEventDispatcher;
	
	import mx.collections.ArrayCollection;
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.InvokeEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.mxml.RemoteObject;
	
	public class DefaultExpenceTypeService extends EventDispatcher
	{
		private static var _instance:DefaultExpenceTypeService;
		
		private var _service:RemoteObject;
		private var localCash:Object;
		private var serviceInvokeCounter:int = 0;
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function DefaultExpenceTypeService(target:IEventDispatcher=null)
		{
			super(target);
			
			if (_instance != null)
				throw new Error("Singleton !");
				
			localCash = new Object();
			serviceInvokeCounter = 0;
		}
		
		public static function get instance():DefaultExpenceTypeService 
		{
			if (_instance == null)
				_instance = new DefaultExpenceTypeService();
			
			return _instance;
		}
		
		public function saveDefaultExpenceType(value:DefaultExpenceType):AsyncToken 
		{
			if (value == null) return null;
			
			value.isLoaded = false;
			value.isLoading = true;
	        var asyncToken:AsyncToken = service.save(value.toVO());
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:defaultexpencetypeVO = defaultexpencetypeVO(event.result);
		            	value.updateFields(result);
		            	updateLocalCash(result);
						value.isLoaded = true;
						value.isLoading = false;
		            },
		            function (event:FaultEvent):void
		            {
						value.isLoaded = false;
						value.isLoading = false;
		            	trace(event.fault.message);
		            }
	            )
	        );
	
	        return asyncToken;
		}
		
		public function getDefaultExpenceType(id:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.get(id);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:defaultexpencetypeVO = defaultexpencetypeVO(event.result);
		            	updateLocalCash(result);
	                	DefaultExpenceType(localCash[result.defaultexpencetypeid]).isLoaded = true;
	                	DefaultExpenceType(localCash[result.defaultexpencetypeid]).isLoading = false;
		            },
		            function (event:FaultEvent):void
		            {
		            	trace(event.fault.message);
		            }
	            )
	        );
	
	        return asyncToken;
		}
		
		public function deleteDefaultExpenceType(id:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.deleteEntity(id);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	localCash[id] = null;
	                	DefaultExpenceType(localCash[id]).isLoading = false;
		            },
		            function (event:FaultEvent):void
		            {
		            	trace(event.fault.message);
		            }
	            )
	        );
	
	        return asyncToken;
		}
		
		public function getAll():AsyncToken 
		{
	        var asyncToken:AsyncToken = service.getAll();
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:ArrayCollection = new ArrayCollection(event.result as Array);
		            	for each (var type:defaultexpencetypeVO in result) 
		            	{
			            	updateLocalCash(type);
		                	DefaultExpenceType(localCash[type.defaultexpencetypeid]).isLoaded = true;
		                	DefaultExpenceType(localCash[type.defaultexpencetypeid]).isLoading = false;
		            	}
		            },
		            function (event:FaultEvent):void
		            {
		            	trace(event.fault.message);
		            }
	            )
	        );
	
	        return asyncToken;
		}
		
		public function getLocal(defaultExpenceTypeId:int, getFromDB:Boolean = false):DefaultExpenceType 
		{
			var result:DefaultExpenceType = localCash[defaultExpenceTypeId] as DefaultExpenceType;

			if (result == null && getFromDB) 
			{
				result = new DefaultExpenceType();
				result.defaultexpencetypeid = defaultExpenceTypeId;
				localCash[defaultExpenceTypeId] = result;
				getDefaultExpenceType(defaultExpenceTypeId);
			}
			
			return result;
		}
		
		private function updateLocalCash(item:defaultexpencetypeVO):void 
		{
        	var existingItem:DefaultExpenceType = localCash[item.defaultexpencetypeid] as DefaultExpenceType;
        	if (existingItem == null) 
        	{
        		existingItem = new DefaultExpenceType();
        		localCash[item.defaultexpencetypeid] = existingItem;
        	}
        	
        	existingItem.updateFields(item);
		}
		
	    private function get service():RemoteObject
	    {
	        if (_service == null) {
	           
	            _service = new RemoteObject( "ColdFusion" );
	            _service.source = "com.llsvc.domain.cfc.defaultexpencetypeGateway";
	            _service.showBusyCursor = true;
	            
	            _service.addEventListener(InvokeEvent.INVOKE, 
	                function(event:InvokeEvent):void 
	                { 
	                	serviceInvokeCounter++;
	                	serviceIsBusy = true; 
	                });
	            
	            _service.addEventListener(ResultEvent.RESULT,
	                function(event:ResultEvent):void 
	                { 
	                	serviceInvokeCounter--;
	                	if (serviceInvokeCounter <= 0) 
	                	{
	                		serviceInvokeCounter = 0;
	                		serviceIsBusy = false; 
	                	}
	                });
	            
	            _service.addEventListener(FaultEvent.FAULT,
	                function(event:FaultEvent):void 
	                { 
	                	serviceInvokeCounter--;
	                	if (serviceInvokeCounter <= 0) 
	                	{
	                		serviceInvokeCounter = 0;
	                		serviceIsBusy = false; 
	                	}
	                });
	        }
	
	        return _service;
	    }
	    
	}
}