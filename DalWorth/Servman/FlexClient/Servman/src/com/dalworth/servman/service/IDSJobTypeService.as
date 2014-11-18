package com.dalworth.servman.service
{
	import Intuit.Sb.Cdm.vo.JobType;
	
	import flash.events.Event;
	import flash.events.EventDispatcher;
	
	import mx.collections.ArrayCollection;
	import mx.collections.Sort;
	import mx.collections.SortField;
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.InvokeEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.mxml.RemoteObject;
	
	public class IDSJobTypeService extends EventDispatcher
	{
		private static var _instance:IDSJobTypeService;
		
		private var _service:RemoteObject;
		private var serviceInvokeCounter:int = 0;
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function IDSJobTypeService()
		{
			super(null);
			
			if (_instance != null)
				throw new Error("Singleton !");
			
			serviceInvokeCounter = 0;
		}
		
		public static function getInstance():IDSJobTypeService 
		{
			if (_instance == null)
				_instance = new IDSJobTypeService();
			
			return _instance;
		}
		
		public function getAll():ArrayCollection 
		{
			var result:ArrayCollection = new ArrayCollection();
			
			var asyncToken:AsyncToken = service.GetAll();
			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
						for each (var jobType:JobType in (event.result as Array))
						{
							if (jobType.Active && jobType.JobTypeParentId == null)
								result.addItem(jobType);
						}
						
						var sort:Sort = new Sort();
						sort.fields = [new SortField("Name", true)];
						result.sort = sort;
						result.refresh();

						dispatchEvent(new Event("jobTypesLoaded"));
					},
					function (event:FaultEvent):void
					{
						trace(event.fault.message);
						dispatchEvent(new Event("jobTypesLoadFailed"));
					}
				)
			);
			
			return result;
		}
		
		private function get service():RemoteObject
		{
			if (_service == null) {
				
				_service = new RemoteObject( "GenericDestination" );
				_service.source = "Servman.Intuit.Weborb.IDSJobTypeService";
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