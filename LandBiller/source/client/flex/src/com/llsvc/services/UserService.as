package com.llsvc.services
{
	import com.llsvc.domain.User;
	import com.llsvc.domain.events.UserEvent;
	import com.llsvc.domain.vo.invoiceVO;
	import com.llsvc.domain.vo.userVO;
	
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
	
	public class UserService extends EventDispatcher
	{
		private static var _instance:UserService;
		
		private var _service:RemoteObject;
		private var localCash:Object;
		private var serviceInvokeCounter:int = 0;
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function UserService(target:IEventDispatcher=null)
		{
			super(target);
			
			if (_instance != null)
				throw new Error("Singleton !");
				
			localCash = new Object();
			serviceInvokeCounter = 0;
		}
		
		public static function get instance():UserService 
		{
			if (_instance == null)
				_instance = new UserService();
			
			return _instance;
		}
		
		public function saveUser(value:User):AsyncToken 
		{
			if (value == null) return null;
			
			value.isLoaded = false;
			value.isLoading = true;
	        var asyncToken:AsyncToken = service.save(value.toVO());
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:userVO = userVO(event.result);
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
		
		public function getUser(id:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.get(id);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:userVO = event.result as userVO;
		            	if (result != null) 
		            	{
			            	updateLocalCash(result);
		                	User(localCash[result.userid]).isLoaded = true;
		                	User(localCash[result.userid]).isLoading = false;
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
		
		public function deleteUser(id:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.deleteEntity(id);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
	                	User(localCash[id]).isLoading = false;
		            	localCash[id] = null;
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
		                for each (var item:userVO in result)
		                {
		                	updateLocalCash(item);
		                	User(localCash[item.userid]).isLoaded = true;
		                	User(localCash[item.userid]).isLoading = false;
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
		
		public function getLocal(userId:int, getFromDB:Boolean = false):User 
		{
			var result:User = localCash[userId] as User;

			if (result == null && getFromDB) 
			{
				result = new User();
				result.userid = userId;
				localCash[userId] = result;
				getUser(userId);
			}
			
			return result;
		}
		
		public function login(username:String, password:String):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.login(username, password);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:userVO = event.result as userVO;
		            	if (result != null) 
		            	{
			            	updateLocalCash(result);
		                	User(localCash[result.userid]).isLoaded = true;
		                	User(localCash[result.userid]).isLoading = false;
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
		
		public function saveCascade(user:User):void 
		{
			if (user == null 
				|| user.login == null 
				|| user.login.person == null 
				|| user.login.person.address == null
				|| user.login.person.address.state == null)
			{
				throw new Error("User entity is incomplete!");
			}
			
			AddressService.instance.saveAddress(user.login.person.address).addResponder(
				new mx.rpc.Responder(
					function (event:ResultEvent):void 
					{
						user.login.person.addressid = user.login.person.address.addressid;
						
						PersonService.instance.savePerson(user.login.person).addResponder(
							new mx.rpc.Responder(
								function (event:ResultEvent):void 
								{
									user.login.personid = user.login.person.personid;

									LoginService.instance.saveLogin(user.login).addResponder(
										new mx.rpc.Responder(
											function (event:ResultEvent):void 
											{
												user.loginid = user.login.loginid;
												
												saveUser(user).addResponder(
													new mx.rpc.Responder(
														function (event:ResultEvent):void 
														{
															dispatchEvent(new UserEvent(UserEvent.REGISTRATION_COMPLETE, user));
														}, faultRegistrationHandler));
											}, faultRegistrationHandler));
								}, faultRegistrationHandler));
					}, faultRegistrationHandler));
		}
		
		private function faultRegistrationHandler(event:FaultEvent):void 
		{
			dispatchEvent(new Event("registrationFailed"));
		}
		
		private function updateLocalCash(item:userVO):void 
		{
        	var existingItem:User = localCash[item.userid] as User;
        	if (existingItem == null) 
        	{
        		existingItem = new User();
        		localCash[item.userid] = existingItem;
        	}
        	
        	existingItem.updateFields(item);
		}
		
	    private function get service():RemoteObject
	    {
	        if (_service == null) {
	           
	            _service = new RemoteObject( "ColdFusion" );
	            _service.source = "com.llsvc.domain.cfc.userGateway";
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
	    
	    private function faultHandler(event:FaultEvent):void 
	    {
	    	dispatchEvent(new Event("loadingFault"));
	    	trace(event.fault.message);
	    }
	    
	}
}