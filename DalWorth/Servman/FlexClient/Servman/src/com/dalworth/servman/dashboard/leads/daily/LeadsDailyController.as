package com.dalworth.servman.dashboard.leads.daily
{
	import com.affilia.util.DateUtil;
	import com.dalworth.servman.domain.Lead;
	import com.dalworth.servman.domain.LeadStatus;
	
	import mx.collections.ArrayCollection;
	import mx.core.UIComponent;
	
	public class LeadsDailyController
	{
		private var model:LeadsDailyModel;
		private var view:UIComponent;
		
		public function LeadsDailyController(view:UIComponent)
		{
			this.view = view;
			this.model = LeadsDailyModel.getInstance();
		}
		
		public function setLeads(leads:ArrayCollection):void 
		{
			model.dailyCollection.removeAll();
			
			prepareDailyCollection();
			
			for each (var lead:Lead in leads)
			{
				var item:Object = getItemByDate(lead.DateCreated)
				item["total"] = (item["total"] as int) + 1;
				if (lead.LeadStatusId == LeadStatus.STATUS_CONVERTED_ID) 
					item["converted"] = (item["converted"] as int) + 1;
			}
		}
		
		private function prepareDailyCollection():void 
		{
			var currentDate:Date = model.startDate;
			while(currentDate.time < model.endDate.time) 
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
		
	}
}