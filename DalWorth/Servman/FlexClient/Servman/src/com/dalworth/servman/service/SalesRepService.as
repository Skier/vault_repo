package com.dalworth.servman.service
{
	import com.dalworth.servman.domain.Phone;
	import com.dalworth.servman.domain.SalesRep;
	import com.dalworth.servman.service.registry.SalesRepRegistry;
	
	import flash.events.EventDispatcher;
	
	import mx.collections.ArrayCollection;
	import mx.rpc.AsyncToken;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.InvokeEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.mxml.RemoteObject;
	
	public class SalesRepService extends EventDispatcher
	{
		private static var _instance:SalesRepService;
		
		private var _service:RemoteObject;
		private var serviceInvokeCounter:int = 0;
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function SalesRepService()
		{
			super(null);
			
			if (_instance != null)
				throw new Error("Singleton !");
			
			serviceInvokeCounter = 0;
		}
		
		public static function getInstance():SalesRepService 
		{
			if (_instance == null)
				_instance = new SalesRepService();
			
			return _instance;
		}
		
		public function getSalesRep(id:int):AsyncToken 
		{
			var asyncToken:AsyncToken = service.getById(id);
			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
						SalesRepRegistry.getInstance().storeLocal(event.result);
					},
					function (event:FaultEvent):void
					{
						trace(event.fault.message);
					}
				)
			);
			
			return asyncToken;
		}
		
		public function saveSalesRep(salesRep:SalesRep, ownPhones:Array = null, companyPhones:Array = null):AsyncToken 
		{
			var responder:Responder = 
				new Responder(
					function (event:ResultEvent):void
					{
						SalesRepRegistry.getInstance().storeLocal(event.result);
					},
					function (event:FaultEvent):void
					{
						trace(event.fault.message);
					}
				);
			
			var asyncToken:AsyncToken;
			
			if (ownPhones == null && companyPhones == null)
				asyncToken = service.Save(salesRep);
			else 
				asyncToken = service.SaveWithPhones(salesRep, ownPhones, companyPhones);
			
			asyncToken.addResponder(responder);

			return asyncToken;
		}
		
		public function getAll():AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetAll();
			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
						SalesRepRegistry.getInstance().forceUpdate(event.result as Array);
					},
					function (event:FaultEvent):void
					{
						trace(event.fault.message);
					}
				)
			);
			
			return asyncToken;
		}
		
		public function getByCompanyPhoneId(phoneId:int):AsyncToken 
		{
			var asyncToken:AsyncToken = service.GetByCompanyPhoneId(phoneId);
			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
						var result:Array = event.result as Array;
						for each (var item:SalesRep in result)
						{
							SalesRepRegistry.getInstance().storeLocal(item);
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
		
		public function addCompanyPhone(salesRep:SalesRep, phone:Phone):AsyncToken 
		{
			var asyncToken:AsyncToken = service.AddCompanyPhone(salesRep.prepareToSend(), phone.prepareToSend());
			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
						trace(event.message);
					},
					function (event:FaultEvent):void
					{
						trace(event.fault.message);
					}
				)
			);
			
			return asyncToken;
		}
		
		public function removeCompanyPhone(salesRep:SalesRep, phone:Phone):AsyncToken 
		{
			var asyncToken:AsyncToken = service.RemoveCompanyPhone(salesRep.prepareToSend(), phone.prepareToSend());
			asyncToken.addResponder(
				new Responder(
					function (event:ResultEvent):void
					{
						trace(event.message);
					},
					function (event:FaultEvent):void
					{
						trace(event.fault.message);
					}
				)
			);
			
			return asyncToken;
		}
		
		private function get service():RemoteObject
		{
			if (_service == null) {
				
				_service = new RemoteObject( "GenericDestination" );
				_service.source = "Servman.Intuit.Weborb.SalesRepService";
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