package com.dalworth.servman.main.owner.setting.customerServiceRep
{
	import com.dalworth.servman.service.CustomerServiceRepService;
	
	import mx.collections.Sort;
	import mx.collections.SortField;
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class CustomerServiceRepsController
	{
		private var model:CustomerServiceRepsModel;
		private var view:UIComponent;
		
		public function CustomerServiceRepsController(view:UIComponent)
		{
			this.view = view;
			this.model = CustomerServiceRepsModel.getInstance();
		}
		
		public function initModel():void 
		{
			initcustomerServiceReps();
		}
		
		private function initcustomerServiceReps():void 
		{
			model.customerServiceReps.removeAll();
			model.isBusy = true;
			CustomerServiceRepService.getInstance().getAll().addResponder(
				new Responder(
					function(event:ResultEvent):void 
					{
						model.isBusy = false;
						model.customerServiceReps.source = event.result as Array;

						var sort:Sort = new Sort();
						sort.fields = [new SortField("ShowAs")];
						model.customerServiceReps.sort = sort;
						model.customerServiceReps.refresh();
					}, 
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show(event.fault.message);
					}));
		}

	}
}