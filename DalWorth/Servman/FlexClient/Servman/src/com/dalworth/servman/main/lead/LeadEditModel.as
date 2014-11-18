package com.dalworth.servman.main.lead
{
	import com.dalworth.servman.domain.Lead;
	
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class LeadEditModel
	{
		public var lead:Lead;
		
		public var salesReps:ArrayCollection;
		public var businessPartners:ArrayCollection;
		public var users:ArrayCollection;
		public var leadTypes:ArrayCollection;
		
		public var jobs:ArrayCollection;

		public var isBusy:Boolean;

		public var canSelectBusinessPartner:Boolean = true;
		public var canSelectSalesRep:Boolean = true;
		public var canSelectAssignedUser:Boolean = true;
		public var canViewEmployeeNotes:Boolean = true;
		public var canEditEmployeeNotes:Boolean = true;
		public var canEditCustomerNotes:Boolean = true;
		public var canUpdate:Boolean = true;
		public var canCancel:Boolean = true;
		public var canConvertToProject:Boolean = true;

		private static var _instance:LeadEditModel;
		public static function getInstance():LeadEditModel
		{
			if (_instance == null)
				_instance = new LeadEditModel(new Private());
			
			return _instance;
		}
		
		public function LeadEditModel(accessPrivate:Private) 
		{
			salesReps = new ArrayCollection();
			businessPartners = new ArrayCollection();
			users = new ArrayCollection();
			leadTypes = new ArrayCollection();
			jobs = new ArrayCollection();
		}
	}
}

class Private {}