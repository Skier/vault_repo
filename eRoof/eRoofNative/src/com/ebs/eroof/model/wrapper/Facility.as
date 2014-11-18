package com.ebs.eroof.model.wrapper
{
	import com.ebs.eroof.dto.Facilities_DTO;
	
	import flash.events.EventDispatcher;
	
	import mx.collections.ArrayCollection;

	[Bindable]
	public class Facility extends EventDispatcher
	{
		public var facilityDTO:Facilities_DTO;
		
		private var _client:Client;
		public function get client():Client { return _client; }
		public function set client(value:Client):void 
		{
			_client = value;
		}
		
		private var _sectionsCollection:ArrayCollection;
		public function get sectionsCollection():ArrayCollection 
		{ 
			return _sectionsCollection; 
		}
		
		public function get name():String
		{
			return facilityDTO.FacilityName;
		}
		
		public function Facility(dto:Facilities_DTO)
		{
			super();

			if (dto == null)
				throw new Error("Facility::Facility() - DTO object can not be null!");
			
			facilityDTO = dto;
			_sectionsCollection = new ArrayCollection();
		}
		
	}
}