package com.llsvc.services
{
	import com.llsvc.domain.Project;
	import com.llsvc.domain.vo.projectVO;
	
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
	
	public class ProjectService extends EventDispatcher
	{
		private static var _instance:ProjectService;
		
		private var _service:RemoteObject;
		private var localCash:Object;
		private var serviceInvokeCounter:int = 0;
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function ProjectService(target:IEventDispatcher=null)
		{
			super(target);
			
			if (_instance != null)
				throw new Error("Singleton !");
				
			localCash = new Object();
			serviceInvokeCounter = 0;
		}
		
		public static function get instance():ProjectService 
		{
			if (_instance == null)
				_instance = new ProjectService();
			
			return _instance;
		}
		
		public function saveProject(value:Project):AsyncToken 
		{
			if (value == null) return null;
			
			value.isLoaded = false;
			value.isLoading = true;
	        var asyncToken:AsyncToken = service.save(value.toVO());
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:projectVO = projectVO(event.result);
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
		
		public function getProject(id:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.get(id);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:projectVO = projectVO(event.result);
		            	updateLocalCash(result);
	                	Project(localCash[result.projectid]).isLoaded = true;
	                	Project(localCash[result.projectid]).isLoading = false;
		            },
		            function (event:FaultEvent):void
		            {
		            	trace(event.fault.message);
		            }
	            )
	        );
	
	        return asyncToken;
		}
		
		public function deleteProject(id:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.deleteEntity(id);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
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
		
		public function getByUserId(userId:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.getByUserId(userId);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:ArrayCollection = new ArrayCollection(event.result as Array);
		                for each (var item:projectVO in result)
		                {
		                	updateLocalCash(item);
		                	Project(localCash[item.projectid]).isLoaded = true;
		                	Project(localCash[item.projectid]).isLoading = false;
		                }
		                
		                dispatchEvent(new Event("projectsByUserIdLoaded"));
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
		                for each (var item:projectVO in result)
		                {
		                	updateLocalCash(item);
		                	Project(localCash[item.projectid]).isLoaded = true;
		                	Project(localCash[item.projectid]).isLoading = false;
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
		
		public function getLocal(projectId:int, getFromDB:Boolean = false):Project 
		{
			var result:Project = localCash[projectId] as Project;

			if (result == null && getFromDB) 
			{
				result = new Project();
				result.projectid = projectId;
				localCash[projectId] = result;
				getProject(projectId);
			}
			
			return result;
		}
		
		private function updateLocalCash(item:projectVO):void 
		{
        	var existingItem:Project = localCash[item.projectid] as Project;
        	if (existingItem == null) 
        	{
        		existingItem = new Project();
        		localCash[item.projectid] = existingItem;
        	}
        	
        	existingItem.updateFields(item);
		}
		
	    private function get service():RemoteObject
	    {
	        if (_service == null) {
	           
	            _service = new RemoteObject( "ColdFusion" );
	            _service.source = "com.llsvc.domain.cfc.projectGateway";
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