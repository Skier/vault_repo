package com.dalworth.leadCentral.lead.convert
{
	import com.dalworth.leadCentral.service.QbInvoiceService;
	
	import flash.events.Event;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class ConvertLeadToJobController
	{
		private var model:ConvertLeadToJobModel;
		private var view:UIComponent;
		
		public function ConvertLeadToJobController(view:UIComponent)
		{
			this.view = view;
			this.model = ConvertLeadToJobModel.getInstance();
		}
		
		public function commitQbInvoice():void 
		{
			prepareJob();
			
			model.isBusy = true;
			QbInvoiceService.getInstance().matchToLead(model.currentQbInvoice, model.currentLead.prepareToSend(), null, null,
				new Responder(
					function (event:ResultEvent):void 
					{
						model.isBusy = false;
						view.dispatchEvent(new Event("jobMatchComplete"));
					},
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show(event.fault.message);
					}
				));
			
		} 
		
		private function prepareJob():void 
		{
			model.currentQbInvoice.LeadId = model.currentLead.Id;
		}

	}
}