package com.dalworth.servman.main.bp
{
	import com.dalworth.servman.domain.BusinessPartner;
	import com.dalworth.servman.events.BusinessPartnerEvent;
	import com.dalworth.servman.service.BusinessPartnerService;
	
	import flash.events.Event;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class BusinessPartnerEditController
	{
		private var view:UIComponent;
		private var model:BusinessPartnerEditModel;
		
		public function BusinessPartnerEditController(view:UIComponent)
		{
			this.view = view;
			this.model = BusinessPartnerEditModel.getInstance();
		}
		
		public function initModel(businessPartner:BusinessPartner):void 
		{
			model.businessPartner = businessPartner;
		}
		
		public function saveBusinessPartner(businessPartner:BusinessPartner, ownPhones:Array = null, companyPhones:Array = null):void 
		{
			model.isBusy = true;
			BusinessPartnerService.getInstance().saveBusinessPartner(businessPartner, ownPhones, companyPhones).addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						model.isBusy = false;
						var result:BusinessPartner = event.result as BusinessPartner;

						model.businessPartner.applyFields(result);

						if(model.businessPartner.RelatedUser != null && result.RelatedUser != null)
							model.businessPartner.RelatedUser.applyFields(result.RelatedUser);
							
						view.dispatchEvent(new BusinessPartnerEvent(BusinessPartnerEvent.BUSINESS_PARTNER_SAVE, model.businessPartner));
						view.dispatchEvent(new Event("closeBusinessPartnerEditor"));
					},
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show(event.fault.message);
					}));
		}

	}
}