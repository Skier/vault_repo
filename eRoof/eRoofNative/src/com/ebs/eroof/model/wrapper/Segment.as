package com.ebs.eroof.model.wrapper
{
	import com.ebs.eroof.dto.Segments_DTO;
	import com.ebs.eroof.model.company.Company;
	
	import flash.events.EventDispatcher;
	
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class Segment extends EventDispatcher
	{
		public var segmentDTO:Segments_DTO;
		
		private var _company:Company;
		public function get company():Company { return _company; }
		public function set company(value:Company):void 
		{
			_company = value;
		}
				
		private var _clientsCollection:ArrayCollection;
		public function get clientCollection():ArrayCollection 
		{
			return _clientsCollection;
		}
		
		public function get name():String
		{
			return segmentDTO.SegmentName;
		}
		
		public function addToCompany(cmp:Company = null):void 
		{
			if (cmp == null)
				cmp = company;
			
			if (cmp == null)
				return;
			
			cmp.segmentCollection.addItem(this);
			this.company = cmp;
		}
		
		public function Segment(dto:Segments_DTO)
		{
			super();

			if (dto == null)
				throw new Error("Segment::Segment() - DTO object can not be null!");
			
			segmentDTO = dto;
			_clientsCollection = new ArrayCollection();
		}
		
	}
}