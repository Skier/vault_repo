package com.dalworth.leadCentral.transport.leadForm
{
	import com.dalworth.leadCentral.domain.LeadForm;
	import com.dalworth.leadCentral.domain.TrackingPhoneRotation;
	import com.dalworth.leadCentral.service.PhoneService;
	import com.dalworth.leadCentral.transport.leadForm.LeadFormModel;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class LeadFormController
	{
		private var model:LeadFormModel;
		private var view:UIComponent;
		
		public function LeadFormController(view:UIComponent)
		{
			this.view = view;
			this.model = LeadFormModel.getInstance();
		}
		
		public function setLeadForm(leadForm:LeadForm):void 
		{
			model.leadForm = leadForm;
			initRotations();
		}
		
		private function initRotations():void 
		{
			model.rotations.removeAll();
			if (model.leadForm == null)
				return;
				
			PhoneService.getInstance().getTrackingPhoneRotationsByLeadForm(model.leadForm,
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

					if (rotation.RelatedWebForm != null && rotation.RelatedWebForm.Id == model.leadForm.Id) 
						callFrom = rotation.ReferralUri;

					pagesVisited++;
				}
				
				cameFrom = firstRotation.ParentReferralUri;
				callTime = model.leadForm.DateCreated;
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