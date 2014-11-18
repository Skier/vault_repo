package com.dalworth.servman.main.owner.setting.salesRep
{
	import com.dalworth.servman.domain.SalesRep;
	import com.dalworth.servman.events.SalesRepEvent;
	import com.dalworth.servman.service.SalesRepService;
	
	import flash.events.Event;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class SalesRepEditController
	{
		private var view:UIComponent;
		private var model:SalesRepEditModel;
		
		public function SalesRepEditController(view:UIComponent)
		{
			this.view = view;
			this.model = SalesRepEditModel.getInstance();
		}
		
		public function initModel(salesRep:SalesRep):void 
		{
			model.salesRep = salesRep;
		}
		
		public function saveSalesRep(salesRep:SalesRep, ownPhones:Array, companyPhones:Array):void 
		{
			model.isBusy = true;
			SalesRepService.getInstance().saveSalesRep(salesRep, ownPhones, companyPhones).addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						model.isBusy = false;
						var result:SalesRep = event.result as SalesRep;

						model.salesRep.applyFields(result);

						if(model.salesRep.RelatedUser != null && result.RelatedUser != null)
							model.salesRep.RelatedUser.applyFields(result.RelatedUser);
							
						view.dispatchEvent(new SalesRepEvent(SalesRepEvent.SALES_REP_SAVE, model.salesRep));
						view.dispatchEvent(new Event("closeSalesRepEditor"));
					},
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show(event.fault.message);
					}));
		}

	}
}