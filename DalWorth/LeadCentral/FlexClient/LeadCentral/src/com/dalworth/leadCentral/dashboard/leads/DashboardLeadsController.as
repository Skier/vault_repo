package com.dalworth.leadCentral.dashboard.leads
{
	import com.affilia.util.DateUtil;
	import com.dalworth.leadCentral.domain.Lead;
	import com.dalworth.leadCentral.domain.LeadAmountSummary;
	import com.dalworth.leadCentral.domain.enum.LeadStatusEnum;
	import com.dalworth.leadCentral.service.LeadService;
	
	import flash.events.TimerEvent;
	import flash.utils.Timer;
	
	import mx.collections.ArrayCollection;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class DashboardLeadsController
	{
		private var model:DashboardLeadsModel;
		private var view:UIComponent;
		
		private var timer:Timer;
		
		public function DashboardLeadsController(view:UIComponent)
		{
			this.view = view;
			this.model = DashboardLeadsModel.getInstance();
			
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
		
		public function setLeads(leads:ArrayCollection):void 
		{
			model.dailyCollection.removeAll();
			
			prepareDailyCollection();
			
			for each (var lead:Lead in leads)
			{
				var item:Object = getItemByDate(lead.DateCreated)
				item["total"] = (item["total"] as int) + 1;
				if (lead.LeadStatusId == LeadStatusEnum.CONVERTED) 
					item["converted"] = (item["converted"] as int) + 1;
			}
		}
		
		private function prepareDailyCollection():void 
		{
			var currentDate:Date = model.startDate;
			while(currentDate.time <= model.endDate.time) 
			{
				var result:Object = {dateStr:DateUtil.getDateStr(currentDate), total:0, converted:0};
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
			
			var result:Object = {dateStr:dateStr, total:0, converted:0};
			model.dailyCollection.addItem(result);
			return result; 
		}
		
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
			LeadService.getInstance().getLeadSummariesByLeadIds(leads, 
				new Responder(
					function (event:ResultEvent):void 
					{
						isBusy = false;
						parseAmounts(event.result as Array);
					}, 
					function (event:FaultEvent):void 
					{
						isBusy = false;
					}));
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
		
	}
}