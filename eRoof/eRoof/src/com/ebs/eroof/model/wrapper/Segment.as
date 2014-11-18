package com.ebs.eroof.model.wrapper
{
	import com.ebs.eroof.dto.Segments_DTO;
	
	import flash.events.EventDispatcher;
	
	import mx.collections.ArrayCollection;
	import mx.formatters.CurrencyFormatter;
	import mx.formatters.NumberFormatter;
	
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
		
		public function get totalClients():Number
		{
			return segmentDTO.ClientsCount;
		}
		
		public function get totalFacilities():Number
		{
			return segmentDTO.FacilitiesCount;
		}
		
		public function get totalSections():Number
		{
			return segmentDTO.SectionsCount;
		}
		
		public function get totalSqFt():Number
		{
			return segmentDTO.TotalSqFt;
		}
		
		public function get totalSqFtStr():String
		{
			var nf:NumberFormatter = new NumberFormatter();
			nf.useThousandsSeparator = true;
			nf.precision = 0;
			
			
			return nf.format(segmentDTO.TotalSqFt);
		}
		
		public function get totalValue():Number
		{
			return segmentDTO.TotalValue;
		}
		
		public function get totalValueStr():String
		{
			var cf:CurrencyFormatter = new CurrencyFormatter();
			cf.useThousandsSeparator = true;
			cf.precision = 2;
			
			return cf.format(segmentDTO.TotalValue);
		}
		
		public function addToCompany(cmp:Company = null):void 
		{
			if (cmp == null)
				cmp = company;
			
			if (cmp == null)
				return;
			
			if (!cmp.segmentCollection.contains(this))
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