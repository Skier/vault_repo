package com.dalworth.servman.main.businessPartner
{
	import com.dalworth.servman.domain.BusinessPartner;
	import com.dalworth.servman.main.MainAppModel;
	import com.dalworth.servman.service.BusinessPartnerService;
	
	import flash.events.Event;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class BusinessPartnerController
	{
		private var view:UIComponent;
		private var model:BusinessPartnerModel = BusinessPartnerModel.getInstance();
		
		public function BusinessPartnerController(view:UIComponent)
		{
			this.view = view;
		}
		
		public function initModel():void 
		{
			getBusinessPartner();
		}
		
		private function getBusinessPartner():void 
		{
			var id:String = MainAppModel.getInstance().currentUser.QbUserId;
			BusinessPartnerService.getInstance().getByQbUserId(id).addResponder(
				new Responder(
					function(event:ResultEvent):void 
					{
						model.businessPartner = event.result as BusinessPartner;
						view.dispatchEvent(new Event(BusinessPartnerView.MODEL_INITED));
					},
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}))
		}
		
	}
}