package com.dalworth.servman.main.owner.setting.owner
{
	import com.dalworth.servman.main.owner.OwnerModel;
	import com.dalworth.servman.service.OwnerService;
	
	import mx.collections.Sort;
	import mx.collections.SortField;
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class OwnersController
	{
		private var model:OwnersModel;
		private var view:UIComponent;
		
		public function OwnersController(view:UIComponent)
		{
			this.view = view;
			this.model = OwnersModel.getInstance();
		}
		
		public function initModel():void 
		{
			initOwners();
		}
		
		private function initOwners():void 
		{
			model.owners.removeAll();
			model.isBusy = true;
			OwnerService.getInstance().getAll().addResponder(
				new Responder(
					function(event:ResultEvent):void 
					{
						model.isBusy = false;
						model.owners.source = event.result as Array;

						var sort:Sort = new Sort();
						sort.fields = [new SortField("ShowAs")];
						model.owners.sort = sort;
						model.owners.refresh();
					}, 
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show(event.fault.message);
					}));
		}

	}
}