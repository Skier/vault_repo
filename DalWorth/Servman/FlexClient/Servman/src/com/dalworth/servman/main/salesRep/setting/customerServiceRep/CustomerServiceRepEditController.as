package com.dalworth.servman.main.owner.setting.customerServiceRep
{
	import com.dalworth.servman.domain.CustomerServiceRep;
	import com.dalworth.servman.events.CustomerServiceRepEvent;
	import com.dalworth.servman.service.CustomerServiceRepService;
	
	import flash.events.Event;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class CustomerServiceRepEditController
	{
		private var view:UIComponent;
		private var model:CustomerServiceRepEditModel;
		
		public function CustomerServiceRepEditController(view:UIComponent)
		{
			this.view = view;
			this.model = CustomerServiceRepEditModel.getInstance();
		}
		
		public function initModel(customerServiceRep:CustomerServiceRep):void 
		{
			model.customerServiceRep = customerServiceRep;
		}
		
		public function saveCustomerServiceRep(customerServiceRep:CustomerServiceRep):void 
		{
			model.isBusy = true;
			CustomerServiceRepService.getInstance().saveCustomerServiceRep(customerServiceRep).addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						model.isBusy = false;
						var result:CustomerServiceRep = event.result as CustomerServiceRep;

						model.customerServiceRep.applyFields(result);

						if(model.customerServiceRep.RelatedUser != null && result.RelatedUser != null)
							model.customerServiceRep.RelatedUser.applyFields(result.RelatedUser);
							
						view.dispatchEvent(new CustomerServiceRepEvent(CustomerServiceRepEvent.CUSTOMER_SERVICE_REP_SAVE, model.customerServiceRep));
						view.dispatchEvent(new Event("closeCustomerServiceRepEditor"));
					},
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show(event.fault.message);
					}));
		}

	}
}