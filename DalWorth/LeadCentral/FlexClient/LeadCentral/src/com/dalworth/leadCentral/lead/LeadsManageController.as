package com.dalworth.leadCentral.lead
{
	import com.dalworth.leadCentral.domain.AmountSummary;
	import com.dalworth.leadCentral.domain.Lead;
	import com.dalworth.leadCentral.domain.LeadAmountSummary;
	import com.dalworth.leadCentral.service.LeadService;
	
	import flash.events.TimerEvent;
	import flash.utils.Timer;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class LeadsManageController
	{
		private var view:UIComponent;
		private var model:LeadsManageModel;
		
		private var timer:Timer;
		
		public function LeadsManageController(view:UIComponent)
		{
			this.model = LeadsManageModel.getInstance();
			this.view = view;
			
			initTimer();
		}
		
		private function initTimer():void 
		{
			timer = new Timer(5000);
			timer.addEventListener(TimerEvent.TIMER, 
				function (event:TimerEvent):void 
				{
					updateAmounts();
				});
			timer.start();
		}
		
		private var lastFilter:LeadFilter;
		public function refreshLeads(filter:LeadFilter):void 
		{
			model.currentPage = 1;

			if (filter == null)
				return;
			
			lastFilter = filter;
 
			//if (model.isBusy)
				//return;
			
			//model.isBusy = true;
			LeadService.getInstance().getLeadsCount(filter, 
				new Responder(
					function(event:ResultEvent):void 
					{
						//model.isBusy = false;
						model.leadsCount = event.result as int;
						refreshPaging();
					},
					function(event:FaultEvent):void 
					{
						//model.isBusy = false;
					}))
		}
		
        public function goFirst():void 
        {
        	model.currentPage = 1;
        	loadCurrentPage();
        }
        
        public function goPrev():void 
        {
        	if (model.currentPage > 1)
        	{
        		model.currentPage--;
        		loadCurrentPage();
        	}
        }
        
        public function goNext():void 
        {
        	if (model.currentPage < model.pages.length)
        	{
        		model.currentPage++;
        		loadCurrentPage();
        	}
        }
        
        public function goLast():void 
        {
        	model.currentPage = model.pages.length;
        	loadCurrentPage();
        }
        
        public function setPageSize(pgSize:int):void 
        {
        	model.pageSize = pgSize;
        	refreshPaging();
        }
        
        public function setCurrentPage(pgNum:int):void 
        {
        	model.currentPage = pgNum;
        	refreshPaging();
        }

        private function refreshPaging():void 
        {
        	var floor:int = Math.floor(model.leadsCount / model.pageSize);
            var pagesCount:int = (model.leadsCount % model.pageSize) > 0 ? (floor + 1) : floor;

            model.pages.removeAll();
            for (var i:int = 0; i < pagesCount; i++) 
            {
                var name:String = int(i + 1).toString();
                model.pages.addItem({label:name, data:i});
            }

            if (model.currentPage > pagesCount) 
            {
                model.currentPage = pagesCount;
            } else if (model.currentPage < 1)
            {
                model.currentPage = 1;
            } else
            {
	            var pg:int = model.currentPage;
	            model.currentPage = 0;
	            model.currentPage = pg;
            }   

            loadCurrentPage();
        }
        
        private var loadLeadsResponder:Responder = 
        	new Responder(
				function(event:ResultEvent):void 
				{
					//model.isBusy = false;
					model.leads.source = event.result as Array;
				},
				function(event:FaultEvent):void 
				{
					//model.isBusy = false;
					Alert.show(event.fault.message);
				});
        
        private function loadCurrentPage():void 
        {
        	if (lastFilter == null)
        		return;
        	
        	if (model.currentPage < 1)
        		return;
        		
        	var offset:int = (model.currentPage - 1) * model.pageSize;
        	var limit:int = model.pageSize;

			//model.isBusy = true;
			LeadService.getInstance().getLeadsLimit(lastFilter, offset, limit, loadLeadsResponder);
        }
        
		private function getLeadById(id:int):Lead 
		{
			for each (var lead:Lead in model.leads) 
			{
				if (lead.Id == id)
					return lead;
			}
			return null;
		}
		
        private var getAmountsResponder:Responder = 
			new Responder(
				function (event:ResultEvent):void 
				{
					isBusy = false;
					parseAmounts(event.result as Array);
					updateSummary();
				}, 
				function (event:FaultEvent):void 
				{
					isBusy = false;
					Alert.show(event.fault.message);
				});

		private var isBusy:Boolean = false;
		private function updateAmounts():void 
		{
			if (isBusy)
				return;
				
			var leads:Array = new Array();
			var leadCount:int = 0;
			for each (var lead:Lead in model.leads)
			{
				if (leadCount == 5)
					break;
					
				if (lead.RelatedQbInvoices != null && lead.RelatedQbInvoices.length > 0)
				{
					if (lead.AmountSummary == null)
					{
						leads.push(lead.Id);
						leadCount++;
					}
				}
			}
			if (leads.length == 0)
				return;

			isBusy = true;
			LeadService.getInstance().getLeadSummariesByLeadIds(leads, getAmountsResponder);
		}
		
		private function parseAmounts(amounts:Array):void 
		{
			for each (var lead:Lead in model.leads)
			{
				var summary:LeadAmountSummary = getAmountById(lead.Id, amounts);
				if (summary != null)
					lead.AmountSummary = summary;
			}
		}
		
		private function getAmountById(id:int, amounts:Array):LeadAmountSummary
		{
			for each (var amount:LeadAmountSummary in amounts) 
			{
				if (amount.LeadId == id)
					return amount;
			}
			return null;
		}
		
		private function updateSummary():void 
		{
			model.summary = new AmountSummary();
			for each (var lead:Lead in model.leads)
			{
				model.summary.push(lead.AmountSummary);
			}
		}
		
	}
}