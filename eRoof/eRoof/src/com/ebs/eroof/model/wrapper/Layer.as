package com.ebs.eroof.model.wrapper
{
	import com.ebs.eroof.dto.Layers_DTO;
	
	import flash.events.EventDispatcher;

	[Bindable]
	public class Layer extends EventDispatcher
	{
		public var layerDTO:Layers_DTO; 
		
		private var _section:Section;
		public function get section():Section { return _section; }
		public function set section(value:Section):void 
		{
			_section = value;
		}
		
		public function get layerNo():Number
		{
			return layerDTO.LayerNumber;
		}
		
		public function get layerType():String
		{
			return layerDTO.LayerType;
		}
		
		public function get description():String
		{
			return layerDTO.Description;
		}
		
		public function get attachment():String
		{
			return layerDTO.Attachment;
		}
		
		public function Layer(dto:Layers_DTO)
		{
			super(null);
			
			if (dto == null)
				throw new Error("Layer::Layer() - DTO object can not be null!");
			
			layerDTO = dto;
		}
		
	}
}