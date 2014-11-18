package com.dalworth.leadCentral.lead.filter
{
	import com.affilia.util.DateUtil;
	import com.dalworth.leadCentral.domain.LeadSource;
	import com.dalworth.leadCentral.domain.LeadStatus;
	import com.dalworth.leadCentral.domain.User;
	import com.dalworth.leadCentral.lead.LeadFilter;
	
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class LeadsFilterModel
	{
		public var leadFilter:LeadFilter;
		
		public var leadStatuses:ArrayCollection;
		public var leadSources:ArrayCollection;
		public var users:ArrayCollection;
		
		public var statusesDropdownCollection:ArrayCollection;
		
		public var selectedStatusesIndices:Array;

		private static var _instance:LeadsFilterModel;
		public static function getInstance():LeadsFilterModel
		{
			if (_instance == null)
				_instance = new LeadsFilterModel(new Private());
			
			return _instance;
		}
		
		public function LeadsFilterModel(accessPrivate:Private) 
		{
			leadFilter = new LeadFilter();
			
			leadStatuses = new ArrayCollection();
			leadSources = new ArrayCollection();
			users = new ArrayCollection();
			
			selectedStatusesIndices = new Array();
			
			statusesDropdownCollection = new ArrayCollection(["New & Pending","Cancelled","Converted"]);
		}
		
		public function getFilterDescription():String
		{
			var result:String = "";
			
			if (leadFilter.LeadStatuses != null && leadFilter.LeadStatuses.length > 0)
			{
				if (result != "")
					result += ", ";
					
				result += "Statuses:";
				for each (var i:int in leadFilter.LeadStatuses)
				{
					result += (getLeadStatusById(i).Name + ";");
				}
			}
			
			if (leadFilter.DateFrom != null)
			{
				if (result != "") 
					result += ", ";
				result += " After: ";
				result += DateUtil.getDateStr(leadFilter.DateFrom);
			}
			
			if (leadFilter.DateTo != null)
			{
				if (result != "") 
					result += ", ";
				result += "Before: ";
				result += DateUtil.getDateStr(leadFilter.DateTo);
			}
			
			if (leadFilter.LeadSourceId > 0)
			{
				if (result != "") 
					result += ", ";
				result += "Lead Source:";
				result += getLeadSourceById(leadFilter.LeadSourceId).Name;
			}
			
			if (leadFilter.AssignedToUserId > 0)
			{
				if (result != "") 
					result += ", ";
				result += "Assigned To: ";
				result += getUserById(leadFilter.AssignedToUserId).Name;
			}
			
			if (leadFilter.CreatedByUserId > 0)
			{
				if (result != "") 
					result += ", ";
				result += "Created  By: ";
				result += getUserById(leadFilter.CreatedByUserId).Name;
			}
			
			return (result == "" ? "[no filter]" : result);
		}
		
		private function getLeadStatusById(id:int):LeadStatus
		{
			if (leadStatuses != null && leadStatuses.length > 0)
			{
				for each (var item:LeadStatus in leadStatuses)
				{
					if (item.Id == id)
						return item;
				}
			}
			return null;
		}
		
		private function getLeadSourceById(id:int):LeadSource
		{
			if (leadSources != null && leadSources.length > 0)
			{
				for each (var item:LeadSource in leadSources)
				{
					if (item.Id == id)
						return item;
				}
			}
			return null;
		}
		
		private function getUserById(id:int):User
		{
			if (users != null && users.length > 0)
			{
				for each (var item:User in users)
				{
					if (item.Id == id)
						return item;
				}
			}
			return null;
		}
		
	}
}	

class Private {}
