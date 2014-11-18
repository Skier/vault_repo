package com.llsvc.services
{
	import com.llsvc.domain.State;
	import com.llsvc.domain.vo.stateVO;
	
	import flash.events.EventDispatcher;
	import flash.events.IEventDispatcher;
	
	import mx.collections.ArrayCollection;
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.InvokeEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.mxml.RemoteObject;
	
	public class StateService extends EventDispatcher
	{
		private static var _instance:StateService;
		
		private var _service:RemoteObject;
		private var localCash:Object;
		private var serviceInvokeCounter:int = 0;
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function StateService(target:IEventDispatcher=null)
		{
			super(target);
			
			if (_instance != null)
				throw new Error("Singleton !");
				
			localCash = new Object();
			serviceInvokeCounter = 0;
		}
		
		public static function get instance():StateService 
		{
			if (_instance == null)
				_instance = new StateService();
			
			return _instance;
		}
		
		public function saveState(value:State):AsyncToken 
		{
			if (value == null) return null;
			
			value.isLoaded = false;
			value.isLoading = true;
	        var asyncToken:AsyncToken = service.save(value.toVO());
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:stateVO = stateVO(event.result);
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
		
		public function getState(id:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.get(id);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:stateVO = stateVO(event.result);
		            	updateLocalCash(result);
	                	State(localCash[result.stateid]).isLoaded = true;
	                	State(localCash[result.stateid]).isLoading = false;
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
		                for each (var item:stateVO in result)
		                {
		                	updateLocalCash(item);
		                	State(localCash[item.stateid]).isLoaded = true;
		                	State(localCash[item.stateid]).isLoading = false;
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
		
		public function getLocal(stateId:int, getFromDB:Boolean = false):State 
		{
			var result:State = localCash[stateId] as State;

			if (result == null && getFromDB) 
			{
				result = new State();
				result.stateid = stateId;
				localCash[stateId] = result;
				getState(stateId);
			}
			
			return result;
		}
		
		private function updateLocalCash(item:stateVO):void 
		{
        	var existingItem:State = localCash[item.stateid] as State;
        	if (existingItem == null) 
        	{
        		existingItem = new State();
        		localCash[item.stateid] = existingItem;
        	}
        	
        	existingItem.updateFields(item);
		}
		
	    private function get service():RemoteObject
	    {
	        if (_service == null) {
	           
	            _service = new RemoteObject( "ColdFusion" );
	            _service.source = "com.llsvc.domain.cfc.stateGateway";
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