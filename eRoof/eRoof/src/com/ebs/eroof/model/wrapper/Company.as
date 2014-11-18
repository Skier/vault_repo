package com.ebs.eroof.model.wrapper
{
	import com.ebs.eroof.dto.Consultant_DTO;
	
	import flash.events.EventDispatcher;
	
	import mx.collections.ArrayCollection;

	[Bindable]
	public class Company extends EventDispatcher
	{
		public static const DEFAULT_COMPANY_NAME:String = "Aegis";
		
		public var name:String;

		public var consultantDTO:Consultant_DTO;

		private var _segmentCollection:ArrayCollection;
		public function get segmentCollection():ArrayCollection 
		{
			return _segmentCollection;
		}
		
		public function Company()
		{
			super();
			
			name = DEFAULT_COMPANY_NAME;
			_segmentCollection = new ArrayCollection();
		}
		
	}
}