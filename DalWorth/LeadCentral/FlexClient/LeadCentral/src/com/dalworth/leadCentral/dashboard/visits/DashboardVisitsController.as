package com.dalworth.leadCentral.dashboard.visits
{
	import com.affilia.util.DateUtil;
	import com.dalworth.leadCentral.domain.TrackingPhone;
	import com.dalworth.leadCentral.domain.TrackingPhoneRotation;
	import com.dalworth.leadCentral.service.PhoneService;
	import com.dalworth.leadCentral.service.registry.TrackingPhoneRegistry;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class DashboardVisitsController
	{
		private var model:DashboardVisitsModel;
		private var view:UIComponent;
		
		public function DashboardVisitsController(view:UIComponent)
		{
			this.view = view;
			this.model = DashboardVisitsModel.getInstance();
		}
		
		private var waitingForUpdate:Boolean;
		
		public function refreshVisits():void 
		{
			if (model.isBusy)
			{
				waitingForUpdate = true;
				return;
 			}
			
			if (model.selectedLeadSources == null)
				return;	
				
			model.isBusy = true;
			
			var fromDate:Date = new Date(model.startDate.fullYear, model.startDate.month, model.startDate.date);
			var toDate:Date = new Date(model.endDate.fullYear, model.endDate.month, model.endDate.date);
			toDate = new Date(toDate.time + 86400000);
			PhoneService.getInstance().getTrackingPhoneRotations(model.selectedLeadSources.source, fromDate, toDate,
				new Responder(
					function(event:ResultEvent):void 
					{
						model.isBusy = false;
						var result:Array = event.result as Array; 
						model.dailyCollection.removeAll();
						
						if (waitingForUpdate)
						{
							waitingForUpdate = false;
							refreshVisits();
							return;
						}
						
						model.pageViews.source = result;
						parseVisits();
						parseDailyCollection();
					}, 
					function(event:FaultEvent):void 
					{
						model.isBusy = false;
						Alert.show(event.fault.message);
					}));
		}
		
		private function parseDailyCollection():void 
		{
			prepareDailyCollection();

			for each (var visit:SiteVisit in model.visits)
			{
				var item:Object = getItemByDate(visit.startTime);
				item["total"]++;
				if (visit.relatedPhoneCalls.length > 0)
					item["contacted"]++;
			}
			
			model.dailyCollection.refresh();
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
		
		private function parseVisits():void 
		{
			model.visits.removeAll();
			
			for each (var pageView:TrackingPhoneRotation in model.pageViews)
			{
				var visit:SiteVisit = getVisitByRotation(pageView);
				visit.relatedRotations.addItem(pageView);
				visit.endTime = pageView.TimeDisplay;
				if (pageView.RelatedPhoneCall != null)
				{
					visit.relatedPhoneCalls.addItem(pageView.RelatedPhoneCall);
					
					var phone:TrackingPhone = TrackingPhoneRegistry.getInstance().getLocal(pageView.RelatedPhoneCall.TrackingPhoneId) as TrackingPhone;
					if (phone != null)
						visit.callFrom = phone.ScreenNumber;
				}
			}
		}
		
		private function getVisitByRotation(rotation:TrackingPhoneRotation):SiteVisit
		{
			var visit:SiteVisit;
			
			for each (visit in model.visits)
			{
				if (visit.sessionId == rotation.SessionIdUid)
					return visit;
			}
			
			visit = new SiteVisit();
			visit.sessionId = rotation.SessionIdUid;
			visit.startTime = rotation.TimeDisplay;
			visit.cameFrom = rotation.ParentReferralUri;
			visit.userHostAddress = rotation.UserHostAddress;

			model.visits.addItem(visit);

			return visit;
		}
		
	}
}