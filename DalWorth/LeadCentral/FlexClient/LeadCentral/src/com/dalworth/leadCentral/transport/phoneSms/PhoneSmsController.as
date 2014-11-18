package com.dalworth.leadCentral.transport.phoneSms
{
	import com.dalworth.leadCentral.domain.PhoneSms;
	import com.dalworth.leadCentral.domain.TrackingPhoneRotation;
	import com.dalworth.leadCentral.service.PhoneService;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class PhoneSmsController
	{
		private var model:PhoneSmsModel;
		private var view:UIComponent;
		
		public function PhoneSmsController(view:UIComponent)
		{
			this.view = view;
			this.model = PhoneSmsModel.getInstance();
		}
		
		public function setPhoneSms(phoneSms:PhoneSms):void 
		{
			model.phoneSms = phoneSms;
			initRotations();
		}
		
		private function initRotations():void 
		{
			model.rotations.removeAll();
			if (model.phoneSms == null)
				return;
				
			PhoneService.getInstance().getTrackingPhoneRotationsByPhoneSms(model.phoneSms,
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

					if (rotation.RelatedPhoneSms != null && rotation.RelatedPhoneSms.Id == model.phoneSms.Id) 
						callFrom = rotation.ReferralUri;

					pagesVisited++;
				}
				
				cameFrom = firstRotation.ParentReferralUri;
				callTime = model.phoneSms.DateCreated;
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