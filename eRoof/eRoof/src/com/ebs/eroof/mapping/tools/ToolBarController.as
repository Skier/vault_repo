package com.ebs.eroof.mapping.tools
{
	import com.ebs.eroof.mapping.IMapContainer;
	
	import flash.events.Event;
	
	import mx.core.UIComponent;
	
	public class ToolBarController
	{
		private var view:UIComponent;
		private var model:ToolBarModel;
		
		public function ToolBarController(view:UIComponent)
		{
			this.view = view;
			this.model = ToolBarModel.getInstance();
		}
		
		public function setContainer(value:IMapContainer):void 
		{
			model.mapContainer = value;
			initToolBar();
		}
		
		private function initToolBar():void 
		{
			var toolEdit:MapToolEditPolygon = new MapToolEditPolygon();
			toolEdit.init(model.mapContainer);
			model.tools.addItem(toolEdit);

			var toolCreate:MapToolCreatePolygon = new MapToolCreatePolygon();
			toolCreate.init(model.mapContainer);
			toolCreate.addEventListener("closePolygonRequest", closePolygonHandler);
			model.tools.addItem(toolCreate);
		}	
			
		public function activateTool(tool:IMapTool):void 
		{
			if (model.activeTool != null) 
				model.activeTool.deactivate();
			
			model.activeTool = tool;
			
			if (model.activeTool)
				model.activeTool.activate();
		}
		
		private function closePolygonHandler(event:Event):void 
		{
			//IMapTool(event.currentTarget).deactivate();
		}
	}
}