package com.titus.catalog.storage.service
{
	import com.titus.catalog.model.Submittal;
	
	import mx.collections.ArrayCollection;
	import mx.controls.Alert;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.RemoteObject;
	
	public class SubmittalService
	{
		private static var instance:SubmittalService;
		
		private var service:RemoteObject;
		private var localSubmittals:Object;
		
		public static function getInstance():SubmittalService
		{
			if (!instance)
				instance = new SubmittalService();
	
			return instance;
		}
		
		public function SubmittalService()
		{
			if (instance)
				throw new Error("DB Storage is singleton!");

			localSubmittals = new Object();
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

		public function getSubmittals(modelItemId:int):ArrayCollection
		{
			var result:ArrayCollection = new ArrayCollection();
			
        	getService().GetSubmittals.addEventListener(ResultEvent.RESULT, 
        		function (event:ResultEvent):void 
        		{
        			result.removeAll();
					for each (var sub:Submittal in event.result as Array) 
					{
						var localSub:Submittal = localSubmittals[sub.FileId];
						if (localSub == null) {
							localSubmittals[sub.FileId] = sub;
							result.addItem(sub);
						} else {
							result.addItem(localSub);
						}
					}
        		});
        		 
        	getService().GetSubmittals.addEventListener(FaultEvent.FAULT, 
        		function(evt:FaultEvent):void 
        		{
        			result.removeAll();
	        		Alert.show(evt.fault.message, "Error");
	        	});
	        		
        	getService().GetSubmittals(modelItemId);
	        
	        return result;
		}
	}
}