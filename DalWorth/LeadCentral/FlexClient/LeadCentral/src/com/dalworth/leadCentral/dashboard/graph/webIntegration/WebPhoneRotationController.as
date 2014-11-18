package com.dalworth.leadCentral.dashboard.graph.webIntegration
{
	import com.affilia.util.DateUtil;
	import com.dalworth.leadCentral.domain.TrackingPhoneRotation;
	import com.dalworth.leadCentral.service.PhoneService;
	
	import mx.collections.Sort;
	import mx.collections.SortField;
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class WebPhoneRotationController
	{
		private var model:WebPhoneRotationModel;
		private var view:UIComponent;
		
		public function WebPhoneRotationController(view:UIComponent)
		{
			this.view = view;
			this.model = WebPhoneRotationModel.getInstance();
		}
		
		public function refreshRotations():void 
		{
			if (model.isBusy || model.selectedLeadSources == null)
				return;
				
			model.isBusy = true;
			
			var fromDate:Date = new Date(model.startDate.fullYear, model.startDate.month, model.startDate.date);
			var toDate:Date = new Date(model.endDate.fullYear, model.endDate.month, model.endDate.date);
			toDate = new Date(toDate.time + 86400000);
			PhoneService.getInstance().getTrackingPhoneRotations(model.selectedLeadSources.source, fromDate, toDate,
				new mx.rpc.Responder(
					function(event:ResultEvent):void 
					{
						model.isBusy = false;
						var result:Array = event.result as Array; 
						
						model.dailyCollection.removeAll();
						prepareDailyCollection();
						model.rotations.source = result;
						for each (var rotation:TrackingPhoneRotation in result)
						{
							var item:Object = getItemByDate(rotation.TimeDisplay);
							item["total"] = (item["total"] as int) + 1;
							if (rotation.RelatedPhoneCall != null)
								item["contacted"] = (item["contacted"] as int) + 1;
						}
						model.dailyCollection.refresh();
					}, 
					function(event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show(event.fault.message);
					}));
		}
		
		private function prepareDailyCollection():void 
		{
			var currentDate:Date = model.startDate;
			while(currentDate.time <= model.endDate.time) 
			{
				var result:Object = {dateStr:DateUtil.getDateStr(currentDate), total:0, contacted:0};
				model.dailyCollection.addItem(result);
				currentDate = new Date(currentDate.time + 86400000);
			}
		}
		
		private function getItemByDate(date:Date):Object 
		{
			var dateStr:String = DateUtil.getDateStr(date);
			
			for each(var item:Object in model.dailyCollection) 
			{
				if (item["dateStr"] == dateStr)
					return item;
			}
			
			var result:Object = {dateStr:dateStr, total:0, contacted:0};
			model.dailyCollection.addItem(result);
			return result; 
		}
		
	}
}