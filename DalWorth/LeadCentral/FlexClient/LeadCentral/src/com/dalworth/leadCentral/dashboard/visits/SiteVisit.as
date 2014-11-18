package com.dalworth.leadCentral.dashboard.visits
{
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class SiteVisit
	{
		public var sessionId:String;
		public var startTime:Date;
		public var endTime:Date;
		public var userHostAddress:String;
		public var cameFrom:String;
		public var callFrom:String;
		public var relatedRotations:ArrayCollection;
		public var relatedPhoneCalls:ArrayCollection;

		public function SiteVisit()
		{
			relatedRotations = new ArrayCollection();
			relatedPhoneCalls = new ArrayCollection();
		}
		
	}
}