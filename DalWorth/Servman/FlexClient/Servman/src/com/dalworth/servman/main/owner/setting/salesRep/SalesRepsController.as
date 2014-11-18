package com.dalworth.servman.main.owner.setting.salesRep
{
	import com.dalworth.servman.main.owner.OwnerModel;
	import com.dalworth.servman.service.SalesRepService;
	
	import mx.collections.Sort;
	import mx.collections.SortField;
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class SalesRepsController
	{
		private var model:SalesRepsModel;
		private var view:UIComponent;
		
		public function SalesRepsController(view:UIComponent)
		{
			this.view = view;
			this.model = SalesRepsModel.getInstance();
		}
		
		public function initModel():void 
		{
			initSalesReps();
		}
		
		private function initSalesReps():void 
		{
			model.salesReps.removeAll();
			model.isBusy = true;
			SalesRepService.getInstance().getAll().addResponder(
				new Responder(
					function(event:ResultEvent):void 
					{
						model.isBusy = false;
						model.salesReps.source = event.result as Array;

						var sort:Sort = new Sort();
						sort.fields = [new SortField("ShowAs")];
						model.salesReps.sort = sort;
						model.salesReps.refresh();
					}, 
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show(event.fault.message);
					}));
		}

	}
}