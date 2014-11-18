package com.llsvc.services
{
	import com.llsvc.domain.File;
	import com.llsvc.domain.vo.fileVO;
	
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
	
	public class FileService extends EventDispatcher
	{
		private static var _instance:FileService;
		
		private var _service:RemoteObject;
		private var localCash:Object;
		private var serviceInvokeCounter:int = 0;
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function FileService(target:IEventDispatcher=null)
		{
			super(target);
			
			if (_instance != null)
				throw new Error("Singleton !");
				
			localCash = new Object();
			serviceInvokeCounter = 0;
		}
		
		public static function get instance():FileService 
		{
			if (_instance == null)
				_instance = new FileService();
			
			return _instance;
		}
		
		public function saveFile(value:File):AsyncToken 
		{
			if (value == null) throw new Error("Cannot save null File");
			
			value.isLoaded = false;
			value.isLoading = true;
	        var asyncToken:AsyncToken = service.save(value.toVO());
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:fileVO = fileVO(event.result);
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
		
		public function getFile(id:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.get(id);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
		            	var result:fileVO = fileVO(event.result);
		            	updateLocalCash(result);
	                	File(localCash[result.fileid]).isLoaded = true;
	                	File(localCash[result.fileid]).isLoading = false;
		            },
		            function (event:FaultEvent):void
		            {
		            	trace(event.fault.message);
		            }
	            )
	        );
	
	        return asyncToken;
		}
		
		public function deleteFile(id:int):AsyncToken 
		{
	        var asyncToken:AsyncToken = service.deleteEntity(id);
	        asyncToken.addResponder(
	        	new Responder(
		            function (event:ResultEvent):void
		            {
	                	File(localCash[id]).isLoading = false;
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
		                for each (var item:fileVO in result)
		                {
		                	updateLocalCash(item);
		                	File(localCash[item.fileid]).isLoaded = true;
		                	File(localCash[item.fileid]).isLoading = false;
		                }
		                
		                dispatchEvent(new Event("filesByUserIdLoaded"));
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
		                for each (var item:fileVO in result)
		                {
		                	updateLocalCash(item);
		                	File(localCash[item.fileid]).isLoaded = true;
		                	File(localCash[item.fileid]).isLoading = false;
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
		
		public function getLocal(fileId:int, getFromDB:Boolean = false):File 
		{
			var result:File = localCash[fileId] as File;

			if (result == null && getFromDB) 
			{
				result = new File();
				result.fileid = fileId;
				localCash[fileId] = result;
				getFile(fileId);
			}
			
			return result;
		}
		
		private function updateLocalCash(item:fileVO):void 
		{
        	var existingItem:File = localCash[item.fileid] as File;
        	if (existingItem == null) 
        	{
        		existingItem = new File();
        		localCash[item.fileid] = existingItem;
        	}
        	
        	existingItem.updateFields(item);
		}
		
	    private function get service():RemoteObject
	    {
	        if (_service == null) {
	           
	            _service = new RemoteObject( "ColdFusion" );
	            _service.source = "com.llsvc.domain.cfc.fileGateway";
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