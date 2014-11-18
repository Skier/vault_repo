package com.ebs.eroof.model.wrapper
{
	import com.ebs.eroof.dto.CorePhotos_DTO;
	
	import flash.events.EventDispatcher;
	
	import mx.utils.UIDUtil;

	[Bindable]
	public class CorePhoto extends EventDispatcher
	{
		public var corePhotoDTO:CorePhotos_DTO;
		public var uniqueId:String;
		
		private var _section:Section;
		public function get section():Section { return _section; }
		public function set section(value:Section):void 
		{
			_section = value;
			uniqueId = UIDUtil.createUID();
		}
		
		public function get dateTaken():Date
		{
			return corePhotoDTO.PhotoDate;
		}
		
		public function get description():String
		{
			return corePhotoDTO.Description;
		}
		
		public function CorePhoto(dto:CorePhotos_DTO)
		{
			super(null);

			if (dto == null)
				throw new Error("CorePhoto::CorePhoto() - DTO object can not be null!");
			
			corePhotoDTO = dto;
		}
		
	}
}