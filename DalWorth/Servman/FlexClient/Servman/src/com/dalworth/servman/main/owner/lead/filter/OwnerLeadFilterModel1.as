package com.dalworth.servman.main.owner.lead.filter
{
	import com.affilia.util.DateUtil;
	import com.dalworth.servman.domain.BusinessPartner;
	import com.dalworth.servman.domain.LeadStatus;
	import com.dalworth.servman.domain.LeadType;
	import com.dalworth.servman.domain.SalesRep;
	import com.dalworth.servman.domain.User;
	import com.dalworth.servman.main.lead.LeadFilter;
	
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class OwnerLeadFilterModel1
	{
		public var leadFilter:LeadFilter;
		
		public var leadStatuses:ArrayCollection;
		public var leadTypes:ArrayCollection;
		public var salesReps:ArrayCollection;
		public var businessPartners:ArrayCollection;
		public var users:ArrayCollection;
		
		public var selectedStatusesIndices:Array;

		private static var _instance:OwnerLeadFilterModel;
		public static function getInstance():OwnerLeadFilterModel
		{
			if (_instance == null)
				_instance = new OwnerLeadFilterModel(new Private());
			
			return _instance;
		}
		
		public function OwnerLeadFilterModel1(accessPrivate:Private) 
		{
			leadFilter = new LeadFilter();
			
			leadStatuses = new ArrayCollection();
			leadTypes = new ArrayCollection();
			salesReps = new ArrayCollection();
			businessPartners = new ArrayCollection();
			users = new ArrayCollection();
			
			selectedStatusesIndices = new Array();
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
			
			if (leadFilter.LeadTypeId > 0)
			{
				if (result != "") 
					result += ", ";
				result += "Type:";
				result += getLeadTypeById(leadFilter.LeadTypeId).Name;
			}
			
			if (leadFilter.SalesRepId > 0)
			{
				if (result != "") 
					result += ", ";
				result += "Sales Rep:";
				result += getSalesRepById(leadFilter.SalesRepId).ShowAs;
			}
			
			if (leadFilter.BusinessPartnerId > 0)
			{
				if (result != "") 
					result += ", ";
				result += "Business Partner:";
				result += getBusinessPartnerById(leadFilter.BusinessPartnerId).ShowAs;
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
		
		private function getLeadTypeById(id:int):LeadType
		{
			if (leadTypes != null && leadTypes.length > 0)
			{
				for each (var item:LeadType in leadTypes)
				{
					if (item.Id == id)
						return item;
				}
			}
			return null;
		}
		
		private function getSalesRepById(id:int):SalesRep
		{
			if (salesReps != null && salesReps.length > 0)
			{
				for each (var item:SalesRep in salesReps)
				{
					if (item.Id == id)
						return item;
				}
			}
			return null;
		}
		
		private function getBusinessPartnerById(id:int):BusinessPartner
		{
			if (businessPartners != null && businessPartners.length > 0)
			{
				for each (var item:BusinessPartner in businessPartners)
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
