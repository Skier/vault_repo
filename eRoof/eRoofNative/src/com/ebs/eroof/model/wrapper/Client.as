package com.ebs.eroof.model.wrapper
{
	import com.ebs.eroof.dto.Clients_DTO;
	
	import flash.events.EventDispatcher;
	
	import mx.collections.ArrayCollection;

	[Bindable]
	public class Client extends EventDispatcher
	{
		public var clientDTO:Clients_DTO;
		
		private var _segment:Segment;
		public function get segment():Segment { return _segment; }
		public function set segment(value:Segment):void 
		{
			_segment = value;
		}
				
		private var _facilityCollection:ArrayCollection;
		public function get facilityCollection():ArrayCollection 
		{
			return _facilityCollection;
		}
		
		public function get name():String
		{
			return clientDTO.ClientName;
		}
		
		public function get BriefName():String
		{
			return clientDTO.BriefName;
		}
		
		public function get Country():String
		{
			return clientDTO.Country;
		}
		
		public function get City():String
		{
			return clientDTO.City;
		}
		
		public function get EMail():String
		{
			return clientDTO.EMail;
		}
		
		public function get Phone():String
		{
			return clientDTO.Phone;
		}

		public function addToSegment(seg:Segment = null):void 
		{
			if (seg == null)
				seg = segment;
			
			if (seg == null)
				return;
			
			seg.clientCollection.addItem(this);
			this.segment = seg;
		}
		
		public function Client(dto:Clients_DTO)
		{
			super();

			if (dto == null)
				throw new Error("Client::Client() - DTO object can not be null!");
			
			clientDTO = dto;
			_facilityCollection = new ArrayCollection();
		}

	}
}