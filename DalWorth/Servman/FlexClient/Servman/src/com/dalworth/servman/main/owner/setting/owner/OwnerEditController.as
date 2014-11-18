package com.dalworth.servman.main.owner.setting.owner
{
	import com.dalworth.servman.domain.Owner;
	import com.dalworth.servman.events.OwnerEvent;
	import com.dalworth.servman.service.OwnerService;
	
	import flash.events.Event;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class OwnerEditController
	{
		private var view:UIComponent;
		private var model:OwnerEditModel;
		
		public function OwnerEditController(view:UIComponent)
		{
			this.view = view;
			this.model = OwnerEditModel.getInstance();
		}
		
		public function initModel(owner:Owner):void 
		{
			model.owner = owner;
		}
		
		public function saveOwner(owner:Owner):void 
		{
			model.isBusy = true;
			OwnerService.getInstance().saveOwner(owner).addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						model.isBusy = false;
						var result:Owner = event.result as Owner;

						model.owner.applyFields(result);

						if(model.owner.RelatedUser != null && result.RelatedUser != null)
							model.owner.RelatedUser.applyFields(result.RelatedUser);
							
						view.dispatchEvent(new OwnerEvent(OwnerEvent.OWNER_SAVE, model.owner));
						view.dispatchEvent(new Event("closeOwnerEditor"));
					},
					function (event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show(event.fault.message);
					}));
		}

	}
}