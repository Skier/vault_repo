package com.dalworth.servman.main.ptojectType
{
	import com.dalworth.servman.domain.ProjectType;
	import com.dalworth.servman.domain.QbItem;
	import com.dalworth.servman.events.ProjectTypeEvent;
	import com.dalworth.servman.service.ProjectTypeService;
	import com.dalworth.servman.service.QbItemService;
	
	import mx.collections.ArrayCollection;
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class ProjectTypeEditController
	{
		private var model:ProjectTypeEditModel;
		private var view:UIComponent;
		
		public function ProjectTypeEditController(view:UIComponent)
		{
			this.view = view;
			this.model = ProjectTypeEditModel.getInstance();
		}

		public function initItems():void 
		{
			model.availableQbItems.removeAll();
			model.selectedQbItems.removeAll();

			if (model.projectType == null)
				return;
				
			view.enabled = false;
			QbItemService.getInstance().getByProjectTypeId(model.projectType.Id).addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						model.selectedQbItems.source = event.result as Array;
						view.enabled = true;
						
						refreshItems();
					},
					function (event:FaultEvent):void 
					{
						view.enabled = true;
						Alert.show(event.fault.message);
					}));
			
		}
		
		private function refreshItems():void 
		{
			model.availableQbItems.removeAll();
			for each (var item:QbItem in model.qbItems) 
			{
				if (!isContains(item, model.selectedQbItems))
					model.availableQbItems.addItem(item);
			}
		}
		
		private function isContains(item:QbItem, collection:ArrayCollection):Boolean 
		{
			for each (var i:QbItem in collection)
			{
				if (i.ListId == item.ListId)
					return true;
			}
			return false;
		}
		
		public function updateProjectType(newProjectType:ProjectType):void 
		{
			view.enabled = false;
			ProjectTypeService.getInstance().saveProjectType(newProjectType, model.selectedQbItems.source).addResponder(
				new Responder(
					function (event:ResultEvent):void 
					{
						view.enabled = true;
						model.projectType.applyFields(event.result as ProjectType);

						view.dispatchEvent(new ProjectTypeEvent(ProjectTypeEvent.PROJECT_TYPE_SAVED, model.projectType));
					},
					function (event:FaultEvent):void 
					{
						view.enabled = true;
						Alert.show(event.fault.message);
					}));
		}
		
	}
}