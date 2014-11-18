package com.ebs.eroof.model.wrapper
{
	import com.ebs.eroof.dto.Inspections_DTO;
	
	import flash.events.EventDispatcher;

	[Bindable]
	public class Inspection extends EventDispatcher
	{
		public var inspectionDTO:Inspections_DTO;
		
		private var _section:Section;
		public function get section():Section { return _section; }
		public function set section(value:Section):void 
		{
			_section = value;
		}
		
		public function get dateStr():String
		{
			return inspectionDTO.InspectionDate.toLocaleDateString();
		}

		public function get typeStr():String
		{
			return inspectionDTO.InspectionType;
		}

		public function get companyStr():String
		{
			return inspectionDTO.InspectorCompany;
		}

		public function get nameStr():String
		{
			return inspectionDTO.InspectorName;
		}

		public function get assessment():String
		{
			return inspectionDTO.Assessment;
		}

		public function Inspection(dto:Inspections_DTO)
		{
			super(null);

			if (dto == null)
				throw new Error("Inspection::Inspection() - DTO object can not be null!");
			
			inspectionDTO = dto;
		}
		
	}
}