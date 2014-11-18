package com.dalworth.servman.main.employee
{
	import com.dalworth.servman.service.BusinessPartnerService;
	import com.dalworth.servman.service.IDSCustomerService;
	
	import flash.events.Event;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class EmployeeController
	{
		private var view:UIComponent;
		private var model:EmployeeModel = EmployeeModel.getInstance();
		
		public function EmployeeController(view:UIComponent)
		{
			this.view = view;
		}
		
		public function initModel():void 
		{
			getCustomers();
		}
		
		private function getCustomers():void 
		{
			IDSCustomerService.getInstance().getAll().addResponder(
				new Responder(
					function(event:ResultEvent):void 
					{
						model.customers.source = event.result as Array;
						getBusinessPartners();
					},
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}))
		}
		
		private function getBusinessPartners():void 
		{
			BusinessPartnerService.getInstance().getAll().addResponder(
				new Responder(
					function(event:ResultEvent):void 
					{
						model.businessPartners.source = event.result as Array;
						view.dispatchEvent(new Event(EmployeeView.MODEL_INITED));
					},
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}))
		}
		
	}
}