package com.dalworth.leadCentral.transport.phoneCall
{
	import com.dalworth.leadCentral.domain.PhoneCall;
	import com.dalworth.leadCentral.domain.TrackingPhoneRotation;
	import com.dalworth.leadCentral.service.PhoneService;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class PhoneCallController
	{
		private var model:PhoneCallModel;
		private var view:UIComponent;
		
		public function PhoneCallController(view:UIComponent)
		{
			this.view = view;
			this.model = PhoneCallModel.getInstance();
		}
		
		public function setPhoneCall(phoneCall:PhoneCall):void 
		{
			model.phoneCall = phoneCall;
			initRotations();
		}
		
		private function initRotations():void 
		{
			model.rotations.removeAll();
			if (model.phoneCall == null)
				return;
				
			PhoneService.getInstance().getTrackingPhoneRotationsByPhoneCall(model.phoneCall.prepareToSend(),
				new Responder(
					function (event:ResultEvent):void 
					{
						model.rotations.source = event.result as Array;
						updateSummary();
					}, 
					function (event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}));
		}
		
		private function updateSummary():void 
		{
			var cameFrom:String = "";
			var callFrom:String = "";
			var callTime:Date = null;
			var pagesVisited:int = 0;
			var searchQuery:String = "";
			
			if (model.rotations.length > 0) 
			{
				var firstRotation:TrackingPhoneRotation;
				for each (var rotation:TrackingPhoneRotation in model.rotations)
				{
					if (!firstRotation || firstRotation.TimeDisplay.time > rotation.TimeDisplay.time)
						firstRotation = rotation;

					if (rotation.RelatedPhoneCall != null && rotation.RelatedPhoneCall.Id == model.phoneCall.Id) 
						callFrom = rotation.ReferralUri;

					pagesVisited++;
				}
				
				cameFrom = firstRotation.ParentReferralUri;
				callTime = model.phoneCall.DateCreated;
				searchQuery = firstRotation.ParentReferralUri;
			}
			
			model.cameFrom = cameFrom;
			model.callFrom = callFrom;
			model.callTime = callTime;
			model.pagesVisited = pagesVisited;
			model.searchQuery = searchQuery;
		}

	}
}