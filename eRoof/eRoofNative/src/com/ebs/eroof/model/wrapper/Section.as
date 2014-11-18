package com.ebs.eroof.model.wrapper
{
	import com.ebs.eroof.dto.Sections_DTO;
	
	import flash.events.EventDispatcher;

	[Bindable]
	public class Section extends EventDispatcher
	{
		public var sectionDTO:Sections_DTO;
		
		private var _facility:Facility;
		public function get facility():Facility { return _facility; }
		public function set facility(value:Facility):void 
		{
			_facility = value;
		}
		
		public function get name():String
		{
			return sectionDTO.RoofName;
		}
		
		public function Section(dto:Sections_DTO)
		{
			super();

			if (dto == null)
				throw new Error("Section::Section() - DTO object can not be null!");
			
			sectionDTO = dto;
		}
		
	}
}