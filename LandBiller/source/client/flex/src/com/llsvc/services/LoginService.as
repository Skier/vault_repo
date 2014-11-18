package com.llsvc.services
{
	import com.llsvc.domain.Login;
	import com.llsvc.domain.vo.loginVO;
	
	import flash.events.Event;
	import flash.events.EventDispatcher;
	import flash.events.IEventDispatcher;
	
	import mx.collections.ArrayCollection;
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.InvokeEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.mxml.RemoteObject;
	
	public class LoginService extends EventDispatcher
	{
		private static var _instance:LoginService;
		
		private var _service:RemoteObject;
		private var localCash:Object;
		private var serviceInvokeCounter:int = 0;
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function LoginService(target:IEventDispatcher=null)
		{
			super(target);
			
			if (_instance != null)
				throw new Error("Singleton !");
				
			localCash = new Object();
			serviceInvokeCounter = 0;
		}
		
		public static function get instance():LoginService 
		{
			if (_instance == null)
				_instance = new LoginService();
			
			return _instance;
		}
		
		public function saveLogin(value:Login):AsyncToken 
		{
			if (value == null) return null;
			
			value.isLoaded = false;
			value.isLoading = true;
	        var asyncToken:AsyncToken = service.save(value.toVO());
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:loginVO = loginVO(event.result);
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
		
		public function getLogin(id:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.get(id);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:loginVO = loginVO(event.result);
		            	updateLocalCash(result);
	                	Login(localCash[result.loginid]).isLoaded = true;
	                	Login(localCash[result.loginid]).isLoading = false;
		            },
		            function (event:FaultEvent):void
		            {
		            	trace(event.fault.message);
		            }
	            )
	        );
	
	        return asyncToken;
		}
		
		public function deleteLogin(id:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.deleteEntity(id);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	localCash[id] = null;
	                	Login(localCash[id]).isLoading = false;
		            },
		            function (event:FaultEvent):void
		            {
		            	trace(event.fault.message);
		            }
	            )
	        );
	
	        return asyncToken;
		}
		
		public function getByUserId(userId:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.getByUserId(userId);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:ArrayCollection = new ArrayCollection(event.result as Array);
		                for each (var item:loginVO in result)
		                {
		                	updateLocalCash(item);
		                	Login(localCash[item.loginid]).isLoaded = true;
		                	Login(localCash[item.loginid]).isLoading = false;
		                }
		                
		                dispatchEvent(new Event("loginsByUserIdLoaded"));
		            },
		            function (event:FaultEvent):void
		            {
		            	trace(event.fault.message);
		            }
	            )
	        );
	
	        return asyncToken;
		}
		
		public function checkUser(username:String):AsyncToken 
		{
	        return service.checkUser(username);
		}
		
		public function getLocal(loginId:int, getFromDB:Boolean = false):Login 
		{
			var result:Login = localCash[loginId] as Login;

			if (result == null && getFromDB) 
			{
				result = new Login();
				result.loginid = loginId;
				localCash[loginId] = result;
				getLogin(loginId);
			}
			
			return result;
		}
		
		private function updateLocalCash(item:loginVO):void 
		{
        	var existingItem:Login = localCash[item.loginid] as Login;
        	if (existingItem == null) 
        	{
        		existingItem = new Login();
        		localCash[item.loginid] = existingItem;
        	}
        	
        	existingItem.updateFields(item);
		}
		
	    private function get service():RemoteObject
	    {
	        if (_service == null) {
	           
	            _service = new RemoteObject( "ColdFusion" );
	            _service.source = "com.llsvc.domain.cfc.loginGateway";
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